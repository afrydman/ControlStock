using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.StockRepository;
using Services;
using Services.StockService;

namespace SharedForms.Stock
{
    public partial class stockBase : Form
    {
        public stockBase()
        {
            InitializeComponent();
        }
        private void stockBase_Load(object sender, EventArgs e)
        {

        }
        public static void GenerarArchivo(string p, DataGridViewRowCollection dataGridViewRowCollection)
        {
            string file =
                String.Format(
                    Application.StartupPath + @"\{0}-" + String.Format("{0:dd-MM-yyyy-HH_mm}", DateTime.Now) + ".txt", p);


            using (StreamWriter writetext = new StreamWriter(file))
            {
                foreach (DataGridViewRow row in dataGridViewRowCollection)
                {
                    if (HelperService.haymts)
                    {
                        writetext.WriteLine(row.Cells[0].Value.ToString() + "-" + (HelperService.ConvertToDecimalSeguro(row.Cells[5].Value.ToString())).ToString());
                    }
                    else
                    {
                        writetext.WriteLine(row.Cells[0].Value.ToString() + "-" + (Convert.ToInt32(row.Cells[5].Value.ToString())).ToString());
                    }

                }
            }


        }
        public virtual void CuentoArticulos(DataGridView tabla, TextBox txtPares, int column = 5)
        {

            decimal aux = 0;


            foreach (DataGridViewRow row in tabla.Rows)
            {
                aux += HelperService.ConvertToDecimalSeguro(row.Cells[column].Value.ToString());

            }

            txtPares.Text = !HelperService.haymts ? Convert.ToInt32(aux).ToString() : aux.ToString();

        }
        public virtual bool Valido(string txtinterno, string txtCantidad, ComboBox cmnArticulos, ComboBox cmbColores, string txtTalle, out string codigo)
        {
            codigo = "";


            var stockService = new StockService();

            int cantidadMaxima = 100;


            if (txtCantidad == "")
            {
                MessageBox.Show("Debe de seleccionar una Cantidad", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!HelperService.haymts)
            {
                if (HelperService.ConvertToDecimalSeguro(txtCantidad) > cantidadMaxima)
                {
                    MessageBox.Show("Cantidad demasiada alta, maximo permitido " + cantidadMaxima.ToString(), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (HelperService.ConvertToDecimalSeguro(txtCantidad) == 0)
                {
                    MessageBox.Show("Cantidad nula, ingrese un nro mayor a 0", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }


            if (HelperService.validarCodigo(txtinterno))
            {
                codigo = txtinterno;
                return true;
            }

            if (cmnArticulos.SelectedIndex == -1)
            {
                MessageBox.Show("Debe de seleccionar un articulo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (cmbColores.SelectedIndex == -1)
            {
                MessageBox.Show("Debe de seleccionar un color", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (txtTalle == "")
            {
                MessageBox.Show("Debe de seleccionar un talle", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }



            if (HelperService.esCliente(GrupoCliente.Slipak))
            {

                codigo = stockService.GetCodigoBarraDinamico(((ProductoData)cmnArticulos.SelectedItem),
               ((ColorData)cmbColores.SelectedItem), txtTalle);
            }
            else
            {
                codigo = ((ProductoData)cmnArticulos.SelectedItem).CodigoInterno + ((ColorData)cmbColores.SelectedItem).Codigo + Convert.ToInt32(txtTalle).ToString("00");
            }


            return true;

        }
        public void cargoDetallesEnTabla(IEnumerable<IGetteableCodigoAndCantidad> detalles, DataGridView tabla, TextBox txtPares)
        {
            
            var stockService = new StockService(new StockRepository());
            foreach (var rd in detalles)
            {
                StockData s = stockService.obtenerProducto(rd.GetCodigo());

                if (!HelperService.validarCodigo(s.Codigo))
                    s = new stockDummyData(rd.GetCodigo());


                AgregoATabla(tabla, s, rd.GetCantidad().ToString());
                CuentoArticulos(tabla, txtPares, HelperService.haymts ? 6 : 5);

            }

        }
        public void Agregar(TextBox txtInterno, TextBox txtCantidad, ComboBox cmbArticulo, ComboBox cmbColores, TextBox txtTalle, DataGridView tabla,TextBox txtPares)
        {
            string codigo = "";
            var stockService = new StockService();
            if (Valido(txtInterno.Text, txtCantidad.Text, cmbArticulo, cmbColores, txtTalle.Text, out codigo))
            {

                StockData s = stockService.obtenerProducto(codigo);

                if (!HelperService.validarCodigo(s.Codigo))
                {
                    s = new stockDummyData(txtInterno.Text);
                }

                AgregoATabla(tabla, s, txtCantidad.Text);
                CuentoArticulos(tabla, txtPares, HelperService.haymts ? 6 : 5);
                txtInterno.Text = "";
                txtCantidad.Text = "";
                txtInterno.Focus();
            }
        }
        public void AgregoATabla(DataGridView tabla, StockData s, string txtCantidad)
        {
            tabla.Rows.Add();
            int fila;
            fila = tabla.RowCount - 1;
            //Codigo nombre  color talle subtotal
            tabla[0, fila].Value = s.Codigo;

            if ((s.GetType() == typeof(StockData)))
            {

                tabla[1, fila].Value = s.Producto.Proveedor.RazonSocial;
                tabla[2, fila].Value = s.Producto.Show;
                tabla[3, fila].Value = s.Color.Description;
            }
            else
            {
                tabla[1, fila].Value = "Sin Descripcion";
                tabla[2, fila].Value = "Sin Descripcion";
                tabla[3, fila].Value = "Sin Descripcion";
            }
            if (!HelperService.haymts)
                tabla[4, fila].Value = s.Codigo.Substring(10, 2);
            else
            {
                tabla[4, fila].Value = s.Metros.ToString();
            }
            tabla[5, fila].Value = txtCantidad;
            if (HelperService.haymts)
                tabla[6, fila].Value = HelperService.ConvertToDecimalSeguro(txtCantidad) * s.Metros;



        }

    }
}
