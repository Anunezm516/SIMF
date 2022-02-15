using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data
{
    public partial class SIFMContext
    {
        internal IEnumerable<UsuarioInternoQueryDto> ConsultaUsuarioInternoXCompania(long idCompania)
        {
            return UsuarioInternoQueryDto.FromSqlRaw("QRY_EDUsuarioInterno @p0", idCompania).ToList();
        }
    }
}
