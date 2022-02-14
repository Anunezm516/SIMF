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

        public static string ObtenerDescripcionError(string CodigoError) => CodigoError switch
        {
            ERROR_GENERAL => "Ha ocurrido un error inesperado, por favor intenta nuevamente en unos minutos y si el error persiste comunícate con el área de soporte. ",

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
