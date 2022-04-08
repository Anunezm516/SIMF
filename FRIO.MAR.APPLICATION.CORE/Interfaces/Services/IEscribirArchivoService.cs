using FRIO.MAR.APPLICATION.CORE.DTOs.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATIONCORE_GSEDOCPYME.Interfaces.General
{
    public interface IEscribirArchivoService
    {
        string EscribirZip(string pathDirectorio);
        EscribirZipResponseDTO ResponseEscribirZip(List<ArchivoResponseDTO> archivos, string rutaBase, string subdirectorio, string carpeta);
    }
}
