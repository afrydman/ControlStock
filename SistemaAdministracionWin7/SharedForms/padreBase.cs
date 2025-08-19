using System;
using System.Configuration;
using System.Windows.Forms;

using Repository.Repositories.UsuarioRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.UsuarioService;
using Services.VentaService;
using SharedForms.Helper;
using SharedForms.Ventas;

namespace SharedForms
{
    public partial class padreBase : Form
    {
        public padreBase()
        {
            InitializeComponent();
        }



        public ResultadoActualizacion verificoNuevaVersion(string currentCatalog, out string newCatalog)
        {
            newCatalog = "";
            
            bool migrationComplete = false;
            try
            {
                if (UpdateHelper.hayUnaActualizacionDB(currentCatalog))
                {

                    newCatalog = UpdateHelper.newDB(currentCatalog);
                    migrationComplete = UpdateHelper.migro(newCatalog, currentCatalog);
                   
                }
                else
                {
                    return ResultadoActualizacion.sin_actualizacion_disponible;
                }

            }
            catch (Exception e)
            {
                HelperService.writeLog("Error al realizar actualizacion -> ", true, true);
                HelperService.writeLog(e.ToString(), true, true);
                return ResultadoActualizacion.Actualizacion_conErrores;
            }

            if (migrationComplete)
                return ResultadoActualizacion.Actualizacion_correcta;

            return ResultadoActualizacion.Actualizacion_conErrores;

        }






        public bool verificarFormularioVentas()
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());
            if (ventaService.GetByRangoFecha(DateTime.Now.AddDays(1), DateTime.Now.AddYears(1), HelperService.IDLocal, HelperService.Prefix).Count > 0)
            {
                var usuarioService = new UsuarioService(new UsuarioRepository());
                    
                string resultado = "";
                helperForms.InputBox("Alerta2", "Se registran ventas en fechas posteriores a la fecha del sistema actual \n ingrese la contrasena para continuar.", ref resultado, true);
                if (!usuarioService.VerificarPermiso(resultado))
                {
                    MessageBox.Show("La contrasena es erronea, intente nuevamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

            }
            return true;

        }
        private void padreBase_Load(object sender, EventArgs e)
        {

        }
        public static void AbrirForm(Form frm, Form Padre, bool refresh, FormStartPosition position = FormStartPosition.WindowsDefaultLocation, string frmName="")
        {
            var ventaService = new VentaService(new VentaRepository(), new VentaDetalleRepository());

            if (frmName=="")
                frmName = frm.Text;

            bool abierto = false;
            foreach (Form hijo in Padre.MdiChildren)
            {

                if (hijo.Text == frmName)
                {
                    if (refresh)
                    {
                        hijo.Close();
                    }
                    else
                    {
                        abierto = true;
                        hijo.Focus();
                    }
                    break;

                }

            }

            if (!abierto)
            {
                if (frm is ventaBase &&
                    !HelperService.validarTrial(DateTime.Today.Date, HelperService.IDLocal, ConfigurationManager.AppSettings["UpdateNet"]))
                {
                    MessageBox.Show("Se alcanzo el nro maximo de ventas para la version trial del sistema.\nComuniquese con el adminsitrador para obtener un upgrade", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                }
                else
                {
                    frm.MdiParent = Padre;
                    frm.AutoScroll = true;
                    frm.Text = frmName;
                    //frm.MaximumSize = frm.Size;
                    frm.MinimumSize = frm.Size;
                    frm.MinimizeBox = false;
                    frm.MaximizeBox = false;

                    frm.StartPosition = position;
                    frm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    frm.Show();
                }
               
                
            }
        }

        public static void AbrirForm(Form frm, Form Padre)
        {
            AbrirForm(frm, Padre, false);


        }



        public void loadLocalInformation()
        {
            try
            {

                HelperService.consultarArticulo = true;
                HelperService.IDLocal = new Guid(ConfigurationManager.AppSettings["idLocal"]);
                HelperService.Prefix = Convert.ToInt32(ConfigurationManager.AppSettings["firstNum"]);
                HelperService.decimalSeparator = ConfigurationManager.AppSettings["decimalSeparator"];
                HelperService.idListaPrecio = new Guid(ConfigurationManager.AppSettings["idListaPrecio"]);
                
                
                
            }
            catch (ConfigurationErrorsException e1)
            {
                MessageBox.Show("Error en el archivo de configuracion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HelperService.writeLog("Error: " + e1.ToString(), true);
                Application.Exit();
            }
            catch (Exception e2)
            {
                MessageBox.Show("No se encuentra el archivo de configuracion del Local", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HelperService.writeLog("Error: " + e2.ToString(), true);
                Application.Exit();
            }

        }
       

        public static void AbrirFormAdmin(Form form, Form padre,bool sadmin=false)
        {

            var usuarioService = new UsuarioService(new UsuarioRepository());
            
            string resultado = "";
            helperForms.InputBox("Alerta", "Ingrese el password de administrador para confirmar", ref resultado, true);

            bool verificado = sadmin ? usuarioService.VerificarPermisoSA(resultado) : usuarioService.VerificarPermiso(resultado);
            
            if (!verificado)
            {
                MessageBox.Show("La contrasena es erronea, intente nuevamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                AbrirForm(form, padre);
            }

        }
        public static void CerrarForm(Form frm, Form Padre)
        {



            foreach (Form hijo in Padre.MdiChildren)
            {
                if (hijo.Text == frm.Text)
                {
                    hijo.Close();
                }

            }

        }
    }
}
