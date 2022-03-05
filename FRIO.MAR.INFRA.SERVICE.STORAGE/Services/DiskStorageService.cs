
using FRIO.MAR.APPLICATION.CORE.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FRIO.MAR.INFRA.SERVICE.STORAGE.Services
{
    public class DiskStorageService : IStorageService
    {
        public bool GuardarArchivo(MemoryStream ms, string RutaArchivo, ref string mensaje, string containerName = null)
        {
            try
            {
                var RutaDirectorio = RutaArchivo.Replace(Path.GetFileName(RutaArchivo), null);
                if (!GenerarDirectorio(RutaDirectorio, ref mensaje, containerName)) return false;
                ms.Position = 0;
                using (FileStream file = new FileStream(RutaArchivo, FileMode.Create, FileAccess.Write))
                    ms.WriteTo(file);
                return true;
            }
            catch (Exception ex)
            {
                mensaje = "GuardarArchivo (Disk) => " + ex.Message;
                return false;
            }
        }

        public bool EliminarArchivo(string RutaArchivo, ref string mensaje, string containerName = null)
        {
            try
            {
                FileInfo archivo = new FileInfo(RutaArchivo);
                if (archivo.Exists)
                    archivo.Delete();
                return true;
            }
            catch (Exception ex)
            {
                mensaje = "EliminarArchivo (Disk) => " + ex.Message;
                return false;
            }
        }

        public bool ValidarRutaArchivo(string RutaArchivo, ref string mensaje, string containerName = null)
        {
            try
            {
                if ((new FileInfo(RutaArchivo)).Exists)
                    return true;
                mensaje = "Archivo no encontrado";
                return false;
            }
            catch (Exception ex)
            {
                mensaje = "ValidarRutaArchivo (Disk) => " + ex.Message;
                return false;
            }
        }

        public bool GenerarDirectorio(string RutaDirectorio, ref string mensaje, string containerName = null)
        {
            try
            {
                if (!Directory.Exists(RutaDirectorio))
                    Directory.CreateDirectory(RutaDirectorio);
                return true;
            }
            catch (Exception ex)
            {
                mensaje = "GenerarDirectorio (Disk) => " + ex.Message;
                return false;
            }
        }

        //public bool GenerarRecovery<T>(ref T Obj, bool DBResult, string DirectorioRecovery, string NombreArchivo, ref string XmlRecovery, ref string mensaje, string containerName = null)
        //{
        //    try
        //    {
        //        var RutaArchivoRecovery = Obj.GetNestedPropertyValue("RutaArchivoRecovery")?.ToString();
        //        var GenerarArchivoRecovery = Convert.ToBoolean(Obj.GetNestedPropertyValue("GenerarArchivoRecovery"));
        //        if (DBResult)
        //        {
        //            if (RutaArchivoRecovery != null)
        //                EliminarArchivo(RutaArchivoRecovery, ref mensaje, containerName);
        //            return true;
        //        }
        //        if (!GenerarArchivoRecovery) return true;
        //        if (ValidarRutaArchivo(RutaArchivoRecovery, ref mensaje, containerName)) throw new Exception(mensaje);
        //        RutaArchivoRecovery = IOUtilities.GenerarRutaXmlRecovery(NombreArchivo, DirectorioRecovery, ref mensaje);
        //        if (RutaArchivoRecovery == null) throw new Exception(mensaje);
        //        Obj.SetNestedPropertyValue("RutaArchivoRecovery", RutaArchivoRecovery);
        //        var xml = new XmlDocument();
        //        var cadxml = GSConversions.SerializeXml(Obj);
        //        xml.LoadXml(cadxml);
        //        XmlRecovery = xml.OuterXml;
        //        using (var ms = new MemoryStream())
        //        {
        //            xml.Save(ms);
        //            ms.Flush(); ms.Position = 0;
        //            if (!GuardarArchivo(ms, RutaArchivoRecovery, ref mensaje, containerName))
        //                throw new Exception(mensaje);
        //        };
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        mensaje = ex.Message;
        //        return false;
        //    }
        //}

        public MemoryStream ObtenerArchivo(string RutaArchivo, ref string mensaje, string containerName = null)
        {
            try
            {
                var byteFile = File.ReadAllBytes(RutaArchivo);
                return new MemoryStream(byteFile);
            }
            catch (Exception ex)
            {
                mensaje = "ObtenerArchivo (Disk) => " + ex.Message;
                return null;
            }
        }

        //public List<T> ObtenerRecovery<T>(string DirectorioRecovery, string PrefijoArchivo, ref string mensaje, string containerName = null)
        //{
        //    try
        //    {
        //        var dir = new DirectoryInfo(DirectorioRecovery);
        //        PrefijoArchivo = string.Format("Recovery_{0}*.xml", PrefijoArchivo);
        //        var files = dir.GetFiles(PrefijoArchivo);
        //        if (files == null) throw new Exception("Archivo no encontrado");
        //        var lst = new List<T>();
        //        foreach (var file in files)
        //        {
        //            var msFile = ObtenerArchivo(file.FullName, ref mensaje, containerName);
        //            if (msFile == null) throw new Exception(mensaje);
        //            var obj = GSConversions.DeserializeXmlObject<T>(msFile);
        //            lst.Add(obj);
        //        }
        //        return lst;
        //    }
        //    catch (Exception ex)
        //    {
        //        mensaje = "ObtenerArchivo (Disk) => " + ex.Message;
        //        return null;
        //    }
        //}
    }
}
