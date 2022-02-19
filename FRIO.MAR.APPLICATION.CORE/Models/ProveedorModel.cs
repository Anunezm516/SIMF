using FRIO.MAR.APPLICATION.CORE.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Models
{
    public class ProveedorModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUERIDO)]
        public string TipoIdentificacion { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUERIDO)]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUERIDO)]
        public string RazonSocial { get; set; }

        public string NombreComercial { get; set; }

        public string Direccion { get; set; }

        public string CorreoElectronico { get; set; }

        public string Telefono { get; set; }

        public string Ip { get; set; }
        public long Usuario { get; set; }
    }
}

