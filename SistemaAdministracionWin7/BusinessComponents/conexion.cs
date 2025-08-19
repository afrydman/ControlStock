using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace BusinessComponents
{
    public class conexion
    {

        public static string catalog = "";
        public static string server = "";
        public static string remote_catalog = "";
        public static string remote_server = "";



        public static void setValues(string _cat = "", string _serv = "", string _rem_cat = "", string _rem_serv = "")
        {
            catalog = _cat;
            server = _serv;
            remote_catalog = _rem_cat;
            remote_server = _rem_serv;
        }

        public static SqlConnection getRemoteConection()
        {
            return getConnection(false);
        }
        public static SqlConnection getLocalConection()
        {
            //
            return getConnection(true);
        }
       

        public static void closeConecction(bool local=true)
        {
            SqlConnection c = Persistence.Conexion.GetConexion(local);

            while 
            (c.State == ConnectionState.Executing || c.State == ConnectionState.Fetching || c.State == ConnectionState.Connecting)
            {
                Thread.Sleep(5);
            }
            c.Close();

        }

        public static SqlConnection getConnection(bool local= true)
        {
            SqlConnection c = Persistence.Conexion.GetConexion(local);

            if (c != null)
            {
                return c;
            }
            else
            {
                return null;
            }
        
        }

    }
}
