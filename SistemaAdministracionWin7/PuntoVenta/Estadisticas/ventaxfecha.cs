using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DTO.BusinessEntities;

using Repository.ClienteRepository;
using Repository.Repositories.FormaPagoRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.ClienteService;
using Services.FormaPagoService;
using Services.VentaService;
using SharedForms.Ventas;

namespace PuntoVenta.Estadisticas
{
    public partial class ventaxfecha : Form
    {
        public ventaxfecha()
        {
            InitializeComponent();
        }

        private void ventaxfecha_Load(object sender, EventArgs e)
        {
            pickerDesde.Value = pickerDesde.Value.Date.AddDays(-30);
        }
        Guid _ventaID;
        private void mostrarVenta(VentaData v)
        {

            _ventaID = v.ID;

            padre.AbrirForm(new mostrarVentaMenor(v), this.MdiParent, true);



        }


        private void cargarVentas()
        {

            limpiarVentas();
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            List<VentaData> ventasHoy = ventaService.GetByRangoFecha(pickerDesde.Value.Date, pickerHasta.Value.Date, HelperService.IDLocal, HelperService.Prefix);
            var ClienteService = new ClienteService(new ClienteRepository());
            var formaPagoService = new FormaPagoService(new FormaPagoRepository(), new FormaPagoCuotasRepository());


            foreach (VentaData v in ventasHoy)
            {

                tablaTodas.Rows.Add();
                int fila;
                fila = tablaTodas.RowCount - 1;
                tablaTodas[0, fila].Value = HelperService.convertToFechaHoraConFormato(v.Date);
                tablaTodas[1, fila].Value = v.NumeroCompleto.ToString();
                foreach (PagoData fp in v.Pagos)
                {
                    if (fp.FormaPago.Description == "" || fp.FormaPago.Description == null)
                    {
                        fp.FormaPago = formaPagoService.GetByID(fp.FormaPago.ID);
                    }
                    tablaTodas[2, fila].Value += fp.FormaPago.Description + " - ";
                }

                tablaTodas[3, fila].Value = v.Monto;
                tablaTodas[4, fila].Value = !v.Enable ? "Anulada" : "No Anulada";
                tablaTodas[5, fila].Value = v.Cambio ? "Cambio" : "Venta";
                tablaTodas[6, fila].Value = v.ID;
                if (!v.Enable)
                    tablaTodas.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;
                

                if (v.Cliente .RazonSocial == null || v.Cliente.RazonSocial == "")
                {
                    v.Cliente = ClienteService.GetByID(v.Cliente.ID);
                }
                tablaTodas[7, fila].Value = v.Cliente.RazonSocial;

            }
        }
        private void limpiarVentas()
        {
            tablaTodas.DataSource = null;
            tablaTodas.ClearSelection();
            tablaTodas.Rows.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            limpiarVentas();
            cargarVentas();
        }

        private void tablaTodas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            VentaData v = ventaService.GetByID(new Guid(tablaTodas.Rows[tablaTodas.SelectedCells[0].RowIndex].Cells[6].Value.ToString()));
            mostrarVenta(v);
        }
    }
}
