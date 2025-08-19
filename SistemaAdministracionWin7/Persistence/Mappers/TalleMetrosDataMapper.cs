using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistence
{
    public static class TalleMetrosDataMapper
    {

        public static string getTalle(Guid idProducto, Guid idColor, decimal metros)
        {


            string talle ="-";
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@idProducto", idProducto));
            ParametersList.Add(new SqlParameter("@idColor", idColor));
            ParametersList.Add(new SqlParameter("@metros", metros));
            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.metrosTalle_getTalle", ParametersList);
            while (dataReader.Read())
            {

                talle =dataReader["talle"].ToString();
              

                }
            dataReader.Close();




            return talle;
        }

        public static void nuevoTalle(Guid idProducto, Guid idColor, decimal metros,string talle,int talledec)
        {

            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@idProducto", idProducto));
            ParametersList.Add(new SqlParameter("@idColor", idColor));
            ParametersList.Add(new SqlParameter("@metros", metros));
            ParametersList.Add(new SqlParameter("@talle", talle));
            ParametersList.Add(new SqlParameter("@talledec", talledec));

             Conexion.ExecuteNonQuery("dbo.metrosTalle_insert", ParametersList);


        }

        public static int  obtengoUltimo(Guid idProducto, Guid idColor)
        {
            int talle = 0;
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@idProducto", idProducto));
            ParametersList.Add(new SqlParameter("@idColor", idColor));
            
            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.metrosTalle_getLast", ParametersList);
            while (dataReader.Read())
            {
                if (dataReader["talle"] != System.DBNull.Value)
                    talle =Convert.ToInt32(dataReader["talle"].ToString());


            }
            dataReader.Close();




            return talle;
        }

        public static decimal getMetros(Guid idProducto, Guid idColor, int talle)
        {
            decimal metros = -1;
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@idProducto", idProducto));
            ParametersList.Add(new SqlParameter("@idColor", idColor));
            ParametersList.Add(new SqlParameter("@talle", talle));
            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.metrosTalle_getMetro", ParametersList);
            while (dataReader.Read())
            {
                if (dataReader["metros"] != System.DBNull.Value)
                    metros = Convert.ToDecimal(dataReader["metros"].ToString());


            }
            dataReader.Close();




            return metros;
        }

        public static Dictionary<string, decimal> obtenerTodoByProductoColor(Guid idProducto, Guid idColor)
        {
            Dictionary<string, decimal> dict = new Dictionary<string, decimal>();
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@idProducto", idProducto));
            ParametersList.Add(new SqlParameter("@idColor", idColor));

            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.obtenerTodoByProductoColor", ParametersList);
            while (dataReader.Read())
            {

                dict.Add(dataReader["talle"].ToString(), Convert.ToDecimal(dataReader["metros"].ToString()));
              

            }
            dataReader.Close();




            return dict;
        }

        public static Dictionary<decimal, string> obtenerTodoByProducto(Guid idProducto)
        {
            Dictionary<decimal, string> dict = new Dictionary<decimal, string>();
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@idProducto", idProducto));
            

            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.obtenerTodoByProducto", ParametersList);
            while (dataReader.Read())
            {
                if (!dict.ContainsKey(Convert.ToDecimal(dataReader["metros"].ToString())))
                {
                    dict.Add(Convert.ToDecimal(dataReader["metros"].ToString()), dataReader["talledec"].ToString());    
                }
            }
            dataReader.Close();

            return dict;
        }
    }
}
