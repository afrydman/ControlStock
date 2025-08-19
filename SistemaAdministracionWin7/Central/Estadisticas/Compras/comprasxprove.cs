using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.ColoresRepository;
using Repository.Repositories.ComprasProveedoresRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Services.ColorService;
using Services.ComprasProveedorService;
using Services.ProductoService;
using Services.ProveedorService;

namespace Central.Estadisticas.Compras
{
    public partial class comprasxprove : Form
    {
        public comprasxprove()
        {
            InitializeComponent();
        }

        private void comprasxprove_Load(object sender, EventArgs e)
        {
            cargarProveedores();
            inicilizoPickers();
        }
        private void inicilizoPickers()
        {
            picker.Value = DateTime.Now.AddDays(-30);
        }
        private void cargarProveedores()
        {
            cmbProveedores.DisplayMember = "razonsocial";
            cmbProveedores.DataSource = new ProveedorService(new ProveedorRepository()).GetAll(true);
            
        }

        private void cmbProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cargarTabla()
        {
            limpiarTabla();
            var comprasProveedoresService = new ComprasProveedorService(new ComprasProveedoresRepository(),
                new CompraProveedoresDetalleRepository());
            var productoService = new ProductoService(new ProductoRepository());

            List<ComprasProveedoresData> cs = comprasProveedoresService.GetbyProveedor(((ProveedorData)cmbProveedores.SelectedItem).ID, true);


            cs = cs.FindAll(c => c.Date >= picker.Value && c.Date <= pickerHasta.Value);

            

            decimal pares = 0;
            string codigo;
            string codPro;
            string codArt;
            string codCol;
            foreach (ComprasProveedoresData r in cs)
            {

                foreach (ComprasProveedoresdetalleData item in r.Children)
	                {
                	
	                codigo = item.Codigo;
                    
                codPro = codigo.Substring(0, 4);
                codArt = codigo.Substring(0, 7);
                codCol = codigo.Substring(7, 3);

                ProductoData p = productoService.GetProductoByCodigoInterno(codArt,false).First();
	                    var proveedorService = new ProveedorService(new ProveedorRepository());

                        ProveedorData pr = proveedorService.GetByID(p.Proveedor.ID);
	                    var colorService = new ColorService(new ColorRepository());
                        
                    ColorData col = colorService.GetByCodigo(codCol);

                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                //Codigo nombre  color talle subtotal
                tabla[0, fila].Value = r.Date;
                tabla[1, fila].Value = p.Show;
                tabla[2, fila].Value = col.Description;
                tabla[3, fila].Value = codigo.Substring(10, 2);
                tabla[4, fila].Value = item.Cantidad;
                tabla[5, fila].Value = r.ID;

                tabla[6, fila].Value = item.PrecioUnidad.ToString();
                tabla[7, fila].Value = item.PrecioExtra.ToString();
                tabla[8, fila].Value = item.SubTotal.ToString();
                pares+=item.Cantidad;
                tabla.ClearSelection();



	                }
                                



            }
            lblPares.Text = pares.ToString();




        }

        private void limpiarTabla()
        {
            tabla.Rows.Clear();
            tabla.ClearSelection();
            lblPares.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (valido())
            {
                cargarTabla();
            }
        }

        private bool valido()
        {
           
            if (cmbProveedores.SelectedIndex == -1)
            {
                MessageBox.Show("Primero debe de seleccionar un proveedor", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }

            if (picker.Value>pickerHasta.Value)
            {
                MessageBox.Show("Error al seleccionar las fechas", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }



            return true;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
