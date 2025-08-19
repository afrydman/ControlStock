using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Repository.Repositories.TeporadaRepository;
using Services.TemporadaService;

namespace SharedForms.Stock.Administrar
{
    public partial class temporada : Form
    {
        public temporada()
        {
            InitializeComponent();
        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tabla.SelectedCells.Count > 0)
            {

                idp = new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                txtNombre.Text = tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[1].Value.ToString();

                setearAEditar(true);
            }
        }


        private void temporada_Load(object sender, EventArgs e)
        {
            cargarTemporadas();
        }



        private void cargarTemporadas()
        {
            var temporadaService = new TemporadaService(new TemporadaRepository());

            List<TemporadaData> lista = temporadaService.GetAll();


            lista.Sort(delegate(TemporadaData x, TemporadaData y)
            {
                return (x.Description).CompareTo(y.Description);
            });

            foreach (TemporadaData f in lista)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;

                tabla[0, fila].Value = f.ID;
                tabla[1, fila].Value = f.Description;
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

        

        private void button2_Click_1(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            setearAEditar(false);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var temporadaService = new TemporadaService(new TemporadaRepository());
            if (txtNombre.Text.Length > 0)
            {
                if (idp == new Guid())//agregar
                {
                    temporadaService.Insert(new TemporadaData(Guid.NewGuid(), txtNombre.Text));
                }
                else
                {//modificar
                    temporadaService.Update(new TemporadaData(idp, txtNombre.Text));
                }
                txtNombre.Text = "";
                limpiarTabla();
                cargarTemporadas();
                setearAEditar(false);
            }
            else
            {
                MessageBox.Show("Debe de ingresar un nombre", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }



   
    }
}
