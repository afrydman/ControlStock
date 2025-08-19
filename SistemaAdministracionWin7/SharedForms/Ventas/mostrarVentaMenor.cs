using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO;
using DTO.BusinessEntities;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
using ObjectDumper;
using Repository.ClienteRepository;
using Repository.Repositories.CajaRepository;
using Repository.Repositories.FormaPagoRepository;
using Repository.Repositories.ListaPrecioRepository;
using Repository.Repositories.PersonalRepository;
using Repository.Repositories.StockRepository;
using Repository.Repositories.UsuarioRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.CajaService;
using Services.ClienteService;
using Services.FormaPagoService;
using Services.ListaPrecioService;
using Services.PersonalService;
using Services.StockService;
using Services.UsuarioService;
using Services.VentaService;

namespace SharedForms.Ventas
{
    public partial class mostrarVentaMenor : ventaBase
    {

         public VentaData _ventaAnular { get; set; }
         public mostrarVentaMenor()
        {

            InitializeComponent();
        }
         public mostrarVentaMenor(VentaData v)
        {
            
            InitializeComponent();
            _ventaAnular = v;
            

        }




         private void CalcularTotales()
         {
             txtNeto.Text = CalcularSubTotal(tabla).ToString();
             txtUnidadTotal.Text = CalcularCantidadTotal(tabla);
             txtiva.Text = calcularIva(tabla).ToString();
             txtTotal.Text = CalcularTotal(tabla, new DataGridView()).ToString();
         }
        private void cargarVenta(VentaData v) {

            var stockService = new StockService(new StockRepository());
            lblnumerofactura.Text = v.NumeroCompleto;
            
            txtdescuento.Text = v.Descuento.ToString();
            
            txtsubtotal.Text = v.Monto.ToString();

            txtiva.Text = v.IVA.ToString();

            
            cmbVendedor.Text = v.Vendedor.NombreContacto;
            cmbClientes.Text = v.Cliente.RazonSocial;
            tabla.Enabled = true;
            tabla.ReadOnly = true;
            fecha.Value = v.Date;
            cmbclase.Text = v.ClaseDocumento.ToString();
            txtComentario.Text = v.Description; 
            StockData s;
             foreach (VentaDetalleData vd in v.Children)
            {
                s = stockService.obtenerProducto(vd.Codigo);
                

                if (!HelperService.validarCodigo(s.Codigo))
                {
                    s = new stockDummyData(vd.Codigo);
                }

                AgregarArticulo(s, tabla,vd.PrecioUnidad.ToString(),"0",vd.Cantidad,vd.Alicuota,vd.Bonificacion);
                
            }


             var formaPagoService = new FormaPagoService(new FormaPagoRepository(), new FormaPagoCuotasRepository());
             foreach (PagoData f in v.Pagos)
            {

                tablapagos.Rows.Add();
                int fila;
                fila = tablapagos.RowCount - 1;


                if (f.FormaPago.Description == "" || f.FormaPago.Description == null)
                {
                    f.FormaPago = formaPagoService.GetByID(f.FormaPago.ID);
                }
                tablapagos[1, fila].Value = f.FormaPago.Description;
                tablapagos[2, fila].Value = f.Cuotas;

                tablapagos[3, fila].Value = f.Recargo;
                tablapagos[4, fila].Value = f.Lote;
                tablapagos[5, fila].Value = f.Cupon;
                tablapagos[6, fila].Value = f.Importe;
                tablapagos[7, fila].Value = (f.Importe + ((f.Importe * f.Recargo) / 100)).ToString();

            }
            calcularRecargo(tablapagos,txtRecargos);
            CalcularTotales();
            
            cmbClientes.Enabled = false;
           
        
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
                if (listaRecargos[i] > 0)
                {
                    //resto el importe original
                    //aux-algo
                    aux = aux - listaImportes[i];


                    //sumo el importe con recargo
                    //aux+algootro
                    aux += (listaImportes[i] + ((listaImportes[i] * listaRecargos[i]) / 100));

                }
            }

