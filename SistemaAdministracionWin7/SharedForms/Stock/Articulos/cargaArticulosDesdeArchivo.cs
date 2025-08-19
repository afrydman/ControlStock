using System;
using System.Collections.Generic;
using System.Transactions;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Services;
using Services.ProductoService;
using Services.ProveedorService;
using Excel = Microsoft.Office.Interop.Excel;

namespace SharedForms.Stock.Articulos
{
    public partial class cargaArticulosDesdeArchivo : Form
    {
        public cargaArticulosDesdeArchivo()
        {
            InitializeComponent();
        }
      

        private void button1_Click(object sender, EventArgs e)
        {
            var productoService = new ProductoService(new ProductoRepository());
            var proveedorService = new ProveedorService(new ProveedorRepository());
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            Excel.Range last;
            Excel.Range range;

            string file;
            this.openFileDialog1.FileName = "";

            string codigo = "";
            string descripcion = "";
            
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog1.FileName;



                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(file, 0, true, 5, "", "", true,
                    Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

                foreach (Excel.Worksheet hoja in xlWorkBook.Worksheets)
                {//por cada hoja...

                    if (hoja.Name.Length==4)
                    {//codigo descripcion
                        last= hoja.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                        range = hoja.get_Range("A2", last);
                        
                        
                        object[,] range_values = (object[,])range.Cells.Value2;
                        
                        List<Tuple<string, string>> items = new List<Tuple<string, string>>();//codigo - descripcion

                        for (int i = 1; i <= range_values.GetLength(0); i++)
                        {
                            if (range_values[i, 1] != null && range_values[i, 2]!=null)
                            {
                                items.Add(Tuple.Create(range_values[i, 1].ToString(), range_values[i, 2].ToString()));    
                            }
                        }

                        personaData provedorHoja = proveedorService.GetByCodigo(hoja.Name);
                       
                        if (provedorHoja==null || !string.IsNullOrEmpty(provedorHoja.codigo))
                        {
                            List<productoData> productosProveedor = productoService.GetbyProveedor(provedorHoja.ID);


                            foreach (Tuple<string, string> item in items)
                            {
                                tabla.Rows.Add();
                                int fila;
                                fila = tabla.RowCount - 1;


                                //id provedordesc codigo descr cod)int accion
                                


                                productoData encontro = productosProveedor.Find(delegate(productoData data)
                                {
                                    return data.codigoProveedor.ToLower() == item.Item1.ToLower().Trim();
                                });

                                if (encontro!=null && encontro.Description != "")//encontro algo
                                {

                                    cargarRow(encontro, item,fila,provedorHoja, true);
                                   


                                }
                                else
                                {//no encontro entonces inserto producto

                                    cargarRow(encontro, item, fila, provedorHoja, false);
                                   
                                }
                            }


                        }
                        else
                        {
                            MessageBox.Show("No existe un provedor con el codigo: " + hoja.Name + "\nDicha hoja no sera tomada en cuenta ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        


                        
                    }
                    else
                    {
                        MessageBox.Show("Error en el formato del nombre de la hoja:" + hoja.Name + "\nDicha hoja no sera tomada en cuenta ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                
                
                //xlWorkSheet = (Excel.Worksheet) xlWorkBook.Worksheets.get_Item(1);

                //Excel.Range excelCell = (Excel.Range)xlWorkSheet.get_Range("A1", "A1");//cambiar el rango, a uno que sea hasta que termina
                

                //MessageBox.Show(excelCell.Value2.ToString());//si el value2 es null no le podes hacer to string!

                xlWorkBook.Close(false, misValue, misValue);
                xlApp.Quit();

                //releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
            }
        
    }

        string  INSERTAR_TEXT="Insertar producto";
        string ACTUALIZAR_TEXT = "Actualizar Descripcion";

        private void cargarRow(productoData encontro, Tuple<string, string> item,int fila,personaData proveedor, bool actualizar)
        {
            if (encontro != null) { 
                tabla[0, fila].Value = encontro.ID;
                tabla[4, fila].Value = encontro.codigoInterno;
            }
            tabla[1, fila].Value = proveedor.razonSocial;
            tabla[2, fila].Value = item.Item1;
            tabla[3, fila].Value = item.Item2;

            tabla[5, fila].Value = actualizar ? ACTUALIZAR_TEXT : INSERTAR_TEXT;
            tabla[6, fila].Value = proveedor.codigo;
            tabla[7, fila].Value = proveedor.ID;


        }

        

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
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void cargaArticulosDesdeArchivo_Load(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            bool todook = true;
            var productoService = new ProductoService(new ProductoRepository());
            if (tabla.Rows.Count > 1)
            {

                DialogResult mb =
                    MessageBox.Show(
                        "Esta por dar de alta/modificar los articulos previamente ingresados, esta seguro que desea continuar?",
                        "Alta", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (mb == DialogResult.OK)
                {

                    var opts = new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                    };

                    using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
                    {

                        //conexion.closeConecction(); //Para que se vuelva a abrir dentro de la trans!!todo!

                        try
                        {

                            productoData p;
                            foreach (DataGridViewRow row in tabla.Rows)
                            {
                                if (!todook)
                                    continue;

                                if (tabla[5, row.Index].Value.ToString() == INSERTAR_TEXT)
                                {
                                    p = new productoData();

                                    p.codigoInterno = productoService.GenerarCodigoInterno(tabla[6, row.Index].Value.ToString());
                                    p.codigoProveedor = tabla[2, row.Index].Value.ToString();

                                    p.proveedor = new proveedorData(new Guid(tabla[7, row.Index].Value.ToString()));
                                    p.Description = tabla[3, row.Index].Value.ToString();

                                    todook = productoService.Insert(p);
                                }
                                else
                                {
                                    p = productoService.GetByID(new Guid(tabla[0, row.Index].Value.ToString()));
                                    p.Description = tabla[3, row.Index].Value.ToString();
                                    todook = productoService.Update(p);
                                }




                            }

                            if (todook){
                                trans.Complete();
                             MessageBox.Show("Carga completa!", "Carga",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                limpiarTabla();
                            }
                            else
                            {
                                MessageBox.Show("Error al actualizar los datos desde el archivo", "Alerta",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                HelperService.writeLog("Error al actualizar los datos desde el archivo", true, true);

                            }


                        }
                        catch (Exception ee)
                        {
                            HelperService.writeLog("Error al actualizar los datos desde el archivo", true, true);
                            HelperService.writeLog(ee.Message, true, true);
                            HelperService.writeLog(ee.StackTrace, true, true);


                        }




                    }

                }
            
            }
            else
            {
                MessageBox.Show("Tabla vacia", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void limpiarTabla()
        {
           tabla.Rows.Clear();
        } 
    }
}
