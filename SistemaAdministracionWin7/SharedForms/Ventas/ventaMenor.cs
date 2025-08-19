using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ValeRepository;
using Repository.Repositories.VentaRepository;
using Services;

using Services.ProductoService;
using Services.ValeService;
using Services.VentaService;

namespace SharedForms.Ventas
{
    public partial class ventaMenor : ventaBase
    {
        public ventaMenor()
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
                AgregarArticulo(s, tabla, HelperService.ConvertToDecimalSeguro(precio).ToString(), "0", 1, alicuota);
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

            SetControls(txtNeto,txtsubtotal,txtUnidadTotal,txtiva,txtTotal,txtdescuento,txtRecargos,txtComentario,
                new TextBox(), new TextBox(), new TextBox(), new RadioButton(), new ComboBox(), 
                tabla,new DataGridView(), tablapagos,
                cmbclase,cmbClientes,cmbVendedor,cmbListaPrecios,new ComboBox(), 
                dateTimePicker1,new DateTimePicker());

            cargarClase();
            cargarListaPrecios();
            cargarClientes();
            lblnumerofactura.Text = obtenerNumeroFactura();
            cargarVendedores();

            setClientGUI();
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
                txtdescuento.Text = "0";
                txtsubtotal.Text = "0";
                txtNeto.Text = "0";
            }
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
            HelperService.VerificoTextBoxNumerico(txtdescuento, e);
            
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

   
        

        
        
        
		private void cmbclase_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeteoAlicuotas(tabla, cmbclase.Text.ToLower());
            foreach (DataGridViewRow row in tabla.Rows)
            {
                calcularCantidad(row.Index, tabla);
            }
            CalcularTotales();
        }
    
        
	
     
        
		private void txtdescuento_TextChanged(object sender, EventArgs e)
        {
            if (txtdescuento.Text == "" || HelperService.ConvertToDecimalSeguro(txtdescuento.Text)>100)
            {
                txtdescuento.Text = "0";
            }

            txtTotal.Text = CalcularTotal(tabla,new DataGridView(),  HelperService.ConvertToDecimalSeguro(txtdescuento.Text)).ToString();
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

       

       
        private void tablapagos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //valido que lote y cupon no sean cualquier cosa.


        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tablaContentClick(sender,e);
        }

        private void tabla_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            tablaCellEndEdit(sender,e);
        }

        private void tabla_KeyPress(object sender, KeyPressEventArgs e)
        {
            tablakeyPress(sender, e);
        }

        private void tabla_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            tablaRowsRemoved(sender, e);
        }

    }
}