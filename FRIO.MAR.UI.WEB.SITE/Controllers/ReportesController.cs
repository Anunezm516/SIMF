using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    //[Filters.MenuFilter(Constants.VentanasSoporte.ReportesVentas)]
    public class ReportesController : BaseController
    {
        private readonly IProveedorRepository _proveedorRepository;
        private readonly IReporteQueryService _reporteQueryService;
        private readonly IUtilidadRepository _utilidadRepository;

        public ReportesController(
            IProveedorRepository proveedorRepository,
            IReporteQueryService reporteQueryService,
            IUtilidadRepository utilidadRepository, ILogInfraServices logInfraServices) : base(logInfraServices)
        {
            _proveedorRepository = proveedorRepository;
            _reporteQueryService = reporteQueryService;
            _utilidadRepository = utilidadRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Ventas() => View();    

        public PartialViewResult ReporteVentas(EstadoFactura EstadoFactura, DateTime FechaInicio, DateTime FechaFin)
        {
            return PartialView("_VentasDetalle", _reporteQueryService.GetFacturasVentas(EstadoFactura, FechaInicio, FechaFin));
        }

        public IActionResult Compras()
        {
            //ViewBag.Proveedores = new SelectList(_proveedorRepository.GetProveedores().Select(c => new SelectListItem
            //{
            //    Text = c.Identificacion + " - " + c.RazonSocial,
            //    Value = c.ProveedorId.ToString(),
            //}).ToList(), "Value", "Text");
            ViewBag.Proveedores = _proveedorRepository.GetProveedores();

            return View();
        }

        public PartialViewResult ReporteCompras(long Proveedor, DateTime FechaInicio, DateTime FechaFin)
        {
            return PartialView("_ComprasDetalle", _reporteQueryService.GetFacturasCompras(Proveedor, FechaInicio, FechaFin));
        }
    }
}
