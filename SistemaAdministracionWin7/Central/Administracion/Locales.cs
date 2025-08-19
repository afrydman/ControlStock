using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.LocalRepository;
using Services;
using Services.LocalService;

namespace Central
{
    public partial class Locales : Form
    {
        public Locales()
        {
            InitializeComponent();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            txtCodigo.Text = txtNombre.Text.Replace(" ","");
            cargarLocal(txtNombre.Text);
        }

        private void cargarLocal(string search)
        {
            var localService = new LocalService(new LocalRepository());
            List<LocalData> ps = localService.GetAll();

            if (search != null && search.Length > 0)
            {
                tabla.Rows.Clear();
                ps = ps.FindAll(delegate(LocalData x)
                {
                    return x.Nombre.ToLower().StartsWith(search.ToLower());
                });
            }
            else
            {
                limpiar(true);
            }

            foreach (LocalData loc in ps)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;

                tabla[0, fila].Value = loc.ID;
                tabla[1, fila].Value = loc.Nombre;
                tabla[2, fila].Value = loc.Codigo;
                tabla[3, fila].Value = loc.Direccion;
                tabla[4, fila].Value = loc.Telefono;
                tabla[5, fila].Value = loc.Email;

                tabla.ClearSelection();
            }

        }

        private void Locales_Load(object sender, EventArgs e)
        {
            if (HelperService.esCliente(GrupoCliente.Slipak))
            {
                this.Text = "Centros";
            }
            
            cargarLocales();
        }

        private void cargarLocales()
        {
            limpiar(true);
            var localService = new LocalService(new LocalRepository());
            List<LocalData> locales = localService.GetAll();

            foreach (LocalData loc in locales)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;

                tabla[0, fila].Value = loc.ID;
                tabla[1, fila].Value = loc.Nombre;
                tabla[2, fila].Value = loc.Codigo;
                tabla[3, fila].Value = loc.Direccion;
                tabla[4, fila].Value = loc.Telefono;
                tabla[5, fila].Value = loc.Email;

                tabla.ClearSelection();
            }
        }

        private void limpiar(bool limparTabla)
        {

            if (limparTabla)
            {
                tabla.DataSource = null;
                tabla.ClearSelection();
                tabla.Rows.Clear();
            }

            cargarControles(new LocalData());
           
        }

        private void cargarControles(LocalData localData)
        {
            txtNombre.Text = localData.Nombre;
            txtCodigo.Text = localData.Codigo; ;
            txtDireccion.Text = localData.Direccion;
            txtObs.Text = localData.Description;
            txtEmail.Text = localData.Email;
            txtTelefono.Text = localData.Telefono;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            var localService = new LocalService(new LocalRepository());
            if (dg == DialogResult.OK)
            {
                if (valido())
                {
                    LocalData l = cargoObjeto();
                    bool task;
                    if (blneditar)
                    {
                        l.ID = idp;
                        task = localService.Update(l);
                        
                        if (task)
                        {
                            MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                 
                        setearAEditar(false);
                    }
                    else
                    {
                        task = localService.Insert(l);
                        if (task)
                        {
                            MessageBox.Show("Operacion completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    limpiar(true);
                    cargarLocales();
                    
                }

            }





        }

        private bool valido()
        {
            if (txtNombre.Text=="")
            {
                MessageBox.Show("Debe ingresar un nombre valido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private LocalData cargoObjeto()
        {
            LocalData l = new LocalData();
            l.Codigo = txtCodigo.Text;
            l.Description = txtObs.Text;
            l.Direccion = txtDireccion.Text;
            l.Email = txtEmail.Text;
            l.Nombre = txtNombre.Text;
            l.Enable = true;
            l.Telefono = txtTelefono.Text;
            return l;
        }

        Guid idp;
        bool blneditar = false;

        private void setearAEditar(bool editar)
        {
            if (editar)
            {
                button1.Text = "Editar";
                blneditar = true;
            }
            else
            {
                button1.Text = "Agregar";
                blneditar = false;
                idp = new Guid();
            }
        }


        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tabla.SelectedCells.Count > 0)
            {
                var localService = new LocalService(new LocalRepository());
                LocalData p = new LocalData();
                idp = new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                p = localService.GetByID(idp);
                cargarControles(p);
                setearAEditar(true);
            }
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            limpiar(false);
            idp = new Guid();
            setearAEditar(false);

        }
    }
}