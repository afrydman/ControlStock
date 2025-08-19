using System;
using System.Data;
using System.Data.Common;

namespace Repository.ConnectionFactoryStuff
{
    public class ConnectionFactory
    {
        private static string _localConnectionString;
        

        public static IDbConnection CreateConnection(string connectionString)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var connection = factory.CreateConnection();

            connection.ConnectionString = connectionString;

            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new ConnectionException("Hubo un error al intentar conectar a la base de datos.", ex);
            }

            return connection;
        }


        //public static IDbConnection GetConnection(string conn)
        //{
        //    _localConnectionString = conn;
           
        //}

    }
}
