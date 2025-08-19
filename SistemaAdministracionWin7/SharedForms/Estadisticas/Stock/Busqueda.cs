using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.ColoresRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.StockRepository;
using Services;
using Services.ColorService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.StockService;

namespace SharedForms.Stock
{
    public partial class Busqueda : Form
    {   
        int maxTalle = 51;
        public Busqueda()
        {
            InitializeComponent();
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedIndex > -1)
            {
                cargarArticulos((ProveedorData)cmbProveedor.SelectedItem);
            }
        }
        private void cagarProveedores()
        {

            var proveedorService = new ProveedorService(new ProveedorRepository());

            cmbProveedor.DataSource = proveedorService.GetAll(true);
            cmbProveedor.DisplayMember = "razonSocial";
        }

        private void cargarArticulos(ProveedorData proveedorData)
        {
            var productoService = new ProductoService(new ProductoRepository());
            cmbArticulo.DataSource = productoService.GetbyProveedor(proveedorData.ID);
            cmbArticulo.DisplayMember = "Show";
        }

        private void cmbArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbArticulo.SelectedIndex>-1)
            {
                cargarTabla((ProductoData)cmbArticulo.SelectedItem);
                
            }
        }

        private void cargarTabla(string codigo)
        {
            var productoService = new ProductoService(new ProductoRepository());
            cargarTabla(productoService.GetProductoByCodigoInterno(codigo,false).First());
        
        }
        private void cargarTabla(ProductoData productoData)
        {

            limpiarTabla();

            bool[] arrayTalle = new bool[51];
            bool[] arrayCol = new bool[_colores.Count+1];

            int auxI = 0;
            foreach (ColorData color in _colores)
	        {
                
        	
	            tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                //codigo nombre  color talle subtotal
                tabla[0, fila].Value = color.Description;

                string stock;
                for (int i = 1; i < maxTalle; i++)
			    {
                    stock = stockService.GetStockTotal(productoData.ID, color.ID, i).ToString();
                        
                     if (stock != "-666")
                     {
                         tabla[i, fila].Value = stock;
                         arrayTalle[i] = true;
                         arrayCol[auxI] = true;
                     }
                     else
                     {
                         tabla[i, fila].Value = "";
                     }

			    }


                auxI++;


	        }
            for (int i = 1; i < maxTalle; i++)
            {
                tabla.Columns[i].Visible = arrayTalle[i];
            }
            for (int i = 0; i < auxI; i++)
            {
                tabla.Rows[i].Visible = arrayCol[i];
            }
            tabla.ClearSelection();

            

        }

        private void limpiarTabla()
        {
            tabla.Rows.Clear();
            tabla.ClearSelection();
        }
        private StockService stockService = null;
        private void Busqueda_Load(object sender, EventArgs e)
        {
            stockService = new StockService(new StockRepository());
            var colorService = new ColorService(new ColorRepository());
            if (HelperService.esCliente(GrupoCliente.Opiparo))
            {
                maxTalle = 20;
            }
            cagarProveedores();
            tunearTabla();
            _colores = colorService.GetAll(true);
        }
        List<ColorData> _colores = new List<ColorData>();
        private void tunearTabla()
        {
            foreach (DataGridViewColumn column in tabla.Columns)
            {
                column.Width = 40;
            }
            tabla.Columns[0].Width = 80;

            for (int i = maxTalle; i < 51; i++)
            {
                tabla.Columns[i].Visible = false;
            }

        }

    
        private bool validoelinterno(string p)
        {
            bool valido = true;

            if (p.Length != 7)
            {
                valido = false;
            }

            return valido;
        }

        private void cargoconelcodigo(string cod)
        {
            string pr = cod.Substring(0, 4);
            string a = cod.Substring(4, 3);



            List<ProveedorData> proveedores = (List<ProveedorData>)cmbProveedor.DataSource;


            cmbProveedor.SelectedItem = proveedores.Find(delegate(ProveedorData p)
            {
                return p.Codigo == pr;
            });


            List<ProductoData> productos = (List<ProductoData>)cmbArticulo.DataSource;



            cmbArticulo.SelectedItem = productos.Find(delegate(ProductoData p)
            {
                return p.CodigoInterno == cod;
            });



        }

        private void txtinterno_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }

            if ((e.KeyChar.ToString() == "\r" && txtinterno.Text.Length >= 7))
            {
                if (HelperService.esCliente(GrupoCliente.CalzadosMell))
                {
                    cargarTabla(txtinterno.Text);
                }
                else
                {
                    if (validoelinterno(txtinterno.Text))
                    {
                        cargoconelcodigo(txtinterno.Text);
                    }
                }
                
            }
        }

    
    }
}