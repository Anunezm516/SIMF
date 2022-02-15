using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data
{
    public partial class SIFMContext
    {
        internal List<ParametroQueryDto> ConsultarParametroXCodigoXNit(string nitCompania, string Codigos)
        {
            return ParametroQueryDto.FromSqlRaw("QRY_ParametroXCodigoXNit @p0, @p1", nitCompania, Codigos).ToList();
        }
    }
}

