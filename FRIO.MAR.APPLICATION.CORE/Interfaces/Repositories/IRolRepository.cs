
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Entities.StoreProcedures;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IRolRepository
    {
        bool ActualizaRol(SPRol rol, ref string mensaje);
        int? AsignarVentanas(long idRol, string idPermisos, long usuarioAuditoria, ref string mensaje);
        bool RegistrarRol(SPRol rol, ref string mensaje);
        List<Rol> ObtenerCodigoRol();
        List<Rol> GetRoles();
        List<Rol> GetRolesUsuario(long IdUsuario);

    }
}
