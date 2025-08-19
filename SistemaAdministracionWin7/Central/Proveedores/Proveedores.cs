using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BusinessComponents;
using DTO.BusinessEntities;

namespace Central.Proveedores
{
    public partial class Proveedores : Form
    {
        public Proveedores()
        {
            InitializeComponent();
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
            
            cargarProveedores();
            cargarCodigo();
        }

        private void cargarCodigo()
        {


            List<personaData> ps = new proveedor().GetAll(false);

            ps.Sort(delegate(personaData x, personaData y)
            {
                return Convert.ToInt32(x.codigo).CompareTo(Convert.ToInt32(y.codigo));
            });

            int maxcodigo=0000;
            
            if (ps.Count>0)
            {
                maxcodigo = Convert.ToInt32(ps[ps.Count - 1].codigo) + 1;    
            }
            

            txtCodigo.Text = maxcodigo.ToString("0000");
        }
        private void limpiar(bool limparTabla)
        {

            if (limparTabla)
            {
                tabla.DataSource = null;
                tabla.ClearSelection();
                tabla.Rows.Clear();    
            }
            
            cargarControles(new proveedorData());
            cargarCodigo();
        }
        private void cargarProveedores()
        {
            cargarProveedores(null);
        }
        
        private void cargarProveedores(string search)
        {

            List<personaData> ps = new proveedor().GetAll();

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

            foreach (proveedorData p in ps) 
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

        private void button1_Click(object sender, EventArgs e)
        {
            
                DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            bool task = false;
                if (dg == DialogResult.OK)
                {
                    proveedorData p = new proveedorData();

                    p = cargarProveedor();
                    if (blneditar)
                    {
                        task = new proveedor().Update(p);

                        if (task)
                        {
                            MessageBox.Show("Proveedor editado exitosamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            setearAEditar(false);
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        task = new proveedor().Insert(p);
                        
                        if (task)
                        {
                            MessageBox.Show("Proveedor agregado exitosamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    
                    limpiar(true);
                    
                    cargarProveedores();
                }
        }

        private proveedorData cargarProveedor()
        {
            proveedorData p = new proveedorData();

            p.cuil = txtCuil.Text;
            p.descuento = txtDescuento.Text;
            p.dir = txtDireccion.Text;
            p.email = txtEmail.Text;
            p.facebook = txtFacebook.Text;
            p.ingresoB = txtIngresoBruto.Text;
            p.nombrecontacto = txtNombre.Text;
            p.observaciones = txtObs.Text;
            p.razonSocial = txtRazon.Text;
            p.tel = txtTelefono.Text;
            p.enable = true;
            if (!blneditar)
            {
                cargarCodigo();
            }
            else
            {
                p.id = idp;
            }
            

            p.codigo = txtCodigo.Text;
            





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

                    new proveedor().Delete((new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString())));
                    MessageBox.Show("Proveedor anulado de forma correcta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                proveedorData p = new proveedorData();
                idp = new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                p = (proveedorData)new proveedor().GetByID(idp);
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

        private void cargarControles(proveedorData p)
        {
            txtCuil.Text = p.cuil ;
            txtDescuento.Text = p.descuento;
            txtDireccion.Text = p.dir;
            txtEmail.Text = p.email;
            txtFacebook.Text = p.facebook;
            txtIngresoBruto.Text = p.ingresoB;
            txtNombre.Text = p.nombrecontacto;
            txtObs.Text = p.observaciones;
            txtRazon.Text = p.razonSocial;
            txtTelefono.Text = p.tel;
            txtCodigo.Text = p.codigo;   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            limpiar(false);
            idp = new Guid();
            setearAEditar(false);

        }

        private void txtRazon_TextChanged(object sender, EventArgs e)
        {
            cargarProveedores(txtRazon.Text);
        }
    }
}