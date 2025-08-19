using System;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.StockRepository;
using Services;
using Services.StockService;

namespace SharedForms.Stock
{
    public partial class verEnvio : Form
    {
        public verEnvio()
        {
            InitializeComponent();
        }
        RemitoData _remito;
        public verEnvio(RemitoData remito)
        {
            InitializeComponent();
            _remito = remito;
        }

        private void verEnvio_Load(object sender, EventArgs e)
        {
            var stockService = new StockService(new StockRepository());

            if (HelperService.haymts)
            {
                tabla.Columns[4].HeaderText = "mts";
                
            }
            else
            {
                tabla.Columns[6].Visible = false;
            }

            lblOrigen.Text = _remito.Local.Codigo;
            lblDestino.Text = _remito.LocalDestino.Codigo;
            lblFechaG.Text = _remito.Date.ToString();
            lblFechaR.Text = _remito.FechaRecibo.ToString();
            lblestado.Text = _remito.estado.ToString();
            lblCantidad.Text = _remito.CantidadTotal.ToString();
            lblnro.Text = _remito.Show;
            lblObs.Text = _remito.Description;

            StockData s;
            int fila;



            foreach (remitoDetalleData detalle in _remito.Children)
            {
                tabla.Rows.Add();

                s = stockService.obtenerProducto(detalle.Codigo);
                fila = tabla.RowCount - 1;

                tabla[0, fila].Value = detalle.Codigo;
                
                    
                tabla[1, fila].Value = s.Producto.Proveedor.RazonSocial;
                tabla[2, fila].Value = s.Producto.Show;
                tabla[3, fila].Value = s.Color.Description;


                tabla[4, fila].Value = HelperService.haymts ? s.Metros : s.Talle;
                tabla[5, fila].Value = detalle.Cantidad;
                if (HelperService.haymts)
                    tabla[6, fila].Value = detalle.Cantidad*s.Metros;
                    
                
                

                tabla.ClearSelection();
            }



            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stockBase.GenerarArchivo("Envio_" + _remito.Show, tabla.Rows);
        }
    }
}
