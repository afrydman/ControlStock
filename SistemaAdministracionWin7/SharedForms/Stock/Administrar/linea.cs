using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Repository.LineaRepository;
using Services.LineaService;

namespace SharedForms.Stock.Administrar
{
    public partial class linea : Form
    {
        public linea()
        {
            InitializeComponent();
        }

        private void linea_Load(object sender, EventArgs e)
        {
            cargarLineas();
        }

        private void cargarLineas()
        {
            var lineaService = new LineaService(new LineaRepository());
            List<LineaData> lista = lineaService.GetAll();


            lista.Sort(delegate(LineaData x, LineaData y)
            {
                return (x.Description).CompareTo(y.Description);
            });

            foreach (LineaData f in lista)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;

                tabla[0, fila].Value = f.ID;
                tabla[1, fila].Value = f.Description;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var lineaService = new LineaService(new LineaRepository());
            if (txtNombre.Text.Length>0)
            {
                
            
            if (idp==new Guid())//agregar
            {

                lineaService.Insert(new LineaData(idp, txtNombre.Text));
            }
            else
            {//modificar
                lineaService.Update(new LineaData(idp, txtNombre.Text));
            }
            txtNombre.Text = "";
            limpiarTabla();
            cargarLineas();
            setearAEditar(false);
            }
            else
            {
                MessageBox.Show("Debe de ingresar un nombre", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void limpiarTabla()
        {
            tabla.DataSource = null;
            tabla.ClearSelection();
            tabla.Rows.Clear();   
        }


        Guid idp = new Guid();
       

        private void setearAEditar(bool p)
        {
            if (p)
            {
                button1.Text = "Editar";
            }
            else
            {
                button1.Text = "Agregar";
                idp = new Guid();
                tabla.ClearSelection();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            setearAEditar(false);
        }

        private void tabla_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (tabla.SelectedCells.Count > 0)
            {

                idp = new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                txtNombre.Text = tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[1].Value.ToString();

                setearAEditar(true);
            }
        }
    }
}
