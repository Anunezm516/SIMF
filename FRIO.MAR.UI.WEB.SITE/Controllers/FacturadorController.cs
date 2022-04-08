using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.Models;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using FRIO.MAR.UI.WEB.SITE.Constants;
using GS.TOOLS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Filters.MenuFilter(Constants.VentanasSoporte.Facturador)]
    public class FacturadorController : BaseController
    {
        private readonly IUtilidadRepository _utilidadRepository;
        private readonly IAccountAppService _accountAppService;

        public FacturadorController(
            IUtilidadRepository utilidadRepository,
            IAccountAppService accountAppService, ILogInfraServices logInfraServices) : base(logInfraServices)
        {
            _utilidadRepository = utilidadRepository;
            _accountAppService = accountAppService;
        }

        public IActionResult Index()
        {
            FacturadorModel model = new FacturadorModel();
            try
            {
                ViewData["tipoIdentificacion"] = new SelectList(_utilidadRepository.GetTipoIdentificaciones().ToList(), "Codigo", "Nombre");
                ViewData["Sucursales"] = GenerarCodigos();
                ViewData["PuntoEmision"] = ViewData["Sucursales"];

                var result = _accountAppService.ConsultarFacturador();
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
        public IActionResult Index(FacturadorModel model)
        {
            try
            {
                var usr = GetUserLogin();
                ViewData["tipoIdentificacion"] = new SelectList(_utilidadRepository.GetTipoIdentificaciones().ToList(), "Codigo", "Nombre");
                ViewData["Sucursales"] = GenerarCodigos();
                ViewData["PuntoEmision"] = ViewData["Sucursales"];

                var result = _accountAppService.ActualizarFacturador(model, usr.IdUsuario, usr.IPLogin);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    //HttpContext.Session.SetString("FotoBase64", model.ImagenBase64);
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

        private List<SelectListItem> GenerarCodigos()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            
            for (int i = 1; i <= 999; i++)
            {
                items.Add(new SelectListItem
                {
                    Text = i.ToString().PadLeft(3, '0'),
                    Value = i.ToString().PadLeft(3, '0'),
                });
            }

            return items;
        }
    }
}
