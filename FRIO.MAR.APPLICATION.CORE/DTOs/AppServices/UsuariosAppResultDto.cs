
using FRIO.MAR.APPLICATION.CORE.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FRIO.MAR.APPLICATION.CORE.DTOs.AppServices
{
    public class UsuariosAppResultDto
    {
        public string IdUsuario { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUERIDO)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUERIDO)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUERIDO)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUERIDO)]
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string IdTelegram { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUERIDO)]
        public long IdRol { get; set; }
        public string NombreRol { get; set; }

        public UsuariosAppResultDto()
        {

        }
        /*
        public UsuariosAppResultDto(Spusuario usuario, List<SpusuarioRol> usuarioRol)
        {
            Sprol rol = null;

            if (usuarioRol != null && usuarioRol.Any())
            {
                rol = usuarioRol.FirstOrDefault().IdRolNavigation;
            }

            IdRol = rol?.IdRol ?? 0;
            NombreRol = rol?.Nombre ?? "";

            IdUsuario = Crypto.CifrarId(usuario.IdUsuario);
            Nombre = usuario.Nombre;
            Usuario = usuario.Usuario;
            CorreoElectronico = usuario.CorreoElectronico;
            Apellido = usuario.Apellido;
            Telefono = usuario.Telefono;
            IdTelegram = usuario.IdTelegram;
        }
    */
    }
}
