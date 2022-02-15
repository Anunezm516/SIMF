
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.UI.WEB.SITE.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FRIO.MAR.UI.WEB.SITE.Filters
{
    public class MenuFilterAttribute : ActionFilterAttribute
    {
        private readonly string _permiso;

        public MenuFilterAttribute(VentanasSoporte permiso)
        {
            _permiso = ((int)permiso).ToString();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userClaim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData);
            var user = JsonConvert.DeserializeObject<LoginAppResultDto>(userClaim.Value);

            if (user.ForzarCambioClave)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    area = "",
                    controller = "Account",
                    action = "ChangePassword"
                }));
            }

            if (!user.VentanasActivasConcat.Contains(_permiso))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    area = "",
                    controller = "Pages",
                    action = "solicitud-permiso"
                }));
            }
        }

    }
}