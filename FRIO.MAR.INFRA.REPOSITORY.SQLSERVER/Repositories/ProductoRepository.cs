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
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        public ProductoRepository(SIFMContext context) : base(context)
        {
        }

        public List<Producto> GetProductos()
        {
            return _context.Producto.Where(x => x.Estado).ToList();
        }

        public Producto GetProducto(long Id)
        {
            return _context.Producto.Include(x => x.InventarioProveedor).Include(x => x.InventarioVenta).FirstOrDefault(x => x.ProductoId == Id);
        }
    }
}
