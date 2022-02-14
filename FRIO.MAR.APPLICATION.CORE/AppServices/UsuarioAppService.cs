using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.AppServices
{
    public class UsuarioAppService : IUsuarioAppService
    {
        public MethodResponseDto ConsultarUsuarios()
        {
            throw new NotImplementedException();
        }

        public MethodResponseDto CrearUsuario(UsuariosAppResultDto usuariosApp, long IdUsuarioCreacion, string Ip)
        {
            throw new NotImplementedException();
        }

        public MethodResponseDto EditarUsuario(UsuariosAppResultDto usuariosApp, long IdUsuarioCreacion, string Ip)
        {
            throw new NotImplementedException();
        }

        public (bool, string) EliminarUsuario(string IdCifrado, long IdUsuario, string Ip)
        {
            throw new NotImplementedException();
        }

        public MethodResponseDto ObtenerUsuario(long idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
