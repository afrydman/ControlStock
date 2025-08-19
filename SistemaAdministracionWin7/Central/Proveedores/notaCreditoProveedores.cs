using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using DTO;
using DTO.BusinessEntities;
using Repository.Repositories.NotasRepository;
using Repository.Repositories.PersonalRepository;
using Repository.Repositories.ProveedorRepository;
using Services;
using Services.NotaService;
using Services.NotaService.Cliente.Credito;
using Services.PersonalService;
using Services.ProveedorService;

namespace Central.Proveedores
{
    public partial class notaCreditoProveedores : notaBase
    {
        public notaCreditoProveedores()
        {
            InitializeComponent();
        }

        private void notaDebitoCliente_Load(object sender, EventArgs e)
        {
            cargarClase();
            cargarNumero();
            cargarClientes();
            cargarVendedores();
        }

        private void cargarClase()
        {
            List<string> clases = new List<string>();
            clases.Add("A");
            clases.Add("B");
            clases.Add("C");

            cmbclase.DataSource = clases;


        }

        private void cargarNumero()
        {
            var notaCreditoProveedoresService = new NotaService(new NotaCreditoProveedoresRepository(), new NotaCreditoProveedoresDetalleRepository());
            lblNro.Text = notaCreditoProveedoresService.GetNextNumberAvailable(HelperService.IDLocal, HelperService.Prefix, true);
        }

        private void cargarVendedores()
        {


            var personalService = new PersonalService(new PersonalRepository());

            List<PersonalData> vendedores = personalService.GetAll();

            cmbVendedor.DataSource = vendedores;
            cmbVendedor.DisplayMember = "nombrecontacto";
        }

        private void cargarClientes()
        {
            cmbClientes.DataSource = new ProveedorService(new ProveedorRepository()).GetAll(true); 
            cmbClientes.DisplayMember = "razonSocial";
        }



        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            var notaCreditoProveedoresService = new NotaService(new NotaCreditoProveedoresRepository(), new NotaCreditoProveedoresDetalleRepository());
            if (dg == DialogResult.OK)
            {
                if (validoTodo(tabla))
                {
                    NotaData mov = cargoNota(cmbclase.Text, fecha.Value, txtTotal.Text, txtobs.Text, txtIva.Text, ((PersonalData)cmbVendedor.SelectedItem).ID, ((ClienteData)cmbClientes.SelectedItem).ID, tabla, tipoNota.CreditoProveedor, Convert.ToInt32(notaCreditoProveedoresService.GetNextNumberAvailable(HelperService.IDLocal, HelperService.Prefix, false)));
                    bool task = notaCreditoProveedoresService.Insert(mov);
                    if (task)
                    {
                        MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpioControles();
                        cargarNumero();

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
            
            tabla.Rows.Clear();
            txtIva.Text = "";
            txtobs.Text = "";
            txtsubt.Text = "";
            txtTotal.Text = "";

        }

        
   

        
         

        private void tabla_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void tabla_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            
        }

         

        private void tabla_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            
        }

         

        private void tabla_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            string subt = "";
            string iva = "";
            string Total = "";
          

            bool ok = validoElDetalle(e, tabla);
            if (ok)
            {
                calcularSubTotalRow(e, tabla, out subt, cmbclase.Text, out iva, out Total);
                txtsubt.Text = subt;
                txtIva.Text = iva;
                txtTotal.Text = Total;
            }
            else
            {
                e.Cancel = true;


            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cmbclase_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbclase.Text.ToLower() != "a")
            {
                seteoAlicuotasnula(tabla);
            }
            else
            {
                tabla.Columns[3].ReadOnly = false;
            }
            foreach (DataGridViewRow row in tabla.Rows)
            {
                calcularSubTotal(row.Index, tabla);
            }
          


            string subt = "";
            string iva = "";
            string Total = "";
            calcularTotales(tabla, out subt, cmbclase.Text, out iva, out Total);
            txtsubt.Text = subt;
            txtIva.Text = iva;
            txtTotal.Text = Total;
        }

        private void tabla_RowValidating_1(object sender, DataGridViewCellCancelEventArgs e)
        {
            string subt = "";
            string iva = "";
            string Total = "";



            bool ok = validoElDetalle(e, tabla);
            if (ok)
            {
                calcularSubTotalRow(e, tabla, out subt, cmbclase.Text, out iva, out Total);
                txtsubt.Text = subt;
                txtIva.Text = iva;
                txtTotal.Text = Total;
            }
            else
            {
                if (!(tabla[0, e.RowIndex].Value == null && tabla[1, e.RowIndex].Value == null &&
                    tabla[2, e.RowIndex].Value == null))
                {

                    e.Cancel = true;
                }


            }
        }
    }
}
