﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.DTOs.AppServices.ApexCharts
{
    public class GraficoMes
    {
        public string NombreCorto { get; set; }
        public string NombreCompleto { get; set; }
        public int NumeroMes { get; set; }
        public decimal? ValorTotal { get; set; }
        public decimal? ValorTotalDecimal { get; set; }
    }
}
