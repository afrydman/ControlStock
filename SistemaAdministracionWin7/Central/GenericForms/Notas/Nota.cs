using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Central.GenericForms.Notas;
using DTO;
using DTO.BusinessEntities;
using Repository.Repositories.NotasRepository;
using Repository.Repositories.PersonalRepository;
using Services;
using Services.AbstractService;
using Services.PersonalService;
using Services.TributoService;
using SharedForms;
using SharedForms.Ventas;

namespace Central.GenericForms
{
    public partial class Nota2<T> : ventaBase where T : PersonaData
    {

        private IGetNextNumberAvailable<NotaData> MyService;
        private IGenericService<T> ServiceTercero;
        private string _name;
        private  T defaultPersona;
        public Nota2(IGetNextNumberAvailable<NotaData> myService, IGenericService<T> _serviceTercero, string name)
        {
            InitializeComponent();
            MyService = myService;
            ServiceTercero = _serviceTercero;
            _name = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //var notaCreditoClienteService = new NotaService(new NotaCreditoClienteRepository(), new NotaCreditoClienteDetalleRepository());

            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dg == DialogResult.OK)
            {
                if (validoTodo(tabla))
                {
                    NotaData nota = cargoNota(cmbclase.Text, fecha.Value, txtTotal.Text, txtobs.Text, txtIva.Text, ((PersonalData)cmbVendedor.SelectedItem).ID,
                        ((PersonaData)cmbTercero.SelectedItem).ID, tabla, tipoNota.CreditoCliente, 
                        Convert.ToInt32(MyService.GetNextNumberAvailable(HelperService.IDLocal, HelperService.Prefix, false)));

                    bool task = MyService.Insert(nota);
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
        private void cargarVendedores()
        {
            var personalService = new PersonalService(new PersonalRepository());

            List<PersonalData> vendedores = personalService.GetAll();


            cmbVendedor.DataSource = vendedores;
            cmbVendedor.DisplayMember = "nombrecontacto";
        }
        private void cargarTerceros()
        {
            var list = ServiceTercero.GetAll(true);
            //PersonaData p = new PersonaData();
          
            //list.Add(p);
            cmbTercero.DataSource = list;

            cmbTercero.DisplayMember = "razonSocial";
        }

        private void cargarClase()
        {
            cmbclase.DataSource = Enum.GetValues(typeof(ClaseDocumento));
        }


        public void AgregarItem(string description, decimal precio, decimal cantidad)
        {
            AgregarArticulo(description, tabla, precio.ToString(), "0", cantidad, 0, 0);
            
            
            CalcularTotales();
        }

        private void Nota_Load(object sender, EventArgs e)
        {



            SetControls(txtNeto, txtSubtotal, txtUnidadTotal, txtIva, txtTotal, txtDescuento, txtTributos, txtobs,
                txtTributoAlicuota, txtTributoImporte, txtTributoBase, radioAlicuota, cmbTributo,

                 tabla, tablaTributos, new DataGridView(), 
                 cmbclase, cmbTercero, cmbVendedor, new ComboBox(), new ComboBox(),
                 fecha, new DateTimePicker());

            SetUI();
            
            cargarClase();
            cargarNumero();
            cargarTerceros();
            cargarVendedores();

            CargarTributos();
        }

        private void CargarTributos()
        {
            var tributoService = new TributoService();

            cmbTributo.DataSource = tributoService.GetAll();
            cmbTributo.DisplayMember = "Description";
        }

        private void SetUI()
        {


            this.Text = _name;

            Type typeTercero = typeof(T);

            if (typeTercero == typeof(ClienteData))
            {
                lblTercero.Text = "Clientes";
            }
            else { lblTercero.Text = "Proveedores"; }



        }

        private void limpioControles()
        {

            tabla.Rows.Clear();
            txtIva.Text = "";
            txtobs.Text = "";
            txtNeto.Text = "";
            txtTotal.Text = "";
            tablaTributos.Rows.Clear();
            txtTributos.Text = "";
            txtTributoImporte.Text = "";
            txtTributoAlicuota.Text = "";
            txtTributoBase.Text = "";

        }

        private void cargarNumero()
        {

            lblNro.Text = MyService.GetNextNumberAvailable(HelperService.IDLocal, HelperService.Prefix, true);
        }

        private void cmbclase_SelectedIndexChanged(object sender, EventArgs e)
        {



            SeteoAlicuotas(tabla, cmbclase.Text.ToLower());
            foreach (DataGridViewRow row in tabla.Rows)
            {
                calcularCantidad(row.Index, tabla);
            }
            CalcularTotales();
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
                txtNeto.Text = subt;
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
            if (tabla.Rows.Count <= 0)
            {
                MessageBox.Show("Ingrese un detalle", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            foreach (DataGridViewRow r in tablaTributos.Rows)
            {

                var baseTributo = HelperService.ConvertToDecimalSeguro(r.Cells[2].Value);
                if (baseTributo > 0 && baseTributo != HelperService.ConvertToDecimalSeguro(txtNeto.Text))
                {
                    MessageBox.Show(string.Format("El tributo {0} tiene un neto diferente al de la venta.Debe de borrarlo y volverlo a ingresar.", r.Cells[1].Value), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            return true;

        }


        public virtual NotaData cargoNota(string cmbclase, DateTime fecha, string txtTotal, string txtobs, string txtIva, Guid idvendedor, Guid idtercero, DataGridView tabla, tipoNota tipo, int numero)
        {
            NotaData n = new NotaData();

            n.Enable = true;

            switch (cmbclase.ToLower())
            {
                case "a":
                    n.ClaseDocumento = ClaseDocumento.A;
                    break;

                case "b":
                    n.ClaseDocumento = ClaseDocumento.B;
                    break;

                case "c":
                    n.ClaseDocumento = ClaseDocumento.C;
                    break;
            }

            n.Date = fecha;
            n.Prefix = HelperService.Prefix;
            n.ID = Guid.NewGuid();
            n.Local.ID = HelperService.IDLocal;
            n.Monto = HelperService.ConvertToDecimalSeguro(txtTotal);
            n.Numero = numero;
            n.Description = txtobs;
            n.IVA = HelperService.ConvertToDecimalSeguro(txtIva);
            n.tipo = tipo;
            n.Vendedor.ID = idvendedor;//((personalData)cmbVendedor.SelectedItem).ID;
            n.tercero.ID = idtercero;//((proveedorData)cmbClientes.SelectedItem).ID;
            n.Children = cargarDetalles(n.ID, tabla);


           
            n.Tributos = cargoTributos();
            return n;

        }

   
        public virtual List<NotaDetalleData> cargarDetalles(Guid id, DataGridView tabla)
        {
            List<NotaDetalleData> ds = new List<NotaDetalleData>();
            NotaDetalleData d;
            foreach (DataGridViewRow item in tabla.Rows)
            {

                if (item.Cells[1].Value != null)
                {
                    d = new NotaDetalleData();
                    d.FatherID = id;
                    d.Cantidad = HelperService.ConvertToDecimalSeguro(item.Cells[7].Value.ToString());
                    d.Description= item.Cells[1].Value.ToString();
                    d.PrecioUnidad = HelperService.ConvertToDecimalSeguro(item.Cells[4].Value.ToString());
                    d.Alicuota = HelperService.ConvertToDecimalSeguro(item.Cells[10].Value.ToString());
                    d.Bonificacion = HelperService.ConvertToDecimalSeguro(item.Cells[6].Value.ToString());
                    ds.Add(d);

                }
            }
            return ds;
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

      

        private void tabla_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if (!IsDigitsOnly(tabla[7, e.RowIndex].Value.ToString()))
                {
                    tabla[7, e.RowIndex].Value = "1";
                }
                if (!IsDigitsOnly(tabla[6, e.RowIndex].Value.ToString()))
                {
                    tabla[6, e.RowIndex].Value = "0";
                }
                

                ActualizarRow(e.RowIndex, e.ColumnIndex);
            }
        }
        private void ActualizarRow(int RowIndex, int ColumnIndex)
        {
            calcularCantidad(RowIndex, tabla);
            CalcularTotales();
        }

        private void tabla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tabla_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalcularTotales();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            padreBase.AbrirForm(new AgregarItemNota<T>(), this.MdiParent, false, FormStartPosition.CenterScreen);
        }

        private void tablaTributos_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            //txtRecargos.Text = calcularTributo(tablaTributos).ToString(); // todo! ver esto
            CalcularTotales();
        }

        private void txtTributoAlicuota_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtTributoAlicuota, e);
        }

        private void txtTributoImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtTributoImporte, e);
        }

        private void txtTributoAlicuota_TextChanged(object sender, EventArgs e)
        {
            CalcularTributoImporte();
        }

        private void radioAlicuota_CheckedChanged(object sender, EventArgs e)
        {
            TributoRadioButton();
        }

        private void radioImporte_CheckedChanged(object sender, EventArgs e)
        {
            TributoRadioButton();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (ValidoTributo(txtTributoImporte.Text))
            {
                AgregoTributo();
                txtTributoImporte.Text = "";


                CalcularTotales();
            }
        }

        private void tablaTributos_RowsRemoved_1(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            //txtRecargos.Text = calcularTributo(tablaTributos).ToString(); // todo! ver esto
            CalcularTotales();
        }

    }
}
