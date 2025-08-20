using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using System.Data.SqlClient;

namespace Persistence
{
    public class retiroTipoDataMapper
    {
        public static List<DTO.BusinessEntities.TipoRetiroData> getAll(bool connLocal = true)
        {
            List<TipoRetiroData> rs = new List<TipoRetiroData>();
            TipoRetiroData r;
            
            

            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.tiporetiro_getAll", null,connLocal);
            while (dataReader.Read())
            {

                r = new TipoRetiroData();

                r.Description = dataReader["descripcion"].ToString();
                r.ID = new Guid(dataReader["id"].ToString()); ;


                rs.Add(r);
            }
            dataReader.Close();

            return rs;
        }

        public static TipoRetiroData getbyID(Guid idRetiro)
        {
            
            TipoRetiroData r= new TipoRetiroData();


            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", idRetiro));
            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.tiporetiro_selectRow", ParametersList);
            while (dataReader.Read())
            {

               r.Description = dataReader["descripcion"].ToString();
               r.ID = new Guid(dataReader["id"].ToString()); ;

            }
            dataReader.Close();

            return r;
        }

        public static bool insert(TipoRetiroData p, bool connLocal )
        {
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", p.ID));
            ParametersList.Add(new SqlParameter("@descripcion", p.Description));


            return Conexion.ExecuteNonQuery("dbo.tiporetiro_insert", ParametersList,true,connLocal);
        }

        public static bool update(TipoRetiroData p, bool connLocal )
        {
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", p.ID));
            ParametersList.Add(new SqlParameter("@descripcion", p.Description));

            return Conexion.ExecuteNonQuery("dbo.tiporetiro_update", ParametersList,true,connLocal);
        }
    }
}
