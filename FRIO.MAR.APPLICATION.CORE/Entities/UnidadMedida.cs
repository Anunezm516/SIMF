using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class UnidadMedida
    {
        public int UnidadMedidaId { get; set; }

        [Column(TypeName = "varchar(5)")]
        public string Codigo { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Nombre { get; set; }

        [Column(TypeName = "varchar(25)")]
        public string Abreviatura { get; set; }

        public bool Estado { get; set; }
    }
}
