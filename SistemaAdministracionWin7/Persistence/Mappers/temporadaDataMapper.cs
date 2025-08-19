using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using System.Data.SqlClient;

namespace Persistence
{
    public static class temporadaDataMapper
    {
        public static List<DTO.BusinessEntities.TemporadaData> getAll()
        {
            List<TemporadaData> lineas = new List<TemporadaData>();
            TemporadaData v;


            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.temporada_SelectAll", null);
            while (dataReader.Read())
            {
                v = new TemporadaData();
                v.ID = new Guid(dataReader["id"].ToString());
                v.Description = dataReader["descripcion"].ToString();
                lineas.Add(v);
            }
            dataReader.Close();


            return lineas;
        }

        public static bool Insert(Guid guid, string nombre)
        {
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", guid));
            ParametersList.Add(new SqlParameter("@descripcion", nombre));

            return Conexion.ExecuteNonQuery("dbo.temporada_Insert", ParametersList);
        }

        public static bool Update(Guid guid, string nombre)
        {
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", guid));
            ParametersList.Add(new SqlParameter("@descripcion", nombre));

            return Conexion.ExecuteNonQuery("dbo.temporada_Update", ParametersList);
        }

        public static TemporadaData getbyid(Guid guid)
        {

            TemporadaData v = new TemporadaData();
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", guid));

            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.temporada_SelectRow", ParametersList);
            while (dataReader.Read())
            {
                v = new TemporadaData();
                v.ID = new Guid(dataReader["id"].ToString());
                
            }
            dataReader.Close();


            return v;
        }
    }
}
