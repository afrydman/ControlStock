using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Central.Proveedores;
using DTO.BusinessEntities;
using Repository.Repositories.ComprasProveedoresRepository;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.StockRepository;
using Services.ComprasProveedorService;
using Services.LocalService;
using Services.StockService;

namespace Central.Estadisticas.Compras
{
    public partial class comprasxdia : Form
    {
        public comprasxdia()
        {
            InitializeComponent();
        }

        private void comprasxdia_Load(object sender, EventArgs e)
        {
            cargarLocales();
            inicilizoPickers();
            getCompras();
        }
        private void cargarLocales()
        {
            var localService = new LocalService(new LocalRepository());
            cmbLocales.DataSource = localService.GetAll();
            cmbLocales.DisplayMember = "Codigo";
        }
        private void inicilizoPickers()
        {
            picker.Value = DateTime.Now.AddDays(-30);
        }

        private void getCompras()
        {
            limparTabla();
            var comprasProveedoresService = new ComprasProveedorService(new ComprasProveedoresRepository(),
                new CompraProveedoresDetalleRepository());
            
            List<ComprasProveedoresData> compras = comprasProveedoresService.GetAll();


            var stockService = new StockService(new StockRepository());

            List<ComprasProveedoresData> comprasAux;

            comprasAux = compras.FindAll(delegate(ComprasProveedoresData c) { return c.Enable; });
            comprasAux = comprasAux.FindAll(delegate(ComprasProveedoresData c) { return c.Date.DayOfYear >= picker.Value.Date.DayOfYear && c.Date.DayOfYear <= pickerHasta.Value.Date.DayOfYear; });

            // filtramo lo locales
            comprasAux = comprasAux.FindAll(delegate(ComprasProveedoresData c) { return c.Local.ID == ((LocalData)cmbLocales.SelectedItem).ID; });


            List<ComprasProveedoresdetalleData> detalles = new List<ComprasProveedoresdetalleData>();
            foreach (ComprasProveedoresData c in comprasAux)
            {
                detalles.AddRange(c.Children);
                
            }

            StockData s = new StockData();
            foreach (ComprasProveedoresdetalleData r in detalles)
            {//se que tiene uno solo por como esta hecho la parte de compra de stock!


                s = stockService.obtenerProducto(r.Codigo);
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                //Codigo nombre  color talle subtotal
                tabla[0, fila].Value = s.Producto.Proveedor.RazonSocial;
                tabla[1, fila].Value = s.Producto.Show;
                tabla[2, fila].Value = s.Color.Description;
                tabla[3, fila].Value = s.Talle;
                tabla[4, fila].Value = r.Cantidad.ToString();
                tabla[5, fila].Value = r.FatherID;
                tabla[6, fila].Value = r.PrecioUnidad.ToString();
                tabla[7, fila].Value = r.PrecioExtra.ToString();
                tabla[8, fila].Value = r.SubTotal.ToString();

                
                tabla.ClearSelection();


            }




        }

        private void limparTabla()
        {
            tabla.Rows.Clear();
            tabla.ClearSelection();
        }

        private void picker_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var comprasProveedoresService = new ComprasProveedorService(new ComprasProveedoresRepository(),
                   new CompraProveedoresDetalleRepository());
            if (tabla.SelectedCells.Count > 0)
            {
                Guid aux = new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[5].Value.ToString());
                padre.AbrirForm(new MostrarcomprasAProveedores(comprasProveedoresService.GetByID(aux)), this.MdiParent);
            }
        }

        private void pickerHasta_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbLocales_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            getCompras();
        }
    }
}
