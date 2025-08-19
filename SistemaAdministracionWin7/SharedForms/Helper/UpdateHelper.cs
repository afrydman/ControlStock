using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using Services;


namespace SharedForms.Helper
{


    public enum ResultadoActualizacion
    {
        Actualizacion_correcta=1,
        Actualizacion_conErrores=2,
        sin_actualizacion_disponible=3
    };


    public static class UpdateHelper
    {

        private class tables
        {
            public string p1 { get; set; }
            public int p2 { get; set; }
            public bool p3 { get; set; }

            public tables(string s1, int i1, bool b1)
            {
                p1 = s1;
                p2 = i1;
                p3 = b1;
            }
        }


        public static bool hayUnaActualizacionDB(string currentCatalog)
        {
            decimal newDb_version = 0;

            try
            {
                if (File.Exists(HelperService.db_version))
                {
                    newDb_version = HelperService.ConvertToDecimalSeguro(File.ReadAllText(HelperService.db_version));
                }
                return (Convert.ToDecimal(currentCatalog.Split('_')[1]) < newDb_version);

            }
            catch (Exception)
            {

                MessageBox.Show("Error al actualizar la DB \n Contactese con el administrador", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                HelperService.writeLog("Error al actualizar la db, no se encuentra archivo version db.", true, true);
                HelperService.writeLog("***--- no se encuentra archivo version db. ---***", true, true);
                return false;
            }

            return false;



        }

        public  static string newDB(string currentDb)
        {
            //metodo deprecado, se actualizo los metodos de conexion y este metodo no se usaba. se la come
            return "";

            //string[] newName = currentDb.Split('_');


            //string newDb = newName[0] + '_' + (Convert.ToInt32(newName[1]) + 1).ToString();
            //bool task = Conexion.ExecuteNonQuery("Create database " + newDb, null, false);

            //string text = "";

            //if (File.Exists(HelperService.db_script))
            //{
            //    text = File.ReadAllText(HelperService.db_script);
            //}
            //string pattern = "^\\s*GO\\s*$";

            ////string output = Regex.Replace(text, pattern, " ", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            //IEnumerable<string> commandStrings = Regex.Split(text, pattern, 
            //               RegexOptions.Multiline | RegexOptions.IgnoreCase);


            //SqlConnectionStringBuilder connection2 = new SqlConnectionStringBuilder();

            //connection2.DataSource = ConfigurationManager.AppSettings["DataSource"];
            //connection2.UserID = ConfigurationManager.AppSettings["User"];
            //connection2.Password = ConfigurationManager.AppSettings["Password"];
            //connection2.IntegratedSecurity = true;
            //connection2.InitialCatalog = newDb;

            //String strConn2 = connection2.ToString();

            ////create connection
            //SqlConnection conn_Hasta = new SqlConnection(strConn2);


            //bool task2 = true;



            //using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
            //    conn_Hasta.Open();
            //    foreach (string s in commandStrings)
            //    {
            //        if (!string.IsNullOrWhiteSpace(s))
            //            task2 = Conexion.ExecuteNonQuery(s, null, false, conn_Hasta);    

            //        if (!task2)
            //        {
            //            break;
            //        }

            //    }
            //     if (task2)
            //        trans.Complete();


            //}
            //conn_Hasta.Close();


            //return task && task2 ? newDb : "";


        }

        public static bool migro(string newDb, string currentDb)
        {
            


            SqlConnectionStringBuilder connection1 = new SqlConnectionStringBuilder();

            connection1.DataSource = ConfigurationManager.AppSettings["DataSource"];
            connection1.UserID = ConfigurationManager.AppSettings["User"];
            connection1.Password = ConfigurationManager.AppSettings["Password"];
            connection1.IntegratedSecurity = true;
            connection1.InitialCatalog = currentDb;

            String strConn1 = connection1.ToString();

            //create connection
            SqlConnection conn_desde = new SqlConnection(strConn1);


            SqlConnectionStringBuilder connection2 = new SqlConnectionStringBuilder();

            connection2.DataSource = ConfigurationManager.AppSettings["DataSource"];
            connection2.UserID = ConfigurationManager.AppSettings["User"];
            connection2.Password = ConfigurationManager.AppSettings["Password"];
            connection2.IntegratedSecurity = true;
            connection2.InitialCatalog = newDb;

            String strConn2 = connection2.ToString();

            //create connection
            SqlConnection conn_Hasta = new SqlConnection(strConn2);


            return true;

            //////SqlDataReader dataReaderDesde =
            //////    Conexion.ExcuteText(
            //////        "SELECT TABLE_NAME FROM information_schema.tables where TABLE_CATALOG = '" + currentDb +
            //////        "' order by TABLE_NAME desc", null, conn_desde);
            //////SqlDataReader dataReaderHacia =
            //////    Conexion.ExcuteText(
            //////        "SELECT TABLE_NAME FROM information_schema.tables where TABLE_CATALOG = '" + newDb+
            //////        "' order by TABLE_NAME desc", null, conn_Hasta);

            ////List<string> tablasDesde = new List<string>();
            ////List<string> tablasHasta = new List<string>();
            ////while (dataReaderDesde.Read())
            ////{
            ////    tablasDesde.Add(dataReaderDesde["TABLE_NAME"].ToString());
            ////}
            ////dataReaderDesde.Close();

            ////while (dataReaderHacia.Read())
            ////{
            ////    tablasHasta.Add(dataReaderHacia["TABLE_NAME"].ToString());
            ////}
            ////dataReaderHacia.Close();


            //List<tables> unificado = new List<tables>();


            //tables aux;
            //foreach (string desde in tablasDesde)
            //{
            //    if (tablasHasta.Contains(desde))
            //    {
            //        aux = new tables(desde, 2, false);
            //    }
            //    else
            //    {
            //        aux = new tables(desde, 1, false);
            //    }

            //    unificado.Add(aux);
            //}

            //foreach (string hasta in tablasHasta)
            //{
            //    bool esta = unificado.Any(c => c.p1.Contains(hasta));
            //    if (!esta)
            //    {
            //        aux = new tables(hasta, 3, false);
            //        unificado.Add(aux);
            //    }
            //}

            //bool todasLasTablas = true;
            //bool todasIguales = true;
            //foreach (tables t in unificado)
            //{

            //    if (t.p2==2)
            //    {
            //        t.p3 = (ComparoCOlumnas(t.p1, conn_desde, conn_Hasta));
            //        if (!t.p3)
            //        {
            //            todasIguales = false;
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        todasLasTablas = false;
            //        break;
            //    }
            //}

            //if (todasIguales && todasLasTablas)
            //{
            //    return Migrar(currentDb, newDb, unificado);
            //}


            //return false;

        }

        private static bool Migrar(string currentDb, string newDb, List<tables> unificado)
        {
            SqlConnectionStringBuilder connection2 = new SqlConnectionStringBuilder();

            connection2.DataSource = ConfigurationManager.AppSettings["DataSource"];
            connection2.UserID = ConfigurationManager.AppSettings["User"];
            connection2.Password = ConfigurationManager.AppSettings["Password"];
            connection2.IntegratedSecurity = true;
            connection2.InitialCatalog = newDb;

            String strConn2 = connection2.ToString();

            //create connection
            SqlConnection conn_Hasta = new SqlConnection(strConn2);

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                conn_Hasta.Open();
                foreach (tables row in unificado)
                {
                    try
                    {
                        migrarTabla(row.p1, conn_Hasta, currentDb, newDb);
                    }
                    catch (Exception ee)
                    {
                        HelperService.writeLog("******* MIGRATION ERROR **********", true, true);
                        HelperService.writeLog(ee.ToString(),true,true);
                        return false;
                    }
                }
                trans.Complete();
            }
            conn_Hasta.Close();
            return true;
        }

