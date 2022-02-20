using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.Models;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using FRIO.MAR.UI.WEB.SITE.Constants;
using GS.TOOLS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    //[Filters.MenuFilter(Constants.VentanasSoporte.ProductosInternos)]
    public class ProductosController : BaseController
    {
        private readonly IUtilidadRepository _utilidadRepository;
        private readonly IProductoAppService _ProductoAppService;

        public ProductosController(IUtilidadRepository utilidadRepository, IProductoAppService ProductoAppService, ILogInfraServices logInfraServices) : base(logInfraServices)
        {
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
            ViewBag.EsNuevo = true;

            ProductoModel model = new ProductoModel();
            try
            {    
                ViewData["unidadMeida"] = new SelectList(_utilidadRepository.GetUnidadesMedida(), "Codigo", "Nombre");
                ViewData["IVA"] = new SelectList(_utilidadRepository.GetImpuestos(1), "Porcentaje", "Nombre");

                if (!string.IsNullOrEmpty(Id))
                {
                    ViewBag.EsNuevo = false;
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
            ViewBag.EsNuevo = true;
            try
            {
                ViewData["unidadMeida"] = new SelectList(_utilidadRepository.GetUnidadesMedida(), "Codigo", "Nombre");
                ViewData["IVA"] = new SelectList(_utilidadRepository.GetImpuestos(1), "Porcentaje", "Nombre");

                if (ModelState.IsValid)
                {
                    var usr = GetUserLogin();
                    model.Ip = usr.IPLogin;
                    model.Usuario = usr.IdUsuario;

                    if (string.IsNullOrEmpty(model.Id))
                    {
                        var result = _ProductoAppService.CrearProducto(model);
                        if (result.TieneErrores) throw new Exception(result.MensajeError);
                        if (result.Estado)
                        {
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Producto registrado con éxito");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, result.Mensaje);
                        }
                    }
                    else
                    {
                        ViewBag.EsNuevo = false;
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
                    return Json(new ResponseToViewDto { Estado = false, Mensaje = "Error al eliminar el Producto" });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }
    }
}
