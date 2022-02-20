using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Repositories
{
    public class BodegaRepository : Repository<Bodega>, IBodegaRepository
    {
        public BodegaRepository(SIFMContext context) : base(context)
        {
        }

        public List<Bodega> GetBodegas()
        {
            return _context.Bodega.Where(x => x.Estado).ToList();
        }
    }
}
