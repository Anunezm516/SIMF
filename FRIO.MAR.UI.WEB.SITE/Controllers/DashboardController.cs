using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using GS.TOOLS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Filters.MenuFilter(Constants.VentanasSoporte.Dashboard)]
    public class DashboardController : BaseController
    {
        private readonly INotificacionAppService _notificacionAppService;
        private readonly IAccountAppService _accountAppService;

        public DashboardController(
            INotificacionAppService notificacionAppService,
            IAccountAppService accountAppService, ILogInfraServices logInfraServices) : base(logInfraServices)
        {
            _notificacionAppService = notificacionAppService;
            _accountAppService = accountAppService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Menu") == null)
            {
                var usr = GetUserLogin();
                (var menu, string error) = _accountAppService.LoadMenu(usr.IdUsuario);
                if (menu != null)
                {
                    var IdRol = menu.OrderBy(x => x.IdRol).FirstOrDefault()?.IdRol ?? 0;
                    HttpContext.Session.SetString("Menu", JsonConvert.SerializeObject(menu));
                    //HttpContext.Session.SetInt32("IdRol", (int)IdRol);
                    //HttpContext.Session.SetString("Roles", "");
                }
                else
                {
                    HttpContext.Session.SetString("Rol", JsonConvert.SerializeObject(new List<Menu>()));
                    //HttpContext.Session.SetInt32("IdRol", 0);
                    //HttpContext.Session.SetString("Roles", 0);
                }
            }
            return View();
        }

        public PartialViewResult GetNotificaciones()
        {
            List<NotificacionAppResultDto> notificaciones = new List<NotificacionAppResultDto>();

            try
            {
                var usr = GetUserLogin();
                var result = _notificacionAppService.GetNotificaciones(usr.IdUsuario, false);
                if (result.TieneErrores) throw new Exception(result.MensajeError);

                if (result.Estado)
                {
                    notificaciones = result.Data;
                    HttpContext.Session.SetInt32("CantidadNotificaciones", notificaciones.Count());
                }
            }
            catch (Exception ex)
            {
                RegistrarLogError(this.GetCaller(), ex);
            }

            return PartialView("_notificacion", notificaciones);
        }

        public PartialViewResult MarcarNotificacion(long Id)
        {
            List<NotificacionAppResultDto> notificaciones = new List<NotificacionAppResultDto>();

            try
            {
                var usr = GetUserLogin();
                var resultMarca = _notificacionAppService.MarcarLeido(Id);
                if (resultMarca.TieneErrores) throw new Exception(resultMarca.MensajeError);

                var result = _notificacionAppService.GetNotificaciones(usr.IdUsuario, false);
                if (result.TieneErrores) throw new Exception(result.MensajeError);

                if (result.Estado)
                {
                    notificaciones = result.Data;
                    HttpContext.Session.SetInt32("CantidadNotificaciones", notificaciones.Count());
                }
            }
            catch (Exception ex)
            {
                RegistrarLogError(this.GetCaller(), ex);
            }

            return PartialView("_notificacion", notificaciones);
        }

    }
}
