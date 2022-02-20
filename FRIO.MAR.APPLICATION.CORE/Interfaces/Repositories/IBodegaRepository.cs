using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IBodegaRepository : IRepository<Bodega>
    {
        List<Bodega> GetBodegas();
    }
}
