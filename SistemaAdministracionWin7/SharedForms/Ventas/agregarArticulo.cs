using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.ColoresRepository;
using Repository.Repositories.PrecioRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.StockRepository;
using Repository.Repositories.TalleMetrosRepository;
using Services;
using Services.ColorService;
using Services.PrecioService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.StockService;

namespace SharedForms.Ventas
{
    public partial class agregarArticulo : ventaBase, IreceptorArticulo
    {
       private Guid idlista;
        public agregarArticulo(Guid idListaPrecio) 
        {
            InitializeComponent();
            idlista = idListaPrecio;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            validoyAgrego();

        }

        private void validoyAgrego()
        {
            if (validarDetalle(txtinterno.Text, cmbArticulo.SelectedIndex, cmbColores.SelectedIndex, txtPongotalle.Text, txtprecio.Text))
                agregarProducto();
        }

        private void cagarProveedores()
        {
            var proveedorService = new ProveedorService(new ProveedorRepository());
            cmbProveedor.DataSource = proveedorService.GetAll(true);
            cmbProveedor.DisplayMember = "razonSocial";
        }
        private void cargarColores()
        {
            var colorService = new ColorService(new ColorRepository());
            cmbColores.DataSource = colorService.GetAll(true);
            cmbColores.DisplayMember = "Description";
        }



        private StockMetrosService stockMetrosSerice = null;
        private StockService stockService = null;

        private void agregarArticulo_Load(object sender, EventArgs e)
        {

            stockMetrosSerice = new StockMetrosService(new TalleMetrosRepository());
            stockService = new StockService(new StockRepository());

            if (HelperService.talleUnico)
            {
                txtPongotalle.Text = "0";
                txtPongotalle.ReadOnly = true;
                txtPongotalle.Visible = false;
                lblTalle.Visible = false;
            }

            if (HelperService.haymts)
            {
                txtPongotalle.Text = "0";
                lblTalle.Text = "Mts";


            }
            if (HelperService.talleUnico)
            {
                txtPongotalle.Text = "0";
                lblTalle.Text = "Mts";


            }
            //ventaBase
            cagarProveedores();
            cargarColores();
            if (HelperService.talleUnico)
                buscoData();

        }
        private void agregarProducto()
        {
            txtprecio.Text = HelperService.ConvertToDecimalSeguro(txtprecio.Text).ToString();


            if (txtinterno.Text == "" || txtinterno.Text.Length < 12)
            {
                if (HelperService.haymts)
                {
                    txtinterno.Text = stockMetrosSerice.obtenerCodigo(((ProductoData)cmbArticulo.SelectedItem).CodigoInterno,
                        ((ColorData)cmbColores.SelectedItem).Codigo, txtPongotalle.Text);
                }
                else
                {
                    txtinterno.Text = ((ProductoData)cmbArticulo.SelectedItem).CodigoInterno + ((ColorData)cmbColores.SelectedItem).Codigo + Convert.ToInt32(txtPongotalle.Text).ToString("00");
                }
            }


            StockData s = stockService.obtenerProducto(txtinterno.Text);

            //if (s.NotInDB && !HelperService.validarCodigo(s.Codigo))
            //{
            //    s = new stockDummyData(txtinterno.Text);
            //} //todo! verificar este comentt.

            foreach (Form hijo in this.MdiParent.MdiChildren)
            {
                if (hijo.GetType() != typeof(agregarArticulo) && hijo.GetType().BaseType == typeof(ventaBase))
                {
                    ((ventaBase)hijo).agregarArticulo(s, cmbArticulo.SelectedIndex, cmbColores.SelectedIndex, txtPongotalle.Text, txtprecio.Text);
                }

            }

            this.Close();

        }

        private void txtinterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }

