
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

        [Column(TypeName = "varchar(20)")]
        public string Codigo { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Nombre { get; set; }

        [Column(TypeName = "varchar(1000)")]
        public string Descripcion { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Marca { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Modelo { get; set; }

        //[Column(TypeName = "varchar(50)")]
        //public string UnidadMedida { get; set; }

        public virtual long ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<ProductoClienteImagen> ProductoClienteImagen { get; set; }
    }
}
