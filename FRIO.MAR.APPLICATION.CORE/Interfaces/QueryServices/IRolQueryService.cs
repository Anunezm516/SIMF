using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using System.Collections.Generic;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices
{
    public interface IRolQueryService
    {
        IEnumerable<RolesQueryDto> ConsultaRolesXCompania(long idCompania, ref string mensaje);

        IEnumerable<IdQueryDto> ConsultarRolVentanas(short IdRol, ref string mensaje);
    }
}
