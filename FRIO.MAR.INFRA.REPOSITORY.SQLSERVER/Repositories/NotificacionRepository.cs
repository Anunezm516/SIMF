using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Repositories
{
    public class NotificacionRepository : Repository<Notificacion>, INotificacionRepository
    {
        public NotificacionRepository(SIFMContext context) : base(context)
        {
        }

        public List<Notificacion> GetNotificaciones(long IdUsuario, bool Leidas)
        {
            return _context.Notificaciones.Where(x => x.IdUsuario == IdUsuario && x.Estado == 1 && x.EsNotificacionLeida == Leidas).ToList();
        }
    }
}
