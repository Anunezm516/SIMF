using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Services;
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
        private readonly IClienteRepository _clienteRepository;
        private readonly IProveedorRepository _proveedorRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IProductoClienteRepository _productoClienteRepository;
        private readonly IDashboardAppService _dashboardAppService;
        private readonly IStorageService _storageService;
        private readonly INotificacionAppService _notificacionAppService;
        private readonly IAccountAppService _accountAppService;
        private readonly ILogInfraServices logInfraServices;

        public DashboardController(
            IClienteRepository clienteRepository,
            IProveedorRepository proveedorRepository,
            IProductoRepository productoRepository,
            IProductoClienteRepository productoClienteRepository,
            IDashboardAppService dashboardAppService,
            IStorageService storageService,
            INotificacionAppService notificacionAppService,
            IAccountAppService accountAppService, ILogInfraServices logInfraServices) : base(logInfraServices)
        {
            _clienteRepository = clienteRepository;
            _proveedorRepository = proveedorRepository;
            _productoRepository = productoRepository;
            _productoClienteRepository = productoClienteRepository;
            _dashboardAppService = dashboardAppService;
            _storageService = storageService;
            _notificacionAppService = notificacionAppService;
            _accountAppService = accountAppService;
            this.logInfraServices = logInfraServices;
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

                    string imagen = _storageService.ObtenerImagenBase64(GlobalSettings.TipoAlmacenamiento, usr.Foto ?? "", "");

                    HttpContext.Session.SetString("FotoBase64", string.IsNullOrEmpty(imagen) ? AppConstants.SinImagen : imagen);

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

            ViewBag.CantidadCliente = _clienteRepository.Find(x => x.Estado).Count();
            ViewBag.CantidadProveedor = _proveedorRepository.Find(x => x.Estado).Count();

            var productos = _productoRepository.Find(x => x.Estado);
            ViewBag.CantidadProducto = productos.Where(x => x.TipoProducto == TipoProducto.Bien).Count();
            ViewBag.CantidadServicio = productos.Where(x => x.TipoProducto == TipoProducto.Servicio).Count();

            return View();
        }

        [HttpPost]
        public JsonResult GetGraficos()
        {
            try
            {
                var result =_dashboardAppService.GetGraficoLineaComportamientoLineaVentasCompras(DateTime.Now.Year);
                if (result.TieneErrores) throw new Exception(result.MensajeError);

                return Json(new ResponseToViewDto { Estado = result.Estado, Data = result.Data, Mensaje = result.Mensaje });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
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
