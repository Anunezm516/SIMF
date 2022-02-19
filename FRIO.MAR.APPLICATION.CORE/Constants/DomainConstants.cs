using System;
using System.ComponentModel;
using System.Net;

namespace FRIO.MAR.APPLICATION.CORE.Constants
{
    public static class DomainConstants
    {
        public const string MENSAJE_CAMPO_REQUERIDO = "Este campo es requerido";
        public const string ERROR_GENERAL = "E999";
        public const string ENCRIPTA_KEY = "-L4gRa(3*.!,";
        public const string COMPONENTE_NAME = "FRIO_MAR";

        public const string ERROR_USUARIO_REGISTRADO_MAIL = "P501";
        public const string ERROR_USUARIO_REGISTRADO_USERNAME = "P502";
        public const string ERROR_USUARIO_NO_REGISTRADO = "P503";
        public const string ERROR_USUARIO_ANONIMO = "P504";
        public const string ERROR_ENVIAR_MAIL = "P018";

        public const string MAIL_BIENVENIDA_ASUNTO = "MAIL_BIENVENIDA_ASUNTO";
        public const string MAIL_BIENVENIDA_CUERPO = "MAIL_BIENVENIDA_CUERPO";

        public const string MAIL_RECUPERAR_PASSWORD_ASUNTO = "MAIL_RECUPERAR_PASSWORD_ASUNTO";
        public const string MAIL_RECUPERAR_PASSWORD_CUERPO = "MAIL_RECUPERAR_PASSWORD_CUERPO";

        public const string PARAM_CUERPO = "{Cuerpo}";
        public const string PARAM_USERNAME = "{Username}";
        public const string PARAM_PASSWORD_TEMPORAL = "{Password_Temporal}";
        public const string PARAM_USUARIO_NOMBRE_COMPLETO = "{Usuario_Nombre_Completo}";

        public static string ObtenerDescripcionError(string CodigoError) => CodigoError switch
        {
            ERROR_GENERAL => $"{CodigoError}: Ha ocurrido un error inesperado, por favor intenta nuevamente en unos minutos y si el error persiste comunícate con el área de soporte. ",
            ERROR_USUARIO_REGISTRADO_MAIL => $"{CodigoError}: El correo electrónico ya se encuentra registrado.",
            ERROR_USUARIO_REGISTRADO_USERNAME => $"{CodigoError}: El usuario ya se encuentra registrado.",
            ERROR_USUARIO_NO_REGISTRADO => $"{CodigoError}: El usuario no se encuentra registrado.",
            ERROR_USUARIO_ANONIMO => $"{CodigoError}: El usuario o el correo electrónico no encontrado.",
            ERROR_ENVIAR_MAIL => $"{CodigoError}: Error al enviar el mail.",
        };
    }

   
    public enum EstadoUsuario
    {
        Eliminado = 0,
        Activo = 1,
        Bloqueado = 2,
        Inhabilitado = 3
    }

    public enum TipoUsuario
    {
        Interno = 1,
        Externo = 2
    }


}
