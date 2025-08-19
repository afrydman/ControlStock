using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO;
using DTO.BusinessEntities;
using Repository.Repositories.ComprasProveedoresRepository;
using Repository.Repositories.NotasRepository;
using Repository.Repositories.OrdenPagoRepository;
using Repository.Repositories.ProveedorRepository;
using Services;
using Services.ComprasProveedorService;
using Services.NotaService;

using Services.OrdenPagoService;
using Services.ProveedorService;

namespace Central.Proveedores
{
    public partial class resumenCCProveedores : Form
    {
        public resumenCCProveedores()
        {
            InitializeComponent();
        }

        private void resumenCCProveedores_Load(object sender, EventArgs e)
        {
        
        }

        private void CargarTabla()
        {
            limpiarTabla();


            var proveedoresService =new ProveedorService(new ProveedorRepository());
            List<ProveedorData> proveedores = proveedoresService.GetAll(true);
           
            DateTime maxDatePago = new DateTime();
            DateTime maxDateCompra = new DateTime();
            decimal subt = 0;
            int fila;

            foreach (ProveedorData p in proveedores)
            {
              

                proveedoresService.GetCC(p,out maxDatePago,out maxDateCompra,out subt);

                tabla.Rows.Add();
                fila = tabla.RowCount - 1;
                //Codigo nombre  color talle subtotal
                tabla[0, fila].Value = p.RazonSocial;
                tabla[1, fila].Value = subt.ToString();

                tabla[2, fila].Value = maxDatePago != HelperDTO.BEGINNING_OF_TIME_DATE ? maxDatePago.ToShortDateString() + " - " + ((int)(DateTime.Today - maxDatePago).TotalDays).ToString() + " Dias" : "-";
                tabla[3, fila].Value = maxDateCompra != HelperDTO.BEGINNING_OF_TIME_DATE ? maxDateCompra.ToShortDateString() + " - " + ((int)(DateTime.Today - maxDateCompra).TotalDays).ToString() + " Dias" : "-";


                _tot += subt;
                subt = 0;
       
              }
            lblTotal.Text = _tot.ToString();


            }
           

        private void limpiarTabla()
        {
            tabla.ClearSelection();
            tabla.Rows.Clear();
            
        }
        decimal _tot = 0;
        private void resumenCCProveedores_Load_1(object sender, EventArgs e)
        {
            CargarTabla();
        }
    }
}
