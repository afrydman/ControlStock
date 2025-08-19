using DTO.BusinessEntities;
using Repository.Repositories.CajaRepository;
using Services;
using Services.CajaService;
using Services.FormaPagoService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharedForms.Impositivo
{
    public partial class ModificarCajaCascada: Form
    {
        public ModificarCajaCascada()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //calcular

            tablaNuevo.DataSource = null;
            tablaNuevo.ClearSelection();
            tablaNuevo.Rows.Clear();

            var cajaService = new CajaService(new CajaRepository());

            DateTime fechaDesde = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            List<DTO.BusinessEntities.CajaData> listCajas = cajaService.GetByRangoFechas2(fechaDesde, hasta.Value, HelperService.IDLocal, HelperService.Prefix);



            var cajaInicial = cajaService.GetCajaInicial(fechaDesde, HelperService.IDLocal);
            decimal newCajaInicial = Convert.ToDecimal(txtNuevaCaja.Text);


            decimal diffBetweenCajas;
            
            if(cajaInicial.Monto>newCajaInicial)
                diffBetweenCajas = newCajaInicial - cajaInicial.Monto;
            else
                diffBetweenCajas =   newCajaInicial - cajaInicial.Monto;

            var sortedCajas = listCajas.OrderBy(c => c.Date).ToList(); // Sort first

            for (int j = 0; j < sortedCajas.Count; j++)
            {

                tablaNuevo.Rows.Add();
                int fila;
                fila = tablaNuevo.RowCount - 1;
                tablaNuevo[0, fila].Value = sortedCajas[j].ID;
                tablaNuevo[1, fila].Value = sortedCajas[j].Date.ToString("dd/MM/yyyy");
                tablaNuevo[3, fila].Value = (sortedCajas[j].Monto + diffBetweenCajas).ToString();//caja final = db record


                if (j != 0)
                    tablaNuevo[2, fila].Value = (listCajas[j - 1].Monto+ diffBetweenCajas).ToString();//caja inicial
                else
                    tablaNuevo[2, fila].Value = newCajaInicial.ToString();
            }


            //poner en rojo las que tengan valor <1
            bool todoPositivo = true;
            foreach (var item in tablaNuevo.Rows)
            {

            }

            btnConfirmar.Enabled = todoPositivo;


        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            limpiarControles();
            cargarDatos();


        }

        private Guid starterCajaId;


        private void cargarDatos()
        {
            var cajaService = new CajaService(new CajaRepository());

            List<DTO.BusinessEntities.CajaData> listCajas = cajaService.GetByRangoFechas2(desde.Value.AddDays(-2), hasta.Value, HelperService.IDLocal, HelperService.Prefix);



            var cajaInicial = cajaService.GetCajaInicial(desde.Value, HelperService.IDLocal);


            var sortedCajas = listCajas.OrderBy(c => c.Date).ToList(); // Sort first

            for (int j = 0; j < sortedCajas.Count; j++)
            {

                tablaCajas.Rows.Add();
                int fila;
                fila = tablaCajas.RowCount - 1;
                tablaCajas[0, fila].Value = sortedCajas[j].ID;
                tablaCajas[1, fila].Value = sortedCajas[j].Date.ToString("dd/MM/yyyy");
                tablaCajas[3, fila].Value = sortedCajas[j].Monto.ToString();//caja final = db record


                if (j != 0)
                    tablaCajas[2, fila].Value = listCajas[j - 1].Monto.ToString();//caja inicial
                else
                    tablaCajas[2, fila].Value = cajaInicial.Monto.ToString();
            }








           


        }

        private void limpiarControles()
        {
            tablaCajas.DataSource = null;
            tablaCajas.ClearSelection();
            tablaCajas.Rows.Clear();

            tablaNuevo.DataSource = null;
            tablaNuevo.ClearSelection();
            tablaNuevo.Rows.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtTotalRetiros_TextChanged(object sender, EventArgs e)
        {

        }

        private void ModificarCajaCascada_Load(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = false;
        }

        private void tablaCajas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            loadValues();
        }

        private void loadValues()
        {
            starterCajaId = new Guid(tablaCajas.Rows[tablaCajas.SelectedCells[0].RowIndex].Cells[0].Value.ToString());

            txtFecha.Text = tablaCajas.Rows[tablaCajas.SelectedCells[0].RowIndex].Cells[1].Value.ToString();

            txtNuevaCaja.ReadOnly = false;

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            //generar esta logica....
            //tener en cuenta con las cajas duplicadas del mismo dia ( para las tablas tambien)
            //grabar la caja como ultimo minuto del dia ? o updatear la vieja(hora mas temprana)  e insertar una nueva hora mas vieja?
            
        }
    }
}
