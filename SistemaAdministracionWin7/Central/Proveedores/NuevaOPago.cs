using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using ObjectDumper;
using Repository.ChequeRepository;
using Repository.Repositories.CuentaRepository;
using Repository.Repositories.OrdenPagoRepository;
using Repository.Repositories.ProveedorRepository;
using Services;
using Services.ChequeService;
using Services.CuentaService;
using Services.OrdenPagoService;
using Services.ProveedorService;

namespace Central.Proveedores
{
    public partial class NuevaOPago : Form
    {
        public NuevaOPago()
        {
            InitializeComponent();
        }
        public void refresh2()
        {

            cargarChques();
        }

        private void NuevoRecibo_Load(object sender, EventArgs e)
        {
            CargarNumero();
            CargarClienes();
            cargarChques();
            cargarCajas();
        }

        private void cargarCajas()
        {
            var cuentaService = new CuentaService(new CuentaRepository());
            List<CuentaData> cajas = cuentaService.GetCuentasByTipo(TipoCuenta.Otra, true);
            
            CuentaData aux = new CuentaData();
            aux.Description = "Seleccione una caja";

            cajas.Insert(0, aux);
            cmbCajas.DataSource = cajas;
            cmbCajas.DisplayMember = "Show";
        }

        private void cargarChques()
        {
            var chequeService = new ChequeService(new ChequeRepository());

            List<ChequeData> cs = chequeService.GetChequesUtilizables(true);
            ChequeData aux = new ChequeData();
            aux.BancoEmisor.Description = "Seleccione un cheque";
            aux.Monto = 0;
            cs.Insert(0, aux);
            cmbCheques.DataSource = cs;
            cmbCheques.DisplayMember = "Show";
        }

        private void CargarClienes()
        {
            cmbClientes.DataSource = new ProveedorService(new ProveedorRepository()).GetAll(true);
            cmbClientes.DisplayMember = "razonSocial";

        }

        private void CargarNumero()
        {
            var OrdenPagoService = new OrdenPagoService(new OrdenPagoRepository(), new OrdenPagoDetalleRepository());

            lblNro.Text = OrdenPagoService.GetNextNumberAvailable(HelperService.IDLocal, HelperService.Prefix,true);
        }

        private void radioEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioEfectivo.Checked)
            {
                txtImporte.Text = "";
                txtImporte.Enabled = true;
                cmbCheques.Enabled = false;
                cmbCajas.Enabled = true;
            }
        }

        private void radioCheque_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCheque.Checked)
            {
                txtImporte.Text = "";
                txtImporte.Enabled = false;
                cmbCheques.Enabled = true;
                cmbCajas.Enabled = false;
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
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (pagoValido())
            {

                tablapagos.Rows.Add();
                int fila;
                fila = tablapagos.RowCount - 1;

                tablapagos[0, fila].Value = radioCheque.Checked?((ChequeData)cmbCheques.SelectedItem).ID:Guid.Empty;
                tablapagos[1, fila].Value= radioCheque.Checked?"Cheque - "+((ChequeData)cmbCheques.SelectedItem).Show:"Efectivo";
                tablapagos[2, fila].Value = HelperService.ConvertToDecimalSeguro(txtImporte.Text);
                if (radioCheque.Checked)
                {//tenemos q sacar el cheque para que no lo ponga 2 veces!
                    List<ChequeData> cheques = (List<ChequeData>)cmbCheques.DataSource;
                    cheques.Remove(((ChequeData)cmbCheques.SelectedItem));
                    cmbCheques.DataSource = null;
                    cmbCheques.DataSource = cheques;
                    cmbCheques.DisplayMember = "Show";
                    cmbCheques.SelectedIndex = 0;
                }
                else
                {
                    //es efectiivo
                    tablapagos[3, fila].Value = ((CuentaData)cmbCajas.SelectedItem).ID;

                }
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
            if (radioEfectivo.Checked && cmbCajas.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione una Caja", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            var OrdenPagoService = new OrdenPagoService(new OrdenPagoRepository(), new OrdenPagoDetalleRepository());

            if (dg == DialogResult.OK)
            {
                if (validoTodo())
                {
                    OrdenPagoData r = cargoOrdenPago();

                    bool task = OrdenPagoService.Insert(r);
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
        }

        private void limpioControles()
        {
            tablapagos.Rows.Clear();
            cmbCheques.SelectedIndex = 0;
            txtImporte.Text = "";
            lblTotal.Text = "0";
            radioEfectivo.Checked=true;
            cmbClientes.SelectedIndex = 0;
            
        }

        private OrdenPagoData cargoOrdenPago()
        {
            OrdenPagoData r = new OrdenPagoData();
            r.Enable = true;
            r.ID = Guid.NewGuid();
            r.Tercero.ID = ((ProveedorData)cmbClientes.SelectedItem).ID;
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

                if (d.Cheque!=null && d.Cheque.ID != Guid.Empty)
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
            return true;
        }

        private void tablapagos_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            
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

        private void tablapagos_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            
            var chequeService = new ChequeService(new ChequeRepository());
            
            Guid posiblecheque = new Guid(e.Row.Cells[0].Value.ToString());
            if (posiblecheque != Guid.Empty)
            {//tenemos q volver a agregar el cheque
                ChequeData c = chequeService.GetByID(posiblecheque);
                List<ChequeData> cheques = (List<ChequeData>)cmbCheques.DataSource;
                cheques.Add(c);

                cheques.Sort(delegate(ChequeData x, ChequeData y)
                {
                    return x.Interno.CompareTo(y.Interno);

                });

                cmbCheques.DataSource = null;
                cmbCheques.DataSource = cheques;
                cmbCheques.DisplayMember = "Show";
            }
            
        }

        private void tablapagos_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            calcularTotal();
        }
    }
}
