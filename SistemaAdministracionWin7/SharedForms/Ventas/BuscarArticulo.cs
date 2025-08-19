using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.ProductoRepository;
using Services.ProductoService;


namespace SharedForms.Ventas
{
    public partial class BuscarArticulo : Form //where T:Form
    {

        private IreceptorArticulo _returnForm;



        public BuscarArticulo(IreceptorArticulo alguno)
        {
            InitializeComponent();
            var productoService = new ProductoService(new ProductoRepository());
            todos = productoService.GetAll(true);
            _returnForm = alguno;
        }
        public BuscarArticulo(IreceptorArticulo alguno, ProveedorData unicoProveedor)
        {
            InitializeComponent();
            var productoService = new ProductoService(new ProductoRepository());
            todos = productoService.GetbyProveedor(unicoProveedor.ID);
            _returnForm = alguno;
        }



        private List<ProductoData> todos; 

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            cargarTabla(textBox1.Text);
        }

        private void limpiarTabla()
        {
            tabla.Rows.Clear();
            tabla.ClearSelection();


        }

        private void cargarTabla(string p)
        {
            limpiarTabla();

            if (p.Length == 0)
                return;

            var productoService = new ProductoService(new ProductoRepository());

            List<ProductoData> rta = productoService.Search(todos, p);

            foreach (ProductoData o in rta)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;

                tabla[0, fila].Value = o.ID;
                tabla[1, fila].Value = o.Proveedor.RazonSocial;
                tabla[2, fila].Value = o.Show;
            }

                

        }

        private void button1_Click(object sender, EventArgs e)
        {
            enviarAForm();
        }

        private void enviarAForm()
        {
            if (tabla.SelectedCells.Count > 0)
            {
               Guid idp = new Guid(tabla.Rows[tabla.SelectedCells[0].RowIndex].Cells[0].Value.ToString());

                _returnForm.selectProducto(idp);
               this.Close();
            }
            else
            {
                MessageBox.Show("Debe de seleccionar un solo articulo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void tabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            enviarAForm();
        }

        private void BuscarArticulo_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
          

        }

        private void BuscarArticulo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "\r")
            {
                enviarAForm();
            }
        }
    }
}
