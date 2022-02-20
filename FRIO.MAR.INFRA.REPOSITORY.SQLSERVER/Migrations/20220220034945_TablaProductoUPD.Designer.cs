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
    [Migration("20220220034945_TablaProductoUPD")]
    partial class TablaProductoUPD
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
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

                    b.Property<long>("IdUsuario")
                        .HasColumnType("bigint");

                    b.Property<string>("Ip")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("SitioWeb")
                        .HasColumnType("varchar(25)")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("AccesoUsuario");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.Bodega", b =>
                {
                    b.Property<long>("BodegaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .HasColumnType("varchar(10)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(100)");

                    b.Property<long>("UsuarioCreacion")
                        .HasColumnType("bigint");

                    b.Property<long?>("UsuarioEliminacion")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioModificacion")
                        .HasColumnType("bigint");

                    b.HasKey("BodegaId");

                    b.ToTable("Bodega");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.Cliente", b =>
                {
                    b.Property<long>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CorreoElectronico")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Direccion")
                        .HasColumnType("varchar(300)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Identificacion")
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Ip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreComercial")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("RazonSocial")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Telefono")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TipoIdentificacion")
                        .HasColumnType("varchar(3)");

                    b.Property<long>("UsuarioCreacion")
                        .HasColumnType("bigint");

                    b.Property<long?>("UsuarioEliminacion")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioModificacion")
                        .HasColumnType("bigint");

                    b.HasKey("ClienteId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.Correo", b =>
                {
                    b.Property<long>("IdCorreo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Adjunto")
                        .HasColumnType("int");

                    b.Property<string>("Asunto")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("Copias")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("Correos")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaEnvio")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaIngreso")
                        .HasColumnType("datetime");

                    b.Property<int?>("IntentosEnvio")
                        .HasColumnType("int");

                    b.Property<string>("Mensaje")
                        .HasColumnType("varchar(5000)")
                        .HasMaxLength(5000)
                        .IsUnicode(false);

                    b.Property<int?>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("IdCorreo")
                        .HasName("PK__Correo__872F8EAEDC7FA84E");

                    b.ToTable("Correo");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.Impuesto", b =>
                {
                    b.Property<int>("ImpuestoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("Porcentaje")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<int>("TipoImpuestoId")
                        .HasColumnType("int");

                    b.HasKey("ImpuestoId");

                    b.HasIndex("TipoImpuestoId");

                    b.ToTable("Impuesto");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.Notificacion", b =>
                {
                    b.Property<long>("IdNotificacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("EsNotificacionLeida")
                        .HasColumnType("bit");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("FechaNotificacionLeida")
                        .HasColumnType("datetime");

                    b.Property<long>("IdUsuario")
                        .HasColumnType("bigint");

                    b.Property<string>("Mensaje")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<int>("TipoCriticidad")
                        .HasColumnType("int");

                    b.Property<int>("TipoNotificacion")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.HasKey("IdNotificacion")
                        .HasName("PK_EVNotificacion");

                    b.ToTable("Notificaciones");
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

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.Producto", b =>
                {
                    b.Property<long>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .HasColumnType("varchar(10)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<long>("UsuarioCreacion")
                        .HasColumnType("bigint");

                    b.Property<long?>("UsuarioEliminacion")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioModificacion")
                        .HasColumnType("bigint");

                    b.HasKey("ProductoId");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.Proveedor", b =>
                {
                    b.Property<long>("ProveedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CorreoElectronico")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Direccion")
                        .HasColumnType("varchar(300)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Identificacion")
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Ip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreComercial")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("RazonSocial")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Telefono")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TipoIdentificacion")
                        .HasColumnType("varchar(3)");

                    b.Property<long>("UsuarioCreacion")
                        .HasColumnType("bigint");

                    b.Property<long?>("UsuarioEliminacion")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioModificacion")
                        .HasColumnType("bigint");

                    b.HasKey("ProveedorId");

                    b.ToTable("Proveedores");
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

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.Sucursal", b =>
                {
                    b.Property<long>("SucursalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .HasColumnType("varchar(10)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(100)");

                    b.Property<long>("UsuarioCreacion")
                        .HasColumnType("bigint");

                    b.Property<long?>("UsuarioEliminacion")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioModificacion")
                        .HasColumnType("bigint");

                    b.HasKey("SucursalId");

                    b.ToTable("Sucursal");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.SucursalBodega", b =>
                {
                    b.Property<long>("SucursalBodegaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("BodegaId")
                        .HasColumnType("bigint");

                    b.Property<long>("SucursalId")
                        .HasColumnType("bigint");

                    b.HasKey("SucursalBodegaId");

                    b.HasIndex("BodegaId");

                    b.HasIndex("SucursalId");

                    b.ToTable("SucursalBodega");
                });

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.TipoImpuesto", b =>
                {
                    b.Property<int>("TipoImpuestoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .HasColumnType("varchar(4)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(50)");

                    b.HasKey("TipoImpuestoId");

                    b.ToTable("TipoImpuesto");
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

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.Impuesto", b =>
                {
                    b.HasOne("FRIO.MAR.APPLICATION.CORE.Entities.TipoImpuesto", "TipoImpuesto")
                        .WithMany("Impuestos")
                        .HasForeignKey("TipoImpuestoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("FRIO.MAR.APPLICATION.CORE.Entities.SucursalBodega", b =>
                {
                    b.HasOne("FRIO.MAR.APPLICATION.CORE.Entities.Bodega", "Bodega")
                        .WithMany("SucursalBodega")
                        .HasForeignKey("BodegaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FRIO.MAR.APPLICATION.CORE.Entities.Sucursal", "Sucursal")
                        .WithMany("SucursalBodega")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
