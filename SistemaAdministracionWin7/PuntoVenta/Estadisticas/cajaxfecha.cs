using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.AdministracionRepository;
using Repository.Repositories.CajaRepository;
using Repository.Repositories.FormaPagoRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.CajaService;
using Services.FormaPagoService;
using Services.IngresoService;
using Services.RetiroService;
using Services.VentaService;

namespace PuntoVenta.Estadisticas
{
    public partial class cajaxfecha : Form
    {
        public cajaxfecha()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            verCaja(HelperService.IDLocal, picker.Value);
        }
        private void cargarIngresos(Guid idlocal, DateTime fecha)
        {
            var ingresoService = new IngresoService(new IngresoRepository());
            decimal total = 0;
            List<IngresoData> retirosHoy = ingresoService.GetByFecha(fecha.Date, idlocal, HelperService.Prefix);

            foreach (IngresoData r in retirosHoy)
            {
                tablaIngresos.Rows.Add();
                int fila;
                fila = tablaIngresos.RowCount - 1;
                tablaIngresos[0, fila].Value = r.fechaUso;
                tablaIngresos[1, fila].Value = r.Personal.NombreContacto;
                tablaIngresos[2, fila].Value = r.TipoIngreso.Description;
                tablaIngresos[3, fila].Value = r.Description;
                tablaIngresos[4, fila].Value = r.Monto;
                tablaIngresos[5, fila].Value = !r.Enable ? "Anulada" : "No Anulada";

                if (r.Enable)
                {
                    total += r.Monto;
                }


            }

            txtTotalIngresos.Text = total.ToString();

        }
        private void verCaja(Guid idlocal, DateTime fecha)
        {
            limpiarTablas();
            cargarCajaInicial(idlocal, fecha);
            cargarVentas(idlocal, fecha);
            cargarRetiros(idlocal, fecha);
            cargarIngresos(idlocal, fecha);
            //cargarCambios(idlocal, fecha); los carga con las ventas. 

            cargarCajaActual();


        }

        private void limpiarTablas()
        {
            tablaVentas.Rows.Clear();
          
            tablaRetiros.Rows.Clear();
            tablaIngresos.Rows.Clear();

        }
        //private void cargarCambios(Guid idlocal, DateTime fecha)
        //{
        //    cargoDatos(cambiosHoy, tablaVentas, true);

        //}


        private void cargarRetiros(Guid idlocal, DateTime fecha)
        {
            var retiroService = new RetiroService(new RetiroRepository());
            decimal total = 0;
            List<RetiroData> retirosHoy = retiroService.GetByRangoFecha(fecha.Date.AddDays(-1).AddHours(23).AddMinutes(59), fecha.Date.AddDays(1), idlocal, HelperService.Prefix);
            foreach (RetiroData r in retirosHoy)
            {
                tablaRetiros.Rows.Add();
                int fila;
                fila = tablaRetiros.RowCount - 1;
                tablaRetiros[0, fila].Value = r.fechaUso;
                tablaRetiros[1, fila].Value = r.Personal.NombreContacto;
                tablaRetiros[2, fila].Value = r.TipoRetiro.Description;
                tablaRetiros[3, fila].Value = r.Description;
                tablaRetiros[4, fila].Value = r.Monto;
                tablaRetiros[5, fila].Value = !r.Enable ? "Anulada" : "No Anulada";

                if (r.Enable)
                {
                    total += r.Monto;
                }


            }
            txtTotalRetiros.Text = total.ToString();
         
        }
        private void cargarCajaInicial(Guid idlocal, DateTime fecha)
        {
                var cajaService = new CajaService(new CajaRepository());

                CajaData c = cajaService.GetCajaInicial(fecha.Date, idlocal);

            txtInicial.Text = c.Monto.ToString();

           

        }

        private void cargarCajaActual()
        {

            decimal inicial = 0;
            decimal ventas = 0;
            decimal retiros = 0;
            decimal pagos = 0;
            decimal cobros = 0;
            decimal ingresos = 0;

            if (txtInicial.Text != "")
            {
                inicial = HelperService.ConvertToDecimalSeguro(txtInicial.Text);
            }

            if (txtTotalEfectivo.Text != "")
            {
                ventas = HelperService.ConvertToDecimalSeguro(txtTotalEfectivo.Text);
            }

            if (txtTotalRetiros.Text != "")
            {
                retiros = HelperService.ConvertToDecimalSeguro(txtTotalRetiros.Text);
            }

            if (txtTotalIngresos.Text != "")
            {
                ingresos = HelperService.ConvertToDecimalSeguro(txtTotalIngresos.Text);
            }
         

            txtFinal.Text = (inicial + ventas - retiros + cobros - pagos + ingresos).ToString();



        }
        private void cargarVentas(Guid idlocal, DateTime fecha)
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            List<VentaData> ventasHoy = ventaService.GetByFecha(fecha.Date, idlocal, HelperService.Prefix);

