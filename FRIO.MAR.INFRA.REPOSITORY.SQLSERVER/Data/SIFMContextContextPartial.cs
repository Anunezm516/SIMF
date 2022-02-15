

using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data
{
    public partial class SIFMContext
    {
        private DbSet<ConsultarUsuarioQueryDto> ConsultarUsuarioQueryDto { get; set; }
        private DbSet<LoginInternoQueryDto> LoginInternoQueryDto { get; set; }
        private DbSet<VentanaLoginQueryDto> VentanaLoginInternoQueryDto { get; set; }
        private DbSet<UsuarioInternoQueryDto> UsuarioInternoQueryDto { get; set; }
        private DbSet<UsuariosQueryDto> UsuariosQueryDto { get; set; }
        private DbSet<RolesQueryDto> RolesQueryDto { get; set; }
        private DbSet<IdQueryDto> IdQueryDto { get; set; }
        private DbSet<CredencialQueryDto> CredencialQueryDto { get; set; }
        private DbSet<ParametroQueryDto> ParametroQueryDto { get; set; }
        private DbSet<PermisosQueryDto> PermisosQueryDto { get; set; }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<ConsultarUsuarioQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<UsuariosQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<LoginInternoQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<VentanaLoginQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<UsuarioInternoQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<RolesQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<IdQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<CredencialQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<ParametroQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<PermisosQueryDto>().HasNoKey().ToView(null);
        }

    }
}
