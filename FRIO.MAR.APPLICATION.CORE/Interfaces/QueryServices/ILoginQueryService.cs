
using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using System.Collections.Generic;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices
{
    public interface ILoginQueryService
    {
        LoginInternoQueryDto LoginInterno(string usuario, string claveEncriptada, string nitCiaNube, string ipLogin, bool dirActivo, string nomUsrDirAct, string grupoUsuarioDirActivo, ref string mensaje);
        List<VentanaLoginQueryDto> ConsultarVentana(long IdUsuario, ref string mensaje);
        bool? ValidarUsuarioClaveRecuperacion(string usuario, string claveRecuperacion, bool esUsuarioInterno, string nitCiaNube, ref string mensaje);
    }
}
