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
    public class VentasRepository : Repository<Factura>, IVentasRepository
    {
        public VentasRepository(SIFMContext context) : base(context)
        {
        }

        public Factura GetFactura(long Id, EstadoFactura Estado)
        {
            return _context.Factura.Include(x => x.FacturaDetalle).FirstOrDefault(x => x.Estado == Estado && x.FacturaId == Id);
        }

        public List<Factura> GetFacturas(EstadoFactura[] Estados)
        {
            return _context.Factura.Include(x => x.FacturaDetalle).Where(x => Estados.Contains(x.Estado)).ToList();
        }
    }
}
