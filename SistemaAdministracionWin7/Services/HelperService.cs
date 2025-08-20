using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

using DTO.BusinessEntities;
using Services.LocalService;
using Helper.LogService;

namespace Services
{
    public static class HelperService
    {

        public static Guid IDLocal;
        public static Guid idListaPrecio;

        public static bool consultarArticulo = true;
        public static bool soyCentral;

        public static int intervalTime = 10;


        public static usuarioData usuarioActual;

        public static MiRazonData razonActual;

        public static string Cuit;

        private const string formatoFecha = "dd/MM/yyyy";
        private const string formatoFechaHora = "HH:mm dd/MM/yyyy";



        //static section
        public static string db_version = Application.StartupPath + @"\db_update\v.txt";
        //public static string db_script = Application.StartupPath + @"\db_update\script.txt";

        public static string logFile = @"_log.txt";
        public static string logFileError = @"_logError.txt";

        public static Guid idEfectivo = new Guid("db4ee880-aba0-469d-a9bc-fe2b32fcabba");
        public static Guid idVale = new Guid("d1eb91cc-8d77-4457-982f-106b3a77c6b8");// tambien es el de sena de cliente en ingreso
        public static Guid idCC = new Guid("DC4E499D-F085-4510-9AC1-000000000000");

        public static Guid idrecibo = new Guid("DC4E499D-F085-4510-9AC1-000000000001");
        public static Guid idordenpago = new Guid("DC4E499D-F085-4510-9AC1-000000000002");

        public static Guid iddepositoCuenta = new Guid("DC4E499D-F085-4510-9AC1-000000000003");//retiro y deposito
        public static Guid idextraccioCuenta = new Guid("DC4E499D-F085-4510-9AC1-000000000004");// extraccion e ingreso


        //Condiciones IVA
        public static Guid idResponsableInscripto = new Guid("AAAAAAAA-0000-0000-0000-000000000000");
        public static Guid idSujetoExento = new Guid("AAAAAAAA-0000-0000-0000-000000000001");
        public static Guid idConsumidorFinal = new Guid("AAAAAAAA-0000-0000-0000-000000000002");
        public static Guid idMonotributo = new Guid("AAAAAAAA-0000-0000-0000-000000000003");



        //Condiciones Venta
        public static Guid idCondicionVentaContado = new Guid("AAAAAAAA-0000-0001-0000-000000000000");
        public static Guid idCondicionVentaCuentaCorriente = new Guid("AAAAAAAA-0000-0001-0000-000000000001");
        public static Guid idCondicionVentaCheque = new Guid("AAAAAAAA-0000-0001-0000-000000000002");
        public static Guid idCondicionVentaOtra = new Guid("AAAAAAAA-0000-0001-0000-000000000003");



        public static int Prefix = 1;


        public static int? TallesPorProductoDefault = 50;
        public static bool haymts = false;
        public static bool talleUnico = false;
        public static string decimalSeparator = ".";




        public class MessageBoxHelper
        {
            private static DialogResult baseMessage(string text, string title, MessageBoxButtons button, MessageBoxIcon icon)
            {
                return MessageBox.Show(text, title, button, icon);
            }

