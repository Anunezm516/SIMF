using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.Parameters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace FRIO.MAR.UI.WEB.SITE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DomainParameters.NombreAplicacion = string.Format("{0}_V{1}", DomainConstants.COMPONENTE_NAME, AppConstants.Version);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.ConfigureHttpsDefaults(co =>
                        {
                            co.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
                        });
                    }).UseStartup<Startup>();
                });
    }
}
