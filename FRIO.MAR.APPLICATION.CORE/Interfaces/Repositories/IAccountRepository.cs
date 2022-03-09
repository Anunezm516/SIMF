using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using FRIO.MAR.APPLICATION.CORE.Entities;
using System.Collections.Generic;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IAccountRepository : IRepository<Usuario>
    {
        bool RegistrarAccesoUsuario(AccesoUsuario acceso);
        List<VentanaLoginQueryDto> ConsultarVentana(long IdUsuario, ref string mensaje);
        Facturador GetFacturador();
        bool UpdateFacturador(Facturador facturador);
    }
}
