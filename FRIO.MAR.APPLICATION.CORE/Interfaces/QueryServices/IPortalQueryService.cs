
using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using System.Collections.Generic;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices
{
    public interface IPortalQueryService
    {
        CredencialQueryDto ConsultarCredencialXNit(string nitCompania, ref string mensaje);
        
        IEnumerable<ParametroQueryDto> ConsultarParametroXCodigoXNit(string nitCompania, string codigos, ref string mensaje);
        IEnumerable<PermisosQueryDto> ConsultarVentanasActivas(ref string mensaje);
    }
}
