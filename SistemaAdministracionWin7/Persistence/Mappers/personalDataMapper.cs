using System;
using System.Collections.Generic;
using DTO;
using System.Data.SqlClient;

namespace Persistence
{
    public class personalDataMapper
    {
        public static List<PersonalData> getPersonalbyLocal(Guid idLocal)
        {

            List<PersonalData> personas= new List<PersonalData>();
            PersonalData p;
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@ID", idLocal));
            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.personal_SelectAll", null);
            while (dataReader.Read())
            {
                p = new PersonalData();
                p = filldaObject(dataReader);
               
                personas.Add(p);
            }
            dataReader.Close();

            
            return personas;
        }

        private static PersonalData filldaObject(SqlDataReader dataReader)
        {
            PersonalData p = new PersonalData();
            p.ID = new Guid(dataReader["id"].ToString());
            p.NombreContacto = dataReader["nombre"].ToString();
            p.cuil = dataReader["cuil"].ToString();
            //p.observaciones = dataReader["descripcion"].ToString();
            //p.tel = dataReader["telefono"].ToString();
            return p;

        }

        private static PersonalData getPersonalByID(Guid id)
        {
            PersonalData p= new PersonalData();;
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@ID", id));
            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.getPersonalByID", ParametersList);
            while (dataReader.Read())
            {
                
                
                p = filldaObject(dataReader);
                
            }
            dataReader.Close();
            return p;
        }



        public static PersonalData getPersonalbyId(Guid idPersonal)
        {

            PersonalData p = new PersonalData(); 
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@ID", idPersonal));
            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.personal_SelectRow", ParametersList);
            
            while (dataReader.Read())
            {
                p = filldaObject(dataReader);
            
            }
            dataReader.Close();




            return p;
        }

        public static List<PersonalData> getAll(bool connLocal = true)
        {

            List<PersonalData> personas = new List<PersonalData>();
            PersonalData p;
            List<SqlParameter> ParametersList = new List<SqlParameter>();

            SqlDataReader dataReader = Conexion.ExcuteReader("dbo.personal_SelectAll", null, connLocal);
            while (dataReader.Read())
            {
               
                p = filldaObject(dataReader);

                personas.Add(p);
            }
            dataReader.Close();


            return personas;
        }

        public static bool update(PersonalData p, bool connLocal = true)
        {
            List<SqlParameter> ParametersList = getParameterList(p);

            return Conexion.ExecuteNonQuery("dbo.personal_Update", ParametersList, true, connLocal);
            
        }

        private static List<SqlParameter> getParameterList(PersonalData p)
        {
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@id", p.ID));
            ParametersList.Add(new SqlParameter("@nombre", p.NombreContacto));
           // ParametersList.Add(new SqlParameter("@telefono", p.tel));
            ParametersList.Add(new SqlParameter("@cuil", p.cuil));
            //ParametersList.Add(new SqlParameter("@descripcion", p.observaciones));

            return ParametersList;
        }

        public static bool insert(PersonalData p, bool connLocal = true)
        {
            List<SqlParameter> ParametersList = getParameterList(p);

            return Conexion.ExecuteNonQuery("dbo.personal_Insert", ParametersList, true, connLocal);
        }
    }
}