            public static DialogResult UpdateOk(string aux)
            {
                return baseMessage("Actualizado correctamente " + aux, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            public static DialogResult UpdateError(string aux)
            {
                return baseMessage("Hubo un error al actualizar " + aux, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            public static DialogResult InsertOk(string aux)
            {
                return baseMessage(aux + " insertado correctamente ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            public static DialogResult InsertError(string aux)
            {
                return baseMessage("Hubo un error al procesar e insertar " + aux, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            public static DialogResult DisableOk(string aux)
            {
                return baseMessage(aux + " deshabilitado correctamente ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            public static DialogResult DisableError(string aux)
            {
                return baseMessage("Hubo un error al procesar y deshabilitar " + aux, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            public static DialogResult EnableeOk(string aux)
            {
                return baseMessage(aux + " habilitado correctamente ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            public static DialogResult EnableError(string aux)
            {
                return baseMessage("Hubo un error al procesar y habilitar " + aux, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



            //
            public static DialogResult confirmOperation()
            {
                return baseMessage("Esta seguro que desea confirmar la operacion?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }



            public static DialogResult VentaUpdateOk()
            {
                return UpdateOk("Venta");
            }
            public static DialogResult VentaInsertOk()
            {
                return InsertOk("Venta");
            }
            public static DialogResult VentaUpdateError()
            {
                return UpdateError("Venta");
            }
            public static DialogResult VentaInsertError()
            {
                return InsertError("Venta");
            }

            public static DialogResult PedidoInsertOk()
            {
                return InsertOk("Pedido");
            }

            public static DialogResult PedidoInsertError()
            {
                return InsertError("Pedido");
            }
            public static DialogResult PedidoUpdateOk()
            {
                return UpdateOk("Pedido");
            }

            public static DialogResult PedidoUpdateError()
            {
                return UpdateError("Pedido");
            }


        }



        public static void setPass()
        {
            var serv = new UsuarioService.UsuarioService();
            usuarioActual = serv.obtenerUsuario();
            if (String.IsNullOrEmpty(usuarioActual.hashPassword))
            {
                MessageBox.Show("Error de conexion con la base de datos", "Alerta, usuario nulo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                writeLog("Usuario sin password", true, true);
                Application.Exit();
            }
            if (esCliente(GrupoCliente.Slipak))
            {
                haymts = true;
            }
            if (esCliente(GrupoCliente.Balarino))
            {
                talleUnico = true;
            }




        }




        public static bool validarTrial(DateTime fecha, Guid idlocal, string global, int maxventas = 9)
        {

            CultureInfo provider = CultureInfo.InvariantCulture;
            return  true;
            string format = "MMddyyyy";
            DateTime result = DateTime.ParseExact(global, format, provider);
            bool ok = true;

            if (DateTime.Now > result)
            {


                List<VentaData> ventas = new VentaService.VentaService().GetByFecha(fecha, idlocal, HelperService.Prefix);

                if (ventas != null && ventas.Count > maxventas)
                    ok = false;
            }
            return ok;
        }



        public static List<string> GetListDB()
        {
            return null;// Conexion.listAllDb();
        }

        public static Stack<string> getParameters()
        {
            Stack<string> param = new Stack<string>();
            try
            {


                consultarArticulo = true;
                IDLocal = new Guid(ConfigurationManager.AppSettings["idLocal"]);
                Prefix = Convert.ToInt32(ConfigurationManager.AppSettings["firstNum"]);
                decimalSeparator = ConfigurationManager.AppSettings["decimalSeparator"];



            }
            catch (ConfigurationErrorsException e)
            {
                MessageBox.Show("Error en el archivo de configuracion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            catch (Exception)
            {
                MessageBox.Show("No se encuentra el archivo de configuracion del Local", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }



            return param;

        }



        public static bool validarCodigo(string codigo)
        {//esta funcion es tambien conocida como, la longitudEs12?
            if (codigo.Length != 12)
                return false;

            return true;
        }

        public static void WriteException(Exception exception)
        {
            // Try to find existing logger (Central or PuntoVenta) first, then fallback to app
            string loggerId = "app";
            if (Log.Exists("Central"))
            {
                loggerId = "Central";
            }
            else if (Log.Exists("PuntoVenta"))
            {
                loggerId = "PuntoVenta";
            }
            else if (!Log.Exists("app"))
            {
                LoggerConfig.InitializeLogger("app", "app.log", LogLevel.Info);
            }
            
            // Use the new logger's exception handling
            Log.WriteError(loggerId, "Exception occurred", exception);
        }

        public static void writeLog(string log, bool printDate, bool isError = false)
        {
            // Try to find existing logger (Central or PuntoVenta) first, then fallback to app
            string loggerId = "app";
            if (Log.Exists("Central"))
            {
                loggerId = "Central";
            }
            else if (Log.Exists("PuntoVenta"))
            {
                loggerId = "PuntoVenta";
            }
            else if (!Log.Exists("app"))
            {
                LoggerConfig.InitializeLogger("app", "app.log", LogLevel.Info);
            }

            // Add connection info to log message if needed
            string fullMessage = log;
            if (printDate)
            {
                string conn = ConfigurationManager.ConnectionStrings["Local"]?.ConnectionString ?? "Null connection";
                fullMessage = $"{log}\n[Connection: {conn}]";
            }

            // Log to appropriate logger with correct level
            if (isError)
            {
                Log.WriteError(loggerId, fullMessage);
            }
            else
            {
                Log.WriteInfo(loggerId, fullMessage);
            }
        }

        // This method is no longer needed as LoggerConfig handles folder creation
        private static void VerificarCarpetaLogs()
        {
            // Handled by LoggerConfig.LogFolder property
        }
        public static void writeLog(string log)
        {
            writeLog(log, true);
        }


        public static string replace_decimal_separator(string p)
        {
            if (decimalSeparator == ".")
            {
                return p.Replace(',', '.');
            }
            else
            {
                return p.Replace('.', ',');
            }
        }



        public static string convertToFechaConFormato(DateTime fecha)
        {
            return fecha.ToString(formatoFecha);
        }

        public static string convertToFechaHoraConFormato(DateTime fecha)
        {
            return fecha.ToString(formatoFechaHora);
        }

        public static decimal ConvertToDecimalSeguro(object p, int decimals = 2)
        {

            if (p == null)
            {
                return 0;
            }
            try
            {
                decimal n;
                if (!Decimal.TryParse(p.ToString(), out n))
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
            return ConvertToDecimalSeguro(p.ToString(), decimals);
        }

        public static decimal ConvertToDecimalSeguro(string p, int decimals = 2)
        {
            if (string.IsNullOrWhiteSpace(p))
                return 0;
            return Decimal.Round(Convert.ToDecimal(replace_decimal_separator(p)), decimals);
        }



        public static void VerificoTextBoxNumerico(TextBox textbox, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '.' && (textbox.Text.IndexOf(".") > -1 || textbox.Text.IndexOf(",") > -1)) || (e.KeyChar == ',' && (textbox.Text.IndexOf(",") > -1 || textbox.Text.IndexOf(".") > -1)) || (char.IsLetter(e.KeyChar)))
            {
                e.Handled = true;
            }
        }



        public static bool esCliente(GrupoCliente grupoCliente)
        {
            return usuarioActual.Cliente == grupoCliente;
        }

        public static bool IsProtectedPayment(Guid idF)
        {
            return idF == idCC || idF == idVale || idF == idEfectivo;
        }

        public static void LoadConfigurations()
        {

            try
            {
                setPass();

                Cuit = ConfigurationManager.AppSettings["CUIT"];
                var serv = new MiRazonService();
                razonActual = serv.GetByID();
                if (razonActual == null) throw new Exception("Return null");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al Cargar InfoLocal", "LoadConfigurations", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                throw;
            }
        }
    }
}
