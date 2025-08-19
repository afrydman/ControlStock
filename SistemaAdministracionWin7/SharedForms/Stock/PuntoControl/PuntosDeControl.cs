using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using DTO.BusinessEntities;
using Services;
using Services.PuntoControlService;

namespace SharedForms.Estadisticas.Stock.PuntoControl
{
    public partial class PuntosDeControl : Form
    {
        public PuntosDeControl()
        {
            InitializeComponent();
        }

        private void PuntosDeControl_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {


            limpiarTabla();
            cargarPuntos();

        }

        private void cargarPuntos()
        {
            var puntoControlService = new PuntoControlService();
            DateTime fecha1 = HelperDTO.BEGINNING_OF_TIME_DATE;
            DateTime fecha2 = HelperDTO.END_OF_TIME;

            if (checkGenerado.Checked)
            {
                fecha1 = generadoDesde.Value;
                fecha2 = generadoHasta.Value;
            }


            var puntos = puntoControlService.GetByRangoFecha(fecha1, fecha2, HelperService.IDLocal, HelperService.Prefix,
                checkAnulados.Checked);

            int fila;
            foreach (PuntoControlStockData punto in puntos)
            {
                tabla.Rows.Add();
                fila = tabla.RowCount - 1;

                tabla[0, fila].Value = punto.ID;
                tabla[1, fila].Value = punto.NumeroCompleto;
                tabla[2, fila].Value = HelperService.convertToFechaHoraConFormato(punto.Date);
                tabla[3, fila].Value = punto.Local.Description;
                tabla[4, fila].Value = punto.Enable ? "No Anulado" : "Anulado";

                tabla.ClearSelection();
            }
        }

        private void limpiarTabla()
        {
            tabla.Rows.Clear();
            tabla.ClearSelection();
        }

        private void tabla_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var puntoControlService = new PuntoControlService();
            if (tabla.SelectedCells.Count > 0)
            {
                PuntoControlStockData v = puntoControlService.GetByID(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));

                padreBase.AbrirForm(new verPuntodeControl(v), this.MdiParent, true);
            }
        }
    }
}
