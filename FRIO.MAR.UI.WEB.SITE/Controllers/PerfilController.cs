using FRIO.MAR.CROSSCUTTING.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Authorize]
    //[Filters.MenuFilter(Constants.VentanasSoporte.Permisos)]
    public class PerfilController : BaseController
    {
        public PerfilController(ILogInfraServices logInfraServices) : base(logInfraServices)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
