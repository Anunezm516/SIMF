
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using GS.TOOLS;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
                return _context.ConsultarVentanasXUsuarioLogin(IdUsuario);
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                return null;
            }
        }
    }
}
