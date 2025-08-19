using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.ColoresRepository;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.StockRepository;
using Services;
using Services.ColorService;
using Services.LocalService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.StockService;
using SharedForms.Ventas;

namespace SharedForms.Estadisticas.Stock
{
    public partial class stockBusqueda : Form, IreceptorArticulo
    {
        public stockBusqueda()
        {
            InitializeComponent();
        }

        private void stock_Load(object sender, EventArgs e)
        {
            if (HelperService.esCliente(GrupoCliente.Opiparo))
            {
                maxTalle = 20;
            }
            cargarLocales();
            
            cagarProveedores();
            var colorService = new ColorService(new ColorRepository());
            _colores = colorService.GetAll(true);
            
        }


        private void cargarLocales()
        {
            
            var localService = new LocalService(new LocalRepository());
            cmbLocales.DataSource = localService.GetAll();
            cmbLocales.DisplayMember = "Codigo";
        }


        List<ColorData> _colores = new List<ColorData>();
        private void tunearTabla()
        {
            DataGridViewTextBoxColumn primeraColumna = new DataGridViewTextBoxColumn();

            primeraColumna.HeaderText = "Color/Mts";
            primeraColumna.Width = 80;
            primeraColumna.Name = "primera";
            primeraColumna.Frozen = true;
            tabla.Columns.Add(primeraColumna);

        }
        private bool validoelinterno(string p)
        {
            bool valido = true;

            if (p.Length != 7)
            {
                valido = false;
            }



            return valido;
        }

        private void cargoconelcodigo(string cod)
        {
            string pr = cod.Substring(0, 4);
            string a = cod.Substring(4, 3);



            List<ProveedorData> proveedores = (List<ProveedorData>)cmbProveedor.DataSource;


            cmbProveedor.SelectedItem = proveedores.Find(delegate(ProveedorData p)
            {
                return p.Codigo == pr;
            });


            List<ProductoData> productos = (List<ProductoData>)cmbArticulo.DataSource;



            cmbArticulo.SelectedItem = productos.Find(delegate(ProductoData p)
            {
                return p.CodigoInterno == cod;
            });



        }

