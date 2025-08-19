namespace Persistence
{
    public static class cuentaDataMapper
    {
        //public static DTO.BusinessEntities.cuentaData getbyid(Guid id)
        //{//[cuentas_SelectRow]
        //    cuentaData cuenta = new cuentaData();
          
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //     ParametersList.Add(new SqlParameter("@id", id));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.cuentas_SelectRow", ParametersList);

        //    while (dataReader.Read())
        //    {
        //        cuenta = cargoCuenta(dataReader);
               
        //    }
        //    dataReader.Close();



        //    return cuenta;
        //}

        //public static List<DTO.BusinessEntities.cuentaData> getAll()
        //{
        //    //[cuentas_selectAll]
        //    cuentaData cuenta;
        //    List<cuentaData> cs = new List<cuentaData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.cuentas_selectAll", null);

        //    while (dataReader.Read())
        //    {
        //        cuenta = cargoCuenta(dataReader);
        //        cs.Add(cuenta);
        //    }
        //    dataReader.Close();




        //    return cs;
        //}

        //private static cuentaData cargoCuenta(SqlDataReader dataReader)
        //{
        //    cuentaData c = new cuentaData();
        //    c.ID = new Guid(dataReader["id"].ToString());
        //    c.banco.ID = new Guid(dataReader["idbanco"].ToString());
        //    c.cbu = dataReader["cbu"].ToString();
        //    c.cuenta = dataReader["cuenta"].ToString();
        //    c.Description = dataReader["descripcion"].ToString();
        //    c.descubierto = Convert.ToDecimal(dataReader["descubierto"].ToString());
        //    c.enable = Convert.ToBoolean(dataReader["enable"].ToString());
        //    c.esCuentaCorriente = Convert.ToBoolean(dataReader["esCuentaCorriente"].ToString());
        //    c.saldo = Convert.ToDecimal(dataReader["saldo"].ToString());
        //    c.sucursal = dataReader["sucursal"].ToString();
        //    c.tipoCuenta = (DTO.BusinessEntities.TipoCuenta)Convert.ToInt32(dataReader["tipoCuenta"].ToString());
        //    c.titular = dataReader["titular"].ToString();
        //    return c;
            
        //}

    

        //public static bool insert(DTO.BusinessEntities.cuentaData c)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", c.ID));
        //    ParametersList.Add(new SqlParameter("@idbanco", c.banco.ID));
        //    ParametersList.Add(new SqlParameter("@cbu", c.cbu));
        //    ParametersList.Add(new SqlParameter("@cuenta", c.cuenta));
        //    ParametersList.Add(new SqlParameter("@descripcion", c.Description));
        //    ParametersList.Add(new SqlParameter("@descubierto", c.descubierto));
        //    ParametersList.Add(new SqlParameter("@enable", c.enable));
        //    ParametersList.Add(new SqlParameter("@esCuentaCorriente", c.esCuentaCorriente));
        //    ParametersList.Add(new SqlParameter("@saldo", c.saldo));
        //    ParametersList.Add(new SqlParameter("@sucursal", c.sucursal));
        //    ParametersList.Add(new SqlParameter("@tipoCuenta", c.tipoCuenta));
        //    ParametersList.Add(new SqlParameter("@titular", c.titular));



        //    return Conexion.ExecuteNonQuery("dbo.cuentas_insert", ParametersList);
        //}

        //public static bool disable(Guid id)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));
            



        //    return Conexion.ExecuteNonQuery("dbo.cuentas_disable", ParametersList);
        //}

        //public static bool updateSaldo(Guid idcuenta, decimal p)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idcuenta", idcuenta));
        //    ParametersList.Add(new SqlParameter("@saldo", p));



        //    return Conexion.ExecuteNonQuery("dbo.cuentas_updateSaldo", ParametersList);
        //}
    }
}
