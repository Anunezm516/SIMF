
using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FRIO.MAR.INFRA.QUERY.QueryServices
{
    public class UsuarioQueryService : BaseQueryService, IUsuarioQueryService
    {
        public UsuarioQueryService(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }
        
        public IEnumerable<UsuarioInternoQueryDto> ConsultaUsuarioInternoXCompania(long idCompania, ref string mensaje)
        {
            try
            {
                using var scope = serviceScopeFactory.CreateScope();
                using var edocQueryContext = scope.ServiceProvider.GetRequiredService<SIFMContext>();
                return edocQueryContext.ConsultaUsuarioInternoXCompania(idCompania);
            }
            catch (Exception ex)
            {
                mensaje = $"Error al consultar Usuarios Internos por Compania. EX: {ex.Message}";
                return null;
            }
        }

        //public IEnumerable<UsuariosQueryDto> ObtenerUsuario(long idUsuario, ref string mensaje)
        //{
        //    try
        //    {
        //        using var scope = serviceScopeFactory.CreateScope();
        //        using var edocQueryContext = scope.ServiceProvider.GetRequiredService<GSEPSoporteQueryContext>();
        //        //return edocQueryContext.ObtenerUsuario(idUsuario);
        //        return edocQueryContext.ObtenerUsuario(idUsuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        mensaje = $"Error al consultar Usuarios. EX: {ex.Message}";
        //        return null;
        //    }
        //}

        //public List<UsuariosQueryDto> ConsultarUsuario(ref string mensaje)
        //{
        //    try
        //    {
        //        using var scope = serviceScopeFactory.CreateScope();
        //        using var edocQueryContext = scope.ServiceProvider.GetRequiredService<GSEPSoporteQueryContext>();
        //        return edocQueryContext.ConsultarUsuarios();
        //        return edocQueryContext.ConsultarUsuario();
        //    }
        //    catch (Exception ex)
        //    {
        //        mensaje = $"Error al consultar Usuarios. EX: {ex.Message}";
        //        return null;
        //    }
        //}

    }
}
