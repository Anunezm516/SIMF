using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.Services
{
    public interface IGemboxService
    {
        byte[] ConstruirFactura(Factura factura, Facturador facturador, string format, string RutaBase = "", string NombreArchivo = "", string ExtensionFormato = "xlsx");
    }
}
