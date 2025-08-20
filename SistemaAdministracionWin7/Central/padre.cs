using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Central.Estadisticas.Compras;
using Central.GenericForms;
using Central.Proveedores;
using DTO.BusinessEntities;
using Repository.ChequeraRepository;
using Repository.ChequeRepository;
using Repository.ClienteRepository;
using Repository.Repositories.AdministracionRepository;
using Repository.Repositories.BancosRepository;
using Repository.Repositories.CajaRepository;
using Repository.Repositories.NotasRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.UsuarioRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.AdministracionService;
using Services.BancoService;
using Services.CajaService;
using Services.ChequeraService;
using Services.ChequeService;
using Services.LineaService;
using Services.ListaPrecioService;
using Services.NotaService;
using Services.ProveedorService;
using Services.TemporadaService;
using Services.UsuarioService;
using SharedForms;
using SharedForms.Estadisticas.Cajas;
using SharedForms.Estadisticas.Cajas;
using SharedForms.Estadisticas.RetirosIngresos;
using SharedForms.Estadisticas.Stock;
using SharedForms.Estadisticas.Stock.PuntoControl;
using SharedForms.Estadisticas.Stock.StockMetros;
using SharedForms.Estadisticas.Ventas;
using SharedForms.Impositivo;
using SharedForms.Stock;
using SharedForms.Stock.Administrar;
using SharedForms.Stock.Articulos;
using SharedForms.Stock.Precios;
using SharedForms.Ventas;
using Services.ClienteService;
using Services.JsonSerializationService;
using DTO;
using Helper.LogService;


namespace Central
{
    public partial class padre : padreBase
    {
        public padre()
        {
            InitializeComponent();
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void padre_Load(object sender, EventArgs e)
        {
            //if (ConfigurationManager.AppSettings["updateDB"] != null && Convert.ToBoolean(ConfigurationManager.AppSettings["updateDB"].ToString()))
            //{
            //    string newCatalog = "";

            //    ResultadoActualizacion r = verificoNuevaVersion(Central.Properties.Settings.Default.currentCatalog, out newCatalog);

            //    if (r == ResultadoActualizacion.Actualizacion_correcta && !string.IsNullOrWhiteSpace(newCatalog))
            //    {

            //        UpdateConfig(newCatalog);

            //        MessageBox.Show("Actualizacion realizada correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        HelperService.writeLog("Actualizacion realizada correctamente \n db version: " + newCatalog, true, true);
            //    }
            //    else if (r == ResultadoActualizacion.Actualizacion_conErrores)
            //    {
            //        MessageBox.Show("Error al actualizar la DB \n Contactese con el administrador", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        HelperService.writeLog("Error al actualizar la db", true, true);
            //        Application.Exit();
            //    }
            //}


            loadLocalInformation();
//            conexion.getLocalConection();
            
            
            HelperService.LoadConfigurations();
            HelperService.writeLog("Iniciando Sistema", true);
            this.WindowState = FormWindowState.Maximized;

            setmenuesbyClient();
            MostrarAlertas();

        }

        private void UpdateConfig(string newCatalog)
        {
            try
            {


                Properties.Settings.Default.currentCatalog = newCatalog;
                Properties.Settings.Default.Save(); //

            }
            catch (Exception e)
            {
                HelperService.writeLog(e.ToString(), true, true);
                MessageBox.Show("Error al actualizar la Configuracion \n Contactese con el administrador", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                Application.Exit();
            }
        }


        private void MostrarAlertas()
        {
            AlertaCheques();
        }

        private void AlertaCheques()
        {
            //verifico cheques a cobrar
            bool alerta = false;

            List<EstadoCheque> e = new List<EstadoCheque>();
            e.Add(EstadoCheque.EnCartera);

            var chequeraService = new ChequeraService(new ChequeraRepository());
            var chequeService = new ChequeService(new ChequeRepository());

            List<ChequeData> cs = chequeService.GetChequesTercero(true, e,true);

            if (cs.Exists(delegate(ChequeData c) { return c.FechaCobro.Date <= DateTime.Now.Date; }))
            {
                alerta = true;
            }

            if (!alerta)
            {//verfico cheques a pagar
                List<ChequeraData> chequerasPropias = chequeraService.GetAll(true);

                List<ChequeData> cheques = new List<ChequeData>();
                foreach (ChequeraData chequera in chequerasPropias)
                {
                    cheques.AddRange(chequeService.GetByChequera(chequera.ID, true));
                }
                if (cheques.Exists(delegate(ChequeData c) { return c.FechaCobro.Date <= DateTime.Now.Date; }))
                {
                    alerta = true;
                }


            }




            if (alerta)
            {
                MessageBox.Show("El sistema detecto cheques proximos a cobrar/pagar \n  Para visualizarlos vaya a Cheques-> Avisos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setmenuesbyClient()
        {
            if (HelperService.esCliente(GrupoCliente.Chinela))
            {
                //cajaToolStripMenuItem.Visible = false;

                //ventasToolStripMenuItem1.Visible = false;
                auxToolStripMenuItem.Visible = false;

            }
            if (HelperService.esCliente(GrupoCliente.Slipak))
            {
                //stockVendidoToolStripMenuItem.Visible = false;
                //stockXLocalToolStripMenuItem.Visible = false;
                //stockActualXLocalToolStripMenuItem.Visible = false;
                //stockDetalladoXToolStripMenuItem.Visible = false;
                
                //localesToolStripMenuItem.Visible = true;
                //localesToolStripMenuItem.Text = "Centros";
                //administrarLocalesToolStripMenuItem.Text = "Administrar Centros";
                //personalToolStripMenuItem.Visible = false;
                //administrarLineaToolStripMenuItem.Visible = false;
                //administrarTemporadaToolStripMenuItem.Visible = false;
                //imprimirEtiquetasToolStripMenuItem.Visible = false;

            }

            if (HelperService.esCliente(GrupoCliente.Balarino))
            {
                nuevoEnvioToolStripMenuItem.Text = "Baja de Stock";
                verEnviosToolStripMenuItem.Text = "Ver Movimientos de Stock";
            }

        }

       

        private void administrarProveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nombre = "Administrar Proveedores";
            AbrirForm(new genericoPersona<ProveedorData>(new ProveedorService(new ProveedorRepository()), nombre), this, false, FormStartPosition.CenterScreen, nombre);
        }

        private void coloresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new Colores(), this);
        }

        private void preciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new cambioPrecios(), this);
        }

