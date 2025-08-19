using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.ListaPrecioRepository;
using Repository.Repositories.PrecioRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Services;
using Services.ListaPrecioService;
using Services.PrecioService;
using Services.ProductoService;
using Services.ProveedorService;

namespace SharedForms.Stock.Precios
{
    public partial class cambioPrecios : Form
    {
        public cambioPrecios()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void cambioPrecios_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = (HelperService.talleUnico) || (HelperService.haymts) ? "Precios" : "Precios X talle";
            if (HelperService.talleUnico)
            {
                label6.Visible = false;
                label2.Visible = false;
                txtTalleDesde.Visible = false;
                txtTalleHasta.Visible = false;
                tablaPrecios.Columns[0].Visible = false;
            }
            tunearForm();
            cargarProveedores();
            cargarListaPrecios();
        }

        private void cargarProveedores()
        {
            cmbProveedor.DisplayMember = "RazonSocial";
            cmbProveedor.DataSource =new ProveedorService(new ProveedorRepository()).GetAll(true);
            cmbProveedor.DisplayMember = "RazonSocial";
        }

        private void tunearForm()
        {
            if (HelperService.talleUnico)
            {
                txtTalleDesde.ReadOnly = true;
                txtTalleHasta.ReadOnly = true;
                txtTalleHasta.Text = "0";
                txtTalleDesde.Text = "0";
            }
        }

        private void cargarListaPrecios()
        {
            var listaPrecioService = new ListaPrecioService(new ListaPrecioRepository());
            List<listaPrecioData> listas = listaPrecioService.GetAll();
            cmbListaPrecios.DisplayMember = "Description";
            cmbListaPrecios.DataSource = listas;
            cmbListaPrecios.DisplayMember = "Description";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
     


        }

        private void limpiarTablaArticulos()
        {
            lblTitulo.Text = (HelperService.talleUnico) || (HelperService.haymts) ? "Precios" : "Precios X talle";
            tabla.DataSource = null;
            tabla.ClearSelection();
            tabla.Rows.Clear();
        }

