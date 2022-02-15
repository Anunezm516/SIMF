
using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using GS.TOOLS;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FRIO.MAR.INFRA.QUERY.QueryServices
{
    public class RolQueryService : BaseQueryService, IRolQueryService
    {
        public RolQueryService(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        public IEnumerable<RolesQueryDto> ConsultaRolesXCompania(long idCompania, ref string mensaje)
        {
            try
            {
                using var scope = serviceScopeFactory.CreateScope();
                using var edocQueryContext = scope.ServiceProvider.GetRequiredService<SIFMContext>();
                return edocQueryContext.ConsultaRolesXCompania(idCompania);
            }
            catch (Exception ex)
            {
                mensaje = $"Error al consultar Roles por Compania. EX: {ex.Message}";
                return null;
            }
        }

        public IEnumerable<IdQueryDto> ConsultarRolVentanas(short IdRol, ref string mensaje)
        {
            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    using (var edocQueryContext = scope.ServiceProvider.GetRequiredService<SIFMContext>())
                    {
                        return edocQueryContext.ConsultarRolVentanas(IdRol);
                    };
                };
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return null;
            }
        }
    }
}
