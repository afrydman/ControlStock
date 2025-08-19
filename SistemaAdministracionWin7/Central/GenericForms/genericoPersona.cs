using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository;
using Repository.ClienteRepository;
using Repository.Repositories.ProveedorRepository;
using Services;
using Services.AdministracionService;
using Services.BancoService;
using Services.ClienteService;
using Services.Interfaces;
using Services.ProveedorService;

namespace Central
{
    public partial class genericoPersona<T>: Form where T : PersonaData, new()
    {

        private IGenericService<T> iPersona;
        private string _myname;
        //private PersonaService<PersonaData, IGenericRepository<PersonaData>> iPersona;
        public genericoPersona(IGenericService<T> aService,string myName)
        {
            InitializeComponent();
            iPersona = aService;
            _myname = myName;

        }

        private string tipo;
        private void Proveedores_Load(object sender, EventArgs e)
        {
            tipo = iPersona.GetType().Name.ToString();//todos se llaman xxxService
            tipo = tipo.Substring(0, tipo.Length - 7);
            tipo = tipo.First().ToString().ToUpper() + tipo.Substring(1);
            
            this.Text = _myname;
                

            cargarTabla();

            if (iPersona.GetType()==new ClienteService().GetType())
            {
                txtCodigo.Visible = false;
                lblcodigo.Visible = false;
                tabla.Columns[2].Visible = false;
            }

            cargarCondicionIVA();

        }

        private void cargarCondicionIVA()
        {
            cmbIVA.DataSource = new CondicionIVAService().GetAll(false);
            cmbIVA.DisplayMember = "Description";


        }
       
        private void limpiar(bool limparTabla,bool limpiarCampos)
        {
            if (limparTabla)
            {
                tabla.DataSource = null;
                tabla.ClearSelection();
                tabla.Rows.Clear();
               
            }
            if (limpiarCampos)
            {
                setearAEditar(false);
                setearAEliminar(true);
                cargarControles(new PersonaData());
            }
            cargarCodigo();

            
           
        }

        private string ESTADO_ELIMINADO = "Eliminado";
        private string ESTADO_NO_ELIMINADO = "No Eliminado";


        private void cargarTabla()
        {
            bool verEliminados = chckvereliminado.Checked;//
            string buscar = txtRazon.Text;

            List<T> ps = iPersona.GetAll(!verEliminados);
            if (ps != null)
            {
                if (!string.IsNullOrEmpty(buscar))
                ps = ps.FindAll(x => x.RazonSocial.ToLower().StartsWith(buscar.ToLower()));


            limpiar(true, false);
            
            foreach (PersonaData p in ps) 
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                
                tabla[0, fila].Value = p.ID;
                tabla[1, fila].Value = p.RazonSocial;
                tabla[2, fila].Value = p.Codigo;
                tabla[3, fila].Value = p.Email;
                tabla[4, fila].Value = p.NombreContacto;
                tabla[5, fila].Value = p.Telefono;
                tabla[6, fila].Value = p.cuil;
                    tabla[7, fila].Value = p.CondicionIVaDescripcion;
                tabla[8, fila].Value = p.Enable ? ESTADO_NO_ELIMINADO : ESTADO_ELIMINADO;
                
                    if (!p.Enable)
                    tabla.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;
                tabla.ClearSelection();
            }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool task = false;
                DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dg == DialogResult.OK)
                {
                    T p = new T();

                    p = cargarPersona();
                    if (blneditar)
                    {
                        task=iPersona.Update(p);

                        if (task)
                        {
                            MessageBox.Show(tipo + " editado exitosamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            setearAEditar(false);    
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un Error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        
                    }
                    else
                    {
                        task = iPersona.Insert(p);
                        if (task)
                        {
                            MessageBox.Show(tipo + " agregado exitosamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);    
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un Error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        
                    }
                    
                    limpiar(true,true);
                    
                    cargarTabla();
                }
        }

        private T cargarPersona()
        {
            T p = new T();

            
            p.cuil = txtCuil.Text;
            p.Codigo = txtCodigo.Text;
            p.Direccion = txtDireccion.Text;
            p.Email = txtEmail.Text;
            p.Facebook = txtFacebook.Text;
            
            p.NombreContacto = txtNombre.Text;
            p.Description = txtObs.Text;
            p.RazonSocial = txtRazon.Text;
            p.Telefono = txtTelefono.Text;
            p.Enable = true;
            if (blneditar)
            {
                p.ID = idp;
            }
            p.CondicionIva = (CondicionIvaData) cmbIVA.SelectedItem;
            return p;
        }
        private void cargarCodigo()
        {
            if (iPersona.GetType() == new ProveedorService().GetType())
            {
                var maxcodigo = new ProveedorService().NextAvailableCode();
                txtCodigo.Text = maxcodigo.ToString("0000");
            }
            else
            {
                txtCodigo.Text = 0.ToString("0000");
            }
            
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
                    T aux = new T();
                    aux.ID = new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                    bool task;
                    if (blnEliminar)
                    {

                        task = iPersona.Disable(aux);    
                    }
                    else
                    {
                        task = iPersona.Enable(aux);
                    }
                    
                    if (task)
                    {
                        MessageBox.Show(tipo + (blnEliminar ? " anulado" : " retornado") + "  de forma correcta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);       
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un Error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                    limpiar(true,true);
                    cargarTabla();
                }
            }
        }
        Guid idp = new Guid();
        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tabla.SelectedCells.Count>0)
            {
                PersonaData p = new PersonaData();
                idp = new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                p =  iPersona.GetByID(idp);
                
                cargarControles(p);
                setearAEditar(true);
                setearAEliminar(p.Enable);
            }
            
        }
        bool blneditar = false;


        private const string BOTON_EDITAR = "Editar";
        private const string BOTON_AGREGAR = "Agregar";

        private const string BOTON_ELIMINAR = "Eliminar";
        private const string BOTON_DESELIMINAR = "Retornar";

        private bool blnEliminar=true;

        private void setearAEliminar(bool eliminar)
        {
            if (eliminar)
            {
                btnEliminar.Text = BOTON_ELIMINAR;
                blnEliminar = true;
            }
            else
            {
                btnEliminar.Text = BOTON_DESELIMINAR;
                blnEliminar = false;
            }

        }
        
        private void setearAEditar(bool editar)
        {
            if (editar)
            {
                btnAgregar.Text = BOTON_EDITAR;
                blneditar = true; 
            }
            else
            {
                btnAgregar.Text = BOTON_AGREGAR;
                blneditar = false;
                idp = new Guid();
            }
        }

        private void cargarControles(PersonaData p)
        {
           
            
            txtDireccion.Text = p.Direccion;
            txtEmail.Text = p.Email;
            txtFacebook.Text = p.Facebook;
            
            
            
            txtRazon.Text = p.RazonSocial;
            txtTelefono.Text = p.Telefono;

            txtNombre.Text = p.NombreContacto;
            txtCuil.Text = p.cuil;
            txtCodigo.Text = p.Codigo;

            txtObs.Text = p.Description;
            txtIIBB.Text = p.IngresosBrutos;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            limpiar(false,true);
            idp = new Guid();
            setearAEditar(false);

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtRazon_TextChanged(object sender, EventArgs e)
        {
            cargarTabla();
        }

     
        private void chckvereliminado_CheckedChanged(object sender, EventArgs e)
        {
            cargarTabla();
        }
    }
}