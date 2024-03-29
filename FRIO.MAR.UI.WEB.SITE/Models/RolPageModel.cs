﻿using FRIO.MAR.APPLICATION.CORE.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace FRIO.MAR.UI.WEB.SITE.Models
{
    public class RolPageModel
    {
        public long IdRol { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string Nombre { get; set; }

        //[Required(ErrorMessage = "La descripción del rol es requerida")]
        //public string Descripcion { get; set; }
        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [Range(0, 1, ErrorMessage = "El estado es inválido")]
        public byte Estado { get; set; }

    }
}
