
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Parameters;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using FRIO.MAR.UI.WEB.SITE.Constants;
using FRIO.MAR.UI.WEB.SITE.Models;
using GS.TOOLS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Authorize]
    [Filters.MenuFilter(Constants.VentanasSoporte.Rol)]
    public class RolController : BaseController
    {
        private readonly IRolAppService _rolAppService;

        public RolController(IRolAppService rolAppService, ILogInfraServices logInfraServices) : base(logInfraServices)
        {
            this._rolAppService = rolAppService;
        }

        public IActionResult Index()
        {
            var userLogin = GetUserLogin();
            (var roles, string error) = _rolAppService.ConsultarRoles(userLogin.IdUsuario);

            if(roles == null || !roles.Any())
                TempData["msg"] = "<script>$.jGrowl('No existen roles para mostrar', { life: 5000, theme: 'growl-success' });</script>";
            
            if (!string.IsNullOrEmpty(error)) RegistrarLogError(this.GetCaller(), error);

            return View(roles);
        }

        [HttpPost]
        public IActionResult Guardar(RolPageModel model)
        {
            if (ModelState.IsValid)
            {
                var userLogin = GetUserLogin();

                if (model.IdRol == 0)
                {
                    (var result, string error) = _rolAppService.RegistrarRol(userLogin.IdUsuario, model.Nombre, model.Estado == 1);
                    if (result) TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Rol registrado con éxito");
                    else TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_ERROR.Replace("{Mensaje_Respuesta}", "La información ingresada es inválida");
                    if (!string.IsNullOrEmpty(error)) RegistrarLogError(this.GetCaller(), error);
                }
                else
                {
                    (var result, string error) = _rolAppService.ActualizarRol(model.IdRol, model.Nombre, model.Estado == 1, userLogin.IdUsuario);
                    if (result) TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Rol actualizado con éxito");
                    else TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_ERROR.Replace("{Mensaje_Respuesta}", "La información ingresada es inválida");
                    if (!string.IsNullOrEmpty(error)) RegistrarLogError(this.GetCaller(), error);
                }
            }
            else
                TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_ERROR.Replace("{Mensaje_Respuesta}", "La información ingresada es inválida");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ConsultarVentanasActivas()
        {
            try
            {
                var userLogin = GetUserLogin();
                (var result, string error) = _rolAppService.ConsultarVentanasActivas(userLogin.Usuario);
                if (!string.IsNullOrEmpty(error)) RegistrarLogError(this.GetCaller(), error);
                return Json(new
                {
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, AppParameters.ExcepcionGenerica + RegistrarLogError(this.GetCaller(), ex));
            }
        }

        [HttpPost]
        public IActionResult ConsultarRolVentanas(short IdRol)
        {
            try
            {
                var userLogin = GetUserLogin();

                (var result, string error) = _rolAppService.ConsultarRolVentanas(IdRol, userLogin.Usuario);
                if (!string.IsNullOrEmpty(error)) RegistrarLogError(this.GetCaller(), error);
                if (result == null) return StatusCode(StatusCodes.Status500InternalServerError, AppParameters.ExcepcionGenerica);
                return Json(new
                {
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, AppParameters.ExcepcionGenerica + RegistrarLogError(this.GetCaller(), ex));
            }
        }

        [HttpPost]
        public IActionResult AsignaRolVentanas(short IdRol, string Nombre, string IdVentanas)
        {
            try
            {
                var userLogin = GetUserLogin();

                (var result, string error) = _rolAppService.Asignar(IdRol, IdVentanas, userLogin.IdUsuario);
                if (!string.IsNullOrEmpty(error)) RegistrarLogError(this.GetCaller(), error);
                if (result == null) return StatusCode(StatusCodes.Status500InternalServerError, AppParameters.ExcepcionGenerica);
                if (result == 0)
                {
                    var mensaje = $"Rol [{Nombre}] no encontrado.";
                    return StatusCode(StatusCodes.Status500InternalServerError, mensaje);
                }

                return StatusCode(StatusCodes.Status200OK, "Registro actualizado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, AppParameters.ExcepcionGenerica + RegistrarLogError(this.GetCaller(), ex));
            }
        }
    }
}
