using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.FormaPagoRepository;
using Repository.Repositories.StockRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.FormaPagoService;
using Services.StockService;
using Services.VentaService;
using SharedForms.Impositivo;


namespace SharedForms.Ventas
{
    public partial class Ventas : Form
    {
        public Ventas()
        {
            InitializeComponent();
        }

        public void refresh2() 
        {

            cargarVentas();
            

        }

        private void Ventas_Load(object sender, EventArgs e)
        {

            ajustarGui();
        }

        private void ajustarGui()
        {
            if (HelperService.esCliente(GrupoCliente.Balarino))
            {
                tablaPagos.Columns[5].Visible = false;
            }
        }

  
       

        private void cargarVentas()
        {
            var stockService = new StockService(new StockRepository());
            limpioTablas();
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());

            Dictionary<string, decimal> pagosAcum = new Dictionary<string, decimal>(); 
            limpiarVentas();
            List<VentaData> ventasHoy = ventaService.GetByRangoFecha(DateTime.Now.Date);

            if (!chckCambios.Checked)
            {
                ventasHoy = ventasHoy.FindAll(data => !data.Cambio);
            }

            if (!chckAnuladas.Checked) //ver anuladas
                ventasHoy = ventasHoy.FindAll(data => data.Enable);


            




            ventasHoy.Sort(delegate(VentaData x, VentaData y)
            {
                return x.Date.CompareTo(y.Date);
            });

            var formaPagoService = new FormaPagoService(new FormaPagoRepository(), new FormaPagoCuotasRepository());
            foreach (VentaData v in ventasHoy)
            {

                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                tabla[0, fila].Value = HelperService.convertToFechaHoraConFormato(v.Date);
                tabla[1, fila].Value = v.NumeroCompleto;
                foreach (PagoData fp in v.Pagos)
                {
                    if (string.IsNullOrEmpty(fp.FormaPago.Description))
                    {
                        fp.FormaPago = formaPagoService.GetByID(fp.FormaPago.ID);
                    }
                    tabla[2, fila].Value += fp.FormaPago.Description + " - ";

                    if (v.Enable)
                    {
                        if (!pagosAcum.ContainsKey(fp.FormaPago.Description))
                            pagosAcum.Add(fp.FormaPago.Description, 0);

                        if (fp.Importe > 0)
                            pagosAcum[fp.FormaPago.Description] += fp.Importe;    
                    }
                   
                    
                    
                }

                tabla[3, fila].Value = v.Monto.ToString();
                tabla[4, fila].Value = !v.Enable?"Anulada":"no anulada" ;
                tabla[5, fila].Value = v.ID;
                tabla[6, fila].Value = v.Cliente.RazonSocial;
                tabla[7, fila].Value = v.Cambio ? "Cambio" : "Venta";

                if (!v.Enable)
                    tabla.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;

                
                int fila2;
                StockData aux;
                foreach (VentaDetalleData detalle in v.Children)
                {
                    tablaProductos.Rows.Add();
                    fila2 = tablaProductos.RowCount - 1;
                    aux = stockService.obtenerProducto(detalle.Codigo);

                    tablaProductos[0, fila2].Value = v.NumeroCompleto;
                    tablaProductos[1, fila2].Value = detalle.Codigo;
                    tablaProductos[2, fila2].Value = aux.Producto.Proveedor.RazonSocial;
                    tablaProductos[3, fila2].Value = aux.Producto.Show;
                    tablaProductos[4, fila2].Value = aux.Color.Description;
                    tablaProductos[5, fila2].Value = aux.Talle.ToString();
                    tablaProductos[6, fila2].Value = detalle.Cantidad;
                    tablaProductos[7, fila2].Value = detalle.PrecioUnidad+(v.Descuento!=0?"*":"");

                    if (!v.Enable)
                        tablaProductos.Rows[fila2].DefaultCellStyle.BackColor = Color.Pink;
                }

            }


            foreach (string key in pagosAcum.Keys)
            {
                tablaPagos.Rows.Add();
                int fila;
                fila = tablaPagos.RowCount - 1;
                tablaPagos[0, fila].Value = key;
                tablaPagos[1, fila].Value = pagosAcum[key].ToString();

            }


        }

        private void limpioTablas()
        {
            tabla.Rows.Clear();
            tablaPagos.Rows.Clear();
            tablaProductos.Rows.Clear();
        }

        private void limpiarVentas()
        {
            tabla.DataSource = null;
            tabla.ClearSelection();
            tabla.Rows.Clear();
        }

        private void tabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());

            if (tabla.SelectedCells.Count>0)
            {
                VentaData v = ventaService.GetByID(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[5].Value.ToString()));

                mostrarVenta(v);    
            }
            
        }

        

        private void mostrarVenta(VentaData v)
        {
            padreBase.CerrarForm(new cerrarCaja(), this);

            padreBase.AbrirForm(new mostrarVentaMenor(v), this.MdiParent, true);
            
            
        }

        private void tabla_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             if (tabla.SelectedCells.Count>0)
            {
                var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
                VentaData v = ventaService.GetByID(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[5].Value.ToString()));

            mostrarVenta(v);
            }
        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabla2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void tabla2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            cargarVentas();
        }
    }
}
