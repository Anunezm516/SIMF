using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices
{
    public interface IInventarioDomainService
    {
        MethodResponseDto QryInventarioMovimiento(InventarioMantenimientoDto mantenimientoDto, long IdUsuario, string IP, ref long IdInventarioMovimiento, ref string mensaje, ref string mensajeError);
        MethodResponseDto QryInventarioTransferencia(long IdUsuario, string IP, InventarioTransferenciaDto transferencia, ref string mensaje, ref string mensajeError);
        MethodResponseDto ListarInventarios(long BodegaId);
    }
}