            txtTotal.Text = aux.ToString();
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
            if (tablapagos.Rows.Count == 0)//que haya articulos
            {
                MessageBox.Show("Seleccione una forma de pago", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            decimal aux = 0;
            foreach (DataGridViewRow row in tablapagos.Rows)
	        {
		        aux+=HelperService.ConvertToDecimalSeguro(tablapagos[5,row.Index].Value);
	        }
            if (HelperService.ConvertToDecimalSeguro(txtTotal.Text)!=aux)
            {
                MessageBox.Show("La suma de formas de pago debe de coincidir con el importe Total", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        
        
        }
        

        private void borrarTotal()
        {
            txtTotal.Text = "";
          
        }

        private void calcularSubTotal()
        {
            decimal subt = 0;
            decimal descuento = 0;

            for (int i = 0; i < tabla.Rows.Count; i++)
			{
                subt += HelperService.ConvertToDecimalSeguro(tabla[5, i].Value);
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

       

        private void venta_Load(object sender, EventArgs e)
        {




            SetControls(txtNeto, new TextBox(), txtUnidadTotal, txtiva, txtTotal, new TextBox(), txtRecargos, txtComentario,
                new TextBox(), new TextBox(), new TextBox(), new RadioButton(), new ComboBox(),
                 tabla, new DataGridView(), tablapagos,

                 cmbclase, cmbClientes, cmbVendedor, cmbListaPrecios, new ComboBox(),
                 fecha, new DateTimePicker());
          
            cargarListaPrecios();
            cargarClientes();
            cargarClases();

        
            cargarVendedores();
            cargarFormasPago();
            setClientGUI();
            if (HelperService.haymts)
            {
               
                tabla.Columns[3].HeaderText = "Mts";
                lblTotal.Text = "Mts";


            }
            else
            {
                tabla.Columns[7].Visible = false;
            }


            try
            {
                if (_ventaAnular != null)
                {
                    cargarVenta(_ventaAnular);
                    if (!_ventaAnular.Enable || _ventaAnular.Prefix != HelperService.Prefix)
                    {
                        bloquearSianulada();
                    }
                }
            }
            catch (Exception ee)
            {
                HelperService.writeLog(ee.ToString(), true, true);
                HelperService.writeLog(
                               ObjectDumperExtensions.DumpToString(_ventaAnular, "MostrarVentaMenor"), true, true);

            }
            
        }

        private void setClientGUI()
        {
            if (HelperService.haymts)
            {
                tabla.Columns[3].HeaderText = "Mts";
                tabla.Columns[7].Visible = true;
                tabla.Columns[8].Visible = true;
            }
            else
            {
                tabla.Columns[8].Visible = false;
            }
            if (HelperService.talleUnico)
            {
                tabla.Columns[3].Visible = false;
                lblTotal.Text = "Unidades";
            }

            if (HelperService.esCliente(GrupoCliente.Slipak))
            {
               // dateTimePicker1.Enabled = true;
                lblTotal.Text = "Total";
            }
            if (HelperService.esCliente(GrupoCliente.Slipak))
            {
                this.Height = 666;
            }
            //btnCC.Visible = HelperService.esCliente(GrupoCliente.Slipak);
           
            
        }


        private void cargarClases()
        {
            List<string> clases = new List<string>();
            clases.Add("A");
            clases.Add("B");
            clases.Add("C");

            cmbclase.DataSource = clases;
        }

        private void bloquearSianulada()
        {
            button2.Enabled = false;
        }

        private void cargarClientes()
        {
            var ClienteService = new ClienteService(new ClienteRepository());

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

       
        private void cargarFormasPago()
        {
            

            

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
            
            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dg == DialogResult.OK)
            {
                bool needPass = false;
                string resultado = "";
                bool task = false;

                var cajaService = new CajaService(new CajaRepository());


                if (cajaService.IsClosed(DateTime.Now,HelperService.IDLocal))
                {
                    needPass = true;
                    helperForms.InputBox("Alerta", "Ingrese la contrasena para confirmar la operacion", ref resultado);

                }
                var usuarioService = new UsuarioService(new UsuarioRepository());
            
                if (needPass)
                {
                    if (usuarioService.VerificarPermiso(resultado))
                    {
                        task = anularVenta();
                        
                    }
                    else
                    {
                        MessageBox.Show("La contrasena es erronea, intente nuevamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                }
                else
                {
                    task = anularVenta();
                    
                }

                if (task)
                {
                    MessageBox.Show("Actualizado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    actualizarVentas();
                    bloquearSianulada();
                }
                else
                {
                    MessageBox.Show("Ocurrio un Error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }
        private bool anularVenta()
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            return ventaService.Disable(_ventaAnular.ID, true);

        }

       

       

      

        private VentaData cargarVenta()
        {
            VentaData nuevaVenta = new VentaData();
            nuevaVenta.Date = DateTime.Now;



            //List<formasPagoCuotasData> formas = new List<formasPagoCuotasData>();

            //formasPagoCuotasData nforma;
            //foreach (DataGridViewRow f  in tablapagos.Rows)
            //{
            //    nforma = new formasPagoCuotasData();
            //    nforma.ID = new Guid(tablapagos[0, f.Index].Value.ToString());
            //    nforma.aumento = HelperService.ConvertToDecimalSeguro(tablapagos[2, f.Index].Value.ToString());
            //    nforma.importe = HelperService.ConvertToDecimalSeguro(tablapagos[5, f.Index].Value.ToString());
            //    nforma.lote = tablapagos[3, f.Index].Value.ToString();
            //    nforma.cupon =tablapagos[4, f.Index].Value.ToString();

            //    formas.Add(nforma);
            //}
            //nuevaVenta.formasdepago = formas;

            if (txtdescuento.Text == "" || txtdescuento.Text == "." || txtdescuento.Text == ",")
                txtdescuento.Text = "0";
            nuevaVenta.Descuento = HelperService.ConvertToDecimalSeguro(txtdescuento.Text);
            
            

            LocalData l = new LocalData();
            l.ID = HelperService.IDLocal;
            nuevaVenta.Local = l;

            PersonalData p = new PersonalData();
            p = ((PersonalData)cmbVendedor.SelectedItem);
            nuevaVenta.Vendedor = p;
            nuevaVenta.Numero = Convert.ToInt32(lblnumerofactura.Text);
            nuevaVenta.Cliente = ((ClienteData)cmbClientes.SelectedItem);
            
            nuevaVenta.Monto = HelperService.ConvertToDecimalSeguro(txtsubtotal.Text);

            


            return nuevaVenta;

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

        }

        

        

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void txtCodigo_Enter(object sender, EventArgs e)
        {
            
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            
           
        }

        private void txtprecio_TextChanged(object sender, EventArgs e)
        {
         
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

        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {

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

        private void button4_Click(object sender, EventArgs e)
        {
//            venta.imprimir(venta.impresiones.factura, _ventaAnular.ID);

        }

        private void button5_Click(object sender, EventArgs e)
        {
           // venta.imprimir(venta.impresiones.remito, _ventaAnular.ID);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

  



                
    }
}