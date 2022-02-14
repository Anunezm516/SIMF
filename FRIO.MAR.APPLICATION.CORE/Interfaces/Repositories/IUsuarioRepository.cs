using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using System.Collections.Generic;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        List<Usuario> GetUsuariosAdministrador();
        bool EditarUsuario(Usuario editar, ref string mensaje);
        List<Usuario> ConsultarUsuarios();
        Usuario ObtenerUsuario(long idUsuario, ref string mensaje);
        List<Usuario> ObtenerCodigoUsuario();
        Usuario ObtenerUsuario(long id);
        bool GuardarUsuarioRol(UsuarioRol model);
    }
}
