
using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data
{
    public partial class SIFMContext
    {
        internal CredencialQueryDto ConsultarCredencialXNit(string NitCompania)
        {
            return CredencialQueryDto.FromSqlRaw("QRY_CredencialXNit @p0", NitCompania).ToList().FirstOrDefault();
        }
    }
}
