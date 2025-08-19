using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Repository.Repositories.BancosRepository;
using Services.BancoService;

namespace Central.Tesoreria
{
    public partial class verCuenta : Form
    {
        CuentaData _c = null;
        public verCuenta(CuentaData c)
        {
            InitializeComponent();
            _c = c;
        }

        private void verCuenta_Load(object sender, EventArgs e)
        {
            cargarBancos();
            if (_c != null)
            {
                cargarCuenta(_c);
            }
        }

        private void cargarBancos()
        {
         
            var bancoService = new BancoService(new BancoRepository());
            List<BancoData> bs = bancoService.GetAll(true);

            BancoData aux = new BancoData();
            aux.Description = "Seleccione un banco";
            bs.Insert(0, aux);

            cmbBancos.DataSource = bs;
            cmbBancos.DisplayMember = "Description";
        }

        private void cargarCuenta(CuentaData c)
        {
            txtDescripcion.Text = c.Description;



            switch (c.TipoCuenta)
            {
                case TipoCuenta.Banco:
                    cmbTipo.SelectedText = "Banco";

                    cmbBancos.Text = c.Banco.Description;
                    txtsucursal.Text = c.Sucursal;
                    if (c.esCuentaCorriente)
                    {
                        cmbtipoBanco.SelectedText = "Cuenta Corriente";
                    }
                    else
                    {
                        cmbtipoBanco.SelectedText = "Caja Ahorro";
                    }

                    txtTitular.Text = c.Titular;
                    txtcuenta.Text = c.Cuenta;
                    txtCBU.Text = c.cbu;
                    txtsaldo.Text = c.Saldo.ToString();
                    txtlimite.Text = c.Descubierto.ToString();
                    txtTitular.Text = c.Titular;


                    break;
                case TipoCuenta.Cartera:

                    break;
                case TipoCuenta.Tarjeta:
                    break;
                case TipoCuenta.Otra:
                    cmbTipo.SelectedText = "Otra";
                    txtsaldo.Text = c.Saldo.ToString();
                    txtlimite.Text = c.Descubierto.ToString();

                    break;
                default:
                    break;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
