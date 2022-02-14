
using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IRolRepository
    {
        bool ActualizaRol(Rol rol, ref string mensaje);
        short? AsignarVentanas(long idRol, string idPermisos, long usuarioAuditoria, ref string mensaje);
        bool RegistrarRol(Rol rol, ref string mensaje);
        List<Rol> ObtenerCodigoRol();
        List<Rol> GetRoles();
        List<Rol> GetRolesUsuario(long IdUsuario);

    }
}
