using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

using DTO;
using DTO.BusinessEntities;
using Repository.ColoresRepository;
using Repository.Repositories.FormaPagoRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ValeRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.ColorService;
using Services.FormaPagoService;
using Services.ProductoService;
using Services.TributoService;
using Services.ValeService;
using Services.VentaService;

namespace SharedForms.Ventas
{
    public partial class ventaMayor : ventaBase
    {
        public ventaMayor()
        {
            InitializeComponent();
            var productoService = new ProductoService(new ProductoRepository());
            productosTodos = productoService.GetAll(true);
        }
        
		private List<ProductoData> productosTodos;
        internal override void agregarArticulo(StockData s, int cmbArticuloIndex, int cmbColorIndex, string talle, string precio)
        {
            if (validarDetalle(s.Codigo, cmbArticuloIndex, cmbColorIndex, talle, HelperService.ConvertToDecimalSeguro(precio).ToString()))
            {
                decimal alicuota = 0;
                if (HelperService.esCliente(GrupoCliente.Slipak))
                {
                    alicuota = 21;
                }
                
                AgregarArticulo(s, tabla, HelperService.ConvertToDecimalSeguro(precio).ToString(), "0",  1, alicuota);
                CalcularTotales();
                
                
                limpiarControles(false);
            }

            if (tabla.Rows.Count > 0)
            {
                cmbListaPrecios.Enabled = false;
            }
        }

    
		private void venta_Load(object sender, EventArgs e)
        {


            SetControls(txtNeto, txtSubtotal, txtUnidadTotal, txtiva, txtTotal, txtDescuento, txtRecargos,txtComentario,
                txtTributoAlicuota,txtTributoImporte,txtTributoBase,radioAlicuota,cmbTributo,
                
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
                dateTimePicker1.Enabled = true;
                lblTotal.Text = "Total";
            }
            if (HelperService.esCliente(GrupoCliente.Slipak))
            {
                this.Height = 666;
            }
            btnCC.Visible = HelperService.esCliente(GrupoCliente.Slipak);
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
                txtSubtotal.Text = "0";
            }
        }
        
		private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
		    tablaContentClick(sender, e);
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

        
		
  
		private void tabla_RowsRemoved_1(object sender, DataGridViewRowsRemovedEventArgs e)
		{
		    tablaRowsRemoved(sender, e);

		}
        
		private void tablapagos_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (tablapagos.Rows.Count == 0)
            {//pq saque el pago de cc
                button3.Enabled = true;
            }

		    //calcularRecargo(tablapagos, txtRecargos);//todo! esto!


            CalcularTotales();
     
        }

        
       
        
		private void tabla_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            tablaCellEndEdit(sender,e);
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
        
		
        
		private void tabla_KeyPress(object sender, KeyPressEventArgs e)
		{
		    tablakeyPress(sender, e);
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
            if (ValidoTributo(txtTributoImporte.Text))
            {
                AgregoTributo();
                txtTributoImporte.Text = "";


                CalcularTotales();
            }
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
            HelperService.VerificoTextBoxNumerico(txtTributoImporte, e);
            //if (e.Handled)//todo! ver este if
            //{
            //    txtTributoAlicuota.Text = "0";
            //}
            
        }

        private void tablaTributos_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            //txtRecargos.Text = calcularTributo(tablaTributos).ToString(); // todo! ver esto
            CalcularTotales();
        }

        private void radioAlicuota_CheckedChanged(object sender, EventArgs e)
        {
            TributoRadioButton();
        }

        private void radioImporte_CheckedChanged(object sender, EventArgs e)
        {
            TributoRadioButton();
        }

        private void txtTributoAlicuota_TextChanged(object sender, EventArgs e)
        {
            CalcularTributoImporte();
        }

        private void txtTributoImporte_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTributoAlicuota_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtTributoAlicuota, e);
        }

       
    }
}