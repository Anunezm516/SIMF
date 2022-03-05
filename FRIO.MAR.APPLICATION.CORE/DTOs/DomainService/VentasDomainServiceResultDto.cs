﻿using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.DTOs.DomainService
{
    public class VentasDomainServiceResultDto
    {
        public string Id { get; set; }
        public string Fecha { get; set; }
        public string Identificacion { get; set; }
        public string Adquiriente { get; set; }
        public string ValorTotal { get; set; }
        public EstadoFactura Estado { get; set; }

        public VentasDomainServiceResultDto()
        {

        }

        public VentasDomainServiceResultDto(Factura factura)
        {
            Id = Crypto.CifrarId(factura.FacturaId);
            Fecha = factura.FechaModificacion.ToString("yyyy-MM-dd HH:mm:ss");
            Identificacion = factura.Identificacion;
            Adquiriente = factura.RazonSocial;
            ValorTotal = Utilidades.DoubleToString_FrontCO(factura.ValorTotal, 2);
            Estado = factura.Estado;

        }
    }
}
