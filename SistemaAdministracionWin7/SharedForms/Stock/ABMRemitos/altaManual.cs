using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.ColoresRepository;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.RemitoRepository;
using Repository.Repositories.StockRepository;
using Repository.Repositories.UsuarioRepository;
using Services;
using Services.ColorService;
using Services.LocalService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.RemitoService;
using Services.StockService;
using Services.UsuarioService;
using SharedForms.Ventas;

namespace SharedForms.Stock
{
    public partial class altaManual : stockBase, IreceptorArticulo
    {
        public altaManual()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tabla.Rows.Count == 0)
            {
                MessageBox.Show("Debe de ingresar al menos un articulo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult mb = MessageBox.Show("Esta por dar de alta los articulos previamente ingresados, esta seguro que desea continuar?", "Alta", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (mb == DialogResult.OK)
                {

                    bool needPass = true;
                    string resultado = "";

                    helperForms.InputBox("Alerta", "Ingrese la contrasena para confirmar la operacion", ref resultado);

                    var usuarioService = new UsuarioService(new UsuarioRepository());
                    if (needPass)
                    {

                        if (!usuarioService.VerificarPermiso(resultado))
                        {
                            MessageBox.Show("La contrasena es erronea, intente nuevamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            HelperService.writeLog(
                                        "Password incorrecta", true, false);
                            GenerarArchivo("AltaManual", tabla.Rows);

                        }
                        else
                        {
                            RemitoData r = cargarRemito();
                            try
                            {


                                bool task;
                                bool task2;
                                task = remitoService.Insert(r);
                                task2 = remitoService.confirmarRecibo(r.ID, fechaRecibo.Value);
                                var localService = new LocalService(new LocalRepository());
                                if (task && task2)
                                {

                                    HelperService.MessageBoxHelper.InsertOk("");
                                    limpiarControles();
                                    ObtenerNumero();

                                }
                                else
                                {
                                    HelperService.MessageBoxHelper.InsertError("");
                                    HelperService.writeLog(
                                        "Se genero el archivo de AltaManual debido a... \n  Generar remito:" +
                                        task.ToString() + " Confirmar recibo:" + task2.ToString(), true, true);
                                    GenerarArchivo("AltaManual", tabla.Rows);

                                }
                            }
                            catch (Exception ee)
                            {
                                HelperService.MessageBoxHelper.InsertError("");
                                GenerarArchivo("AltaManual", tabla.Rows);
                                HelperService.writeLog(ee.ToString(), true, true);
                            }



                        }
                    }

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
            DateTime aux = fecha.Value;
            r.Date = aux;
            r.FechaRecibo = fechaRecibo.Value;
            r.ID = Guid.NewGuid();
            r.LocalDestino.ID = HelperService.IDLocal;
            r.Local.ID = HelperService.IDLocal;
            r.Numero = Convert.ToInt32(lblnro.Text.Split('-')[1]);
            r.Prefix = HelperService.Prefix;

            List<remitoDetalleData> rds = new List<remitoDetalleData>();
            int cantFilas = tabla.Rows.Count - 1;
            int fila;
            for (fila = 0; fila <= cantFilas; fila++)
            {
                remitoDetalleData detalle = new remitoDetalleData();
                detalle.Codigo = tabla[0, fila].Value.ToString();
                detalle.Cantidad = HelperService.ConvertToDecimalSeguro(tabla[5, fila].Value.ToString());
                detalle.FatherID = r.ID;
                rds.Add(detalle);

            }
            r.Description = txtObs.Text;
            r.Children = rds;
            r.CantidadTotal = HelperService.ConvertToDecimalSeguro(txtPares.Text);
            r.Enable = true;
            return r;
        }

        private void limpiarControles()
        {

            tabla.Rows.Clear();
            txtinterno.Text = "";
            txtPares.Text = "0";

            if (HelperService.talleUnico)
            {
                txtPongotalle.Text = "0";
                txtPongotalle.ReadOnly = true;
            }
        }

        private RemitoService remitoService = null;
        private StockService stockService = null;
        private void altaManual_Load(object sender, EventArgs e)
        {
            stockService = new StockService(new StockRepository());
            remitoService = new RemitoService(new RemitoRepository(), new RemitoDetalleRepository());
            cagarProveedores();
            cargarColores();
            ObtenerNumero();
            txtinterno.Focus();

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

            if (HelperService.esCliente(GrupoCliente.Opiparo) || HelperService.esCliente(GrupoCliente.Slipak))
            {
                txtinterno.Visible = false;
                lbinterno.Visible = false;
            }
        }

        private void ObtenerNumero()
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

            cmbProveedor.DataSource = new ProveedorService(new ProveedorRepository()).GetAll(true);
            cmbProveedor.DisplayMember = "razonSocial";
        }



        private void tabla_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

            CuentoArticulos(tabla, txtPares, HelperService.haymts ? 6 : 5);
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

        private void txtinterno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtinterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r" && (txtinterno.Text == "*" || txtinterno.Text == "0"))
            {
                padreBase.AbrirForm(new BuscarArticulo(this), this.MdiParent, false, FormStartPosition.CenterScreen);
            }
            if (e.KeyChar.ToString() == "\r" && (txtinterno.Text.Length >= 12))
            {
                Agregar(txtinterno, txtCantidad, cmbArticulo, cmbColores, txtPongotalle, tabla, txtPares);
            }

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {

        }






        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            
            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion? \n se borraran todos los items cargados", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dg == DialogResult.OK)
            {
                var result= openFileDialog1.ShowDialog();
                if (result == DialogResult.OK) { 
                List<remitoDetalleData> detalles = HelperArchivoStock.ObtengoRemitoDetalleDeTxt(openFileDialog1.FileName);

                limpiarControles();
                cargoDetallesEnTabla(detalles, tabla, txtPares);
                }
            }
        }



        private void label2_Click(object sender, EventArgs e)
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
            txtPongotalle.Focus();
        }
    }
}
