using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.ChequeraRepository;
using Repository.Repositories.CuentaRepository;
using Services.ChequeraService;
using Services.CuentaService;

namespace Central.Tesoreria
{
    public partial class chequera : Form
    {
        public chequera()
        {
            InitializeComponent();
        }

        private void chequera_Load(object sender, EventArgs e)
        {
            cargoCuentasCorrientes();
            CargoChequeras();
            cargoCodigoInterno();
        }
        List<ChequeraData> chequeras = null;

        private void CargoChequeras()
        {
            tabla.Rows.Clear();
            var chequeraService = new ChequeraService(new ChequeraRepository());
            chequeras = chequeraService.GetAll(false);
            
            foreach (ChequeraData c in chequeras)
            {

             
                    tabla.Rows.Add();
                    int fila;
                    fila = tabla.RowCount - 1;
                    //id nombre Codigo
                    tabla[0, fila].Value = c.ID;
                    tabla[1, fila].Value = c.Show;
                    tabla[2, fila].Value = c.CodigoInterno.ToString("0000");
                    tabla[3, fila].Value = c.Primero;
                    tabla[4, fila].Value = c.Ultimo;
                    tabla[5, fila].Value = c.Siguiente;
                tabla[6, fila].Value = c.Enable?"No anulada":"Anulada";
                if (!c.Enable)
                    tabla.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;
            }
        }

        private void cargoCodigoInterno()
        {
            var chequeraService = new ChequeraService(new ChequeraRepository());

            txtInterno.Text = chequeraService.GetNextNumberAvailable();

        }

        private void cargoCuentasCorrientes()
        {
            var cuentaService = new CuentaService(new CuentaRepository());
            List<CuentaData> cuentas = cuentaService.GetAll( true);

            cuentas = cuentas.FindAll(delegate(CuentaData c) { return c.TipoCuenta == TipoCuenta.Banco && c.esCuentaCorriente; });

            CuentaData aux = new CuentaData();
            aux.Description = "Seleccione una cuenta habilitada";
            cuentas.Insert(0, aux);
            cmbCuentas.DataSource = cuentas;
            cmbCuentas.DisplayMember = "Show";
        }

        private void button1_Click(object sender, EventArgs e)
        {
             DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
             var chequeraService = new ChequeraService(new ChequeraRepository());

             if (dg == DialogResult.OK)
             {
                 if (valido())
                 {
                     ChequeraData c = CargoChequera();
                     bool taskDone = chequeraService.Insert(c);
                     if (taskDone)
                     {

                         MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         limpioControles();
                         CargoChequeras();
                         cargoCodigoInterno();

                     }
                     else
                     {
                         MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                 }
             }
        }

        private ChequeraData CargoChequera()
        {
            ChequeraData c = new ChequeraData();
            c.ID = Guid.NewGuid();
            c.CodigoInterno = Convert.ToInt32(txtInterno.Text);
            c.Cuenta = (CuentaData)cmbCuentas.SelectedItem;
            c.Description = txtDescripcion.Text;
            c.Enable = true;
            c.Primero = txtPrimero.Text;
            c.Ultimo = txtUltimo.Text;
            c.Siguiente = txtSiguiente.Text;
            return c;
        }

        private void limpioControles()
        {
            cmbCuentas.SelectedIndex = 0;
            txtDescripcion.Text = "";
            txtInterno.Text = "";
            txtPrimero.Text = "";
            txtUltimo.Text = "";
            txtSiguiente.Text = "";
        }

        private bool valido()
        {
            if (cmbCuentas.SelectedIndex==0)
            {
                MessageBox.Show("Seleccione una cuenta valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtDescripcion.Text=="")
            {
                MessageBox.Show("Ingrese una descripcion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtPrimero.Text == "")
            {
                MessageBox.Show("Ingrese el primer cheque a utilizar de su chequera", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtUltimo.Text == "")
            {
                MessageBox.Show("Ingrese el ultimo cheque a utilizar de su chequera", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } if (txtSiguiente.Text == "")
            {
                MessageBox.Show("Ingrese el siguiente cheque a utilizar de su chequera", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Convert.ToInt32(txtUltimo.Text)<Convert.ToInt32(txtPrimero.Text))
            {
                MessageBox.Show("Error al ingresar numerador de cheques", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Convert.ToInt32(txtSiguiente.Text) < Convert.ToInt32(txtPrimero.Text))
            {
                MessageBox.Show("Error al ingresar primer y ultimo Numero  de cheques", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Convert.ToInt32(txtSiguiente.Text) > Convert.ToInt32(txtUltimo.Text))
            {
                MessageBox.Show("Error al ingresar siguiente Numero de cheques", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
             DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
             var chequeraService = new ChequeraService(new ChequeraRepository());
             if (dg == DialogResult.OK)
             {

                 if (tabla.SelectedCells.Count > 0)
                 {
                     bool task = chequeraService.Disable(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                     if (task)
                     {
                         MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         CargoChequeras();
                     }
                     else
                     {
                         MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }

                 }
             }
        }
    }
}
