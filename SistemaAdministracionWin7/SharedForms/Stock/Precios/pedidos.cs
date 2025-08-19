using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.ClienteRepository;
using Repository.Repositories.FormaPagoRepository;
using Repository.Repositories.PedidoRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.ClienteService;
using Services.FormaPagoService;
using Services.PedidoService;
using Services.VentaService;

namespace SharedForms.Stock
{
    public partial class pedidos : Form
    {
        public pedidos()
        {
            InitializeComponent();
        }

        public void refresh2() 
        {

            cargarVentas();
           
            //cargarCajaInicial();
            //cargarCajaActual();
            //cargarCantidadVentas();
        }

        private PedidoService _pedidoService = null;
        private void Ventas_Load(object sender, EventArgs e)
        {
            _pedidoService = new PedidoService(new PedidoRepository(), new PedidoDetalleRepository());
            dateDesde.Value=dateDesde.Value.AddDays(-30);
            cargarClientes();
            setearFechas();
            cargarPedidos();
        }
      

        private void cargarPedidos()
        {
            List<pedidoData> pedidos = _pedidoService.GetAll();

 
                pedidos = pedidos.FindAll(delegate(pedidoData x)
                {
                    if (cmbClientes.SelectedIndex > 0)
                    {
                        return ((x.Cliente.ID == ((ClienteData)cmbClientes.SelectedItem).ID) && (x.Date >= dateDesde.Value) && (x.Date <= dateHasta.Value) && (x.completo == chkincompleto.Checked));
                    }
                    else {
                        return ((x.Date >= dateDesde.Value) && (x.Date <= dateHasta.Value) && (x.completo == chkincompleto.Checked));
                    }
                    
                });
            

            //fecha


            int fila;
            foreach (pedidoData item in pedidos)
            {
                 tabla.Rows.Add();

                fila = tabla.RowCount - 1;
                //id Numero fecha cliente Monto anulada completada
                tabla[0, fila].Value = item.ID;
                tabla[1, fila].Value = item.NumeroCompleto;
                tabla[2, fila].Value = item.Date.ToShortDateString();
                tabla[3, fila].Value = item.Cliente.RazonSocial;
                tabla[4, fila].Value = item.Monto.ToString();
                tabla[5, fila].Value = !item.Enable?"Anulada":"No Anulada";
                tabla[6, fila].Value = item.completo? "Completa" : "No Completa";
            }

        }

        private void setearFechas()
        {
            dateDesde.Value.AddMonths(-1);

        }

        private void cargarClientes()
        {
            ClienteData aux = new ClienteData();
            
            aux.RazonSocial="sin especificar";

            cmbClientes.DisplayMember = "razonSocial";

            var ClienteService = new ClienteService(new ClienteRepository());
            List<ClienteData> clientes = ClienteService.GetAll(true);
            clientes.Insert(0,aux);
            cmbClientes.DataSource = clientes;
            cmbClientes.DisplayMember = "razonSocial";


        }

        


       

        private void cargarVentas()
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            var formaPagoService = new FormaPagoService(new FormaPagoRepository(), new FormaPagoCuotasRepository());
            limpiarVentas();
            List<VentaData> ventasHoy = ventaService.GetByRangoFecha(DateTime.Now.Date);
            
            
            foreach (VentaData v in ventasHoy)
            {

                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                tabla[0, fila].Value = v.Date;
                tabla[1, fila].Value = v.NumeroCompleto;
                foreach (PagoData fp in v.Pagos)
                {
                    if (fp.FormaPago.Description == "" || fp.FormaPago.Description == null)
                    {
                        fp.FormaPago = formaPagoService.GetByID(fp.FormaPago.ID);
                    }
                    tabla[2, fila].Value += fp.FormaPago.Description + " - ";
                }

                tabla[3, fila].Value = v.Monto.ToString();
                tabla[4, fila].Value = !v.Enable?"Anulada":"no anulada" ;
                tabla[5, fila].Value = v.ID;
                tabla[6, fila].Value = v.Cliente.RazonSocial;
            }


        }

        private void limpiarVentas()
        {
            tabla.DataSource = null;
            tabla.ClearSelection();
            tabla.Rows.Clear();
        }

        private void tabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showdaPedido();
            
        }

        private void mostrarCambio(VentaData v) 
        {

           

           
            
        }

        private void mostrarPedido(pedidoData v)
        {
           
            
                padreBase.AbrirForm(new mostrarpedido(v), this.MdiParent, true);
            
            
        }

        private void tabla_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showdaPedido();
        }

        private void showdaPedido() { 
        
         if (tabla.SelectedCells.Count>0)
            {
                pedidoData v = _pedidoService.GetByID(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));

            mostrarPedido(v);
            }
        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            limparTabla();
            cargarPedidos();
        }

        private void limparTabla()
        {
            tabla.DataSource = null;
            tabla.ClearSelection();
            tabla.Rows.Clear(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dg = HelperService.MessageBoxHelper.confirmOperation();

            if (dg == DialogResult.OK)
            {
                if (tabla.SelectedCells.Count > 0)
                {
                    anularPedido(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                    
                }
            }
            limparTabla();
            cargarPedidos();

        }

        private void anularPedido(Guid id)
        {

            bool task = _pedidoService.Disable(new pedidoData(id));

            if (task)
                HelperService.MessageBoxHelper.PedidoUpdateOk();
            else
                HelperService.MessageBoxHelper.PedidoUpdateError();


        }

       
    }
}
