using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Models
{
    public class PerfilModel
    {
        public string Usuario { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public List<IFormFile> Imagen { get; set; }
        public string ImagenBase64 { get; set; }
        public string ImagenRuta { get; set; }
        public string NombreRol { get; set; }

        public PerfilModel()
        {

        }

        public PerfilModel(Usuario usuario, List<UsuarioRol> usuarioRol)
        {
            Rol rol = null;

            if (usuarioRol != null && usuarioRol.Any())
            {
                rol = usuarioRol.FirstOrDefault().IdRolNavigation;
            }

            NombreRol = rol?.Nombre ?? "";
            Usuario = usuario.Username;
            Nombre = usuario.Nombre;
            Apellido = usuario.Apellido;
            CorreoElectronico = usuario.CorreoElectronico;
            Telefono = usuario.Telefono;
            ImagenRuta = usuario.Foto;
        }

    }
}
