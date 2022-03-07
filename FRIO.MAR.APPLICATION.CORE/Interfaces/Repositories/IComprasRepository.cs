using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IComprasRepository : IRepository<CFactura>
    {
        CFactura GetFactura(long Id, EstadoFactura Estado);
        CFactura GetFacturaNoIgualEstado(long Id, EstadoFactura Estado);
        List<CFactura> GetFacturas(EstadoFactura[] Estados);
    }
}
