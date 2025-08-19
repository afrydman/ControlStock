using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.ProductoRepository;
using Repository.Repositories.ProveedorRepository;
using Services.ProductoService;
using Services.ProveedorService;

namespace Central
{
    public partial class Auxiliar : Form
    {
        public Auxiliar()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cagarProveedores();
        }
        private void cagarProveedores()
        {
            cmbProveedor.DisplayMember = "razonSocial";
            cmbProveedor.DataSource = new ProveedorService(new ProveedorRepository()).GetAll(false);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedIndex > -1)
            {

                CrearProductosTalle((ProveedorData)cmbProveedor.SelectedItem);


            }
        }

        private void CrearProductosTalle(ProveedorData proveedorData)
        {
            var productoService = new ProductoService(new ProductoRepository());
            List<ProveedorData> provedoores = new ProveedorService(new ProveedorRepository()).GetAll(false);
            List<ProductoData> ps ;
            int aux = 0;
            var productoTalleService = new ProductoTalleService(new ProductoTalleRepository());
            foreach (ProveedorData proved in provedoores)
            {
                
               



                if (aux>=60 && aux <80)
                {
                    ps = productoService.GetbyProveedor(proved.ID);
                    foreach (ProductoData p in ps)
                    {

                        
                        ProductoTalleData ptalle;
                        for (int i = 0; i < 51; i++)
                        {
                            ptalle = new ProductoTalleData();
                            ptalle.IDProducto = p.ID;
                            ptalle.Talle = i;
                            ptalle.ID = Guid.NewGuid();
                            productoTalleService.Insert(ptalle);

                        }
                    }
                }
                aux++;

                
            }



           
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
