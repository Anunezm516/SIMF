
namespace FRIO.MAR.UI.WEB.SITE.Constants
{
    public static class WebSiteConstants
    {
        public const string ROLES_ADMIN = "SuperAdministrador,Administrador";
        public const string ROLES_SEGUIMIENTO = "Seguimiento";
        public const string ROLES_AGENTE = "Agente";
        public const string ROLES_ESTANDAR = "Estandar";

        public const string MENSAJE_SWEET_ALERT_ERROR = "<script> $(document).ready(function () { MensajeError(); });  function MensajeError(){ Swal.fire({ icon: 'error', title: '', text: '{Mensaje_Respuesta}' }); } </script>";
        public const string MENSAJE_SWEET_ALERT_SUCCESS = "<script>  $(document).ready(function () { MensajeSuccess(); });  function MensajeSuccess(){ Swal.fire({ text: '{Mensaje_Respuesta}', icon: 'success', confirmButtonText: 'OK', allowOutsideClick: false }); } </script>";

        public const string MENSAJE_TOAST_ALERT_ERROR = "<script> $(document).ready(function () { MensajeError(); });  function MensajeError(){ $.toast({ heading: '¡Alerta!', text: '{Mensaje_Respuesta}', position: 'top-right', stack: 6, hideAfter: 5000, showHideTransition: 'fade', icon: 'error' }); } </script>";
        public const string MENSAJE_TOAST_ALERT_SUCCESS = "<script>  $(document).ready(function () { MensajeSuccess(); });  function MensajeSuccess(){ $.toast({ heading: 'Exitoso', text: '{Mensaje_Respuesta}', position: 'top-right', stack: 5, showHideTransition: 'fade', icon: 'success' }); } </script>";


        //public const string MENSAJE_SWEET_ALERT_ERROR = "Swal.fire({ type: 'error', title: 'Plan', text: '{Mensaje_Respuesta}' });";
        //public const string MENSAJE_SWEET_ALERT_SUCCESS = "Swal.fire({ text: '{Mensaje_Respuesta}', type: 'success', confirmButtonText: 'OK', allowOutsideClick: false });";
    }

    public enum VentanasSoporte
    {
        Dashboard = 100,

        Configuracion=200,
        Usuarios,
        Permisos, 
        Rol,
        

        Mantenimiento=300,
        Clientes,
        Proveedores,
        Bodegas,
        Sucursales,
        ProductosInternos,
        ProductosCliente,

        Ventas=400,

        Compras=500,

        Inventario=600,

        Reportes=700,
        ReportesVentas=701,
        ReportesCompras=702,
        ReportesInventario=703,
        
    }
}
