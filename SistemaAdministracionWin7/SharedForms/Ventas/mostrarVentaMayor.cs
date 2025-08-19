using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

using DTO;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.ColoresRepository;
using Repository.Repositories.FormaPagoRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.StockRepository;
using Repository.Repositories.ValeRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.ColorService;
using Services.FormaPagoService;
using Services.ProductoService;
using Services.StockService;
using Services.TributoService;
using Services.ValeService;
using Services.VentaService;

namespace SharedForms.Ventas
{
    public partial class mostrarventaMayor : ventaBase
    {

        private VentaData _ventaAnular = null;
        public mostrarventaMayor()
        {
            InitializeComponent();
            var productoService = new ProductoService(new ProductoRepository());
            productosTodos = productoService.GetAll(true);
        }

        public mostrarventaMayor(VentaData v)
        {
            
            InitializeComponent();
            _ventaAnular = v;
            

        }

        
		private List<ProductoData> productosTodos;
        internal void agregarArticulo(StockData s, int cmbArticuloIndex, int cmbColorIndex, string talle, string precio)
        {
            if (validarDetalle(s.Codigo, cmbArticuloIndex, cmbColorIndex, talle, HelperService.ConvertToDecimalSeguro(precio).ToString()))
            {
                decimal alicuota = 0;
                if (HelperService.esCliente(GrupoCliente.Slipak))
                {
                    alicuota = 21;
                }
                
                AgregarArticulo(s, tabla, HelperService.ConvertToDecimalSeguro(precio).ToString(), "0",  1, alicuota);

                //txtsubtotal.Text = CalcularSubTotal(tabla).ToString();
                CalcularTotales();
                
                
                limpiarControles(false);
            }

            if (tabla.Rows.Count > 0)
            {
                cmbListaPrecios.Enabled = false;
            }
        }

        private void CalcularTotales()
        {
            txtNeto.Text = CalcularSubTotal(tabla).ToString();
            txtUnidadTotal.Text = CalcularCantidadTotal(tabla);
            txtiva.Text = calcularIva(tabla).ToString();
            txtRecargos.Text = calcularTributo(tablaTributos).ToString();
            txtTotal.Text = CalcularTotal(tabla, tablaTributos).ToString();
        }
        
