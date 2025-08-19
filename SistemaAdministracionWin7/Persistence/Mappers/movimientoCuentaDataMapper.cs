namespace Persistence
{
    public static class movimientoCuentaDataMapper
    {
        //public static bool insert(DTO.BusinessEntities.MovimientoCuentaData mov)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", mov.ID));
        //    ParametersList.Add(new SqlParameter("@anulado", mov.Enable));
        //    ParametersList.Add(new SqlParameter("@idcheque", mov.cheque.ID));
        //    ParametersList.Add(new SqlParameter("@idcuentadestino", mov.cuentaDestino.ID));
        //    ParametersList.Add(new SqlParameter("@idcuentaorigen", mov.cuentaOrigen.ID));
        //    ParametersList.Add(new SqlParameter("@fecha", mov.Date));
        //    ParametersList.Add(new SqlParameter("@Prefix", mov.Prefix));
        //    ParametersList.Add(new SqlParameter("@idlocal", mov.Local.ID));
        //    ParametersList.Add(new SqlParameter("@Monto", mov.Monto));
        //    ParametersList.Add(new SqlParameter("@Numero", mov.Numero));
        //    ParametersList.Add(new SqlParameter("@obs", mov.obs));





        //    return Conexion.ExecuteNonQuery("dbo.[movimientoscuenta_Insert]", ParametersList);
        //}


        //public static bool disable(Guid id)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));
        //    return Conexion.ExecuteNonQuery("dbo.[movimientoscuenta_disable]", ParametersList);
        
        //}

        //public static List<DTO.BusinessEntities.MovimientoCuentaData> getbyCajaDestino(Guid id)
        //{
        //    MovimientoCuentaData c;
        //    List<MovimientoCuentaData> cs = new List<MovimientoCuentaData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idcuentadestino", id));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.movimientoscuenta_getbyCajaDestino", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        c = cargoMovimiento(dataReader);
                
        //        cs.Add(c);

        //    }
        //    dataReader.Close();




        //    return cs;
        //}

        //private static MovimientoCuentaData cargoMovimiento(SqlDataReader dataReader)
        //{
        //    MovimientoCuentaData m = new MovimientoCuentaData();

        //    m.Enable= Convert.ToBoolean(dataReader["anulado"].ToString());
        //    if (dataReader["idcheque"] != System.DBNull.Value)
        //        m.cheque.ID = new Guid(dataReader["idcheque"].ToString());
            
        //    m.cuentaDestino.ID = new Guid(dataReader["idcuentadestino"].ToString());
        //    if (dataReader["idcuentaorigen"] != System.DBNull.Value)
        //        m.cuentaOrigen.ID = new Guid(dataReader["idcuentaorigen"].ToString());
        //    m.Date = Convert.ToDateTime(dataReader["fecha"].ToString());
        //    m.Prefix = Convert.ToInt32(dataReader["Prefix"].ToString());
        //    m.ID = new Guid(dataReader["id"].ToString());
        //    m.Local.ID = new Guid(dataReader["idlocal"].ToString());
        //    m.Monto = Convert.ToDecimal(dataReader["Monto"].ToString());
        //    m.Numero = Convert.ToInt32(dataReader["Numero"].ToString());
        //    m.obs = dataReader["obs"].ToString();


        //    return m;
        //}

        //public static List<MovimientoCuentaData> getbyCajaOrigen(Guid id)
        //{
        //    MovimientoCuentaData c;
        //    List<MovimientoCuentaData> cs = new List<MovimientoCuentaData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idcuentaorigen", id));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.movimientoscuenta_getbyCajaOrigen", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        c = cargoMovimiento(dataReader);

        //        cs.Add(c);

        //    }
        //    dataReader.Close();




        //    return cs;
        //}

        //public static MovimientoCuentaData getLast(int Prefix, Guid idlocal)
        //{
        //    MovimientoCuentaData c = new MovimientoCuentaData();
           
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));
        //    ParametersList.Add(new SqlParameter("@idlocal", idlocal));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.movimientoscuenta_getLast", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        c = cargoMovimiento(dataReader);

        //    }
        //    dataReader.Close();




        //    return c;
        //}

        //public static MovimientoCuentaData getbyid(Guid id)
        //{
        //    MovimientoCuentaData c = new MovimientoCuentaData();

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.movimientoscuenta_getbyid", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        c = cargoMovimiento(dataReader);

        //    }
        //    dataReader.Close();




        //    return c;
        //}

        //public static MovimientoCuentaData getbyCheque(Guid guid)
        //{
        //    MovimientoCuentaData c = new MovimientoCuentaData();

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idcheque", guid));

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.movimientoscuenta_getbycheque", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        c = cargoMovimiento(dataReader);

        //    }
        //    dataReader.Close();




        //    return c;
        //}
    }
}
