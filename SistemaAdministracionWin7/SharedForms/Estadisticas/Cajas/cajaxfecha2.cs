using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using DTO.BusinessEntities;
using Repository.Repositories.AdministracionRepository;
using Repository.Repositories.CajaRepository;
using Repository.Repositories.FormaPagoRepository;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.OrdenPagoRepository;
using Repository.Repositories.ReciboRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.CajaService;
using Services.FormaPagoService;
using Services.IngresoService;
using Services.LocalService;
using Services.OrdenPagoService;
using Services.ReciboService;
using Services.RetiroService;
using Services.VentaService;
using SharedForms.Ventas;

namespace SharedForms.Estadisticas.Cajas
{
    public partial class cajaxfecha2 : Form
    {
        public cajaxfecha2()
        {
            InitializeComponent();
        }

        int fechaTest;
        private void cargarLocales()
        {
            var localService = new LocalService(new LocalRepository());
            cmbLocales.DataSource = localService.GetAll();
            cmbLocales.DisplayMember = "Codigo";
        }
        private void cerrarCaja_Load(object sender, EventArgs e)
        {
            cargarLocales();
           
        }

        private void cargarCobros(Guid idLocal, DateTime fecha)
        {
            var reciboService = new ReciboService(new ReciboRepository(), new ReciboDetalleRepository());
            List<ReciboData> recibos = reciboService.GetByFecha(fecha, HelperService.IDLocal, HelperService.Prefix,true);

            decimal efec = 0;
            decimal cheque = 0;

            cargoPagosCobros(recibos, out efec, out cheque, tablaCobros);


            txtClienteCheque.Text = cheque.ToString();
            txtClienteEfectivo.Text = efec.ToString();


        }

        private void cargarPagos(Guid idLocal, DateTime fecha)
        {
            var OrdenPagoService = new OrdenPagoService(new OrdenPagoRepository(), new OrdenPagoDetalleRepository());
            List<OrdenPagoData> opagos = OrdenPagoService.GetByFecha(fecha,  idLocal,HelperService.Prefix,true);

            decimal efec = 0;
            decimal cheque = 0;

            cargoPagosCobros(opagos, out efec, out cheque, tablaPagos);


            txtProveedorCheque.Text = cheque.ToString();
            txtProveedorEfectivo.Text = efec.ToString();

        }

        private void cargoPagosCobros(IEnumerable<DocumentoGeneralData<ReciboOrdenPagoDetalleData>> opagos, out decimal efec, out decimal cheque, DataGridView tablaPagos)
        {
            cheque = 0;
            efec = 0;
            bool haycheque = false;
            bool hayefectivo = false;
            string straux = "";
            foreach (ReciboData item in opagos)
            {
                if (item.Enable)
                {
                    foreach (ReciboOrdenPagoDetalleData d in item.Children)
                    {
                        if (d.Cheque!=null && d.Cheque.ID != Guid.Empty)
                        {
                            cheque += d.Monto;
                            haycheque = true;
                        }
                        else
                        {
                            efec += d.Monto;
                            hayefectivo = true;
                        }

                    }
                }
                tablaPagos.Rows.Add();
                int fila;
                fila = tablaPagos.RowCount - 1;
                tablaPagos[0, fila].Value = HelperService.convertToFechaHoraConFormato(item.Date);
                tablaPagos[1, fila].Value = item.tercero.RazonSocial;
                if (hayefectivo)
                    straux += " Efectivo - ";
                if (haycheque)
                    straux += " Cheque - ";
                tablaPagos[2, fila].Value = straux;
                tablaPagos[3, fila].Value = item.Monto;
                tablaPagos[4, fila].Value = !item.Enable ? "Anulado" : "No Anulado";

                if (!item.Enable)
                    tablaPagos.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;


                haycheque = false;
                hayefectivo = false;
                straux = "";
            }
        }



        private void limpiarControles()
        {
            txtFinal.Text = "";
            txtInicial.Text = "";
            txtTotalEfectivo.Text = "";
            txtTotalRetiros.Text = "";
            txtTotalTarjeta.Text = "";
            txtTotalVales.Text = "";
            txtCC.Text = "";



            limpiarTablas();
           

        }

        private void limpiarTablas()
        {
            tablaRetiros.DataSource = null;
            tablaRetiros.ClearSelection();
            tablaRetiros.Rows.Clear();

            tablaVentas.DataSource = null;
            tablaVentas.ClearSelection();
            tablaVentas.Rows.Clear();

            tablaCobros.DataSource = null;
            tablaCobros.ClearSelection();
            tablaCobros.Rows.Clear();

            tablaIngresos.DataSource = null;
            tablaIngresos.ClearSelection();
            tablaIngresos.Rows.Clear();

            tablaPagos.DataSource = null;
            tablaPagos.ClearSelection();
            tablaPagos.Rows.Clear();
        }
        private void cargarVentas(Guid idLocal, DateTime fecha)
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            List<VentaData> ventasHoy = ventaService.GetByFecha(fecha, idLocal, HelperService.Prefix);

