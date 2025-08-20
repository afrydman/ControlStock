using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO.BusinessEntities;
using Repository.ClienteRepository;
using Services.ClienteService;

namespace Central
{
    public partial class Clientes: Form
    {
        public Clientes()
        {
            InitializeComponent();
            
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
            
            cargarProveedores();
           
        }

       
        private void limpiar(bool limparTabla)
        {

            if (limparTabla)
            {
                tabla.DataSource = null;
                tabla.ClearSelection();
                tabla.Rows.Clear();    
            }
            setearAEditar(false);
            cargarControles(new clienteData());
           
        }

        private string ESTADO_ELIMINADO = "Eliminado";
        private string ESTADO_NO_ELIMINADO = "No Eliminado";


        private void cargarProveedores()
        {

            var ClienteService = new ClienteService(new ClienteRepository());

            List<clienteData> ps = ClienteService.GetAll(false);



            limpiar(true);

            foreach (clienteData p in ps) 
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                
                tabla[0, fila].Value = p.id;
                tabla[1, fila].Value = p.razonSocial;
                tabla[2, fila].Value = p.email;
                tabla[3, fila].Value = p.nombrecontacto;
                tabla[4, fila].Value = p.tel;
                tabla[5, fila].Value = p.cuil;
                tabla[6, fila].Value = p.enable ? ESTADO_NO_ELIMINADO : ESTADO_ELIMINADO;
                
                tabla.ClearSelection();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dg == DialogResult.OK)
                {
                    clienteData p = new clienteData();

                    p = cargarCliente();
                    if (blneditar)
                    {
                        new cliente().Update(p);
                        MessageBox.Show("Cliente editado exitosamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        setearAEditar(false);
                    }
                    else
                    {
                        new cliente().Insert(p);
                        MessageBox.Show("Cliente agregado exitosamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                    limpiar(true);
                    
                    cargarProveedores();
                }
        }

        private clienteData cargarCliente()
        {
            clienteData p = new clienteData();

            p.cuil = txtCuil.Text;
            
            p.dir = txtDireccion.Text;
            p.email = txtEmail.Text;
            p.facebook = txtFacebook.Text;
            
            p.nombrecontacto = txtNombre.Text;
            p.observaciones = txtObs.Text;
            p.razonSocial = txtRazon.Text;
            p.tel = txtTelefono.Text;
            p.enable = true;
            if (blneditar)
            {
                p.id = idp;
            }
            






            return p;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tabla.SelectedCells.Count != 1)
            {
                MessageBox.Show("Debe seleccionar una sola casilla", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dg == DialogResult.OK)
                {

                    bool task = new cliente().Delete(((new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()))));
                    if (task)
                    {
                        MessageBox.Show("Cliente anulado de forma correcta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);       
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un Error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                    limpiar(true);
                    cargarProveedores();
                }
            }
        }
        Guid idp = new Guid();
        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tabla.SelectedCells.Count>0)
            {
                personaData p = new personaData();
                idp = new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                p =  new cliente().GetByID(idp);
                cargarControles(p);
                setearAEditar(true);
            }
            
        }
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

        private void cargarControles(personaData p)
        {
            txtCuil.Text = p.cuil ;
            
            txtDireccion.Text = p.dir;
            txtEmail.Text = p.email;
            txtFacebook.Text = p.facebook;
            
            txtNombre.Text = p.nombrecontacto;
            txtObs.Text = p.observaciones;
            txtRazon.Text = p.razonSocial;
            txtTelefono.Text = p.tel;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            limpiar(false);
            idp = new Guid();
            setearAEditar(false);

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtRazon_TextChanged(object sender, EventArgs e)
        {
            cargarCliente(txtRazon.Text);
        }

        private void cargarCliente(string search)
        {
            List<personaData> ps = new cliente().GetAll();

            if (search != null && search.Length > 0)
            {
                tabla.Rows.Clear();
                ps = ps.FindAll(delegate(personaData x)
                {
                    return x.razonSocial.ToLower().StartsWith(search.ToLower());
                });
            }
            else
            {
                limpiar(true);
            }

            foreach (clienteData p in ps)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;

                tabla[0, fila].Value = p.id;
                tabla[1, fila].Value = p.razonSocial;
                tabla[2, fila].Value = p.codigo;
                tabla[3, fila].Value = p.nombrecontacto;
                tabla[4, fila].Value = p.tel;
                tabla[5, fila].Value = p.cuil;

                tabla.ClearSelection();
            }

        }
    }
}