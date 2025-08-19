using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO.BusinessEntities;

namespace Persistence
{
    public static class formaPagoCuotasDataMapper
    {
        public static List<decimal> getAumentos(Guid idFp, bool connLocal = true)
        {
            List<decimal> aumentos = new List<decimal>();
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", idFp));


            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.formasPagoCuotas_getAumentos", ParametersList,
                connLocal);


            while (dataReader.Read())
            {
                aumentos.Add(Convert.ToDecimal(dataReader["aumento"].ToString()));
            }
            dataReader.Close();
            return aumentos;
        }




        //voy a morir
        public static List<Guid> getIds(Guid idFp, bool connLocal = true)
        {
            List<Guid> aumentos = new List<Guid>();
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", idFp));


            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.formasPagoCuotas_getAumentos", ParametersList,
                connLocal);


            while (dataReader.Read())
            {
                aumentos.Add(new Guid(dataReader["id"].ToString()));
            }
            dataReader.Close();
            return aumentos;
        }

        public static bool insertAumento(DTO.BusinessEntities.FormaPagoData f, bool connLocal = true)
        {

            Guid id;
            bool tsk;
            for (int i = 0; i < 12; i++)
            {

                List<SqlParameter> ParametersList = new List<SqlParameter>();
                //ParametersList.Add(new SqlParameter("@id", f.IDsCuotas[i]));
                ParametersList.Add(new SqlParameter("@idFp", f.ID));
                ParametersList.Add(new SqlParameter("@cuota", (i + 1).ToString()));
                //ParametersList.Add(new SqlParameter("@aumento", f.aumento2[i]));

                tsk = Conexion.ExecuteNonQuery("dbo.formaspagoCuotas_Insert", ParametersList, true, connLocal);
                if (!tsk)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool updateAumento(DTO.BusinessEntities.FormaPagoData f, bool connLocal = true)
        {
            bool tsk;
            //if (f.aumento2!=null&& f.aumento2.Count>0)//osea , no sos efectivo
            //{


            //    for (int i = 0; i < 12; i++)
            //    {


            //        List<SqlParameter> ParametersList = new List<SqlParameter>();
            //        ParametersList.Add(new SqlParameter("@id", f.IDsCuotas[i]));

            //        ParametersList.Add(new SqlParameter("@aumento", f.aumento2[i]));

            //        tsk= Conexion.ExecuteNonQuery("dbo.formaspagoCuotas_update", ParametersList,true,connLocal);
            //        if (!tsk)
            //        {
            //            return false;
            //        }
            //    }
            return true;
        }

    
    

    public static List<FormaPagoCuotaData> getCuotas(Guid guid)
{
 	throw new NotImplementedException();
}
        public static FormaPagoCuotaData getbyformaycuotas(Guid idf, int cuotas)
        {
            FormaPagoCuotaData formapagocuotas = new FormaPagoCuotaData(); 
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", idf));
            ParametersList.Add(new SqlParameter("@cuotas", cuotas));


            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.formaspagocuotas_SelectRow", ParametersList);


            while (dataReader.Read())
            {
                //formapagocuotas.ID = new Guid(dataReader["id"].ToString());
                //formapagocuotas.aumento = Convert.ToDecimal(dataReader["aumento"].ToString());
                //formapagocuotas.FormaPago.ID = idf;
                //formapagocuotas.cuotas = cuotas;
            }
            dataReader.Close();
            return formapagocuotas;
        
        
        }
        public static Guid getIDbyformaycuotas(Guid idf, int cuotas)
        {
            Guid idformapagocuotas = new Guid();
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", idf));
            ParametersList.Add(new SqlParameter("@cuotas", cuotas));


            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.formaspagocuotas_SelectRow", ParametersList);


            while (dataReader.Read())
            {
                idformapagocuotas = new Guid(dataReader["id"].ToString()); 
            }
            dataReader.Close();
            return idformapagocuotas;
        }

        public static FormaPagoData getbyid(Guid id)
        {
            FormaPagoData formap = new FormaPagoData();
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", id));
            
            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.formaspagocuotas_SelectbyID", ParametersList);


            while (dataReader.Read())
            {
                formap.ID = id;
                formap.Description = dataReader["descripcion"].ToString() + " " +dataReader["cuotas"].ToString()+ " CUOTAS";
                //formap.aumento = Convert.ToDecimal(dataReader["aumento"].ToString());
                
            }
            dataReader.Close();
            return formap;
        }
 
    }
}
