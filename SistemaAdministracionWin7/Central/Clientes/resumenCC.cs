using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.ClienteRepository;
using Repository.Repositories.NotasRepository;
using Repository.Repositories.ReciboRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.ClienteService;

using Services.ReciboService;
using Services.VentaService;

namespace Central
{
    public partial class resumenCC : Form
    {
        public resumenCC()
        {
            InitializeComponent();
        }

        private void resumenCC_Load(object sender, EventArgs e)
        {
            CargarTabla();
        }
        decimal _tot = 0;
        private void CargarTabla()
        {
            limpiarTabla();

            int fila;
            DateTime maxDateRecibo;
            DateTime maxDateVenta;
            decimal subt;
            var ClienteService = new ClienteService(new ClienteRepository());
           
            List<ClienteData> clientes = ClienteService.GetAll(true);
            foreach (ClienteData cliente in clientes)
            {
                ClienteService.GetCC(cliente, out maxDateRecibo, out maxDateVenta, out subt);

                tabla.Rows.Add();
                fila = tabla.RowCount - 1;
                //Codigo nombre  color talle subtotal
                tabla[0, fila].Value = cliente.RazonSocial;
                tabla[1, fila].Value = subt.ToString();

                tabla[2, fila].Value = maxDateRecibo != DTO.HelperDTO.BEGINNING_OF_TIME_DATE ? maxDateRecibo.ToShortDateString() + " - " + ((int)(DateTime.Today - maxDateRecibo).TotalDays).ToString() + " Dias" : "-";
                tabla[3, fila].Value = maxDateVenta != DTO.HelperDTO.BEGINNING_OF_TIME_DATE ? maxDateVenta.ToShortDateString() + " - " + ((int)(DateTime.Today - maxDateVenta).TotalDays).ToString() + " Dias" : "-";
                _tot += subt;

                subt = 0;
                
             }
            lblTotal.Text = _tot.ToString();




            }

     


        private string calcularSubtotal()
        {
            decimal subotal = 0;
            foreach (DataGridViewRow row in tabla.Rows)
            {
                if (tabla[7, row.Index].Value.ToString() == "No Anulada")
                {
                    if (tabla[2, row.Index].Value.ToString().StartsWith("PAGO"))
                    {
                        //if cobrado???
                        if ((tabla[8, row.Index].Value.ToString() == "cheque" && tabla[6, row.Index].Style.BackColor == System.Drawing.Color.Blue) || (tabla[8, row.Index].Value.ToString() != "cheque"))
                        {
                            subotal += HelperService.ConvertToDecimalSeguro(tabla[4, row.Index].Value.ToString());
                        }
                    }
                    else
                    {
                        subotal -= HelperService.ConvertToDecimalSeguro(tabla[3, row.Index].Value.ToString());
                    }
                }
            }
            return subotal.ToString();
        }

        private void limpiarTabla()
        {
            tabla.ClearSelection();
            tabla.Rows.Clear();
            
        }
    }
}
