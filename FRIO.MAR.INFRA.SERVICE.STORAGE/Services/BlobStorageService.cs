
using FRIO.MAR.APPLICATION.CORE.Interfaces.Services;
using FRIO.MAR.INFRA.SERVICE.STORAGE.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FRIO.MAR.INFRA.SERVICE.STORAGE.Services
{
    public class BlobStorageService : IStorageService
    {
        private readonly BlobIOUtilities blobIOUtilities;

        public BlobStorageService(string connectionString)
        {
            blobIOUtilities = new BlobIOUtilities(connectionString);
        }

        public bool EliminarArchivo(string RutaArchivo, ref string mensaje, string containerName = null)
        {
            try
            {
                var (IsSucceed, Message) = blobIOUtilities.DeleteFileAsync(RutaArchivo, containerName).Result;

                if (!IsSucceed)
                    mensaje = Message;
                else
                    return IsSucceed;
            }
            catch (Exception ex)
            {
                mensaje = "EliminarArchivo (Blob) => " + ex.Message;
            }

            return false;
        }

        public bool GuardarArchivo(MemoryStream ms, string RutaArchivo, ref string mensaje, string containerName = null)
        {
            try
            {
                var (IsSucceed, Message) = blobIOUtilities.UpLoadFileAsync(RutaArchivo, ms, containerName).Result;

                if (!IsSucceed)
                    mensaje = Message;
                else
                    return IsSucceed;
            }
            catch (Exception ex)
            {
                mensaje = "GuardarArchivo (Blob) => " + ex.Message;
            }

            return false;
        }

        public MemoryStream ObtenerArchivo(string RutaArchivo, ref string mensaje, string containerName = null)
        {
            try
            {
                var (Stream, Message) = blobIOUtilities.DownLoadFileAsync(RutaArchivo, containerName).Result;
                {
                    if (Stream == null)
                        mensaje = Message;
                    else
                    {
                        var ms = new MemoryStream();
                        Stream.CopyTo(ms);
                        return ms;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "ObtenerArchivo (Blob) => " + ex.Message;
            }

            return null;
        }

        public bool ValidarRutaArchivo(string RutaArchivo, ref string mensaje, string containerName = null)
        {
            try
            {
                var (IsSucceed, Message) = blobIOUtilities.FileExistsAsync(RutaArchivo, containerName).Result;

                if (!IsSucceed)
                    mensaje = Message;
                else
                    return IsSucceed;
            }
            catch (Exception ex)
            {
                mensaje = "ValidarRutaArchivo (Blob) => " + ex.Message;
            }

            return false;
        }

        public bool GenerarDirectorio(string RutaDirectorio, ref string mensaje, string containerName = null)
        {
            throw new NotImplementedException();
        }

        public bool GenerarRecovery<T>(ref T Obj, bool DBResult, string DirectorioRecovery, string NombreArchivo, ref string XmlRecovery, ref string mensaje, string containerName = null)
        {
            throw new NotImplementedException();
        }

        public List<T> ObtenerRecovery<T>(string DirectorioRecovery, string PrefijoArchivo, ref string mensaje, string containerName = null)
        {
            throw new NotImplementedException();
        }
    }
}
