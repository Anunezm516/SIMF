

using Microsoft.Extensions.DependencyInjection;

namespace FRIO.MAR.INFRA.QUERY.QueryServices
{
    public abstract class BaseQueryService
    {
        protected readonly IServiceScopeFactory serviceScopeFactory;

        internal BaseQueryService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }
    }
}
