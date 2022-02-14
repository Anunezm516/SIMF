using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogInfraServices _logInfraServices;

        protected internal BaseController(ILogInfraServices logInfraServices)
        {
            _logInfraServices = logInfraServices;
        }

        public LoginAppResultDto GetUserLogin()
        {
            var userClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData);
            return JsonConvert.DeserializeObject<LoginAppResultDto>(userClaim.Value);
        }

        protected string RegistrarLogError(string origen, Exception ex, bool soloCodigo = false)
        {
            if (soloCodigo)
            {
                return _logInfraServices.AddLog(origen, ex);
            }
            else
            {
                return ". Código de seguimiento: " + _logInfraServices.AddLog(origen, ex);
            }
        }

        protected string RegistrarLogError(string origen, string error, bool soloCodigo = false)
        {
            if (soloCodigo)
            {
                return _logInfraServices.AddLog(origen, error);
            }
            else
            {
                return ". Código de seguimiento: " + _logInfraServices.AddLog(origen, error);
            }
        }
    }
}
