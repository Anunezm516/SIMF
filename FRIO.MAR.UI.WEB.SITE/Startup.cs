using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using FRIO.MAR.UI.WEB.SITE.Extensions;
using FRIO.MAR.UI.WEB.SITE.Parameters;
using FRIO.MAR.UI.WEB.SITE.Settings;
using GS.TOOLS;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FRIO.MAR.UI.WEB.SITE
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string mensaje = null;
            WebSiteSettings settings = GSUtilities.LeerAppSettings<WebSiteSettings>(typeof(Program), ref mensaje, "appsettings.json");

            //Para habilitar sesiones
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(settings.SITIOWEB.LimiteSesion);//30 minutos
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(culture: "es-CO", uiCulture: "es-CO");
                options.SupportedCultures = new CultureInfo[] { new CultureInfo("es-CO") };
                options.SupportedUICultures = new CultureInfo[] { new CultureInfo("es-CO") };

                options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
                {
                    // My custom request culture logic
                    return new ProviderCultureResult("es");
                }));
            });

            if (settings == null) throw new Exception(mensaje);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddSessionStateTempDataProvider();

            services.AddRazorPages();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opciones =>
                {
                    opciones.AccessDeniedPath = "/Home/Forbidden";
                    opciones.LoginPath = "/Account/Login";
                    opciones.LogoutPath = "/Account/Logoff";
                    //opciones.
                    opciones.ExpireTimeSpan = TimeSpan.FromHours(settings.SITIOWEB.LimiteSesion);
                });

            services.AddRepositorys();
            services.AddServices();
            services.AddServicesMediate(Configuration);
            services.AddLoggerServices(Configuration);

            var CadenaConexion = $"Server={settings.SIFM.DataSource};Database={settings.SIFM.InitialCatalog};User Id={settings.SIFM.UserId};Password={settings.SIFM.Password};"; 
            /* GSUtilities.CadenaConexion(settings.SIFM.DataSource,
                    settings.SIFM.InitialCatalog,
                    settings.SIFM.UserId,
                    settings.SIFM.Password, DomainConstants.ENCRIPTA_KEY, ref mensaje);*/
            if (CadenaConexion == null) throw new Exception("Cadena de conexión inválida");

            services.AddDbContext<SIFMContext>(options => options.UseSqlServer(CadenaConexion));

            WebSiteParameters.WebLimiteConsulta = settings.SITIOWEB.LimiteConsulta;
            WebSiteParameters.WebFooter = string.Format("{0} v{1}", settings.SITIOWEB.Footer, AppConstants.Version).Replace("{AnioActual}", APPLICATION.CORE.Utilities.Utilidades.GetHoraActual().Year.ToString());
            WebSiteParameters.WebReCaptchaClaveSitioWeb = GSCrypto.DescifrarClave(settings.SITIOWEB.Recaptcha.ClaveSitioWeb, DomainConstants.ENCRIPTA_KEY);
            WebSiteParameters.WebReCaptchaClaveComGoogle = GSCrypto.DescifrarClave(settings.SITIOWEB.Recaptcha.ClaveComGoogle, DomainConstants.ENCRIPTA_KEY);

            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUtilidadRepository utilidadRepository)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Pages/pages-500");
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Pages/pages-404";
                    await next();
                }

                if (context.Response.StatusCode == 401)
                {
                    context.Request.Path = "/Pages/pages-solicitud-permiso";
                    await next();
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            //Habilitar sesion
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

            });

            InitialData start = new InitialData(utilidadRepository);
            start.Start();
        }
    }
}
