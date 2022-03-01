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
    public class VentasController : BaseController
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IVentasDomainService _ventasDomainService;

        public VentasController(
            IProductoRepository productoRepository,
            ISucursalRepository sucursalRepository,
            IClienteRepository clienteRepository,
            IVentasDomainService ventasDomainService, 
            ILogInfraServices logInfraServices) : base(logInfraServices)
        {
            _productoRepository = productoRepository;
            _sucursalRepository = sucursalRepository;
            _clienteRepository = clienteRepository;
            _ventasDomainService = ventasDomainService;
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
                CargarDatos();

                if (!string.IsNullOrEmpty(Id))
                {
                    var result = _ventasDomainService.ConsultarFactura(long.Parse(Crypto.DescifrarId(Id)), Tipo);
                    if (result.TieneErrores) throw new Exception(result.MensajeError);
                    if (result.Estado)
                    {
                        FacturaModel factura = result.Data;
                        HttpContext.Session.SetString("Detalle", JsonConvert.SerializeObject(factura.Detalle));
                        //HttpContext.Session.SetString("FormaPago", JsonConvert.SerializeObject(factura.Detalle));
                        return View(factura);
                    }
                    else
                    {
                        TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_ERROR.Replace("{Mensaje_Respuesta}", result.Mensaje);
                    }
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

                List<VentasDomainServiceResultDto> facturas = new List<VentasDomainServiceResultDto>();
                var result = _ventasDomainService.ListarFacturas(Estados);
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
        public IActionResult AgregarProducto(DetalleFacturaModel item, EstadoFactura Estado)
        {
            //CargarDatos();
            FacturaModel model = new FacturaModel();

            List<DetalleFacturaModel> detalles = JsonConvert.DeserializeObject<List<DetalleFacturaModel>>(HttpContext.Session.GetString("Detalle") ?? "[]");

            try
            {
                model.EstadoFactura = Estado;
                model.Detalle = new List<DetalleFacturaModel>();

                item.CantidadDec = decimal.Parse(Utilidades.DepuraStrConvertNum(item.Cantidad));
                item.PrecioUnitarioDec = decimal.Parse(Utilidades.DepuraStrConvertNum(item.PrecioUnitario));
                item.IvaPorcentajeDec = decimal.Parse(Utilidades.DepuraStrConvertNum(item.IvaPorcentaje));

                item.SubTotalDec = item.CantidadDec * item.PrecioUnitarioDec;
                item.IvaValorDec = item.SubTotalDec * (item.IvaPorcentajeDec / 100);
                item.TotalDec = item.SubTotalDec + item.IvaValorDec;

                item.Id = Guid.NewGuid().ToString();
                item.SubTotal = Utilidades.DoubleToString_FrontCO(item.SubTotalDec, 2);
                item.IvaValor = Utilidades.DoubleToString_FrontCO(item.IvaValorDec, 2);
                item.Total = Utilidades.DoubleToString_FrontCO(item.TotalDec, 2);

                detalles.Add(item);
                model.Detalle = detalles;

                HttpContext.Session.SetString("Detalle", JsonConvert.SerializeObject(detalles));

                return PartialView("_DetalleFactura", model);
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = true, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

        [HttpPost]
        public IActionResult CalcularFactura(EstadoFactura Estado)
        {
            try
            {
                FacturaModel model = new FacturaModel();

                List<DetalleFacturaModel> detalles = JsonConvert.DeserializeObject<List<DetalleFacturaModel>>(HttpContext.Session.GetString("Detalle") ?? "[]");
                //List<DetalleFacturaModel> formaPago = JsonConvert.DeserializeObject<List<DetalleFacturaModel>>(HttpContext.Session.GetString("FormaPago") ?? "[]");
                model.EstadoFactura = Estado;

                model.Totales = new TotalesFacturaModel(detalles);

                return PartialView("_Totales", model);
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = true, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

        public IActionResult ListarFacturasProforma()
        {
            try
            {
                EstadoFactura[] Estados = new EstadoFactura[] { EstadoFactura.Proforma };
                
                List<VentasDomainServiceResultDto> facturas = new List<VentasDomainServiceResultDto>();
                var result = _ventasDomainService.ListarFacturas(Estados);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    facturas = result.Data;
                    return PartialView("_FacturasProforma", facturas);
                }
                return Json(new ResponseToViewDto { Estado = true, Mensaje = result.Mensaje });

            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = true, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

        private void CargarDatos()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in _clienteRepository.GetClientes())
            {
                items.Add(new SelectListItem
                {
                    Text = item.Identificacion + " - " + item.RazonSocial + " - " + item.NombreComercial,
                    Value = item.ClienteId.ToString(),
                });
            }

            ViewData["clientes"] = new SelectList(items, "Value", "Text");
            ViewData["sucursales"] = new SelectList(_sucursalRepository.GetSucursales(), "SucursalId", "Nombre");

            ViewBag.Producto = _productoRepository.GetProductos().Where(x => x.TipoProducto == TipoProducto.Bien).ToList();
            ViewBag.Servicios = _productoRepository.GetProductos().Where(x => x.TipoProducto == TipoProducto.Servicio).ToList();
        }
    }
}
