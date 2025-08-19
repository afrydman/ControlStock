using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.LineaRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.TeporadaRepository;
using Services.LineaService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.TemporadaService;

namespace SharedForms.Stock.Articulos
{
    public partial class ArticulosConMetros : Form
    {
        public ArticulosConMetros()
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
        }

        private void cargarTemporadas()
        {
            var temporadaService = new TemporadaService(new TemporadaRepository());
            cmbTemporadas.DataSource = temporadaService.GetAll();
            cmbTemporadas.DisplayMember = "Description";
        }

        private void cargarLineas()
        {
            var lineaService = new LineaService(new LineaRepository());
            cmblineas.DataSource = lineaService.GetAll();
            cmblineas.DisplayMember = "Description";
        }

        private void cargarCodigoInterno()
        {
            var productoService = new ProductoService(new ProductoRepository());
            txtCodigoInt.Text = productoService.GenerarCodigoInterno(((ProveedorData)cmbProveedores.SelectedItem).Codigo);
        }

        private void cargarProveedores()
        {
            
            cmbProveedores.DataSource =new ProveedorService(new ProveedorRepository()).GetAll(true);
            cmbProveedores.DisplayMember = "RazonSocial";
        }

        private void cargarArticulos()
        {

            cargarArticulos(new Guid(), null);
        }

        private void cargarArticulos(Guid idP, string search,bool verAnulados=true)
        {
            var productoService = new ProductoService(new ProductoRepository());
            List<ProductoData> ps;
            if (idP == new Guid())
            {
                return;
            }
            else
            {
                ps = productoService.GetbyProveedor(idP);
            }


            if (search != null && search.Length > 0)
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
                
                tabla[3, fila].Value = p.Proveedor.RazonSocial;
                tabla[4, fila].Value = p.CodigoProveedor;
                tabla[5, fila].Value = p.CodigoInterno;
                tabla[6, fila].Value = p.Enable ? "No Eliminado" : "Eliminado";


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
        }

        private void cmbProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            var productoService = new ProductoService(new ProductoRepository());
            if (cmbProveedores.SelectedIndex > -1)
            {
                limpiar();
                cargarArticulos(((ProveedorData)cmbProveedores.SelectedItem).ID, null);
                txtCodigoInt.Text = productoService.GenerarCodigoInterno(((ProveedorData)cmbProveedores.SelectedItem).Codigo);
            }


        }

      

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

                    productoService.Disable(new ProductoData(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString())));
                    MessageBox.Show("Articulo anulado de forma correcta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                    cargarArticulos();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult dg = MessageBox.Show("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            var productoService = new ProductoService(new ProductoRepository());
            if (dg == DialogResult.OK)
            {


                ProductoData p = CargarProducto();
                bool task = false;


                if (idp == new Guid())
                {
                    p.ID = Guid.NewGuid();
                    task = productoService.Insert(p, false);//los talles los voy a insertar cada vez q se cree
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
                else
                {
                    p.ID = idp;
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

        private ProductoData CargarProducto()
        {
            ProductoData p;
            p = new ProductoData();

            p.CodigoInterno = txtCodigoInt.Text;
            p.CodigoProveedor = txtCodigo.Text;
            p.Proveedor = (ProveedorData)cmbProveedores.SelectedItem;
            p.Description = txtDescripcion.Text;
            //p.metros = HelperService.ConvertToDecimalSeguro(HelperService.replace_decimal_separator(txtmts.Text));

            if (cmblineas.SelectedIndex > -1)
            {
                p.Linea.ID = ((LineaData)cmblineas.SelectedItem).ID;
            }
            if (cmbTemporadas.SelectedIndex > -1)
            {
                p.Temporada.ID = ((TemporadaData)cmbTemporadas.SelectedItem).ID;
            }
            return p;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var productoService = new ProductoService(new ProductoRepository());
            limpiar();
            if (cmbProveedores.Items.Count > -1)
            {
                cmbProveedores.SelectedIndex = 0;
            }
            if (cmbProveedores.SelectedIndex > -1)
            {
                limpiar();
                cargarArticulos(((ProveedorData)cmbProveedores.SelectedItem).ID, null);
                txtCodigoInt.Text = productoService.GenerarCodigoInterno(((ProveedorData)cmbProveedores.SelectedItem).Codigo);
            }
            cmblineas.SelectedIndex = -1;
            cmbTemporadas.SelectedIndex = -1;
        }
        Guid idp = new Guid();
        private void tabla_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
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

            //txtmts.Text = p.metros.ToString();


        }

        private void setearAEditar(bool p)
        {
            if (p)
            {
                button1.Text = "Editar";
            }
            else
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
                cargarArticulos(((ProveedorData)cmbProveedores.SelectedItem).ID, txtDescripcion.Text,chckvereliminado.Checked);
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


    }
}