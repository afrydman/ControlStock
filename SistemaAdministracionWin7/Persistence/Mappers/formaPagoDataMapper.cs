using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using System.Data.SqlClient;

namespace Persistence
{
    public class formaPagoDataMapper
    {
        public static List<FormaPagoData> getAll(bool connLocal = true)
        {

            List<FormaPagoData> fps = new List<FormaPagoData>();
            FormaPagoData fp;

            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.formaspago_SelectAll", null, connLocal);
            while (dataReader.Read())
            {

                fp = new FormaPagoData();
                
                fp.Description = dataReader["descripcion"].ToString();
                fp.ID = new Guid(dataReader["id"].ToString());
                fp.Enable = Convert.ToBoolean(dataReader["anulado"].ToString());
                if (dataReader["credito"].ToString()!="")
                {
                    fp.credito= Convert.ToBoolean(dataReader["credito"].ToString());    
                }
                



                fps.Add(fp);
            }
            dataReader.Close();
            return fps;
        }

        public static FormaPagoData getById(Guid id)
        {
            
            FormaPagoData fp = new FormaPagoData();
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", id));
            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.formaspago_SelectRow", ParametersList);
            while (dataReader.Read())
            {

                fp = new FormaPagoData();
                
                fp.Description = dataReader["descripcion"].ToString();
                fp.ID = new Guid(dataReader["id"].ToString());
                fp.Enable = Convert.ToBoolean(dataReader["anulado"].ToString());
                fp.credito = Convert.ToBoolean(dataReader["credito"].ToString());

                
            }
            dataReader.Close();
            return fp;
        }

        public static bool anular(Guid idfp)
        {
            List<SqlParameter> ParametersList = new List<SqlParameter>();

            ParametersList.Add(new SqlParameter("@id", idfp));

            return Conexion.ExecuteNonQuery("dbo.formasPago_anular", ParametersList);
        }

        public static bool insert(FormaPagoData f,bool connLocal = true)
        {
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", f.ID));
            ParametersList.Add(new SqlParameter("@descripcion", f.Description));
            ParametersList.Add(new SqlParameter("@credito", f.credito));



            return Conexion.ExecuteNonQuery("dbo.formaspago_Insert", ParametersList,true,connLocal);
        }

        public static bool update(FormaPagoData f, bool connLocal = true)
        {
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", f.ID));
            ParametersList.Add(new SqlParameter("@descripcion", f.Description));
            ParametersList.Add(new SqlParameter("@anulado", f.Enable));
            ParametersList.Add(new SqlParameter("@credito", f.credito));
            return Conexion.ExecuteNonQuery("dbo.formaspago_Update", ParametersList,true,connLocal);
        }
    }
}
