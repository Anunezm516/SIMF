using System.ComponentModel;

namespace FRIO.MAR.APPLICATION.CORE.Contants
{
    public static class AppConstants
    {
        public const string semillaEncriptacionApi = "@Bip1YvdkmuGooO4ERrx1@";
        public const string Version  = "1.0";
        public const string MensajeUsuarioLogin = "Usuario o Contraseña son incorrectos";
        public const string MensajeUsuarioBloqueado = "¡Usuario bloqueado!, Ha excedido el máximo de intentos, ingrese a la opción: ¿Olvidaste tu contraseña? Para el desbloqueo del usuario.";
    }

    public enum Roles
    {
        Estandar,
        SuperAdministrador = 1,
        Administrador = 2,
        Agente,
        Seguimiento
    }

    public enum TipoNotificacion
    {
        Notificacion = 1,
        Alerta = 2
    }

}
