using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Services;
using FRIO.MAR.APPLICATION.CORE.Utilities;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using GS.TOOLS;
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
        private readonly IAccountRepository _accountRepository;
        private readonly IVentasRepository _ventasRepository;
        private readonly IGemboxService _gemboxService;
        private readonly IProveedorRepository _proveedorRepository;
        private readonly IReporteQueryService _reporteQueryService;
        private readonly IUtilidadRepository _utilidadRepository;

        public ReportesController(
            IAccountRepository accountRepository,
            IVentasRepository ventasRepository,
            IGemboxService gemboxService,
            IProveedorRepository proveedorRepository,
            IReporteQueryService reporteQueryService,
            IUtilidadRepository utilidadRepository, 
            ILogInfraServices logInfraServices) : base(logInfraServices)
        {
            _accountRepository = accountRepository;
            _ventasRepository = ventasRepository;
            _gemboxService = gemboxService;
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

        [HttpGet]
        public IActionResult ImprimirFactura(string data)
        {
            try
            {
                long Id = long.Parse(Crypto.DescifrarId(data));
                string NombreArchivo = Guid.NewGuid().ToString() + ".pdf";
                var factura = _ventasRepository.GetFactura(Id, EstadoFactura.Facturado);
                if (factura == null) throw new Exception("");

                var facturador = _accountRepository.GetFacturador();
                if (facturador == null) throw new Exception("");

                byte[] archivo = _gemboxService.ConstruirFactura(factura, facturador, "pdf", GlobalSettings.FormatoReporte_RutaBase, GlobalSettings.FormatoReporte_NombreArchivo);

                return File(archivo, "text/xml", NombreArchivo);
            }
            catch (Exception ex)
            {
                RegistrarLogError(this.GetCaller(), ex);
            }

            return RedirectToAction("Index");

        }
    }
}
