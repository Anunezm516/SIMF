using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using GS.TOOLS;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Utilities;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using FRIO.MAR.APPLICATION.CORE.Models;
using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.UI.WEB.SITE.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using FRIO.MAR.APPLICATION.CORE.DTOs;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class InventarioController : BaseController
    {
        private readonly ILogInfraServices logInfraServices;
        private readonly SIFMContext _context;
        private readonly IConfiguration _configuration;
        private readonly IInventarioDomainService inventarioService;
        private readonly IInventarioRepository inventarioRepositorio;

        public InventarioController(
            ILogInfraServices logInfraServices,
            SIFMContext context, 
            IConfiguration configuration, 
            IInventarioRepository inventarioRepositorios,
            IInventarioDomainService inventarioServices
            ) : base(logInfraServices)
        {
            _configuration = configuration;
            this.logInfraServices = logInfraServices;
            _context = context;
            inventarioRepositorio = inventarioRepositorios;
            inventarioService = inventarioServices;
        }

        [Filters.MenuFilter(Constants.VentanasSoporte.InventarioLista)]
        public ActionResult ListaInventario(string data)
        {
            ListaInventarioViewModel model = new ListaInventarioViewModel();
            try
            {
                long Id = long.Parse(Crypto.DescifrarId(data));

                var result = inventarioService.ListarInventarios(Id);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    model = result.Data;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_ERROR.Replace("{Mensaje_Respuesta}", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [Filters.MenuFilter(Constants.VentanasSoporte.InventarioLista)]
        public ActionResult GetDetalleProveedor(long IdProducto)
        {
            var ProductoQuery = _context.Producto.Find(IdProducto);

            ListaInventarioProveedorViewModel model = new ListaInventarioProveedorViewModel();
            model.ProductoNombre = ProductoQuery.Descripcion;
            var ListaInventarioProvedor = _context.InventarioMovimientoEntrada.Where(x => x.Estado == true && x.IdProducto == IdProducto).ToList();

            for (int i = 0; i < ListaInventarioProvedor.Count(); i++)
            {

                var ItemLista = ListaInventarioProvedor[i];
                string ProveedorName = "";
                long? ProveedorId = null;
                if (ItemLista.IdProveedor != null)
                {
                    var Proveedor = _context.Proveedores.Find(ItemLista.IdProveedor);
                    ProveedorName = Proveedor.RazonSocial;
                    ProveedorId = Proveedor.ProveedorId;
                }
            }
            return PartialView("DetalleProveedorInventario", model);
        }

        [Filters.MenuFilter(Constants.VentanasSoporte.InventarioMovimiento)]
        public IActionResult MovimientoInventario() => View();

        [HttpPost]
        public JsonResult MovimientoInventario(
            TipoInventario TipoInventarioOrigen, 
            long BodegaOrigen, 
            long Producto, 
            int SucursalOrigen,
            TipoInventario TipoInventarioDestino, 
            int SucursalDestino, 
            long BodegaDestino, 
            string Cantidad, 
            string Motivo, 
            string PrecioStr
            )
        {
            try
            {
                var UsuarioSesion = GetUserLogin();

                InventarioTransferenciaDto transferencia = new InventarioTransferenciaDto
                {
                    TipoInventarioOrigen = TipoInventarioOrigen,
                    BodegaOrigen = BodegaOrigen,
                    Producto = Producto,
                    SucursalOrigen = SucursalOrigen,
                    TipoInventarioDestino = TipoInventarioDestino,
                    SucursalDestino = SucursalDestino,
                    BodegaDestino = BodegaDestino,
                    Cantidad = Convert.ToDecimal(Utilidades.DepuraStrConvertNum(Cantidad)),
                    Motivo = Motivo,
                    PrecioStr = PrecioStr,
                    SubTipoMovimiento = SubtipoMovimientoInventario.Tranferencia
                };
                long IdUsuario = UsuarioSesion.IdUsuario;
                string IP = UsuarioSesion.IPLogin;

                var result = inventarioService.QryInventarioTransferencia(IdUsuario, IP, transferencia);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    return Json(new { status = "success", mensaje = "Se ha registrado la trasnferencia de productos" });
                }
                else
                {
                    return Json(new { status = "error", result.Mensaje });
                    //if (result.TieneErrores)
                    //{
                    //    return Json(new { status = "error", mensaje = $"{DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), mensajeError)} [<a href='{_configuration["InformeCertificado:LinkSoporte"]}'>Soporte</a>]" });
                    //}
                    //else
                    //{
                    //    return Json(new { status = "error", mensaje });
                    //}
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", mensaje = $"{DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex)} [<a href='{_configuration["InformeCertificado:LinkSoporte"]}'>Soporte</a>]" });
            }
        }

        [HttpPost]
        public JsonResult MovimientoInventarioV2(
            TipoInventario TipoInventarioOrigen,
            TipoInventario TipoInventarioDestino, 
            int SucursalDestino, 
            long BodegaDestino, 
            string Motivo, 
            List<ProductoMovimientoDto> productosArray)
        {
            var UsuarioSesion = GetUserLogin();
            List<ProductoMovimientoDto> respuestas = new List<ProductoMovimientoDto>();

            try
            {
                InventarioTransferenciaDto transferencia = new InventarioTransferenciaDto();
                transferencia.TipoInventarioOrigen = TipoInventarioOrigen;
                transferencia.TipoInventarioDestino = TipoInventarioDestino;
                transferencia.SucursalDestino = SucursalDestino;
                transferencia.BodegaDestino = BodegaDestino;
                transferencia.Motivo = Motivo;
                transferencia.SubTipoMovimiento = APPLICATION.CORE.Constants.SubtipoMovimientoInventario.Tranferencia;
                //decimal Precio = Convert.ToDecimal(Utilidades.DepuraStrConvertNum(PrecioStr));
                long IdUsuario = UsuarioSesion.IdUsuario;
                string IP = UsuarioSesion.IPLogin;

                MethodResponseDto result = new MethodResponseDto();

                foreach (var item in productosArray)
                {
                    transferencia.BodegaOrigen = item.bodega;
                    transferencia.Producto = item.producto;
                    transferencia.SucursalOrigen = item.sucursal;
                    transferencia.Cantidad = Convert.ToDecimal(Utilidades.DepuraStrConvertNum(item.cantidad));

                    result = inventarioService.QryInventarioTransferencia(IdUsuario, IP, transferencia);
                    if (result.TieneErrores) throw new Exception(result.MensajeError);
                    item.tieneError = !result.Estado;
                    item.mensaje = result.Mensaje;
                    item.mensajeError = result.MensajeError;

                    respuestas.Add(item);
                }

                if (respuestas.Any())
                {
                    return Json(new { status = "success", mensaje = "Se ha registrado la trasnferencia de productos" });
                }
                else
                {
                    return Json(new { status = "error", result.Mensaje });
                    //if (!string.IsNullOrEmpty(mensajeError))
                    //{
                    //    return Json(new { status = "error", mensaje = $"{DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), mensajeError)} [<a href='{_configuration["InformeCertificado:LinkSoporte"]}'>Soporte</a>]" });
                    //}
                    //else
                    //{
                    //    return Json(new { status = "error", mensaje });
                    //}
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", mensaje = $"{DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex)} [<a href='{_configuration["InformeCertificado:LinkSoporte"]}'>Soporte</a>]" });
            }
        }

        [Filters.MenuFilter(Constants.VentanasSoporte.InventarioMantenimiento)]
        public IActionResult MantenimientoInventario()
        {
            try
            {
                ViewBag.Productos = _context.Producto.Where(x => x.Estado == true).ToList();
                ViewBag.Bodegas = _context.Bodega.Where(x => x.Estado == true).ToList();
                ViewBag.UnidadesMedida = _context.UnidadMedida.Where(x => x.Estado == true).ToList();
                ViewBag.Sucursal = _context.Sucursal.Where(x => x.Estado == true).ToList();
                ViewBag.Cliente = _context.Clientes.Where(x => x.Estado == true).ToList();
                ViewBag.Proveedor = _context.Proveedores.Where(x => x.Estado == true).ToList();

            }
            catch (Exception ex)
            {
                _ = RegistrarLogError(this.GetCaller(), ex);
            }
            return View();
        }

        [HttpPost]
        public JsonResult MantenimientoInventarioCompras(
            long Proveedor,
            TipoInventario TipoInventario, 
            int Sucursal, 
            long Bodega, 
            long Producto, 
            string PrecioStr,
            string UnidadMedida, 
            string Cantidad, 
            string Cufe, 
            string NumeroFactura, 
            long Item, 
            string NitProveedor)
        {
            try
            {
                if (!string.IsNullOrEmpty(NitProveedor))
                {
                    //Proveedor = CrearProveedor(NitProveedor, Item);
                }

                InventarioMantenimientoDto mantenimiento = new InventarioMantenimientoDto();
                mantenimiento.nitProveedor = NitProveedor;
                mantenimiento.proveedor = Proveedor;
                mantenimiento.tipoInventario = TipoInventario;
                mantenimiento.sucursal = Sucursal;
                mantenimiento.bodegas = Bodega;
                mantenimiento.productos = Producto;
                mantenimiento.unidadMedida = UnidadMedida;
                mantenimiento.cantidadStr = Cantidad;
                mantenimiento.cufeFactura = Cufe;
                mantenimiento.numeroFactura = NumeroFactura;
                mantenimiento.tipoMovimiento = TipoMovimientoInventario.Entrada;
                mantenimiento.motivo = "Ingreso de producto por factura Nº " + NumeroFactura;
                mantenimiento.precio = Convert.ToDecimal(Utilidades.DepuraStrConvertNum(PrecioStr));
                decimal vIn = Convert.ToDecimal(Utilidades.DepuraStrConvertNum(mantenimiento.cantidadStr));
                mantenimiento.cantidad = Convert.ToInt64(vIn);
                mantenimiento.subTipoMovimiento = APPLICATION.CORE.Constants.SubtipoMovimientoInventario.Factura;

                string mensajeError = "";
                string mensaje = "";

                long IdInventarioMovimiento = 0;
                int cont = 0;
                bool res = GuardarMantenimiento(mantenimiento, ref IdInventarioMovimiento, ref mensaje, ref mensajeError);
                if (res)
                {
                    /*
                    var erfactLine = _contextEmps.ErfactLine.FirstOrDefault(x => x.IdFactLine == Item);
                    if (erfactLine != null)
                    {
                        erfactLine.IdInventarioMovimiento = IdInventarioMovimiento;
                        _contextEmps.ErfactLine.Update(erfactLine);
                        cont = _contextEmps.SaveChanges();
                    }
                    */
                    if (mantenimiento.tipoMovimiento == TipoMovimientoInventario.Entrada)
                    {
                        return Json(new { status = "success", mensaje = "Se ha registrado la nueva entrada", cont });
                    }
                    else
                    {
                        return Json(new { status = "success", mensaje = "Se ha registrado la nueva salida" });
                    }
                    
                }
                else
                {
                    if (!string.IsNullOrEmpty(mensajeError))
                    {
                        return Json(new { status = "error", mensaje = $"{DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), mensajeError)} [<a href='{_configuration["InformeCertificado:LinkSoporte"]}'>Soporte</a>]" });
                    }
                    else
                    {
                        return Json(new { status = "error", mensaje });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", mensaje = $"{DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex)} [<a href='{_configuration["InformeCertificado:LinkSoporte"]}'>Soporte</a>]" });
            }
        }

        private bool GuardarMantenimiento(InventarioMantenimientoDto mantenimiento, ref long IdInventarioMovimiento, ref string mensaje, ref string mensajeError)
        {
            var UsuarioSesion = GetUserLogin();
            long IdUsuario = UsuarioSesion.IdUsuario;
            string IP = UsuarioSesion.IPLogin;
            var result = inventarioService.QryInventarioMovimiento(mantenimiento, IdUsuario, IP, ref IdInventarioMovimiento);
            if (result.TieneErrores) throw new Exception(result.MensajeError);
            mensaje = result.Mensaje;
            mensajeError = result.MensajeError;
            return result.Estado;
        }

        [HttpPost]
        [Filters.MenuFilter(Constants.VentanasSoporte.InventarioMantenimiento)]
        public JsonResult MantenimientoInventario(InventarioMantenimientoDto mantenimiento)
        {
            try
            {
                mantenimiento.precio = Convert.ToDecimal(Utilidades.DepuraStrConvertNum(mantenimiento.precioStr));
                mantenimiento.cantidad = Convert.ToDecimal(Utilidades.DepuraStrConvertNum(mantenimiento.cantidadStr));

                mantenimiento.subTipoMovimiento = APPLICATION.CORE.Constants.SubtipoMovimientoInventario.Manual;
                
                string mensajeError = "";
                string mensaje = "";

                long IdInventarioMovimiento = 0;

                bool res = GuardarMantenimiento(mantenimiento, ref IdInventarioMovimiento, ref mensaje, ref mensajeError);
                if (res)
                {
                    if (mantenimiento.tipoMovimiento == TipoMovimientoInventario.Entrada)
                    {
                        return Json(new { status = "success", mensaje = "Se ha registrado la nueva entrada" });
                    }
                    else
                    {
                        return Json(new { status = "success", mensaje = "Se ha registrado la nueva salida" });
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(mensajeError))
                    {
                        return Json(new { status = "error", mensaje = $"{DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), mensajeError)} [<a href='{_configuration["InformeCertificado:LinkSoporte"]}'>Soporte</a>]" });
                    }
                    else
                    {
                        return Json(new { status = "error", mensaje });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", mensaje = $"{DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex)} [<a href='{_configuration["InformeCertificado:LinkSoporte"]}'>Soporte</a>]" });
            }
        }
    
        [HttpPost]
        public JsonResult ActualizarComboProductosBodega(long Bodega, long Sucursal, int? TipoInventario, int TipoInventarioProducto, int SucursalProducto)
        {
            try
            {
                if (TipoInventario != null)
                {
                    if (TipoInventario == (int)APPLICATION.CORE.Constants.TipoInventario.proveedor)
                    {
                        //var sucursalesArray = _context.InventarioProveedor.Where(x => x.Estado == true).Select(c => c.IdSucursal).ToArray();
                        //var sucursales = _context.Sucursal.Where(x => sucursalesArray.Contains(x.SucursalId) ).ToList();
                        var sucursales = _context.Sucursal.Where(x => x.Estado).ToList();

                        return Json(new { status = "success", sucursales = sucursales.Where(x => x.Estado == true).ToList() });
                    }
                    else if (TipoInventario == (int)APPLICATION.CORE.Constants.TipoInventario.venta)
                    {
                        //var sucursalesArray = _context.InventarioVenta.Where(x => x.Estado == true).Select(c => c.IdSucursal).ToArray();
                        //var sucursales = _context.Sucursal.Where(x => sucursalesArray.Contains(x.SucursalId) ).ToList();
                        var sucursales = _context.Sucursal.Where(x => x.Estado).ToList();
                        return Json(new { status = "success", sucursales = sucursales.Where(x => x.Estado == true).ToList() });
                    }
                }

                if(Sucursal != 0)
                {
                    var bodegasArray = _context.SucursalBodega.Where(x => x.SucursalId == Sucursal && x.Estado).Select(x => x.BodegaId).ToArray();
                    List<Bodega> bodegas = _context.Bodega.Where(x => x.Estado == true && bodegasArray.Contains(x.BodegaId)).ToList();
                    return Json(new { status = "success", bodegas = bodegas.Where(x => x.Estado == true).ToList() });
                }
                else
                {
                    List<ProductoBodegaDto> productos = inventarioRepositorio.SelProductosBodega(Bodega, SucursalProducto, TipoInventarioProducto);
                    List<Bodega> bodegas = _context.Bodega.Where(x => x.BodegaId != Bodega && x.Estado == true).ToList();

                    if (productos == null)
                    {
                        productos = new List<ProductoBodegaDto>();
                        return Json(new { status = "error", productos, bodegas });
                    }

                    return Json(new { status = "success", productos, bodegas });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", mensaje = $"{DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex)} [<a href='{_configuration["InformeCertificado:LinkSoporte"]}'>Soporte</a>]" });
            }
        }

        public ActionResult GetProductosInventario(int TipoInventario)
        {
            ViewBag.Bodegas = _context.Bodega.Where(x => x.Estado == true).ToList();
            ViewBag.Sucursal = _context.Sucursal.Where(x => x.Estado == true).ToList();
            ViewBag.Productos = _context.Producto.Where(x => x.Estado == true).ToList();

            GetProductoInventarioDto get = new GetProductoInventarioDto();
            try
            {
                get.TipoInventario = TipoInventario;

                if (TipoInventario == (int)APPLICATION.CORE.Constants.TipoInventario.proveedor)
                {
                    var data = _context.InventarioProveedor.Where(x => x.Estado == true && x.StockActual > 0)
                        .Include(c => c.IdProductoNavigation)
                        .Include(c => c.Bodega)
                        .Include(c => c.IdSucursalNavigation)
                        .ToList();
                    if(data != null && data.Any())
                    {
                        get.InventarioProveedor.AddRange(data);
                    }
                }
                else if (TipoInventario == (int)APPLICATION.CORE.Constants.TipoInventario.venta)
                {
                    var data = _context.InventarioVenta.Where(x => x.Estado == true && x.StockActual > 0)
                        .Include(c => c.IdProductoNavigation)
                        .Include(c => c.Bodega)
                        .Include(c => c.IdSucursalNavigation)
                        .ToList();
                    if (data != null && data.Any())
                    {
                        get.InventarioVenta.AddRange(data);
                    }
                }
                else
                {
                    return Json(new { status = "error", verMensaje = false, mensaje = "Seleccione un tipo de inventario Válido" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", verMensaje = true, mensaje = $"{DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex)} [<a href='{_configuration["InformeCertificado:LinkSoporte"]}'>Soporte</a>]" });
            }

            return PartialView("_GetProductosInventario", get);
        }

        public SelectList DropSucursal(string Pais = null)
        {
            var data = _context.Sucursal.Where(x => x.Estado == true).Select(c => new {
                text = c.Nombre,
                value = c.SucursalId,
            }).ToList();

            return new SelectList(data, "text", "value", Pais);
        }

        [Filters.MenuFilter(Constants.VentanasSoporte.ReportesInventario)]
        public ActionResult Reporte()
        {
            ViewBag.Productos = _context.Producto.Where(x => x.Estado == true).ToList();
            //Edcompania edcompania = _contextEmps.Edcompania.FirstOrDefault(x => x.Ruc == UsuarioSesion.Nit && x.Estado == true);
            ViewBag.Proveedor = _context.Proveedores.Where(x =>  x.Estado == true).ToList();

            return View("Reporte/ReportesInventarioMovimiento");
        }
        
        public ActionResult ActualizarComboEntrada()
        {
            try
            {
                var productoModel = _context.Producto.Where(x => x.Estado)
                    .Select(c => new ProductoBodegaDto()
                    {
                        ProductoId = c.ProductoId,
                        Codigo = c.Codigo,
                        Descripcion = c.Descripcion,
                        Stock = Utilidades.DoubleToString_FrontCO(0, 2)
                    }).ToList();

                var sucursal  = _context.Sucursal.Where(x => x.Estado).ToList();

                return Json(new { status = "success", sucursal, productos = productoModel });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", mensaje = $"{DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex)} [<a href='{_configuration["InformeCertificado:LinkSoporte"]}'>Soporte</a>]" });
            }
        }

        [Filters.MenuFilter(Constants.VentanasSoporte.ReportesInventario)]
        public ActionResult GetDetalleReporteInventario(
            long Proveedor,
            TipoInventario TipoInventario, 
            int TipoMovimiento,
            SubtipoMovimientoInventario SubtipoMovimiento, 
            long producto, 
            int sucursal, 
            long bodega, 
            DateTime fechaInicio, 
            DateTime fechaFin)
        {
            List<ReporteInventarioEntradaSalidaDto> inventario = new List<ReporteInventarioEntradaSalidaDto>();
            fechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);
            fechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);

            try
            {
                List<ReporteInventarioEntradaSalidaDto> movimientosEntrada = new List<ReporteInventarioEntradaSalidaDto>();
                List<ReporteInventarioEntradaSalidaDto> movimientosSalida = new List<ReporteInventarioEntradaSalidaDto>();

                if (TipoMovimiento == 0) 
                {
                    movimientosEntrada = _context.InventarioMovimientoEntrada
                      .Where(x => x.Estado == true)
                      .Where(x => x.FechaCreacion >= fechaInicio && x.FechaCreacion <= fechaFin)
                      .Where(x => SubtipoMovimiento == 0 || x.TipoMovimiento == SubtipoMovimiento)
                      .Where(x => TipoInventario == 0 || x.TipoInventario == TipoInventario)
                      .Where(x => producto == 0 || x.IdProducto == producto)
                      .Where(x => sucursal == 0 || x.IdSucursal == producto)
                      .Where(x => bodega == 0 || x.IdInventarioBodega == producto)
                      .Where(x => Proveedor == 0 || x.IdProveedor == Proveedor)
                      .Include(x => x.IdProductoNavigation)
                      .Include(x => x.Bodega)
                      .Include(x => x.IdSucursalNavigation)
                      .Select(c => new ReporteInventarioEntradaSalidaDto(c))
                      .ToList();

                    movimientosSalida = _context.InventarioMovimientoSalida
                        .Where(x => x.Estado == true)
                        .Where(x => x.FechaCreacion >= fechaInicio && x.FechaCreacion <= fechaFin)
                        .Where(x => SubtipoMovimiento == 0 || x.TipoMovimiento == SubtipoMovimiento)
                        .Where(x => TipoInventario == 0 || x.TipoInventario == TipoInventario)
                        .Where(x => producto == 0 || x.IdProducto == producto)
                        .Where(x => sucursal == 0 || x.IdSucursal == producto)
                        .Where(x => bodega == 0 || x.IdInventarioBodega == producto)
                        .Include(x => x.IdProductoNavigation)
                        .Include(x => x.Bodega)
                        .Include(x => x.IdSucursalNavigation)
                        .Select(c => new ReporteInventarioEntradaSalidaDto(c))
                        .ToList();
                }
                else if (TipoMovimiento == 1)
                {
                    movimientosEntrada = _context.InventarioMovimientoEntrada
                      .Where(x => x.Estado == true)
                      .Where(x => x.FechaCreacion >= fechaInicio && x.FechaCreacion <= fechaFin)
                      .Where(x => SubtipoMovimiento == 0 || x.TipoMovimiento == SubtipoMovimiento)
                      .Where(x => TipoInventario == 0 || x.TipoInventario == TipoInventario)
                      .Where(x => producto == 0 || x.IdProducto == producto)
                      .Where(x => sucursal == 0 || x.IdSucursal == producto)
                      .Where(x => bodega == 0 || x.IdInventarioBodega == producto)
                      .Where(x => Proveedor == 0 || x.IdProveedor == Proveedor)
                      .Include(x => x.IdProductoNavigation)
                      .Include(x => x.Bodega)
                      .Include(x => x.IdSucursalNavigation)
                      .Select(c => new ReporteInventarioEntradaSalidaDto(c))
                      .ToList();
                } 
                else if (TipoMovimiento == 2)
                {
                    movimientosSalida = _context.InventarioMovimientoSalida
                        .Where(x => x.Estado == true)
                        .Where(x => x.FechaCreacion >= fechaInicio && x.FechaCreacion <= fechaFin)
                        .Where(x => SubtipoMovimiento == 0 || x.TipoMovimiento == SubtipoMovimiento)
                        .Where(x => TipoInventario == 0 || x.TipoInventario == TipoInventario)
                        .Where(x => producto == 0 || x.IdProducto == producto)
                        .Where(x => sucursal == 0 || x.IdSucursal == producto)
                        .Where(x => bodega == 0 || x.IdInventarioBodega == producto)
                        .Include(x => x.IdProductoNavigation)
                        .Include(x => x.Bodega)
                        .Include(x => x.IdSucursalNavigation)
                        .Select(c => new ReporteInventarioEntradaSalidaDto(c))
                        .ToList();
                }
                
                inventario.AddRange(movimientosEntrada);
                inventario.AddRange(movimientosSalida);

                return PartialView("Reporte/ReportesInventarioMovimientoDetalle", inventario);
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", mensaje = $"{DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex)} [<a href='{_configuration["InformeCertificado:LinkSoporte"]}'>Soporte</a>]" });
            }
        }
        
        
        
        /*
        public JsonResult InventarioConfiguracion(bool Descontar,bool ControlInventario , bool ControlEmision)
        {
            try
            {
                var usr = GetUserLogin();

                var InventarioConfiguracion = _context.InventarioConfiguracionesGenerales.FirstOrDefault(x =>  );

                #region SetearVariableDeSession 
                if (Descontar)
                {
                    HttpContext.Session.SetInt32("DescontarStockAutomatico", 1);
                }
                else
                {
                    HttpContext.Session.SetInt32("DescontarStockAutomatico", 0);
                }
                if (ControlInventario)
                {
                    HttpContext.Session.SetInt32("ControlInventarioSucursal", 1);
                }
                else
                {
                    HttpContext.Session.SetInt32("ControlInventarioSucursal", 0);
                }
                if (ControlEmision)
                {
                    HttpContext.Session.SetInt32("ControlInventarioEmision", 1);
                }
                else
                {
                    HttpContext.Session.SetInt32("ControlInventarioEmision", 0);
                }
                #endregion

                if (InventarioConfiguracion != null)
                {
                    InventarioConfiguracion.DescontarStockAutomatico = Descontar;
                    InventarioConfiguracion.ControlInventarioSucursal = ControlInventario;
                    InventarioConfiguracion.ControlInventarioEmision = ControlEmision;
                    _context.SaveChanges();
                     
                    return Json(new { status = "success" });
                }
                else
                {
                    InventarioConfiguracionesGenerales Inventario = new InventarioConfiguracionesGenerales();
                    Inventario.IdCompania = UsuarioSesion.IdCompania;
                    Inventario.ControlInventarioEmision = ControlEmision;
                    Inventario.ControlInventarioSucursal = ControlInventario;
                    Inventario.DescontarStockAutomatico = Descontar;
                    Inventario.FechaCreacion = DateTime.Now;
                    _context.Add(Inventario);
                    _context.SaveChanges();

                    return Json(new { status = "success" });
                } 
            }
            catch (Exception ex)
            {
                string codigo = ". Código de seguimiento: " + RegistrarLogError(this.GetCaller(), $"[{UsuarioSesion?.Nit ?? ""}] - Inventario -> InventarioConfiguracion => " + Utilidades.LeerExcepcion(ex));
                return Json(new { status = "error", mensaje = $"{DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex)} [<a href='{_configuration["InformeCertificado:LinkSoporte"]}'>Soporte</a>]" });
            }
        }
        */
    }

    public class ReporteInventarioEntradaSalidaDto
    {
        public ReporteInventarioEntradaSalidaDto(InventarioMovimientoEntrada movimiento)
        {
            ID = movimiento.IdInventarioMovimiento.ToString() + DateTime.Now.ToString("mmssfffff");
            Producto = movimiento.IdProductoNavigation?.Descripcion ?? "--";
            IdProducto = movimiento.IdProducto??0;
            Bodega = movimiento.Bodega?.Nombre ?? "--";
            IdBodega = movimiento.IdInventarioBodega??0;
            Sucursal = movimiento.IdSucursalNavigation?.Nombre ?? "--";
            IdSucursal = movimiento.IdSucursal??0;
            ClienteProveedor = "--";
            IdClienteProveedor = movimiento.IdProveedor??0;
            Cantidad = movimiento.Cantidad??0;
            CantidadStr = Utilidades.DoubleToString_FrontCO(movimiento.Cantidad ?? 0, 2);
            Precio = movimiento.Precio ?? 0;
            NumeroFactura = movimiento.NumeroFactura;
            Cufe = movimiento.Cufe;
            Motivo = movimiento.Motivo??"--";
            TipoInventario = movimiento.TipoInventario; //
            TipoMovimiento = TipoMovimientoInventario.Entrada; //Entrada
            SubTipoMovimiento = movimiento.TipoMovimiento;
            Fecha = movimiento.FechaCreacion == null ? "--" : ((DateTime)movimiento.FechaCreacion).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public ReporteInventarioEntradaSalidaDto(InventarioMovimientoSalida movimiento)
        {
            ID = movimiento.IdInventarioMovimiento.ToString() + DateTime.Now.ToString("mmssfffff");
            Producto = movimiento.IdProductoNavigation?.Descripcion ?? "--";
            IdProducto = movimiento.IdProducto ?? 0;
            Bodega = movimiento.Bodega?.Nombre ?? "--";
            IdBodega = movimiento.IdInventarioBodega ?? 0;
            Sucursal = movimiento.IdSucursalNavigation?.Nombre ?? "--";
            IdSucursal = movimiento.IdSucursal ?? 0;
            ClienteProveedor = "--";
            IdClienteProveedor = movimiento.IdCliente ?? 0;
            Cantidad = movimiento.Cantidad ?? 0;
            CantidadStr = Utilidades.DoubleToString_FrontCO(movimiento.Cantidad ?? 0, 2);
            Precio = movimiento.Precio ?? 0;
            NumeroFactura = movimiento.NumeroFactura;
            Cufe = movimiento.Cufe;
            Motivo = movimiento.Motivo ?? "--";
            TipoInventario = movimiento.TipoInventario; //
            TipoMovimiento = TipoMovimientoInventario.Salida; //Salida
            SubTipoMovimiento = movimiento.TipoMovimiento;
            Fecha = movimiento.FechaCreacion == null ? "--" : ((DateTime)movimiento.FechaCreacion).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string ID { get; set; }
        public string Producto { get; set; }
        public long IdProducto { get; set; }
        public string Bodega { get; set; }
        public long IdBodega { get; set; }
        public string Sucursal { get; set; }
        public long IdSucursal { get; set; }
        public string ClienteProveedor { get; set; }
        public long IdClienteProveedor { get; set; }
        public decimal Cantidad { get; set; }
        public string CantidadStr { get; set; }
        public decimal Precio { get; set; }
        public string NumeroFactura { get; set; }
        public string Cufe { get; set; }
        public string Motivo { get; set; }
        public string Fecha { get; set; }
        public TipoInventario TipoInventario { get; set; }
        public TipoMovimientoInventario TipoMovimiento { get; set; }
        public SubtipoMovimientoInventario SubTipoMovimiento { get; set; }

    }
}


