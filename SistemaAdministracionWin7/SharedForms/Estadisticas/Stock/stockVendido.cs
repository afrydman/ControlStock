using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.ColoresRepository;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.ColorService;
using Services.LocalService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.VentaService;
using SharedForms.Ventas;

namespace SharedForms.Estadisticas.Stock
{
    public partial class stockVendido : Form , IreceptorArticulo
    {
        public stockVendido()
        {
            InitializeComponent();
        }

        private void stockVendido_Load(object sender, EventArgs e)
        {
            cargarProveedores();
            cargarLocales();
            inicilizoPickers();
            if (HelperService.talleUnico)
                tabla.Columns[3].Visible = false;
        }

        private void inicilizoPickers()
        {
            desde.Value = desde.Value.Date.AddDays(-30);
        }

        private void cargarLocales()
        {
            var localService = new LocalService(new LocalRepository());
            cmbLocales.DisplayMember = "Codigo";
            cmbLocales.DataSource = localService.GetAll();
            
        }


        private void cargarProveedores()
        {
            List<ProveedorData> pvs =new ProveedorService(new ProveedorRepository()).GetAll(false);
            cmbProveedor.DisplayMember = "razonSocial";
            cmbProveedor.DataSource = pvs;
            
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedIndex > -1)
            {
               
                cargarArticulos((ProveedorData)cmbProveedor.SelectedItem);
                

            }
        }

        private void limparTabla()
        {
            tabla.Rows.Clear();
            tabla.ClearSelection();
            lblTotal.Text = "0";
        }
        private void cargarArticulos(ProveedorData proveedorData)
        {
            var productoService = new ProductoService(new ProductoRepository());
            List<ProductoData> ps = productoService.GetbyProveedor(proveedorData.ID);
            cmbArticulos.DisplayMember = "Show";
            cmbArticulos.DataSource = ps;
           
        }

        private void cmbArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cargarTabla()
        {
            List<string> codigos = new List<string>();

            limparTabla();
            var colorService = new ColorService(new ColorRepository());
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            List<VentaDetalleData> ventasdetalle = new List<VentaDetalleData>();
            List<VentaData> ventasConDetalle = new List<VentaData>();

            foreach (DataGridViewRow rFiltro in tablaFiltro.Rows)
            {
                ventasConDetalle.AddRange(ventaService.GetVentasConDetalle(tablaFiltro[0, rFiltro.Index].Value.ToString(),HelperService.IDLocal));
                codigos.Add(tablaFiltro[0, rFiltro.Index].Value.ToString());
            }

            

            List<VentaDetalleData> ventasdetalleOK = new List<VentaDetalleData>();
            List<VentaData> ventasOK = new List<VentaData>();



            foreach (VentaData venta in ventasConDetalle)
            {
            
                if (venta.Local.ID == (((LocalData)cmbLocales.SelectedItem).ID))
                {
                    if (venta.Date > desde.Value && venta.Date < hasta.Value && venta.Enable)
                    {
                        ventasOK.Add(venta);

                    }
                }
            }


            decimal auxcount = 0;
          


            foreach (VentaData aux in ventasOK)
            {

                foreach (var detalle in aux.Children)
                {
                    if (codigos.Contains(detalle.Codigo))
                    {
                        tabla.Rows.Add();
                        int fila;
                        fila = tabla.RowCount - 1;
                        tabla[0, fila].Value = HelperService.convertToFechaHoraConFormato(aux.Date);
                        tabla[1, fila].Value = aux.NumeroCompleto;
                        tabla[2, fila].Value = colorService.GetByCodigo(detalle.Codigo.Substring(7, 3)).Description;
                        tabla[3, fila].Value = detalle.Codigo.Substring(10, 2);
                        tabla[4, fila].Value = detalle.PrecioUnidad.ToString();
                        tabla[5, fila].Value = detalle.Cantidad.ToString();
                        auxcount += detalle.Cantidad;  
                    }
                   
                }
            

            }




            lblTotal.Text = auxcount.ToString();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbLocales_SelectedIndexChanged(object sender, EventArgs e)
        {
            limparTabla();
          
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tablaFiltro.Rows != null && tablaFiltro.Rows.Count > 0)
            {
                cargarTabla();
            }
            if (tabla.Rows.Count==0)
            {
                MessageBox.Show("Sin registros para los articulos seleccionados", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void selectProducto(Guid idproducto)
        {
            
            var productoService = new ProductoService(new ProductoRepository());
            ProductoData p = productoService.GetByID(idproducto);

            

            cmbProveedor.SelectedItem = p.Proveedor;
            cmbProveedor.SelectedIndex = cmbProveedor.FindStringExact(p.Proveedor.RazonSocial);
            cmbArticulos.SelectedIndex = cmbArticulos.FindStringExact(p.Show);

            txtinterno.Text = "";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (buscoynoestaenlatabla())
            {

                tablaFiltro.Rows.Add();
                int fila;
                fila = tablaFiltro.RowCount - 1;
                ProductoData producto = (ProductoData)cmbArticulos.SelectedItem;

                tablaFiltro[0, fila].Value = producto.CodigoInterno;//Codigo interno
                tablaFiltro[1, fila].Value = ((ProveedorData)cmbProveedor.SelectedItem).RazonSocial; ;//proveedor
                tablaFiltro[2, fila].Value = producto.Show;//articulo    
            }
            else
            {
                MessageBox.Show("Articulo ya seleccionado");
            }

        }

        private bool buscoynoestaenlatabla()
        {
            if (tablaFiltro.Rows!=null && tablaFiltro.Rows.Count>0)
            {
                foreach (DataGridViewRow rFiltro in tablaFiltro.Rows)
                {
                    if (tablaFiltro[0, rFiltro.Index].Value == ((ProductoData) cmbArticulos.SelectedItem).CodigoInterno)
                        return false;
                }
            }
            return true;
        }

        private void txtinterno_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtinterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r" && (txtinterno.Text == "*" || txtinterno.Text == "0"))
            {
                padreBase.AbrirForm(new BuscarArticulo(this), this.MdiParent, false, FormStartPosition.CenterScreen);
            }
        }
    }
}