        private void administrarArticulosToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void nuevaCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CerrarForm(new NuevaOPago(), this);
            CerrarForm(new cuentaCorriente<ProveedorData>(), this);
            AbrirForm(new comprasAProveedores(), this);
        }

        private void administrarPersonalEnLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void administrarListaDePreciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new CommonFormOneField<listaPrecioData>(new ListaPrecioService(), "Lista Precio"), this);
            
        }

        private void administrarFormasDePagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new formasPago(), this);
        }

        private void administrarRetirosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void compraToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void comprasXDiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new comprasxdia(), this);
        }

        private void comprasXProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new comprasxprove(), this);
        }

        private void ventasXFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new ventasxFecha(), this);
        }

        private void retirosXFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new retiros_ingresosxfecha<RetiroData>(), this);
        }

        private void cajaXFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new cajaxfecha2(), this);
        }

        private void ventasXProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void stockVendidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new stockVendido(), this);
        }

        private void administrarLineaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new CommonFormOneField<LineaData>(new LineaService(), "Linea"), this);
        }

        private void administrarTemporadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new CommonFormOneField<TemporadaData>(new TemporadaService(),"Temporada"), this);
        }

        private void administrarClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nombre="Administrar Clientes";



            AbrirForm(new genericoPersona<ClienteData>(new ClienteService(), nombre), this, false, FormStartPosition.CenterScreen, nombre);
        }

        private void nuevaNotaDeCreditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarForm(new cuentaCorriente<ClienteData>(), this);
            CerrarForm(new cuentaCorriente<ProveedorData>(), this);
            CerrarForm(new cerrarCaja(), this);
            AbrirForm(new NuevoRecibo(), this);

        }

        private void cCClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verCCToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string name = "Cuenta Corriente Clientes";
            CerrarForm(new cerrarCaja(), this);

            AbrirForm(new cuentaCorriente<ClienteData>(), this,false,FormStartPosition.WindowsDefaultLocation,name);
        }

        private void cuentaCorrientesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verCCToolStripMenuItem1_Click(object sender, EventArgs e)
        {


          
            bool existeAbierto = false;

            if (this.MdiChildren.Length > 0)
            {
                foreach (Form hijo in this.MdiChildren)
                {
                    if (hijo.GetType() == typeof(cerrarCaja) || hijo.GetType() == typeof(comprasAProveedores) || hijo.GetType() == typeof(NuevaOPago))
                    {
                        existeAbierto = true;
                        break;
                    }

                }


                if (existeAbierto)
                {
                    DialogResult dg = MessageBox.Show("Al Abrir cuenta Corriente, se cerraran los demas formularios \n Desea continuar?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    if (dg == DialogResult.OK)
                    {
                        CerrarForm(new cerrarCaja(), this);
                        CerrarForm(new comprasAProveedores(), this);
                        CerrarForm(new NuevaOPago(), this);

                       
                        CerrarForm(new cerrarCaja(), this);
                      



                    }
                }
            }


            string name = "Cuenta Corriente Proveedores";
            AbrirForm(new cuentaCorriente<ProveedorData>(), this, false, FormStartPosition.WindowsDefaultLocation, name);


        }

        private void nuevaNotaCreditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Al Abrir Orden de Pago, se cerraran los demas formularios \n Desea continuar?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dg == DialogResult.OK)
            {
                CerrarForm(new cerrarCaja(), this);
                CerrarForm(new comprasAProveedores(), this);
                CerrarForm(new cuentaCorriente<ProveedorData>(), this);
                AbrirForm(new NuevaOPago(), this);
            }

        }

        private void nuevoEnvioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new BajaStock(), this);
        }

        private void verEnviosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new verEnviosStock(), this);
        }

        private void stockXLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new stockVendidoTotal(), this);
        }

        private void auxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new Auxiliar(), this);
        }

        private void stockDetalladoXToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (HelperService.esCliente(GrupoCliente.Slipak))
            {
                AbrirForm(new DetallestockLocalMts(), this);
            }
            else
            {
                AbrirForm(new detallestockLocal(), this);
            }

        }

        private void stockXLocalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (HelperService.esCliente(GrupoCliente.Slipak))
            {
                AbrirForm(new StockBusquedaMts(), this);
            }
            else if (!HelperService.talleUnico)
            {
                AbrirForm(new stockBusqueda(), this);
            }
            else
            {
                AbrirForm(new StockBusquedaUnico(), this);
            }

        }

        private void altaDeStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new AltaStock(), this);
        }

        private void nuevaVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarForm(new cerrarCaja(), this);
            AbrirForm(new ventaMayor(), this);
        }

        private void verVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AbrirForm(new Ventas(), this);
        }

        private void altaManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new altaManual(), this);
        }

        private void bajaDeStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new BajaStock(), this);
        }

        private void verRemitosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new verEnviosStock(), this);
        }

        private void nuevoRetiroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarForm(new cerrarCaja(), this);
            AbrirForm(new SharedForms.Impositivo.retiro(), this);
        }

        private void cerrarCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarForm(new SharedForms.Impositivo.retiro(), this);
            CerrarForm(new ventaMayor(), this);
            CerrarForm(new Ventas(), this);
            CerrarForm(new mostrarVentaMenor(), this);


            CerrarForm(new NuevaOPago(), this);
            CerrarForm(new cuentaCorriente<ClienteData>(), this);
            CerrarForm(new cuentaCorriente<ProveedorData>(), this);
            CerrarForm(new cuentaCorriente<ProveedorData>(), this);


            AbrirForm(new cerrarCaja(), this);
        }

        private void modificarCajaInicialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            padre.CerrarForm(new cerrarCaja(), this);

            try
            {
                string resultado = "";
                helperForms.InputBox("Caja", "Ingrese el Monto de la caja", ref resultado, false);
                decimal dresultado = HelperService.ConvertToDecimalSeguro(resultado);
                resultado = "";
                bool task = false;
                helperForms.InputBox("Alerta", "Ingrese el password de administrador para confirmar", ref resultado, true);
                var usuarioService = new UsuarioService(new UsuarioRepository());

                if (!usuarioService.VerificarPermiso(resultado))
                {
                    MessageBox.Show("La contrasena es erronea, intente nuevamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    
                    var cajaService = new CajaService(new CajaRepository());

                    int minaux = cajaService.GetCajaInicial(DateTime.Now.Date, HelperService.IDLocal).Date.Minute;
                     
                    //bool task = Local.cerrarCaja(final);
                    task =
                        cajaService.CerrarCaja(DateTime.Now.Date.AddDays(-1).AddHours(22).AddMinutes(minaux + 1),
                            dresultado, Guid.NewGuid(), HelperService.IDLocal);

                  
                    if (task)
                    {
                        MessageBox.Show("La operacion ha sido completada", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("hubo un error al completar la operacion, revise estar ingresando el Monto de forma correcta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void nuevoIngresoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarForm(new cerrarCaja(), this);
            AbrirForm(new SharedForms.Impositivo.ingreso(), this);
        }

        private void etiquetasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void stockActualXLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new todoStock(), this);

        }

        private void auxToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //AbrirForm(new reportViewer(), this);
        }

        private void nuevoPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void estadoDePedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verResumenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new resumenCC(), this);
        }

        private void verResumenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirForm(new resumenCCProveedores(), this);
        }

        private void porRemitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new preciosPorRemito(), this);
        }

        private void porProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new preciosPorProveedor(), this);
        }

        private void chequeraToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void administrarPreciosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirForm(new cambioPrecios(), this);
        }

        private void aumentoPorcentualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new cambioPrecioPorProveedor(), this);
        }

        private void administrarLocalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new Locales(), this);
        }

        private void administrarTiposDeIngresoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AbrirForm(new Administracion.abmTiposIngreso(), this);
            
            AbrirForm(new CommonFormOneField<TipoIngresoData>(new TipoIngresoService(new TipoIngresoRepository()), "Tipos de Ingreso"), this);
        }

        private void administrarTiposDeRetiroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AbrirForm(new Administracion.abmTiposRetiro(), this);

            AbrirForm(new CommonFormOneField<TipoRetiroData>(new TipoRetiroService(new TipoRetiroRepository()), "Tipos de Retiro"), this);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void administrarColoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new Colores(), this);
        }

        private void nuevoPedidoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirForm(new SharedForms.Stock.pedido(), this);
        }

        private void verPedidosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirForm(new pedidos(), this);
        }

        private void estadoDePedidosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //AbrirForm(new estadoPedido(), this);
        }

        private void imprimirEtiquetasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new printLabel(), this);
        }

        private void nuevoChequeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verChequeraToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void chequeraToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void nuevaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new Tesoreria.nuevaCuenta(), this);
        }

        private void verTodasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new Tesoreria.cuentas(), this);
        }

        private void bancosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AbrirForm(new Tesoreria.banco(), this);
            AbrirForm(new CommonFormOneField<BancoData>(new BancoService(new BancoRepository()),"Bancos"), this);

        }

        private void chequeraToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AbrirForm(new Tesoreria.chequera(), this);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new Tesoreria.nuevoCheque(), this);
        }

        private void verCarterasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verMisChequesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void nuevoMovimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new Tesoreria.movimientosCuenta(), this);
        }

        private void nuevoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirForm(new Tesoreria.Depositocuenta(), this);
        }

        private void verAvisosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verCarteraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new Tesoreria.cartera(), this);
        }

        private void verAvisosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirForm(new Tesoreria.avisosCheques(), this);
        }

        private void verMisChequesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirForm(new Tesoreria.chequesEmitidos(), this);
        }

        private void debitarChequeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new Tesoreria.pagoCheque(), this);
        }

        private void nuevaNotaDeCredioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = "Nueva Nota de Credito de Cliente";
            
            AbrirForm(new Nota2<ClienteData>(new NotaService(new NotaCreditoClienteRepository(), new NotaCreditoClienteDetalleRepository(), new TributoNotaCreditoClientesRepository()), new ClienteService(), name), this, false, FormStartPosition.WindowsDefaultLocation, name);
            //AbrirForm(new Nota<ClienteData>(new NotaService(new NotaCreditoClienteRepository(), new NotaCreditoClienteDetalleRepository()), new ClienteService(), name), this, false, FormStartPosition.WindowsDefaultLocation, name);
            //AbrirForm(new notaCreditoCliente(), this);
        }

        private void nuevaNotaDeDebitoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string name = "Nueva Nota de Debito de Proveedores";

            AbrirForm(new Nota2<ProveedorData>(new NotaService(new NotaDebitoProveedoresRepository(), new NotaDebitoProveedoresDetalleRepository(), new TributoNotaDebitoProveedoresRepository()), new ProveedorService(), name), this, false, FormStartPosition.WindowsDefaultLocation, name);
            //AbrirForm(new notaDebitoProveedores(), this);
        }

        private void nuevaNotaDeCreditoToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            string name = "Nueva Nota de Credito de Proveedores";

            AbrirForm(new Nota2<ProveedorData>(new NotaService(new NotaCreditoProveedoresRepository(), new NotaCreditoProveedoresDetalleRepository(), new TributoNotaCreditoProveedoresRepository()), new ProveedorService(), name), this, false, FormStartPosition.WindowsDefaultLocation, name);
            
        }

        private void nuevaNotaDeDebitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = "Nueva Nota de Debito de Cliente";

            AbrirForm(new Nota2<ClienteData>(new NotaService(new NotaDebitoClienteRepository(), new NotaDebitoClienteDetalleRepository(), new TributoNotaDebitoClientesRepository()), new ClienteService(), name), this, false, FormStartPosition.WindowsDefaultLocation, name);
           
        }

        private void nuevaExtraccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new Tesoreria.extraccionCuenta(), this);
        }

        private void altaDeStockToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirForm(new AltaStock(), this);
        }

        private void altaManualToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirForm(new altaManual(), this);
        }

        private void rVToolStripMenuItem_Click(object sender, EventArgs e)
        {

           // AbrirForm(new test1(), this);

        }

        private void acercaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new SharedForms.AboutBox(), this);
        }

        private void administrarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AbrirForm(new Articulos(), this);

        }

        private void cargaDesdeArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new Exception("no tenes instalado el office");
            //AbrirForm(new cargaArticulosDesdeArchivo(), this);

        }

        private void dbToolStripMenuItem_Click(object sender, EventArgs e)
        {
    
        }

        private void clieToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void proveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void verificarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ventasXPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormAdmin(new SharedForms.Estadisticas.Ventas.comisiones(), this);
        }

        private void bancos2ToolStripMenuItem_Click(object sender, EventArgs e)
        {



           
        }

        private void impresionFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new TestPrinter(), this);
        }

        private void nuevoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AbrirFormAdmin(new puntoControl(), this);
        }

        private void verTodosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirFormAdmin(new PuntosDeControl(), this);
        }

        private void testJsonRemitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var _jsonService = new JsonSerializationService();
            
            

            Guid remitoID = Guid.NewGuid();
            // Create a sample remito
            var remito = new RemitoData
            {
                ID = remitoID,
                Description = "Remito de transferencia entre locales",
                Enable = true,
                Date = DateTime.Now,
                Numero = 1234,
                Prefix = 1,
                Monto = 5000.50m,
                ClaseDocumento = ClaseDocumento.B,
                IVA = 1050.11m,
                Descuento = 100.00m,
                CantidadTotal = 25,
                FechaRecibo = DateTime.Now.AddDays(1),

                // Set origin location
                Local = new LocalData
                {
                    ID = Guid.NewGuid(),

                },

                // Set destination location
                LocalDestino = new LocalData
                {
                    ID = Guid.NewGuid(),

                },

                // Set vendor/employee
                Vendedor = new PersonalData
                {
                    ID = Guid.NewGuid(),

                },

                // Add detail items
                Children = new List<remitoDetalleData>
                {
                    new remitoDetalleData
                    {

                        FatherID =remitoID,
                        Codigo = "PROD001",
                        Cantidad = 10,

                    },
                    new remitoDetalleData
                    {
                        FatherID =remitoID,
                        Codigo = "PROD002",
                        Cantidad = 20,
                    }
                }
            };

            // Serialize to JSON
            string json = _jsonService.SerializeRemito(remito);

            HelperService.writeLog(json);

            _jsonService.JsonToFile(remito, "holu1.txt");
            // RemitoData remito2 = _jsonService.DeserializeRemito(json);

            RemitoData remito2 = _jsonService.ReadJsonFromFile("holu.txt");
        }
    }
}