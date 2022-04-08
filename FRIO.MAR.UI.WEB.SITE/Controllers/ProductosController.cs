using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Services;
using FRIO.MAR.APPLICATION.CORE.Models;
using FRIO.MAR.APPLICATION.CORE.Utilities;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using FRIO.MAR.UI.WEB.SITE.Constants;
using GS.TOOLS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Filters.MenuFilter(Constants.VentanasSoporte.ProductosInternos)]
    public class ProductosController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IStorageService _storageService;
        private readonly IBodegaRepository _bodegaRepository;
        private readonly IInventarioDomainService _inventarioDomainService;
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IUtilidadRepository _utilidadRepository;
        private readonly IProductoAppService _ProductoAppService;

        public ProductosController(
            IConfiguration configuration,
            IStorageService storageService,
            IBodegaRepository bodegaRepository,
            IInventarioDomainService inventarioDomainService,
            ISucursalRepository sucursalRepository,
            IUtilidadRepository utilidadRepository, 
            IProductoAppService ProductoAppService, 
            ILogInfraServices logInfraServices) : base(logInfraServices)
        {
            _configuration = configuration;
            _storageService = storageService;
            _bodegaRepository = bodegaRepository;
            _inventarioDomainService = inventarioDomainService;
            _sucursalRepository = sucursalRepository;
            _utilidadRepository = utilidadRepository;
            _ProductoAppService = ProductoAppService;
        }

        public IActionResult Index()
        {
            List<ProductoModel> Productoes = new List<ProductoModel>();
            try
            {
                ViewData["unidadMeida"] = _utilidadRepository.GetUnidadesMedida();
                ViewData["IVA"] = _utilidadRepository.GetImpuestos(1);
                ViewData["sucursales"] = new SelectList(_sucursalRepository.GetSucursales(), "SucursalId", "Nombre");

                var result = _ProductoAppService.ConsultarProductos();
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    Productoes = result.Data;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_ERROR.Replace("{Mensaje_Respuesta}", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(Productoes);
        }

        [HttpGet]
        public IActionResult Registrar(string Id)
        {
            ViewBag.EsNuevo = string.IsNullOrEmpty(Id);

            ProductoModel model = new ProductoModel();
            try
            {
                CargarDatos();

                if (!string.IsNullOrEmpty(Id))
                {
                    var result = _ProductoAppService.ConsultarProducto(Id);
                    if (result.TieneErrores) throw new Exception(result.MensajeError);
                    if (result.Estado)
                    {
                        model = result.Data;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Mensaje);
                    }
                }
            }
            catch (System.Exception ex)
            {
                model = new ProductoModel();
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Registrar(ProductoModel model)
        {
            ViewBag.EsNuevo = string.IsNullOrEmpty(model.Id);
            try
            {
                CargarDatos();

                if (model.RegistrarInventario)
                {
                    bool pasoValidaciones = true;

                    if (model.TipoInventario == null)
                    {
                        pasoValidaciones = false;
                        ModelState.AddModelError("TipoInventario", DomainConstants.MENSAJE_CAMPO_REQUIRED);
                    }

                    if (model.Sucursal == null)
                    {
                        pasoValidaciones = false;
                        ModelState.AddModelError("Sucursal", DomainConstants.MENSAJE_CAMPO_REQUIRED);
                    }

                    if (model.Bodega == null)
                    {
                        pasoValidaciones = false;
                        ModelState.AddModelError("Bodega", DomainConstants.MENSAJE_CAMPO_REQUIRED);
                    }

                    if (string.IsNullOrEmpty(model.CantidadStr))
                    {
                        pasoValidaciones = false;
                        ModelState.AddModelError("CantidadStr", DomainConstants.MENSAJE_CAMPO_REQUIRED);
                    }

                    if (!pasoValidaciones)
                    {
                        return View(model);
                    }
                }

                if (ModelState.IsValid)
                {
                    var usr = GetUserLogin();
                    model.Ip = usr.IPLogin;
                    model.Usuario = usr.IdUsuario;
                    model.Cantidad = Convert.ToDecimal(Utilidades.DepuraStrConvertNum(model.CantidadStr));
                    model.PrecioUnitario = Convert.ToDecimal(Utilidades.DepuraStrConvertNum(model.PrecioUnitarioStr));


                    if (string.IsNullOrEmpty(model.Id))
                    {
                        var result = _ProductoAppService.CrearProducto(model);
                        if (result.TieneErrores) throw new Exception(result.MensajeError);
                        if (result.Estado)
                        {
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Producto registrado con éxito");

                            if (model.RegistrarInventario)
                            {
                                InventarioMantenimientoDto mantenimiento = new InventarioMantenimientoDto
                                {
                                    productos = model.ProductoId,
                                    cantidad = model.Cantidad,
                                    precio = model.PrecioUnitario,
                                    tipoInventario = model.TipoInventario,
                                    tipoMovimiento = TipoMovimientoInventario.Entrada,
                                    subTipoMovimiento = SubtipoMovimientoInventario.Manual,
                                    bodegas = model.Bodega ?? 0,
                                    sucursal = model.Sucursal ?? 0,
                                    //mantenimiento.cufeFactura = productoForm.cufe;
                                    //numeroFactura = productoForm.numeroDocumento,
                                    unidadMedida = model.UnidadMedida,
                                    proveedor = model.Proveedor ?? 0,
                                    motivo = "Registro de nuevo producto"
                                };

                                long IdInventarioMovimiento = 0;

                                var resultInventario = _inventarioDomainService.QryInventarioMovimiento(mantenimiento, usr.IdUsuario, usr.IPLogin, ref IdInventarioMovimiento);

                            }

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, result.Mensaje);
                        }
                    }
                    else
                    {
                        var result = _ProductoAppService.EditarProducto(model);
                        if (result.TieneErrores) throw new Exception(result.MensajeError);
                        if (result.Estado)
                        {
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Producto actualizado con éxito");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, result.Mensaje);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        

        [HttpPost]
        public JsonResult Eliminar(string Id)
        {
            try
            {
                var usr = GetUserLogin();
                var result = _ProductoAppService.EliminarProducto(Id, usr.IPLogin, usr.IdUsuario);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    return Json(new ResponseToViewDto { Estado = true, Mensaje = "Producto eliminado con éxito" });
                }
                else
                {
                    return Json(new ResponseToViewDto { Estado = false, Mensaje = (string.IsNullOrEmpty(result.Mensaje) ? "Error al eliminar el Producto" : result.Mensaje) });

                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

        private void CargarDatos()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in _utilidadRepository.GetUnidadesMedida())
            {
                items.Add(new SelectListItem
                {
                    Text = item.Simbolo + " - " + item.Comentario,
                    Value = item.Simbolo,
                });
            }

            ViewData["unidadMeida"] = new SelectList(items, "Value", "Text");


            ViewData["IVA"] = _utilidadRepository.GetImpuestos(1);
            ViewData["sucursales"] = new SelectList(_sucursalRepository.GetSucursales(), "SucursalId", "Nombre");
            //ViewData["bodegas"] = new SelectList(_bodegaRepository.GetBodegas(), "BodegaId", "Nombre");
        }
    }
}
