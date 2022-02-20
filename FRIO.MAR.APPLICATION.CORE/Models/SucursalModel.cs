using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Models
{
    public class SucursalModel
    {
        public string Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Ip { get; set; }
        public long Usuario { get; set; }

        public SucursalModel()
        {

        }

        public SucursalModel(Sucursal Sucursal)
        {
            Id = Utilities.Crypto.CifrarId(Sucursal.SucursalId);
            Nombre = Sucursal.Nombre;
            Codigo = Sucursal.Codigo;
        }
    }
}
