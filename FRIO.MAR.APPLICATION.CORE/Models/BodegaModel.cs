using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Models
{
    public class BodegaModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUERIDO)]
        public string Codigo { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUERIDO)]
        public string Nombre { get; set; }

        public string Ip { get; set; }
        public long Usuario { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUERIDO)]
        public long[] Sucursales { get; set; }

        public BodegaModel()
        {

        }

        public BodegaModel(Bodega Bodega)
        {
            Id = Utilities.Crypto.CifrarId(Bodega.BodegaId);
            Nombre = Bodega.Nombre;
            Codigo = Bodega.Codigo;
            Sucursales = Bodega.SucursalBodega.Select(c => c.SucursalId).ToArray();
        }
    }
}
