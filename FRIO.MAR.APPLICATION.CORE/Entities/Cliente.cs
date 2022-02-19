using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public sealed class Cliente : Auditoria
    {
        [Key]
        public long ClienteId { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string TipoIdentificacion { get; set; }

        [Column(TypeName = "varchar(25)")]
        public string Identificacion { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string RazonSocial { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string NombreComercial { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string Direccion { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string CorreoElectronico { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Telefono { get; set; }
    }
}
