
using FRIO.MAR.APPLICATION.CORE.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class ProductoCliente : Auditoria
    {
        public ProductoCliente()
        {
            ProductoClienteImagen = new HashSet<ProductoClienteImagen>();
        }

        [Key]
        public long ProductoClienteId { get; set; }
        public TipoProducto TipoProducto { get; set; }

        [Column(TypeName = "varchar(20)")]
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

        public virtual ICollection<ProductoClienteImagen> ProductoClienteImagen { get; set; }
    }
}
