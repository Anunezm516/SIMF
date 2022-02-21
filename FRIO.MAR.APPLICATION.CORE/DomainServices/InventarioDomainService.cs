using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.Models;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.DomainServices
{
    public class InventarioDomainService : IInventarioDomainService
    {
        private readonly IInventarioRepository _inventarioRepository;

        public InventarioDomainService(IInventarioRepository inventarioRepository)
        {
            _inventarioRepository = inventarioRepository;
        }


        public MethodResponseDto QryInventarioMovimiento(InventarioMantenimientoDto mantenimientoDto, long IdUsuario, string IP, ref long IdInventarioMovimiento, ref string mensaje, ref string mensajeError)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                responseDto.Data = _inventarioRepository.QryInventarioMovimiento(mantenimientoDto, IdUsuario, IP, ref IdInventarioMovimiento, ref mensaje, ref mensajeError);
                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto QryInventarioTransferencia(long IdUsuario, string IP, InventarioTransferenciaDto transferencia, ref string mensaje, ref string mensajeError)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                responseDto.Data = _inventarioRepository.QryInventarioTransferencia(IdUsuario, IP, transferencia, ref mensaje, ref mensajeError);
                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }
        
        public MethodResponseDto ActualizarInventarioEmision(long IdUsuario, string IP, int tipoMovimiento, long IdCliente, RespuestaVentaDto respuestaVentaDto, List<ProductoModel> productos, ref string mensaje, ref string mensajeError)
        {
            MethodResponseDto responseDto = new MethodResponseDto();

            try
            {
                InventarioMantenimientoDto mantenimiento = new InventarioMantenimientoDto();
                mantenimiento.tipoInventario = (int)TipoInventario.venta;
                mantenimiento.tipoMovimiento = tipoMovimiento;
                mantenimiento.subTipoMovimiento = (int)TipoMovimientoInventario.Factura;
                mantenimiento.cliente = IdCliente;
                //mantenimiento.cufeFactura = respuestaVentaDto.UUID;
                mantenimiento.numeroFactura = respuestaVentaDto.NumDocumento;

                if (tipoMovimiento == 1)// Entrada
                {
                    mantenimiento.motivo = "Devolución por emisión de Nota Crédito " + mantenimiento.numeroFactura;
                }
                else if (tipoMovimiento == 2)// Salida
                {
                    mantenimiento.motivo = "Salida por emisión de Factura Número " + mantenimiento.numeroFactura;
                }

                long IdInventarioMovimiento = 0;
                foreach (var item in productos)
                {
                    mantenimiento.bodegas = item.Bodega;
                    mantenimiento.sucursal = item.Sucursal;
                    mantenimiento.productos = item.ProductoId;
                    mantenimiento.cantidad = item.Cantidad;
                    mantenimiento.unidadMedida = item.UnidadMedida;
                    responseDto = QryInventarioMovimiento(mantenimiento, IdUsuario, IP, ref IdInventarioMovimiento, ref mensaje, ref mensajeError);
                }
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }
        
    }
}
