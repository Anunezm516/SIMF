using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Repositories
{
    public class ComprasRepository : Repository<CFactura>, IComprasRepository
    {
        public ComprasRepository(SIFMContext context) : base(context)
        {
        }

        public CFactura GetFactura(long Id, EstadoFactura Estado)
        {
            return _context.CFactura
                .Include(x => x.FacturaFormaPago)
                .Include(x => x.FacturaDetalle)
                .Include(x => x.FacturaAdjunto)
                .FirstOrDefault(x => x.Estado == Estado && x.FacturaId == Id);
        }

        public CFactura GetFacturaNoIgualEstado(long Id, EstadoFactura Estado)
        {
            return _context.CFactura
                .Include(x => x.FacturaFormaPago)
                .Include(x => x.FacturaDetalle)
                .FirstOrDefault(x => x.Estado != Estado && x.FacturaId == Id);
        }

        public List<CFactura> GetFacturas(EstadoFactura[] Estados)
        {
            return _context.CFactura
                .Include(x => x.FacturaDetalle)
                .Where(x => Estados.Contains(x.Estado)).ToList();
        }
    }
}
