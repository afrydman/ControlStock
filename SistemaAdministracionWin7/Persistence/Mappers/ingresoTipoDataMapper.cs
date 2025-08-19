using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using System.Data.SqlClient;

namespace Persistence
{
    public class ingresoTipoDataMapper
    {
        public static List<DTO.BusinessEntities.tipoRetiroData> getAll(bool connLocal = true)
        {
            List<tipoRetiroData> rs = new List<tipoRetiroData>();
            tipoRetiroData r;
            
            

            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.tipoingreso_getAll", null,connLocal);
            while (dataReader.Read())
            {

                r = new tipoRetiroData();

                r.Description = dataReader["descripcion"].ToString();
                r.ID = new Guid(dataReader["id"].ToString()); ;


                rs.Add(r);
            }
            dataReader.Close();

            return rs;
        }

        public static tipoRetiroData getbyID(Guid idRetiro)
        {
            
            tipoRetiroData r= new tipoRetiroData();


            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", idRetiro));
            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.tipoingreso_selectRow", ParametersList);
            while (dataReader.Read())
            {

               r.Description = dataReader["descripcion"].ToString();
               r.ID = new Guid(dataReader["id"].ToString()); ;

            }
            dataReader.Close();

            return r;
        }

        public static bool insert(tipoRetiroData p,bool connLocal)
        {
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", p.ID));
            ParametersList.Add(new SqlParameter("@descripcion", p.Description));


            return Conexion.ExecuteNonQuery("dbo.tipoingreso_insert", ParametersList,true,connLocal);
        }

        public static bool update(tipoRetiroData p, bool connLocal)
        {
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", p.ID));
            ParametersList.Add(new SqlParameter("@descripcion", p.Description));

            return Conexion.ExecuteNonQuery("dbo.tipoingreso_update", ParametersList, true, connLocal);
        }
    }
}
