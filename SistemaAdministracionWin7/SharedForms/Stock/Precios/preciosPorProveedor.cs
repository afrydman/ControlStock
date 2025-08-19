using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.ListaPrecioRepository;
using Repository.Repositories.PrecioRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.StockRepository;
using Services;
using Services.ListaPrecioService;
using Services.PrecioService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.StockService;

namespace SharedForms.Stock.Precios
{
    public partial class preciosPorProveedor : Form
    {
        public preciosPorProveedor()
        {
            InitializeComponent();
        }

        private void preciosPorProveedor_Load(object sender, EventArgs e)
        {
            cargarProveedores();
            cargarListas();
        }

        private void cargarListas()
        {
            var listaPrecioService = new ListaPrecioService(new ListaPrecioRepository());
            List<listaPrecioData> listas = listaPrecioService.GetAll(true);
            cmbLista.DisplayMember = "Description";
            cmbLista.DataSource = listas;
            cmbLista.DisplayMember = "Description";
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            limparTabla();
            var stockService = new StockService(new StockRepository());
            var productoService = new ProductoService(new ProductoRepository());
            if (todoOk())
            {

                List<ProductoData> productosProveedor = productoService.GetbyProveedor(((ProveedorData)cmbProveedor.SelectedItem).ID);

                foreach (ProductoData p in productosProveedor)
                {
                    List<StockData> s = stockService.getAllbyLocalAndProducto(HelperService.IDLocal, p.ID);

                    foreach (StockData item in s)
                   {
                       if (chckStock.Checked )
                       {

                           if (item.Stock > 0)
                           {

                               addtoTable(item);
                           }
                       }
                       else
                       {
                           addtoTable(item);
                       }
                   }
                }
            }
        }

        private void limparTabla()
        {
            tabla.Rows.Clear();
            tabla.ClearSelection();
        }

        private void addtoTable(StockData item)
        {
            var precioService = new PrecioService(new PrecioRepository());
            var productoTalleService = new ProductoTalleService(new ProductoTalleRepository());
            var stockService = new StockService(new StockRepository());
            
            tabla.Rows.Add();
            int fila;
            fila = tabla.RowCount - 1;
            //Codigo nombre  color talle subtotal
            tabla[0, fila].Value =item.Codigo;
            tabla[1, fila].Value = item.Producto.Proveedor.RazonSocial;
            tabla[2, fila].Value = item.Producto.Show;
            tabla[3, fila].Value = item.Color.Description;
            tabla[4, fila].Value = item.Talle.ToString();
            tabla[5, fila].Value = item.Stock.ToString();


            ProductoTalleData ptd = new ProductoTalleData();
            ptd.IDProducto = item.Producto.ID;
            ptd.Talle = Convert.ToInt32(item.Talle);
            decimal precio = precioService.GetPrecio(((listaPrecioData)cmbLista.SelectedItem).ID, (productoTalleService.GetIDByProductoTalle(ptd)));
            
            tabla[6, fila].Value = precio.ToString();
        }

        private bool todoOk()
        {
            bool ok = true;
            if (cmbLista.SelectedIndex==-1 || cmbProveedor.SelectedIndex==-1)
            {
                ok = false;   
            }
            return ok;
        }

        private void preciosPorProveedor_Load_1(object sender, EventArgs e)
        {

            formularioPorUsuario();

            cargarProveedores();
            cargarListas();
        }

        private void formularioPorUsuario()
        {
            if(HelperService.talleUnico)
                tabla.Columns[4].Visible = false;
        }

        private void cargarProveedores()
        {

            List<ProveedorData> pvs =new ProveedorService(new ProveedorRepository()).GetAll(true);
            cmbProveedor.DisplayMember = "razonSocial";
            cmbProveedor.DataSource = pvs;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tabla.Rows.Count > 0)
            {

              //  exportaraExcel();


              

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
        //    for (i = 0; i <= tabla.RowCount-1 ; i++)
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
