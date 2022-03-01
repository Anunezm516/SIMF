using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IVentasRepository : IRepository<Factura>
    {
        Factura GetFactura(long Id, EstadoFactura Estado);
        List<Factura> GetFacturas(EstadoFactura[] Estados);
    }
}
