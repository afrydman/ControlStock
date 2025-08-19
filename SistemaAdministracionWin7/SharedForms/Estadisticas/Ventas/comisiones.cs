using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO;
using DTO.BusinessEntities;
using Repository.ClienteRepository;
using Repository.Repositories.FormaPagoRepository;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.StockRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.ClienteService;
using Services.FormaPagoService;
using Services.LocalService;
using Services.StockService;
using Services.VentaService;

namespace SharedForms.Estadisticas.Ventas
{
    public partial class comisiones : Form
    {
        public comisiones()
        {
            InitializeComponent();
        }

        private void comisiones_Load(object sender, EventArgs e)
        {
            CargarLocales();
            CargarVendedores();
        }

        private void CargarVendedores()
        {
            
            
            PersonalData aux = new PersonalData();

            aux.NombreContacto = "sin especificar";


            List<PersonalData> personales = new List<PersonalData>();
            personales.Insert(0, aux);
            cmbVendedores.DataSource = personales ;
            cmbVendedores.DisplayMember = "nombrecontacto";



        }

        private void CargarLocales()
        {
          

            LocalData aux = new LocalData();

            aux.Codigo = "sin especificar";

            var localService = new LocalService(new LocalRepository());
            List<LocalData> locales = localService.GetAll();
            locales.Insert(0, aux);
            cmbLocales.DataSource = localService.GetAll();
            cmbLocales.DisplayMember = "Codigo";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Limpiar();
            CargarVentas();

        }

        private void Limpiar()
        {
         LimpiarTablas();
            txtComisionTotal.Text = "";
            txtcomisionPorcentaje.Text = "0";

        }

        private void CargarVentas()
        {
            var stockService = new StockService(new StockRepository());
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            List<VentaData> ventasRango = ventaService.GetByRangoFecha(pickerDesde.Value.Date, pickerHasta.Value.Date, ((LocalData)cmbLocales.SelectedItem).ID, HelperService.Prefix);
            
            Dictionary<string, decimal> pagosAcum = new Dictionary<string, decimal>();
            var ClienteService = new ClienteService(new ClienteRepository());
            ventasRango = ventasRango.FindAll(data => data.Enable);
            if (cmbVendedores.SelectedIndex > 0)
            {
                ventasRango = ventasRango.FindAll(delegate(VentaData x)
                {
                    return x.Vendedor.ID == ((PersonaData)cmbVendedores.SelectedItem).ID;
                }
                );
            }
            if (cmbLocales.SelectedIndex > 0)
            {
                ventasRango = ventasRango.FindAll(delegate(VentaData x)
                {
                    return x.Local.ID == ((LocalData)cmbLocales.SelectedItem).ID;
                }
                );
            }
            var formaPagoService = new FormaPagoService(new FormaPagoRepository(), new FormaPagoCuotasRepository());
            foreach (VentaData v in ventasRango)
            {
                

                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                tabla[0, fila].Value =  HelperService.convertToFechaHoraConFormato(v.Date);
                tabla[1, fila].Value = v.NumeroCompleto;
                foreach (PagoData fp in v.Pagos)
                {
                    if (string.IsNullOrEmpty(fp.FormaPago.Description))
                    {
                        fp.FormaPago = formaPagoService.GetByID(fp.FormaPago.ID);

                    }
                    tabla[2, fila].Value += fp.FormaPago.Description + " - ";
                    if (!pagosAcum.ContainsKey(fp.FormaPago.Description))
                    {
                        pagosAcum.Add(fp.FormaPago.Description, 0);
                    }
                    if (fp.Importe>0)
                        pagosAcum[fp.FormaPago.Description] += fp.Importe;
                }
                tabla[3, fila].Value = v.Monto.ToString();
                tabla[4, fila].Value = v.ID;

                if (string.IsNullOrEmpty(v.Cliente.RazonSocial))
                {
                    v.Cliente = ClienteService.GetByID(v.Cliente.ID);
                }
                tabla[5, fila].Value = v.Cliente.RazonSocial;

                tabla[6, fila].Value = v.Cambio?"Cambio":"Venta";
                
               



                
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
                    tablaProductos[7, fila2].Value = detalle.PrecioUnidad + (v.Descuento != 0 ? "*" : "");

                }
            }

            foreach (string key in pagosAcum.Keys)
            {
                tablaPagos.Rows.Add();
                int fila;
                fila = tablaPagos.RowCount - 1;
                tablaPagos[0, fila].Value = key;
                tablaPagos[1, fila].Value = pagosAcum[key].ToString();
                tablaPagos[2, fila].Value = true;
            }
        }

        private void LimpiarTablas()
        {
            tabla.Rows.Clear();
            tablaPagos.Rows.Clear();
            tablaProductos.Rows.Clear();

        }

        private void txtcomisionPorcentaje_TextChanged(object sender, EventArgs e)
        {
            if (txtcomisionPorcentaje.Text == "" || HelperService.ConvertToDecimalSeguro(txtcomisionPorcentaje.Text) > 100)
            {
                txtcomisionPorcentaje.Text = "0";
            }
        }

        private void txtcomisionPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtcomisionPorcentaje, e);
            
        }

        private void btnCalcularComision_Click(object sender, EventArgs e)
        {

            decimal comision = 0;
            decimal totalVentas = 0;
            foreach (DataGridViewRow pago in tablaPagos.Rows)
            {
                if (Convert.ToBoolean(pago.Cells[2].Value))
                {
                    totalVentas += HelperService.ConvertToDecimalSeguro(pago.Cells[1].Value);
                }
            }

            comision = (totalVentas*HelperService.ConvertToDecimalSeguro(txtcomisionPorcentaje.Text))/100;

            txtComisionTotal.Text = HelperService.ConvertToDecimalSeguro(comision).ToString();
        }
    }
}
