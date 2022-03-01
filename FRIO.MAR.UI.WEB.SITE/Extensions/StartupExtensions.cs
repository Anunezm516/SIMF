
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using GS.IO.Constants;
using GS.TOOLS;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.UI.WEB.SITE.Settings;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using FRIO.MAR.CROSSCUTTING.LOG.Services;
using NLog;
using FRIO.MAR.APPLICATION.CORE.Parameters;
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.AppServices;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Repositories;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.DomainServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices;
using FRIO.MAR.INFRA.QUERY.QueryServices;
using FRIO.MAR.INFRA.SERVICE.MAIL.Services;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Services;

namespace FRIO.MAR.UI.WEB.SITE.Extensions
{
    public static class StartupExtensions
    {
        public static void AddRepositorys(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IPermisoRepository, PermisoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUtilidadRepository, UtilidadRepository>();
            services.AddScoped<INotificacionRepository, NotificacionRepository>();
            services.AddScoped<IMailRepository, MailRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProveedorRepository, ProveedorRepository>();
            services.AddScoped<IBodegaRepository, BodegaRepository>();
            services.AddScoped<ISucursalRepository, SucursalRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IInventarioRepository, InventarioRepository>();
            services.AddScoped<IVentasRepository, VentasRepository>();

        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<IAccountAppService, AccountAppService>();
            services.AddScoped<IMailDomainService, MailDomainService>();
            services.AddScoped<IPermisoAppService, PermisoAppService>();

            services.AddScoped<IPortalQueryService, PortalQueryService>();
            services.AddScoped<IRolQueryService, RolQueryService>();
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<IRolAppService, RolAppService>();
            services.AddScoped<IUsuarioQueryService, UsuarioQueryService>();


            services.AddScoped<IEnvioMail, EnvioMail>();
            services.AddScoped<INotificacionAppService, NotificacionAppService>();

            services.AddScoped<IClienteAppService, ClienteAppService>();
            services.AddScoped<IProveedorAppService, ProveedorAppService>();
            services.AddScoped<IBodegaAppService, BodegaAppService>();
            services.AddScoped<ISucursalAppService, SucursalAppService>();
            services.AddScoped<IProductoAppService, ProductoAppService>();
            services.AddScoped<IInventarioDomainService, InventarioDomainService>();
            services.AddScoped<IVentasDomainService, VentasDomainService>();

        }

        public static void AddServicesMediate(this IServiceCollection services, IConfiguration Configuration)
        {

        }

        public static void AddValidatorService(this IServiceCollection services)
        {

        }

        public static void AddLoggerServices(this IServiceCollection services, IConfiguration configuration)
        {
            string mensaje = "";
            WebSiteSettings settings = GSUtilities.LeerAppSettings<WebSiteSettings>(typeof(Program), ref mensaje, "appsettings.json");
            if (settings.LOG.Habilitar)
            {
                switch ((DestinosIO)settings.LOG.Destino)
                {
                    case DestinosIO.Disk:
                        services.AddSingleton<ILogInfraServices>(_ => new LogDiskInfraServices(settings.LOG.Habilitar, settings.LOG.Disk.Ruta, settings.LOG.Disk.Layout, settings.LOG.Disk.FileName));
                        break;
                    default: throw new Exception("Destino LOG no reconocido: " + settings.LOG.Destino);
                }

                GlobalDiagnosticsContext.Set("Application", DomainParameters.NombreAplicacion);
                GlobalDiagnosticsContext.Set("Version", AppConstants.Version);
            }
        }
    }
}
