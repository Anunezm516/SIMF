using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Models
{
    public class ProductoModel
    {
        public string Id { get; set; }


        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(10, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string Codigo { get; set; }


        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(100, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(10, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string UnidadMedida { get; set; }


        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string IVA { get; set; }


        //[Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string CantidadStr { get; set; }
        public decimal Cantidad { get; set; }


        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string PrecioUnitarioStr { get; set; }
        public decimal PrecioUnitario { get; set; }


        //[Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public long? Bodega { get; set; }
        public long? Proveedor { get; set; }

        //[Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public long? Sucursal { get; set; }

        public long ProductoId { get; set; }



        //[Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public TipoInventario? TipoInventario { get; set; }

        public bool RegistrarInventario { get; set; }

        public string Ip { get; set; }
        public long Usuario { get; set; }

        public ProductoModel()
        {

        }

        public ProductoModel(Producto producto)
        {
            Id = Utilities.Crypto.CifrarId(producto.ProductoId);
            Codigo = producto.Codigo;
            Descripcion = producto.Descripcion;
            UnidadMedida = producto.UnidadMedida;
            PrecioUnitarioStr = APPLICATION.CORE.Utilities.Utilidades.DoubleToString_FrontCO(producto.PrecioUnitario, 2);
            IVA = producto.IvaCodigo + "|" + producto.IvaPorcentaje;
        } 
    }
}
