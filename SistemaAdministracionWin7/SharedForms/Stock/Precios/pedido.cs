using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO;
using DTO.BusinessEntities;
using Repository.ClienteRepository;
using Repository.ColoresRepository;
using Repository.Repositories.ListaPrecioRepository;
using Repository.Repositories.PedidoRepository;
using Repository.Repositories.PersonalRepository;
using Repository.Repositories.PrecioRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.StockRepository;
using Services;
using Services.ClienteService;
using Services.ColorService;
using Services.ListaPrecioService;
using Services.PedidoService;
using Services.PersonalService;
using Services.PrecioService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.StockService;

namespace SharedForms.Stock
{
    public partial class pedido : Form
    {

        public pedido()
        {

            InitializeComponent();
        }
        


       

        private void button1_Click(object sender, EventArgs e)
        {

            agregarProducto();
            
            
            
        }

        private void agregarProducto()
        {
            if (validar())
            {
                AgregarArticulo();
                calcularSubTotal();
                //borrarTotal();
                limpiarControles(false);
                txtPongotalle.Text = "";
                txtinterno.Text = "";
            }
            cmbFormaPago.Focus();

        
        }
        private bool validarVenta() 
        {
            if (tabla.Rows.Count==0)//que haya articulos
            {

                MessageBox.Show("Debe de ingresar al menos un articulo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cmbClientes.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un cliente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cmbVendedor.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un vendedor", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
           
            return true;
        
        
        }
        private bool validar(){
        //{
        //    if (!HelperService.validarCodigo(txtPongotalle.Text))
        //    {
        //        MessageBox.Show("Codigo de articulo erroneo","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Information);
        //        return false;
        //    }

            if (HelperService.validarCodigo(txtinterno.Text))
            {
                return true;
            }

            if (cmbArticulo.SelectedIndex==-1)
            {
                MessageBox.Show("Debe de seleccionar un articulo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (cmbColores.SelectedIndex == -1)
            {
                MessageBox.Show("Debe de seleccionar un color", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txtPongotalle.Text == "")
            {
                MessageBox.Show("Debe de seleccionar un talle", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            if (txtprecio.Text == "" || txtprecio.Text == "."||txtprecio.Text==",")
	        {
                MessageBox.Show("Debe de ingresar un precio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
	        }
            if (HelperService.ConvertToDecimalSeguro(txtprecio.Text) <= 0)
            {
                MessageBox.Show("Debe de ingresar un precio mayor a cero", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            txtprecio.Text = HelperService.ConvertToDecimalSeguro(txtprecio.Text).ToString();
            
            


            return true;
                
        }

       

        private void borrarTotal()
        {
            txtTotal.Text = "";
            cmbFormaPago.SelectedIndex = -1;
        }

        private void calcularSubTotal()
        {
            decimal subt = 0;
            decimal descuento = 0;

            for (int i = 0; i < tabla.Rows.Count; i++)
			{
                subt += HelperService.ConvertToDecimalSeguro(tabla[6, i].Value);
			}

            if (txtdescuento.Text == "")
            {
                descuento = 0;
            }
            else
            {
                descuento = HelperService.ConvertToDecimalSeguro(txtdescuento.Text);
            }

            if (descuento < 100 && descuento >= 0)
            {
                txtsubtotal.Text = (subt - ((subt * descuento) / 100)).ToString();
            }
            else
            {
                MessageBox.Show("El descuento debe de ser entre 0 y 100", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdescuento.Text = "0";
                txtsubtotal.Text = subt.ToString();
            }
            
        }

        private void AgregarArticulo()
        {

            string codigo = "";

            //revisar esto
            if (HelperService.esCliente(GrupoCliente.CalzadosMell))
            {
                codigo = txtinterno.Text;
            }
            else
            {
                codigo = ((ProductoData)cmbArticulo.SelectedItem).CodigoInterno + ((ColorData)cmbColores.SelectedItem).Codigo + Convert.ToInt32(txtPongotalle.Text).ToString("00");
            }
                //stockData s = stock.obtenerProducto(Codigo);


            if (HelperService.esCliente(GrupoCliente.Opiparo))
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                
                //Codigo nombre  color talle uniutario canitdad subtotal 

                tabla[0, fila].Value = codigo;
                tabla[1, fila].Value = ((ProductoData)cmbArticulo.SelectedItem).Show;
                tabla[2, fila].Value = ((ColorData)cmbColores.SelectedItem).Description;

                tabla[3, fila].Value = txtPongotalle.Text;
                tabla[4, fila].Value = txtprecio.Text;
                tabla[5, fila].Value = 1;
                tabla[6, fila].Value = txtprecio.Text;


                tabla.ClearSelection();
            }
            else
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                //Codigo nombre  color talle subtotal 
                //Codigo nombre  color talle uniutario canitdad subtotal 


                tabla[0, fila].Value = codigo;
                if (!HelperService.esCliente(GrupoCliente.CalzadosMell))
                {
                    tabla[1, fila].Value = ((ProductoData)cmbArticulo.SelectedItem).Show;
                    tabla[2, fila].Value = ((ColorData)cmbColores.SelectedItem).Description;
                }
                tabla[3, fila].Value = txtPongotalle.Text;
                tabla[5, fila].Value = txtprecio.Text;
                tabla.ClearSelection();
            }
                
        }

        private PedidoService _pedidoService = null;
        private void venta_Load(object sender, EventArgs e)
        {
            _pedidoService = new PedidoService(new PedidoRepository(), new PedidoDetalleRepository());
            cagarProveedores();
            cargarColores();
            cargarListaPrecios();
            cargarClientes();
            

            obtenerNumeroPedido();
            cargarVendedores();
            
            txtinterno.Focus();
            
        }

        private void cargarClientes()
        {
            var ClienteService = new ClienteService(new ClienteRepository());
            cmbClientes.DisplayMember = "razonSocial";
            cmbClientes.DataSource = ClienteService.GetAll(true);
            cmbClientes.DisplayMember = "razonSocial";

            
        }

        private void cargarListaPrecios()
        {
            var listaPrecioService = new ListaPrecioService(new ListaPrecioRepository());
            cmbListaPrecios.DisplayMember = "Description";
            cmbListaPrecios.DataSource = listaPrecioService.GetAll();
            cmbListaPrecios.DisplayMember = "Description";

            cmbListaPrecios.SelectedIndex = 0;
        }

        private void cargarColores()
        {
            var colorService = new ColorService(new ColorRepository());
            cmbColores.DisplayMember = "Description";
            cmbColores.DataSource = colorService.GetAll(true);
            cmbColores.DisplayMember = "Description";
        }

        private void cagarProveedores()
        {
            cmbProveedor.DisplayMember = "razonSocial";
            cmbProveedor.DataSource =new ProveedorService(new ProveedorRepository()).GetAll(true);
            cmbProveedor.DisplayMember = "razonSocial";
        }

        

        private void obtenerNumeroPedido()
        {
            
            lblnumerofactura.Text = _pedidoService.GetNextNumberAvailable(HelperService.IDLocal,HelperService.Prefix,true);

        }

        private void cargarVendedores()
        {
            var personalService = new PersonalService(new PersonalRepository());

            List<PersonalData> vendedores = personalService.GetAll();

            cmbVendedor.DisplayMember = "nombrecontacto";
            cmbVendedor.DataSource = vendedores;
            cmbVendedor.DisplayMember = "nombrecontacto";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hacerVenta();
            txtinterno.Focus();
        }

        private void hacerVenta()
        {
            DialogResult dg = HelperService.MessageBoxHelper.confirmOperation();

            if (dg == DialogResult.OK)
            {

                if (Control.ModifierKeys != Keys.Shift)
                {
                    if (validarVenta())
                    {
                        procesarVenta();
                        obtenerNumeroPedido();
                        actualizarVentas();
                        limpiarControles(true);
                    }

                }
                else
                {
                    //
                }

            }
        }
        

        private void actualizarVentas()
        {
            
            
            foreach(Form hijo in this.MdiParent.MdiChildren)
            {
                if (hijo.GetType() == typeof(pedidos))
                {

                    ((pedidos)hijo).refresh2();
                }

            }
        }



        private pedidoData cargarVenta()
        {
            pedidoData nuevaVenta = new pedidoData();
            nuevaVenta.Date = DateTime.Now;



          

            if (txtdescuento.Text == "" || txtdescuento.Text == "." || txtdescuento.Text == ",")
                txtdescuento.Text = "0";
            nuevaVenta.Descuento = HelperService.ConvertToDecimalSeguro(txtdescuento.Text);
            
            

            LocalData l = new LocalData();
            l.ID = HelperService.IDLocal;
            nuevaVenta.Local = l;

            PersonalData p = new PersonalData();
            p = ((PersonalData)cmbVendedor.SelectedItem);
            nuevaVenta.Vendedor = p;
            nuevaVenta.Numero = Convert.ToInt32(lblnumerofactura.Text.Split('-')[1]);
            
            nuevaVenta.Prefix = HelperService.Prefix;
            nuevaVenta.Cliente = ((ClienteData)cmbClientes.SelectedItem);
            
            nuevaVenta.Monto = HelperService.ConvertToDecimalSeguro(txtsubtotal.Text);

            nuevaVenta.ID = Guid.NewGuid();
            

            return nuevaVenta;

        }

        private bool esCambio()
        {
            foreach (DataGridViewRow row in tabla.Rows)
            {
                if (Convert.ToBoolean(tabla[4, row.Index].Value)==true)
                {
                    return true;
                }
            }
            return false;
        }

        private void procesarVenta()
        {
            pedidoData nuevoPedido = new pedidoData();

            nuevoPedido = cargarVenta();

            Guid ventaID = nuevoPedido.ID;



            int cantFilas = tabla.Rows.Count-1;
            int fila;


            pedidoDetalleData detalle;
            for (fila = 0; fila <= cantFilas; fila++)
            {
              detalle  = new pedidoDetalleData();

                detalle = new pedidoDetalleData();
                detalle.FatherID = ventaID;
                detalle.precio = HelperService.ConvertToDecimalSeguro(tabla[4, fila].Value) ;
                detalle.cantidad = Convert.ToInt32(tabla[5, fila].Value.ToString());
                detalle.codigo = tabla[0, fila].Value.ToString();


                nuevoPedido.Children.Add(detalle);
            }
            bool task = _pedidoService.Insert(nuevoPedido);

            if (task)
            {
                HelperService.MessageBoxHelper.PedidoInsertOk();
            }
            else
            {
                HelperService.MessageBoxHelper.PedidoInsertError();
            }

        }

        

        private void limpiarControles(bool totales)
        {
            txtdescripcion.Text = "";
            txtlinea.Text = "";
            txtcolor.Text = "";
            txttalle.Text = "";
            txtstocktotal.Text = "";
            txtprecio.Text="";
            txtcupon.Text = "";
            txtlote.Text = "";
            


            if (totales)
            {
                txtPongotalle.Text = "";
                txtTotal.Text = "0";
                txtdescuento.Text = "0";
                txtsubtotal.Text = "0";
                cmbFormaPago.SelectedIndex = -1;
                tabla.DataSource = null;
                tabla.ClearSelection();
                tabla.Rows.Clear();

                tablapagos.DataSource = null;
                tablapagos.ClearSelection();
                tablapagos.Rows.Clear();

                txtImporteRecargo.Text = "";
                txtImporte.Text = "";
                txtRecargo.Text = "";


                
            }
            
            
        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tabla.SelectedCells.Count > 0)
            {

                if (e.ColumnIndex == 4)//el checkbox
                {

                    tabla.Rows[e.RowIndex].Cells[5].Value = HelperService.ConvertToDecimalSeguro(tabla.Rows[e.RowIndex].Cells[5].Value) * -1;
                    calcularSubTotal();
                    

                }
            }
        }

       

        private void tabla_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            calcularSubTotal();
            borrarTotal();
        }

        private void tabla_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            
            calcularSubTotal();
            borrarTotal();
            
        }

        private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFormaPago.SelectedIndex>-1)
            {
                decimal aumento;
                //aumento = ((formaPagoData)cmbFormaPago.SelectedItem).aumento;
                //txtRecargo.Text = aumento.ToString();
                txtImporte.Text = "";
                txtImporteRecargo.Text = "";
            }
            
        }

        

        private void calcularTotal()
        {

            decimal subt = HelperService.ConvertToDecimalSeguro(txtsubtotal.Text);





            List<decimal> listaRecargos = new List<decimal>();
            List<decimal> listaImportes = new List<decimal>();

            foreach (DataGridViewRow row in tablapagos.Rows)
            {
                listaRecargos.Add(HelperService.ConvertToDecimalSeguro(tablapagos[2, row.Index].Value));
                listaImportes.Add(HelperService.ConvertToDecimalSeguro(tablapagos[5, row.Index].Value));

            }

            decimal aux = subt;
            for (int i = 0; i < listaRecargos.Count; i++)
            {
                if (listaRecargos[i]>0)
                {
                    //resto el importe original
                    //aux-algo
                    aux = aux-listaImportes[i];
                    

                    //sumo el importe con recargo
                    //aux+algootro
                    aux+= (listaImportes[i] + ((listaImportes[i] * listaRecargos[i]) / 100));

                }
            }

            txtTotal.Text = aux.ToString();


            
            

           
                        
            
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void txtCodigo_Enter(object sender, EventArgs e)
        {

        }

        private void txtprecio_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void txtprecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '.' && (txtprecio.Text.IndexOf(".") > -1 || txtprecio.Text.IndexOf(",") > -1)) || (e.KeyChar == ',' && (txtprecio.Text.IndexOf(",") > -1 || txtprecio.Text.IndexOf(".") > -1))||(char.IsLetter(e.KeyChar)) )
            {
                e.Handled = true;
            }
            if (e.KeyChar=='\r')
            {
                agregarProducto();
            }
            
            
        }

        private void venta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString()=="F12")//f12
            {
                hacerVenta();
                txtinterno.Focus();
            }
            
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar.ToString() == "\r" && txtPongotalle.Text.Length == 20)
            //{
            //    txtprecio.Focus();
            //}
            if ((char.IsLetter(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedIndex>-1)
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

        //private void txtinterno_KeyPress(object sender, EventArgs e)
        //{
        //    if (char.IsLetter(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //}

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

        private bool convertidor(string value, string mensaje, out int number) {



            
            bool result = Int32.TryParse(value, out number);
            if (result)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Error ingresando el Codigo" + mensaje, "Alerta" , MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void txtinterno_KeyPress(object sender, KeyPressEventArgs e)
        {

                if (char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (txtinterno.Text.Length==12 && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (e.KeyChar.ToString() == "\r" && txtinterno.Text.Length>=12)
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
        }

        private void txtPongotalle_Leave(object sender, EventArgs e)
        {
            if (todovalido())
            {
                if (!HelperService.esCliente(GrupoCliente.CalzadosMell))
                {
                    cargarStock();
                    cargarPrecio();    
                }

                
            }
            else
            {
                MessageBox.Show("Complete correctamente los campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cargarPrecio()
        {
            var productoTalleService = new ProductoTalleService(new ProductoTalleRepository());
            var precioService = new PrecioService(new PrecioRepository());
            ProductoTalleData p = new ProductoTalleData();
            p.IDProducto = ((ProductoData)cmbArticulo.SelectedItem).ID;
            p.Talle = Convert.ToInt32(txtPongotalle.Text);
            decimal precio = precioService.GetPrecio(((listaPrecioData)cmbListaPrecios.SelectedItem).ID, (productoTalleService.GetIDByProductoTalle(p)));
            if (precio>0)
	{
		 txtprecio.Text = precio.ToString();
    }
            else
            {
                txtprecio.Text = "0";
            }
            
        }

        private void cargarStock()
        {
            var stockService = new StockService(new StockRepository());
            if ((ProductoData)cmbArticulo.SelectedItem!=null &&((ColorData)cmbColores.SelectedItem != null && Convert.ToInt32(txtPongotalle.Text) >0 ))
            {
                decimal stock = stockService.GetStockTotal(((ProductoData)cmbArticulo.SelectedItem).ID, ((ColorData)cmbColores.SelectedItem).ID, Convert.ToInt32(txtPongotalle.Text));
            if (stock<0)
	{
        txtstocktotal.Text = "-1";
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
                if (cmbProveedor.SelectedIndex==-1)
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
            }


            return valido;
        }

        private void txtdescuento_TextChanged(object sender, EventArgs e)
        {

            
        }

        private void txtdescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '.' && (txtdescuento.Text.IndexOf(".") > -1 || txtdescuento.Text.IndexOf(",") > -1)) || (e.KeyChar == ',' && (txtdescuento.Text.IndexOf(",") > -1 || txtdescuento.Text.IndexOf(".") > -1)) || (char.IsLetter(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

       

        private void txtdescuento_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtinterno_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            agregarPago();
            limpiarPago();

        }

        private void limpiarPago()
        {
            txtlote.Text = "";
            txtcupon.Text = "";
            txtImporte.Text = "0";
            txtRecargo.Text = "0";
            txtImporteRecargo.Text = "0";
            cmbFormaPago.SelectedIndex = -1;

        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '.' && (txtImporte.Text.IndexOf(".") > -1 || txtImporte.Text.IndexOf(",") > -1)) || (e.KeyChar == ',' && (txtImporte.Text.IndexOf(",") > -1 || txtImporte.Text.IndexOf(".") > -1)) || (char.IsLetter(e.KeyChar)))
            {
                e.Handled = true;
            }
            if (e.KeyChar == '\r')
            {
                agregarPago();
            }
            
        }

        private void agregarPago()
        {

            decimal importe;
            decimal importeR;
            if (cmbFormaPago.SelectedIndex > -1)
            {
                if (((FormaPagoData)cmbFormaPago.SelectedItem).ID==HelperService.idCC)
                {

                    tablapagos.DataSource = null;
                    tablapagos.ClearSelection();
                    tablapagos.Rows.Clear();

                    tablapagos.Rows.Add();
                    int fila;
                    fila = tablapagos.RowCount - 1;
                    //Codigo nombre  color talle subtotal
                    tablapagos[0, fila].Value = ((FormaPagoData)cmbFormaPago.SelectedItem).ID;
                    tablapagos[1, fila].Value = cmbFormaPago.Text;
                    tablapagos[2, fila].Value = "0";
                    tablapagos[3, fila].Value = txtlote.Text;
                    tablapagos[4, fila].Value = txtcupon.Text;
                    tablapagos[5, fila].Value = txtsubtotal.Text;
                    tablapagos[6, fila].Value = txtsubtotal.Text;
                    button3.Enabled = false;
                }
                else
                {

                
                    try
                    {
                        importe = HelperService.ConvertToDecimalSeguro(txtImporte.Text);
                        importeR = HelperService.ConvertToDecimalSeguro(txtImporteRecargo.Text);


                        tablapagos.Rows.Add();
                        int fila;
                        fila = tablapagos.RowCount - 1;
                        //Codigo nombre  color talle subtotal
                        tablapagos[0, fila].Value = ((FormaPagoData)cmbFormaPago.SelectedItem).ID;
                        tablapagos[1, fila].Value = cmbFormaPago.Text;
                        tablapagos[2, fila].Value = txtRecargo.Text;
                        tablapagos[3, fila].Value = txtlote.Text;
                        tablapagos[4, fila].Value = txtcupon.Text;
                        tablapagos[5, fila].Value = importe.ToString();
                        tablapagos[6, fila].Value = importeR.ToString();



                        calcularTotal();

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Debe de ingresar un Monto valido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe de seleccionar una foma de pago", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            txtRecargo.Text = "";
            txtImporte.Text = "";
            txtImporteRecargo.Text = "";
            cmbFormaPago.SelectedIndex = -1;
        }

        private void txtdescuento_Leave_1(object sender, EventArgs e)
        {


            decimal descuento = 0;
            if (txtdescuento.Text != "" && txtdescuento.Text != "." && txtdescuento.Text != ",")
            {
                descuento = HelperService.ConvertToDecimalSeguro(txtdescuento.Text);
            }
            else
            {
                MessageBox.Show("El descuento debe ser entre 0 y 100", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdescuento.Text = "0";
            }

            if (!(descuento < 100 && descuento >= 0))
            {
                MessageBox.Show("El descuento debe ser entre 0 y 100", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdescuento.Text = "0";
            }
            calcularTotal();
        }

        private void txtdescuento_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void txtImporte_Leave(object sender, EventArgs e)
        {
            if (txtRecargo.Text!="")
            {
                calcularImporteRecargo(txtImporte.Text, txtRecargo.Text);    
            }
            
        }

        private void calcularImporteRecargo(string importe, string recargo)
        {

            decimal aumento = HelperService.ConvertToDecimalSeguro(recargo);
            if (importe!="")
            {
                decimal subt = HelperService.ConvertToDecimalSeguro(importe);
                txtImporteRecargo.Text = (subt + ((subt * aumento) / 100)).ToString();
            }
            else
            {
                txtImporteRecargo.Text = "0";
            }
            
        }

        private void tabla_RowsRemoved_1(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            
                calcularSubTotal();    
            
            
        }

        private void txtsubtotal_TextChanged(object sender, EventArgs e)
        {



            calcularTotal();
        }

        private void tablapagos_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

            if (tablapagos.Rows.Count==0)
            {//pq saque el pago de cc
                button3.Enabled = true;
            }

            calcularTotal();
        }

        private void txtdescuento_Leave(object sender, EventArgs e)
        {
           
        }

        private void txtdescuento_TextChanged_3(object sender, EventArgs e)
        {

        }

        private void txtdescuento_Leave_2(object sender, EventArgs e)
        {
            calcularSubTotal();
        }

        private void cmbColores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabla_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            
            if (tabla.Rows.Count>0)
            {
                calcularCantidad(e.RowIndex);    
            }
            
            
        }

        private void calcularCantidad(int rowIndex)
        {
            
            tabla[6, rowIndex].Value = HelperService.ConvertToDecimalSeguro(tabla[4, rowIndex].Value.ToString()) * Convert.ToInt32(tabla[5, rowIndex].Value.ToString());
            calcularSubTotal();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

      

  



                
    }
}