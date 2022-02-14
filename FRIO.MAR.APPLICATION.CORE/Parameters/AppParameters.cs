

using FRIO.MAR.APPLICATION.CORE.Constants;

namespace FRIO.MAR.APPLICATION.CORE.Parameters
{
    public static class AppParameters
    {
        public static string NombreAplicacion { get; set; }
        public static string DirectorioRecovery { get; set; }
        public static string ExcepcionGenerica { get; } = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL);
    }
}
