using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Models
{
    public class BodegaModel
    {
        public string Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Ip { get; set; }
        public long Usuario { get; set; }

        public BodegaModel()
        {

        }

        public BodegaModel(Bodega Bodega)
        {
            Id = Utilities.Crypto.CifrarId(Bodega.BodegaId);
            Nombre = Bodega.Nombre;
            Codigo = Bodega.Codigo;
        }
    }
}
