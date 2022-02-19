using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class TipoImpuesto
    {
        [Key]
        public int TipoImpuestoId { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Nombre { get; set; }

        [Column(TypeName = "varchar(4)")]
        public string Codigo { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<Impuesto> Impuestos { get; set; }
    }
}
