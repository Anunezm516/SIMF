using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Contants
{
    public static class GlobalSettings
    {
        public static string EnvioMailUrl { get; set; }
        public static string EnvioMailClave { get; set; }
        public static string EnvioMailNombreMostrar { get; set; }
        public static string EnvioMailCorreoMostrar { get; set; }

        public static int LoginAppNumeroIntentoBloqueo { get; set; }
        public static int LoginAppDiasForzarCambioPassword { get; set; }
        public static int NumeroDiasReaperturaTicket { get; set; }

        public static string TelegramToken { get; set; }


        public static int NumeroIntentosEnvioBloqueo { get; set; }
    }
}
