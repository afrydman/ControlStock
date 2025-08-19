namespace Persistence
{
    public static class valeDataMapper
    {

        //public static DTO.BusinessEntities.valeData getByID(Guid id)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));
        //    valeData v = new valeData();
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.vales_GetbyID", ParametersList);


        //    while (dataReader != null && dataReader.Read())
        //    {
        //        v = cargoVale(dataReader);
        //    }
            
        //    dataReader.Close();
        //    return v;

        //}

        //public static bool insert(DTO.BusinessEntities.valeData r, bool connLocal)
        //{

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", r.ID));
        //    ParametersList.Add(new SqlParameter("@Numero", r.Numero));
        //    ParametersList.Add(new SqlParameter("@fecha", r.Date));
        //    ParametersList.Add(new SqlParameter("@esCambio", r.esCambio));
        //    ParametersList.Add(new SqlParameter("@idAsoc", r.IDAsoc));
        //    ParametersList.Add(new SqlParameter("@Monto", r.Monto));
        //    ParametersList.Add(new SqlParameter("@anulado", r.Enable));
        //    ParametersList.Add(new SqlParameter("@idlocal", r.IDLocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", r.Prefix));

        //    return Conexion.ExecuteNonQuery("dbo.vales_insert", ParametersList, true, connLocal);

        //}

        //public static List<DTO.BusinessEntities.valeData> getValeByFecha(Guid idLocal, SqlDateTime ayer, SqlDateTime manana, int Prefix,bool connLocal)
        //{//
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@fechaAyer", ayer));
        //    ParametersList.Add(new SqlParameter("@fechaMan", manana));
        //    ParametersList.Add(new SqlParameter("@idlocal", idLocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));
        //    valeData v;
        //    List<valeData> vales = new List<valeData>();
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.vales_GetbyFecha", ParametersList, connLocal);


        //    while (dataReader != null && dataReader.Read())
        //    {
        //        v = new valeData();
        //        v = cargoVale(dataReader);

        //        vales.Add(v);

        //    }
            
        //    dataReader.Close();
        //    return vales;
        //}

        //public static int getLast(Guid idLocal,int Prefix,bool connLocal)
        //{

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.vale_getLast", ParametersList, connLocal);

        //    int num = 1;
        //    while (dataReader!=null && dataReader.Read())
        //    {
        //        if (dataReader["Numero"] != System.DBNull.Value)
        //        {

        //            num = Convert.ToInt32(dataReader["Numero"].ToString());
        //        }
        //    }
        //    if (dataReader!=null)
        //    {
        //        dataReader.Close();    
        //    }
            
        //    return num;



        //}

        //private static valeData cargoVale(SqlDataReader dataReader)
        //{
        //    valeData aux = new valeData();
        //    aux.Enable  = Convert.ToBoolean(dataReader["anulado"].ToString());
        //    aux.esCambio = Convert.ToBoolean(dataReader["esCambio"].ToString());
        //    aux.Date = Convert.ToDateTime(dataReader["fecha"].ToString());
        //    aux.ID = new Guid(dataReader["id"].ToString());
        //    aux.IDAsoc =  new Guid(dataReader["idAsoc"].ToString());
        //    aux.IDLocal = new Guid(dataReader["idLocal"].ToString());
        //    aux.Monto = Convert.ToDecimal(dataReader["Monto"].ToString());
        //    aux.Numero = Convert.ToInt32(dataReader["Numero"].ToString());
        //    aux.Prefix = Convert.ToInt32(dataReader["Prefix"].ToString());
        //    return aux;
        //}

        //public static valeData getbyVenta(Guid guid)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idventa", guid));
        //    valeData v = new valeData();
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.vales_Getbyventa", ParametersList);


        //    while (dataReader != null && dataReader.Read())
        //    {
        //        v = cargoVale(dataReader);
        //    }

        //    dataReader.Close();
        //    return v;

        //}

        //public static bool anular(Guid guid,bool connLocal)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", guid));
        //    return Conexion.ExecuteNonQuery("dbo.vales_anular", ParametersList, true, connLocal);

        //}

        //public static List<valeData> getBigger(int ultima, Guid idlocal, int Prefix, bool connLocal)
        //{
        //    valeData v;
        //    List<valeData> ventas = new List<valeData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

        //    ParametersList.Add(new SqlParameter("@ultimo", ultima));
        //    ParametersList.Add(new SqlParameter("@id", idlocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));

        //    SqlDataReader dataReader;


        //    dataReader = Conexion.ExcuteReader("dbo.vales_getbiggerthan", ParametersList, connLocal);


        //    while (dataReader.Read())
        //    {
        //        v = cargoVale(dataReader);

        //        ventas.Add(v);
        //    }
        //    dataReader.Close();

        //    return ventas;
        //}
    }
}
