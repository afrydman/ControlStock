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
using Repository.Repositories.TalleMetrosRepository;
using Services;
using Services.ColorService;
using Services.LocalService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.StockService;

namespace SharedForms.Estadisticas.Stock.StockMetros
{
    public partial class StockBusquedaMts : Form
    {
        public StockBusquedaMts()
        {
            InitializeComponent();
        }
        
		private void stock_Load(object sender, EventArgs e)
        {
            txtTotal.Text = "0";
            if (HelperService.esCliente(GrupoCliente.Opiparo))
            {
                maxTalle = 20;
            }
            cargarLocales();
            cagarProveedores();
            limpiarTabla();
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
            if ((e.KeyChar.ToString() == "\r" && txtinterno.Text.Length >= 7))
            {
                if (validoelinterno(txtinterno.Text))
                {
                    cargoconelcodigo(txtinterno.Text);
                }
            }
        }
        
		private void cargarTabla(ProductoData productoData, Guid idLocal)
        {
            limpiarTabla();
            var stockService = new StockService(new StockRepository());
		    var stockMetrosService = new StockMetrosService(new TalleMetrosRepository());
            Dictionary<decimal, string> tallesMetro = stockMetrosService.obtenerTodoByProducto(productoData.ID);
            List<decimal> talles = tallesMetro.Keys.ToList();
		    if (talles!=null)
		    {
		        
		   
            talles.Sort();
            int columna = -1;
            foreach (decimal entry in talles)
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
                foreach (ColorData color in _colores)
                {
                    sum = 0;
                    Dictionary<string, decimal> tallesMetroColor = stockMetrosService.obtenerTodoByProductoColor(productoData.ID, color.ID);
                    tabla.Rows.Add();
                    int fila;
                    fila = tabla.RowCount - 1;
                    //Codigo nombre  color talle subtotal
                    tabla[0, fila].Value = color.Description;
                    decimal stock = -666;
                    string talleAux = "-";
                    foreach (DataGridViewColumn cc in tabla.Columns)
                    {
                        talleAux = "-";
                        stock = -666;
                        if (cc.HeaderText != "Color/Mts" && cc.HeaderText != "Total x Color")
                        {
                            try
                            {
                                talleAux = tallesMetroColor.Where(p => p.Value == HelperService.ConvertToDecimalSeguro(cc.HeaderText)).Single().Key;
                            }
                            catch
                            {
                                talleAux = "-";
                            }
                            if (talleAux != "-")
                            {
                                
                                stock =
                                    stockService.GetStockTotal(productoData.ID,
                                        color.ID,
                                        Convert.ToInt32(stockMetrosService.from61ToDec(talleAux))
                                        ,
                                        idLocal);
                            }
                            if (stock != -666)
                            {
                                tabla[cc.Index, fila].Value = stock + " -  (" + stock * HelperService.ConvertToDecimalSeguro(cc.HeaderText) + ")";
                                if (stock * HelperService.ConvertToDecimalSeguro(cc.HeaderText) > 0)
                                {
                                    sum += stock * HelperService.ConvertToDecimalSeguro(cc.HeaderText);    
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
        
		private void limpiarNegativos()
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
                foreach (DataGridViewRow row in tabla.Rows)
                {
                    if (tabla[column.Index, row.Index].Value.ToString() != "" && tabla[column.Index, row.Index].Value.ToString() != "0")
                    {
                        filetear = false;
                    }
                }
                if (filetear)
                    tabla.Columns[column.Index].Visible = false;
            }
        }
        
		private void limpiarColumnasYFilas(bool LimpiarSiVacio, bool LimpiarSiNegativo)
		{
		    bool limpiarSiAmbos = LimpiarSiVacio && LimpiarSiNegativo;
            bool filetearPorVacio = true;
            bool filetearPorNegativo = true;
		    bool filetearporAmbos = true;
            foreach (DataGridViewRow row in tabla.Rows)
            {
                filetearPorVacio = true;
                filetearPorNegativo = true;
                filetearporAmbos = true;
                foreach (DataGridViewCell celda in row.Cells)
                {
                    if (celda.ColumnIndex > 0)
                    {
                        if (celda.Value.ToString() != "")
                        {
                            if(celda.Value.ToString().Substring(0,1) != "0")
                                filetearPorVacio = false;

                            if (celda.Value.ToString().Substring(0, 1) != "-")
                                filetearPorNegativo = false;

                            if(celda.Value.ToString().Substring(0,1) != "0"&&celda.Value.ToString().Substring(0, 1) != "-")
                                filetearporAmbos = false;

                        }
                        
                    }
                }

                if (LimpiarSiVacio && filetearPorVacio)
                    tabla.Rows[row.Index].Visible = false;

                if (LimpiarSiNegativo && filetearPorNegativo)
                    tabla.Rows[row.Index].Visible = false;

                if (limpiarSiAmbos && filetearporAmbos)
                    tabla.Rows[row.Index].Visible = false;
                
               
            }
            foreach (DataGridViewColumn column in tabla.Columns)
            {
                filetearPorVacio = true;
                filetearPorNegativo = true;
                foreach (DataGridViewRow row in tabla.Rows)
                {
                    
                    if (tabla[column.Index, row.Index].Value.ToString() != "" && tabla[column.Index, row.Index].Value.ToString().Substring(0, 1) != "0")
                    {
                        filetearPorVacio = false;
                    }
                    if (tabla[column.Index, row.Index].Value.ToString() != "" && tabla[column.Index, row.Index].Value.ToString().Substring(0, 1) != "-")
                    {
                        filetearPorNegativo = false;
                    }
                }
                if (LimpiarSiVacio && filetearPorVacio)
                    tabla.Columns[column.Index].Visible = false;

                if (LimpiarSiNegativo && filetearPorNegativo)
                    tabla.Columns[column.Index].Visible = false;
            }
        }
        
		private int obtenerColumnaPorMetros(decimal metroaux)
        {
            foreach (DataGridViewColumn columna in tabla.Columns)
            {
                if (columna.HeaderText != "Color/Mts")
                {
                    if (HelperService.ConvertToDecimalSeguro(columna.HeaderText) == metroaux)
                        return columna.Index;
                }
            }
            return -1;
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
            cmbProveedor.DataSource =new ProveedorService(new ProveedorRepository()).GetAll(false);
            cmbProveedor.DisplayMember = "razonSocial";
        }
        
		private void cargarArticulos(ProveedorData proveedorData)
        {
            var productoService = new ProductoService(new ProductoRepository());
            cmbArticulo.DataSource = productoService.GetbyProveedor(proveedorData.ID);
            cmbArticulo.DisplayMember = "Show";
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
            txtTotal.Text = "0";
            if (cmbArticulo.SelectedIndex > -1)
            {
                cargarTabla((ProductoData)cmbArticulo.SelectedItem, ((LocalData)cmbLocales.SelectedItem).ID);
                calcularTotal();
                if (chckLimpiar.Checked || checkLimpiarNegativo.Checked)
                {
                    
                    limpiarColumnasYFilas(chckLimpiar.Checked, checkLimpiarNegativo.Checked);
                   
                }
            }
            else
            {
                MessageBox.Show("Seleccione un Articulo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
		private void calcularTotal()
        {
            decimal sum = 0;
            foreach (DataGridViewRow row in tabla.Rows)
            {
                if (tabla[tabla.ColumnCount - 1, row.Index].Value != "Total x Color")
                {
                    sum += HelperService.ConvertToDecimalSeguro(tabla[tabla.ColumnCount - 1, row.Index].Value);
                }
            }
            txtTotal.Text = sum.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            limpiarColumnasYFilas(chckLimpiar.Checked, checkLimpiarNegativo.Checked);
        }
    }
}
