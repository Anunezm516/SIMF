using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Models;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using FRIO.MAR.UI.WEB.SITE.Constants;
using GS.TOOLS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Authorize]
    public class PerfilController : BaseController
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public PerfilController(
            IUsuarioAppService usuarioAppService,
            ILogInfraServices logInfraServices
            ) : base(logInfraServices)
        {
            _usuarioAppService = usuarioAppService;
        }

        public IActionResult Index()
        {
            PerfilModel model = new PerfilModel();
            try
            {
                var usr = GetUserLogin();

                var result = _usuarioAppService.ConsultarPerfil(usr.IdUsuario);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    model = result.Data;
                }
                else
                {
                    TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_ERROR.Replace("{Mensaje_Respuesta}", result.Mensaje);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(PerfilModel model)
        {
            try
            {
                var usr = GetUserLogin();

                var result = _usuarioAppService.ActualizarPerfil(model, usr.IdUsuario, usr.IPLogin);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    HttpContext.Session.SetString("FotoBase64", model.ImagenBase64);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Mensaje);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }
    }
}
