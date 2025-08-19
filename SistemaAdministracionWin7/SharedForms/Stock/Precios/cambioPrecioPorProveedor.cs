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
    public partial class cambioPrecioPorProveedor : Form
    {
        public cambioPrecioPorProveedor()
        {
            InitializeComponent();
        }

        private void cambioPrecioPorProveedor_Load(object sender, EventArgs e)
        {
            if (HelperService.talleUnico)
            {
                tabla.Columns[4].Visible = false;
            }
            Cargarlistas();
            CargarProveedores();
        }

        private void Cargarlistas()
        {
            var listaPrecioService = new ListaPrecioService(new ListaPrecioRepository());
            List<listaPrecioData> listas = listaPrecioService.GetAll();
            cmbListaPrecios.DisplayMember = "Description";
            cmbListaPrecios.DataSource = listas;
            cmbListaPrecios.DisplayMember = "Description";
        }

        private void CargarProveedores()
        {
            var proveedorService = new ProveedorService(new ProveedorRepository());
            cmbProveedor.DataSource = proveedorService.GetAll(true);
            cmbProveedor.DisplayMember = "RazonSocial";
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void limpiarTablaArticulos()
        {
            tabla.DataSource = null;
            tabla.ClearSelection();
            tabla.Rows.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in tabla.Rows)
            {
                if (tabla[5, row.Index].Value.ToString() ==  "0")
                {
                    tabla[6, row.Index].Value = "0";
                }
                else
                {
                    tabla[6, row.Index].Value = HelperService.ConvertToDecimalSeguro(tabla[5, row.Index].Value) + (HelperService.ConvertToDecimalSeguro(txtpercent.Text) * HelperService.ConvertToDecimalSeguro(tabla[5, row.Index].Value) / 100);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int hasta = 0;
            int desde = 0;
            string aux = "";
            Guid idproducto = new Guid();
            if (validoTodo())
            {
                foreach (DataGridViewRow row in tabla.Rows)
                {
                    if (tabla[5, row.Index].Value.ToString() != "0" && Convert.ToBoolean(tabla[7, row.Index].Value ))
                    {
                        aux = tabla[4, row.Index].Value.ToString();
                        if (aux.IndexOf('-')>0)
                        {
                            desde = Convert.ToInt32(aux.Split('-')[0]);
                            hasta = Convert.ToInt32(aux.Split('-')[1]);
                        }
                        else
                        {
                            desde =  Convert.ToInt32(aux);
                            hasta = Convert.ToInt32(aux);
                        }
                idproducto = new Guid(tabla[0, row.Index].Value.ToString());

                List<ProductoTalleData> modificados = new List<ProductoTalleData>();

                ProductoTalleData objeto;
                ListaPrecioProductoTalleData ronald = new ListaPrecioProductoTalleData();
                var precioService = new PrecioService(new PrecioRepository());
                var productoTalleService = new ProductoTalleService(new ProductoTalleRepository());
                for (int i = desde; i <= hasta; i++)
                {
                    objeto = new ProductoTalleData();
                    objeto.IDProducto = idproducto;
                    objeto.Talle = i;
                    objeto.ID = productoTalleService.GetIDByProductoTalle(objeto);
                    ronald.FatherID = ((listaPrecioData)cmbListaPrecios.SelectedItem).ID;
                    ronald.ProductoTalle.IDProducto = objeto.ID;
                    ronald.Precio = HelperService.ConvertToDecimalSeguro(tabla[6, row.Index].Value);


                    precioService.InsertOrUpdate(ronald);
                    



                }



                    }
                       
                }
                limpiarTablaArticulos();
                cargarTabla();
            }
        }

        private bool validoTodo()
        {


            if (tabla.Rows.Count==0)
            {
                MessageBox.Show("Debe cargar articulos a la tabla para actualizar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (tabla.Rows[0].Cells[6].Value.ToString()=="-")
            {
                MessageBox.Show("Debe calcular el aumento para continuar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            
            
            
            
            
            
            
            return true;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cargarTabla();
        }

        private void cargarTabla()
        {
            var precioService = new PrecioService(new PrecioRepository());
            var productoTalleService = new ProductoTalleService(new ProductoTalleRepository());
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

                    List<ProductoTalleData> todos = productoTalleService.GetByProducto(p.ID);


                    todos.Sort(delegate(ProductoTalleData x, ProductoTalleData y)
                    {
                        return Convert.ToInt32(x.Talle).CompareTo(Convert.ToInt32(y.Talle));
                    });



                    decimal precio = 0;
                    decimal aux = 0;
                    int inicio = 0;
                    int final = 0;
                    int fila;

                    if (todos.Count > 0)
                    {
                        inicio = todos[0].Talle;
                        aux = precioService.GetPrecio(((listaPrecioData)cmbListaPrecios.SelectedItem).ID, todos[0].ID);
                    }
                    foreach (ProductoTalleData pt in todos)
                    {
                        precio = precioService.GetPrecio(((listaPrecioData)cmbListaPrecios.SelectedItem).ID, pt.ID);
                        if (precio != aux)
                        {
                            final = pt.Talle - 1;

                            tabla.Rows.Add();
                            fila = tabla.RowCount - 1;
                            //id nombre Codigo
                            tabla[0, fila].Value = p.ID;
                            tabla[1, fila].Value = p.Description;

                            tabla[2, fila].Value = p.CodigoProveedor;

                            tabla[3, fila].Value = Convert.ToInt32(p.CodigoInterno).ToString("0000000");
                            tabla[4, fila].Value = inicio == final ? inicio.ToString() : inicio.ToString() + "-" + final.ToString();
                            tabla[5, fila].Value = aux > -200 ? aux.ToString() : "0";
                            tabla[6, fila].Value = "-";
                            tabla[7, fila].Value = true;

                            inicio = pt.Talle;
                            aux = precio;
                        }



                    }
                    final = todos[todos.Count - 1].Talle;

                    tabla.Rows.Add();
                    fila = tabla.RowCount - 1;
                    //id nombre Codigo
                    tabla[0, fila].Value = p.ID;
                    tabla[1, fila].Value = p.Description;

                    tabla[2, fila].Value = p.CodigoProveedor;

                    tabla[3, fila].Value = Convert.ToInt32(p.CodigoInterno).ToString("0000000");
                    tabla[4, fila].Value = inicio.ToString() + "-" + final.ToString();
                    tabla[5, fila].Value = aux > -200 ? aux.ToString() : "0";
                    tabla[6, fila].Value = "-";
                    tabla[7, fila].Value = true;
                    tabla.ClearSelection();
                }


            }
            else
            {
                MessageBox.Show("Seleccione un proveedor y una lista de precios para continuar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
