using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Models
{
    public class ProductoClienteModel
    {
        public string Id { get; set; }
        public long ProductoClienteId { get; set; }
        public string ClienteId { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(10, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string Codigo { get; set; }


        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(100, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string Nombre { get; set; }


        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(1000, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(10, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string UnidadMedida { get; set; }

        [MaxLength(50, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string Marca { get; set; }

        [MaxLength(50, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string Modelo { get; set; }

        public List<IFormFile> Imagen { get; set; }
        public string ImagenBase64 { get; set; }
        public string ImagenRuta { get; set; }

        public string Ip { get; set; }
        public long Usuario { get; set; }

        public ProductoClienteModel()
        {
            ImagenBase64 = AppConstants.SinImagen;
        }

        public ProductoClienteModel(ProductoCliente producto)
        {
            Id = Utilities.Crypto.CifrarId(producto.ProductoClienteId);
            Codigo = producto.Codigo;
            Nombre = producto.Nombre;
            Descripcion = producto.Descripcion;
            UnidadMedida = producto.UnidadMedida;
            Marca = producto.Marca;
            Modelo = producto.Modelo;
            ImagenRuta = producto.ProductoClienteImagen?.FirstOrDefault()?.Ruta ?? "";

        } 
    }
}
