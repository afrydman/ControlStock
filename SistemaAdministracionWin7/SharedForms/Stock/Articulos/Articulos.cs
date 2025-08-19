using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.LineaRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.TeporadaRepository;
using Services;
using Services.LineaService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.TemporadaService;

namespace SharedForms.Stock.Articulos
{
    public partial class Articulos : Form
    {
        public Articulos()
        {
            InitializeComponent();
        }

        private void Articulos_Load(object sender, EventArgs e)
        {
            cargarArticulos();
            cargarProveedores();
            cargarCodigoInterno();
            cargarLineas();
            cargarTemporadas();
            cmblineas.SelectedIndex = -1;
            cmbTemporadas.SelectedIndex = -1;


            if (HelperService.esCliente(GrupoCliente.CalzadosMell))
            {
                txtCodigoInt.ReadOnly = false;
            }
        }

        private void cargarTemporadas()
        {
            var temporadaService = new TemporadaService(new TemporadaRepository());
            cmbTemporadas.DisplayMember = "Description";
            cmbTemporadas.DataSource = temporadaService.GetAll();
            
        }

        private void cargarLineas()
        {
            var lineaService = new LineaService(new LineaRepository());
            cmblineas.DisplayMember = "Description";
            cmblineas.DataSource = lineaService.GetAll();

        }

        private void cargarCodigoInterno()
        {
            txtCodigoInt.Text = generarCodigoInterno();
        }

        private void cargarProveedores()
        {
            cmbProveedores.DisplayMember = "RazonSocial";
            cmbProveedores.DataSource =new ProveedorService(new ProveedorRepository()).GetAll(true);
            
        }

        private void cargarArticulos() {

            cargarArticulos(new Guid(),null);
        }

        private void cargarArticulos(Guid idP,string search,bool verAnulados = true)
        {
            var productoService = new ProductoService(new ProductoRepository());
            List<ProductoData> ps;
            if (idP==new Guid())
            {
                return;
            }
            else
            {
                ps = productoService.GetbyProveedor(idP, false);
            }


            if (search!=null && search.Length>0)
            {
                ps = ps.FindAll(x => x.Description.ToLower().Contains(search.ToLower()) && (verAnulados || x.Enable));
            }
            else
            {
                ps = ps.FindAll(x => (verAnulados || x.Enable));
            }

            ps.Sort(delegate(ProductoData x, ProductoData y)
            {
                return x.CodigoProveedor.CompareTo(y.CodigoProveedor);
            });




            foreach (ProductoData p in ps)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;

                tabla[0, fila].Value = p.ID;
                tabla[1, fila].Value = p.Description;
                tabla[2, fila].Value = p.Proveedor.RazonSocial;
                tabla[3, fila].Value = p.CodigoProveedor;
                tabla[4, fila].Value = p.CodigoInterno;
                tabla[5, fila].Value = p.Enable ? "No Eliminado" : "Eliminado";
                if (!p.Enable)
                    tabla.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;

                tabla.ClearSelection();
            }



        }
        private void limpiar()
        {

            txtDescripcion.Text = "";
            txtCodigo.Text = "";
            
            tabla.DataSource = null;
            tabla.ClearSelection();
            tabla.Rows.Clear();
            cargarCodigoInterno();
            cmblineas.SelectedIndex = -1;
            cmbTemporadas.SelectedIndex = -1;
            button4.Text = EliminarText;
        }

