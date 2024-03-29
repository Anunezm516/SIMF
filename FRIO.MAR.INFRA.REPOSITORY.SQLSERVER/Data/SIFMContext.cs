﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data
{
    public partial class SIFMContext : DbContext
    {
        public string DefaultConecctionString = string.Empty;

        public SIFMContext() : base()
        {
        }

        public SIFMContext(string connectionString)
              : base()
        {
            this.DefaultConecctionString = connectionString;
        }

        public SIFMContext(DbContextOptions<SIFMContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //if (this.DefaultConecctionString == null)
                //{

                //}
                optionsBuilder.UseSqlServer(DefaultConecctionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
            OnModelCreatingPartialSeguridad(modelBuilder);
            OnModelCreatingPartialVenta(modelBuilder);
            OnModelCreatingPartialUtilidad(modelBuilder);
            OnModelCreatingPartialCompra(modelBuilder);
            OnModelCreatingPartialInventario(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        partial void OnModelCreatingPartialSeguridad(ModelBuilder modelBuilder);
        partial void OnModelCreatingPartialVenta(ModelBuilder modelBuilder);
        partial void OnModelCreatingPartialUtilidad(ModelBuilder modelBuilder);
        partial void OnModelCreatingPartialCompra(ModelBuilder modelBuilder);
        partial void OnModelCreatingPartialInventario(ModelBuilder modelBuilder);

    }
}
