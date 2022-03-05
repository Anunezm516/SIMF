using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    //[Filters.MenuFilter(Constants.VentanasSoporte.ReportesVentas)]
    public class ReportesController : BaseController
    {
        private readonly IReporteQueryService _reporteQueryService;
        private readonly IUtilidadRepository _utilidadRepository;

        public ReportesController(
            IReporteQueryService reporteQueryService,
            IUtilidadRepository utilidadRepository, ILogInfraServices logInfraServices) : base(logInfraServices)
        {
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
            return PartialView("_VentasDetalle", _reporteQueryService.GetFacturas(EstadoFactura, FechaInicio, FechaFin));
        }
    }
}
