using System;
using System.Windows.Forms;

using DTO.BusinessEntities;
//using Excel = Microsoft.Office.Excel;
using Repository.Repositories.ListaPrecioRepository;
using Repository.Repositories.PrecioRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.RemitoRepository;
using Repository.Repositories.StockRepository;
using Services;
using Services.ListaPrecioService;
using Services.PrecioService;
using Services.ProductoService;
using Services.RemitoService;
using Services.StockService;

namespace SharedForms.Stock.Precios
{
    public partial class preciosPorRemito : Form
    {
        public preciosPorRemito()
        {
            InitializeComponent();
        }

        private void preciosPorRemito_Load(object sender, EventArgs e)
        {
            cargarRemitos();
            cargarListas();
        }

        private void cargarListas()
        {
            var remitoService = new RemitoService(new RemitoRepository(), new RemitoDetalleRepository());
            cmbRemito.DataSource = remitoService.GetByLocalOrigen(HelperService.IDLocal,false,true);
            cmbRemito.DisplayMember = "Show";
        }

        private void cargarRemitos()
        {
            var listaPrecioService = new ListaPrecioService(new ListaPrecioRepository());
            cmbLista.DataSource = listaPrecioService.GetAll(true);
            cmbLista.DisplayMember = "Description";
        }

        private void limparTabla()
        {
            tabla.Rows.Clear();
            tabla.ClearSelection();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var precioService = new PrecioService(new PrecioRepository());
            var productoTalleService = new ProductoTalleService(new ProductoTalleRepository());
            var stockService = new StockService(new StockRepository());
            limparTabla();
            if (todoOk())
            {
                RemitoData r = ((RemitoData)cmbRemito.SelectedItem);

                foreach (var item in r.Children)
                {
                    if (!yaEstaEnTabla(item.Codigo))
                    {


                        StockData s = stockService.obtenerProducto(item.Codigo);

                        tabla.Rows.Add();
                        int fila;
                        fila = tabla.RowCount - 1;
                        //Codigo nombre  color talle subtotal
                        tabla[0, fila].Value = item.Codigo;
                        tabla[1, fila].Value = s.Producto.Proveedor.RazonSocial;
                        tabla[2, fila].Value = s.Producto.Show;
                        tabla[3, fila].Value = s.Color.Description;
                        tabla[4, fila].Value = s.Talle;


                        ProductoTalleData p = new ProductoTalleData();
                        p.IDProducto = s.Producto.ID;
                        p.Talle = Convert.ToInt32(s.Talle);
                        decimal precio = precioService.GetPrecio(((listaPrecioData)cmbLista.SelectedItem).ID, (productoTalleService.GetIDByProductoTalle(p)));
                       
                        tabla[5, fila].Value = precio.ToString();
                    }

                }



            }
        }

        private bool yaEstaEnTabla(string codigo)
        {

            foreach (DataGridViewRow row in tabla.Rows)
            {
                if (row.Cells[0].Value.ToString().Substring(0, 7) == codigo.Substring(0, 7) && row.Cells[0].Value.ToString().Substring(10, 2) == codigo.Substring(10, 2))
                {
                    return true;
                }
            }
            return false;
        }

        private bool todoOk()
        {
            bool ok = true;
            if (cmbLista.SelectedIndex == -1 || cmbRemito.SelectedIndex == -1)
            {
                ok = false;
            }
            return ok;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (tabla.Rows.Count > 0)
            {

                // exportaraExcel();




            }
            else
            {
                MessageBox.Show("Primero debe de cargar la informacion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        //    for (i = 0; i <= tabla.RowCount -1; i++)
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
    }
}
