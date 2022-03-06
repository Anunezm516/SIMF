using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class CodigoSeguimiento
    {
        public long CodigoSeguimientoId { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Codigo { get; set; }
        public long ProductoClienteId { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
