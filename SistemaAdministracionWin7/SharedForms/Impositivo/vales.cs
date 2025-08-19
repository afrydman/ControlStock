using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.AdministracionRepository;
using Repository.Repositories.ValeRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.IngresoService;
using Services.ValeService;
using Services.VentaService;
using SharedForms.Ventas;

namespace SharedForms.Impositivo
{
    public partial class vales : Form
    {
        public vales()
        {
            InitializeComponent();
        }

        private void vales_Load(object sender, EventArgs e)
        {
            desde.Value = desde.Value.Date.AddDays(-90);
        }

        private void cargarVales()
        {
            var valeService = new ValeService(new ValeRepository());

            List<valeData> vales = valeService.GetByRangoFecha(desde.Value, hasta.Value.Date.AddDays(1), HelperService.IDLocal, HelperService.Prefix);
            foreach (valeData v in vales)
            {

                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                tabla[0, fila].Value = v.ID;
                tabla[1, fila].Value = v.Show;
                tabla[2, fila].Value =  HelperService.convertToFechaHoraConFormato(v.Date);
                tabla[3, fila].Value = v.EsCambio?"Cambio":"Sena Cliente";
                tabla[4, fila].Value = v.Monto;
                tabla[5, fila].Value = !v.Enable?"Anulado":"No Anulado";
                tabla[6, fila].Value = v.idAsoc;

                if (!v.Enable)
                    tabla.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;

                
            }


        }

        private void tabla_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var ingresoService = new IngresoService(new IngresoRepository());
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            if (tabla.SelectedCells.Count > 0)
            {
                if (tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[3].Value.ToString().ToLower()=="cambio")
                {
                    VentaData v = ventaService.GetByID(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[6].Value.ToString()));

                    padreBase.AbrirForm(new mostrarVentaMenor(v), this.MdiParent, true);
                }
                else
                {
                    IngresoData ing = ingresoService.GetByID(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[6].Value.ToString()));

                    MessageBox.Show("El vale se genero por una Sena de Cliente \r\n Descripcion: " + ing.Description, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                 
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargarVales();
        }
    }
}
