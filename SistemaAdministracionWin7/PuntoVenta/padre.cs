using System;
using System.Windows.Forms;

using DTO.BusinessEntities;

using System.Configuration;
using Repository.Repositories.CajaRepository;
using Repository.Repositories.UsuarioRepository;
using Services;
using Services.CajaService;
using Services.UsuarioService;
using SharedForms;
using SharedForms.Estadisticas.Stock;
using SharedForms.Estadisticas.Stock.PuntoControl;
using SharedForms.Helper;
using SharedForms.Impositivo;
using SharedForms.Stock;
using SharedForms.Ventas;
using ingreso = SharedForms.Impositivo.ingreso;
using retiro = SharedForms.Impositivo.retiro;

namespace PuntoVenta
{
    public partial class padre : padreBase
    {

        public padre()
        {
            InitializeComponent();
        }
        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Esta seguro que desea salir?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dg == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void veranticuosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void controlstocksarasaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void nuevaVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (verificarFormularioVentas())
            {
                AbrirForm(new ventaMenor(), this);

                CerrarForm(new cerrarCaja(), this);
                
            }

          
        }

        private void padre_Load(object sender, EventArgs e)
        {

            if (ConfigurationManager.AppSettings["updateDB"]!=null && Convert.ToBoolean(ConfigurationManager.AppSettings["updateDB"].ToString()))
            {
                string newCatalog = "";

                ResultadoActualizacion r = verificoNuevaVersion(PuntoVenta.Properties.Settings.Default.currentCatalog, out newCatalog);

                if (r == ResultadoActualizacion.Actualizacion_correcta && !string.IsNullOrWhiteSpace(newCatalog))
                {

                    UpdateConfig(newCatalog);

                    MessageBox.Show("Actualizacion realizada correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    HelperService.writeLog("Actualizacion realizada correctamente \n db version: " + newCatalog, true, true);
                }
                else if (r == ResultadoActualizacion.Actualizacion_conErrores)
                {
                    MessageBox.Show("Error al actualizar la DB \n Contactese con el administrador", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    HelperService.writeLog("Error al actualizar la db", true, true);
                    Application.Exit();
                }
            }

            loadLocalInformation();
            
            HelperService.setPass();
            HelperService.writeLog("Iniciando Sistema", true);

            this.WindowState = FormWindowState.Maximized;
            setMenuesbyClient();
        }

        private void UpdateConfig(string newCatalog)
        {
            try
            {


                //Properties.Settings.Default.currentCatalog = newCatalog;
                Properties.Settings.Default.Save(); //

            }
            catch (Exception e)
            {
                HelperService.writeLog(e.ToString(), true, true);
                MessageBox.Show("Error al actualizar la Configuracion \n Contactese con el administrador", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                Application.Exit();
            }
        }
        




        private void setMenuesbyClient()
        {
            if (HelperService.esCliente(GrupoCliente.Chinela))
            {
                //estadisticasToolStripMenuItem.Visible = false;
                //retirosToolStripMenuItem.Visible = false;
            }
        }


       






        private void verVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new Ventas(), this);
        }



        private void altaDeStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new AltaStock(), this);
        }

        private void altaManualDeStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new altaManual(), this);
        }

        private void nuevoRetiroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarForm(new cerrarCaja(), this);
            AbrirForm(new retiro(), this);
        }

        private void bajaDeStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new BajaStock(), this);
        }

        private void cerrarCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarForm(new retiro(), this);
            CerrarForm(new ingreso(), this);
            CerrarForm(new ventaMayor(), this);
            CerrarForm(new Ventas(), this);
            CerrarForm(new mostrarVentaMenor(), this);

            AbrirForm(new cerrarCaja(), this);
        }

        private void verTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verStockToolStripMenuItem_Click(object sender, EventArgs e)
        {


            AbrirFormAdmin(new Busqueda(), this);

        }

        private void modificarCajaInicialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            


        }

        private void verRemitosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new verEnviosStock(), this);
        }

        private void verStockDetalladoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormAdmin(new detallestockLocal(), this);



        }



        private void retirosXFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormAdmin(new SharedForms.Estadisticas.RetirosIngresos.retiros_ingresosxfecha<RetiroData>(), this);

        }

        private void cajaXFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HelperService.esCliente(GrupoCliente.Chinela))
            {
                AbrirFormAdmin(new Estadisticas.cajaxfecha(), this);
            }
            else
            {
                AbrirFormAdmin(new Estadisticas.cajaxfecha(), this);
            }


        }

        private void nuevoIngresoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new ingreso(), this);
        }

        private void verValesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new vales(), this);
        }

        private void articulosPreciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormAdmin(new Estadisticas.articulosPrecios(), this);
        }

        private void valesCanjeadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormAdmin(new Estadisticas.valesCanjear(), this);
        }

        private void ventasXFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormAdmin(new SharedForms.Estadisticas.Ventas.ventasxFecha(), this);
        }

        private void verFaltasXFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new salidaArticulosFecha(), this);
        }

        private void acercaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirForm(new SharedForms.AboutBox(), this);
        }

        private void comisionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormAdmin(new SharedForms.Estadisticas.Ventas.comisiones(), this);
        }

        private void ingresosXFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormAdmin(new SharedForms.Estadisticas.RetirosIngresos.retiros_ingresosxfecha<IngresoData>(), this);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormAdmin(new puntoControl(), this);
        }

        private void verTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormAdmin(new PuntosDeControl(), this);
        }

        private void masVendidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormAdmin(new SharedForms.Estadisticas.Ventas.StockMasVendido(), this);
        }

        private void cajaDelDiaDeHoyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarForm(new cerrarCaja(), this);

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
                        MessageBox.Show("Ocurrio un error", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("hubo un error al completar la operacion, revise estar ingresando el Monto de forma correcta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void modificacionEnCascadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarForm(new retiro(), this);
            CerrarForm(new ingreso(), this);
            CerrarForm(new ventaMayor(), this);
            CerrarForm(new Ventas(), this);
            CerrarForm(new mostrarVentaMenor(), this);

            AbrirFormAdmin(new ModificarCajaCascada(), this);
        }
    }
}