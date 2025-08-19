using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Repository.ChequeraRepository;
using Repository.ChequeRepository;
using Repository.Repositories.BancosRepository;
using Repository.Repositories.MovimientoRepository;
using Repository.Repositories.OrdenPagoRepository;
using Repository.Repositories.ReciboRepository;
using Services;
using Services.BancoService;
using Services.ChequeraService;
using Services.ChequeService;
using Services.MovimientoCuentaService;
using Services.OrdenPagoService;
using Services.ReciboService;

namespace Central.Tesoreria
{
    public partial class verCheque : Form
    {
        ChequeData _c = null;
        public verCheque(ChequeData c)
        {
            InitializeComponent();
            _c = c;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
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
        }

        private ChequeData cargoCheque()
        {

            ChequeData c = new ChequeData();
            c.Enable = true;
            c.BancoEmisor.ID = ((BancoData)cmbBancos.SelectedItem).ID;
            c.EstadoCheque = EstadoCheque.EnCartera;
            c.Date = DateTime.Now;
            c.FechaCobro = cobro.Value;
            c.FechaEmision = emision.Value;
            c.ID = Guid.NewGuid();
            
            c.Chequera.ID = cmbOrigen.SelectedIndex > 0?((ChequeraData)cmbOrigen.SelectedItem).ID:Guid.Empty;
            c.Interno = Convert.ToInt32(lblInterno.Text);
            c.Local.ID = HelperService.IDLocal;
            c.Monto = HelperService.ConvertToDecimalSeguro(txtmonto.Text);
            c.Numero = txtnroCheque.Text;
            c.Description = txtObs.Text;
            c.Titular = txtTitularCuenta.Text;
            return c;
            
        }

        private bool valido()
        {


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
            cargarInterno();

            if (_c!=null)
            {
                cargoControles(_c);
                cargoMovimientos(_c);
            }
        }

        private void cargoMovimientos(ChequeData c)
        {
            tabla.Rows.Clear();
            //emicion o recepcion del cheque
            var reciboService = new ReciboService(new ReciboRepository(), new ReciboDetalleRepository());


            tabla.Rows.Add();
            int fila;
            fila = tabla.RowCount - 1;
            //id nombre Codigo
            tabla[0, fila].Value = HelperService.convertToFechaConFormato(c.Date);

            tabla[1, fila].Value = (c.Chequera != null && c.Chequera.ID != Guid.Empty) ? "Generacion Interna del cheque, chequera :" + c.Chequera.Show : "Generacion Interna del cheque (tercero) ";
            

            // recibir de un cliente
            if (c.Chequera ==null)
            {// es de un tercero, busco cuando me lo dieron
                ReciboData r = reciboService.GetReciboDeCheque(c.ID);
                if (r!=null)
                {
                    
               
                tabla.Rows.Add();
                
                fila = tabla.RowCount - 1;
                //id nombre Codigo
                tabla[0, fila].Value = r.Date;

                tabla[1, fila].Value = "Recibo n" + r.NumeroCompleto + " Cliente: " + r.tercero.RazonSocial;
                }
            }
            // o fue  
            // entregado 
            //o fue depositado 
            //o fue rechazado / anulado
            var OrdenPagoService = new OrdenPagoService(new OrdenPagoRepository(), new OrdenPagoDetalleRepository());

            var movimientoCuentaService = new MovimientoCuentaService(new MovimientoRepository());

            switch (c.EstadoCheque)
            {
                case EstadoCheque.EnCartera:
                   
                    tabla.Rows.Add();
                
                    fila = tabla.RowCount - 1;
                    //id nombre Codigo
                    tabla[0, fila].Value = HelperService.convertToFechaConFormato(DateTime.Now);

                    tabla[1, fila].Value = "En Cartera";
                    break;
                case  EstadoCheque.Anulado  :
                        tabla.Rows.Add();
                        fila = tabla.RowCount - 1;
                        tabla[0, fila].Value = HelperService.convertToFechaConFormato(c.FechaAnuladooRechazado);
                        tabla[1, fila].Value = c.EstadoCheque.ToString();
                    break;
                case  EstadoCheque.Rechazado:
                    tabla.Rows.Add();
                    fila = tabla.RowCount - 1;
                    tabla[0, fila].Value = HelperService.convertToFechaConFormato(c.FechaAnuladooRechazado);
                    tabla[1, fila].Value = c.EstadoCheque.ToString();
                    break;
                case EstadoCheque.Depositado:
                    MovimientoCuentaData m = movimientoCuentaService.GetbyCheque(c.ID);
                    tabla.Rows.Add();
                        fila = tabla.RowCount - 1;
                        tabla[0, fila].Value = HelperService.convertToFechaConFormato(m.Date);
                        tabla[1, fila].Value = "Depositado en Cuenta " + m.cuentaDestino.Show;
                    break;
                case EstadoCheque.Entregado:
                    OrdenPagoData ordenPago = OrdenPagoService.GetOrdenQueEntregoCheque(c.ID);
                        tabla.Rows.Add();
                        fila = tabla.RowCount - 1;
                        tabla[0, fila].Value = HelperService.convertToFechaConFormato(ordenPago.Date);
                        tabla[1, fila].Value = "Entregado en O.Pago " + ordenPago.NumeroCompleto + " Proveedor:" +  ordenPago.Tercero.RazonSocial;
                    break;
                case EstadoCheque.Acreditado:
                    
                    break;
                case EstadoCheque.EntregadoSinOpago:
                     tabla.Rows.Add();
                        fila = tabla.RowCount - 1;
                    tabla[0, fila].Value = HelperService.convertToFechaConFormato(c.FechaAnuladooRechazado);
                    tabla[1, fila].Value = "Entregado  ";
                    break;
                
               
            }
     

        }

