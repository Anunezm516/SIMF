using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.Models;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.DomainServices
{
    public class InventarioDomainService : IInventarioDomainService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IInventarioRepository _inventarioRepository;

        public InventarioDomainService(IProductoRepository productoRepository, IInventarioRepository inventarioRepository)
        {
            _productoRepository = productoRepository;
            _inventarioRepository = inventarioRepository;
        }

        public MethodResponseDto QryInventarioMovimiento(InventarioMantenimientoDto mantenimientoDto, long IdUsuario, string IP, ref long IdInventarioMovimiento)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                string Mensaje = "";
                string MensajeError = "";

                responseDto.Estado = _inventarioRepository.QryInventarioMovimiento(mantenimientoDto, IdUsuario, IP, ref IdInventarioMovimiento, ref Mensaje, ref MensajeError);
                responseDto.Mensaje = Mensaje;
                responseDto.MensajeError = MensajeError;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto QryInventarioTransferencia(long IdUsuario, string IP, InventarioTransferenciaDto transferencia)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                string Mensaje = "";
                string MensajeError = "";

                responseDto.Estado = _inventarioRepository.QryInventarioTransferencia(IdUsuario, IP, transferencia, ref Mensaje, ref MensajeError);
                //if (!responseDto.Estado)
                //{
                //    responseDto.Mensaje = mensaje;
                //}
                responseDto.Mensaje = Mensaje;
                responseDto.MensajeError = MensajeError;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }
        
        public MethodResponseDto ActualizarInventarioEmision(
            long IdUsuario,
            string IP,
            TipoMovimientoInventario tipoMovimiento, 
            long IdCliente, 
            RespuestaVentaDto respuestaVentaDto, 
            List<DetalleFacturaModel> productos)
        {
            MethodResponseDto responseDto = new MethodResponseDto();

            try
            {
                InventarioMantenimientoDto mantenimiento = new InventarioMantenimientoDto();
                mantenimiento.tipoInventario = TipoInventario.venta;
                mantenimiento.tipoMovimiento = tipoMovimiento;
                mantenimiento.subTipoMovimiento = SubtipoMovimientoInventario.Factura;
                mantenimiento.cliente = IdCliente;
                //mantenimiento.cufeFactura = respuestaVentaDto.UUID;
                mantenimiento.numeroFactura = respuestaVentaDto.NumDocumento;

                if (tipoMovimiento == TipoMovimientoInventario.Entrada)// Entrada
                {
                    mantenimiento.motivo = "Devolución por emisión de Nota Crédito " + mantenimiento.numeroFactura;
                }
                else if (tipoMovimiento == TipoMovimientoInventario.Salida)// Salida
                {
                    mantenimiento.motivo = "Salida por emisión de Factura Número " + mantenimiento.numeroFactura;
                }

                long IdInventarioMovimiento = 0;
                foreach (var item in productos.Where(x => x.TipoProducto == TipoProducto.Bien))
                {
                    mantenimiento.bodegas = item.BodegaId;
                    mantenimiento.sucursal = item.SucursalId;
                    mantenimiento.productos = item.ProductoId;
                    mantenimiento.cantidad = item.CantidadDec;
                    mantenimiento.unidadMedida = item.UnidadMedida;
                    responseDto = QryInventarioMovimiento(mantenimiento, IdUsuario, IP, ref IdInventarioMovimiento);
                }
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }
        
        public MethodResponseDto ListarInventarios(long BodegaId)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                ListaInventarioViewModel model = new ListaInventarioViewModel();
                List<InventarioVenta> ListaInventario = ListaInventario = _inventarioRepository.GetInventarioVenta(BodegaId);
                List<InventarioProveedor> ListaInventarioProveedor = ListaInventarioProveedor = _inventarioRepository.GetInventarioProveedor(BodegaId);

                model.ListaInventario = ListaInventario.Select(itemBBDD => new ItemInventarioViewModel(itemBBDD)).ToList();
                model.ListaInventarioProvedor = ListaInventarioProveedor.Select(itemBBDD => new ItemInventarioProvedorViewModel(itemBBDD)).ToList();

                var InventarioConfiguracion = _inventarioRepository.GetConfiguracionesGenerales();
                if (InventarioConfiguracion != null)
                {
                    model.Descontar = InventarioConfiguracion.DescontarStockAutomatico;
                    model.ControlSucursal = InventarioConfiguracion.ControlInventarioSucursal;
                    model.ControlEmision = InventarioConfiguracion.ControlInventarioEmision;
                }
                else
                {
                    model.Descontar = true;
                    model.ControlSucursal = true;
                    model.ControlEmision = true;
                }
                responseDto.Data = model;
                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public List<ProductoInventarioDto> GetProductosInventario(TipoInventario tipoInventario, long Sucursal)
        {
            try
            {
                var configuracion = _inventarioRepository.GetConfiguracionesGenerales() ?? new InventarioConfiguracionesGenerales { ControlInventarioEmision = true, ControlInventarioSucursal = true, DescontarStockAutomatico = true};
                if (configuracion.ControlInventarioSucursal)
                {
                    return _inventarioRepository.GetProductosInventario(tipoInventario, Sucursal);
                }
                else
                {
                    return _productoRepository.GetProductos().Where(x => x.TipoProducto == TipoProducto.Bien).Select(c => new ProductoInventarioDto(c, null, null, 0)).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
