using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO.BusinessEntities;

using Repository.Repositories.AdministracionRepository;
using Services.AdministracionService;

namespace Central.Administracion
{
    public partial class abmTiposIngreso : Form
    {
        public abmTiposIngreso()
        {
            InitializeComponent();
        }

        private void abmTiposIngreso_Load(object sender, EventArgs e)
        {
            cargarIngresos("");
        }

        private void cargarIngresos(string search)
        {
            var tipoIngresoService = new TipoIngresoService(new TipoIngresoRepository());
            List<tipoIngresoData> tipos = tipoIngresoService.GetAll(false, false);
            tipos.Sort(delegate(tipoIngresoData x, tipoIngresoData y)
            {
                return x.Description.CompareTo(y.Description);
            });


            if (search != null && search.Length > 0)
            {
                tipos = tipos.FindAll(delegate(tipoIngresoData x)
                {
                    return x.Description.ToLower().StartsWith(search.ToLower());
                });
            }

            foreach (tipoIngresoData t in tipos)
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

            if (dg == DialogResult.OK)
            {
                var tipoIngresoService = new TipoIngresoService(new TipoIngresoRepository());
                tipoIngresoData nuevoIngreso = new tipoIngresoData();
                nuevoIngreso.Description = txtNombre.Text;
                nuevoIngreso.ID = Guid.NewGuid();
                nuevoIngreso.Enable = true;

                if (valido(nuevoIngreso))
                {
                    tipoIngresoService.Insert(nuevoIngreso);
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

        private bool valido(tipoIngresoData nuevoIngreso)
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
