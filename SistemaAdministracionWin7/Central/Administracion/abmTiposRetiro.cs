using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.AdministracionRepository;
using Services.AdministracionService;


namespace Central.Administracion
{
    public partial class abmTiposRetiro : Form
    {
        public abmTiposRetiro()
        {
            InitializeComponent();
        }

        private void abmTiposIngreso_Load(object sender, EventArgs e)
        {
            cargarIngresos("");
        }

        private void cargarIngresos(string search)
        {
            var tipoRetiroService = new TipoRetiroService(new TipoRetiroRepository());
            List<tipoRetiroData> tipos = tipoRetiroService.GetAll(false, false);
           


            if (search != null && search.Length > 0)
            {
                tipos = tipos.FindAll(delegate(tipoRetiroData x)
                {
                    return x.Description.ToLower().StartsWith(search.ToLower());
                });
            }

            foreach (tipoRetiroData t in tipos)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                //id nombre codigo
                tabla[0, fila].Value = t.ID;
                tabla[1, fila].Value = t.Description;
                   
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            cargarIngresos(txtNombre.Text);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            var tipoRetiroService = new TipoRetiroService(new TipoRetiroRepository());
            if (dg == DialogResult.OK)
            {

                tipoRetiroData nuevoIngreso = new tipoRetiroData();
                nuevoIngreso.Description = txtNombre.Text;
                nuevoIngreso.ID = Guid.NewGuid();

                if (valido(nuevoIngreso))
                {
                    tipoRetiroService.Insert(nuevoIngreso);
                    MessageBox.Show("Nuevo Tipo de Ingreso agregado exitosamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                    cargarIngresos("");
                }
                
            }
        }

        private void limpiar()
        {
            tabla.Rows.Clear();
            txtNombre.Text = "";
        }

        private bool valido(tipoRetiroData nuevoIngreso)
        {
            if (nuevoIngreso.Description=="")
            {
                
                    MessageBox.Show("Ingrese un nombre", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
            }
            return true;
        }
    }
}
