﻿
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IUsuarioAppService
    {
        MethodResponseDto CrearUsuario(UsuariosAppResultDto usuariosApp, long IdUsuarioCreacion, string Ip);
        MethodResponseDto EditarUsuario(UsuariosAppResultDto usuariosApp, long IdUsuarioCreacion, string Ip);
        MethodResponseDto ConsultarUsuarios();
        MethodResponseDto ObtenerUsuario(long idUsuario);
        (bool, string) EliminarUsuario(string IdCifrado, long IdUsuario, string Ip);
    }
}
