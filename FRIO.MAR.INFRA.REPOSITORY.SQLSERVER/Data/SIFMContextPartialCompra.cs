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
        public virtual DbSet<Proveedor> Proveedores { get; set; }


        partial void OnModelCreatingPartialCompra(ModelBuilder modelBuilder) 
        {


        }

    }
}
