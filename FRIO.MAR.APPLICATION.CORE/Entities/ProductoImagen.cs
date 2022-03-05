﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class ProductoImagen
    {
        [Key]
        public long ProductoImagenId { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Nombre { get; set; }


        [Column(TypeName = "varchar(max)")]
        public string Ruta { get; set; }
        
        
        [Column(TypeName = "varchar(max)")]
        public string ImagenBase64 { get; set; }
        
        public bool Estado { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
