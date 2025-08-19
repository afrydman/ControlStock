using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.AdministracionRepository;
using Repository.Repositories.LocalRepository;
using Services;
using Services.AdministracionService;
using Services.IngresoService;
using Services.LocalService;
using Services.RetiroService;

namespace SharedForms.Estadisticas.RetirosIngresos
{
    public partial class retiros_ingresosxfecha<T> : Form where T : MovimientoEnCajaData
    {
        public retiros_ingresosxfecha()
        {
            InitializeComponent();
        }

        private const string NULLDESCRIPTION = "Sin Especificar";

        private void retirosxfecha_Load(object sender, EventArgs e)
        {
            setDatetimes();
            cargarLocales();
            cargoTipos();
            CargoTitulo();

        }

        private void CargoTitulo()
        {
            if (typeof(T) == typeof(RetiroData))
            {
                this.Text = "Retiros x Fecha";
            }
            else
            {
                this.Text = "Ingresos x Fecha";
            }
        }

        private void cargoTipos()
        {


            if (typeof(T) == typeof(RetiroData))
            {
                TipoRetiroData aux = new TipoRetiroData();
                aux.Description = NULLDESCRIPTION;
                var list = new TipoRetiroService().GetAll(true, false);
                list.Insert(0, aux);
                cmbTipo.DataSource = list;

            }
            else
            {
                TipoIngresoData aux = new TipoIngresoData();
                aux.Description = NULLDESCRIPTION;
                var list = new TipoIngresoService().GetAll(true, false);
                list.Insert(0, aux);
                cmbTipo.DataSource = list;
            }
            cmbTipo.DisplayMember = "Description";
        }

        private void setDatetimes()
        {
            desde.Value = desde.Value.Date.AddDays(-30);
        }

        private void cargarLocales()
        {
            var localService = new LocalService(new LocalRepository());
            cmbLocales.DataSource = localService.GetAll();
            cmbLocales.DisplayMember = "Codigo";

        }
        private void cargarItems()
        {


            decimal total = 0;
            txtFinal.Text = "0";
            if (desde.Value <= hasta.Value)
            {


                if (typeof (T) == typeof (RetiroData))
                {
                    RetiroService service = new RetiroService();



                    tablaRetiros.DataSource = null;

                    tablaRetiros.Rows.Clear();

                    List<RetiroData> retiros = service.GetByRangoFecha(desde.Value.Date, hasta.Value,
                        ((LocalData) cmbLocales.SelectedItem).ID, HelperService.Prefix, false);


                    if (cmbTipo.Text != NULLDESCRIPTION)
                    {
                        retiros = retiros.FindAll(r => r.TipoRetiro.ID == ((TipoRetiroData) cmbTipo.SelectedItem).ID);
                    }
                


                    foreach (RetiroData r in retiros)
                    {

                        tablaRetiros.Rows.Add();
                        int fila;
                        fila = tablaRetiros.RowCount - 1;
                        //id tipo Monto descripcion
                        tablaRetiros[0, fila].Value = r.ID;
                        tablaRetiros[1, fila].Value = r.Date;
                        tablaRetiros[2, fila].Value = r.TipoRetiro.Description;
                        tablaRetiros[3, fila].Value = r.Monto;
                        tablaRetiros[4, fila].Value = r.Description;
                        tablaRetiros[5, fila].Value = !r.Enable ? "Anulada" : "no anulada";

                        
                        if (!r.Enable){
                            tablaRetiros.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;
                            }
                        else
                        {
                            total += r.Monto;
                        }

                        tablaRetiros.ClearSelection();
                    }
                }
                else
                {
                    IngresoService service = new IngresoService();



                    tablaRetiros.DataSource = null;

                    tablaRetiros.Rows.Clear();

                    List<IngresoData> ingresos = service.GetByRangoFecha(desde.Value.Date, hasta.Value,
                        ((LocalData)cmbLocales.SelectedItem).ID, HelperService.Prefix, false);


                    if (cmbTipo.Text != NULLDESCRIPTION)
                    {
                        ingresos = ingresos.FindAll(r => r.TipoIngreso.ID == ((TipoIngresoData) cmbTipo.SelectedItem).ID);
                    }
                    foreach (IngresoData r in ingresos)
                    {

                        tablaRetiros.Rows.Add();
                        int fila;
                        fila = tablaRetiros.RowCount - 1;
                        //id tipo Monto descripcion
                        tablaRetiros[0, fila].Value = r.ID;
                        tablaRetiros[1, fila].Value = r.Date;
                        tablaRetiros[2, fila].Value = r.TipoIngreso.Description;
                        tablaRetiros[3, fila].Value = r.Monto;
                        tablaRetiros[4, fila].Value = r.Description;
                        tablaRetiros[5, fila].Value = !r.Enable ? "Anulada" : "no anulada";


                        if (!r.Enable)
                            tablaRetiros.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;
                        else
                        {
                            total += r.Monto;
                        }
                        tablaRetiros.ClearSelection();
                    }
                }
                txtFinal.Text = total.ToString();
            }
            else
            {
                MessageBox.Show("Ingrese un rango correcto de fecha", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void picker_ValueChanged(object sender, EventArgs e)
        {


        }

        private void hasta_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbLocales_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tablaRetiros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargarItems();
        }

        private void hasta_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
