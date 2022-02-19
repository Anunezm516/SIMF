using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public sealed class Proveedor : Auditoria
    {
        [Key]
        public long ProveedorId { get; set; }

        [MaxLength(3)]
        public string TipoIdentificacion { get; set; }

        [MaxLength(25)]
        public string Identificacion { get; set; }

        [MaxLength(100)]
        public string RazonSocial { get; set; }

        [MaxLength(100)]
        public string NombreComercial { get; set; }

        [MaxLength(300)]
        public string Direccion { get; set; }

        [MaxLength(100)]
        public string CorreoElectronico { get; set; }

        [MaxLength(50)]
        public string Telefono { get; set; }
    }
}
