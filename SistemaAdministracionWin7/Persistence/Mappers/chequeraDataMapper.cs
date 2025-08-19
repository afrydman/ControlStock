namespace Persistence
{
    public static class chequeraDataMapper
    {
        //public static List<DTO.BusinessEntities.chequeraData> getAll()
        //{
        //    chequeraData chequera;
        //    List<chequeraData> bs = new List<chequeraData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.chequeras_SelectAll", null);
        //    if (dataReader != null)
        //    {
        //        while (dataReader.Read())
        //        {
        //            chequera = cargoChequera(dataReader);
        //            bs.Add(chequera);
        //        }
        //        dataReader.Close();
        //    }



        //    return bs;
        //}

        //private static chequeraData cargoChequera(SqlDataReader dataReader)
        //{
        //    chequeraData c = new chequeraData();
        //    c.ID = new Guid(dataReader["id"].ToString());
        //    c.codigoInterno = Convert.ToInt32(dataReader["interno"].ToString());
        //    c.cuenta.ID = new Guid(dataReader["idcuenta"].ToString());
        //    c.Description = dataReader["descripcion"].ToString();
        //    c.enable = Convert.ToBoolean(dataReader["enable"].ToString());
        //    c.primero = dataReader["primero"].ToString();
        //    c.siguiente = dataReader["siguiente"].ToString();
        //    c.ultimo = dataReader["ultimo"].ToString();

        //    return c;
        //}

        //public static bool insert(DTO.BusinessEntities.chequeraData c)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", c.ID));
        //    ParametersList.Add(new SqlParameter("@codigoInterno", c.codigoInterno));
        //    ParametersList.Add(new SqlParameter("@idcuenta", c.cuenta.ID));
        //    ParametersList.Add(new SqlParameter("@descripcion", c.Description));
        //    ParametersList.Add(new SqlParameter("@enable", c.enable));
        //    ParametersList.Add(new SqlParameter("@primero", c.primero));
        //    ParametersList.Add(new SqlParameter("@siguiente", c.siguiente));
        //    ParametersList.Add(new SqlParameter("@ultimo", c.ultimo));




        //    return Conexion.ExecuteNonQuery("dbo.[chequeras_Insert]", ParametersList);
        //}

        //public static bool disable(Guid guid)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", guid));
      

        //    return Conexion.ExecuteNonQuery("dbo.[chequeras_Disable]", ParametersList);
        //}

        //public static chequeraData getById(Guid id)
        //{
        //    chequeraData chequera = new chequeraData(); ;
        //    List<chequeraData> bs = new List<chequeraData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.[chequera_SelectRow]", ParametersList);
        //    if (dataReader != null)
        //    {
        //        while (dataReader.Read())
        //        {
        //            chequera = cargoChequera(dataReader);

        //        }
        //        dataReader.Close();
        //    }



        //    return chequera;
        //}

        //public static bool SetearSiguiente(Guid idChequera, string newSiguiente)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idChequera));
        //    ParametersList.Add(new SqlParameter("@siguiente", newSiguiente));

        //    return Conexion.ExecuteNonQuery("dbo.[chequeras_setSiguiente]", ParametersList);
        //}
    }
}
