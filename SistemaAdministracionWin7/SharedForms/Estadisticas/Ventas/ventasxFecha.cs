using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.ClienteRepository;
using Repository.Repositories.FormaPagoRepository;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.ClienteService;
using Services.FormaPagoService;
using Services.LocalService;
using Services.VentaService;
using SharedForms.Ventas;

namespace SharedForms.Estadisticas.Ventas
{
    public partial class ventasxFecha : Form
    {
        public ventasxFecha()
        {
            InitializeComponent();
        }


        private void ventasxFecha_Load(object sender, EventArgs e)
        {
            cargarClientes();
            cargarLocales();

            if (HelperService.esCliente(GrupoCliente.Slipak))
            {
                grupoPares.Visible = false;

            }
            pickerDesde.Value = pickerDesde.Value.Date.AddDays(-30);
        }

        private void cargarClientes()
        {
            ClienteData aux = new ClienteData();

            aux.RazonSocial = "sin especificar";

            var ClienteService = new ClienteService(new ClienteRepository());
            List<ClienteData> clientes = ClienteService.GetAll(true);

            clientes.Insert(0, aux);
            cmbClientes.DataSource = clientes;
            cmbClientes.DisplayMember = "razonSocial";
        }

        private void cargarLocales()
        {
            var localService = new LocalService(new LocalRepository());

            if (HelperService.esCliente(GrupoCliente.CalzadosMell))
            {
                List<LocalData> l = new List<LocalData>();
                LocalData aux = localService.GetByID(HelperService.IDLocal);
                l.Add(aux);
                cmbLocales.DataSource = l;
            }
            else
            {
                cmbLocales.DataSource = localService.GetAll();        
            }

            cmbLocales.DisplayMember = "Codigo";
        }
        private void limpiarVentas()
        {
            tablaTodas.DataSource = null;
            tablaTodas.ClearSelection();
            tablaTodas.Rows.Clear();
        }

       

        private void cargarVentas()
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            decimal paresVendidos = 0;
            decimal paresSalidaCambio = 0;
            decimal paresEntradaCambio = 0;
            Dictionary<string, decimal> pagosAcum = new Dictionary<string, decimal>(); 
            limpiarVentas();
            var ClienteService = new ClienteService(new ClienteRepository());
            List<VentaData> ventasHoy = ventaService.GetByRangoFecha(pickerDesde.Value.Date, pickerHasta.Value.Date, ((LocalData)cmbLocales.SelectedItem).ID, HelperService.Prefix);
            

            if (cmbClientes.SelectedIndex > 0)
            {
                ventasHoy = ventasHoy.FindAll(delegate(VentaData x)
                {
                    return x.Cliente.ID == ((ClienteData)cmbClientes.SelectedItem).ID;
                }
                );
            }
            var formaPagoService = new FormaPagoService(new FormaPagoRepository(), new FormaPagoCuotasRepository());
            foreach (VentaData v in ventasHoy)
            {
                tablaTodas.Rows.Add();
                int fila;
                fila = tablaTodas.RowCount - 1;
                tablaTodas[0, fila].Value =  HelperService.convertToFechaHoraConFormato(v.Date);
                tablaTodas[1, fila].Value = v.NumeroCompleto;

                foreach (PagoData fp in v.Pagos)
                {
                    if (fp.FormaPago.Description == "" || fp.FormaPago.Description == null)
                    {
                        fp.FormaPago = formaPagoService.GetByID(fp.FormaPago.ID);

                    }
                    tablaTodas[2, fila].Value += fp.FormaPago.Description + " - ";
                    if (!pagosAcum.ContainsKey(fp.FormaPago.Description))
                    {
                        pagosAcum.Add(fp.FormaPago.Description,0);
                    }
                    if (fp.Importe > 0)
                        pagosAcum[fp.FormaPago.Description] += fp.Importe;
                    
                }

                tablaTodas[3, fila].Value = v.Monto;
                tablaTodas[4, fila].Value = !v.Enable ? "Anulada" : "No Anulada";
                if (v.Cambio)
                {
                    tablaTodas[5, fila].Value = "Cambio";

                    paresSalidaCambio += -1*(v.Children.Where(item => item.Cantidad > 0).Sum(item => item.Cantidad));
                    paresEntradaCambio += v.Children.Where(item => item.Cantidad < 0).Sum(item => item.Cantidad);
                }
                else
                { 
                    tablaTodas[5, fila].Value = "Venta";
                    paresVendidos += v.Children.Sum(item => item.Cantidad);
                }
                
                
                tablaTodas[6, fila].Value = v.ID;
                if (v.Cliente.RazonSocial == null || v.Cliente.RazonSocial == "")
                {
                    v.Cliente = ClienteService.GetByID(v.Cliente.ID);
                }
                tablaTodas[7, fila].Value = v.Cliente.RazonSocial;

            }



            txtParesVendidos.Text = paresVendidos.ToString();
            txtEntradaCambio.Text = paresEntradaCambio.ToString();
            txtSalidaCambio.Text = paresSalidaCambio.ToString();

            foreach (string key in pagosAcum.Keys)
            {
                tablaPagos.Rows.Add();
                int fila;
                fila = tablaPagos.RowCount - 1;
                tablaPagos[0, fila].Value = key;
                tablaPagos[1, fila].Value = pagosAcum[key].ToString();

            }

        }

        private void tablaTodas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            VentaData v = ventaService.GetByID(new Guid(tablaTodas.Rows[tablaTodas.SelectedCells[0].RowIndex].Cells[6].Value.ToString()));
            mostrarVenta(v);
        }


        Guid _ventaID;
        private void mostrarVenta(VentaData v)
        {

            _ventaID = v.ID;

             if (HelperService.esCliente(GrupoCliente.Slipak))
            {
                padreBase.AbrirForm(new mostrarventaMayor(v), this.MdiParent, true);     
            }
             else
             {
                 padreBase.AbrirForm(new mostrarVentaMenor(v), this.MdiParent, true);     
             }
        }



        private void picker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbLocales_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tablaTodas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //venta.imprimir(venta.impresiones.remito, _ventaID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtEntradaCambio.Text = "0";
            txtParesVendidos.Text = "0";
            txtSalidaCambio.Text = "0";
            limparPagos();
            limpiarVentas();
            cargarVentas();
        }

        private void limparPagos()
        {
            tablaPagos.DataSource = null;
            tablaPagos.ClearSelection();
            tablaPagos.Rows.Clear();
        }
    }
}
