
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using GS.TOOLS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Repositories
{
    public class AccountRepository : Repository<Usuario>, IAccountRepository
    {
        public AccountRepository(SIFMContext context) : base(context)
        {
        }

        public bool RegistrarAccesoUsuario(AccesoUsuario acceso)
        {
            _context.AccesoUsuario.Add(acceso);
            return _context.SaveChanges() > 0;
        }

        public List<VentanaLoginQueryDto> ConsultarVentana(long IdUsuario, ref string mensaje)
        {
            try
            {
                var rol = _context.UsuarioRol.Include(x => x.IdRolNavigation).FirstOrDefault(x => x.Estado == true && x.IdUsuario == IdUsuario);
                var permisos = _context.RolPermiso.Include(x => x.IdPermisoNavigation).Where(x => x.Estado == true && x.IdRol == rol.IdRol);

                return permisos.Select(c => new VentanaLoginQueryDto
                {
                    Codigo = c.IdPermisoNavigation.Codigo ?? 0,
                    Icono = c.IdPermisoNavigation.Icono ?? "",
                    IdPadre = c.IdPermisoNavigation.IdPadre,
                    IdPermiso = c.IdPermiso ?? 0,
                    IdRol = rol.IdRol ?? 0,
                    NombreAbreviado = c.IdPermisoNavigation.NombreAbreviado ?? "",
                    Rol = rol.IdRolNavigation.Nombre ?? "",
                    Url = c.IdPermisoNavigation.Url ??"",
                }).ToList();

                //(from p in _context.Permisos join rp in _context.RolPermiso on p.)
                //var res = (from r in _context.Rol join pr in _context.RolPermiso on r.IdRol equals pr.IdRol);

                ///return _context.ConsultarVentanasXUsuarioLogin(IdUsuario);
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                return null;
            }
        }
    }
}
