using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Services;
using SharedForms.Ventas;

namespace Central.GenericForms.Notas
{
    public partial class AgregarItemNota<T> : ventaBase where T : PersonaData
    {
        public AgregarItemNota()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (valido(txtDescripcion.Text,txtCantidad.Text,txtUnitario.Text))
                AgregoItem();
        }

        private void AgregoItem()
        {

            foreach (Form hijo in this.MdiParent.MdiChildren)
            {
                if (hijo.GetType() == typeof(Nota2<T>))
                {

                    ((Nota2<T>)hijo).AgregarItem(txtDescripcion.Text,HelperService.ConvertToDecimalSeguro(txtUnitario.Text),HelperService.ConvertToDecimalSeguro(txtCantidad.Text));
                }

            }

            this.Close();

        }

        private bool valido(string description, string cantidad, string unitario)
        {

            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Debe de ingresar una descripcion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            if (unitario == "" || unitario == "." || unitario == ",")
            {
                MessageBox.Show("Debe de ingresar un precio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cantidad == "" || cantidad == "." || cantidad == ",")
            {
                MessageBox.Show("Debe de ingresar una cantidad", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            return true;
        }

        private void txtUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {

            HelperService.VerificoTextBoxNumerico(txtUnitario, e);
            
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtCantidad, e);
        }
    }
}
