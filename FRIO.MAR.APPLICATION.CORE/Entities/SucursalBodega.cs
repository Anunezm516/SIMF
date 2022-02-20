using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class SucursalBodega
    {
        [Key]
        public long SucursalBodegaId { get; set; }

        [ForeignKey("Sucursal")]
        public long SucursalId { get; set; }

        [ForeignKey("Bodega")]
        public long BodegaId { get; set; }

        public virtual Sucursal Sucursal { get; set; }
        public virtual Bodega Bodega { get; set; }
    }
}
