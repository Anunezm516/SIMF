using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IAccountAppService
    {
        (List<Menu>, string) LoadMenu(long IdUsuario);
        MethodResponseDto Login(string username, string password, string ip, bool DirActivo = false, string NomUsrDirAct = null, string GrupoUsuarioDirActivo = null);
        MethodResponseDto RecuperarPassword(string username, string email);
        MethodResponseDto CambiarPassword(long id, string ClaveActual, string ClaveNueva, string ClaveNuevaConfirma, bool EsCorreoRecuperacion);
    }
}
