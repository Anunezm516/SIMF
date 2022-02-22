
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
        public virtual DbSet<Bodega> Bodega { get; set; }
        public virtual DbSet<Sucursal> Sucursal { get; set; }
        public virtual DbSet<SucursalBodega> SucursalBodega { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        //public virtual DbSet<InventarioBogedas> InventarioBogedas { get; set; }
        public virtual DbSet<InventarioMovimientoEntrada> InventarioMovimientoEntrada { get; set; }
        public virtual DbSet<InventarioMovimientoSalida> InventarioMovimientoSalida { get; set; }
        public virtual DbSet<InventarioProveedor> InventarioProveedor { get; set; }
        public virtual DbSet<InventarioVenta> InventarioVenta { get; set; }
        //public virtual DbSet<InventarioBodegaSucursales> InventarioBodegaSucursales { get; set; }
        public virtual DbSet<InventarioConfiguracionesGenerales> InventarioConfiguracionesGenerales { get; set; }

        partial void OnModelCreatingPartialInventario(ModelBuilder modelBuilder) 
        {

            //Inventario
            //modelBuilder.Entity<InventarioBogedas>(entity =>
            //{
            //    entity.HasKey(e => e.IdInventarioBodega)
            //        .HasName("PK__Invent__054B749313D6F13E");

            //    entity.ToTable("InventarioBogedas");

            //    entity.Property(e => e.Codigo)
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

            //    entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");

            //    entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

            //    entity.Property(e => e.Ip)
            //        .HasColumnName("IP")
            //        .HasMaxLength(15)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Nombre)
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.HasOne(d => d.IdSucursalNavigation)
            //        .WithMany(p => p.InventarioBogedas)
            //        .HasForeignKey(d => d.IdSucursal)
            //        .HasConstraintName("FK__Inventa__IdSuc__0268428D");
            //});

            modelBuilder.Entity<InventarioMovimientoEntrada>(entity =>
            {
                entity.HasKey(e => e.IdInventarioMovimiento)
                    .HasName("PK__Invent__4D66004A55C73884");

                entity.ToTable("InventarioMovimientoEntrada");

                entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.CodigoTransferencia)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cufe)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Motivo)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroFactura)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(18, 6)");

                entity.HasOne(d => d.Bodega)
                    .WithMany(p => p.InventarioMovimientoEntrada)
                    .HasForeignKey(d => d.IdInventarioBodega)
                    .HasConstraintName("FK__Inventa__IdInv__0AFD888E");

                entity.HasOne(d => d.IdProductoNavigation)
                   .WithMany(p => p.InventarioMovimientoEntrada)
                   .HasForeignKey(d => d.IdProducto)
                   .HasConstraintName("FK__Inventa__IdPro__0A096455");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.InventarioMovimientoEntrada)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("FK__Inventa__IdSuc__0BF1ACC7");
            });

            modelBuilder.Entity<InventarioMovimientoSalida>(entity =>
            {
                entity.HasKey(e => e.IdInventarioMovimiento)
                    .HasName("PK__Invent__4D66004AE5E880D4");

                entity.ToTable("InventarioMovimientoSalida");

                entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.CodigoTransferencia)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cufe)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Motivo)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroFactura)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(18, 6)");

                entity.HasOne(d => d.Bodega)
                    .WithMany(p => p.InventarioMovimientoSalida)
                    .HasForeignKey(d => d.IdInventarioBodega)
                    .HasConstraintName("FK__Inventa__IdInv__0638D371");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.InventarioMovimientoSalida)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__Inventa__IdPro__0544AF38");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.InventarioMovimientoSalida)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("FK__Inventa__IdSuc__072CF7AA");

            });

            modelBuilder.Entity<InventarioProveedor>(entity =>
            {
                entity.HasKey(e => e.IdInventarioProveedor)
                    .HasName("PK__Invent__17576659E0B70AEB");

                entity.ToTable("InventarioProveedor");

                entity.Property(e => e.CantidadDescripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.StockActual).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bodega)
                    .WithMany(p => p.InventarioProveedor)
                    .HasForeignKey(d => d.IdInventarioBodega)
                    .HasConstraintName("FK__Inventa__IdInv__1392CE8F");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.InventarioProveedor)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__Inventa__IdPro__129EAA56");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.InventarioProveedor)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("FK__Inventa__IdSuc__22D5121F");
            });

            modelBuilder.Entity<InventarioVenta>(entity =>
            {
                entity.HasKey(e => e.IdInventarioVenta)
                    .HasName("PK__Invent__3FAD48D7775D9CA1");

                entity.ToTable("InventarioVenta");

                entity.Property(e => e.CantidadDescripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.StockActual).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bodega)
                    .WithMany(p => p.InventarioVenta)
                    .HasForeignKey(d => d.IdInventarioBodega)
                    .HasConstraintName("FK__Inventa__IdInv__0FC23DAB");

                entity.HasOne(d => d.IdProductoNavigation)
                 .WithMany(p => p.InventarioVenta)
                 .HasForeignKey(d => d.IdProducto)
                 .HasConstraintName("FK__Inventa__IdPro__0ECE1972");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.InventarioVenta)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("FK__Inventa__IdSuc__21E0EDE6");
            });

            //modelBuilder.Entity<InventarioBodegaSucursales>(entity =>
            //{
            //    entity.HasKey(e => e.IdInventarioBadegaSucursales)
            //        .HasName("PK__Invent__DDB1275A3FB5B349");

            //    entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

            //    entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");

            //    entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

            //    entity.HasOne(d => d.IdSucursalNavigation)
            //        .WithMany(p => p.InventarioBodegaSucursales)
            //        .HasForeignKey(d => d.IdSucursal)
            //        .HasConstraintName("FK__Inventa__IdSuc__1B33F057");

            //    entity.HasOne(d => d.Bodega)
            //        .WithMany(p => p.InventarioBodegaSucursales)
            //        .HasForeignKey(d => d.IdBodega)
            //        .HasConstraintName("FK__Inventa__IdBod__1E105D02");

            //});

            modelBuilder.Entity<InventarioConfiguracionesGenerales>(entity =>
            {
                entity.HasKey(e => e.IdInventarioConfiguracionesGenerales)
                    .HasName("PK__Invent__2D482365639E2D77");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            });

        }

    }
}
