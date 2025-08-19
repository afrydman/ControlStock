using System;
using System.Collections.Generic;
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

namespace SharedForms.Estadisticas.Stock.StockMetros
{
    public partial class StockBusquedaUnico : Form
    {
        public StockBusquedaUnico()
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

            DataGridViewTextBoxColumn col;
            foreach (ColorData c in _colores)
            {
                 col = new DataGridViewTextBoxColumn();
                 col.HeaderText = c.Description;
                col.Width = 80;
                col.Name = c.Description;
                col.Frozen = false;
                tabla.Columns.Add(col);
            }
        }

        private void cargarLocales()
        {
            var localService = new LocalService(new LocalRepository());
            cmbLocales.DataSource = localService.GetAll();
            cmbLocales.DisplayMember = "Codigo";
        }
        List<ColorData> _colores = new List<ColorData>();




        private void txtinterno_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void cargarTabla(ProveedorData proveedor, Guid idLocal)
        {
            limpiarTabla();
            var productoService = new ProductoService(new ProductoRepository());
            List<ProductoData> productos = productoService.GetbyProveedor(proveedor.ID);
            var stockService = new StockService(new StockRepository());

            //Codigo nombre  color talle subtotal
            
            int fila;
            int col;
            decimal stock = 0;

            foreach (ProductoData p in productos)
            {
                tabla.Rows.Add();
                
                fila = tabla.RowCount - 1;
                col = 0;
                tabla[0, fila].Value = p.Show;
                foreach (ColorData c in _colores)
                {
                    col++;
                    stock = -666;
                    stock =
                        stockService.GetStockTotal(p.CodigoInterno + c.Codigo, idLocal);

                    tabla[col, fila].Value = stock != -666 ? stock.ToString() : "-";
                }
            }
            tabla.ClearSelection();
        }

        private void limpiarTabla()
        {
            tabla.Rows.Clear();
            tabla.ClearSelection();
        }

        private void cmbArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cagarProveedores()
        {
            cmbProveedor.DataSource =new ProveedorService(new ProveedorRepository()).GetAll(false);
            cmbProveedor.DisplayMember = "razonSocial";
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        int maxTalle = 51;

        private void txtinterno_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbLocales_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiarTabla();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtTotal.Text = "0";
            if (cmbProveedor.SelectedIndex > -1)
            {
                cargarTabla((ProveedorData)cmbProveedor.SelectedItem, ((LocalData)cmbLocales.SelectedItem).ID);
                calcularTotal();

                
            }
            else
            {
                MessageBox.Show("Seleccione un Proveedor", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void calcularTotal()
        {
            decimal sum = 0;
            int aux = 0;
            foreach (DataGridViewRow row in tabla.Rows)
            {
                
                if ( int.TryParse(tabla[1, row.Index].Value.ToString(),out aux))
                {
                    sum += aux;
                }
            }
            txtTotal.Text = sum.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
