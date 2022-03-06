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
    public class ProductoClienteRepository : Repository<ProductoCliente>, IProductoClienteRepository
    {
        public ProductoClienteRepository(SIFMContext context) : base(context)
        {
        }

        public List<ProductoCliente> GetProductos(long ClienteId)
        {
            return _context.ProductoCliente.Where(x => x.ClienteId == ClienteId && x.Estado == true).ToList();
            //return _context.Clientes.Include(x => x.ProductoCliente)
            //    .FirstOrDefault(x => x.ClienteId == ClienteId && x.Estado)?.ProductoCliente?.ToList();
        }

        public ProductoCliente GetProducto(long ClienteId, long ProductoClienteId)
        {
            return _context.ProductoCliente
                .Include(x => x.ProductoClienteImagen).FirstOrDefault(x => x.ProductoClienteId == ProductoClienteId);
        }
    }
}
