using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.ChequeRepository;
using Repository.ClienteRepository;
using Repository.Repositories.CuentaRepository;
using Repository.Repositories.ReciboRepository;
using Services;
using Services.ChequeService;
using Services.ClienteService;
using Services.CuentaService;
using Services.ReciboService;

namespace Central
{
    public partial class NuevoRecibo : Form
    {
        public NuevoRecibo()
        {
            InitializeComponent();
            reciboService = new ReciboService(new ReciboRepository(), new ReciboDetalleRepository());
        }

        private ReciboService reciboService;
        public void refresh2()
        {

            cargarChques();
        }

        private void NuevoRecibo_Load(object sender, EventArgs e)
        {
            reciboService = new ReciboService(new ReciboRepository(), new ReciboDetalleRepository());
            CargarNumero();
            CargarClienes();
            cargarChques();
            cargarCajas();
        }
        private void cargarCajas()
        {
            CuentaService cuentaService = new CuentaService(new CuentaRepository());
            List<CuentaData> cajas = cuentaService.GetCuentasByTipo(TipoCuenta.Otra, true);
            CuentaData aux = new CuentaData();
            aux.Description = "Seleccione una caja";

            cajas.Insert(0, aux);
            cmbCajas.DataSource = cajas;
            cmbCajas.DisplayMember = "Show";
        }

        private void cargarChques()
        {
            ChequeService chequeService = new ChequeService(new ChequeRepository());
            List<EstadoCheque> estados = new List<EstadoCheque>();
            estados.Add(EstadoCheque.Creado);
            List<ChequeData> cs = chequeService.GetChequesTercero(true, estados,true);
            ChequeData aux = new ChequeData();
            aux.BancoEmisor.Description = "Seleccione un cheque";
            aux.Monto = 0;
            cs.Insert(0, aux);
            cmbCheques.DataSource = cs;
            cmbCheques.DisplayMember = "Show";
        }

        private void CargarClienes()
        {
            ClienteService ClienteService = new ClienteService(new ClienteRepository());
            cmbClientes.DataSource = ClienteService.GetAll(true);
            cmbClientes.DisplayMember = "razonSocial";

        }

        private void CargarNumero()
        {
            lblNro.Text = reciboService.GetNextNumberAvailable(HelperService.IDLocal, HelperService.Prefix,true);
        }

        private void radioEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioEfectivo.Checked)
            {
                txtImporte.Text = "";
                txtImporte.Enabled = true;
                cmbCheques.Enabled = false;
            }
        }

        private void radioCheque_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCheque.Checked)
            {
                txtImporte.Text = "";
                txtImporte.Enabled = false;
                cmbCheques.Enabled = true;
            }
        }

        private void cmbCheques_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCheques.SelectedIndex>-1)
            {
                txtImporte.Text = ((ChequeData)cmbCheques.SelectedItem).Monto.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            padre.AbrirForm(new Tesoreria.nuevoCheque(),  this.MdiParent);
        }
        double _total;
        private void button3_Click(object sender, EventArgs e)
        {
            if (pagoValido())
            {

                tablapagos.Rows.Add();
                int fila;
                fila = tablapagos.RowCount - 1;

                tablapagos[0, fila].Value = radioCheque.Checked?((ChequeData)cmbCheques.SelectedItem).ID:Guid.Empty;
                tablapagos[1, fila].Value = radioCheque.Checked ? "Cheque - " + ((ChequeData)cmbCheques.SelectedItem).Show : "Efectivo";
                tablapagos[2, fila].Value = HelperService.ConvertToDecimalSeguro(txtImporte.Text);
                calcularTotal();
                if (radioCheque.Checked)
                {//tenemos q sacar el cheque para que no lo ponga 2 veces!
                    List<ChequeData> cheques = (List<ChequeData>)cmbCheques.DataSource;
                    cheques.Remove(((ChequeData)cmbCheques.SelectedItem));
                    cmbCheques.DataSource = null;
                    cmbCheques.DataSource = cheques;
                    cmbCheques.DisplayMember = "Show";
                }
            }
        }

        private bool pagoValido()
        {
            if (radioCheque.Checked && cmbCheques.SelectedIndex==0)
            {
                MessageBox.Show("Seleccione un Cheque", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtImporte.Text == "")
            {
                MessageBox.Show("Ingrese un importe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (validoTodo())
            {
                ReciboData r = cargoRecibo();

                bool task = reciboService.Insert(r);
                if (task)
                {
                    MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpioControles();
                    CargarNumero();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void limpioControles()
        {
            tablapagos.Rows.Clear();
            cmbCheques.SelectedIndex = 0;
            txtImporte.Text = "";
            
            radioEfectivo.Checked=true;
            cmbClientes.SelectedIndex = 0;
            
        }

        private ReciboData cargoRecibo()
        {
            ReciboData r = new ReciboData();
            r.Enable =true;
            r.ID = Guid.NewGuid();
            r.tercero.ID = ((ClienteData)cmbClientes.SelectedItem).ID;
            r.Children = cargoDetalles(r.ID);
            r.Date = fecha.Value;
            r.Prefix = HelperService.Prefix;
            r.Numero = Convert.ToInt32(lblNro.Text.Split('-')[1]);
            r.Monto = HelperService.ConvertToDecimalSeguro(lblTotal.Text);
            r.Local.ID = HelperService.IDLocal;
            return r;
        }

        private List<ReciboOrdenPagoDetalleData> cargoDetalles(Guid id)
        {
            List<ReciboOrdenPagoDetalleData> det = new List<ReciboOrdenPagoDetalleData>();
            ReciboOrdenPagoDetalleData d;
           
            foreach (DataGridViewRow f in tablapagos.Rows)
            {
                d = new ReciboOrdenPagoDetalleData();
                d.FatherID = id;
                d.Cheque.ID = new Guid(tablapagos[0, f.Index].Value.ToString());
                d.Monto = HelperService.ConvertToDecimalSeguro(tablapagos[2, f.Index].Value.ToString());
                if (d.Cheque.ID!=Guid.Empty)
                {
                    d.Cuenta.ID = ((CuentaData)cmbCajas.SelectedItem).ID;
                }
                det.Add(d);
            }

            return det;
        }

     

        private bool validoTodo()
        {
            if (tablapagos.Rows.Count==0)
            {
                MessageBox.Show("Ingrese un Pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            bool hayEfectivo = false;
            foreach (DataGridViewRow item in tablapagos.Rows)
            {
              
                if (new Guid(item.Cells[0].Value.ToString())==Guid.Empty)
                {//es efectivo
                    hayEfectivo = true;
                }
            }

            if (hayEfectivo && cmbCajas.SelectedIndex == 0)
            {
                MessageBox.Show("Ingrese una cuenta donde acreditar el efectivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void tablapagos_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            calcularTotal();
        }

        private void calcularTotal()
        {
            decimal tot = 0;

            foreach (DataGridViewRow item in tablapagos.Rows)
            {
                tot += HelperService.ConvertToDecimalSeguro(item.Cells[2].Value);
            }
            lblTotal.Text = tot.ToString();
        }
    }
}
