using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Central.GenericForms;
using DTO.BusinessEntities;
using Repository.ColoresRepository;
using Repository.Repositories.ComprasProveedoresRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.StockRepository;
using Services;
using Services.ColorService;
using Services.ComprasProveedorService;
using Services.ProveedorService;
using Services.StockService;
using SharedForms.Ventas;

namespace Central.Proveedores
{
    public partial class mostrarComprasCompleto : ventaBase
    {
        public mostrarComprasCompleto(ComprasProveedoresData c)
        {
            _compra = c;
            InitializeComponent();
        }

       
        ComprasProveedoresData _compra = null;
        private void mostrarCOmprasCompleto_Load(object sender, EventArgs e)
        {

            SetControls(txtNeto, txtSubtotal, new TextBox(), txtIva, txtTotal, txtDescuento, new TextBox(), 
                tabla, new DataGridView(), tablapagos,
                cmbclase, cmbClientes, cmbVendedor, cmbListaPrecios, dateTimePicker1);

            cargarProveedores();
            cargarClase(cmbClase);

            if (_compra != null)
                cargarCompra(_compra);
            
            if (HelperService.haymts)
            {
                lbltalle.Text = "Mts";

                tabla.Columns[3].HeaderText = "Mts";
                tabla.Columns[3].ReadOnly = true;
            }
        }
        private void cargarCompra(ComprasProveedoresData _compra)
        {
            var comprasProveedoresService = new ComprasProveedorService(new ComprasProveedoresRepository(),
                   new CompraProveedoresDetalleRepository());

            var stockService = new StockService(new StockRepository());
            cmbClase.Text = _compra.ClaseDocumento.ToString();
            txtObs.Text = _compra.Description;
            txtTotal.Text = _compra.Monto.ToString();
            

            lblnum.Text = _compra.NumeroCompleto;
            fechaContable.Value = _compra.Date;
            fechaFactura.Value = _compra.FechaFactura;
           
            txtDescuento.Text = _compra.Descuento.ToString();
            var proveedorService = new ProveedorService(new ProveedorRepository());
            bool setcmb = false;
            StockData s;
            foreach (ComprasProveedoresdetalleData item in _compra.Children)
            {
                s = stockService.obtenerProducto(item.Codigo);
                if (!HelperService.validarCodigo(s.Codigo))
                {
                    s = new stockDummyData(item.Codigo);

                }
                else
                {
                    if (!setcmb)
                    {

                        ProveedorData pp = proveedorService.GetByID(s.Producto.Proveedor.ID);
                        cmbProveedores.Text = pp.RazonSocial;
                        setcmb = true;
                    }
                }
                
                AgregarArticulo(s, tabla,item.PrecioUnidad.ToString(), item.precioExtra.ToString(), item.Cantidad,item.Alicuota);

                

                //txtsubtotal.Text = CalcularSubTotal(tabla).ToString();
                CalcularTotales();
            }

            
            if (!_compra.Enable)
            {
                button1.Enabled = false;
            }

            CalcularTotales();

        }
        private void CalcularTotales()
        {
            txtNeto.Text = CalcularSubTotal(tabla).ToString();
            //txtUnidadTotal.Text = CalcularCantidadTotal(tabla);
            txtIva.Text = calcularIva(tabla).ToString();
            txtTotal.Text = CalcularTotal(tabla, new DataGridView()).ToString();
        }

        private void cargarColores()
        {
            var colorService = new ColorService(new ColorRepository());
            cmbColores.DataSource = colorService.GetAll(true);
            cmbColores.DisplayMember = "Description";
        }

        private void cargarProveedores()
        {
            List<ProveedorData> pvs = new ProveedorService(new ProveedorRepository()).GetAll(false); ;
            cmbProveedores.DisplayMember = "razonSocial";
            cmbProveedores.DataSource = pvs;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var comprasProveedoresService = new ComprasProveedorService(new ComprasProveedoresRepository(),
                      new CompraProveedoresDetalleRepository());
             DialogResult dg = MessageBox.Show("Desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
             bool task;
             if (dg == DialogResult.OK)
             {

                 task = comprasProveedoresService.Disable(_compra.ID, true);


                 if (task)
                 {
                     MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     button1.Enabled = false;
                 }
                 else
                 {
                     MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
                     
                 
             }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            bool task;
            if (dg == DialogResult.OK)
            {
                _compra.Date =fechaContable.Value;
                _compra.FechaFactura =fechaFactura.Value;
                _compra.Description = txtObs.Text;

                var comprasProveedoresService = new ComprasProveedorService(new ComprasProveedoresRepository(),
                      new CompraProveedoresDetalleRepository());
                task = comprasProveedoresService.Update(_compra);


                if (task)
                {
                    MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    foreach (Form hijo in this.MdiParent.MdiChildren)
                    {
                        if (hijo.GetType() == typeof(cuentaCorriente<PersonaData>))
                        {

                            ((cuentaCorriente<PersonaData>)hijo).refresh2();
                        }

                    }



                }
                else
                {
                    MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }
    }
}