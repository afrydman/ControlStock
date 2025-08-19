using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Persistence.LogService;


namespace Persistence
{
    public class Conexion : Loggeable
    {

        public Conexion()
            : base("Conexion")
        {
        }
        public const string logFile = @"\log_DB.txt";
        public const string sqllogFile = @"\sqlLog.txt";


        public static string catalog = "";
        public static string server = "";


        public static SqlConnection c;
        public static SqlConnection cRemote;

        private static int maxTry = 5;
       

        public static List<string> listAllDb()
        {
            List<String> databases = new List<String>();

            SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder();

            connection.DataSource = ConfigurationManager.AppSettings["DataSource"];
            connection.UserID = ConfigurationManager.AppSettings["User"];
            connection.Password = ConfigurationManager.AppSettings["Password"];
            connection.IntegratedSecurity = true;

            String strConn = connection.ToString();

            //create connection
            SqlConnection sqlConn = new SqlConnection(strConn);

            //open connection
            sqlConn.Open();

            //get databases
            DataTable tblDatabases = sqlConn.GetSchema("Databases");

            //close connection
            sqlConn.Close();

            //add to list
            foreach (DataRow row in tblDatabases.Rows)
            {
                String strDatabaseName = row["database_name"].ToString();

                databases.Add(strDatabaseName);


            }

            return databases;
        }
        public static SqlConnection GetConexion(bool local= true)
        {
            SqlConnection auxc = local ? c : cRemote;


            if (auxc == null || auxc.State == ConnectionState.Closed)
            {
                SqlConnectionStringBuilder conStrbuilder = new SqlConnectionStringBuilder();
                try
                {

                    conStrbuilder = generateconnstring(local);
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("Error en el archivo de configuracion de la base de datos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }

                int aux = 10;
                try
                {
                    auxc = new SqlConnection(conStrbuilder.ConnectionString);
                    auxc.Open();
                }
                catch (TimeoutException ex2)
                {

                    aux--;
                }
                catch (SqlException ex)
                {
                    if (ex.Message.ToLower().StartsWith("time"))
                    {
                        aux--;
                    }
                    else
                    {
                        aux = 0;
                        MessageBox.Show("Error en la Conexion a la base de datos \n reinicie la computadora para continuar sin problemas", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                         writeLog(ex.ToString(), true, Application.StartupPath + logFile);

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error en la Conexion a la base de datos \n reinicie la computadora para continuar sin problemas", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    writeLog(e.ToString(), true, Application.StartupPath + logFile);
                }
            }
            if (local)
            {
                c = auxc;
            }
            else
            {
                cRemote = auxc;
            }
            return auxc;

        }

        private static SqlConnectionStringBuilder generateconnstring(bool local)
        {

            SqlConnectionStringBuilder conStrbuilder = new SqlConnectionStringBuilder();
            //todo ver si puede acceder al configuration manager!
            if (local)
            {
                conStrbuilder.DataSource = ConfigurationManager.AppSettings["localDataSource"].ToString();
                conStrbuilder.InitialCatalog = ConfigurationManager.AppSettings["localCatalog"].ToString();
                conStrbuilder.UserID = ConfigurationManager.AppSettings["User"];
                conStrbuilder.Password = ConfigurationManager.AppSettings["Password"];
            }
            else
            {
                conStrbuilder.DataSource = ConfigurationManager.AppSettings["remoteDataSource"].ToString();
                conStrbuilder.InitialCatalog = ConfigurationManager.AppSettings["remoteCatalog"].ToString();
                //conStrbuilder.UserID = ConfigurationManager.AppSettings["RemoteUser"];//"masquesa";
                //conStrbuilder.Password = ConfigurationManager.AppSettings["RemotePassword"]; //"Atumamalegusta2";
                conStrbuilder.UserID = ConfigurationManager.AppSettings["User"];
                conStrbuilder.Password = ConfigurationManager.AppSettings["Password"];
            }
            
           
            conStrbuilder.TrustServerCertificate = true;
            conStrbuilder.MultipleActiveResultSets = true;
            return conStrbuilder;
        }


        public static void CloseConection()
        {

            if (c != null)
            {
                c.Dispose();
                c.Close();
            }
        }


        public static SqlDataReader ExcuteText(string storedProcedure, List<SqlParameter> ParametersList,bool uselocal=true)
        {

            SqlConnection conn;

            conn = uselocal ? c : cRemote;



            if (conn == null)
            {
                conn = GetConexion(uselocal);
            }
            else
            {
                
                if (conn.State == ConnectionState.Closed)
                {
                    
                   conn= GetConexion(uselocal);
                }

            }


            if (conn != null)
            {

                SqlCommand command = new SqlCommand(storedProcedure, conn);
                if (ParametersList != null)
                {
                    foreach (SqlParameter param in ParametersList)
                    {
                        command.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
                    }
                }

                command.CommandType = CommandType.Text;

                try
                {
                    SqlDataReader a = command.ExecuteReader();
                    return a;
                }
                catch (Exception e)
                {

                    writeLog(e.ToString(), true, Application.StartupPath + logFile);

                    generateSqlFile(command, e.ToString());

                }

            }
            return null;
        }


        public static SqlDataReader ExcuteText(string storedProcedure, List<SqlParameter> ParametersList, SqlConnection conn = null)
        {

          

            if (conn == null)
            {
                conn = GetConexion();
            }
            else
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

            }


            if (conn != null)
            {

                SqlCommand command = new SqlCommand(storedProcedure, conn);
                if (ParametersList != null)
                {
                    foreach (SqlParameter param in ParametersList)
                    {
                        command.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
                    }
                }

                command.CommandType = CommandType.Text;

                try
                {
                    SqlDataReader a = command.ExecuteReader();
                    return a;
                }
                catch (Exception e)
                {

                    writeLog(e.ToString(), true, Application.StartupPath + logFile);

                    generateSqlFile(command, e.ToString());

                }

            }
            return null;
        }

        private static void generateSqlFile(SqlCommand cmd, string error)
        {
            string query = cmd.CommandText;

            foreach (SqlParameter p in cmd.Parameters)
            {
                query += "\n " + p.ParameterName + ":" + p.Value.ToString();
            }

            query += "\n\n\n\n\n";
            query += error;
            query += "\n ";


            writeLog(query, true, Application.StartupPath + sqllogFile);




        }

        public static DataTable GetDataTable(string Sp, List<SqlParameter> ParametersList)
        {

            DataTable dt = new DataTable();



            SqlConnection conn = GetConexion();

            if (conn != null)
            {

                SqlCommand command = new SqlCommand(Sp, conn);
                if (ParametersList != null)
                {
                    foreach (SqlParameter param in ParametersList)
                    {
                        command.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
                    }
                }

                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }

            return dt;
        }


        public static SqlDataReader ExcuteReader(string storedProcedure, List<SqlParameter> ParametersList, bool uselocal = true)
        {
            SqlConnection conn;

            conn = uselocal ? c : cRemote;
            int tryCount = 0;

             while (tryCount<maxTry){
                if (conn == null)
                {
                    conn = GetConexion(uselocal);
                }
                else
                {

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                }

                SqlCommand command = new SqlCommand(storedProcedure, conn);
                if (ParametersList != null)
                {
                    foreach (SqlParameter param in ParametersList)
                    {
                        command.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
                    }
                }

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    SqlDataReader a = command.ExecuteReader();
                    return a;
                }

                catch (Exception e)
                {

                    writeLog(e.ToString(), true, Application.StartupPath + logFile);
                    generateSqlFile(command, e.ToString());
                    tryCount++;
                }

            }
           
            return null;
        }


        public static bool ExecuteNonQuery(string storedProcedure, List<SqlParameter> ParametersList, bool isstoredprocedure= true, bool uselocal = true)
        {
            SqlConnection conn;

            conn = uselocal ? c : cRemote;



            if (conn == null)
            {
                conn = GetConexion(uselocal);
            }
            else
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

            }

            SqlCommand command = new SqlCommand(storedProcedure, conn);
            if (ParametersList != null)
            {
                foreach (SqlParameter param in ParametersList)
                {
                    command.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
                }
            }


            command.CommandType = isstoredprocedure ? CommandType.StoredProcedure : CommandType.Text;


            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {

                writeLog(e.ToString(), true, Application.StartupPath + logFile);
                generateSqlFile(command, e.ToString());
            }
            return false;


        }

        public static void writeLog(string log, bool printDate, string file)
        {

            Loggeable.create("conexion");
            //StreamWriter writetext = new StreamWriter(file, true);


            //if (printDate)
            //{
            //    writetext.WriteLine("****************************************");
            //    writetext.WriteLine(DateTime.Now);
            //    writetext.WriteLine("****************************************");
            //}

            //Log("Conexion: " + c.ConnectionString);
            //writetext.WriteLine("=========");

            //if (c != null)
            //{
            //    writetext.WriteLine("Conexion: " + c.ConnectionString);
            //}
            //else
            //{
            //    writetext.WriteLine("Conexion: Null connection");
            //}

            //writetext.WriteLine("=========");


            //writetext.WriteLine(log);
            //writetext.WriteLine("****************************************");
            //writetext.Close();
            
           
            if (c != null)
            {
                Log("Conexion: " + c.ConnectionString);
            }
            else
            {
                Log("Conexion: Null connection");
                
            }
            Log(log);


        }
        public static void writeLog(string log)
        {

            writeLog(log, false, Application.StartupPath + logFile);


        }




    }
}
