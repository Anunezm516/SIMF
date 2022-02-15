
namespace FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices
{
    public class UsuariosQueryDto
    {
        public long IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string IdTelegram { get; set; }
    }
}
