using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices;
using FRIO.MAR.APPLICATION.CORE.Models;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.DomainServices
{
    public class InventarioDomainService : IInventarioDomainService
    {

        public InventarioDomainService()
        {

        }


        public MethodResponseDto QryInventarioMovimiento(InventarioMantenimientoDto mantenimientoDto, long IdCompania, long IdUsuario, string IP, ref long IdInventarioMovimiento)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                //return inventarioRepositorio.QryInventarioMovimiento(mantenimientoDto, IdCompania, IdUsuario, IP, ref IdInventarioMovimiento);
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto QryInventarioTransferencia(long IdCompania, long IdUsuario, string IP, InventarioTransferenciaDto transferencia)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                //return inventarioRepositorio.QryInventarioTransferencia(IdCompania, IdUsuario, IP, transferencia);
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }
        /*
        public MethodResponseDto ActualizarInventarioEmision(long IdCompania, long IdUsuario, string IP, int tipoMovimiento, long IdCliente, RespuestaEDOCDto respuestaEdoc, List<ProductoModel> productos)
        {
            MethodResponseDto responseDto = new MethodResponseDto();

            try
            {
                InventarioMantenimientoDto mantenimiento = new InventarioMantenimientoDto();
                mantenimiento.tipoInventario = (int)TipoInventario.venta;
                mantenimiento.tipoMovimiento = tipoMovimiento;
                mantenimiento.subTipoMovimiento = (int)TipoMovimientoInventario.Factura;
                mantenimiento.cliente = IdCliente;
                mantenimiento.cufeFactura = respuestaEdoc.UUID;
                mantenimiento.numeroFactura = respuestaEdoc.NumDocumento;

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
                    responseDto = QryInventarioMovimiento(mantenimiento, IdCompania, IdUsuario, IP, ref IdInventarioMovimiento);
                }
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }
        */
    }
}
