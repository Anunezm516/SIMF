using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.Services
{
    public interface IStorageService
    {
        bool GuardarArchivo(MemoryStream ms, string RutaArchivo, ref string mensaje, string containerName = null);
        bool EliminarArchivo(string RutaArchivo, ref string mensaje, string containerName = null);
        bool ValidarRutaArchivo(string RutaArchivo, ref string mensaje, string containerName = null);
        MemoryStream ObtenerArchivo(string RutaArchivo, ref string mensaje, string containerName = null);
        bool GenerarDirectorio(string RutaDirectorio, ref string mensaje, string containerName = null);
    }
}
