using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Repository.Repositories.ListaPrecioRepository;
using Services.ListaPrecioService;

namespace SharedForms.Stock.Administrar
{
    public partial class listaPrecio : Form
    {
        public listaPrecio()
        {
            InitializeComponent();
        }

        private void Colores_Load(object sender, EventArgs e)
        {
            cargarListas();
            
        }


        private void cargarListas()
        {
            cargarListas(null);
        }
        private void cargarListas(string search)
        {

            tabla.Rows.Clear();
            var listaPrecioService = new ListaPrecioService(new ListaPrecioRepository());

            List<listaPrecioData> cs = listaPrecioService.GetAll();

            cs.Sort(delegate(listaPrecioData x, listaPrecioData y)
            {
                return x.Description.CompareTo(y.Description);
            });


            if (search != null && search.Length > 0)
            {
                cs = cs.FindAll(delegate(listaPrecioData x)
                {
                    return x.Description.ToLower().StartsWith(search.ToLower());
                });
            }

            foreach (listaPrecioData c in cs)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                //id nombre codigo
                tabla[0, fila].Value = c.ID;
                tabla[1, fila].Value = c.Description;
                tabla[2, fila].Value = c.Enable?"Habilitada":"DesHabilitada";
               
                tabla.ClearSelection();
            }

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            cargarListas(txtNombre.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            var listaPrecioService = new ListaPrecioService(new ListaPrecioRepository());
            bool task = false;
            if (dg == DialogResult.OK)
            {

                listaPrecioData ncolor = new listaPrecioData();

                ncolor.Description = txtNombre.Text;
                ncolor.Enable = true;
                if (valido())
                {
                    task = listaPrecioService.Insert(ncolor);
                    if (task)
                    {
                        MessageBox.Show("Lista generada exitosamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiar();
                        cargarListas();    
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Ingrese un nombre", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void limpiar() {

            
            txtNombre.Text = "";

            tabla.DataSource = null;
            tabla.ClearSelection();
            tabla.Rows.Clear();

           
        }

        private bool valido()
        {
            return (txtNombre.Text != "");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var listaPrecioService = new ListaPrecioService(new ListaPrecioRepository());
            
            if (tabla.SelectedCells.Count != 1)
            {
                MessageBox.Show("Debe seleccionar una sola casilla", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            { 
                DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dg == DialogResult.OK)
                {

                    listaPrecioService.Disable(new listaPrecioData(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString())));
                    
                    MessageBox.Show("Lista anulada de forma correcta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                    cargarListas();
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}