
using FRIO.MAR.APPLICATION.CORE.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class Producto : Auditoria
    {
        [Key]
        public long ProductoId { get; set; }
        public TipoProducto TipoProducto { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Codigo { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Descripcion { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public decimal PrecioUnitario { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? IvaPorcentaje { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string IvaCodigo { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Marca { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Modelo { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string UnidadMedida { get; set; }

    }
}
