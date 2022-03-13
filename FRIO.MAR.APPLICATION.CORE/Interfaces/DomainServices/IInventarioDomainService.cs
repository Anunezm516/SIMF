using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices
{
    public interface IInventarioDomainService
    {
        MethodResponseDto QryInventarioMovimiento(InventarioMantenimientoDto mantenimientoDto, long IdUsuario, string IP, ref long IdInventarioMovimiento);
        MethodResponseDto QryInventarioTransferencia(long IdUsuario, string IP, InventarioTransferenciaDto transferencia);
        MethodResponseDto ActualizarInventarioEmision(
            long IdUsuario,
            string IP,
            TipoMovimientoInventario tipoMovimiento,
            long IdCliente,
            RespuestaVentaDto respuestaVentaDto,
            List<DetalleFacturaModel> productos);
        MethodResponseDto ListarInventarios(long BodegaId);
        List<ProductoInventarioDto> GetProductosInventario(TipoInventario tipoInventario, long Sucursal);
    }
}
