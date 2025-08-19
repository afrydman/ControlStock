using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO.BusinessEntities;

using Repository.ChequeraRepository;
using Repository.ChequeRepository;
using Repository.Repositories.CuentaRepository;
using Repository.Repositories.MovimientoRepository;
using Services;
using Services.ChequeraService;
using Services.ChequeService;
using Services.CuentaService;
using Services.MovimientoCuentaService;

namespace Central.Tesoreria
{
    public partial class pagoCheque : Form
    {
        public pagoCheque()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pagoCheque_Load(object sender, EventArgs e)
        {
            cargoNumero();
            cargarCheques();
            cargarCuentas();

        }

        private void cargarCuentas()
        {
            var cuentaService = new CuentaService(new CuentaRepository());
            List<CuentaData> cuentas = cuentaService.GetAll(true);

            cuentas = cuentas.FindAll(delegate(CuentaData c) { return c.TipoCuenta == TipoCuenta.Banco && c.esCuentaCorriente; });


            cmbCuenta.DataSource = cuentas;
            cmbCuenta.DisplayMember = "Show";
        }

        private void cargarCheques()
        {
            var chequeraService = new ChequeraService(new ChequeraRepository());
            var chequeService = new ChequeService(new ChequeRepository());


            List<ChequeraData> chequerasPropias = chequeraService.GetAll(true);

            List<ChequeData> cheques = new List<ChequeData>();
            List<EstadoCheque> l = new List<EstadoCheque>();
            l.Add(EstadoCheque.Entregado);
            foreach (ChequeraData chequera in chequerasPropias)
            {

                cheques.AddRange(chequeService.GetByChequera(chequera.ID, true, l,true));
            }
            ChequeData aux = new ChequeData();
            aux.BancoEmisor.Description = "Seleccione un cheque";
            aux.Monto = 0;
            cheques.Insert(0, aux);
            cmbCheques.DataSource = cheques;
            cmbCheques.DisplayMember = "Show";


        }

        private void cmbCheques_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCheques.SelectedIndex>0)
            {
                ChequeData c = (ChequeData)cmbCheques.SelectedItem;

                fechaCobro.Value = c.FechaCobro;
                txtImporte.Text = c.Monto.ToString();
                cmbCuenta.SelectedText = c.Chequera.Cuenta.Show;
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
            cargoNumero();
            cargarCheques();
            cargarCuentas();
            txtImporte.Text = "";
            txtObs.Text = "";


        }
        private void cargoNumero()
        {
            var movimientoCuentaService = new MovimientoCuentaService(new MovimientoRepository());

            lblnro.Text = movimientoCuentaService.GetNextNumberAvailable(true, HelperService.IDLocal, HelperService.Prefix);
        }


        private MovimientoCuentaData cargoMovimiento()
        {
            var movimientoCuentaService = new MovimientoCuentaService(new MovimientoRepository());

            MovimientoCuentaData m = new MovimientoCuentaData();
            m.Enable = true;
           
                m.cheque.ID = ((ChequeData)cmbCheques.SelectedItem).ID;


                m.cuentaOrigen.ID = ((CuentaData)cmbCuenta.SelectedItem).ID;


                m.cuentaDestino.ID = Guid.Empty;
            m.Date = fecha.Value;
            m.Prefix = HelperService.Prefix;
            m.Numero = Convert.ToInt32( movimientoCuentaService.GetNextNumberAvailable(false, HelperService.IDLocal, HelperService.Prefix));
            m.Description = txtObs.Text;
            m.Local.ID = HelperService.IDLocal;
            m.ID = Guid.NewGuid();
            m.Monto = HelperService.ConvertToDecimalSeguro(txtImporte.Text);
            return m;
        }

        private bool validoTodo()
        {
            if (cmbCheques.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un cheque", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
    }
}
