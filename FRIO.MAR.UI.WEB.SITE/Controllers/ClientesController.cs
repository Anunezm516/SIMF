using FRIO.MAR.CROSSCUTTING.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Filters.MenuFilter(Constants.VentanasSoporte.Clientes)]
    public class ClientesController : BaseController
    {
        public ClientesController(ILogInfraServices logInfraServices) : base(logInfraServices)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
