
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Repositories
{
    public class PermisoRepository : Repository<Permisos>, IPermisoRepository
    {
        public PermisoRepository(SIFMContext context) : base(context)
        {
        }

    }
}
