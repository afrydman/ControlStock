using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using DTO.BusinessEntities;
using Repository.ColoresRepository;
using Repository.Repositories.PersonalRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.RemitoRepository;
using Repository.Repositories.StockRepository;
using Repository.Repositories.UsuarioRepository;
using Services.ColorService;
using Services.PersonalService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.PuntoControlService;
using Services;
using Services.RemitoService;
using Services.StockService;
using Services.UsuarioService;
using SharedForms.Stock;
using SharedForms.Ventas;

namespace SharedForms.Estadisticas.Stock.PuntoControl
{
    public partial class puntoControl : stockBase, IreceptorArticulo
    {
        public puntoControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Agregar(txtinterno,txtCantidad,cmbArticulo,cmbColores,txtPongotalle,tabla,txtPares);
            if (HelperService.talleUnico)
            {
                txtPongotalle.Text = "0";
                txtPongotalle.ReadOnly = true;
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult mb = MessageBox.Show("Esta por crear un punto de control, esta seguro que desea continuar?", "P.Control", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (mb == DialogResult.OK)
            {
                if (Valido())
                {
                    PuntoControlStockData r = CargarObjeto();
                    bool task = new PuntoControlService().Insert(r);

                    if (task)
                    {
                        MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarControles();
                        cargarNro();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (HelperService.talleUnico)
            {
                txtPongotalle.Text = "0";
                txtPongotalle.ReadOnly = true;
            }
        }

        private PuntoControlStockData CargarObjeto()
        {
            PuntoControlStockData newPuntoControl = new PuntoControlStockData();
            newPuntoControl.Prefix = HelperService.Prefix;
            newPuntoControl.Date = dateTimePicker1.Value;
            newPuntoControl.Description = txtObs.Text;
            newPuntoControl.Enable = true;
            newPuntoControl.ID = Guid.NewGuid();
            newPuntoControl.Local.ID = HelperService.IDLocal;
            newPuntoControl.Numero = Convert.ToInt32(new PuntoControlService().GetNextNumberAvailable(HelperService.IDLocal,
                HelperService.Prefix, false));
            newPuntoControl.Vendedor.ID = ((PersonalData)cmbvendedores.SelectedItem).ID;


            PuntoControlStockDetalleData child;
            foreach (DataGridViewRow row in tabla.Rows)
            {
                child = new PuntoControlStockDetalleData();
                child.Codigo = row.Cells[0].Value.ToString();
                child.Cantidad = Convert.ToDecimal(row.Cells[5].Value.ToString());
                newPuntoControl.Children.Add(child);
            }


            return newPuntoControl;

        }

        private bool Valido()
        {
            if (tabla.Rows.Count == 0)//que haya articulos
            {

                MessageBox.Show("Debe de ingresar al menos un articulo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            var usuarioService = new UsuarioService(new UsuarioRepository());

            string resultado = "";
            helperForms.InputBox("Alerta", "Ingrese la contrasena para confirmar la operacion", ref resultado);
            if (!usuarioService.VerificarPermiso(resultado))
            {
                MessageBox.Show("La contrasena es erronea, intente nuevamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;

            }



            return true;
        }



        private void limpiarControles()
        {
            tabla.DataSource = null;
            tabla.ClearSelection();
            tabla.Rows.Clear();
            txtinterno.Text = "";
            txtPongotalle.Text = "";
            txtObs.Text = "";
            txtCantidad.Text = "";
            txtPares.Text = "";
        }

        private StockService stockService;
        private PuntoControlService pcService;
        private void puntoControl_Load(object sender, EventArgs e)
        {
            stockService = new StockService(new StockRepository());
            pcService = new PuntoControlService();
            //cargarLocales();
            cargarVendedores();
            cargarNro();
            cagarProveedores();
            cargarColores();
            setClientGUI();
        }


        private void setClientGUI()
        {
            if (HelperService.talleUnico)
            {
                txtPongotalle.Text = "0";
                txtPongotalle.ReadOnly = true;
                txtPongotalle.Visible = false;
                lbltalle.Visible = false;
                tabla.Columns[4].Visible = false;


            }


            if (HelperService.haymts)
            {
                lbltalle.Text = "Mts";
                tabla.Columns[4].HeaderText = "Mts";
            }
            else
            {
                tabla.Columns[6].Visible = false;
            }



        }

        private void cargarVendedores()
        {
            var personalService = new PersonalService(new PersonalRepository());

            List<PersonalData> vendedores = personalService.GetAll();

            cmbvendedores.DataSource = vendedores;
            cmbvendedores.DisplayMember = "nombrecontacto";
        }
        private void cargarNro()
        {
            lblnro.Text = pcService.GetNextNumberAvailable(HelperService.IDLocal, HelperService.Prefix, true);
        }

        private void cargarColores()
        {
            var colorService = new ColorService(new ColorRepository());
            cmbColores.DataSource = colorService.GetAll(true);
            cmbColores.DisplayMember = "Description";
        }
        private void cagarProveedores()
        {

            cmbProveedor.DataSource = new ProveedorService(new ProveedorRepository()).GetAll(true);
            cmbProveedor.DisplayMember = "razonSocial";
        }

        private void txtinterno_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }

            else if (e.KeyChar.ToString() == "\r" && (txtinterno.Text == "*" || txtinterno.Text == "0"))
            {
                padreBase.AbrirForm(new BuscarArticulo(this), this.MdiParent, false, FormStartPosition.CenterScreen);
            }
            else if (e.KeyChar.ToString() == "\r" && (txtinterno.Text.Length >= 12))
            {
                Agregar(txtinterno, txtCantidad, cmbArticulo, cmbColores, txtPongotalle, tabla, txtPares);
            }
        }

        public override bool Valido(string txtinterno, string txtCantidad, ComboBox cmnArticulos, ComboBox cmbColores, string txtTalle, out string codigo)
        {
            if (base.Valido(txtinterno, txtCantidad, cmnArticulos, cmbColores, txtTalle, out codigo))
            {
                foreach (DataGridViewRow row in tabla.Rows)
                {
                    if (row.Cells[0].Value.ToString() == codigo)
                    {
                        MessageBox.Show(string.Format("Ya ingreso el mismo Articulo-Color-{0}, borrelo para poder ingresarlo nuevamente.", HelperService.haymts ? "Mts" : "Talle"), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }






        public void selectProducto(Guid idproducto)
        {
            var productoService = new ProductoService(new ProductoRepository());
            ProductoData p = productoService.GetByID(idproducto);

            cmbArticulo.SelectedIndex = cmbArticulo.FindStringExact(p.Show);
            txtCantidad.Focus();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedIndex > -1)
            {
                cargarArticulos((ProveedorData)cmbProveedor.SelectedItem);
            }
        }
        private void cargarArticulos(ProveedorData proveedorData)
        {
            var productoService = new ProductoService(new ProductoRepository());

            cmbArticulo.DataSource = productoService.GetbyProveedor(proveedorData.ID);
            cmbArticulo.DisplayMember = "Show";
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion? \n se borraran todos los items cargados", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dg == DialogResult.OK)
            {
                var result = openFileDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {
                    List<PuntoControlStockDetalleData> detalles = HelperArchivoStock.ObtengoDetalleFromColectoraTxt(openFileDialog1.FileName);

                    limpiarControles();
                    cargoDetallesEnTabla(detalles, tabla, new TextBox());
                }
            }
        }
    }
}