            cargoDatos(ventasHoy, tablaVentas, false);

        }

        private void cargoDatos(List<VentaData> ventasHoy, DataGridView tablaVentas, bool agregar)
        {
            var formaPagoService = new FormaPagoService(new FormaPagoRepository(), new FormaPagoCuotasRepository());

            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            decimal totalE = 0;
            decimal totalT = 0;
            decimal totalVale = 0;
            decimal total = 0;
            decimal totalCC = 0;
            foreach (VentaData v in ventasHoy)
            {

                tablaVentas.Rows.Add();
                int fila;
                fila = tablaVentas.RowCount - 1;
                tablaVentas[0, fila].Value = HelperService.convertToFechaHoraConFormato(v.Date);
                tablaVentas[1, fila].Value = v.NumeroCompleto.ToString();
                foreach (PagoData fp in v.Pagos)
                {
                    if (fp.FormaPago.Description == "" || fp.FormaPago.Description == null)
                    {
                        fp.FormaPago = formaPagoService.GetByID(fp.FormaPago.ID);
                    }
                    tablaVentas[2, fila].Value += fp.FormaPago.Description + " - ";
                }
                total = v.Monto;
                tablaVentas[3, fila].Value = total.ToString();
                tablaVentas[4, fila].Value = !v.Enable ? "Anulada" : "No Anulada";
                tablaVentas[5, fila].Value = v.Cliente.RazonSocial;
                tablaVentas[6, fila].Value = v.Cambio ? "Cambio" : "Venta";



                if (v.Enable)
                {

                    foreach (PagoData fp in v.Pagos)
                    {
                        if (fp.Importe > 0)
                        {

                            if (fp.FormaPago.ID == HelperService.idEfectivo)
                            {
                                totalE += (fp.Importe + (fp.Importe * fp.Recargo / 100));
                            }
                            else if (fp.FormaPago.ID == HelperService.idVale)
                            {
                                totalVale += (fp.Importe + (fp.Importe * fp.Recargo / 100));
                            }
                            else if (fp.FormaPago.ID != HelperService.idCC)
                            {
                                totalT += (fp.Importe + (fp.Importe * fp.Recargo / 100));
                            }
                            else
                            {
                                totalCC += (fp.Importe + (fp.Importe * fp.Recargo / 100));
                            }
                        }
                    }

                }

            } if (agregar)
            {
                decimal aux1 = 0;
                decimal aux2 = 0;
                decimal aux3 = 0;
                decimal aux4 = 0;


                if (txtTotalEfectivo.Text != "")
                {
                    aux1 = HelperService.ConvertToDecimalSeguro(txtTotalEfectivo.Text);
                }

                if (txtTotalTarjeta.Text != "")
                {
                    aux2 = HelperService.ConvertToDecimalSeguro(txtTotalTarjeta.Text);
                }
                if (txtTotalVales.Text != "")
                {
                    aux3 = HelperService.ConvertToDecimalSeguro(txtTotalVales.Text);
                }
                if (txtCC.Text != "")
                {
                    aux4 = HelperService.ConvertToDecimalSeguro(txtCC.Text);
                }


                aux1 += totalE;
                aux2 += totalT;
                aux3 += totalVale;
                aux4 += totalCC;

                txtTotalEfectivo.Text = aux1.ToString();
                txtTotalTarjeta.Text = aux2.ToString();
                txtTotalVales.Text = aux3.ToString();
                txtCC.Text = aux4.ToString();
            }
            else
            {
                txtTotalEfectivo.Text = totalE.ToString();
                txtTotalTarjeta.Text = totalT.ToString();
                txtTotalVales.Text = totalVale.ToString();
                txtCC.Text = totalCC.ToString();
            }

        }

        private void cajaxfecha_Load(object sender, EventArgs e)
        {

        }

    }
}