        private void tabla_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            CargarTalles();



        }

        private void CargarTalles()
        {
            var productoTalleService = new ProductoTalleService(new ProductoTalleRepository());


            if (tabla.SelectedCells.Count > 0)
            {


                limpiarTablaPrecios();


                List<ProductoTalleData> todos = productoTalleService.GetByProducto(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));

                string aux = (HelperService.talleUnico) || (HelperService.haymts) ? "Precios " : "Precios X talle ";


                lblTitulo.Text = aux + tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                cargarPrecios(todos);
                cargarControles();
            }
        }

        private void cargarControles()
        {
        //descr prove cod codint

            txtDescripcion.Text = tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
            txtProveedor.Text = tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
             txtCodigo.Text= tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[3].Value.ToString();
             txtInterno.Text = tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[4].Value.ToString(); 
        }

        private void limpiarTablaPrecios()
        {

            
            tablaPrecios.DataSource = null;
            tablaPrecios.ClearSelection();
            tablaPrecios.Rows.Clear();

        }

        private void cargarPrecios(List<ProductoTalleData> todos)
        {

            var precioService = new PrecioService(new PrecioRepository());

            decimal precio = 0;
            decimal aux = 0;
            int inicio = 0;
            int final = 0;
            int fila;

            if (todos.Count > 0)
            {
                inicio = todos[0].Talle;
                aux = precioService.GetPrecio(((listaPrecioData)cmbListaPrecios.SelectedItem).ID, todos[0].ID);
                final = todos[todos.Count - 1].Talle;
            }
            foreach (ProductoTalleData pt in todos)
            {
                precio = precioService.GetPrecio(((listaPrecioData)cmbListaPrecios.SelectedItem).ID, pt.ID);
                if (precio != aux)
                {
                    final = pt.Talle - 1;

                    tablaPrecios.Rows.Add();
                    fila = tablaPrecios.RowCount - 1;
                    //id nombre Codigo
                    tablaPrecios[0, fila].Value = inicio == final ? inicio.ToString() : inicio.ToString() + "-" + final.ToString();
                    tablaPrecios[1, fila].Value = aux > -200 ? aux.ToString() : "0";

                    inicio = pt.Talle;
                    aux = precio;
                }
            }

            

            tablaPrecios.Rows.Add();
            fila = tablaPrecios.RowCount - 1;
            //id nombre Codigo
            tablaPrecios[0, fila].Value = inicio.ToString() + "-" + final.ToString();
            tablaPrecios[1, fila].Value = aux > -200 ? aux.ToString() : "0";
            tablaPrecios.ClearSelection();
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //update precios
            if (valido()){
                int hasta = Convert.ToInt32(txtTalleHasta.Text);
                int desde =  Convert.ToInt32(txtTalleDesde.Text);
                
                int aux = hasta-desde+1;
                Guid idproducto = new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
                var productoTalleService = new ProductoTalleService(new ProductoTalleRepository());
                List<ProductoTalleData> modificados = new List<ProductoTalleData>();

                ProductoTalleData objeto;
                ListaPrecioProductoTalleData ronald = new ListaPrecioProductoTalleData();
                var precioService = new PrecioService(new PrecioRepository());
               
                for (int i = desde; i <= hasta; i++)
                {
                     objeto= new ProductoTalleData();
                     objeto.IDProducto = idproducto;
                     objeto.Talle = i;
                     objeto.ID = productoTalleService.GetIDByProductoTalle(objeto);
                     ronald.FatherID = ((listaPrecioData)cmbListaPrecios.SelectedItem).ID;
                     ronald.ProductoTalle.IDProducto = objeto.ID;
                     ronald.Precio =HelperService.ConvertToDecimalSeguro(txtNuevo.Text);

                    precioService.InsertOrUpdate(ronald);

                }
                limpiarTablaPrecios();
                MessageBox.Show("Precio Actualizado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                List<ProductoTalleData> todos = productoTalleService.GetByProducto(new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));


                cargarPrecios(todos);

            }
            

            



        }

        private bool valido()
        {
            bool valido = true;
            if (tabla.Rows==null || tabla.Rows.Count==0)
            {
                return false;
            }

            if (tabla.SelectedCells.Count != 1)
            {
                valido = false;
            }
            int hasta = Convert.ToInt32(txtTalleHasta.Text);
            int desde = Convert.ToInt32(txtTalleDesde.Text);

            

            if (desde>hasta)
            {
                valido = false;
            }




            return valido;
        }

        private void cmbListaPrecios_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiarTablaPrecios();
        }

        private void txtNuevo_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperService.VerificoTextBoxNumerico(txtNuevo, e);
            
            
        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void txtTalleDesde_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTalleDesde_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTalleHasta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var productoService = new ProductoService(new ProductoRepository());
            if (cmbProveedor.SelectedIndex > -1)
            {
                limpiarTablaArticulos();
                List<ProductoData> busqueda = productoService.GetbyProveedor(((ProveedorData)cmbProveedor.SelectedItem).ID);



                busqueda.Sort(delegate(ProductoData x, ProductoData y)
                {
                    return Convert.ToInt32(x.CodigoInterno).CompareTo(Convert.ToInt32(y.CodigoInterno));
                });

                foreach (ProductoData p in busqueda)
                {
                    tabla.Rows.Add();
                    int fila;
                    fila = tabla.RowCount - 1;
                    //id nombre Codigo
                    tabla[0, fila].Value = p.ID;
                    tabla[1, fila].Value = p.Description;

                    tabla[2, fila].Value = ((ProveedorData)cmbProveedor.SelectedItem).RazonSocial;
                    tabla[3, fila].Value = p.CodigoProveedor;

                    tabla[4, fila].Value = Convert.ToInt32(p.CodigoInterno).ToString("0000000");
                    tabla.ClearSelection();
                }

                limpiarTablaPrecios();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            CargarTalles();
        }

 
    }
}