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
using Repository.Repositories.PersonalRepository;
using Services;
using Services.PersonalService;
using Services.PuntoControlService;
using SharedForms.Stock;

namespace SharedForms.Estadisticas.Stock.PuntoControl
{
    public partial class verPuntodeControl : stockBase
    {
        private PuntoControlStockData _puntoControl = null;
        public verPuntodeControl(PuntoControlStockData puntoControl)
        {
            InitializeComponent();
            _puntoControl = puntoControl;
        }

        private void verPuntodeControl_Load(object sender, EventArgs e)
        {
            cargarVendedores();


            setClientGUI();

            if (_puntoControl != null)
                cargarControles(_puntoControl);

        }

        private void cargarControles(PuntoControlStockData pc)
        {

            dateTimePicker1.Value = pc.Date;
            txtObs.Text = pc.Description;
            button3.Enabled = pc.Enable;
            lblnro.Text = pc.NumeroCompleto;

            cargoDetallesEnTabla(pc.Children, tabla, new TextBox());

        }

        private void setClientGUI()
        {
            if (HelperService.talleUnico)
            {
                txtPongotalle.Text = "0";
                txtPongotalle.ReadOnly = true;
                txtPongotalle.Visible = false;
                lbltalle.Visible = false;
                tabla.Columns[4].Visible = false;


            }


            if (HelperService.haymts)
            {
                lbltalle.Text = "Mts";
                tabla.Columns[4].HeaderText = "Mts";
            }
            else
            {
                tabla.Columns[6].Visible = false;
            }



        }


        private void cargarVendedores()
        {
            var personalService = new PersonalService(new PersonalRepository());

            List<PersonalData> vendedores = personalService.GetAll();

            cmbvendedores.DataSource = vendedores;
            cmbvendedores.DisplayMember = "nombrecontacto";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dg == DialogResult.OK)
            {
                var puntoControlService = new PuntoControlService();

                bool task = puntoControlService.Disable(_puntoControl.ID);

                if (task)
                {
                    MessageBox.Show("Anulado Exitosamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button3.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Ocurrio un error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }




            }
        }
    }
}
