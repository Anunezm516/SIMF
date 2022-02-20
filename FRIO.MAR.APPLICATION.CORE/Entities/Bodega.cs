using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class Bodega : Auditoria
    {
        public Bodega()
        {
            SucursalBodega = new HashSet<SucursalBodega>();
        }

        [Key]
        public long BodegaId { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Nombre { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Codigo { get; set; }

        public virtual ICollection<SucursalBodega> SucursalBodega { get; set; }

    }
}
