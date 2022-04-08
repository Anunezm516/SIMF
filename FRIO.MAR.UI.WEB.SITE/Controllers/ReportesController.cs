using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices;
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
using System.IO;
using System.Linq;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class ReportesController : BaseController
    {
        private readonly IVentasDomainService _ventasDomainService;
        private readonly IComprasDomainService _comprasDomainService;
        private readonly IClienteRepository _clienteRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IVentasRepository _ventasRepository;
        private readonly IGemboxService _gemboxService;
        private readonly IProveedorRepository _proveedorRepository;
        private readonly IReporteQueryService _reporteQueryService;
        private readonly IUtilidadRepository _utilidadRepository;

        public ReportesController(
            IVentasDomainService ventasDomainService,
            IComprasDomainService comprasDomainService,
            IClienteRepository clienteRepository,
            IAccountRepository accountRepository,
            IVentasRepository ventasRepository,
            IGemboxService gemboxService,
            IProveedorRepository proveedorRepository,
            IReporteQueryService reporteQueryService,
            IUtilidadRepository utilidadRepository, 
            ILogInfraServices logInfraServices) : base(logInfraServices)
        {
            _ventasDomainService = ventasDomainService;
            _comprasDomainService = comprasDomainService;
            _clienteRepository = clienteRepository;
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

        #region Ventas
        [Filters.MenuFilter(Constants.VentanasSoporte.ReportesVentas)]
        public IActionResult Ventas() => View();    

        [Filters.MenuFilter(Constants.VentanasSoporte.ReportesVentas)]
        public PartialViewResult ReporteVentas(EstadoFactura EstadoFactura, DateTime FechaInicio, DateTime FechaFin)
        {
            return PartialView("_VentasDetalle", _reporteQueryService.GetFacturasVentas(EstadoFactura, FechaInicio, FechaFin));
        }
        #endregion

        #region Compras
        [Filters.MenuFilter(Constants.VentanasSoporte.ReportesCompras)]
        public IActionResult Compras()
        {
            ViewBag.Proveedores = _proveedorRepository.GetProveedores();

            return View();
        }

        [Filters.MenuFilter(Constants.VentanasSoporte.ReportesCompras)]
        public PartialViewResult ReporteCompras(long Proveedor, DateTime FechaInicio, DateTime FechaFin)
        {
            return PartialView("_ComprasDetalle", _reporteQueryService.GetFacturasCompras(Proveedor, FechaInicio, FechaFin));
        }

        [HttpPost]
        [Filters.MenuFilter(Constants.VentanasSoporte.ReportesCompras)]
        public IActionResult DescargaAdjuntosCompra(string FacturaId)
        {
            try
            {
                var result = _comprasDomainService.DescargarAdjuntos(long.Parse(Crypto.DescifrarId(FacturaId)));
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                return Json(new ResponseToViewDto { Estado = result.Estado, Mensaje = result.Mensaje, Data = result.Data });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = true, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

        [HttpPost]
        [Filters.MenuFilter(Constants.VentanasSoporte.ReportesCompras)]
        public IActionResult DescargaAdjuntosVentas(string FacturaId)
        {
            try
            {
                var result = _ventasDomainService.DescargarAdjuntos(long.Parse(Crypto.DescifrarId(FacturaId)));
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                return Json(new ResponseToViewDto { Estado = result.Estado, Mensaje = result.Mensaje, Data = result.Data });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = true, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }
        #endregion

        #region Producto-Servicios
        [ActionName("Producto-Servicios")]
        [Filters.MenuFilter(Constants.VentanasSoporte.ReportesProductoServicio)]
        public IActionResult ProductosCliente()
        {
            ViewBag.Clientes = _clienteRepository.GetClientes();
            return View("ProductosCliente");
        }

        [Filters.MenuFilter(Constants.VentanasSoporte.ReportesProductoServicio)]
        public IActionResult ReporteProductosCliente(long Cliente, DateTime FechaInicio, DateTime FechaFin)
        {
            return PartialView("_ProductosClienteDetalle", _reporteQueryService.GetProductosFactura(Cliente, FechaInicio, FechaFin));
        }

        #endregion

        [HttpPost]
        public JsonResult ImprimirFactura(string data)
        {
            try
            {
                long Id = long.Parse(Crypto.DescifrarId(data));
                string NombreArchivo = Guid.NewGuid().ToString() + ".pdf";
                var factura = _ventasRepository.GetFactura(Id);
                if (factura == null)
                {
                    return Json(new ResponseToViewDto { Estado = false, Mensaje = "Documento no encontrado" });
                }

                var facturador = _accountRepository.GetFacturador();
                if (facturador == null)
                {
                    return Json(new ResponseToViewDto { Estado = false, Mensaje = "No existen datos del facturador" });
                }

                byte[] archivo = _gemboxService.ConstruirFactura(factura, facturador, "pdf", GlobalSettings.FormatoReporte_RutaBase, GlobalSettings.FormatoReporte_NombreArchivo);

                return Json(new ResponseToViewDto { Estado = true, Data = archivo.ToArray() });

                //return File(archivo, "text/xml", NombreArchivo);
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });

            }
        }

        [HttpGet]
        public FileResult DownloadZip(string fileGuid, string filename)
        {
            try
            {
                string nombreArchivo = Path.GetFileName(fileGuid);
                return File(fileGuid.Replace(@"wwwroot\", ""), "application/zip", nombreArchivo);
            }
            catch (Exception ex)
            {
                _ = RegistrarLogError(this.GetCaller(), ex);
                throw;
            }
        }

    }
}
