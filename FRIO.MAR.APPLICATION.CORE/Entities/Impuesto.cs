using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class Impuesto
    {
        [Key]
        public int ImpuestoId { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Nombre { get; set; }
        public decimal Porcentaje { get; set; }
        public bool Estado { get; set; }

        public int TipoImpuestoId { get; set; }
        public virtual TipoImpuesto TipoImpuesto { get; set; }
    }
}
