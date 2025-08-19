using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO.BusinessEntities;

namespace Persistence
{
    public class lineaDataMapper
    {
        public static LineaData getLineaByID(Guid idLinea)
        {
            LineaData v = new LineaData();
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", idLinea));

            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.linea_SelectRow", ParametersList);
            while (dataReader.Read())
            {
                v.ID = new Guid(dataReader["id"].ToString());
                v.Description = dataReader["descripcion"].ToString();

            }
            dataReader.Close();


            return v;
        }

        public static List<DTO.BusinessEntities.LineaData> getall()
        {
            List<LineaData> lineas = new List<LineaData>();
            LineaData v ;
            
            
            
            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.linea_SelectAll", null);
            while (dataReader.Read())
            {
                v = new LineaData();
                v.ID = new Guid(dataReader["id"].ToString());
                v.Description = dataReader["descripcion"].ToString();
                lineas.Add(v);
            }
            dataReader.Close();


            return lineas;
        }

        public static bool insert(Guid guid, string nombre)
        {
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", guid));
            ParametersList.Add(new SqlParameter("@descripcion", nombre));

            return Conexion.ExecuteNonQuery("dbo.linea_Insert", ParametersList);
        }

        public static bool update(Guid guid, string nombre)
        {
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", guid));
            ParametersList.Add(new SqlParameter("@descripcion", nombre));

            return Conexion.ExecuteNonQuery("dbo.linea_Update", ParametersList);
        }
    }
}
