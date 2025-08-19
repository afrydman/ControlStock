using System;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.OrdenPagoRepository;
using Repository.Repositories.ProveedorRepository;
using Services.OrdenPagoService;
using Services.ProveedorService;

namespace Central.Proveedores
{
    public partial class verPago : Form
    {
        private OrdenPagoData _op= null;
        public verPago(Guid id)
        {
            var OrdenPagoService = new OrdenPagoService(new OrdenPagoRepository(), new OrdenPagoDetalleRepository());

            _op = OrdenPagoService.GetByID(id);
            InitializeComponent();
        }

        private void verPago_Load(object sender, EventArgs e)
        {
            if (_op == null || _op.ID == Guid.Empty)
            {
                Close();
            }
            CargarClienes();
            CargarOrdenPago(_op);
        }

        private void CargarOrdenPago(OrdenPagoData _opago)
        {

            fecha.Value = _opago.Date;
            cmbClientes.Text = _opago.Tercero.RazonSocial;
            lblNro.Text = _opago.NumeroCompleto;


            foreach (ReciboOrdenPagoDetalleData r in _opago.Children)
            {

                tablapagos.Rows.Add();
                int fila;
                fila = tablapagos.RowCount - 1;

                if (r.Cheque == null || r.Cheque.ID == Guid.Empty)
                {
                    tablapagos[1, fila].Value = "Efectivo";

                }
                else
                {
                    tablapagos[1, fila].Value = "Cheque - " + r.Cheque.Show;
                }
                tablapagos[2, fila].Value = r.Monto.ToString();

                lblTotal.Text = _opago.Monto.ToString();
            }

        }
        private void CargarClienes()
        {
            cmbClientes.DataSource = new ProveedorService(new ProveedorRepository()).GetAll(false);
            cmbClientes.DisplayMember = "razonSocial";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            var OrdenPagoService = new OrdenPagoService(new OrdenPagoRepository(), new OrdenPagoDetalleRepository());

            if (dg == DialogResult.OK)
            {
                bool task = OrdenPagoService.Disable(_op);
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
    }
}
