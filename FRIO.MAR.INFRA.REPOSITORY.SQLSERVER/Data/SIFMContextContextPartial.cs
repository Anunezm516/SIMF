

using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data
{
    public partial class SIFMContext
    {
        private DbSet<VentanaLoginQueryDto> VentanaLoginInternoQueryDto { get; set; }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<VentanaLoginQueryDto>().HasNoKey().ToView(null);
        }

    }
}
