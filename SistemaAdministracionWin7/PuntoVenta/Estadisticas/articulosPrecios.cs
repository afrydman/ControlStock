using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.ListaPrecioRepository;
using Repository.Repositories.PrecioRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.StockRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.ListaPrecioService;
using Services.PrecioService;
using Services.ProductoService;
using Services.StockService;
using Services.VentaService;

namespace PuntoVenta.Estadisticas
{
    public partial class articulosPrecios : Form
    {
        public articulosPrecios()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var stockService = new StockService(new StockRepository());
            limpiar();
            if (todoValid())
            {
                StockData s = stockService.obtenerProducto(txtinterno.Text);
                var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
                txtArticulo.Text = s.Producto.Show;
                txtProveedor.Text = s.Producto.Proveedor.RazonSocial;

                limpiarTablaPrecios();
                var productoTalleService = new ProductoTalleService(new ProductoTalleRepository());

                List<ProductoTalleData> todos = productoTalleService.GetByProducto(s.Producto.ID);
                cargarPrecios(todos);

                List<VentaData> ventas = new List<VentaData>();
                ventas = ventaService.GetVentasConDetalle(txtinterno.Text.Substring(0, 7), HelperService.IDLocal);
                //List<ventaDetalleData> detalles = ventaDetalle.getbyCodigoInterno(txtinterno.Text.Substring(0,7));

                foreach (VentaData v in ventas)
                {
                    foreach (var det in v.Children)
                    {
                        if (det.Codigo.Substring(0, 7) == txtinterno.Text.Substring(0, 7))//la venta puede tener otros articulos.
                        {
                            tabla.Rows.Add();
                            int fila;
                            fila = tabla.RowCount - 1;
                            //Codigo nombre  color talle subtotal
                            tabla[0, fila].Value = v.ID;
                            tabla[1, fila].Value = HelperService.convertToFechaConFormato(v.Date);
                            tabla[2, fila].Value = v.NumeroCompleto;
                            tabla[3, fila].Value = det.Codigo.Substring(10, 2);
                            tabla[4, fila].Value = det.PrecioUnidad.ToString();
                            tabla[5, fila].Value = det.Bonificacion.ToString();

                            decimal Bonificacion = HelperService.ConvertToDecimalSeguro((det.PrecioUnidad * det.Bonificacion) / 100);
                            decimal subtotalConBonif = HelperService.ConvertToDecimalSeguro(det.PrecioUnidad - Bonificacion);



                            tabla[6, fila].Value = subtotalConBonif.ToString();
                        }
                    }

                }

            }
            else
            {

                MessageBox.Show("Corrija los campos e intente nuevamete", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void limpiarTablaPrecios()
        {


            tablaPrecios.DataSource = null;
            tablaPrecios.ClearSelection();
            tablaPrecios.Rows.Clear();

        }
        private void cargarPrecios(List<ProductoTalleData> todos)
        {

            var precioService = new PrecioService(new PrecioRepository());

            decimal precio = 0;
            decimal aux = 0;
            int inicio = 0;
            int final = 0;
            int fila;

            if (todos.Count > 0)
            {
                inicio = todos[0].Talle;
                aux = precioService.GetPrecio(((listaPrecioData)cmbLista.SelectedItem).ID, todos[0].ID);
            }
            foreach (ProductoTalleData pt in todos)
            {
                precio = precioService.GetPrecio(((listaPrecioData)cmbLista.SelectedItem).ID, pt.ID);
                if (precio != aux)
                {
                    final = pt.Talle - 1;

                    tablaPrecios.Rows.Add();
                    fila = tablaPrecios.RowCount - 1;
                    //id nombre Codigo
                    tablaPrecios[0, fila].Value = inicio == final ? inicio.ToString() : inicio.ToString() + "-" + final.ToString();
                    tablaPrecios[1, fila].Value = aux > -200 ? aux.ToString() : "0";

                    inicio = pt.Talle;
                    aux = precio;
                }
            }
            if (todos.Count > 0)
            {
                final = todos[todos.Count - 1].Talle;

                tablaPrecios.Rows.Add();
                fila = tablaPrecios.RowCount - 1;
                //id nombre Codigo
                tablaPrecios[0, fila].Value = inicio.ToString() + "-" + final.ToString();
                tablaPrecios[1, fila].Value = aux > -200 ? aux.ToString() : "0";
                tablaPrecios.ClearSelection();
            }
            


        }


        private void limpiar()
        {
            limpiarTabla();
            limpiartext();
        }

        private void limpiartext()
        {
            txtArticulo.Text = "";
           
            txtProveedor.Text = "";
           
        }

        private void limpiarTabla()
        {
            tabla.Rows.Clear();
            tabla.ClearSelection();
        }

        private bool todoValid()
        {
            if (desde.Value.Date>=hasta.Value.Date)
            {
                return false;
            }
            if (txtinterno.Text.Length<7)
            {
                MessageBox.Show("Ingrese al menos 7 digitos");
                return false;
            }
            return true;
        }

        private void articulosPrecios_Load(object sender, EventArgs e)
        {
            desde.Value = desde.Value.Date.AddDays(-30);

            cargarListas();
        }

        private void cargarListas()
        {
            var listaPrecioService = new ListaPrecioService(new ListaPrecioRepository());
            cmbLista.DisplayMember = "Description";
            cmbLista.DataSource = listaPrecioService.GetAll();
            cmbLista.DisplayMember = "Description";
            cmbLista.SelectedIndex = 0;
        }
    }
}
