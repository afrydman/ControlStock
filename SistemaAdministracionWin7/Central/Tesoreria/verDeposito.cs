using System;
using System.Windows.Forms;
using DTO.BusinessEntities;

using Repository.Repositories.AdministracionRepository;
using Repository.Repositories.MovimientoRepository;
using Services.IngresoService;
using Services.MovimientoCuentaService;
using Services.RetiroService;

namespace Central.Tesoreria
{
    public partial class verDeposito : Form
    {
        MovimientoCuentaData m = null;
        public verDeposito(MovimientoCuentaData _m)
        {
            InitializeComponent();
            m = _m;
        }

        private void verDeposito_Load(object sender, EventArgs e)
        {


            if (m!=null)
            {
                cargardeposito(m); 
            }
        }

        private void cargardeposito(MovimientoCuentaData m)
        {
            if (!m.Enable)
            {
                button3.Enabled = false;
            }

            if (m.cheque!=null && m.cheque.ID != Guid.Empty)
            {
                radioCheque.Checked = true;
                txtcheque.Text = m.cheque.Show;

            }
            else
            {
                radioEfectivo.Checked = true;
                txtorigen.Text = m.cuentaOrigen.Show;
            }
            txtdestino.Text = m.cuentaDestino.Show;
            dateTimePicker1.Value = m.Date;
            lblnro.Text = m.Show;
            txtObs.Text = m.Description;
            txtImporte.Text = m.Monto.ToString();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
                DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                var movimientoCuentaService = new MovimientoCuentaService(new MovimientoRepository());
                var ingresoService = new IngresoService(new IngresoRepository());
                var retiroService = new RetiroService(new RetiroRepository());

                if (dg == DialogResult.OK)
                {
                    bool task = movimientoCuentaService.Disable(m.ID);
                    if (task)
                    {
                        MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (m.Date.Date==DateTime.Now.Date)
                        {
                            DialogResult dg2 = MessageBox.Show("Desea anular el retiro/ingreso asociado?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            if (dg2 == DialogResult.OK)
                            {
                                if (m.cuentaOrigen.TipoCuenta == TipoCuenta.Banco)
                                {
                                    ingresoService.Disable(new IngresoData(m.ID));
                                }
                                else if (m.cuentaOrigen.TipoCuenta == TipoCuenta.Otra)
                                {
                                    retiroService.Disable(new RetiroData(m.ID));
                                }
                            
                            }
                        }
                        button3.Enabled = false;

                        refreshMovimientos();

                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }

        private void refreshMovimientos()
        {
            foreach (Form hijo in this.MdiParent.MdiChildren)
            {
                if (hijo.GetType() == typeof(movimientosCuenta))
                {

                    ((movimientosCuenta)hijo).refresh2();
                }

            }
        }
    }
}
