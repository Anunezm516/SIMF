
using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using GS.TOOLS;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FRIO.MAR.INFRA.QUERY.QueryServices
{
    public class RolQueryService : BaseQueryService, IRolQueryService
    {
        private readonly SIFMContext context;

        public RolQueryService(SIFMContext context, IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            this.context = context;
        }

        public List<RolesQueryDto> ConsultaRolesXCompania(long idCompania, ref string mensaje)
        {
            try
            {
                using var scope = serviceScopeFactory.CreateScope();
                using var edocQueryContext = scope.ServiceProvider.GetRequiredService<SIFMContext>();
                return edocQueryContext.ConsultaRolesXCompania(idCompania).ToList();
            }
            catch (Exception ex)
            {
                mensaje = $"Error al consultar Roles por Compania. EX: {ex.Message}";
                return null;
            }
        }

        public List<IdQueryDto> ConsultarRolVentanas(short IdRol, ref string mensaje)
        {
            try
            {
                return context.RolPermiso.Where(x => x.IdRol == IdRol).Select(c => new IdQueryDto { Id = (int)(c.IdPermiso ?? 0) }).ToList();
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return null;
            }
        }
    }
}
