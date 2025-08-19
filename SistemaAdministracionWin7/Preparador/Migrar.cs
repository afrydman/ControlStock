using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Persistence;

namespace Preparador
{
    public partial class Migrar : Form
    {
        public Migrar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            tablaDif.Rows.Clear();





            SqlConnectionStringBuilder connection1 = new SqlConnectionStringBuilder();

            connection1.DataSource = ConfigurationManager.AppSettings["DataSource"];
            connection1.UserID = ConfigurationManager.AppSettings["User"];
            connection1.Password = ConfigurationManager.AppSettings["Password"];
            connection1.IntegratedSecurity = true;
            connection1.InitialCatalog = cmbDesde.Text;

            String strConn1 = connection1.ToString();

            //create connection
            SqlConnection conn_desde = new SqlConnection(strConn1);


            SqlConnectionStringBuilder connection2 = new SqlConnectionStringBuilder();

            connection2.DataSource = ConfigurationManager.AppSettings["DataSource"];
            connection2.UserID = ConfigurationManager.AppSettings["User"];
            connection2.Password = ConfigurationManager.AppSettings["Password"];
            connection2.IntegratedSecurity = true;
            connection2.InitialCatalog = cmbHacia.Text;

            String strConn2 = connection2.ToString();

            //create connection
            SqlConnection conn_Hasta = new SqlConnection(strConn2);









            SqlDataReader dataReaderDesde =
                Conexion.ExcuteText(
                    "SELECT TABLE_NAME FROM information_schema.tables where TABLE_CATALOG = '" + cmbDesde.Text +
                    "' order by TABLE_NAME desc", null, conn_desde);
            SqlDataReader dataReaderHacia =
                Conexion.ExcuteText(
                    "SELECT TABLE_NAME FROM information_schema.tables where TABLE_CATALOG = '" + cmbHacia.Text +
                    "' order by TABLE_NAME desc", null, conn_Hasta);

            List<string> tablasDesde = new List<string>();
            List<string> tablasHasta = new List<string>();
            while (dataReaderDesde.Read())
            {
                tablasDesde.Add(dataReaderDesde["TABLE_NAME"].ToString());
            }
            dataReaderDesde.Close();

            while (dataReaderHacia.Read())
            {
                tablasHasta.Add(dataReaderHacia["TABLE_NAME"].ToString());
            }
            dataReaderHacia.Close();


            List<Tuple<string, int>> unificado = new List<Tuple<string, int>>();


            Tuple<string, int> aux;
            foreach (string desde in tablasDesde)
            {
                if (tablasHasta.Contains(desde))
                {
                    aux = new Tuple<string, int>(desde, 2);
                }
                else
                {
                    aux = new Tuple<string, int>(desde, 1);
                }

                unificado.Add(aux);
            }

            foreach (string hasta in tablasHasta)
            {
                bool esta = unificado.Any(c => c.Item1.Contains(hasta));
                if (!esta)
                {
                    aux = new Tuple<string, int>(hasta, 3);
                    unificado.Add(aux);
                }
            }


            foreach (Tuple<string, int> t in unificado)
            {
                tablaDif.Rows.Add();
                int fila;
                fila = tablaDif.RowCount - 1;
                switch (t.Item2)
                {
                    case 1:
                        tablaDif[0, fila].Value = t.Item1;
                        tablaDif[1, fila].Value = "No esta";
                        tablaDif[2, fila].Value = " - ";
                        
                        tablaDif[3, fila].ReadOnly = true;

                        break;
                    case 2:
                        tablaDif[0, fila].Value = t.Item1;
                        tablaDif[1, fila].Value = t.Item1;
                        if (ComparoCOlumnas(t.Item1, conn_desde, conn_Hasta))
                        {
                            tablaDif[2, fila].Value = "Iguales";
                            tablaDif[3, fila].ReadOnly = false;
                            tablaDif[3, fila].Value = true;
                        }
                        else
                        {
                            tablaDif[2, fila].Value = "Distintos";
                            tablaDif[3, fila].ReadOnly = true;
                        }



                        break;
                    case 3:
                        tablaDif[1, fila].Value = t.Item1;
                        tablaDif[0, fila].Value = "No esta";
                        tablaDif[2, fila].Value = " - ";
                        tablaDif[3, fila].ReadOnly = true;
                        break;
                }
            }

            //todo verificar que realmente este comparando de verdad
            // permitir migrar aquellos que son igales
            //los que no son iguales que genere automatico el sql?

        }

        private bool ComparoCOlumnas(string tabla, SqlConnection conn_d, SqlConnection conn_h)
        {
            SqlDataReader dataReaderColumnsDesde;
            SqlDataReader dataReaderColumnsHasta;

            List<Tuple<string, string, string>> valoresD = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> valoresH = new List<Tuple<string, string, string>>();
            Tuple<string, string, string> aux;
            dataReaderColumnsDesde =
                Conexion.ExcuteText("SELECT COLUMN_NAME,DATA_TYPE,IS_NULLABLE" +
                                    " FROM INFORMATION_SCHEMA.COLUMNS" +
                                    " WHERE TABLE_NAME = '" + tabla + "'", null, conn_d);


            while (dataReaderColumnsDesde.Read())
            {
                aux = new Tuple<string, string, string>(dataReaderColumnsDesde["COLUMN_NAME"].ToString(),
                    dataReaderColumnsDesde["DATA_TYPE"].ToString(), dataReaderColumnsDesde["IS_NULLABLE"].ToString());
                valoresD.Add(aux);
            }


            dataReaderColumnsHasta =
                Conexion.ExcuteText("SELECT COLUMN_NAME,DATA_TYPE,IS_NULLABLE" +
                                    " FROM INFORMATION_SCHEMA.COLUMNS" +
                                    " WHERE TABLE_NAME = '" + tabla + "'", null, conn_h);



            while (dataReaderColumnsHasta.Read())
            {
                aux = new Tuple<string, string, string>(dataReaderColumnsHasta["COLUMN_NAME"].ToString(),
                    dataReaderColumnsHasta["DATA_TYPE"].ToString(), dataReaderColumnsHasta["IS_NULLABLE"].ToString());
                valoresH.Add(aux);

            }
            dataReaderColumnsDesde.Close();
            dataReaderColumnsHasta.Close();
            return CompareTuples(valoresD, valoresH);


        }

