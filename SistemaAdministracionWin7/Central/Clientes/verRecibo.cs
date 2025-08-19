using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.ChequeRepository;
using Repository.ClienteRepository;
using Repository.Repositories.ReciboRepository;
using Services;
using Services.ChequeService;
using Services.ClienteService;
using Services.ReciboService;

namespace Central
{
    public partial class verRecibo : Form
    {
        public verRecibo()
        {
            InitializeComponent();
            reciboSerice = new ReciboService(new ReciboRepository(), new ReciboDetalleRepository());
        }
        ReciboData _recibo = null;
        private ReciboService reciboSerice = null;
        public verRecibo(Guid recibo)
        {
            
            InitializeComponent();
            reciboSerice = new ReciboService(new ReciboRepository(), new ReciboDetalleRepository());
            _recibo = reciboSerice.GetByID(recibo);
        }

        public void refresh2()
        {

            cargarChques();
        }

        private void NuevoRecibo_Load(object sender, EventArgs e)
        {
            CargarClienes();
            if (_recibo!=null)
            {
                cargoControles(_recibo);
            }
        }

        private void cargoControles(ReciboData _recibo)
        {
            fecha.Value = _recibo.Date;
            cmbClientes.Text = _recibo.tercero.RazonSocial;
            lblNro.Text = _recibo.NumeroCompleto;


            foreach (ReciboOrdenPagoDetalleData r in _recibo.Children)
            {

                tablapagos.Rows.Add();
                int fila;
                fila = tablapagos.RowCount - 1;

                if (r.Cheque!=null && r.Cheque.ID == Guid.Empty)
                {
                    tablapagos[1, fila].Value = "Efectivo";

                }
                else
                {
                    tablapagos[1, fila].Value = "Cheque - " + r.Cheque.Show;
                }
                tablapagos[2, fila].Value = r.Monto.ToString();

                lblTotal.Text = _recibo.Monto.ToString();
            }
        }

        private void cargarChques()
        {
            var chequeService = new ChequeService(new ChequeRepository());

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
            var ClienteService = new ClienteService(new ClienteRepository());
            cmbClientes.DataSource = ClienteService.GetAll(true);
            cmbClientes.DisplayMember = "razonSocial";

        }

        private void CargarNumero()
        {
            lblNro.Text = reciboSerice.GetNextNumberAvailable(HelperService.IDLocal, HelperService.Prefix,true);
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
                tablapagos[1, fila].Value= radioCheque.Checked?"Cheque - "+cmbCheques.SelectedText:"Efectivo";
                tablapagos[2, fila].Value  = txtImporte.Text;
                calcularTotal();
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

            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dg == DialogResult.OK)
            {
                bool task = reciboSerice.Disable(_recibo);
                if (task)
                {
                    MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button2.Enabled = false;
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
            r.Enable = true;
            r.ID = Guid.NewGuid();
            r.tercero.ID = ((ClienteData)cmbClientes.SelectedItem).ID;
            r.Children = cargoDetalles(r.ID);
            r.Date = fecha.Value;
            r.Prefix = HelperService.Prefix;
            r.Numero = Convert.ToInt32(lblNro.Text.Split('-')[2]);
            r.Monto = HelperService.ConvertToDecimalSeguro(lblTotal.Text);

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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
