
using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using System.Collections.Generic;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices
{
    public interface IUsuarioQueryService
    {
        IEnumerable<UsuarioInternoQueryDto> ConsultaUsuarioInternoXCompania(long idCompania, ref string mensaje);

        //IEnumerable<UsuariosQueryDto> ObtenerUsuario(long idUsuario, ref string mensaje);

        //List<UsuariosQueryDto> ConsultarUsuario(ref string mensaje);
    }
}
