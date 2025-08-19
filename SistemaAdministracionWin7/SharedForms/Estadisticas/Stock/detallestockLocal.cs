using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DTO;
using DTO.BusinessEntities;
//using Microsoft.Office.Interop.Excel;
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
using Services.PuntoControlService;
using Services.StockService;
using SharedForms.Estadisticas.Stock.PuntoControl;
using SharedForms.Ventas;

namespace SharedForms.Estadisticas.Stock
{
    public partial class detallestockLocal : Form, IreceptorArticulo
    {
        public detallestockLocal()
        {
            InitializeComponent();
        }

        private void detallestockLocal_Load(object sender, EventArgs e)
        {
            cagarProveedores();
            cargarColores();
            cargarLocales();
            pickerDesde.Value = DateTime.Today.AddYears(-1);
            checkPuntoDeControl.Checked = true;
            if (HelperService.haymts)
            {
                lbltalle.Text = "Mts";
                tabla.Columns[6].HeaderText = "Mts";
            }
            if (HelperService.talleUnico)
            {
                lbltalle.Visible = false;
                txtTalle.Visible = false;
                tabla.Columns[6].Visible = false;
            }

        }
        private void cargarLocales()
        {
            var localService = new LocalService(new LocalRepository());
            cmbLocales.DataSource = localService.GetAll();
            cmbLocales.ValueMember = "Id";
            cmbLocales.DisplayMember = "Codigo";
            cmbLocales.SelectedValue = HelperService.IDLocal;
        }

        private void cmbArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        decimal aux = 0;

        private void cargarTabla(string codigo, Guid idLocal)
        {

            limpiarTabla();
            var stockService = new StockService(new StockRepository());
            List<detalleStockData> detalles = stockService.GetDetalleStock(codigo, idLocal,puntoControl);

         
            detalles = detalles.Any()?detalles.FindAll(dd => dd.fecha >= pickerDesde.Value.Date && dd.fecha <= pickerHasta.Value.Date.AddHours(23)):new List<detalleStockData>();

            foreach (detalleStockData d in detalles)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                tabla[0, fila].Value = HelperService.convertToFechaHoraConFormato(d.fecha);

                tabla[1, fila].Value = d.descripcion;
                tabla[2, fila].Value = d.codigo;
                tabla[3, fila].Value = d.producto.Proveedor.RazonSocial;
                tabla[4, fila].Value = d.producto.Show;
                tabla[5, fila].Value = d.color.Description;
                tabla[6, fila].Value = d.codigo.Substring(10, 2);
                tabla[7, fila].Value = d.cantidad.ToString();
                aux += d.cantidad;

                if(d.descripcion.ToLower().Contains("punto"))
                    tabla.Rows[fila].DefaultCellStyle.BackColor = Color.LawnGreen;
                tabla.ClearSelection();

            }
            lblTotal.Text = aux.ToString();

            if (tabla.Rows.Count == 0)
            {
                MessageBox.Show("Sin registro de stock para el articulo seleccionado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private bool ContieneCodigo(PuntoControlStockData punto, string codigo)
        {
            if (string.IsNullOrEmpty(codigo)) return false;

         
            return punto.Children.Any(child => child.Codigo.Substring(0,codigo.Length) == codigo );
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
            List<ColorData> colocolo = colorService.GetAll(true); ;

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
            cmbProveedor.DataSource = new ProveedorService(new ProveedorRepository()).GetAll(false);
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
            if (e.KeyChar.ToString() == "\r" && (txtinterno.Text == "*" || txtinterno.Text == "0"))
            {
                padreBase.AbrirForm(new BuscarArticulo(this), this.MdiParent, false, FormStartPosition.CenterScreen);
            }
            else if (e.KeyChar.ToString() == "\r" && txtinterno.Text.Length >= 7)
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
            LimpioPuntoControl();

            string cod = ArmoCodigoVacio();

            if (checkPuntoDeControl.Checked)
                BuscarPuntoControl(cod, ((LocalData)cmbLocales.SelectedItem).ID);

            cargarTabla(cod, ((LocalData)cmbLocales.SelectedItem).ID);


        }

        private void LimpioPuntoControl()
        {
            puntoControl = null;
            txtPuntoControl.Text = "";
        }

        private string ArmoCodigoVacio()
        {
            string cod = "";
            if (cmbArticulo.SelectedIndex > -1)
            {
                cod += (((ProductoData) cmbArticulo.SelectedItem).CodigoInterno);
            }

            if (cmbColores.SelectedIndex > 0)
            {
                cod += ((ColorData) cmbColores.SelectedItem).Codigo;
            }
            if (txtTalle.Text != "")
            {
                cod += Convert.ToInt32(txtTalle.Text).ToString("00");
            }
            if (txtinterno.Text != "" && txtinterno.Text.Length >= 7)
            {
                cod = txtinterno.Text;
            }
            return cod;
        }

        private PuntoControlStockData puntoControl;

        private void BuscarPuntoControl(string codigo, Guid idLocal)
        {
            var puntoControlService = new PuntoControlService();

            var puntos = puntoControlService.GetByRangoFecha(pickerDesde.Value, pickerHasta.Value, idLocal, HelperService.Prefix,true);

            PuntoControlStockData ultimoPunto = puntos.FindLast(punto => ContieneCodigo(punto, codigo));

            if (ultimoPunto != null && ultimoPunto.Date != HelperDTO.BEGINNING_OF_TIME_DATE)
            {
                txtPuntoControl.Text = ultimoPunto.NumeroCompleto;
                puntoControl = ultimoPunto;
                pickerDesde.Value = puntoControl.Date;
            }
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



        public void selectProducto(Guid idproducto)
        {
            var productoService = new ProductoService(new ProductoRepository());

            ProductoData p = productoService.GetByID(idproducto);


            cmbProveedor.SelectedItem = p.Proveedor;
            cmbProveedor.SelectedIndex = cmbProveedor.FindStringExact(p.Proveedor.RazonSocial);
            cmbArticulo.SelectedIndex = cmbArticulo.FindStringExact(p.Show);

            txtinterno.Text = "";
            txtTalle.Focus();
        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtinterno_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            padreBase.AbrirForm(new verPuntodeControl(puntoControl), this.MdiParent, true);
        }
    }
}
