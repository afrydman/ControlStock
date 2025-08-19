namespace Persistence
{
    public static class chequeDataMapper
    {
        //public static int obtenerUltimoInterno()
        //{
        //    int ultimo = -1;
            
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.cheque_getUltimo", null);
        //    if (dataReader != null)
        //    {
        //        while (dataReader.Read())
        //        {
        //            ultimo = Convert.ToInt32(dataReader["ultimo"].ToString());
        //        }
        //        dataReader.Close();

        //    }


        //    return ultimo;
        //}

        //public static List<DTO.BusinessEntities.chequeData> getByChequera(Guid idChequera)
        //{
        //    chequeData cheque;
        //    List<chequeData> cs = new List<chequeData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idchequera", idChequera));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.cheque_getByChequera", ParametersList);

        //    if (dataReader != null)
        //    {
        //        while (dataReader.Read())
        //        {
        //            cheque = cargoCheque(dataReader);
        //            cs.Add(cheque);
        //        }
        //        dataReader.Close();
        //    }



        //    return cs;
        //}

        //private static chequeData cargoCheque(SqlDataReader dataReader)
        //{
        //    chequeData c = new chequeData();
        //    c.Enable = Convert.ToBoolean(dataReader["anulado"].ToString());
        //    c.bancoEmisor.ID = new Guid(dataReader["idbanco"].ToString());
        //    c.estado = (estadoCheque)Convert.ToInt32(dataReader["estado"].ToString()); ;
        //    c.Date = Convert.ToDateTime(dataReader["fecha"].ToString());
        //    c.DateCobro = Convert.ToDateTime(dataReader["fechaCobro"].ToString());
        //    c.DateEmision = Convert.ToDateTime(dataReader["fechaEmision"].ToString());
        //    c.ID =  new Guid(dataReader["id"].ToString());
        //    c.chequera.ID = new Guid(dataReader["idchequera"].ToString());
        //    c.interno = Convert.ToInt32(dataReader["interno"].ToString());
        //    c.Local.ID = new Guid(dataReader["idlocal"].ToString());
        //    c.Monto = Convert.ToDecimal(dataReader["Monto"].ToString());
        //    c.Numero = dataReader["Numero"].ToString();
        //    c.observaciones = dataReader["observaciones"].ToString();
        //    c.titular = dataReader["titular"].ToString();
        //    c.Date_anulado_rechazado = Convert.ToDateTime(dataReader["fechaanuladoorechazado"]);
        //    return c;
        //}

        //public static bool insert(chequeData c)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@anulado", c.Enable));
        //    ParametersList.Add(new SqlParameter("@idbanco", c.bancoEmisor.ID));
        //    ParametersList.Add(new SqlParameter("@estado", c.estado));
        //    ParametersList.Add(new SqlParameter("@fecha", c.Date));
        //    ParametersList.Add(new SqlParameter("@fechaCobro", c.DateCobro));
        //    ParametersList.Add(new SqlParameter("@fechaEmision", c.DateEmision));
        //    ParametersList.Add(new SqlParameter("@id", c.ID));
        //    ParametersList.Add(new SqlParameter("@idChequera", c.chequera.ID));
        //    ParametersList.Add(new SqlParameter("@interno", c.interno));
        //    ParametersList.Add(new SqlParameter("@idlocal", c.Local.ID));
        //    ParametersList.Add(new SqlParameter("@Monto", c.Monto));
        //    ParametersList.Add(new SqlParameter("@Numero", c.Numero));
        //    ParametersList.Add(new SqlParameter("@observaciones", c.observaciones));
        //    ParametersList.Add(new SqlParameter("@titular", c.titular));
        //    ParametersList.Add(new SqlParameter("@fechaanuladoorechazado", c.Date_anulado_rechazado));
            

        //    return Conexion.ExecuteNonQuery("dbo.cheques_insert", ParametersList);
        //}

        //public static chequeData getbyId(Guid id)
        //{
        //    chequeData cheque = new chequeData();
            
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.[cheques_GetbyID]", ParametersList);

        //    if (dataReader != null)
        //    {


        //        while (dataReader.Read())
        //        {
        //            cheque = cargoCheque(dataReader);

        //        }
        //        dataReader.Close();
        //    }




        //    return cheque;
        //}

        //public static bool update(chequeData c)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@anulado", c.Enable));
        //    ParametersList.Add(new SqlParameter("@idbanco", c.bancoEmisor.ID));
        //    ParametersList.Add(new SqlParameter("@estado", c.estado));
        //    ParametersList.Add(new SqlParameter("@fecha", c.Date));
        //    ParametersList.Add(new SqlParameter("@fechaCobro", c.DateCobro));
        //    ParametersList.Add(new SqlParameter("@fechaEmision", c.DateEmision));
        //    ParametersList.Add(new SqlParameter("@id", c.ID));
        //    ParametersList.Add(new SqlParameter("@idChequera", c.chequera.ID));
        //    ParametersList.Add(new SqlParameter("@interno", c.interno));
        //    ParametersList.Add(new SqlParameter("@idlocal", c.Local.ID));
        //    ParametersList.Add(new SqlParameter("@Monto", c.Monto));
        //    ParametersList.Add(new SqlParameter("@Numero", c.Numero));
        //    ParametersList.Add(new SqlParameter("@observaciones", c.observaciones));
        //    ParametersList.Add(new SqlParameter("@titular", c.titular));
        //    ParametersList.Add(new SqlParameter("@fechaanuladoorechazado", c.Date_anulado_rechazado));


        //    return Conexion.ExecuteNonQuery("dbo.cheques_update", ParametersList);
        //}

        //public static List<chequeData> getAll()
        //{
        //    chequeData cheque;
        //    List<chequeData> cs = new List<chequeData>();
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.cheques_GetAll", null);
        //    if (dataReader != null)
        //    {
        //        while (dataReader.Read())
        //        {
        //            cheque = cargoCheque(dataReader);
        //            cs.Add(cheque);
        //        }
        //        dataReader.Close();

        //    }


        //    return cs;
        //}
    }
}
