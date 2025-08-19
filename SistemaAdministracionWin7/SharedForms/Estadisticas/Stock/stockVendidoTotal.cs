using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.StockRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.LocalService;
using Services.StockService;
using Services.VentaService;

namespace SharedForms.Estadisticas.Stock
{
    public partial class stockVendidoTotal : Form
    {
        public stockVendidoTotal()
        {
            InitializeComponent();
        }

        private void stockVendido_Load(object sender, EventArgs e)
        {
            
            cargarLocales();
            desde.Value = desde.Value.Date.AddDays(-30);
            if (HelperService.talleUnico)
                tabla.Columns[6].Visible = false;
        }

        private void cargarLocales()
        {
            var localService = new LocalService(new LocalRepository());

            cmbLocales.DataSource = localService.GetAll();
            cmbLocales.DisplayMember = "Codigo";
        }


        

        private void limparTabla()
        {
            tabla.Rows.Clear();
            tabla.ClearSelection();
        }
        

        

        private void cargarTabla()
        {
            var stockService = new StockService(new StockRepository());

            limparTabla();
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            List<VentaData> ventas = ventaService.GetByRangoFecha(desde.Value, hasta.Value, ((LocalData)cmbLocales.SelectedItem).ID, HelperService.Prefix);

            foreach (VentaData v in ventas)
            {
                


                foreach (VentaDetalleData detalle in v.Children)
                {
                    StockData s = stockService.obtenerProducto(detalle.Codigo);
                    tabla.Rows.Add();
                    int fila;
                    fila = tabla.RowCount - 1;
                    tabla[0, fila].Value =  HelperService.convertToFechaHoraConFormato(v.Date);
                    tabla[1, fila].Value = v.NumeroCompleto;
                    tabla[2, fila].Value = detalle.Codigo;
                    tabla[3, fila].Value = s.Producto.Proveedor.RazonSocial;
                    tabla[4, fila].Value = s.Producto.Show;
                    tabla[5, fila].Value = s.Color.Description;
                    tabla[6, fila].Value = s.Talle;
                    tabla[7, fila].Value = detalle.PrecioUnidad;
                    tabla[8, fila].Value = detalle.Cantidad;
                    tabla[9, fila].Value = detalle.Cantidad * detalle.PrecioUnidad;

                }

            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbLocales_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limparTabla();

            cargarTabla();
        }

        private void desde_ValueChanged(object sender, EventArgs e)
        {

        }

        private void hasta_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
