using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.StockRepository;
using Services;
using Services.ProductoService;
using Services.ProveedorService;
using Services.StockService;

namespace SharedForms.Admin
{
    public partial class verificandoStock : Form
    {
        public verificandoStock()
        {
            InitializeComponent();
        }

        private void verificandoStock_Load(object sender, EventArgs e)
        {
            cargarProveedores();
        }

        private void cargarProveedores()
        {
            var proveedorService = new ProveedorService(new ProveedorRepository());

            cmbProveedor.DisplayMember = "cod_razon";
            cmbProveedor.DataSource = proveedorService.GetAll(true);
            
        }

        private decimal getStock(string codigo)
        {
            var stockService = new StockService(new StockRepository());

            return stockService.GetStockTotal(codigo, HelperService.IDLocal);

        }

        private decimal getDetalleStock(string codigo)
        {
            var stockService = new StockService(new StockRepository());

            List<detalleStockData> detalles = stockService.GetDetalleStock(codigo, HelperService.IDLocal);

            decimal acum = 0;
            foreach (detalleStockData detalle in detalles)
            {
                acum+=detalle.cantidad;
            }

            return acum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargoTabla();



        }

        private void cargoTabla()
        {
            tabla.Rows.Clear();

            var productoService = new ProductoService(new ProductoRepository());
            List<ProductoData> ps = productoService.GetbyProveedor(((ProveedorData)cmbProveedor.SelectedItem).ID, false);


            int fila;
            decimal detalle;
            decimal total;
            foreach (ProductoData productoData in ps)
            {
                tabla.Rows.Add();

                detalle = getDetalleStock(productoData.CodigoInterno);
                total = getStock(productoData.CodigoInterno);
                
                if (total < -666)
                    total = 0;

                fila = tabla.RowCount - 1;

                tabla[0, fila].Value = productoData.ID;
                tabla[1, fila].Value = productoData.CodigoInterno;
                tabla[2, fila].Value = productoData.Show;
                tabla[3, fila].Value = detalle.ToString();
                tabla[4, fila].Value = total.ToString();
                tabla[5, fila].Value = detalle == total ? "ök" : "ERROR";
                tabla[6, fila].Value = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var stockService = new StockService(new StockRepository());

             foreach (DataGridViewRow row in tabla.Rows)
                {
                    if (tabla[4, row.Index].Value.ToString() != "0" && Convert.ToBoolean(tabla[6, row.Index].Value))
                    {
                        stockService.setDinamicallyStock(tabla[1, row.Index].Value.ToString().Substring(0, 7), HelperService.IDLocal);
                    }
             }
             cargoTabla();

                  
        }
    }
}
