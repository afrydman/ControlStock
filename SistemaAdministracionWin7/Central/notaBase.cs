using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Services;

namespace Central
{
    public partial class notaBase : Form
    {
        public notaBase()
        {
            InitializeComponent();
        }

        private void notaBase_Load(object sender, EventArgs e)
        {

        }

        public void calcularSubTotal(int Index, DataGridView tabla)
        {

            if (tabla[1, Index] != null && tabla[1, Index].Value != null && tabla[3, Index] != null && tabla[3, Index].Value != null && tabla[2, Index] != null && tabla[2, Index].Value != null)
            {


                decimal aux = Convert.ToDecimal(tabla[1, Index].Value) *
                             Convert.ToDecimal(HelperService.ConvertToDecimalSeguro(tabla[2, Index].Value));


                tabla[4, Index].Value = decimal.Round(((aux * HelperService.ConvertToDecimalSeguro(tabla[3, Index].Value.ToString())) / 100), 2) + aux;
            }
        }

        public virtual void calcularSubTotalRow(DataGridViewCellCancelEventArgs e, DataGridView tabla, out string txtsubt, string cmbclase, out string txtIva, out string txtTotal)
        {

            decimal aux = Convert.ToDecimal(tabla[1, e.RowIndex].Value) *
                          Convert.ToDecimal(HelperService.ConvertToDecimalSeguro(tabla[2, e.RowIndex].Value));


            tabla[4, e.RowIndex].Value =
                decimal.Round(((aux * HelperService.ConvertToDecimalSeguro(tabla[3, e.RowIndex].Value.ToString())) / 100), 2) +
                aux;
            calcularTotales(tabla, out txtsubt, cmbclase, out txtIva, out txtTotal);

        }

        public virtual void calcularTotales(DataGridView tabla, out string txtsubt, string cmbclase, out string txtIva, out string txtTotal)
        {
            decimal subtot = 0;
            decimal iva = 0;
            decimal aux = 0;
            foreach (DataGridViewRow item in tabla.Rows)
            {
                aux = 0;
                if (item.Cells[0].Value != null)
                {
                    aux = decimal.Round(HelperService.ConvertToDecimalSeguro(item.Cells[1].Value.ToString()) * HelperService.ConvertToDecimalSeguro(item.Cells[2].Value.ToString()), 2);
                    subtot += aux;
                    iva += decimal.Round(Convert.ToDecimal((aux * HelperService.ConvertToDecimalSeguro(item.Cells[3].Value.ToString())) / 100), 2);
                }
            }

            txtsubt = subtot.ToString();
            txtIva = iva.ToString();
            txtTotal = (iva + subtot).ToString();

        }

        public virtual bool validoTodo(DataGridView tabla)
        {
            if (tabla.Rows.Count == 0)
            {
                MessageBox.Show("Ingrese un detalle", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;

        }


       


        

        public void seteoAlicuotasnula(DataGridView tabla)
        {
            foreach (DataGridViewRow row in tabla.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    row.Cells[3].Value = "0";

                }
            }
            tabla.Columns[3].ReadOnly = true;
        }

        public virtual bool validoElDetalle(DataGridViewCellCancelEventArgs e, DataGridView tabla)
        {


            if (tabla[0, e.RowIndex].Value == null && tabla[1, e.RowIndex].Value == null && tabla[2, e.RowIndex].Value == null)
            {
                return false;
                //tabla.Rows.RemoveAt(e.RowIndex);
            }

            if (tabla[3, e.RowIndex].Value == null)
            {
                tabla[3, e.RowIndex].Value = "0";
            }
            if (tabla[0, e.RowIndex].Value == null || tabla[1, e.RowIndex].Value == null || tabla[2, e.RowIndex].Value == null || tabla[3, e.RowIndex].Value == null)
            {
                MessageBox.Show("Complete todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (tabla[0, e.RowIndex].Value.ToString() == "")
            {
                MessageBox.Show("Ingrese un detalle", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (tabla[1, e.RowIndex].Value.ToString() == "")
            {
                MessageBox.Show("Ingrese una Cantidad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            double distance;
            if (!double.TryParse(tabla[1, e.RowIndex].Value.ToString(), out distance))
            {

                MessageBox.Show("Ingrese una Cantidad numerica", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (tabla[2, e.RowIndex].Value.ToString() == "")
            {
                MessageBox.Show("Ingrese un precio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            double auxpr;
            if (!double.TryParse(tabla[2, e.RowIndex].Value.ToString(), out auxpr))
            {

                MessageBox.Show("Ingrese un precio numerico valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

       

    }
}
