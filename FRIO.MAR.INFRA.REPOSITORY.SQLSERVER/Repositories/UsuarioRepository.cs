
using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using GS.TOOLS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Repositories
{
    public sealed class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public UsuarioRepository(SIFMContext context, IServiceScopeFactory serviceScopeFactory) : base(context)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public List<Usuario> GetUsuariosAdministrador()
        {
            return (from usuario in _context.Usuario
                    join usuarioRol in _context.UsuarioRol on usuario.IdUsuario equals usuarioRol.IdUsuario
                            join rol in _context.Rol on usuarioRol.IdRol equals rol.IdRol
                            where 
                                rol.IdRol == ((int)Roles.SuperAdministrador) || rol.IdRol == ((int)Roles.Administrador)
                            select usuario
                            ).ToList();
        }

        //Editar Usuario
        public bool EditarUsuario(Usuario editar, ref string mensaje)
        {
            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    using (var context = scope.ServiceProvider.GetRequiredService<SIFMContext>())
                    {
                        context.Update(editar);
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return false;
            }
        }

        public List<Usuario> ConsultarUsuarios()
        {
            return _context.Usuario
                .Include(x => x.SpusuarioRol)
                .Include(x => x.SpusuarioRol).ThenInclude(x => x.IdRolNavigation)
                .Where(x => x.Estado != ((int)EstadoUsuario.Eliminado)).ToList();
        }

        public Usuario ObtenerUsuario(long id)
        {
            return _context.Usuario
                .Include(x => x.SpusuarioRol)
                .Include(x => x.SpusuarioRol).ThenInclude(x => x.IdRolNavigation)
                .Where(x => x.IdUsuario == id && x.Estado != ((int)EstadoUsuario.Eliminado)).FirstOrDefault();
        }

        //Obtener Usuarios
        public Usuario ObtenerUsuario(long idUsuario, ref string mensaje)
        {
            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    using (var context = scope.ServiceProvider.GetRequiredService<SIFMContext>())
                    {
                        Usuario obtener = context.Usuario.Where(x => x.IdUsuario == idUsuario).FirstOrDefault();
                        return obtener;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return null;
            }
        }

        //Obtener Codigo Usuario
        public List<Usuario> ObtenerCodigoUsuario()
        {
            return _context.Usuario.Where(x => x.Estado == 1).ToList();
        }

        public bool GuardarUsuarioRol(UsuarioRol model)
        {
            var result = _context.UsuarioRol.FirstOrDefault(x => x.IdUsuario == model.IdUsuario && x.Estado == true);
            if (result == null)
            {
                _context.UsuarioRol.Add(model);
            }
            else
            {
                result.IdUsuario = model.IdUsuario;
                result.IdRol = model.IdRol;
                _context.UsuarioRol.Update(result);
            }

            return _context.SaveChanges() > 0;
        }
    }
}
