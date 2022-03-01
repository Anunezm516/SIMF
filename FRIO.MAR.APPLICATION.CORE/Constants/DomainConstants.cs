using System;
using System.ComponentModel;
using System.Net;

namespace FRIO.MAR.APPLICATION.CORE.Constants
{
    public static class DomainConstants
    {
        public const string CODIGO_TRANSACCION = "{Fecha}-{Usuario}-{Compania}-{Tipo_Inventario}-{Tipo_Movimiento}-{Tabla}-{Id_Inventario}-{Hora}";

        public const string MENSAJE_CAMPO_REQUIRED = "Este campo es requerido";
        public const string MENSAJE_CAMPO_MAX_LENGTH = "Ha excedido la capacidad máxima de caracteres";

        public const string ERROR_GENERAL = "E999";
        public const string ENCRIPTA_KEY = "-L4gRa(3*.!,";
        public const string COMPONENTE_NAME = "FRIO_MAR";

        public const string ERROR_CLIENTE_ANONIMO = "P101";
        public const string ERROR_CLIENTE_REGISTRADO_IDENTIFICACION = "P102";

        public const string ERROR_PROVEEDOR_ANONIMO = "P201";
        public const string ERROR_PROVEEDOR_REGISTRADO_IDENTIFICACION = "P202";

        public const string ERROR_BODEGA_ANONIMO = "P301";
        public const string ERROR_BODEGA_REGISTRADO_CODIGO = "P302";
        public const string ERROR_BODEGA_REGISTRADO_PRODUCTO = "P303";

        public const string ERROR_SUCURSAL_ANONIMO = "P401";
        public const string ERROR_SUCURSAL_REGISTRADO_CODIGO = "P402";

        public const string ERROR_USUARIO_REGISTRADO_MAIL = "P501";
        public const string ERROR_USUARIO_REGISTRADO_USERNAME = "P502";
        public const string ERROR_USUARIO_NO_REGISTRADO = "P503";
        public const string ERROR_USUARIO_ANONIMO = "P504";

        public const string ERROR_PRODUCTO_ANONIMO = "P601";
        public const string ERROR_PRODUCTO_REGISTRADO_CODIGO = "P602";
        public const string ERROR_PRODUCTO_REGISTRADO_BODEGA = "P603";


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


            ERROR_CLIENTE_ANONIMO => $"{CodigoError}: El cliente no se encuetra registrado.",
            ERROR_CLIENTE_REGISTRADO_IDENTIFICACION => $"{CodigoError}: La identificación del cliente ya se encuentra registrada.",

            ERROR_PROVEEDOR_ANONIMO => $"{CodigoError}: El proveedor no se encuetra registrado.",
            ERROR_PROVEEDOR_REGISTRADO_IDENTIFICACION => $"{CodigoError}: La identificación del proveedor ya se encuentra registrada.",

            ERROR_BODEGA_ANONIMO => $"{CodigoError}: La bodega no se encuetra registrado.",
            ERROR_BODEGA_REGISTRADO_CODIGO => $"{CodigoError}: El código de la bodega ya se encuentra registrada.",
            ERROR_BODEGA_REGISTRADO_PRODUCTO => $"{CodigoError}: No se puede eliminar la bodega, la bodega contiene productos actualmente.",

            ERROR_SUCURSAL_ANONIMO => $"{CodigoError}: La sucursal no se encuetra registrado.",
            ERROR_SUCURSAL_REGISTRADO_CODIGO => $"{CodigoError}: El código de la sucursal ya se encuentra registrada.",

            ERROR_PRODUCTO_ANONIMO => $"{CodigoError}: El producto no se encuetra registrado.",
            ERROR_PRODUCTO_REGISTRADO_CODIGO => $"{CodigoError}: El código del producto ya se encuentra registrada.",
            ERROR_PRODUCTO_REGISTRADO_BODEGA => $"{CodigoError}: No se puede eliminar el producto, la producto se encuentra registrado en una bodega.",
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

    public enum TipoProducto
    {
        Bien = 1,
        Servicio = 2
    }

    public enum TipoInventario
    {
        proveedor = 1,
        venta = 2
    }

    public enum TipoMovimientoInventario
    {
        Manual = 1,
        Tranferencia = 2,
        Factura = 3
    }

    public enum EstadoFactura
    {
        Eliminado = 0,
        Facturado = 1,
        Borrador = 2,
        Proforma = 3
    }

    public class RespuestaVentaDto
    {
        public string NumDocumento { get; set; }
    }

}