        private void txtinterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar.ToString() == "\r" && (txtinterno.Text == "*" || txtinterno.Text == "0"))
            {
                padreBase.AbrirForm(new BuscarArticulo(this), this.MdiParent, false, FormStartPosition.CenterScreen);
            }
            else if ((e.KeyChar.ToString() == "\r" && txtinterno.Text.Length >= 7))
            {
                if (validoelinterno(txtinterno.Text))
                {
                    cargoconelcodigo(txtinterno.Text);
                }
            }
        }
        private int obtenerColumnaPorMetros(int metroaux)
        {
            foreach (DataGridViewColumn columna in tabla.Columns)
            {
                if (columna.HeaderText != "Color/Mts")
                {
                    if (Convert.ToInt32(columna.HeaderText) == metroaux)
                        return columna.Index;
                }
            }
            return -1;


        }

        private void cargarTabla(ProductoData productoData, Guid idLocal)
        {

            limpiarTabla();
            var stockService = new StockService(new StockRepository());
            

            List<int> talles = Enumerable.Range(1, 50 - 1).ToList();

            talles.Sort();

            int columna = -1;
            foreach (int entry in talles)
            {
                columna = obtenerColumnaPorMetros(entry);
                if (columna == -1)
                {
                    DataGridViewColumn cc = new DataGridViewTextBoxColumn();
                    cc.HeaderText = entry.ToString();
                    cc.Width = 80;
                    cc.Name = entry.ToString();
                    tabla.Columns.Add(cc);

                }
            }

            DataGridViewColumn c = new DataGridViewTextBoxColumn();
            c.HeaderText = "Total x Color";
            c.Width = 80;
            c.Name = "col";
            tabla.Columns.Add(c);


            if (talles != null && talles.Count > 0)
            {

                decimal sum = 0;
                int auxI = 0;

                List<StockData> stockProducto = stockService.getAllbyLocalAndProducto(idLocal, productoData.ID);
                foreach (ColorData color in _colores)
                {
                    sum = 0;

                    

                    tabla.Rows.Add();
                    int fila;
                    fila = tabla.RowCount - 1;
                    //Codigo nombre  color talle subtotal
                    tabla[0, fila].Value = color.Description;



                    decimal stock = -666;
                    int talleAux = -1;
                    foreach (DataGridViewColumn cc in tabla.Columns)
                    {
                        talleAux = -1;
                        stock = -666;
                        if (cc.HeaderText != "Color/Mts" && cc.HeaderText != "Total x Color")
                        {
                            try
                            {
                                stock = stockProducto.Find(delegate(StockData s)
                                {
                                    return s.Color.ID == color.ID && s.Talle == Convert.ToInt32(cc.HeaderText);
                                }).Stock;
                            }
                            catch (Exception)
                            {
                                
                              
                            }
                            

                            if (stock != -666)
                            {
                                tabla[cc.Index, fila].Value = stock;
                                if (stock  > 0)
                                {
                                    sum += stock;
                                }

                            }
                            else
                            {
                                tabla[cc.Index, fila].Value = "";
                            }
                        }
                        tabla[tabla.ColumnCount - 1, fila].Value = sum.ToString();

                    }





                }
            }
            tabla.ClearSelection();



        }

        private void calcularTotal()
        {
            int sum = 0;
            foreach (DataGridViewRow row in tabla.Rows)
            {
                if (tabla[tabla.ColumnCount - 1, row.Index].Value != "Total x Color")
                {
                    sum += Convert.ToInt32(tabla[tabla.ColumnCount - 1, row.Index].Value);
                }
            }
            txtTotal.Text = sum.ToString();
        }

        private void limpiarTabla()
        {
            tabla.Rows.Clear();
            tabla.Columns.Clear();

            tabla.ClearSelection();

            tunearTabla();
        }

        private void cmbArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cagarProveedores()
        {
            cmbProveedor.DisplayMember = "razonSocial";
            cmbProveedor.DataSource =new ProveedorService(new ProveedorRepository()).GetAll(false);
            
        }

        private void cargarArticulos(ProveedorData proveedorData)
        {
            cmbArticulo.DisplayMember = "Show";
            var productoService = new ProductoService(new ProductoRepository());
            cmbArticulo.DataSource = productoService.GetbyProveedor(proveedorData.ID);
            
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedIndex > -1)
            {
                cargarArticulos((ProveedorData)cmbProveedor.SelectedItem);
            }
        }
        int maxTalle = 51;

        private void txtinterno_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbLocales_SelectedIndexChanged(object sender, EventArgs e)
        {

            limpiarTabla();
            cmbArticulo.SelectedIndex = -1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbArticulo.SelectedIndex > -1)
            {
                cargarTabla((ProductoData)cmbArticulo.SelectedItem, ((LocalData)cmbLocales.SelectedItem).ID);
                calcularTotal();
                if (chckLimpiar.Checked)
                {
                    limpiarColumnasYFilas();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un Articulo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }
        private void limpiarColumnasYFilas()
        {
            bool filetear = true;
            foreach (DataGridViewRow row in tabla.Rows)
            {
                filetear = true;
                foreach (DataGridViewCell celda in row.Cells)
                {
                    if (celda.ColumnIndex > 0)
                    {
                        if (celda.Value.ToString() != "" && celda.Value.ToString() != "0")
                        {
                            filetear = false;
                        }
                    }
                }
                if (filetear)
                    tabla.Rows[row.Index].Visible = false;
            }

            
            foreach (DataGridViewColumn column in tabla.Columns)
            {
                
                filetear = true;
                foreach (DataGridViewRow r in tabla.Rows)
                {
                    if (tabla[column.Index, r.Index].Value.ToString() != "" && tabla[column.Index, r.Index].Value.ToString() != "0")
                    {
                            filetear = false;
                    }

                  
                }
                if (filetear)
                    tabla.Columns[column.Index].Visible = false;
            }
        }

        public void selectProducto(Guid idproducto)
        {
            var productoService = new ProductoService(new ProductoRepository());
            
            ProductoData p = productoService.GetByID(idproducto);
            

            cmbProveedor.SelectedItem = p.Proveedor;
            cmbProveedor.SelectedIndex = cmbProveedor.FindStringExact(p.Proveedor.RazonSocial);
            cmbArticulo.SelectedIndex = cmbArticulo.FindStringExact(p.Show);

            txtinterno.Text = "";
            
        }
    }
}
