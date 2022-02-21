using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices
{
    public interface IInventarioDomainService
    {
        MethodResponseDto QryInventarioMovimiento(InventarioMantenimientoDto mantenimientoDto, long IdCompania, long IdUsuario, string IP, ref long IdInventarioMovimiento);
        MethodResponseDto QryInventarioTransferencia(long IdCompania, long IdUsuario, string IP, InventarioTransferenciaDto transferencia);
    }
}
