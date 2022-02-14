

using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using GS.TOOLS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Repositories
{
    public class RolRepository : Repository<Rol>, IRolRepository
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public RolRepository(IServiceScopeFactory serviceScopeFactory, SIFMContext context) : base(context)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public List<Rol> GetRoles()
        {
            return _context.Rol.ToList();
        }

        public bool ActualizaRol(Rol rol, ref string mensaje)
        {
            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    using var edocCmdContext = scope.ServiceProvider.GetRequiredService<SIFMContext>();
                    //edocCmdContext.ActualizarRol(rol);
                }

                return true;
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return false;
            }
        }

        public bool RegistrarRol(Rol rol, ref string mensaje)
        {
            try
            {
                _context.Rol.Add(new Rol
                {
                    IdRol = _context.Rol.Max(x => x.IdRol) + 1,
                    Nombre = rol.Nombre,
                    Ip = "",
                    Estado = true,
                    FechaCreacion = APPLICATION.CORE.Utilities.Utilidades.GetHoraActual(),
                    //UsuarioCreacion = rol.IdUsuario
                });

                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return false;
            }
        }

        public short? AsignarVentanas(long idRol, string idPermisos, long usuarioAuditoria, ref string mensaje)
        {
            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    using (var edocCmdContext = scope.ServiceProvider.GetRequiredService<SIFMContext>())
                    {
                        return 0;
                        //return edocCmdContext.AsignarVentanas(idRol, idPermisos, usuarioAuditoria);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return null;
            }
        }

        public List<Rol> ObtenerCodigoRol()
        {
            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    using (var context = scope.ServiceProvider.GetRequiredService<SIFMContext>())
                    {
                        var obtener = context.Rol.Where(x => x.Estado == true).ToList();
                        return obtener;
                    }
                }
            }
            catch (Exception ex)
            {
                //mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return null;
            }
        }

        public List<Rol> GetRolesUsuario(long IdUsuario)
        {
            var result = _context.UsuarioRol.Include(x => x.IdRolNavigation).Where(x => x.IdUsuario == IdUsuario && x.Estado == true);
            if(result != null)
            {
                return result.Select(x => x.IdRolNavigation).ToList();
            }
            return null;
        }
    }
}
