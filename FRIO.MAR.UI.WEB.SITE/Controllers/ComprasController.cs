using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.Models;
using FRIO.MAR.APPLICATION.CORE.Utilities;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using FRIO.MAR.UI.WEB.SITE.Constants;
using GS.TOOLS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    //[Filters.MenuFilter(Constants.VentanasSoporte.Sucursales)]
    public class ComprasController : BaseController
    {
        private readonly IUtilidadRepository _utilidadRepository;
        private readonly IProductoClienteRepository _productoClienteRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IProveedorRepository _proveedorRepository;
        private readonly IComprasDomainService _comprasDomainService;

        public ComprasController(
            IUtilidadRepository utilidadRepository,
            IProductoClienteRepository productoClienteRepository,
            IProductoRepository productoRepository,
            ISucursalRepository sucursalRepository,
            IProveedorRepository proveedorRepository,
            IComprasDomainService comprasDomainService, 
            ILogInfraServices logInfraServices) : base(logInfraServices)
        {
            _utilidadRepository = utilidadRepository;
            _productoClienteRepository = productoClienteRepository;
            _productoRepository = productoRepository;
            _sucursalRepository = sucursalRepository;
            _proveedorRepository = proveedorRepository;
            _comprasDomainService = comprasDomainService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Factura(string Id, EstadoFactura Tipo)
        {
            FacturaModel model = new FacturaModel();
            try
            {
                HttpContext.Session.SetString("Detalle", JsonConvert.SerializeObject(new List<DetalleFacturaModel>()));
                HttpContext.Session.SetString("FormaPago", JsonConvert.SerializeObject(new List<FormaPagoFacturaModel>()));

                CargarDatos();

                if (!string.IsNullOrEmpty(Id))
                {
                    var result = _comprasDomainService.ConsultarFactura(long.Parse(Crypto.DescifrarId(Id)), Tipo);
                    if (result.TieneErrores) throw new Exception(result.MensajeError);
                    if (result.Estado)
                    {
                        model = result.Data;
                        HttpContext.Session.SetString("Detalle", JsonConvert.SerializeObject(model.Detalle));
                        HttpContext.Session.SetString("FormaPago", JsonConvert.SerializeObject(model.FormaPago));
                    }
                    else
                    {
                        TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_ERROR.Replace("{Mensaje_Respuesta}", result.Mensaje);
                    }
                }
                else
                {
                    model.EstadoFactura = Tipo;
                }
            }
            catch (System.Exception ex)
            {
                model = new FacturaModel();
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }
            return View(model);
        }

        public IActionResult ListarFacturasBorrador()
        {
            try
            {
                EstadoFactura[] Estados = new EstadoFactura[] { EstadoFactura.Borrador };

                List<ComprasDomainServiceResultDto> facturas = new List<ComprasDomainServiceResultDto>();
                var result = _comprasDomainService.ListarFacturas(Estados);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    facturas = result.Data;
                    return PartialView("_FacturasBorrador", facturas);
                }

                return Json(new ResponseToViewDto { Estado = true, Mensaje = result.Mensaje });
            }
            catch (System.Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = true, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

        [HttpPost]
        public JsonResult EliminarFormaPago(string Id)
        {
            try
            {
                List<FormaPagoFacturaModel> formaPagos = JsonConvert.DeserializeObject<List<FormaPagoFacturaModel>>(HttpContext.Session.GetString("FormaPago") ?? "[]");
                formaPagos.RemoveAll(x => x.Id == Id);
                HttpContext.Session.SetString("FormaPago", JsonConvert.SerializeObject(formaPagos));
                return Json(new ResponseToViewDto { Estado = true });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

        [HttpPost]
        public JsonResult EliminarItem(string Id)
        {
            try
            {
                List<DetalleFacturaModel> detalles = JsonConvert.DeserializeObject<List<DetalleFacturaModel>>(HttpContext.Session.GetString("Detalle") ?? "[]");
                detalles.RemoveAll(x => x.Id == Id);
                HttpContext.Session.SetString("Detalle", JsonConvert.SerializeObject(detalles));
                return Json(new ResponseToViewDto { Estado = true });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

        [HttpPost]
        public JsonResult AgregarServicio(DetalleFacturaModel Servicio, long ProductoClienteId, long Cliente, EstadoFactura Estado)
        {
            try
            {
                List<DetalleFacturaModel> detalles = JsonConvert.DeserializeObject<List<DetalleFacturaModel>>(HttpContext.Session.GetString("Detalle") ?? "[]");
                var producto = _productoClienteRepository.GetProducto(Cliente, ProductoClienteId);
                if (producto != null)
                {
                    Servicio.Descripcion += $" de {producto.Codigo} - {producto.Nombre}";
                    Servicio.CodigoSeguimiento = _utilidadRepository.GenerarCodigoSeguimientoProducto(producto.ProductoClienteId);
                }

                detalles.Add(CalcularLinea(Servicio));
                HttpContext.Session.SetString("Detalle", JsonConvert.SerializeObject(detalles));

                return Json(new ResponseToViewDto { Estado = true });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

        [HttpPost]
        public JsonResult AgregarProducto(DetalleFacturaModel Producto)
        {
            try
            {
                List<DetalleFacturaModel> detalles = JsonConvert.DeserializeObject<List<DetalleFacturaModel>>(HttpContext.Session.GetString("Detalle") ?? "[]");

                detalles.Add(CalcularLinea(Producto));

                HttpContext.Session.SetString("Detalle", JsonConvert.SerializeObject(detalles));

                return Json(new ResponseToViewDto { Estado = true });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

        [HttpPost]
        public JsonResult AgregarFormaPago(FormaPagoFacturaModel FormaPago)
        {
            try
            {
                List<FormaPagoFacturaModel> formaPagos = JsonConvert.DeserializeObject<List<FormaPagoFacturaModel>>(HttpContext.Session.GetString("FormaPago") ?? "[]");
                FormaPago.Id = Guid.NewGuid().ToString();

                FormaPago.ValorDec = decimal.Parse(Utilidades.DepuraStrConvertNum(FormaPago.Valor));
                formaPagos.Add(FormaPago);

                HttpContext.Session.SetString("FormaPago", JsonConvert.SerializeObject(formaPagos));

                return Json(new ResponseToViewDto { Estado = true });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

        [HttpPost]
        public PartialViewResult CalcularFactura(EstadoFactura Estado)
        {
            FacturaModel model = new FacturaModel();

            List<DetalleFacturaModel> detalles = JsonConvert.DeserializeObject<List<DetalleFacturaModel>>(HttpContext.Session.GetString("Detalle") ?? "[]");
            List<FormaPagoFacturaModel> formaPagos = JsonConvert.DeserializeObject<List<FormaPagoFacturaModel>>(HttpContext.Session.GetString("FormaPago") ?? "[]");

            model.EstadoFactura = Estado;

            model.Totales = new TotalesFacturaModel(detalles, formaPagos);

            return PartialView("_Totales", model);
        }

        [HttpPost]
        public JsonResult ActualizarItem(string Tipo, string Id, string Valor)
        {
            try
            {
                List<DetalleFacturaModel> detalles = JsonConvert.DeserializeObject<List<DetalleFacturaModel>>(HttpContext.Session.GetString("Detalle") ?? "[]");

                detalles.ForEach(item =>
                {
                    if (item.Id == Id)
                    {
                        if (Tipo == "Cantidad")
                        {
                            item.Cantidad = Valor;
                            item.CantidadDec = decimal.Parse(Utilidades.DepuraStrConvertNum(Valor));
                        }
                        if (Tipo == "Precio")
                        {
                            item.PrecioUnitario = Valor;
                            item.PrecioUnitarioDec = decimal.Parse(Utilidades.DepuraStrConvertNum(Valor));
                        }
                    }

                    item = CalcularLinea(item);
                });

                HttpContext.Session.SetString("Detalle", JsonConvert.SerializeObject(detalles));

                return Json(new ResponseToViewDto { Estado = true });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = true, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

        public PartialViewResult ConstruirDetalles(EstadoFactura Estado)
        {
            FacturaModel model = new FacturaModel
            {
                EstadoFactura = Estado,
                Detalle = JsonConvert.DeserializeObject<List<DetalleFacturaModel>>(HttpContext.Session.GetString("Detalle") ?? "[]")
            };
            return PartialView("_DetalleFactura", model);
        }
        
        public PartialViewResult ConstruirFormasPago(EstadoFactura Estado)
        {
            FacturaModel model = new FacturaModel
            {
                EstadoFactura = Estado,
                FormaPago = JsonConvert.DeserializeObject<List<FormaPagoFacturaModel>>(HttpContext.Session.GetString("FormaPago") ?? "[]")
            };
            return PartialView("_FormasPago", model);
        }

        [HttpPost]
        public IActionResult GuardarFactura(FacturaModel model)
        {
            try
            {
                var usr = GetUserLogin();

                //model.EstadoFactura = EstadoFactura.Proforma;

                model.Detalle = JsonConvert.DeserializeObject<List<DetalleFacturaModel>>(HttpContext.Session.GetString("Detalle") ?? "[]");
                model.FormaPago = JsonConvert.DeserializeObject<List<FormaPagoFacturaModel>>(HttpContext.Session.GetString("FormaPago") ?? "[]");
                model.Ip = usr.IPLogin;
                model.Usuario = usr.IdUsuario;

                var result = _comprasDomainService.GuardarFactura(model);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    HttpContext.Session.Remove("Detalle");
                    HttpContext.Session.Remove("FormaPago");
                    GS.TOOLS.GSUtilities.ClearMemory();
                }
                return Json(new ResponseToViewDto { Estado = result.Estado, Mensaje = result.Mensaje });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = true, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });

            }
        }

        public IActionResult Facturar(FacturaModel model)
        {
            try
            {
                var usr = GetUserLogin();

                model.EstadoFactura = EstadoFactura.Facturado;

                model.Detalle = JsonConvert.DeserializeObject<List<DetalleFacturaModel>>(HttpContext.Session.GetString("Detalle") ?? "[]");
                model.FormaPago = JsonConvert.DeserializeObject<List<FormaPagoFacturaModel>>(HttpContext.Session.GetString("FormaPago") ?? "[]");
                model.Totales = new TotalesFacturaModel(model.Detalle, model.FormaPago);

                var validaciones = _comprasDomainService.ValidarFactura(model);
                if (validaciones.TieneErrores) throw new Exception(validaciones.MensajeError);
                if (!validaciones.Estado)
                {
                    return Json(new ResponseToViewDto { Estado = validaciones.Estado, Mensaje = validaciones.Mensaje });
                }

                model.Ip = usr.IPLogin;
                model.Usuario = usr.IdUsuario;

                var result = _comprasDomainService.GuardarFactura(model);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    HttpContext.Session.Remove("Detalle");
                    HttpContext.Session.Remove("FormaPago");
                    GS.TOOLS.GSUtilities.ClearMemory();
                }
                return Json(new ResponseToViewDto { Estado = result.Estado, Mensaje = result.Mensaje });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = true, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });

            }
        }

        [HttpPost]
        public JsonResult EliminarFactura(string Id)
        {
            try
            {
                var result = _comprasDomainService.EliminarFactura(long.Parse(Crypto.DescifrarId(Id)));
                if (result.TieneErrores) throw new Exception(result.MensajeError);

                return Json(new ResponseToViewDto { Estado = result.Estado, Mensaje =(result.Estado? "Factura eliminada correctamente" : result.Mensaje) });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = true, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }


        [HttpPost]
        public JsonResult ActualizarProductosCliente(long Cliente)
        {
            try
            {
                var result = _productoClienteRepository.GetProductos(Cliente);

                return Json(new ResponseToViewDto { Estado = result != null, Data = result });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = true, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

        #region metodos privados
        private void CargarDatos()
        {
            ViewData["sucursales"] = new SelectList(_sucursalRepository.GetSucursales(), "SucursalId", "Nombre");

            ViewBag.Proveedores = _proveedorRepository.GetProveedores();
            ViewBag.Producto = _productoRepository.GetProductos().Where(x => x.TipoProducto == TipoProducto.Bien).ToList();
            ViewBag.Servicios = _productoRepository.GetProductos().Where(x => x.TipoProducto == TipoProducto.Servicio).ToList();
        }

        private DetalleFacturaModel CalcularLinea(DetalleFacturaModel Producto)
        {
            Producto.CantidadDec = decimal.Parse(Utilidades.DepuraStrConvertNum(Producto.Cantidad));
            Producto.PrecioUnitarioDec = decimal.Parse(Utilidades.DepuraStrConvertNum(Producto.PrecioUnitario));
            Producto.IvaPorcentajeDec = decimal.Parse(Utilidades.DepuraStrConvertNum(Producto.IvaPorcentaje));

            Producto.SubTotalDec = Producto.CantidadDec * Producto.PrecioUnitarioDec;
            Producto.IvaValorDec = Producto.SubTotalDec * (Producto.IvaPorcentajeDec / 100);
            Producto.TotalDec = Producto.SubTotalDec + Producto.IvaValorDec;

            Producto.Id = Guid.NewGuid().ToString();
            Producto.SubTotal = Utilidades.DoubleToString_FrontCO(Producto.SubTotalDec, 2);
            Producto.IvaValor = Utilidades.DoubleToString_FrontCO(Producto.IvaValorDec, 2);
            Producto.Total = Utilidades.DoubleToString_FrontCO(Producto.TotalDec, 2);
            return Producto;
        }
        #endregion
    
    }
}