        private void cmbProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedores.SelectedIndex>-1)
	{
		    limpiar();
            cargarArticulos(((ProveedorData)cmbProveedores.SelectedItem).ID,null);
            txtCodigoInt.Text =generarCodigoInterno();
	}
            

        }

        private string generarCodigoInterno()
        {

            ProveedorData p = ((ProveedorData)cmbProveedores.SelectedItem);
            //string lastcode = BusinessComponents.producto.getLastCode(p.Codigo).Substring(4);

            //int aux = 0;
            //if (lastcode!="")
            //{
            //    aux= Convert.ToInt32(lastcode);
            //}
            //aux++;

            

            //return p.Codigo + aux.ToString("000");
            var productoService = new ProductoService(new ProductoRepository());
            return productoService.GenerarCodigoInterno(p.Codigo);


        }
        string  EliminarText = "Eliminar";
        string RetornarText = "Retornar";

        private void button4_Click(object sender, EventArgs e)
        {
            var productoService = new ProductoService(new ProductoRepository());
            if (tabla.SelectedCells.Count != 1)
            {
                MessageBox.Show("Debe seleccionar una sola casilla", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dg == DialogResult.OK)
                {

                    if (button4.Text == EliminarText)
                    {
                        productoService.Disable(new ProductoData(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString())));
                        MessageBox.Show("Articulo anulado de forma correcta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  
                    }
                    else
                    {
                        productoService.Enable(new ProductoData(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString())));
                        MessageBox.Show("Articulo retornado de forma correcta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                    cargarArticulos();
                    botonlimpiar();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                var productoService = new ProductoService(new ProductoRepository());
                if (dg == DialogResult.OK)
                {
                    ProductoData p;

                    p = new ProductoData();
                    p.Enable = true;
                    p.CodigoInterno = txtCodigoInt.Text;
                    p.CodigoProveedor = txtCodigo.Text;
                    p.Proveedor = (ProveedorData)cmbProveedores.SelectedItem;
                    p.Description = txtDescripcion.Text;
                    if (cmblineas.SelectedIndex>-1)
	                {
                        p.Linea.ID = ((LineaData)cmblineas.SelectedItem).ID;
	                }
                    if (cmbTemporadas.SelectedIndex > -1)
                    {
                        p.Temporada.ID = ((TemporadaData)cmbTemporadas.SelectedItem).ID;
                    }


                    bool task = false;


                    if (idp == new Guid())
                    {
                        if (valido(p))
                        {
                            
                        
                        p.ID = Guid.NewGuid();
                        task = productoService.Insert(p);
                        if (task)
                        {
                            MessageBox.Show("Articulo insertado de forma correcta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            setearAEditar(false);
                            limpiar();

                            cargarArticulos(((ProveedorData)cmbProveedores.SelectedItem).ID, null);
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un Error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        }


                    }
                    else
                    {
                        p.ID = idp;
                        if (valido(p)){
                            task = productoService.Update(p);

                            if (task)
                            {
                                MessageBox.Show("Articulo actualizado de forma correcta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                setearAEditar(false);
                                limpiar();

                                cargarArticulos(((ProveedorData)cmbProveedores.SelectedItem).ID, null);
                            }
                            else
                            {
                                MessageBox.Show("Ocurrio un Error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                    
                }
        }

      

        private bool valido(ProductoData p)
        {

            if (p.Description=="")
            {
                MessageBox.Show("La descripcion no puede ser vacia", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            var productoService = new ProductoService(new ProductoRepository());

            List<ProductoData> productos = productoService.GetbyProveedor(((ProveedorData)cmbProveedores.SelectedItem).ID);

            if (productos!=null && productos.Count>0)
            {
                productos=productos.FindAll(x => x.Enable && x.CodigoInterno == p.CodigoInterno);

                if (productos.Count>0)
                {
                    MessageBox.Show("Ya existe un articulo sin eliminar con el mismo Codigo interno", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
 
            }
           

            return true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            botonlimpiar();
            
        }

        private void botonlimpiar()
        {
            limpiar();
            if (cmbProveedores.Items.Count > -1)
            {
                cmbProveedores.SelectedIndex = 0;
            }
            if (cmbProveedores.SelectedIndex > -1)
            {
                limpiar();
                cargarArticulos(((ProveedorData)cmbProveedores.SelectedItem).ID, null);
                txtCodigoInt.Text = generarCodigoInterno();
            }
            setearAEditar(false);
            cmblineas.SelectedIndex = -1;
            cmbTemporadas.SelectedIndex = -1;
        }
        Guid idp = new Guid();
        private void tabla_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var productoService = new ProductoService(new ProductoRepository());
            if (tabla.SelectedCells.Count>0)
            {
                ProductoData p = new ProductoData();
                idp = new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                p = productoService.GetByID(idp);
                cargarControles(p);
                setearAEditar(true);
            }
        }

        private void cargarControles(ProductoData p)
        {
            //cmbProveedores.Text = p.proveedor.razonSocial;
            txtDescripcion.Text = p.Description;
            txtCodigo.Text = p.CodigoProveedor;
            txtCodigoInt.Text = p.CodigoInterno;
            cmbTemporadas.SelectedItem = ((List<TemporadaData>)cmbTemporadas.DataSource).Find(
                delegate(TemporadaData l)
                {
                    return l.ID == p.Temporada.ID;
                });

            cmblineas.SelectedItem = ((List<LineaData>)cmblineas.DataSource).Find(
                delegate(LineaData l)
                {
                    return l.ID == p.Linea.ID;
                });

            button4.Text = p.Enable ? EliminarText : RetornarText;



        }

        private void setearAEditar(bool p)
        {
            if (p)
            {
                button1.Text = "Editar";
            }else
            {
                button1.Text = "Crear";
                idp = new Guid();
            }
            
        }

        private void tabla_CellContentDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var productoService = new ProductoService(new ProductoRepository());
            if (tabla.SelectedCells.Count > 0)
            {
                ProductoData p = new ProductoData();
                idp = new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                p = productoService.GetByID(idp);
                cargarControles(p);
                setearAEditar(true);
            }
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            if (cmbProveedores.SelectedIndex > -1)
            {
                
                tabla.ClearSelection();
                tabla.Rows.Clear();
                cargarArticulos(((ProveedorData)cmbProveedores.SelectedItem).ID,txtDescripcion.Text,chckvereliminado.Checked);
            }
        }

        private void chckvereliminado_CheckedChanged(object sender, EventArgs e)
        {
            if (cmbProveedores.SelectedIndex > -1)
            {

                tabla.ClearSelection();
                tabla.Rows.Clear();
                cargarArticulos(((ProveedorData)cmbProveedores.SelectedItem).ID, txtDescripcion.Text, chckvereliminado.Checked);
            }
        }

        private void cmbTemporadas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    
    }
}