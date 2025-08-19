using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.RemitoRepository;
using Repository.Repositories.StockRepository;
using Repository.Repositories.UsuarioRepository;
using Services;
using Services.LocalService;
using Services.RemitoService;
using Services.StockService;
using Services.UsuarioService;

namespace SharedForms.Stock
{
    public partial class AltaStock : Form


    {

        RemitoData _remitoActual = new RemitoData();
        public AltaStock()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiarTabla();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiarTabla();
            cargarAltas();

        }

        private void limpiarTabla()
        {
            tabla.Rows.Clear();
            txtDestino.Text = "";
            txtNro.Text = "";
            txtOrigen.Text = "";
            txtObs.Text = "";
            txtPares.Text = "0";
            txtParesV.Text = "0";
        }

        private void cargarAltas()
        {
            var localService = new LocalService(new LocalRepository());

            if (cmbremitos.SelectedIndex>-1)
            {
                _remitoActual= (RemitoData)cmbremitos.SelectedItem;

                _remitoActual.LocalDestino = localService.GetByID(_remitoActual.LocalDestino.ID);
                _remitoActual.Local = localService.GetByID(_remitoActual.Local.ID);

                txtOrigen.Text = _remitoActual.Local.Codigo;
                txtDestino.Text = _remitoActual.LocalDestino.Codigo;
                txtNro.Text = _remitoActual.Show;
                txtObs.Text = _remitoActual.Description;
               


                decimal count = 0;
                int fila=0;
                txtObs.Text = _remitoActual.Description;
                foreach (remitoDetalleData rd in _remitoActual.Children)
                {


                    for (int i = 1; i <= rd.Cantidad; i++)
                    {

                        tabla.Rows.Add();

                        fila = tabla.RowCount - 1;
                        //Codigo nombre  color talle verificado
                        tabla[0, fila].Value = rd.Codigo;

                        StockData s = stockService.obtenerProducto(rd.Codigo);
                        tabla[1, fila].Value = s.Producto.Proveedor.RazonSocial;
                        tabla[2, fila].Value = s.Producto.Show;
                        tabla[3, fila].Value = s.Color.Description;
                        tabla[4, fila].Value = s.Talle;
                        tabla[5, fila].Value = 1;
                        //if (HelperService.haymts)
                        //{
                        //    tabla[6, fila].Value = rd.Cantidad * s.Talle;
                        //    count += (rd.Cantidad * s.Talle);
                        //}
                        //else
                        //{
                        //    count+=rd.Cantidad;
                        //}


                    }
                    
                }
                if (HelperService.haymts)
                {
                    txtPares.Text = count.ToString();    
                }
                else
                {
                    txtPares.Text = _remitoActual.CantidadTotal.ToString();
                }
                
            }
            else
            {
                MessageBox.Show("Debe de seleccionar un remito", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private RemitoService remitoService = null;
        private StockService stockService = null;
        private void AltaStock_Load(object sender, EventArgs e)
        {
            stockService = new StockService(new StockRepository());
            remitoService = new RemitoService(new RemitoRepository(), new RemitoDetalleRepository());
            obtenerAltas();
            if (!HelperService.haymts)
                tabla.Columns[6].Visible = false;
        }

        private void obtenerAltas()
        {

            List<RemitoData> remitos = remitoService.getByLocalSinRecibir(HelperService.IDLocal,false);
             
             cmbremitos.DataSource = remitos;
             cmbremitos.DisplayMember = "showConFecha";

        }

    

        private void button4_Click(object sender, EventArgs e)
        {

            if (HelperService.validarCodigo(txtcodigo.Text))
            {
                if (!verificarProducto(txtcodigo.Text))
                {
                    MessageBox.Show("Este articulo no se encuentra en la lista", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    txtcodigo.Text = "";
                    txtcodigo.Focus();
                }
                calcularVerificados();

            }
            else
            {
                MessageBox.Show("Codigo invalido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void calcularVerificados()
        {
            int count = 0;
            foreach (DataGridViewRow row in tabla.Rows)
            {
                if ((row.Cells[7].Style.BackColor == Color.Green))
                {
                    count += 1;
                    
                }
            }
            txtParesV.Text = count.ToString();
        }

        private bool verificarProducto(string searchValue)
        {

            foreach (DataGridViewRow row in tabla.Rows)
            {
                if ((row.Cells[0].Value.ToString().Equals(searchValue)) && (row.Cells[7].Style.BackColor != Color.Green))
                {
                    row.Cells[7].Style.BackColor = Color.Green;
                    return true;
                }
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Esta por confirmar un alta, seguro que desea continuar?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            {
                bool ok = confirmarAlta();
                if (ok){
                    limpiarTabla();
                    limparPares();
                    MessageBox.Show("Alta realizada Satifactoriamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    obtenerAltas();
                }
                
            }
        }

        private void limparPares()
        {
            txtPares.Text = "0";
            txtParesV.Text = "0";
            txtObs.Text = "";
        }

        private bool confirmarAlta()
        {
            bool todoactivo = true;

            foreach (DataGridViewRow row in tabla.Rows)
            {
                if (row.Cells[7].Style.BackColor!=Color.Green)
                { todoactivo = false;
                break;
                }
            }

            if (!todoactivo)
            {
                MessageBox.Show("Debe de verificar todos los articulos para confirmar el alta!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            

            else
            {

                    bool needPass = true;
                    string resultado = "";
                    
                    helperForms.InputBox("Alerta", "Ingrese la contrasena para confirmar la operacion", ref resultado);

                    var usuarioService = new UsuarioService(new UsuarioRepository());
            
                    if (needPass)
                    {
                        if (!usuarioService.VerificarPermiso(resultado))
                        {
                            MessageBox.Show("La contrasena es erronea, intente nuevamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        else
                        {

                         
                           return remitoService.confirmarRecibo(_remitoActual.ID,fechaRecibo.Value);
                        }
                    }
                
            }

            return false;
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            if (DialogResult.OK==MessageBox.Show("Esta seguro que desea marcar todos los articulos como vistos?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            {
            foreach (DataGridViewRow row in tabla.Rows)
            { row.Cells[7].Style.BackColor = Color.Green; }    
            }
            calcularVerificados();

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}