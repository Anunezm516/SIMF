﻿// <auto-generated />
using System;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    [DbContext(typeof(SIFMContext))]
    [Migration("20220214035935_TablaSeguridad")]
    partial class TablaSeguridad
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.AccesoUsuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime");

                    b.Property<long?>("IdCompania")
                        .HasColumnType("bigint");

                    b.Property<long>("IdUsuario")
                        .HasColumnType("bigint");

                    b.Property<string>("Ip")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("SitioWeb")
                        .HasColumnType("varchar(5)")
                        .HasMaxLength(5)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("AccesoUsuario");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.Parametros", b =>
                {
                    b.Property<string>("Codigo")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime");

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired()
                        .HasColumnType("varchar(25)")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<string>("UsuarioEliminacion")
                        .HasColumnType("varchar(25)")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<string>("UsuarioModificacion")
                        .HasColumnType("varchar(25)")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<string>("Valor")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.HasKey("Codigo");

                    b.ToTable("Parametros");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.Permisos", b =>
                {
                    b.Property<long>("IdPermiso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("Codigo")
                        .HasColumnType("bigint");

                    b.Property<string>("Descripcion")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int?>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime");

                    b.Property<string>("Icono")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<long?>("IdPadre")
                        .HasColumnType("bigint");

                    b.Property<string>("Ip")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("NombreAbreviado")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Url")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<long?>("UsuarioCreacion")
                        .HasColumnType("bigint");

                    b.Property<long?>("UsuarioEliminacion")
                        .HasColumnType("bigint");

                    b.Property<long?>("UsuarioModificacion")
                        .HasColumnType("bigint");

                    b.HasKey("IdPermiso")
                        .HasName("PK__SPPermis__0D626EC845308855");

                    b.ToTable("Permisos");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.Rol", b =>
                {
                    b.Property<long>("IdRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime");

                    b.Property<string>("Ip")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<long?>("UsuarioCreacion")
                        .HasColumnType("bigint");

                    b.Property<long?>("UsuarioEliminacion")
                        .HasColumnType("bigint");

                    b.Property<long?>("UsuarioModificacion")
                        .HasColumnType("bigint");

                    b.HasKey("IdRol")
                        .HasName("PK__Rol__2A49584CB35FDE7B");

                    b.ToTable("Rol");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.RolPermiso", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime");

                    b.Property<long?>("IdPermiso")
                        .HasColumnType("bigint");

                    b.Property<long?>("IdRol")
                        .HasColumnType("bigint");

                    b.Property<long?>("UsuarioCreacion")
                        .HasColumnType("bigint");

                    b.Property<long?>("UsuarioEliminacion")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("IdPermiso");

                    b.HasIndex("IdRol");

                    b.ToTable("RolPermiso");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.Usuario", b =>
                {
                    b.Property<long>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<bool?>("Bloqueado")
                        .HasColumnType("bit");

                    b.Property<string>("CorreoElectronico")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int?>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaActualizarPassword")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaUltimaConexion")
                        .HasColumnType("datetime");

                    b.Property<string>("IdTelegram")
                        .HasColumnType("varchar(25)")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<int?>("IntentosFallidos")
                        .HasColumnType("int");

                    b.Property<string>("Ip")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Password")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<long?>("UsuarioCreacion")
                        .HasColumnType("bigint");

                    b.Property<long?>("UsuarioEliminacion")
                        .HasColumnType("bigint");

                    b.Property<long?>("UsuarioModificacion")
                        .HasColumnType("bigint");

                    b.HasKey("IdUsuario")
                        .HasName("PK__SPUsuari__5B65BF97D476A28D");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.UsuarioRol", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit");

                    b.Property<long?>("IdRol")
                        .HasColumnType("bigint");

                    b.Property<long?>("IdUsuario")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("IdRol");

                    b.HasIndex("IdUsuario");

                    b.ToTable("UsuarioRol");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.RolPermiso", b =>
                {
                    b.HasOne("FRIO.MAR.APPLICATION.CORE.Entities.Permisos", "IdPermisoNavigation")
                        .WithMany("RolPermiso")
                        .HasForeignKey("IdPermiso")
                        .HasConstraintName("FK__RolPerm__IdPer__3F466844");

                    b.HasOne("FRIO.MAR.APPLICATION.CORE.Entities.Rol", "IdRolNavigation")
                        .WithMany("RolPermiso")
                        .HasForeignKey("IdRol")
                        .HasConstraintName("FK__RolPerm__IdRol__3E52440B");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.UsuarioRol", b =>
                {
                    b.HasOne("FRIO.MAR.APPLICATION.CORE.Entities.Rol", "IdRolNavigation")
                        .WithMany("UsuarioRol")
                        .HasForeignKey("IdRol")
                        .HasConstraintName("FK__Usuario__IdRol__3C69FB99");

                    b.HasOne("FRIO.MAR.APPLICATION.CORE.Entities.Usuario", "IdUsuarioNavigation")
                        .WithMany("UsuarioRol")
                        .HasForeignKey("IdUsuario")
                        .HasConstraintName("FK__Usuario__IdUsu__3B75D760");
                });
#pragma warning restore 612, 618
        }
    }
}