		private void venta_Load(object sender, EventArgs e)
        {

            SetControls(txtNeto, new TextBox(), txtUnidadTotal, txtiva, txtTotal, new TextBox(), txtRecargos, txtComentario,
                new TextBox(), new TextBox(), new TextBox(), new RadioButton(), new ComboBox(), 
                 tabla, tablaTributos, tablapagos,

                 cmbclase, cmbClientes, cmbVendedor, cmbListaPrecios, new ComboBox(), 
                 dateTimePicker1, new DateTimePicker());
            cargarClase();
            cargarListaPrecios();
            cargarClientes();
            lblnumerofactura.Text = obtenerNumeroFactura();
            cargarVendedores();
		    CargarTributos();
            setClientGUI();



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
                               ObjectDumperExtensions.DumpToString(_ventaAnular, "MostrarVentaMayor"), true, true);

            }
            
        }



        private void CargarTributos()
        {
            var tributoService = new TributoService();

            cmbTributo.DataSource = tributoService.GetAll();
            cmbTributo.DisplayMember = "Description";

        }



        private void setClientGUI()
        {
            if (HelperService.haymts)
            {
                tabla.Columns[3].HeaderText = "Mts";
                tabla.Columns[7].Visible = true;
            }
            else
            {
                tabla.Columns[7].Visible = false;
            }
            if (HelperService.talleUnico)
            {
                tabla.Columns[3].Visible = false;
                lblTotal.Text = "Unidades";
            }

            if (HelperService.esCliente(GrupoCliente.Slipak))
            {
                dateTimePicker1.Enabled = true;
                lblTotal.Text = "Total";
            }
            if (HelperService.esCliente(GrupoCliente.Slipak))
            {
                this.Height = 666;
            }
            btnCC.Visible = HelperService.esCliente(GrupoCliente.Slipak);


           
        }


        private void cargarVenta(VentaData v)
        {

            var stockService = new StockService(new StockRepository());
            lblnumerofactura.Text = v.NumeroCompleto;

            //txtdescuento.Text = v.descuento.ToString();

            txtNeto.Text = v.Monto.ToString();

            txtiva.Text = v.IVA.ToString();


            cmbVendedor.Text = v.Vendedor.NombreContacto;
            cmbClientes.Text = v.Cliente.RazonSocial;
            tabla.Enabled = true;
            tabla.ReadOnly = true;
            dateTimePicker1.Value = v.Date;
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

                AgregarArticulo(s, tabla, vd.PrecioUnidad.ToString(), "0", vd.Cantidad, vd.Alicuota,vd.Bonificacion);
                calcularCantidad(tabla.RowCount - 1, tabla);

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

            //Tributos.

            var tributoService = new TributoService();
            TributoData tr;
            foreach (TributoNexoData tributo in v.Tributos)
            {

                tr = tributoService.GetByID(tributo.Tributo.ID);
                AgregarTributoATabla(tr,tributo.Base,tributo.Alicuota,tributo.Importe,tablaTributos);
                


            }
            txtRecargos.Text = calcularTributo(tablaTributos).ToString();
            CalcularTotales();



        }
        private void bloquearSianulada()
        {
            button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (!HelperService.validarTrial(DateTime.Today.Date, HelperService.IDLocal, ConfigurationManager.AppSettings["UpdateNet"]))
            {
                MessageBox.Show("Se alcanzo el nro maximo de ventas para la version trial del sistema.\nComuniquese con el adminsitrador para obtener un upgrade", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                hacerVenta();    
            }
        }
        
		private void hacerVenta()
        {
            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dg == DialogResult.OK)
            {
                if (validarVenta())
                {
                    procesarVenta();
                    lblnumerofactura.Text = obtenerNumeroFactura();
                    actualizarVentas();
                    limpiarControles(true);
                }
            }
        }
        
		private void procesarVenta()
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            VentaData nuevaVenta = new VentaData();
            nuevaVenta = cargarVenta();
            bool taskDone = ventaService.Insert(nuevaVenta);
            if (taskDone)
            {
                if (nuevaVenta.Cambio && nuevaVenta.Monto < 0)
                {
                    generarVale(nuevaVenta);
                }
                MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
		private void generarVale(VentaData venta)
		{

		    var valeService = new ValeService(new ValeRepository());
            int task = -1;


		    task = valeService.GenerarNuevo(venta);
            
            if (task>0)
            {
                MessageBox.Show("Vale correctament Generado \r\n Por El Monto de " + venta.Monto.ToString() + "\r\n Numero de vale: " +task.ToString(), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
		private void limpiarControles(bool totales)
        {
            
            if (totales)
            {
                tabla.DataSource = null;
                tabla.ClearSelection();
                tabla.Rows.Clear();
                tablapagos.DataSource = null;
                tablapagos.ClearSelection();
                tablapagos.Rows.Clear();
                dateTimePicker1.Value = DateTime.Now;
                txtComentario.Text = "";
                txtTotal.Text = "0";
                txtRecargos.Text = "0";
                tablaTributos.Rows.Clear();
                tablaTributos.ClearSelection();
                txtNeto.Text = "0";
            }
        }
        
		private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 12)
            {
                tabla[7, e.RowIndex].Value = (HelperService.ConvertToDecimalSeguro(tabla[7, e.RowIndex].Value) * -1).ToString();
            }
            if (e.RowIndex>0)
                ActualizarRow(e.RowIndex, e.ColumnIndex);
        }
        
	
        
		private void venta_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode.ToString() == "F12")//f12
            {
                if (!HelperService.validarTrial(DateTime.Today.Date, HelperService.IDLocal, ConfigurationManager.AppSettings["UpdateNet"]))
                {
                    MessageBox.Show("Se alcanzo el nro maximo de ventas para la version trial del sistema.\nComuniquese con el adminsitrador para obtener un upgrade", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    hacerVenta();
                }
            }
            if (e.KeyCode.ToString() == "F3") //f12
            {
                padreBase.AbrirForm(new agregarFormaPago(), this.MdiParent, false, FormStartPosition.CenterScreen);
            }
            if (e.KeyCode.ToString() == "F4") //f12
            {
                padreBase.AbrirForm(new agregarArticulo(
                ((listaPrecioData)cmbListaPrecios.SelectedItem).ID
                ), this.MdiParent, false, FormStartPosition.CenterScreen);
            }


        }
        
		private void txtdescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((e.KeyChar == '.' && (txtdescuento.Text.IndexOf(".") > -1 || txtdescuento.Text.IndexOf(",") > -1)) || (e.KeyChar == ',' && (txtdescuento.Text.IndexOf(",") > -1 || txtdescuento.Text.IndexOf(".") > -1)) || (char.IsLetter(e.KeyChar)))
            //{
            //    e.Handled = true;
            //}
        }
        
		
        
		private void txtdescuento_Leave_1(object sender, EventArgs e)
        {
            //decimal descuento = 0;
            //if (txtdescuento.Text != "" && txtdescuento.Text != "." && txtdescuento.Text != ",")
            //{
            //    descuento = HelperService.ConvertToDecimalSeguro(txtdescuento.Text);
            //}
            //else
            //{
            //    MessageBox.Show("El descuento debe ser entre 0 y 100", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtdescuento.Text = "0";
            //}
            //if (!(descuento < 100 && descuento >= 0))
            //{
            //    MessageBox.Show("El descuento debe ser entre 0 y 100", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtdescuento.Text = "0";
            //}
            //txtTotal.Text = calcularTotal(txtsubtotal.Text, descuento.ToString()).ToString();
        }
        
		private void tabla_RowsRemoved_1(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalcularTotales();
            if (tabla.Rows.Count == 0)
            {
                cmbListaPrecios.Enabled = true;
            }
            
        }
        
		private void tablapagos_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (tablapagos.Rows.Count == 0)
            {//pq saque el pago de cc
                button3.Enabled = true;
            }

		    calcularRecargo(tablapagos, txtRecargos);


            CalcularTotales();
     
        }

        
        bool IsDigitsOnly(string str)
        {
            double ignoreMe;
            return double.TryParse(HelperService.replace_decimal_separator(str), out ignoreMe);
        }
        
		private void tabla_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                
                if (!IsDigitsOnly(tabla[7, e.RowIndex].Value.ToString()))
                {
                    tabla[7, e.RowIndex].Value = "1";
                }
                if (!IsDigitsOnly(tabla[6, e.RowIndex].Value.ToString()))
                {
                    tabla[6, e.RowIndex].Value = "0";
                }
                tabla[12, e.RowIndex].Value = !(HelperService.ConvertToDecimalSeguro(tabla[7, e.RowIndex].Value) > 0);

                ActualizarRow(e.RowIndex, e.ColumnIndex);
            }
        }
       
        
		
        
		private void cmbclase_SelectedIndexChanged(object sender, EventArgs e)
        {
		    SeteoAlicuotas(tabla, cmbclase.Text.ToLower());
            foreach (DataGridViewRow row in tabla.Rows)
            {
                calcularCantidad(row.Index, tabla);
            }
            CalcularTotales();
        }
        
		private void ActualizarRow(int RowIndex, int ColumnIndex)
        {
            calcularCantidad(RowIndex, tabla);
            CalcularTotales();
        }
        
		private void tabla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        
		private void txtdescuento_TextChanged(object sender, EventArgs e)
        {
            //if (txtdescuento.Text == "" || HelperService.ConvertToDecimalSeguro(txtdescuento.Text)>100)
            //{
            //    txtdescuento.Text = "0";
            //}

            //txtTotal.Text = calcularTotal(txtsubtotal.Text, txtdescuento.Text).ToString();
        }
        
		private void button5_Click(object sender, EventArgs e)
        {
            padreBase.AbrirForm(new agregarArticulo(
                ((listaPrecioData)cmbListaPrecios.SelectedItem).ID
                ), this.MdiParent, false, FormStartPosition.CenterScreen);
        }
        
		private void groupBox2_Enter(object sender, EventArgs e)
        {
        }
        internal void agregarPago(Guid idformapago,string descripcion, int cuota, string lote, string cupon, string recargo, string importe, string ImporteRecargo)
        {
            decimal grlRecargo = 0;
            tablapagos.Rows.Add();
            int fila;
            fila = tablapagos.RowCount - 1;
            //Codigo nombre  color talle subtotal
            tablapagos[0, fila].Value = idformapago;
            tablapagos[1, fila].Value = descripcion;//+  "-" +cuota.ToString() +" Cuotas";
            tablapagos[2, fila].Value = cuota;
            tablapagos[3, fila].Value = recargo;
            tablapagos[4, fila].Value = lote;
            tablapagos[5, fila].Value = cupon;
            tablapagos[6, fila].Value = importe;
            tablapagos[7, fila].Value = ImporteRecargo;
            

            calcularRecargo(tablapagos,txtRecargos);
            
           CalcularTotales();
        }

        
		private void button3_Click_1(object sender, EventArgs e)
        {
            padreBase.AbrirForm(new agregarFormaPago(txtTotal.Text), this.MdiParent, false, FormStartPosition.CenterScreen);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tablapagos.DataSource = null;
            tablapagos.ClearSelection();
            tablapagos.Rows.Clear();
            InsertCtaCte();
        }

        private void InsertCtaCte()
        {
            tablapagos.Rows.Add();

            var formaPagoService = new FormaPagoService(new FormaPagoRepository(), new FormaPagoCuotasRepository());
            FormaPagoData cc = formaPagoService.GetAll(false).Find(c => c.Description.ToLower().StartsWith("c"));


            int fila;
            fila = tablapagos.RowCount - 1;
            //Codigo nombre  color talle subtotal
            tablapagos[0, fila].Value = cc.ID;
            tablapagos[1, fila].Value = "Cta Cte";
            tablapagos[2, fila].Value = "0";
            tablapagos[3, fila].Value = "0";
            tablapagos[4, fila].Value = "0";
            tablapagos[5, fila].Value = "0";
            tablapagos[6, fila].Value = txtTotal.Text;
            tablapagos[7, fila].Value = txtTotal.Text;
        }

        private void tablapagos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //valido que lote y cupon no sean cualquier cosa.


        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void txtdescuento_KeyPress_1(object sender, KeyPressEventArgs e)
        {
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

     

        private bool ValidoTributo(string valor)
        {
            if (valor == "" || valor == "." || valor == ",")
            {
                MessageBox.Show("Debe de ingresar un Valor", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (HelperService.ConvertToDecimalSeguro(valor) <= 0)
            {
                MessageBox.Show("Debe de ingresar un valor mayor a cero", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void txtTributo_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void tablaTributos_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            txtRecargos.Text = calcularTributo(tablaTributos).ToString();
            CalcularTotales();
        }
    }
}