        private bool CompareTuples(List<Tuple<string, string, string>> valoresD,
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

        private void Migrar_Load(object sender, EventArgs e)
        {

            cmbDesde.DataSource = Conexion.listAllDb();
            cmbHacia.DataSource = Conexion.listAllDb();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tabla = "";
            if (tablaDif.SelectedCells.Count > 0)
            {
                if (tablaDif.Rows[tablaDif.SelectedCells[0].RowIndex].Cells[2].ToString() != " - ")
                {
                    tabla = tablaDif.Rows[tablaDif.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                }

                SqlConnectionStringBuilder connection1 = new SqlConnectionStringBuilder();

                connection1.DataSource = ConfigurationManager.AppSettings["DataSource"];
                connection1.UserID = ConfigurationManager.AppSettings["User"];
                connection1.Password = ConfigurationManager.AppSettings["Password"];
                connection1.IntegratedSecurity = true;
                connection1.InitialCatalog = cmbDesde.Text;

                String strConn1 = connection1.ToString();

                //create connection
                SqlConnection conn_desde = new SqlConnection(strConn1);


                SqlConnectionStringBuilder connection2 = new SqlConnectionStringBuilder();

                connection2.DataSource = ConfigurationManager.AppSettings["DataSource"];
                connection2.UserID = ConfigurationManager.AppSettings["User"];
                connection2.Password = ConfigurationManager.AppSettings["Password"];
                connection2.IntegratedSecurity = true;
                connection2.InitialCatalog = cmbHacia.Text;

                String strConn2 = connection2.ToString();

                //create connection
                SqlConnection conn_Hasta = new SqlConnection(strConn2);

                cargoTabla(columnaDesde, tabla, conn_desde);
                cargoTabla(columnaHasta, tabla, conn_Hasta);



            }



        }

        private void cargoTabla(DataGridView tabla, string nombretabla, SqlConnection conn)
        {

            SqlDataReader dataReaderColumnsDesde;


            List<Tuple<string, string, string>> valoresD = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> valoresH = new List<Tuple<string, string, string>>();
            Tuple<string, string, string> aux;
            dataReaderColumnsDesde =
                Conexion.ExcuteText("SELECT COLUMN_NAME,DATA_TYPE,IS_NULLABLE" +
                                    " FROM INFORMATION_SCHEMA.COLUMNS" +
                                    " WHERE TABLE_NAME = '" + nombretabla + "'", null, conn);


            while (dataReaderColumnsDesde.Read())
            {
                aux = new Tuple<string, string, string>(dataReaderColumnsDesde["COLUMN_NAME"].ToString(),
                    dataReaderColumnsDesde["DATA_TYPE"].ToString(), dataReaderColumnsDesde["IS_NULLABLE"].ToString());
                valoresD.Add(aux);
            }



            dataReaderColumnsDesde.Close();


            foreach (Tuple<string, string, string> valor in valoresD)
            {
                tabla.Rows.Add();
                int fila;
                fila = tabla.RowCount - 1;
                tabla[0, fila].Value = valor.Item1;
                tabla[1, fila].Value = valor.Item2;
                tabla[2, fila].Value = valor.Item3;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           

            SqlConnectionStringBuilder connection2 = new SqlConnectionStringBuilder();

            connection2.DataSource = ConfigurationManager.AppSettings["DataSource"];
            connection2.UserID = ConfigurationManager.AppSettings["User"];
            connection2.Password = ConfigurationManager.AppSettings["Password"];
            connection2.IntegratedSecurity = true;
            connection2.InitialCatalog = cmbHacia.Text;

            String strConn2 = connection2.ToString();

            //create connection
            SqlConnection conn_Hasta = new SqlConnection(strConn2);
            foreach (DataGridViewRow row in tablaDif.Rows)
            {
                try
                {

                    if (!row.Cells[3].ReadOnly && Convert.ToBoolean(row.Cells[3].Value))
                    {
                        migrarTabla(row.Cells[0].Value.ToString(),  conn_Hasta);
                        row.Cells[4].Value = "ok";
                    }
                }
                catch (Exception ee)
                {
                    txtError.Text = ee.ToString();
                    row.Cells[4].Value = "error";
                    throw;
                }
            }
        }

        private void migrarTabla(string tabla,  SqlConnection conn_Hasta)
        {

            try
            {
                SqlDataReader a= Conexion.ExcuteText("insert into "+ cmbHacia.Text+".dbo."+tabla+" "+
                                    " select * from "+cmbDesde.Text+".dbo."+tabla
                                    , null, conn_Hasta);

                a.Close();


            }
            catch (Exception)
            {
                    
                throw;
            }
            
        }
    }
}
