using System;
using System.Collections.Generic;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public partial class InventarioConfiguracionesGenerales
    {
        public long IdInventarioConfiguracionesGenerales { get; set; }
        public bool DescontarStockAutomatico { get; set; } = true;
        public bool ControlInventarioSucursal { get; set; } = true;
        public bool ControlInventarioEmision { get; set; } = true;
        public DateTime FechaCreacion { get; set; }
    }
}
