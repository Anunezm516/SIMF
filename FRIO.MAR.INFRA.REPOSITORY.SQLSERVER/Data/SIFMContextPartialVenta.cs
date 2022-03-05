﻿
using FRIO.MAR.APPLICATION.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data
{
    public partial class SIFMContext
    {
        public virtual DbSet<Cliente> Clientes { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalle { get; set; }
        public DbSet<FacturaFormaPago> FacturaFormaPago { get; set; }


        partial void OnModelCreatingPartialUtilidad(ModelBuilder modelBuilder) 
        {


        }

    }
}
