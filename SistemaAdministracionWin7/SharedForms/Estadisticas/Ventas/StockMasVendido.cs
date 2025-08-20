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
    public partial class StockMasVendido : Form
    {
        public StockMasVendido()
        {
            InitializeComponent();
        }


        private void ventasxFecha_Load(object sender, EventArgs e)
        {
            cargarClientes();
            cargarLocales();

            if (HelperService.esCliente(GrupoCliente.Slipak))
            {
                //grupoPares.Visible = false;

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
          //  cmbClientes.DataSource = clientes;
           // cmbClientes.DisplayMember = "razonSocial";
        }

        private void cargarLocales()
        {
            var localService = new LocalService(new LocalRepository());

            if (HelperService.esCliente(GrupoCliente.CalzadosMell))
            {
                List<LocalData> l = new List<LocalData>();
                LocalData aux = localService.GetByID(HelperService.IDLocal);
                l.Add(aux);
              //  cmbLocales.DataSource = l;
            }
            else
            {
               // cmbLocales.DataSource = localService.GetAll();
            }

           // cmbLocales.DisplayMember = "Codigo";
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
            //List<VentaData> ventasHoy = ventaService.GetByRangoFecha(pickerDesde.Value.Date, pickerHasta.Value.Date, ((LocalData)cmbLocales.SelectedItem).ID, HelperService.Prefix);
            List<VentaData> ventasHoy = ventaService.GetByRangoFecha(pickerDesde.Value.Date, pickerHasta.Value.Date, HelperService.IDLocal, HelperService.Prefix);


            //if (cmbClientes.SelectedIndex > 0)
            //{
            //    ventasHoy = ventasHoy.FindAll(delegate(VentaData x)
            //    {
            //        return x.Cliente.ID == ((ClienteData)cmbClientes.SelectedItem).ID;
            //    }
            //    );
            //}


            // Assuming ventas is your List<VentaData>
            List<VentaDetalleData> allDetails = new List<VentaDetalleData>();

            // Extract all VentaDetalleData from each VentaData in ventas
            foreach (var venta in ventasHoy.Where(x=>x.Enable))
            {
                allDetails.AddRange(venta.Children);
            }




            tablaTodas.DataSource = null;
            tablaTodas.ClearSelection();
            tablaTodas.Rows.Clear();



            tabla2.DataSource = null;
            tabla2.ClearSelection();
            tabla2.Rows.Clear();

            var groupedDetails = allDetails
                   .GroupBy(detail => detail.GetProveedorArticulo())
                   .Select(group => new
                   {
                       Articulo = group.Key,          // The GetArticulo value
                       Items = group.ToList().Sum(x=>x.Cantidad)          // The list of VentaDetalleData with the same Articulo
                   }).OrderByDescending(x=>x.Items)
                   .ToList();


            var groupedDetails2 = allDetails
                   .GroupBy(detail => detail.GetProveedor())
                   .Select(group => new
                   {
                       Articulo = group.Key,          // The GetArticulo value
                       Items = group.ToList().Sum(x => x.Cantidad)             // The list of VentaDetalleData with the same Articulo
                   }).OrderByDescending(x => x.Items)
                   .ToList();

            var formaPagoService = new FormaPagoService(new FormaPagoRepository(), new FormaPagoCuotasRepository());
            
            
            foreach (var detalle in groupedDetails)
            {
                int fila;

              
                tablaTodas.Rows.Add();
                fila = tablaTodas.RowCount - 1;
                tablaTodas[0, fila].Value = detalle.Articulo.Substring(0,4).ToString();
                tablaTodas[1, fila].Value = detalle.Articulo.Substring(4, 3).ToString();
                tablaTodas[2, fila].Value = detalle.Items.ToString();
              

            }
            foreach (var detalle in groupedDetails2)
            {
                int fila;


                tabla2.Rows.Add();
                fila = tabla2.RowCount - 1;
                tabla2[0, fila].Value = detalle.Articulo.Substring(0, 4).ToString();

                tabla2[1, fila].Value = detalle.Items.ToString();


            }


            //txtParesVendidos.Text = paresVendidos.ToString();
            //txtEntradaCambio.Text = paresEntradaCambio.ToString();
            //txtSalidaCambio.Text = paresSalidaCambio.ToString();

            //foreach (string key in pagosAcum.Keys)
            //{
            //    tablaPagos.Rows.Add();
            //    int fila;
            //    fila = tablaPagos.RowCount - 1;
            //    tablaPagos[0, fila].Value = key;
            //    tablaPagos[1, fila].Value = pagosAcum[key].ToString();

            //}

        }

        private void tablaTodas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            //VentaData v = ventaService.GetByID(new Guid(tablaTodas.Rows[tablaTodas.SelectedCells[0].RowIndex].Cells[6].Value.ToString()));
            //mostrarVenta(v);
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
            //txtEntradaCambio.Text = "0";
            //txtParesVendidos.Text = "0";
            //txtSalidaCambio.Text = "0";
            limparPagos();
            limpiarVentas();
            cargarVentas();
        }

        private void limparPagos()
        {
            //tablaPagos.DataSource = null;
            //tablaPagos.ClearSelection();
            //tablaPagos.Rows.Clear();
        }
    }
}
