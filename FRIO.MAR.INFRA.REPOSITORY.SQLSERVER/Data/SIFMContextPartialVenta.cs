
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
        public virtual DbSet<Factura> Factura { get; set; }


        partial void OnModelCreatingPartialUtilidad(ModelBuilder modelBuilder) 
        {


        }

    }
}
