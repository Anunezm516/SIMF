using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Models
{
    public class ModalProductoModel
    {
        public long ProductoId { get; set; }
        public string Descripcion { get; set; }
        public string UnidadMedida { get; set; }
        public string IVA { get; set; }
        public string PrecioUnitario { get; set; }
        public string Cantidad { get; set; }
    }
}
