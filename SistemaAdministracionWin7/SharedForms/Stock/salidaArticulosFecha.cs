using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.StockRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.LocalService;
using Services.StockService;
using Services.VentaService;

namespace SharedForms.Stock
{
    public partial class salidaArticulosFecha : Form
    {
        public salidaArticulosFecha()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (validoTodo())
            {
                txtTotal.Text = "0";
                cargarTabla();
                if (tablaTodas.Rows.Count > 0)
                {
                    tabla.Rows.Clear();
                    generarResumen();
                }
                else
                {
                    MessageBox.Show("No hay nada para mostrar en estas fechas");
                }
            }
        }

        private bool validoTodo()
        {
            if (pickerDesde.Value > pickerHasta.Value)
            {
                MessageBox.Show("Error al ingresar las fechas");
                return false;
            }

            return true;
        }

        private void cargarTabla()
        {
            limpiarTabla();
            tabla.Rows.Clear();
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            List<VentaData> ventas = ventaService.GetByRangoFecha(pickerDesde.Value.Date, pickerHasta.Value.Date, HelperService.IDLocal, HelperService.Prefix);
            var stockService = new StockService(new StockRepository());

            StockData s = null;
            ventas = ventas.FindAll(delegate(VentaData v) { return v.Enable; });

            
            foreach (VentaData v in ventas)
            {
                foreach (VentaDetalleData detalle in v.Children)
                {
                        tablaTodas.Rows.Add();
                        int fila;
                        fila = tablaTodas.RowCount - 1;

                        tablaTodas[0, fila].Value = v.ID;
                        tablaTodas[1, fila].Value =  HelperService.convertToFechaHoraConFormato(v.Date);
                        tablaTodas[2, fila].Value = v.NumeroCompleto;
                        tablaTodas[3, fila].Value = v.Cambio ? "Cambio" : "Venta";
                        tablaTodas[4, fila].Value = detalle.Codigo;

                        s = stockService.obtenerProducto(detalle.Codigo);

                        if (!HelperService.validarCodigo(s.Codigo))
                            s = new stockDummyData(detalle.Codigo);

                        tablaTodas[5, fila].Value = s.Producto.Proveedor.RazonSocial ;
                        tablaTodas[6, fila].Value = s.Producto.Show;
                        tablaTodas[7, fila].Value = s.Color.Description;
                        tablaTodas[8, fila].Value = detalle.Codigo.Substring(10, 2) ;
                        tablaTodas[9, fila].Value = detalle.Cantidad;

                }

            }





        }

        private void limpiarTabla()
        {
            tablaTodas.DataSource = null;
            tablaTodas.ClearSelection();
            tablaTodas.Rows.Clear();
        }

        private void salidaArticulosFecha_Load(object sender, EventArgs e)
        {
            var localService = new LocalService(new LocalRepository());
            pickerDesde.Value = localService.GetByID(HelperService.IDLocal).fechaStock;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tablaTodas.Rows.Count>0)
            {
             //   exportaraExcel(); 
            }
        }

        //private void exportaraExcel()
        //{
        //    Microsoft.Office.Interop.Excel.Application xlApp;
        //    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        //    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        //    object misValue = System.Reflection.Missing.Value;

        //    Int16 i, j;

        //    xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //    xlWorkBook = xlApp.Workbooks.Add(misValue);

        //    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


        //    //agrego los headers
        //    for (int ii = 0; ii < tabla.ColumnCount; ii++)
        //    {
        //        if (tabla.Columns[ii].Visible)
        //        {
        //            xlWorkSheet.Cells[1, ii + 1] = tabla.Columns[ii].HeaderText.ToString();
        //        }
        //    }


        //    //agrego la data
        //    for (i = 0; i <= tabla.RowCount - 1; i++)
        //    {
        //        for (j = 0; j <= tabla.ColumnCount - 1; j++)
        //        {
        //            if (tabla[j, i].Value != null)
        //            {
        //                xlWorkSheet.Cells[i + 2, j + 1] = tabla[j, i].Value.ToString();

        //            }

        //        }
        //    }

        //    SaveFileDialog openDlg = new SaveFileDialog();

        //    openDlg.InitialDirectory = @"C:\";
        //    openDlg.ShowDialog();
        //    string path = openDlg.FileName;

        //    if (openDlg.ShowDialog() == DialogResult.OK)
        //    {
        //        try
        //        {

        //            xlWorkBook.SaveAs(@path, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        //            MessageBox.Show("Archivo exportado de forma exitosa", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        }
        //        catch (Exception ee)
        //        {
        //            MessageBox.Show("Error al guardar el archivo en esa carpeta, intente nuevamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    xlWorkBook.Close(true, misValue, misValue);
        //    xlApp.Quit();

        //    releaseObject(xlWorkSheet);
        //    releaseObject(xlWorkBook);
        //    releaseObject(xlApp);
        //}
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tablaTodas.Rows.Count > 0)
            {
                tabla.Rows.Clear();
                generarResumen();
            }
            else
            {
                MessageBox.Show("Debe tener faltas para continuar");
            }
        }

        private void generarResumen()
        {
            var stockService = new StockService(new StockRepository());
            List<StockData> listadoArticulos = new List<StockData>();
            StockData s = null;
            foreach (DataGridViewRow row in tablaTodas.Rows)
            {
                s = stockService.obtenerProducto(row.Cells[4].Value.ToString());
                if (!HelperService.validarCodigo(s.Codigo))
                    s = new stockDummyData(row.Cells[4].Value.ToString());

                s.Stock = Convert.ToInt32(row.Cells[9].Value.ToString());
                
                var index = listadoArticulos.FindIndex(c => c.Codigo == row.Cells[4].Value.ToString());
                if (index>=0)
                {
                    listadoArticulos[index].Stock += s.Stock;
                }
                else
                {
                    listadoArticulos.Add(s);
                }

                
            }

            int fila;
            decimal tot = 0;
            listadoArticulos.Sort(delegate(StockData x, StockData y)
            {
                return x.Codigo.CompareTo(y.Codigo);

            });
            
            foreach (StockData elemento in listadoArticulos)
            {

                if (elemento.Stock>0)
                {
                    
                
                        tabla.Rows.Add();
                      
                        fila = tabla.RowCount - 1;

                        tabla[0, fila].Value = elemento.Codigo;
                        tabla[1, fila].Value = elemento.Producto.Proveedor.RazonSocial;
                        tabla[2, fila].Value = elemento.Producto.Show;
                        tabla[3, fila].Value = elemento.Color.Description;
                        tabla[4, fila].Value = elemento.Codigo.Substring(10, 2);
                        tabla[5, fila].Value = elemento.Stock.ToString();
                    tot += elemento.Stock;
                }
            }


            
            if (!HelperService.esCliente(GrupoCliente.Slipak))
            {
                txtTotal.Text = Convert.ToInt32(tot).ToString();
            }
        }
    }
}
