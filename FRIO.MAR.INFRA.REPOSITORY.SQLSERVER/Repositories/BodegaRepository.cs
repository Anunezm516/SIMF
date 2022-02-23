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
    public class BodegaRepository : Repository<Bodega>, IBodegaRepository
    {
        public BodegaRepository(SIFMContext context) : base(context)
        {
        }

        public Bodega GetBodega(long Id)
        {
            return _context.Bodega
                .Include(c => c.InventarioProveedor)
                .Include(v => v.InventarioVenta)
                .Include(x => x.SucursalBodega).FirstOrDefault(x => x.BodegaId == Id && x.Estado);
        }

        public List<Bodega> GetBodegas()
        {
            return _context.Bodega.Where(x => x.Estado).ToList();
        }

        //public bool ActualizarSucursalesBodega(long BodegaId, List<long> SucursalesId)
        //{

        //}
    }
}
