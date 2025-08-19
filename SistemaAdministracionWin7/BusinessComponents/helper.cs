using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using DTO.BusinessEntities;
using System.Configuration;
using Persistence;

namespace BusinessComponents
{
    public class helper
    {
        public static Guid IDLocal;
        public static Guid idListaPrecio;

        public static bool consultarArticulo;
        public static bool soyCentral;

        public static int intervalTime = 10;

        
        public static usuarioData usuarioActual;
        
        //static section
        public static string db_version =  Application.StartupPath+ @"\db_update\v.txt";
        public static string db_script = Application.StartupPath + @"\db_update\script.txt";

        public static string logFile = @"_log.txt";
        public static string logFileError = @"_logError.txt";

        public static Guid idEfectivo = new Guid("db4ee880-aba0-469d-a9bc-fe2b32fcabba");
        public static Guid idVale = new Guid("d1eb91cc-8d77-4457-982f-106b3a77c6b8");// tambien es el de sena de cliente en ingreso
        public static Guid idCC = new Guid("DC4E499D-F085-4510-9AC1-000000000000");
        
        public static Guid idrecibo = new Guid("DC4E499D-F085-4510-9AC1-000000000001");
        public static Guid idordenpago = new Guid("DC4E499D-F085-4510-9AC1-000000000002");

        public static Guid iddepositoCuenta = new Guid("DC4E499D-F085-4510-9AC1-000000000003");//retiro y deposito
        public static Guid idextraccioCuenta = new Guid("DC4E499D-F085-4510-9AC1-000000000004");// extraccion e ingreso
        

        public static int firstNum = 1;

        
   
        public static bool haymts = false;
        public static bool talleUnico = false;
        public static string decimalSeparator = ".";


        public static void setPass() {
            
            //usuarioActual = usuario.obtenerUsuario();
            if (string.IsNullOrEmpty(usuarioActual.hashPassword))
            {
                MessageBox.Show("Error de conexion con la base de datos", "Alerta, usuario nulo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                writeLog("Usuario sin password",true,true);
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


       




        public static List<string> GetListDB()
        {
            return Conexion.listAllDb();
        }

        public static Stack<string> getParameters()
        {
            Stack<string> param = new Stack<string>();
            try
            {
                

                helper.consultarArticulo = true;
                //helper.IDLocal = new Guid(ConfigurationManager.AppSettings["idLocal"]);
                //helper.firstNum = Convert.ToInt32(ConfigurationManager.AppSettings["firstNum"]);
                //helper.decimalSeparator = ConfigurationManager.AppSettings["decimalSeparator"];

                // param.Push(objstream.ReadLine());//server
                //param.Push(objstream.ReadLine());//db
                //param.Push(objstream.ReadLine());//user
                //param.Push(objstream.ReadLine());//pass

            }
            //catch (ConfigurationErrorsException e)
            //{
            //    MessageBox.Show("Error en el archivo de configuracion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Application.Exit();
            //}
            catch (Exception)
            {
                MessageBox.Show("No se encuentra el archivo de configuracion del Local", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }


           
            return param;

        }
        

    
        public static bool validarCodigo(string codigo) {//esta funcion es tambien conocida como, la longitudEs12?
            if (codigo.Length  !=12)
                return false;

            return true;
        }

        public static void writeLog(string log, bool printDate,bool isError=false)
        {
            

            VerificarCarpetaLogs();
            
            StreamWriter writetext = isError ? new StreamWriter(ConfigurationManager.AppSettings["CarpetaLogs"] + @"\" + DateTime.Today.ToString("yyyy-MM-dd") + logFileError, true) : new StreamWriter(ConfigurationManager.AppSettings["CarpetaLogs"] + @"\" + DateTime.Today.ToString("yyyy-MM-dd") + logFile, true);
            
            

            if (printDate)
            {
                writetext.WriteLine("****************************************");
                writetext.WriteLine(DateTime.Now);
                writetext.WriteLine("****************************************");
            }

            writetext.WriteLine("=========");

            SqlConnection c = conexion.getConnection();
            if (c != null)
            {
                writetext.WriteLine("Conexion: " + c.ConnectionString);
            }
            else
            {
                writetext.WriteLine("Conexion: Null connection");
            }

            writetext.WriteLine("=========");


            writetext.WriteLine(log);
            writetext.WriteLine("****************************************");
            writetext.Close();
        }

        private static void VerificarCarpetaLogs()
        {

            if (ConfigurationManager.AppSettings["CarpetaLogs"] == null || string.IsNullOrEmpty(ConfigurationManager.AppSettings["CarpetaLogs"]))
                throw new Exception("CarpetaLogs no esta definido");

            string carpetaLogs = ConfigurationManager.AppSettings["CarpetaLogs"].ToString();

            if (!Directory.Exists(carpetaLogs))
                Directory.CreateDirectory(carpetaLogs);

        }
        public static void writeLog(string log) {

            writeLog(log, true);


        }
        

        public static string replace_decimal_separator(string p)
        {
            if (helper.decimalSeparator==".")
            {
                return p.Replace(',', '.');
            }
            else
            {
                return p.Replace('.', ',');
            }
               
            
        }


        private const string formatoFecha = "dd/MM/yyyy";
        private const string formatoFechaHora = "HH:mm dd/MM/yyyy";

        public static string convertToFechaConFormato(DateTime fecha)
        {
            return fecha.ToString(formatoFecha);
        }

        public static string convertToFechaHoraConFormato(DateTime fecha)
        {
            return fecha.ToString(formatoFechaHora);
        }

        public static decimal ConvertToDecimalSeguro(object p)
        {
            if (p==null)
            {
                p = "0";
            }
            return ConvertToDecimalSeguro(p.ToString());
        }

        public static decimal ConvertToDecimalSeguro(string p)
        {
            return decimal.Round(Convert.ToDecimal(replace_decimal_separator(p)),2);
        }


        public static bool esCliente(GrupoCliente grupoCliente)
        {
            return usuarioActual.cliente == grupoCliente;
        }
    }
}
