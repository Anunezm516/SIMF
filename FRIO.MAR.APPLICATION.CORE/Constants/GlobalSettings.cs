using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Contants
{
    public static class GlobalSettings
    {
        
        public static int LoginAppNumeroIntentoBloqueo { get; set; }
        public static int LoginAppDiasForzarCambioPassword { get; set; }
        public static int NumeroDiasReaperturaTicket { get; set; }


        public static string ConfiguracionMailUser { get; set; }
        public static string ConfiguracionMailPassword { get; set; }
        public static int ConfiguracionMailPort { get; set; }
        public static string ConfiguracionMailHost { get; set; }
        public static bool ConfiguracionMailSsl { get; set; }

        public static int NumeroIntentosEnvioBloqueo { get; set; }

        public static string TimeZoneId { get; set; }
    }
}
