namespace Persistence
{
    public static class bancoDataMapper
    {
        //public static List<DTO.BusinessEntities.bancoData> getAll()
        //{
        //    bancoData banco;
        //    List<bancoData> bs = new List<bancoData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
            
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.bancos_SelectAll", null);
        //    if (dataReader != null)
        //    {
        //        while (dataReader.Read())
        //        {
        //            banco = cargoBanco(dataReader);
        //            bs.Add(banco);
        //        }
        //        dataReader.Close();


        //    }

        //    return bs;
        //}

        //private static bancoData cargoBanco(SqlDataReader dataReader)
        //{
        //    bancoData aux = new bancoData();
        //    aux.ID = new Guid(dataReader["id"].ToString());
        //    aux.Description = dataReader["descripcion"].ToString();
        //    aux.Enable = Convert.ToBoolean(dataReader["enable"].ToString());

        //    return aux;
        //}

        //public static bool insert(DTO.BusinessEntities.bancoData banco)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", banco.ID));
        //    ParametersList.Add(new SqlParameter("@descripcion", banco.Description));


        //    ParametersList.Add(new SqlParameter("@enable", banco.enable));
        //    return Conexion.ExecuteNonQuery("dbo.bancos_insert", ParametersList);
        //}

        //public static bancoData getbyId(Guid id)
        //{
        //    bancoData banco = new bancoData();
          
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.bancos_getbyid", ParametersList);
        //    if (dataReader != null)
        //    {
        //        while (dataReader.Read())
        //        {
        //            banco = cargoBanco(dataReader);

        //        }
        //        dataReader.Close();


        //    }

        //    return banco;
        //}
    }
}
