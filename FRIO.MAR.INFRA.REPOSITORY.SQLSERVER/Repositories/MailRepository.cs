
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Repositories
{
    public class MailRepository : Repository<Correo>, IMailRepository
    {
        public MailRepository(SIFMContext context) : base(context)
        {

        }
        public List<Correo> ConsultarCorreosReenviados()
        {
            return _context.Correos.Where(x => x.Estado == (int)EstadosEnvioMail.Enviado && x.IntentosEnvio != null).ToList();
        }

        public List<Correo> ConsultarErroresDeReenvio()
        {
            return _context.Correos.Where(x => x.Error != null && x.IntentosEnvio !=null).ToList();
        }

        public List<Correo> ConsultarCorreosNoEnviados()
        {
            return _context.Correos.Where(x => x.Estado == (int)EstadosEnvioMail.PendienteEnvio).ToList();
        }

        public bool AddMail(Correo correo)
        {
            _context.Correos.Add(correo);
            return _context.SaveChanges() > 0;
        }
    }
}