            if (txtinterno.Text.Length == 12 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar.ToString() == "\r" && txtinterno.Text.Length >= 12)
            {
                if (validoelinterno(txtinterno.Text))
                {
                    if (!HelperService.esCliente(GrupoCliente.CalzadosMell))
                    {
                        cargoconelcodigo(txtinterno.Text);
                    }

                    txtprecio.Focus();
                }
            }
            else if (e.KeyChar.ToString() == "\r" && (txtinterno.Text == "*" || txtinterno.Text == "0"))
            {
                padreBase.AbrirForm(new BuscarArticulo(this), this.MdiParent, false, FormStartPosition.CenterScreen);
            }
        }

        private bool convertidor(string value, string mensaje, out int number)
        {




            bool result = Int32.TryParse(value, out number);
            if (result)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Error ingresando el Codigo" + mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

        }

        private bool validoelinterno(string cod)
        {
            bool valido = true;



            ///
            ///     0000     000       000         00
            ///proveedor(4)  art(3)    color(3)    talle (2)

            if (cod.Length >= 12)
            {
                valido = true;
            }
            else
            {
                return false;
            }

            string pr = cod.Substring(0, 4);
            string a = cod.Substring(4, 3);
            string col = cod.Substring(7, 3);
            string talle = cod.Substring(10, 2);


            int aux;
            if (!(convertidor(pr, " Proveedor", out aux)))
            {
                return false;
            }
            if (!(convertidor(a, " Articulo", out aux)))
            {
                return false;
            }
            if (!(convertidor(col, " Color", out aux)))
            {
                return false;
            }
            if (!(convertidor(talle, " Talle", out aux)))
            {
                return false;
            }









            return valido;
        }

        private void txtinterno_Leave(object sender, EventArgs e)
        {
            if (validoelinterno(txtinterno.Text))
            {
                if (!HelperService.esCliente(GrupoCliente.CalzadosMell))
                {
                    cargoconelcodigo(txtinterno.Text);
                }

                txtprecio.Focus();
            }
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

        private void cargoconelcodigo(string cod)
        {
            string pr = cod.Substring(0, 4);
            string a = cod.Substring(4, 3);
            string col = cod.Substring(7, 3);
            string talle = cod.Substring(10, 2);



            List<ProveedorData> proveedores = (List<ProveedorData>)cmbProveedor.DataSource;


            cmbProveedor.SelectedItem = proveedores.Find(delegate(ProveedorData p)
            {
                return p.Codigo == pr;
            });


            List<ProductoData> productos = (List<ProductoData>)cmbArticulo.DataSource;



            cmbArticulo.SelectedItem = productos.Find(delegate(ProductoData p)
            {
                return p.CodigoInterno == a;
            });


            List<ColorData> colores = (List<ColorData>)cmbColores.DataSource;

            cmbColores.SelectedItem = colores.Find(delegate(ColorData c)
            {
                return c.Codigo == col;
            });


            txtPongotalle.Text = talle;


        }

        private void cargarPrecio()
        {
            string codigo;
            if (txtinterno.Text == "" || txtinterno.Text.Length < 12)
            {
                if (HelperService.haymts)
                {
                    codigo = stockMetrosSerice.obtenerCodigo(((ProductoData)cmbArticulo.SelectedItem).CodigoInterno,
                        ((ColorData)cmbColores.SelectedItem).Codigo, txtPongotalle.Text, false);
                }
                else
                {
                    codigo = ((ProductoData)cmbArticulo.SelectedItem).CodigoInterno + ((ColorData)cmbColores.SelectedItem).Codigo + Convert.ToInt32(txtPongotalle.Text).ToString("00");
                }
            }
            else
            {

                codigo = txtinterno.Text;
            }
            var productoTalleService = new ProductoTalleService(new ProductoTalleRepository());
            var precioService = new PrecioService(new PrecioRepository());
            StockData s = stockService.obtenerProducto(codigo);
            ProductoTalleData p = new ProductoTalleData();
            p.IDProducto = s.Producto.ID;
            p.Talle = s.Talle;
            if (s.Producto.ID != Guid.Empty)
            {


                decimal precio = precioService.GetPrecio(idlista, (productoTalleService.GetIDByProductoTalle(p)));
                txtprecio.Text = precio.ToString();

            }
            else
            {
                txtprecio.Text = "0";
            }

        }

        private void txtPongotalle_TextChanged(object sender, EventArgs e)
        {
            if (txtPongotalle.Text != "" && txtPongotalle.Text != "0")
            {

                buscoData();

            }
        }

        private void buscoData()
        {
            if (todovalido())
            {
                cargarStock();
                cargarPrecio();
            }
            else
            {
                //MessageBox.Show("Complete correctamente los campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cargarStock()
        {
            string codigo;
            if (txtinterno.Text == "" || txtinterno.Text.Length < 12)
            {
                if (HelperService.haymts)
                {
                    codigo = stockMetrosSerice.obtenerCodigo(((ProductoData)cmbArticulo.SelectedItem).CodigoInterno,
                        ((ColorData)cmbColores.SelectedItem).Codigo, txtPongotalle.Text, false);
                }
                else
                {
                    codigo = ((ProductoData)cmbArticulo.SelectedItem).CodigoInterno + ((ColorData)cmbColores.SelectedItem).Codigo + Convert.ToInt32(txtPongotalle.Text).ToString("00");
                }
            }
            else
            {

                codigo = txtinterno.Text;
            }


            decimal stock = stockService.GetStockTotal(codigo);
            if (stock < 0)
            {
                txtstocktotal.Text = "-1";
            }
            else
            {
                if (HelperService.haymts)
                {
                    txtstocktotal.Text = stock.ToString() + "  (" + (stock * HelperService.ConvertToDecimalSeguro((txtPongotalle.Text))) + ")";
                }
                else
                {
                    txtstocktotal.Text = stock.ToString();
                }
            }
        }

        private bool todovalido()
        {
            bool valido = true;
            if (HelperService.validarCodigo(txtinterno.Text))
            {
                valido = true;
            }
            else
            {
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
                if (!HelperService.haymts && txtPongotalle.Text == "")
                {
                    valido = false;
                }
            }


            return valido;
        }

        private void txtPongotalle_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                txtPongotalle.SelectAll();
            });
        }

        private void txtprecio_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                txtprecio.SelectAll();
            });
        }

        public void selectProducto(Guid idp)
        {

            var productoService = new ProductoService(new ProductoRepository());
            ProductoData p = productoService.GetByID(idp);

            cmbProveedor.SelectedItem = p.Proveedor;
            cmbProveedor.SelectedIndex = cmbProveedor.FindStringExact(p.Proveedor.RazonSocial);
            cmbArticulo.SelectedIndex = cmbArticulo.FindStringExact(p.Show);

            txtinterno.Text = "";
            txtPongotalle.Focus();


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtPongotalle_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtPongotalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtPongotalle, e);

        }

        private void txtprecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtprecio, e);

        }

        private void agregarArticulo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode.ToString() == "F4")
            {
                validoyAgrego();
            }
        }

        private void cmbArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HelperService.talleUnico)
            {
                buscoData();
            }
        }

        private void txtinterno_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
