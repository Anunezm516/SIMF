using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class UnidadMedidas
    {
        public int UnidadMedidaId { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Simbolo { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Nombre { get; set; }
        
        [Column(TypeName = "varchar(max)")]
        public string Comentario { get; set; }


        public bool? Estado { get; set; }
    }
}
