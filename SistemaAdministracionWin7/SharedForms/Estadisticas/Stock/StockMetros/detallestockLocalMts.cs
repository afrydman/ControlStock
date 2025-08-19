using System;
using System.Collections.Generic;
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
    public partial class DetallestockLocalMts : Form
    {
        public DetallestockLocalMts()
        {
            InitializeComponent();
        }

        private void detallestockLocal_Load(object sender, EventArgs e)
        {
            cagarProveedores();
            cargarColores();
            cargarLocales();
            pickerDesde.Value = DateTime.Today.AddDays(-30);
            if (HelperService.haymts)
            {
                lbltalle.Text = "Mts";
                tabla.Columns[6].HeaderText = "Mts";
            }
        }
        private void cargarLocales()
        {
            var localService = new LocalService(new LocalRepository());
            cmbLocales.DataSource = localService.GetAll();
            cmbLocales.DisplayMember = "Codigo";
        }
        
        private void cmbArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        decimal aux = 0;
        decimal aux2 = 0;

        private void cargarTabla(string p,Guid id)
        {

            limpiarTabla();
            var stockService = new StockService(new StockRepository());
            var stockMetrosService = new StockMetrosService(new TalleMetrosRepository());
            List<detalleStockData> detalles = stockService.GetDetalleStock(p, id);


            detalles = detalles.FindAll(
                                            delegate(detalleStockData dd)
                                            {
                                                return dd.fecha.Date >= pickerDesde.Value.Date && dd.fecha.Date <= pickerHasta.Value.Date;
                                            }
                                           );



            decimal mts = 0;
            foreach (detalleStockData d in detalles)
            {

                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                //fecha descripcion color talle Cantidad


                mts = stockMetrosService.obtenerMetrosPorTalle(d.producto.CodigoInterno, d.color.Codigo, Convert.ToInt32(stockMetrosService.from61ToDec(d.codigo.Substring(10, 2))));
                tabla[0, fila].Value = HelperService.convertToFechaHoraConFormato(d.fecha);
                tabla[1, fila].Value = d.descripcion;
                tabla[2, fila].Value = d.codigo;
                tabla[3, fila].Value = d.producto.Proveedor.RazonSocial;
                tabla[4, fila].Value = d.producto.Show;
                tabla[5, fila].Value = d.color.Description;
                tabla[6, fila].Value = mts;
                tabla[7, fila].Value = d.cantidad.ToString()  + " ("  + d.cantidad*mts + " )";
                aux += d.cantidad;
                aux2 += d.cantidad*mts;
                tabla.ClearSelection();

            }
            lblTotal.Text = aux.ToString() + " - ( " + aux2 + " )";

            if (tabla.Rows.Count == 0)
            {
                MessageBox.Show("Sin registro de stock para el articulo seleccionado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void limpiarTabla()
        {
            tabla.Rows.Clear();
            tabla.ClearSelection();
            lblTotal.Text = "0";
            aux = 0;
        }

 

        private void cargarColores()
        {
            var colorService = new ColorService(new ColorRepository());
            cmbColores.DisplayMember = "Description";
            List<ColorData> colocolo = colorService.GetAll(true); 

            ColorData auxColor = new ColorData();
            auxColor.Description = "TODOS";
            auxColor.Enable = true;
            auxColor.ID = new Guid();
            auxColor.Codigo = "todos";
            colocolo.Insert(0, auxColor);
            cmbColores.DataSource = colocolo;
            cmbColores.DisplayMember = "Description";
        }
        private void cagarProveedores()
        {
            cmbProveedor.DisplayMember = "razonSocial";
            cmbProveedor.DataSource =new ProveedorService(new ProveedorRepository()).GetAll(false);
            cmbProveedor.DisplayMember = "razonSocial";
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedIndex > -1)
            {
                cargarArticulos((ProveedorData)cmbProveedor.SelectedItem);
            }
            else
            {
                cargarArticulos(new ProveedorData());
            }
        }
        private void cargarArticulos(ProveedorData proveedorData)
        {
            var productoService = new ProductoService(new ProductoRepository());
            cmbArticulo.DataSource = productoService.GetbyProveedor(proveedorData.ID);
            cmbArticulo.DisplayMember = "Show";
        }

        private void txtinterno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtinterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsLetter(e.KeyChar)))
            {
                e.Handled = true;
            }
            if (e.KeyChar.ToString() == "\r" && txtinterno.Text.Length >= 7)
            {
                cargarTabla(txtinterno.Text, ((LocalData)cmbLocales.SelectedItem).ID);
             
            }
        }

        private void pickerDesde_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void pickerHasta_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void cmbColores_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cod = "";
            if (cmbArticulo.SelectedIndex > -1)
            {
                cod += (((ProductoData)cmbArticulo.SelectedItem).CodigoInterno);
            }

            if (cmbColores.SelectedIndex > 0)
            {
                cod += ((ColorData)cmbColores.SelectedItem).Codigo;
            }
            if (txtTalle.Text != "")
            {
                cod += Convert.ToInt32(txtTalle.Text).ToString("00");
            }
            if (txtinterno.Text != "" && txtinterno.Text.Length >= 7)
            {
                cod = txtinterno.Text;
            }

            cargarTabla(cod,((LocalData)cmbLocales.SelectedItem).ID);


        }

        private void txtTalle_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void pickerDesde_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void pickerDesde_ValueChanged_2(object sender, EventArgs e)
        {

        }

        private void cmbColores_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        
    }
}
