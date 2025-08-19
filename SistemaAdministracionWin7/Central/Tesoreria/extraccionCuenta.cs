using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO.BusinessEntities;

using Repository.Repositories.CuentaRepository;
using Repository.Repositories.MovimientoRepository;
using Services;
using Services.CuentaService;
using Services.MovimientoCuentaService;

namespace Central.Tesoreria
{
    public partial class extraccionCuenta : Form
    {
        public extraccionCuenta()
        {
            InitializeComponent();
        }

        private void extraccionCuenta_Load(object sender, EventArgs e)
        {
            cargoNumero();
            cargoCuentasBancarias();
            cargoCajas();
        }
        private void cargoNumero()
        {
             var movimientoCuentaService = new MovimientoCuentaService(new MovimientoRepository());

             lblnro.Text = movimientoCuentaService.GetNextNumberAvailable(true, HelperService.IDLocal, HelperService.Prefix);
        }
        private void cargoCajas()
        {
            var cuentaService = new CuentaService(new CuentaRepository());

            List<CuentaData> cuentasBanc = cuentaService.GetCuentasByTipo(TipoCuenta.Otra, true);

            CuentaData aux = new CuentaData();
            aux.Description = "Seleccione una Caja";
            cuentasBanc.Insert(0, aux);
            cmbCaja.DataSource = cuentasBanc;
            cmbCaja.DisplayMember = "Show";
        }
        private void cargoCuentasBancarias()
        {
            var cuentaService = new CuentaService(new CuentaRepository());
            List<CuentaData> cuentasBanc = cuentaService.GetCuentasByTipo(TipoCuenta.Banco, true);

            CuentaData aux = new CuentaData();
            aux.Description = "Seleccione una cuenta";
            cuentasBanc.Insert(0, aux);
            cmbCuenta.DataSource = cuentasBanc;
            cmbCuenta.DisplayMember = "Show";

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
            cmbCuenta.SelectedIndex = 0;
            cmbCaja.SelectedIndex = 0;
          
            txtImporte.Text = "";
            txtObs.Text = "";
            cargoNumero();
        }

        private bool validoTodo()
        {
            if (cmbCuenta.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione una cuenta origen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cmbCaja.SelectedIndex == 0 )
            {
                MessageBox.Show("Seleccione una caja destino", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtImporte.Text == "")
            {
                MessageBox.Show("Ingrese un Monto valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }



            return true;
        }
        private MovimientoCuentaData cargoMovimiento()
        {
            var cuentaService = new CuentaService();
            var movimientoCuentaService = new MovimientoCuentaService(new MovimientoRepository());
            MovimientoCuentaData m = new MovimientoCuentaData();
            m.Enable = true;


            m.cuentaOrigen = cuentaService.GetByID(((CuentaData)cmbCuenta.SelectedItem).ID);


            m.cuentaDestino = cuentaService.GetByID(((CuentaData)cmbCaja.SelectedItem).ID);
            m.Date = dateTimePicker1.Value;
            m.Prefix = HelperService.Prefix;
            m.Numero = Convert.ToInt32(movimientoCuentaService.GetNextNumberAvailable(false,HelperService.IDLocal,HelperService.Prefix));
            m.Description = txtObs.Text;
            m.Local.ID = HelperService.IDLocal;
            m.ID = Guid.NewGuid();
            m.Monto = HelperService.ConvertToDecimalSeguro(txtImporte.Text);
            return m;

        }
    }
}
