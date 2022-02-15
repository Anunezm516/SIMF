
using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using GS.TOOLS;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FRIO.MAR.INFRA.QUERY.QueryServices
{
    public sealed class PortalQueryService : BaseQueryService, IPortalQueryService
    {
        public PortalQueryService(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        public CredencialQueryDto ConsultarCredencialXNit(string nitCompania, ref string mensaje)
        {
            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    using (var edocQueryContext = scope.ServiceProvider.GetRequiredService<SIFMContext>())
                    {
                        return edocQueryContext.ConsultarCredencialXNit(nitCompania);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return null;
            }
        }

        public IEnumerable<ParametroQueryDto> ConsultarParametroXCodigoXNit(string nitCompania, string codigos, ref string mensaje)
        {
            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    using (var edocQueryContext = scope.ServiceProvider.GetRequiredService<SIFMContext>())
                    {
                        return edocQueryContext.ConsultarParametroXCodigoXNit(nitCompania, codigos);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return null;
            }
        }

        public IEnumerable<PermisosQueryDto> ConsultarVentanasActivas(ref string mensaje)
        {
            try
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    using (var edocQueryContext = scope.ServiceProvider.GetRequiredService<SIFMContext>())
                    {
                        return edocQueryContext.ConsultarVentanasActivas();
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