            cargoDatos(ventasHoy, tablaVentas, false);

        }

        private void cargoDatos(List<VentaData> ventasHoy, DataGridView tablaVentas, bool agregar)
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            var formaPagoService = new FormaPagoService(new FormaPagoRepository(), new FormaPagoCuotasRepository());
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
                tablaVentas[0, fila].Value = v.Date;
                tablaVentas[1, fila].Value = v.NumeroCompleto;
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
                tablaVentas[5, fila].Value = v.ID;

                if (!v.Enable)
                    tablaVentas.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;


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
            if (txtClienteEfectivo.Text != "")
            {
                cobros = HelperService.ConvertToDecimalSeguro(txtClienteEfectivo.Text);

            }
            if (txtProveedorEfectivo.Text != "")
            {
                pagos = HelperService.ConvertToDecimalSeguro(txtProveedorEfectivo.Text);

            }


            txtFinal.Text = (inicial + ventas - retiros + cobros - pagos + ingresos).ToString();



        }
        private void cargarCajaInicial(Guid idLocal,DateTime fecha)
        {

            var cajaService = new CajaService(new CajaRepository());

            CajaData c = cajaService.GetCajaInicial(DateTime.Now,idLocal);

            txtInicial.Text = c.Monto.ToString();

            if (!HelperService.esCliente(GrupoCliente.Opiparo))
            {
                if (!(((int)DateTime.Now.DayOfWeek == 1 && (int)c.Date.DayOfWeek == 6) || (((int)fecha.DayOfWeek - (int)c.Date.DayOfWeek) == 1)))//si es lunes, hay q restar 2
                {
                    MessageBox.Show("El dia de ayer la caja no ha sido cerrada correctamente, la caja inicial sera tomada como la ultima registrada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }

        private void cargarRetiros(Guid idLocal, DateTime fecha)
        {

            var retiroService = new RetiroService(new RetiroRepository());
            decimal total = 0;
            List<RetiroData> retirosHoy = retiroService.GetByRangoFecha(DateTime.Now.Date.AddDays(-1).AddHours(23).AddMinutes(59), DateTime.Now.Date.AddDays(1), HelperService.IDLocal, HelperService.Prefix);
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


                if (!r.Enable)
                    tablaRetiros.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;



                if (r.Enable)
                {
                    total += r.Monto;
                }


            }

            txtTotalRetiros.Text = total.ToString();

        }

        private void cargarIngresos(Guid idLocal, DateTime fecha)
        {

            decimal total = 0;
            var ingresoService = new IngresoService(new IngresoRepository());

            List<IngresoData> retirosHoy = ingresoService.GetByRangoFecha(DateTime.Now.Date.AddDays(-1).AddHours(23).AddMinutes(59), DateTime.Now.Date.AddDays(1), HelperService.IDLocal, HelperService.Prefix);
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


                if (!r.Enable)
                    tablaIngresos.Rows[fila].DefaultCellStyle.BackColor = Color.Pink;

                if (r.Enable)
                {
                    total += r.Monto;
                }


            }

            txtTotalIngresos.Text = total.ToString();

        }

        //private void cargarCambios(Guid Local, DateTime fecha)
        //{
        //    List<ventaData> cambiosHoy = BusinessComponents.venta.getbyfecha(DateTime.Now.Date, true);

        //    cargoDatos(cambiosHoy, tablaVentas, true);

        //}

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmbLocales.SelectedIndex>=0)
            {
                limpiarControles();
                cargarDatos(((LocalData)cmbLocales.SelectedItem).ID, picker.Value);    
            }
            
        }

        private void cargarDatos(Guid guid, DateTime dateTime)
        {

            cargarCajaInicial(guid,dateTime);
            cargarVentas(guid, dateTime);
            cargarRetiros(guid, dateTime);
            cargarIngresos(guid, dateTime);
            //cargarCambios(guid, dateTime);
            cargarPagos(guid, dateTime);
            cargarCobros(guid, dateTime);
            cargarCajaActual();
        }

        private void tablaVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            VentaData v = ventaService.GetByID(new Guid(tablaVentas.Rows[tablaVentas.SelectedCells[0].RowIndex].Cells[5].Value.ToString()));
            mostrarVenta(v);
        }


        private void mostrarVenta(VentaData v)
        {

        ;

            padreBase.AbrirForm(new mostrarVentaMenor(v), this.MdiParent, true);
        }

        


           

          
            
            
           

        

       



  
    }
}
