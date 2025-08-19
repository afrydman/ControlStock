using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Repository.ChequeraRepository;
using Repository.ChequeRepository;
using Repository.Repositories.BancosRepository;
using Services;
using Services.BancoService;
using Services.ChequeraService;
using Services.ChequeService;

namespace Central.Tesoreria
{
    public partial class nuevoCheque : Form
    {
        public nuevoCheque()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (valido())
            {
                ChequeData c = cargoCheque();
                var chequeService = new ChequeService(new ChequeRepository());
                var chequeraService = new ChequeraService(new ChequeraRepository());
                bool taskDone = chequeService.Insert(c);
                if (taskDone)
                {
                    if (cmbOrigen.SelectedIndex > 0)
	                {
                        chequeraService.SetearSiguiente(((ChequeraData)cmbOrigen.SelectedItem));                                	 
	                }

                    MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpioControles();
                    foreach (Form hijo in this.MdiParent.MdiChildren)
                    {
                        if (hijo.GetType() == typeof(NuevoRecibo))
                        {

                            ((NuevoRecibo)hijo).refresh2();
                        }

                    }
                    cargarInterno(); 

                }
                else
                {
                    MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void limpioControles()
        {
            cmbOrigen.SelectedIndex = 0;
            emision.Value = DateTime.Now;
            cobro.Value = DateTime.Now;
            txtnroCheque.Text = "";
            txtTitularCuenta.Text = "";
            txtmonto.Text = "";
            txtObs.Text = "";
            txtInterno.Text = "0";
        }

        private ChequeData cargoCheque()
        {

            ChequeData c = new ChequeData();
            c.Enable = true;
            c.BancoEmisor.ID = ((BancoData)cmbBancos.SelectedItem).ID;

            c.Date = ingreso.Value;
            c.FechaCobro = cobro.Value;
            c.FechaEmision = emision.Value;
            c.ID = Guid.NewGuid();
            
            c.Chequera.ID = cmbOrigen.SelectedIndex > 0?((ChequeraData)cmbOrigen.SelectedItem).ID:Guid.Empty;
            c.EstadoCheque = EstadoCheque.Creado;
            c.Interno = Convert.ToInt32(txtInterno.Text);
            c.Local.ID = HelperService.IDLocal;
            c.Monto = HelperService.ConvertToDecimalSeguro(txtmonto.Text);
            c.Numero = txtnroCheque.Text;
            c.Description = txtObs.Text;
            c.Titular = txtTitularCuenta.Text;
            return c;
            
        }

        private bool valido()
        {
            if (txtInterno.Text=="")
            {
                MessageBox.Show("Ingrese un nro interno valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;  
            }
            try
            {
                int aux = Convert.ToInt32(txtInterno.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese un nro internoValido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;  
            }

            if (cmbOrigen.SelectedIndex == 0) 
            { //solo se verifica para cheques de tercero.
                var chequeService = new ChequeService(new ChequeRepository());
                if (!chequeService.InternNumberIsValid(txtInterno.Text))
                {
                    MessageBox.Show("Este nro interno ya ha sido utilizado. \r Cierre y abra la ventana para obtener un nro valido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;  
                }
            }

            if (cmbBancos.SelectedIndex==0)
            {
                MessageBox.Show("Seleccione un banco", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;  
            }

            if (txtnroCheque.Text =="")
            {
              MessageBox.Show("ingrese un Numero de cheque", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;  
            }
            if (txtTitularCuenta.Text=="")
            {
                MessageBox.Show("Ingrese un titular de cuenta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
            }
            if (txtmonto.Text=="")
            {
                MessageBox.Show("Ingrese un Monto valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
            }
            try
            {
                decimal aux2 = HelperService.ConvertToDecimalSeguro(txtmonto.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese un Monto valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }



            var chequeraService = new ChequeraService(new ChequeraRepository());

            if (radioDiferido.Checked && cobro.Value<emision.Value)
            {

                MessageBox.Show("La fecha de Cobro no puede ser menor a la de emision", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbOrigen.SelectedIndex>0)
            {//es de una chequera propia
                ChequeraData michequera = chequeraService.GetByID(((ChequeraData)cmbOrigen.SelectedItem).ID);
                
                if (michequera.Cuenta.Banco.ID!=((BancoData)cmbBancos.SelectedItem).ID)
                {
                    MessageBox.Show("Selecciono un banco diferente al de su cuenta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Convert.ToInt32(txtnroCheque.Text)<Convert.ToInt32(michequera.Primero))
                {
                    MessageBox.Show("Ingreso un Numero de cheque menor al permitido en su chequera", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Convert.ToInt32(txtnroCheque.Text) > Convert.ToInt32(michequera.Ultimo))
                {
                    MessageBox.Show("Ingreso un Numero de cheque mayor al permitido en su chequera", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (chequeraService.existeEsteCheque(michequera.ID, txtnroCheque.Text))
                {
                    MessageBox.Show("Ingreso un Numero de cheque ya utilizado con esta chequera", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
               
            }
            else
            {//es de terceros
                
            }



            return true;
        }

        private void cobro_ValueChanged(object sender, EventArgs e)
        {

            radioDiferido.Checked = cobro.Value != emision.Value;
            
        }

        private void radioComun_CheckedChanged(object sender, EventArgs e)
        {
            if (radioComun.Checked)
            {
                cobro.Value = emision.Value;
            }
        }

        private void nuevoCheque_Load(object sender, EventArgs e)
        {
            cargarOrigenes();
            cargarBancos();
            
        }

        private void cargarInterno()
        {
            var chequeService = new ChequeService(new ChequeRepository());
            txtInterno.Enabled = true;
            txtInterno.Text = chequeService.GetNextNumberAvailable().ToString();
        }

        private void cargarOrigenes()
        {

            var chequeraService = new ChequeraService(new ChequeraRepository());

            List<ChequeraData> chequeras = chequeraService.GetAll(true);


            ChequeraData aux = new ChequeraData();
            aux.Description = "Tercero";
            chequeras.Insert(0, aux);



            cmbOrigen.DataSource = chequeras;
            cmbOrigen.DisplayMember = "Show";
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

        private void cmbOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOrigen.SelectedIndex>0)
            {//osea que es una chequera.

                cargarDatosCuenta(((ChequeraData)cmbOrigen.SelectedItem).ID);
                bloquearControles(false);
                cargarinternoPropio();
            }
            else
            {
                bloquearControles(true);
                limpioControles();
                cargarInterno();
            }
        }

        private void cargarinternoPropio()
        {
            
            txtInterno.Enabled = false;
            txtInterno.Text = "1";
        }

        private void bloquearControles(bool p)
        {
            cmbBancos.Enabled = p;
            txtTitularCuenta.Enabled= p;
            
        }

        private void cargarDatosCuenta(Guid chequera)
        {
            var chequeraService = new ChequeraService(new ChequeraRepository());

            ChequeraData c = chequeraService.GetByID(chequera);

            cmbBancos.Text = c.Cuenta.Banco.Description;
            txtTitularCuenta.Text = c.Cuenta.Titular;
            txtnroCheque.Text = c.Siguiente;
        }
    }
}
