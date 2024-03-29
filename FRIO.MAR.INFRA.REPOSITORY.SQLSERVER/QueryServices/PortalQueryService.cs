﻿
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
    public sealed class PortalQueryService : BaseQueryService, IPortalQueryService
    {
        private readonly SIFMContext context;

        public PortalQueryService(SIFMContext context, IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            this.context = context;
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

        public List<ParametroQueryDto> ConsultarParametroXCodigoXNit(string nitCompania, string codigos, ref string mensaje)
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

        public List<PermisosQueryDto> ConsultarVentanasActivas(ref string mensaje)
        {
            try
            {
                return context.Permisos.Where(x => x.Estado == 1).Select(c => new PermisosQueryDto
                {
                    Codigo = c.Codigo,
                    Descripcion = c.Descripcion,
                    IdPermiso = c.IdPermiso,
                    Icono = c.Icono,
                    IdPadre = c.IdPadre,
                    NombreAbreviado = c.NombreAbreviado,
                    Url = c.Url,
                }).ToList();
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return null;
            }
        }
    }
}