        private void cargoControles(ChequeData c)
        {
            var chequeraService = new ChequeraService(new ChequeraRepository());
            if (c.Chequera !=null && c.Chequera.ID != Guid.Empty)
            {
                if (c.Chequera.Description == null || c.Chequera.Description == "")
                {
                    c.Chequera = chequeraService.GetByID(c.Chequera.ID);
                }
                cmbOrigen.Text = c.Chequera.Show;
            }
            lblInterno.Text = c.Interno.ToString();
            emision.Value = c.FechaEmision;
            ingreso.Value = c.Date;
            cobro.Value = c.FechaCobro;
            cmbBancos.Text = c.BancoEmisor.Description;
            txtTitularCuenta.Text = c.Titular;
            txtObs.Text = c.Description;
            txtnroCheque.Text = c.Numero;
            txtmonto.Text = c.Monto.ToString();

            if (c.EstadoCheque == EstadoCheque.Anulado || c.EstadoCheque == EstadoCheque.Acreditado || c.EstadoCheque == EstadoCheque.Depositado || c.EstadoCheque == EstadoCheque.Entregado || c.EstadoCheque == EstadoCheque.EntregadoSinOpago || c.EstadoCheque == EstadoCheque.Rechazado || c.EstadoCheque==EstadoCheque.EnCartera)
            {
                entregasVarias.Enabled = false;
            }
        }

        private void cargarInterno()
        {
            var chequeService = new ChequeService(new ChequeRepository());

            lblInterno.Text = chequeService.GetNextNumberAvailable().ToString(); ;
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
                
            }
        }

        private void cargarDatosCuenta(Guid chequera)
        {
            var chequeraService = new ChequeraService(new ChequeraRepository());
            ChequeraData c = chequeraService.GetByID(chequera);

            cmbBancos.Text = c.Cuenta.Banco.Description;
            txtTitularCuenta.Text = c.Cuenta.Titular;
            txtnroCheque.Text = c.Siguiente;
        }

        private void button2_Click(object sender, EventArgs e)
        {
             DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion de Rechazar?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
             var chequeService = new ChequeService(new ChequeRepository());
             if (dg == DialogResult.OK)
             {//marcar como rechazado

                 if (chequeService.MarcarComo(_c,EstadoCheque.Rechazado,fechaOut.Value,txtObsOut.Text)) {

                     MessageBox.Show("Operacion realizada satifactoriamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     cargoMovimientos(_c);
                     entregasVarias.Enabled = false;
                 }
                 else
                 {
                     MessageBox.Show("Error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     
                 }
                 
             }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion de Anular?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dg == DialogResult.OK)
            {//marcar como anulado
                var chequeService = new ChequeService(new ChequeRepository());
             
                if (chequeService.MarcarComo(_c, EstadoCheque.Anulado, fechaOut.Value, txtObsOut.Text))
                {

                    MessageBox.Show("Operacion realizada satifactoriamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargoMovimientos(_c);
                    entregasVarias.Enabled = false;

                 }
                 else
                 {
                     MessageBox.Show("Error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     
                 }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion de marcar como Entregado?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            var chequeService = new ChequeService(new ChequeRepository());
             
            if (dg == DialogResult.OK)
            {//marcar como anulado

                if (chequeService.MarcarComo(_c, EstadoCheque.EntregadoSinOpago, fechaOut.Value, txtObsOut.Text))
                {
                    MessageBox.Show("Operacion realizada satifactoriamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargoMovimientos(_c);
                    entregasVarias.Enabled = false;

                }
                else
                {
                    MessageBox.Show("Error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}
