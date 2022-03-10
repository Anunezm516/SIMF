using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Services;
using FRIO.MAR.APPLICATION.CORE.Utilities;
using GemBox.Spreadsheet;
using System;
using System.IO;
using System.Linq;

namespace FRIO.MAR.INFRA.SERVICE.GEMBOX.Services
{
    public class GemboxService : IGemboxService
    {
        public readonly string FormatoFechaHora = "dd/MM/yyyy HH:mm:ss";

        public GemboxService()
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public byte[] ConstruirFactura(Factura factura, Facturador facturador, string format, string RutaBase = "", string NombreArchivo = "", string ExtensionFormato = "xlsx")
        {
            try
            {
                string path = Path.Combine(RutaBase, NombreArchivo);
                LoadOptions loadOptions = GetLoadOptions((ExtensionFormato));
                ExcelFile workbook = ExcelFile.Load(path, loadOptions);

                int Row = 0; int Col = 0;
                ExcelWorksheet ws = workbook.Worksheets.FirstOrDefault(x => x.Name == "Factura");

                InsertarValorCelda(ws, "{Emisor_Razon_Social}", facturador.RazonSocial, ref Row, ref Col);
                InsertarValorCelda(ws, "{Emisor_Direccion}", facturador.Direccion, ref Row, ref Col);
                InsertarValorCelda(ws, "{Emisor_Telefono}", facturador.Telefono, ref Row, ref Col);
                InsertarValorCelda(ws, "{Emisor_Telefono}", facturador.Telefono, ref Row, ref Col);
                InsertarValorCelda(ws, "{Emisor_Telefono}", facturador.Telefono, ref Row, ref Col);
                InsertarValorCelda(ws, "R.U.C. {Emisor_Ruc}", facturador.Identificacion, ref Row, ref Col);

                InsertarValorCelda(ws, "{Fecha_Emision}", factura.FechaEmision.HasValue ? factura.FechaEmision.Value.ToString(FormatoFechaHora) : "", ref Row, ref Col);//Fecha de emision
                InsertarValorCelda(ws, "{Cliente_Razon_Social}", factura.RazonSocial, ref Row, ref Col);
                InsertarValorCelda(ws, "{Cliente_Telefono}", factura.Telefono, ref Row, ref Col);
                InsertarValorCelda(ws, "{Cliente_Direccion}", factura.Direccion, ref Row, ref Col);
                
                InsertarValorCelda(ws, "Serie {Sucursal}-{Punto_Emision}-{Secuencial}", "Serie 001-001-0000000001", ref Row, ref Col);
                //InsertarValorCelda(ws, "", factura.RazonSocial, ref Row, ref Col);
                //InsertarValorCelda(ws, "", factura.RazonSocial, ref Row, ref Col);
                


                if (InsertarValorCelda(ws, "{Detalle_Item}", "", ref Row, ref Col))
                {
                    int cantidadLineas = factura.FacturaDetalle.Count;
                    ws.Rows.InsertCopy(Row, cantidadLineas, ws.Rows[Row]);
                    int cont = 0;
                    foreach (var line in factura.FacturaDetalle)
                    {
                        cont++;
                        ////Nro.
                        //ws.Cells[Row, 0].Value = cont.ToString();
                        //ws.Cells[Row, 0].Style.WrapText = true;

                        //Descripcion
                        ws.Cells[Row, 0].Value = Utilidades.DoubleToString_FrontCO(line.Cantidad, 2);
                        ws.Cells[Row, 0].Style.WrapText = true;

                        //Codigo
                        ws.Cells[Row, 1].Value = line.Descripcion;
                        ws.Cells[Row, 1].Style.WrapText = true;

                        //U/M
                        ws.Cells[Row, 3].Value = Utilidades.DoubleToString_FrontCO(line.PrecioUnitario, 2);
                        ws.Cells[Row, 3].Style.WrapText = true;

                        //Cantidad;
                        ws.Cells[Row, 4].Value = Utilidades.DoubleToString_FrontCO(line.Total, 2);
                        ws.Cells[Row, 4].Style.WrapText = true;

                        Row++;
                    }
                }

                if (InsertarValorCelda(ws, "{Detalle_Pago}", "", ref Row, ref Col))
                {
                    int cantidadLineas = factura.FacturaFormaPago.Count;
                    ws.Rows.InsertCopy(Row, cantidadLineas, ws.Rows[Row]);
                    int cont = 0;
                    foreach (var line in factura.FacturaFormaPago)
                    {
                        cont++;

                        ws.Cells[Row, 0].Value = line.DescripcionFormaPago;
                        ws.Cells[Row, 0].Style.WrapText = true;

                        //Codigo
                        ws.Cells[Row, 1].Value = Utilidades.DoubleToString_FrontCO(line.Valor, 2);
                        ws.Cells[Row, 1].Style.WrapText = true;

                        Row++;
                    }
                }


                InsertarValorCelda(ws, "{Subtotal}", Utilidades.DoubleToString_FrontCO(factura.FacturaDetalle.Sum(x => (x.Cantidad * x.PrecioUnitario)), 2), ref Row, ref Col);
                InsertarValorCelda(ws, "{TotalIva}", Utilidades.DoubleToString_FrontCO(factura.FacturaDetalle.Sum(x => (x.IvaValor)), 2), ref Row, ref Col);
                InsertarValorCelda(ws, "{Total}", Utilidades.DoubleToString_FrontCO(factura.ValorTotal, 2), ref Row, ref Col);

                SaveOptions options = GetSaveOptions(format);

                using (var stream = new MemoryStream())
                {
                    workbook.Save(stream, options);
                    return stream.ToArray();
                }


            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        private bool InsertarValorCelda(ExcelWorksheet ws, string clave, object valor, ref int Row, ref int Col)
        {
            try
            {
                Row = 0; Col = 0;
                ws.Cells.FindText(clave, true, true, out Row, out Col);
                if (Row >= 0 && Col >= 0)
                {
                    ws.Cells[Row, Col].Value = valor;
                    ws.Cells[Row, Col].Style.WrapText = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static SaveOptions GetSaveOptions(string format)
        {
            switch (format.ToUpper())
            {
                case "XLSX":
                    return SaveOptions.XlsxDefault;
                case "XLS":
                    return SaveOptions.XlsDefault;
                case "ODS":
                    return SaveOptions.OdsDefault;
                case "CSV":
                    return SaveOptions.CsvDefault;
                case "HTML":
                    return SaveOptions.HtmlDefault;
                case "PDF":
                    return SaveOptions.PdfDefault;

                case "XPS":
                case "PNG":
                case "JPG":
                case "GIF":
                case "TIF":
                case "BMP":
                case "WMP":
                    throw new InvalidOperationException("To enable saving to XPS or image format, add 'Microsoft.WindowsDesktop.App' framework reference.");

                default:
                    throw new NotSupportedException();
            }
        }

        private static LoadOptions GetLoadOptions(string format)
        {
            switch (format.ToUpper())
            {
                case "XLSX":
                    return LoadOptions.XlsxDefault;
                case "XLS":
                    return LoadOptions.XlsDefault;
                case "ODS":
                    return LoadOptions.OdsDefault;
                case "CSV":
                    return LoadOptions.CsvDefault;
                case "HTML":
                    return LoadOptions.HtmlDefault;
                default:
                    return LoadOptions.XlsxDefault;
            }
        }

    }
}
