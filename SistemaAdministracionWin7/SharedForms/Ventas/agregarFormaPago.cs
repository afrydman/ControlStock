using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.FormaPagoRepository;
using Services;
using Services.FormaPagoService;

namespace SharedForms.Ventas
{
    public partial class agregarFormaPago : Form
    {
        public agregarFormaPago()
        {
            InitializeComponent();
        }

        private string t = "";
        public agregarFormaPago(string total)
        {
            InitializeComponent();
            t = total;
            
        }
        private void calcularImporteRecargo(string importe, string recargo)
        {

            decimal aumento = HelperService.ConvertToDecimalSeguro(recargo);
            if (importe != "")
            {
                decimal subt = HelperService.ConvertToDecimalSeguro(importe);
                txtImporteRecargo.Text = HelperService.ConvertToDecimalSeguro((subt + ((subt * aumento) / 100)),2).ToString();
            }
            else
            {
                txtImporteRecargo.Text = "0";
            }

        }
        private void agregarFormaPago_Load(object sender, EventArgs e)
        {
            cargarFormasPago(cmbFormaPago);

            if (HelperService.esCliente(GrupoCliente.Slipak))
            {
                cmbFormaPago.SelectedItem =
                    ((List<FormaPagoData>) cmbFormaPago.DataSource).Find(c => c.Description.ToLower().StartsWith("c"));
            }
            txtImporte.Text = HelperService.ConvertToDecimalSeguro(t).ToString();
        }

        public  void cargarFormasPago(ComboBox cmbFormaPago)
        {
            var formaPagoService = new FormaPagoService(new FormaPagoRepository(), new FormaPagoCuotasRepository());
            cmbFormaPago.DataSource = formaPagoService.GetAll(false);
            cmbFormaPago.DisplayMember = "Description";
            cmbFormaPago.ValueMember = "Id";
            cmbFormaPago.SelectedValue = HelperService.idEfectivo;

        }

        private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCuotas.SelectedText = "-";
            txtlote.Text = "";
            txtcupon.Text = "";

            bool sostarjeta = false;
            sostarjeta = ((FormaPagoData)cmbFormaPago.SelectedItem).Credito;
            cmbCuotas.Enabled = sostarjeta;
            txtlote.Enabled = sostarjeta;
            txtcupon.Enabled = sostarjeta;
            cmbCuotas.SelectedIndex = sostarjeta?1:0;


            if (((FormaPagoData) cmbFormaPago.SelectedItem).Credito)
            {
                if (cmbCuotas.Text != "-")//es decir que se elijio algo que es tarjeta.
                {

                    var forma = (FormaPagoData)cmbFormaPago.SelectedItem;

                    ;
                    txtRecargo.Text =
                        HelperService.ConvertToDecimalSeguro(
                            forma.Cuotas[Convert.ToInt32(cmbCuotas.Text) - 1].Aumento
                            ).ToString();

                }
                else
                {
                    txtRecargo.Text = "0";
                }
            }
                

           
        }

        private void cmbCuotas_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbCuotas.Text!="-")//es decir que se elijio algo que es tarjeta.
            {

                var forma = (FormaPagoData) cmbFormaPago.SelectedItem;
                
                ;
                txtRecargo.Text =
                    HelperService.ConvertToDecimalSeguro(
                        forma.Cuotas[Convert.ToInt32(cmbCuotas.Text)-1].Aumento
                        ).ToString();

            }
            else
            {
                txtRecargo.Text = "0";
            }
            calcularImporteRecargo(txtImporte.Text, txtRecargo.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ValidoyAgregoPago();
        }

        private void ValidoyAgregoPago()
        {
            if (validarPago(((FormaPagoData) cmbFormaPago.SelectedItem), cmbCuotas.SelectedIndex - 1, txtlote.Text,
                txtcupon.Text, txtRecargo.Text, txtImporte.Text, txtImporteRecargo.Text))
            {
                DialogResult lotecuponvalidator = validarLoteyCupon(((FormaPagoData) cmbFormaPago.SelectedItem), txtlote.Text,
                    txtcupon.Text);

                if (lotecuponvalidator == DialogResult.OK)
                {
                    if (((FormaPagoData) cmbFormaPago.SelectedItem).Credito)
                    {
                        agregarPago(((FormaPagoData)cmbFormaPago.SelectedItem).ID, ((FormaPagoData)cmbFormaPago.SelectedItem).Description, cmbCuotas.SelectedIndex, txtlote.Text, txtcupon.Text, txtRecargo.Text, txtImporte.Text, txtImporteRecargo.Text);    
                        
                    }
                    else
                    {
                        agregarPago(((FormaPagoData)cmbFormaPago.SelectedItem).ID, ((FormaPagoData)cmbFormaPago.SelectedItem).Description,0, txtlote.Text, txtcupon.Text, txtRecargo.Text, txtImporte.Text, txtImporteRecargo.Text);    
                    }
                    

                }
            }
        }

        private void  agregarPago(Guid idpago, string descripcion, int cuotas, string lote, string cupon, string recargo, string importe, string importeRecargo)
        {
            foreach (Form hijo in this.MdiParent.MdiChildren)
            {
                if (hijo.GetType() != typeof(agregarFormaPago) && hijo.GetType().BaseType == typeof(ventaBase))
                {

                    ((ventaBase)hijo).agregarPago(idpago, descripcion, cuotas, lote, cupon, recargo, importe, importeRecargo);
                }

            }

            this.Close();
        }

        private DialogResult validarLoteyCupon(FormaPagoData formaPagoData, string lote, string cupon)
        {
            
            DialogResult dg = new DialogResult();
            if (formaPagoData.Credito)
            {
                if (lote != "" && cupon != "")
                {
                    dg = DialogResult.OK;
                }
                else
                {
                    if (lote == "" && cupon == "")
                    {
                        dg =
                            MessageBox.Show(
                                "No ingreso un lote ni cupon,\nEsta seguro que desea confirmar la operacion?", "Alerta",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    }
                    else
                    {
                        if (cupon == "")
                        {
                            dg = MessageBox.Show("No ingreso un cupon,\nEsta seguro que desea confirmar la operacion?",
                                "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        }
                        if (lote == "")
                        {
                            dg = MessageBox.Show("No ingreso un lote,\nEsta seguro que desea confirmar la operacion?",
                                "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        }
                    }
                }


            }
            else
            {
                dg = DialogResult.OK;
            }
            return dg;
        }

        private void agregarPago()
        {
            
        }

        private bool validarPago(FormaPagoData formaPago, int cuota, string lote, string cupon, string recargo, string Importe, string ImporteRecargo)
        {

            if (formaPago.ID==new Guid())
            {
                MessageBox.Show("Seleccione una forma de pago", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (Importe=="")
            {
                MessageBox.Show("Ingrese un importe valido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            try
            {
                HelperService.ConvertToDecimalSeguro(txtImporte.Text);
                HelperService.ConvertToDecimalSeguro(txtImporteRecargo.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ingrese un importe valido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;


        }

       

        private void txtImporte_TextChanged(object sender, EventArgs e)
        {
            
            calcularImporteRecargo(txtImporte.Text, txtRecargo.Text);
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtImporte, e);
            
            
        }

        private void agregarFormaPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F3") //f12
            {
                ValidoyAgregoPago();
            }
        }
    }
}
