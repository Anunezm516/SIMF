using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class Facturador : Auditoria
    {
        public long FacturadorId { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string RazonSocial { get; set; }


        [Column(TypeName = "varchar(20)")]
        public string Identificacion { get; set; }


        [Column(TypeName = "varchar(5)")]
        public string TipoIdentificacion { get; set; }
        

        [Column(TypeName = "varchar(50)")]
        public string Telefono { get; set; }
        
        
        [Column(TypeName = "varchar(100)")]
        public string CorreoElectronico { get; set; }
        
        
        [Column(TypeName = "varchar(500)")]
        public string Direccion { get; set; }


        [Column(TypeName = "varchar(5)")]
        public string Sucursal { get; set; }


        [Column(TypeName = "varchar(5)")]
        public string PuntoEmision { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string Logo { get; set; }

    }
}
