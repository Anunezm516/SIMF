using System.ComponentModel.DataAnnotations;

namespace FRIO.MAR.UI.WEB.SITE.Models
{
    public class UsuarioRecuperaClave
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "El correo es requerido")]
        public string CorreoElectronico { get; set; }
    }
}
