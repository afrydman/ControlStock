using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using System.Data.SqlClient;

namespace Persistence
{
    public static class usuarioDataMapper
    {

        public static DTO.BusinessEntities.usuarioData GetUsuarioByUserName(string username)
        {
            usuarioData u = new usuarioData();
            List<SqlParameter> ParametersList = new List<SqlParameter>();
            ParametersList.Add(new SqlParameter("@username", username));
            SqlDataReader dataReader;

            dataReader = Conexion.ExcuteReader("dbo.usuario_getbyuserName", ParametersList);

            if (dataReader != null)
            {


                while (dataReader.Read())
                {

                    u = cargoUsuario(dataReader);


                }
            }
            if (dataReader != null)
                dataReader.Close();
            return u;
        }

        private static usuarioData cargoUsuario(SqlDataReader dataReader)
        {
         
            usuarioData v = new usuarioData();
            v.Usuario = dataReader["Usuario"].ToString();
            v.hashPassword= dataReader["Password"].ToString();
            v.Cliente = (GrupoCliente)Convert.ToInt32(dataReader["Cliente"].ToString());
            return v;
        }
    }
}
