
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.DTOs.Services;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Services;
using FRIO.MAR.INFRA.SERVICE.STORAGE.Utilities;
using Microsoft.AspNetCore.Http;
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

        public List<ArchivoServiceDto> GuardarImagenes(List<IFormFile> imagenes, string Tipo)
        {
            string RutaImagen = "";
            string mensaje = "";
            string path = Path.Combine(GlobalSettings.DirectorioImagenes, Tipo);
            List<ArchivoServiceDto> rutas = new List<ArchivoServiceDto>();

            foreach (var item in imagenes)
            {
                using MemoryStream ms = new MemoryStream();
                item.CopyTo(ms);

                RutaImagen = Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(item.FileName));

                if (GlobalSettings.TipoAlmacenamiento == "1")
                {
                    if (ms != null)
                    {
                        if (GuardarArchivo(ms, Path.Combine("wwwroot", RutaImagen), ref mensaje))
                        {
                            rutas.Add(new ArchivoServiceDto
                            {
                                Base64 = Convert.ToBase64String(ms.ToArray()),
                                Nombre = item.FileName,
                                Ruta = RutaImagen
                            });
                        }
                    }
                }
                else
                {
                    if (ms != null)
                    {
                        if (GuardarArchivo(ms, RutaImagen, ref mensaje, "images"))
                        {
                            rutas.Add(new ArchivoServiceDto
                            {
                                Base64 = Convert.ToBase64String(ms.ToArray()),
                                Nombre = item.FileName,
                                Ruta = RutaImagen
                            });
                        }
                    }
                }
            }

            return rutas;
        }

        public string ObtenerImagenBase64(string TipoAlmacenamiento, string Imagen, string FileName)
        {
            string mensaje = "";
            MemoryStream ms = null;
            if (TipoAlmacenamiento == "1")
            {
                ms = ObtenerArchivo(Path.Combine("wwwroot", Imagen), ref mensaje, "images");
            }
            else
            {
                ms = ObtenerArchivo(Imagen, ref mensaje, "images");
            }

            return ms == null ? "" : Convert.ToBase64String(ms.ToArray());

        }
    }
}