        private static void migrarTabla(string tabla, SqlConnection conn_Hasta, string desde, string hacia)
        {

            //try
            //{
            //    SqlDataReader a = Conexion.ExcuteText("insert into " + hacia + ".dbo." + tabla + " " +
            //                        " select * from " + desde + ".dbo." + tabla
            //                        , null, conn_Hasta);

            //    a.Close();


            //}
            //catch (Exception)
            //{

            //    throw;
            //}

        }


        public static bool ComparoCOlumnas(string tabla, SqlConnection conn_d, SqlConnection conn_h)
        {
            //SqlDataReader dataReaderColumnsDesde;
            //SqlDataReader dataReaderColumnsHasta;

            //List<Tuple<string, string, string>> valoresD = new List<Tuple<string, string, string>>();
            //List<Tuple<string, string, string>> valoresH = new List<Tuple<string, string, string>>();
            //Tuple<string, string, string> aux;
            //dataReaderColumnsDesde =
            //    Conexion.ExcuteText("SELECT COLUMN_NAME,DATA_TYPE,IS_NULLABLE" +
            //                        " FROM INFORMATION_SCHEMA.COLUMNS" +
            //                        " WHERE TABLE_NAME = '" + tabla + "'", null, conn_d);


            //while (dataReaderColumnsDesde.Read())
            //{
            //    aux = new Tuple<string, string, string>(dataReaderColumnsDesde["COLUMN_NAME"].ToString(),
            //        dataReaderColumnsDesde["DATA_TYPE"].ToString(), dataReaderColumnsDesde["IS_NULLABLE"].ToString());
            //    valoresD.Add(aux);
            //}


            //dataReaderColumnsHasta =
            //    Conexion.ExcuteText("SELECT COLUMN_NAME,DATA_TYPE,IS_NULLABLE" +
            //                        " FROM INFORMATION_SCHEMA.COLUMNS" +
            //                        " WHERE TABLE_NAME = '" + tabla + "'", null, conn_h);



            //while (dataReaderColumnsHasta.Read())
            //{
            //    aux = new Tuple<string, string, string>(dataReaderColumnsHasta["COLUMN_NAME"].ToString(),
            //        dataReaderColumnsHasta["DATA_TYPE"].ToString(), dataReaderColumnsHasta["IS_NULLABLE"].ToString());
            //    valoresH.Add(aux);

            //}
            //dataReaderColumnsDesde.Close();
            //dataReaderColumnsHasta.Close();
            //return CompareTuples(valoresD, valoresH);


            return true;

        }

        private static bool CompareTuples(List<Tuple<string, string, string>> valoresD,
            List<Tuple<string, string, string>> valoresH)
        {
            if (valoresH.Count != valoresD.Count)
            {
                return false;
            }

            foreach (Tuple<string, string, string> v in valoresH)
            {
                if (!valoresD.Any(c => c.Item1 == (v.Item1) && c.Item2 == v.Item2 && c.Item3 == v.Item3))
                    return false;
            }

            return true;
        }
    }
}
