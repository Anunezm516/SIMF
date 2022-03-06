using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IProductoClienteRepository : IRepository<ProductoCliente>
    {
        List<ProductoCliente> GetProductos(long ClienteId);
        ProductoCliente GetProducto(long ClienteId, long ProductoClienteId);
    }
}
