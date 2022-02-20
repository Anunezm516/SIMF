
using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FRIO.MAR.APPLICATION.CORE.DTOs.AppServices
{
    public class UsuariosAppResultDto
    {
        public string IdUsuario { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public long IdRol { get; set; }
        public string NombreRol { get; set; }

        public UsuariosAppResultDto()
        {

        }
        
        public UsuariosAppResultDto(Usuario usuario, List<UsuarioRol> usuarioRol)
        {
            Rol rol = null;

            if (usuarioRol != null && usuarioRol.Any())
            {
                rol = usuarioRol.FirstOrDefault().IdRolNavigation;
            }

            IdRol = rol?.IdRol ?? 0;
            NombreRol = rol?.Nombre ?? "";

            IdUsuario = Crypto.CifrarId(usuario.IdUsuario);
            Nombre = usuario.Nombre;
            Usuario = usuario.Username;
            CorreoElectronico = usuario.CorreoElectronico;
            Apellido = usuario.Apellido;
            Telefono = usuario.Telefono;
        }
    
    }
}
