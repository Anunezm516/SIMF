
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
        public virtual DbSet<Parametros> Parametros { get; set; }
        public virtual DbSet<Notificacion> Notificaciones { get; set; }
        public virtual DbSet<Correo> Correos { get; set; }
        public virtual DbSet<Impuesto> Impuesto { get; set; }
        public virtual DbSet<TipoImpuesto> TipoImpuesto { get; set; }
        public virtual DbSet<TipoIdentificacion> TipoIdentificacion { get; set; }
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }
        public virtual DbSet<Facturador> Facturador { get; set; }

        partial void OnModelCreatingPartialVenta(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Impuesto>(entity =>
            {
                entity.Property(e => e.Porcentaje).HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<Correo>(entity =>
            {
                entity.HasKey(e => e.IdCorreo)
                    .HasName("PK__Correo__872F8EAEDC7FA84E");

                entity.ToTable("Correo");

                entity.Property(e => e.Asunto)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Copias)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Correos)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FechaEnvio).HasColumnType("datetime");

                entity.Property(e => e.FechaIngreso).HasColumnType("datetime");

                entity.Property(e => e.Mensaje)
                    .HasMaxLength(5000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Notificacion>(entity =>
            {
                entity.HasKey(e => e.IdNotificacion)
                    .HasName("PK_EVNotificacion");

                entity.ToTable("Notificaciones");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaNotificacionLeida).HasColumnType("datetime");

                entity.Property(e => e.Mensaje)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Parametros>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("Parametros");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.UsuarioCreacion)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioEliminacion)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioModificacion)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Valor)
                    .IsRequired()
                    .IsUnicode(false);
            });

        }

    }
}
