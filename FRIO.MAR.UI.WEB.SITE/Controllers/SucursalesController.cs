﻿using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Models;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using FRIO.MAR.UI.WEB.SITE.Constants;
using GS.TOOLS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Filters.MenuFilter(Constants.VentanasSoporte.Sucursales)]
    public class SucursalesController : BaseController
    {
        private readonly ISucursalAppService _SucursalAppService;

        public SucursalesController(ISucursalAppService SucursalAppService, ILogInfraServices logInfraServices) : base(logInfraServices)
        {
            _SucursalAppService = SucursalAppService;
        }

        public IActionResult Index()
        {
            List<SucursalModel> Sucursales = new List<SucursalModel>();
            try
            {
                var result = _SucursalAppService.ConsultarSucursales();
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    Sucursales = result.Data;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_ERROR.Replace("{Mensaje_Respuesta}", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(Sucursales);
        }

        [HttpGet]
        public IActionResult Registrar(string Id)
        {
            ViewBag.EsNuevo = string.IsNullOrEmpty(Id);

            SucursalModel model = new SucursalModel();
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    var result = _SucursalAppService.ConsultarSucursal(Id);
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
                model = new SucursalModel();
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Registrar(SucursalModel model)
        {
            ViewBag.EsNuevo = string.IsNullOrEmpty(model.Id);
            try
            {
                if (ModelState.IsValid)
                {
                    var usr = GetUserLogin();
                    model.Ip = usr.IPLogin;
                    model.Usuario = usr.IdUsuario;

                    if (string.IsNullOrEmpty(model.Id))
                    {
                        var result = _SucursalAppService.CrearSucursal(model);
                        if (result.TieneErrores) throw new Exception(result.MensajeError);
                        if (result.Estado)
                        {
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Sucursal registrado con éxito");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, result.Mensaje);
                        }
                    }
                    else
                    {
                        var result = _SucursalAppService.EditarSucursal(model);
                        if (result.TieneErrores) throw new Exception(result.MensajeError);
                        if (result.Estado)
                        {
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Sucursal actualizado con éxito");
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
                var result = _SucursalAppService.EliminarSucursal(Id, usr.IPLogin, usr.IdUsuario);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    return Json(new ResponseToViewDto { Estado = true, Mensaje = "Sucursal eliminado con éxito" });
                }
                else
                {
                    return Json(new ResponseToViewDto { Estado = false, Mensaje = "Error al eliminar el Sucursal" });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }
    }
}
