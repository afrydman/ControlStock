using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.StockRepository;
using Services;
using Services.LocalService;
using Services.StockService;

namespace SharedForms.Estadisticas.Stock
{
    public partial class todoStock : Form
    {
        public todoStock()
        {
            InitializeComponent();
        }

        private void todoStock_Load(object sender, EventArgs e)
        {
            cargarLocales();
            if (HelperService.haymts)
            {
                tabla.Columns[4].HeaderText = "Mts";
            }
            if (HelperService.talleUnico)
                tabla.Columns[4].Visible = false;
        }
        private void cargarLocales()
        {
            var localService = new LocalService(new LocalRepository());
            cmbLocales.DataSource = localService.GetAll();
            cmbLocales.DisplayMember = "Codigo";
        }
        decimal tot = 0;
        private void cmbLocales_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void limpiar()
        {
            tot = 0;
            lblTotal.Text = tot.ToString();
            tabla.Rows.Clear();
            tabla.ClearSelection();
        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbLocales.SelectedIndex>-1)
            {
                cargarTabla(txtFilter.Text);
            }
        }

        private void cargarTabla(string filtro)
        {
            limpiar();

            var stockService = new StockService(new StockRepository());
            List<StockData> stockTotal = stockService.Search(stockService.GetAll(true, ((LocalData)cmbLocales.SelectedItem).ID), filtro);



            foreach (StockData s in stockTotal)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;


                tabla[0, fila].Value = s.Producto.CodigoInterno + s.Color.Codigo + s.Talle.ToString("00");

                tabla[1, fila].Value = s.Producto.Proveedor.RazonSocial;
                tabla[2, fila].Value = s.Producto.Show;
                tabla[3, fila].Value = s.Color.Description;
                tabla[4, fila].Value = s.Talle.ToString();
                tabla[5, fila].Value = s.Stock.ToString();
                
                if (s.Stock>0)
                    tot += s.Stock;

            }
            lblTotal.Text = tot.ToString();
        }
    }
}
