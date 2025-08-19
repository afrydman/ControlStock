using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Repository.ColoresRepository;
using Services.ColorService;

namespace SharedForms.Stock.Administrar
{
    public partial class Colores : Form
    {
        public Colores()
        {
            InitializeComponent();
        }

        private void Colores_Load(object sender, EventArgs e)
        {
            cargarColores();
            cargarUltimoCodigo();
        }

        private void cargarUltimoCodigo()
        {


            var colorService = new ColorService(new ColorRepository());

            List<ColorData> cs = colorService.GetAll(true);

            cs.Sort(delegate(ColorData x, ColorData y)
                 {
                     return x.Codigo.CompareTo(y.Codigo);
                 });

            int maxcodigo = 000;
            if (cs.Count>0)
            {
                maxcodigo = Convert.ToInt32(cs[cs.Count - 1].Codigo) + 1;    
            }
            

            txtCodigo.Text = maxcodigo.ToString("000");
          
        }
        private void cargarColores() {
            cargarColores(null);
        }
        private void cargarColores(string search)
        {

            tabla.Rows.Clear();

            var colorService = new ColorService(new ColorRepository());
            List<ColorData> cs = colorService.GetAll(true);

            cs.Sort(delegate(ColorData x, ColorData y){
                return Convert.ToInt32(x.Codigo).CompareTo(Convert.ToInt32(y.Codigo));
            });


            if (search != null && search.Length > 0)
            {
                cs = cs.FindAll(delegate(ColorData x)
                {
                    return x.Description.ToLower().Contains(search.ToLower());
                });
            }

            foreach (ColorData c in cs)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                //id nombre Codigo
                tabla[0, fila].Value = c.ID;
                tabla[1, fila].Value = c.Description;

                tabla[2, fila].Value = Convert.ToInt32(c.Codigo).ToString("000");
                tabla.ClearSelection();
            }

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            cargarColores(txtNombre.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            var colorService = new ColorService(new ColorRepository());
            if (dg == DialogResult.OK)
            {

                ColorData ncolor = new ColorData();
                ncolor.Codigo = txtCodigo.Text;
                ncolor.Description = txtNombre.Text;
                ncolor.Enable = true;
                if (valido(ncolor))
                {
                    colorService.Insert(ncolor);
                    MessageBox.Show("Color agregado exitosamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                    cargarColores();
                }
                else
                {
                    MessageBox.Show("Ingrese un nombre", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void limpiar() {

            txtCodigo.Text = "";
            txtNombre.Text = "";

            tabla.DataSource = null;
            tabla.ClearSelection();
            tabla.Rows.Clear();

            cargarUltimoCodigo();
        }

        private bool valido(ColorData codigo)
        {
            return (txtNombre.Text != "");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var colorService = new ColorService(new ColorRepository());
            if (tabla.SelectedCells.Count != 1)
            {
                MessageBox.Show("Debe seleccionar una sola casilla", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            { 
                DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dg == DialogResult.OK)
                {

                   colorService.Disable(new ColorData(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString())));
                    MessageBox.Show("Color anulado de forma correcta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                    cargarColores();
                }
            }
        }
    }
}