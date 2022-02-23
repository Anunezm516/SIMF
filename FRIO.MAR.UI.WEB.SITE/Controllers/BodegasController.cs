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
    [Filters.MenuFilter(Constants.VentanasSoporte.Bodegas)]
    public class BodegasController : BaseController
    {
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IBodegaAppService _BodegaAppService;

        public BodegasController(
            ISucursalRepository sucursalRepository,
            IBodegaAppService BodegaAppService, 
            ILogInfraServices logInfraServices) : base(logInfraServices)
        {
            _sucursalRepository = sucursalRepository;
            _BodegaAppService = BodegaAppService;
        }

        public IActionResult Index()
        {
            List<BodegaModel> Bodegaes = new List<BodegaModel>();
            try
            {
                var result = _BodegaAppService.ConsultarBodegas();
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    Bodegaes = result.Data;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_ERROR.Replace("{Mensaje_Respuesta}", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(Bodegaes);
        }

        [HttpGet]
        public IActionResult Registrar(string Id)
        {
            ViewBag.EsNuevo = string.IsNullOrEmpty(Id);

            BodegaModel model = new BodegaModel();
            try
            {
                ViewData["sucursales"] = new SelectList(_sucursalRepository.GetSucursales(), "SucursalId", "Nombre");

                if (!string.IsNullOrEmpty(Id))
                {
                    var result = _BodegaAppService.ConsultarBodega(Id);
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
                model = new BodegaModel();
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Registrar(BodegaModel model)
        {
            ViewBag.EsNuevo = string.IsNullOrEmpty(model.Id);
            try
            {
                ViewData["sucursales"] = new SelectList(_sucursalRepository.GetSucursales(), "SucursalId", "Nombre");

                if (ModelState.IsValid)
                {
                    var usr = GetUserLogin();
                    model.Ip = usr.IPLogin;
                    model.Usuario = usr.IdUsuario;

                    if (string.IsNullOrEmpty(model.Id))
                    {
                        var result = _BodegaAppService.CrearBodega(model);
                        if (result.TieneErrores) throw new Exception(result.MensajeError);
                        if (result.Estado)
                        {
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Bodega registrado con éxito");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, result.Mensaje);
                        }
                    }
                    else
                    {
                        var result = _BodegaAppService.EditarBodega(model);
                        if (result.TieneErrores) throw new Exception(result.MensajeError);
                        if (result.Estado)
                        {
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Bodega actualizado con éxito");
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
                var result = _BodegaAppService.EliminarBodega(Id, usr.IPLogin, usr.IdUsuario);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    return Json(new ResponseToViewDto { Estado = true, Mensaje = "Bodega eliminado con éxito" });
                }
                else
                {
                    return Json(new ResponseToViewDto { Estado = false, Mensaje = (string.IsNullOrEmpty(result.Mensaje) ? "Error al eliminar el Bodega" : result.Mensaje)  });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }
    }
}
