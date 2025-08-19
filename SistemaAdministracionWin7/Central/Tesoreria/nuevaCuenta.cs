using System;
using System.Collections.Generic;
using System.Transactions;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Repository.Repositories.BancosRepository;
using Repository.Repositories.CuentaRepository;
using Repository.Repositories.MovimientoRepository;
using Services;
using Services.BancoService;
using Services.CuentaService;
using Services.MovimientoCuentaService;

namespace Central.Tesoreria
{
    public partial class nuevaCuenta : Form
    {
        public nuevaCuenta()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

             if (dg == DialogResult.OK)
             {
                 if (valido())
                 {
                     insertarCuenta();
                     button1.Enabled = false;
                 }
             }
        }
        CuentaData _cuenta = null;
        public nuevaCuenta(CuentaData c)
        {
            
            InitializeComponent();
            _cuenta = c;
            

        }

        private void insertarCuenta()
        {
            CuentaData c = cargoCuenta();
            var cuentaService = new CuentaService(new CuentaRepository());
            var movimientoCuentaService = new MovimientoCuentaService(new MovimientoRepository());


             var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                try
                {
                    //
                    bool task = cuentaService.Insert(c);
                    if (c.Saldo > 0 && task)
                    {

                        MovimientoCuentaData m = new MovimientoCuentaData();
                        m.Enable = true;
                        m.cuentaDestino.ID = c.ID;
                        m.cuentaOrigen.ID = c.ID;
                        m.Date = DateTime.Now;
                        m.Prefix = HelperService.Prefix;
                        m.ID = Guid.NewGuid();
                        m.Local.ID = HelperService.IDLocal;
                        m.Monto = c.Saldo;
                        m.Numero =
                            Convert.ToInt32(movimientoCuentaService.GetNextNumberAvailable(false, HelperService.IDLocal,
                                HelperService.Prefix));
                        m.Description = "Creacion cuenta";
                        task = movimientoCuentaService.Insert(m);
                    }

                    if (task)
                    {
                        MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        trans.Complete();
                        //limparCampos();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {

                }
            }


        }

     

        private bool valido()
        {

            if (txtdescripcion.Text=="")
            {
                MessageBox.Show("Complete nombre de la cuenta a crear", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (radioBanco.Checked)
            {//es un banco
                if (cmbBancos.SelectedIndex == 0)
                {
                    MessageBox.Show("Complete el banco para continuar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl1.SelectedIndex = 1;
                    return false;
                }
                if (txtsucursal.Text=="")
                {
                    tabControl1.SelectedIndex = 1;
                    MessageBox.Show("Complete la sucursal del banco para continuar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (!radioCajaAhorro.Checked && !radioCuentaCorriente.Checked)
                {
                    tabControl1.SelectedIndex = 1;
                    MessageBox.Show("Complete tipo de cuenta bancaria para continuar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }   

                if (txtCBU.Text=="")
                {
                    tabControl1.SelectedIndex = 2;
                    MessageBox.Show("Complete CBU para continuar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (txtCBU.Text.Length != 22)
                {
                    tabControl1.SelectedIndex = 2;
                    MessageBox.Show("Complete los 22 digitos del CBU para continuar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                int n;
                bool isNumeric = int.TryParse(txtCBU.Text, out n);
                if (isNumeric)
                {
                    tabControl1.SelectedIndex = 2;
                    MessageBox.Show("Complete CBU para continuar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (txtTitular.Text == "")
                {
                    tabControl1.SelectedIndex = 2;
                    MessageBox.Show("Complete Titular para continuar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (txtcuenta.Text == "")
                {
                    tabControl1.SelectedIndex = 2;
                    MessageBox.Show("Complete Numero de cuenta para continuar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (txtlimite.Text=="")
                {
                    tabControl1.SelectedIndex = 2;
                    MessageBox.Show("Complete limite de cuenta para continuar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                    
                }
                if (txtsaldo.Text == "")
                {
                    tabControl1.SelectedIndex = 2;
                    MessageBox.Show("Complete saldo actual de cuenta para continuar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;

                }
                


                
            }

            return true;
        }

        private CuentaData cargoCuenta()
        {
            CuentaData c = new CuentaData();
            c.ID = Guid.NewGuid();
            c.Description = txtdescripcion.Text;
            c.Enable = true;
            c.Saldo = HelperService.ConvertToDecimalSeguro(txtsaldo.Text);
            if (radioBanco.Checked)
            {//es bnco
                c.Banco.ID = ((BancoData)cmbBancos.SelectedItem).ID;
                c.cbu = txtCBU.Text;
                c.Cuenta = txtcuenta.Text;
                c.Descubierto =HelperService.ConvertToDecimalSeguro(txtlimite.Text);
                c.esCuentaCorriente = radioCuentaCorriente.Checked;
                c.Titular = txtTitular.Text;
                c.Sucursal = txtsucursal.Text;
                c.TipoCuenta = TipoCuenta.Banco;
            }
            if (radioCartera.Checked)
            {//cartera
                c.TipoCuenta = TipoCuenta.Cartera;
            }
            if (radioOtra.Checked)
            {//es otra
                c.TipoCuenta = TipoCuenta.Otra;

            }

            return c;
        }

        private void radioBanco_CheckedChanged(object sender, EventArgs e)
        {
            groupBanco.Enabled = radioBanco.Checked;
            GroupTipoCuentaBancaria.Enabled = radioBanco.Checked;
            txtCBU.Enabled= radioBanco.Checked;
            txtTitular.Enabled= radioBanco.Checked;
            txtlimite.Enabled= radioBanco.Checked;
            txtcuenta.Enabled = radioBanco.Checked;
            if (!radioBanco.Checked)
            {
                txtCBU.Text = "";
                txtTitular.Text = "";
                    txtlimite.Text = "";
                    txtcuenta.Text = "";
            }


        }

        private void nuevaCuenta_Load(object sender, EventArgs e)
        {
            
            cargarBancos();
            radioBanco.Checked = true;

            verificarExistenciaCajaChica();


            if (_cuenta!=null)
            {
                
                button1.Enabled = false;
            }
        }

        private void verificarExistenciaCajaChica()
        {
            var cuentaService = new CuentaService(new CuentaRepository());
            List<CuentaData> c = cuentaService.GetCuentasByTipo(TipoCuenta.Otra, true);
            if (c != null && c.Count > 0)
                radioOtra.Enabled = false;

        }

      

        private void cargarBancos()
        {
            //List<bancoData> bs = BusinessComponents.banco.getAll();
            var bancoService = new BancoService(new BancoRepository());
            List<BancoData> bs = bancoService.GetAll(true);

            BancoData aux = new BancoData();
            aux.Description = "Seleccione un banco";
            bs.Insert(0, aux);

            cmbBancos.DataSource = bs;
            cmbBancos.DisplayMember = "Description";
        }
    }
}
