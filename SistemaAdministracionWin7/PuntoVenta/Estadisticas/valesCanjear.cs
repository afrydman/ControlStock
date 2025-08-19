using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DTO.BusinessEntities;

using Repository.Repositories.ValeRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.PagoService;
using Services.ValeService;
using Services.VentaService;

namespace PuntoVenta.Estadisticas
{
    public partial class valesCanjear : Form
    {
        public valesCanjear()
        {
            InitializeComponent();
        }

        private void valesCanjear_Load(object sender, EventArgs e)
        {
            desde.Value = desde.Value.Date.AddDays(-30);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiarTablas();
            if (valido())
            {
                //obtengo vales emitidos
                cargarVales();
            


                //obtengo vales recibidos
                cargarPagosVales();
            }
        }

        private void limpiarTablas()
        {
            tabla.Rows.Clear();
            tabla.ClearSelection();

            tabla2.Rows.Clear();
            tabla2.ClearSelection();
        }

        private void cargarPagosVales()
        {

            
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            List<PagoData> pagos = new PagoService().GetPagosByTipo(HelperService.idVale);
            List<VentaData> ventas = new List<VentaData>();
            VentaData v;
            foreach (PagoData p in pagos)
            {
                v = ventaService.GetByID(p.FatherID);
                ventas.Add(v);
            }

            ventas = ventas.FindAll(delegate(VentaData x) { return x.Date >= desde.Value.Date && x.Date <= hasta.Value.Date.AddDays(1); });
                
            foreach (VentaData vv in ventas)
            {
                tabla2.Rows.Add();
                int fila;
                fila = tabla2.RowCount - 1;
                tabla2[0, fila].Value = vv.ID;
                tabla2[1, fila].Value = vv.NumeroCompleto;
                tabla2[2, fila].Value = HelperService.convertToFechaConFormato(vv.Date);
                tabla2[3, fila].Value = "Venta";
                tabla2[4, fila].Value = vv.Pagos.Find(delegate(PagoData x) { return x.FormaPago.ID == HelperService.idVale; }).Importe;
                tabla2[5, fila].Value = !vv.Enable?"Anualada":"No Anulada";
                //tabla2[6, fila].Value = vv.Fecha;
                if (!vv.Enable)
                    tabla2.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;
                
            }
        }

            private void cargarVales()
            {

                var valeService = new ValeService(new ValeRepository());

                List<valeData> vales = valeService.GetByRangoFecha(desde.Value, hasta.Value.Date.AddDays(1), HelperService.IDLocal, HelperService.Prefix);

            foreach (valeData v in vales)
            {

                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                tabla[0, fila].Value = v.ID;
                tabla[1, fila].Value = v.Numero;
                tabla[2, fila].Value = HelperService.convertToFechaConFormato(v.Date);
                tabla[3, fila].Value = v.EsCambio?"Cambio":"Sena Cliente";
                tabla[4, fila].Value = v.Monto;
                tabla[5, fila].Value = !v.EsCambio?"Anulado":"No Anulado";
                tabla[6, fila].Value = v.idAsoc;

                if (!v.Enable)
                    tabla.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;
                
            }


        }


        private bool valido()
        {
            if (desde.Value>=hasta.Value)
            {
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());

            #region limpioVales
            List<valeData> vales = new List<valeData>();
            valeData v;
            foreach (DataGridViewRow row in tabla.Rows)
            {
                if (!Convert.ToBoolean(tabla[7,row.Index].Value))
                {// no esta marcada
                    v = new valeData();
                    v.ID = new Guid(tabla[0, row.Index].Value.ToString());
                    v.Numero = Convert.ToInt32(tabla[1, row.Index].Value.ToString());
                    v.Date = Convert.ToDateTime(tabla[2, row.Index].Value.ToString());
                    v.EsCambio = tabla[3, row.Index].Value.ToString() == "Cambio";
                    v.Monto = HelperService.ConvertToDecimalSeguro(tabla[4, row.Index].Value.ToString());
                    v.Enable = tabla[5, row.Index].Value.ToString()!="Anulado";
                    v.idAsoc = new Guid(tabla[6, row.Index].Value.ToString());
                    vales.Add(v);
                }
            }

            tabla.Rows.Clear();

            foreach (valeData vv in vales)
            {

                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                tabla[0, fila].Value = vv.ID;
                tabla[1, fila].Value = vv.Numero;
                tabla[2, fila].Value = vv.Date;
                tabla[3, fila].Value = vv.EsCambio ? "Cambio" : "Sena Cliente";
                tabla[4, fila].Value = vv.Monto;
                tabla[5, fila].Value = !vv.EsCambio ? "Anulado" : "No Anulado";
                tabla[6, fila].Value = vv.idAsoc;

            }

            #endregion

            #region pagos

            List<VentaData> ventas = new List<VentaData>();
            VentaData ventita;
            
            foreach (DataGridViewRow row in tabla2.Rows)
            {
                if (!Convert.ToBoolean(tabla2[7, row.Index].Value))
                {// no esta marcada

                    ventita = new VentaData();
                    ventita.ID = new Guid(tabla2[0, row.Index].Value.ToString());
                    ventas.Add(ventaService.GetByID(ventita.ID));
                }
            }

            tabla2.Rows.Clear();



            foreach (VentaData vv in ventas)
            {
                tabla2.Rows.Add();
                int fila;
                fila = tabla2.RowCount - 1;
                tabla2[0, fila].Value = vv.ID;
                tabla2[1, fila].Value = vv.NumeroCompleto;
                tabla2[2, fila].Value = vv.Date;
                tabla2[3, fila].Value = "Venta";
                tabla2[4, fila].Value = vv.Pagos.Find(delegate(PagoData x) { return x.FormaPago.ID == HelperService.idVale; }).Importe;
                tabla2[5, fila].Value = !vv.Enable ? "Anualada" : "No Anulada";
                tabla2[6, fila].Value = vv.Date;


            }





            #endregion


        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
