using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        private readonly IAccountAppService _accountAppService;

        public DashboardController(IAccountAppService accountAppService, ILogInfraServices logInfraServices) : base(logInfraServices)
        {
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
    }
}
