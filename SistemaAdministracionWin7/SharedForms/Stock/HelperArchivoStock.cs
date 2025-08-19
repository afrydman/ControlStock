using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Services;

namespace SharedForms.Stock
{
    public static class HelperArchivoStock
    {

        private static int maxTry = 10;
        public static List<PuntoControlStockDetalleData> ObtengoDetalleFromColectoraTxt(string FileName)
        {
            List<PuntoControlStockDetalleData> detalles = new List<PuntoControlStockDetalleData>();

            string codigo;
            PuntoControlStockDetalleData detalle;
            int badlines = 0;
            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    try
                    {


                        var readLine = sr.ReadLine();
                        if (readLine != null)
                        {

                            codigo = readLine.Substring(0, 12);
                            if (detalles.Any(d => d.Codigo == codigo))
                            {
                                detalles[detalles.FindIndex(d => d.Codigo == codigo)].Cantidad += 1;
                            }
                            else
                            {
                                detalle = new PuntoControlStockDetalleData();
                                detalle.Codigo = codigo;
                                detalle.Cantidad = 1;
                                detalles.Add(detalle);
                            }


                        }
                    }
                    catch (Exception)
                    {
                        badlines++;

                    }
                }
            }
            return detalles;


            /*
             * 
             * 
             * 
             * StockData s = stockService.obtenerProducto(txtinterno.Text);

                if (!HelperService.validarCodigo(s.Codigo))
                {
                    s = new stockDummyData(txtinterno.Text);

                }

                AgregoATabla(tabla, s, txtCantidad.Text);
             
             
             */
        }


        public static List<remitoDetalleData> ObtengoRemitoDetalleDeTxt(string FileName)
        {
            List<remitoDetalleData> aux = new List<remitoDetalleData>();


            remitoDetalleData a;

            string ln;
            int badlines = 0;
            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream && badlines < maxTry)
                {
                    try
                    {


                        a = new remitoDetalleData();
                        ln = sr.ReadLine();
                        a.Codigo = ln.Substring(0, 12);

                        if (ln.Substring(12, 1) == "-")
                        {//lo genero el sistema luego de un error

                            if (HelperService.haymts)
                            {
                                a.Cantidad = HelperService.ConvertToDecimalSeguro(ln.Substring(13));

                            }
                            else
                            {
                                a.Cantidad = Convert.ToInt32(ln.Substring(13));
                            }
                        }
                        else
                        {//es una , y lo genero el colector de datos, por ende es Cantidad = 1
                            a.Cantidad = 1;
                        }
                        aux.Add(a);
                    }
                    catch (Exception)
                    {
                        badlines++;

                    }
                }

            }





            return aux;
        }
    }
}
