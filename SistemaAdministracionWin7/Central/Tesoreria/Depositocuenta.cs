using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO.BusinessEntities;

using Repository.ChequeRepository;
using Repository.Repositories.CuentaRepository;
using Repository.Repositories.MovimientoRepository;
using Services;
using Services.ChequeService;
using Services.CuentaService;
using Services.MovimientoCuentaService;

namespace Central.Tesoreria
{
    public partial class Depositocuenta : Form
    {
        public Depositocuenta()
        {
            InitializeComponent();
        }

        private void Depositocuenta_Load(object sender, EventArgs e)
        {
            cargoNumero();
            cargoCuentasBancarias();
            cargoCajas();
            cargoCheques();

        }

        private void cargoNumero()
        {
            var movimientoCuentaService = new MovimientoCuentaService(new MovimientoRepository());

            lblnro.Text = movimientoCuentaService.GetNextNumberAvailable(true, HelperService.IDLocal, HelperService.Prefix);
        }

        private void cargoCheques()
        {
            var chequeService = new ChequeService(new ChequeRepository());
            List<EstadoCheque> estados = new List<EstadoCheque>();
            estados.Add(EstadoCheque.EnCartera);
            List<ChequeData> cs = chequeService.GetChequesTercero(true, estados, true);
            ChequeData aux = new ChequeData();
            aux.BancoEmisor.Description = "Seleccione un cheque";
            aux.Monto = 0;
            cs.Insert(0, aux);
            cmbCheques.DataSource = cs;
            cmbCheques.DisplayMember = "Show";
        
        }

        private void cargoCajas()
        {
            var cuentaService = new CuentaService(new CuentaRepository());

            List<CuentaData> cuentasBanc = cuentaService.GetCuentasByTipo(TipoCuenta.Otra, true);

            CuentaData aux = new CuentaData();
            aux.Description = "Seleccione una Caja";
            cuentasBanc.Insert(0, aux);
            cmbCajaOrigen.DataSource = cuentasBanc;
            cmbCajaOrigen.DisplayMember = "Show";
        }

        private void cargoCuentasBancarias()
        {
            var cuentaService = new CuentaService(new CuentaRepository());

            List<CuentaData> cuentasBanc = cuentaService.GetCuentasByTipo(TipoCuenta.Banco, true);

            CuentaData aux = new CuentaData();
            aux.Description = "Seleccione una cuenta";
            cuentasBanc.Insert(0, aux);
            cmbCuentaDestino.DataSource = cuentasBanc;
            cmbCuentaDestino.DisplayMember = "Show";

        }

        private void radioEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioEfectivo.Checked)
            {
                cmbCajaOrigen.SelectedIndex = 0;
                cmbCajaOrigen.Enabled = true;
                txtImporte.Enabled = true;
                txtImporte.Text = "";
                cmbCheques.SelectedIndex= 0;
                cmbCheques.Enabled = false;

            }
            else
            {
                cmbCajaOrigen.SelectedIndex = 0;
                cmbCajaOrigen.Enabled = false;
                txtImporte.Enabled = false;
                txtImporte.Text = "";
                cmbCheques.SelectedIndex = 0;
                cmbCheques.Enabled = true;
            }
        }

        private void cmbCheques_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCheques.SelectedIndex > -1)
            {
                txtImporte.Text = ((ChequeData)cmbCheques.SelectedItem).Monto.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
             DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

             var movimientoCuentaService = new MovimientoCuentaService(new MovimientoRepository());


             if (dg == DialogResult.OK)
             {
                 if (validoTodo())
                 {
                     MovimientoCuentaData mov = cargoMovimiento();
                     bool task = movimientoCuentaService.Insert(mov);
                     if (task)
                     {
                         MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         if (mov.cuentaOrigen.TipoCuenta==TipoCuenta.Banco)
                         {
                             MessageBox.Show("Ingreso generado automaticamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                         else if (mov.cuentaOrigen.TipoCuenta == TipoCuenta.Otra)
                         {
                             MessageBox.Show("Retiro generado automaticamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                         limpioControles();

                     }
                     else
                     {
                         MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                 }
             }
        }

        private void limpioControles()
        {
            cmbCuentaDestino.SelectedIndex = 0;
            cmbCajaOrigen.SelectedIndex = 0;
            cargoCheques();
            txtImporte.Text = "";
            txtObs.Text = "";
            cargoNumero();
        }

        private MovimientoCuentaData cargoMovimiento()
        {
            var movimientoCuentaService = new MovimientoCuentaService(new MovimientoRepository());
            var cuentaService = new CuentaService();
            MovimientoCuentaData m = new MovimientoCuentaData();
            m.Enable = true;
            if (radioCheque.Checked)
            {
                m.cheque.ID = ((ChequeData)cmbCheques.SelectedItem).ID;

            }
            else
            {
                m.cuentaOrigen = cuentaService.GetByID(((CuentaData)cmbCajaOrigen.SelectedItem).ID);

            }

            m.cuentaDestino = cuentaService.GetByID(((CuentaData)cmbCuentaDestino.SelectedItem).ID);
            m.Date = dateTimePicker1.Value;
            m.Prefix = HelperService.Prefix;
            m.Numero = Convert.ToInt32(movimientoCuentaService.GetNextNumberAvailable(false, HelperService.IDLocal, HelperService.Prefix));
            m.Description = txtObs.Text;
            m.Local.ID = HelperService.IDLocal;
            m.ID = Guid.NewGuid();
            
            m.Monto = HelperService.ConvertToDecimalSeguro(txtImporte.Text);
            return m;

        }

        private bool validoTodo()
        {
            if (cmbCuentaDestino.SelectedIndex==0)
            {
                MessageBox.Show("Seleccione una cuenta destino", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cmbCajaOrigen.SelectedIndex == 0 && radioEfectivo.Checked)
            {
                MessageBox.Show("Seleccione una cuenta origen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtImporte.Text=="")
            {
                MessageBox.Show("Ingrese un Monto valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }



            return true;
        }

        private void radioCheque_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbCuentaDestino_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
