using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using System.Collections.Generic;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices
{
    public interface IRolQueryService
    {
        List<RolesQueryDto> ConsultaRolesXCompania(long idCompania, ref string mensaje);

        List<IdQueryDto> ConsultarRolVentanas(short IdRol, ref string mensaje);
    }
}
