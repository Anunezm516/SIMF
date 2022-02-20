using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Models
{
    public class ProveedorModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string TipoIdentificacion { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(25, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(100, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string RazonSocial { get; set; }

        [MaxLength(100, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string NombreComercial { get; set; }

        [MaxLength(300, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string Direccion { get; set; }

        [MaxLength(100, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string CorreoElectronico { get; set; }

        [MaxLength(50, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string Telefono { get; set; }

        public string Ip { get; set; }
        public long Usuario { get; set; }

        public ProveedorModel()
        {

        }

        public ProveedorModel(Proveedor cliente)
        {
            Id = Utilities.Crypto.CifrarId(cliente.ProveedorId);
            TipoIdentificacion = cliente.TipoIdentificacion;
            Identificacion = cliente.Identificacion;
            RazonSocial = cliente.RazonSocial;
            NombreComercial = cliente.NombreComercial;
            CorreoElectronico = cliente.CorreoElectronico;
            Direccion = cliente.Direccion;
            Telefono = cliente.Telefono;
        }
    }
}

