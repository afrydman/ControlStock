using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using DTO;
using DTO.BusinessEntities;
using Repository.ColoresRepository;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.PersonalRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.RemitoRepository;
using Repository.Repositories.StockRepository;
using Repository.Repositories.UsuarioRepository;
using Services;
using Services.ColorService;
using Services.LocalService;
using Services.PersonalService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.RemitoService;
using Services.StockService;
using Services.UsuarioService;
using SharedForms.Impositivo;
using SharedForms.Ventas;

namespace SharedForms.Stock
{
    public partial class BajaStock : stockBase, IreceptorArticulo
    {
        TransferService transferService = null;
        public BajaStock()
        {
            InitializeComponent();
             transferService = new TransferService();
        }



        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult mb = MessageBox.Show("Esta por dar de baja los articulos previamente ingresados, esta seguro que desea continuar?", "Baja", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (mb == DialogResult.OK)
            {

                try
                {


                    if (bajaValida())
                    {
                        RemitoData r = cargarRemito();
                        bool task = remitoService.Insert(r);
                        bool task2 = remitoService.confirmarBaja(r);
                        if (task && task2)
                        {

                            transferService.SendRemitoToLocal(r);
                            MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiarControles();
                            cargarNro();
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            HelperService.writeLog(
                                        "Se genero el archivo de Baja debido a... \n  Generar remito:" +
                                        task.ToString() + " Confirmar baja:" + task2.ToString(), true, true);
                            GenerarArchivo("Baja", tabla.Rows);
                        }



                    }
                    else
                    {
                        HelperService.writeLog(
                                        "Se genero el archivo de Baja debido a bajaValida = False"
                                       , true, true);
                        GenerarArchivo("Baja", tabla.Rows);

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GenerarArchivo("Baja", tabla.Rows);
                    HelperService.writeLog(ee.ToString(),true,true);
                }
            }

            if (HelperService.talleUnico)
            {
                txtPongotalle.Text = "0";
                txtPongotalle.ReadOnly = true;
            }
        }




        private RemitoData cargarRemito()
        {
            RemitoData r = new RemitoData();
            r.Date = DateTime.Now;
            r.ID = Guid.NewGuid();
            r.LocalDestino.ID = ((LocalData)cmbDestino.SelectedItem).ID;
            r.Local.ID = ((LocalData)cmbOrigen.SelectedItem).ID;
            r.Numero = Convert.ToInt32(lblnro.Text.Split('-')[1]);
            r.Prefix = HelperService.Prefix;
            r.Vendedor.ID = ((PersonalData)cmbvendedores.SelectedItem).ID;

            remitoDetalleData detalle;
            List<remitoDetalleData> rds = new List<remitoDetalleData>();
            int fila;
            int cantFilas = tabla.Rows.Count - 1;
            for (fila = 0; fila <= cantFilas; fila++)
            {
                 detalle = new remitoDetalleData();
                detalle.Codigo = tabla[0, fila].Value.ToString();
                detalle.Cantidad = HelperService.ConvertToDecimalSeguro(tabla[5, fila].Value.ToString());
                detalle.FatherID = r.ID;
                rds.Add(detalle);

            }

            r.Children = rds;
            r.Description = txtObs.Text;
            r.CantidadTotal = HelperService.ConvertToDecimalSeguro(txtPares.Text);//check that
            r.Enable = true;
            return r;



        }

        private void limpiarControles()
        {
            tabla.DataSource = null;
            tabla.ClearSelection();
            tabla.Rows.Clear();
            txtinterno.Text = "";
            txtPongotalle.Text = "";
            txtObs.Text = "";
            txtPares.Text = "";
        }

        private bool bajaValida()
        {
            if (tabla.Rows.Count == 0)//que haya articulos
            {

                MessageBox.Show("Debe de ingresar al menos un articulo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (cmbDestino.SelectedIndex == -1)
            {
                MessageBox.Show("Debe de seleccionar un destino", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            if (cmbOrigen.SelectedIndex == -1)
            {
                MessageBox.Show("Debe de seleccionar un origen", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private RemitoService remitoService = null;
        private StockService stockService = null;

        private void BajaStock_Load(object sender, EventArgs e)
        {
            stockService = new StockService(new StockRepository());
            remitoService = new RemitoService(new RemitoRepository(), new RemitoDetalleRepository());
            cargarLocales();
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
            if (HelperService.esCliente(GrupoCliente.CalzadosMell))
            {//default pongo el destino a depo.
                cmbDestino.SelectedItem =
                    ((List<LocalData>)cmbDestino.DataSource).Find(c => c.Description.ToLower().StartsWith("dep"));

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

            if (HelperService.esCliente(GrupoCliente.Opiparo) || HelperService.esCliente(GrupoCliente.Slipak))
            {
                txtinterno.Visible = false;
                lblinterno.Visible = false;
                this.Text = "Envio de stock";
                dateTimePicker1.Enabled = true;

            }
            
        }

        private void cargarNro()
        {
            lblnro.Text = remitoService.GetNextNumberAvailable(HelperService.IDLocal, HelperService.Prefix, true);
        }

        private void cargarColores()
        {
            var colorService = new ColorService(new ColorRepository());
            cmbColores.DataSource = colorService.GetAll(true);
            cmbColores.DisplayMember = "Description";
        }

        private void cagarProveedores()
        {

            cmbProveedor.DataSource =new ProveedorService(new ProveedorRepository()).GetAll(true);
            cmbProveedor.DisplayMember = "razonSocial";
        }


        private void cargarVendedores()
        {
            var personalService = new PersonalService(new PersonalRepository());

            List<PersonalData> vendedores = personalService.GetAll();

            cmbvendedores.DataSource = vendedores ;
            cmbvendedores.DisplayMember = "nombrecontacto";
        }



        private void cargarLocales()
        {
            var localService = new LocalService(new LocalRepository());
            List<LocalData> locales = localService.GetAll();


            List<LocalData> otros = locales.FindAll(delegate(LocalData l)
                                            {
                                                return l.ID != HelperService.IDLocal;
                                            }
                                           );
            List<LocalData> origen = new List<LocalData>();

            origen.Add(
                locales.Find(delegate(LocalData l)
            {
                return l.ID == HelperService.IDLocal;
            })
                                           );


            cmbDestino.DataSource = otros;
            cmbDestino.DisplayMember = "Codigo";


            cmbOrigen.DataSource = origen;
            cmbOrigen.DisplayMember = "Codigo";


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

       

        

        private bool todovalido()
        {
            bool valido = true;
            if (cmbProveedor.SelectedIndex == -1)
            {
                valido = false;
            }

            if (cmbArticulo.SelectedIndex == -1)
            {
                valido = false;
            }

            if (cmbColores.SelectedIndex == -1)
            {
                valido = false;
            }

            if (txtPongotalle.Text == "")
            {
                valido = false;
            }


            return valido;
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

        private void txtinterno_Leave(object sender, EventArgs e)
        {
            if (HelperService.validarCodigo(txtinterno.Text))
            {
                cargoconelcodigo(txtinterno.Text);
            }
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


        

        private void button1_Click(object sender, EventArgs e)
        {

            Agregar(txtinterno, txtCantidad, cmbArticulo, cmbColores, txtPongotalle, tabla, txtPares);
            if (HelperService.talleUnico)
            {
                txtPongotalle.Text = "0";
                txtPongotalle.ReadOnly = true;
            }
        }

        private void txtinterno_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion? \n se borraran todos los items cargados", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dg == DialogResult.OK)
            {
                List<remitoDetalleData> detalles = AbroArchivoYcosas();

                limpiarControles();
                cargoDetallesEnTabla(detalles, tabla, txtPares);
            }
        }
        private List<remitoDetalleData> AbroArchivoYcosas()
        {
            List<remitoDetalleData> aux = new List<remitoDetalleData>();


            remitoDetalleData a;

            string ln;
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {


                using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        a = new remitoDetalleData();
                        ln = sr.ReadLine();
                        a.Codigo = ln.Substring(0, 12);

                        if (ln.Length > 12 && ln.Substring(12, 1) == "-")
                        {//lo genero el sistema luego de un error

                            if (HelperService.haymts)
                            {
                                a.Cantidad = HelperService.ConvertToDecimalSeguro(ln.Substring(13));

                            }
                            else
                            {
                                a.Cantidad = Convert.ToInt32(ln.Substring(13));
                            }
                        }
                        else
                        {//es una , y lo genero el colector de datos, por ende es Cantidad = 1 o es el codigode barras viejo
                            a.Cantidad = 1;
                        }
                        aux.Add(a);
                    }

                }

            }



            return aux;
        }

        private void tabla_RowsRemoved_1(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CuentoArticulos(tabla, txtPares, HelperService.haymts ? 6 : 5);
        }

        public void selectProducto(Guid idproducto)
        {
            var productoService = new ProductoService(new ProductoRepository());
            ProductoData p = productoService.GetByID(idproducto);
            

            cmbProveedor.SelectedItem = p.Proveedor;
            cmbProveedor.SelectedIndex = cmbProveedor.FindStringExact(p.Proveedor.RazonSocial);
            cmbArticulo.SelectedIndex = cmbArticulo.FindStringExact(p.Show);

            txtinterno.Text = "";
            txtPongotalle.Focus();
        }

        private void txtinterno_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}