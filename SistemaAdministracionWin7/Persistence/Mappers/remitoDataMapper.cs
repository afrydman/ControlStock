namespace Persistence
{
    public class remitoDataMapper
    {

        //public static List<remitoData> getByLocalOrigen(Guid idLocal)
        //{

        //    List<remitoData> rs = new List<remitoData>();
        //    remitoData r;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.remitos_getbyLocalOrigen", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        r = new remitoData();

        //        r = cargarRemito(dataReader);

        //        rs.Add(r);
        //    }
        //    dataReader.Close();

        //    return rs;
        //}

        //public static List<remitoData> getByLocal(Guid idLocal,bool connLocal = true)
        //{
        //    List<remitoData> rs = new List<remitoData>();
        //    remitoData r;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.remitos_getByLocal", ParametersList,connLocal);
        //    while (dataReader.Read())
        //    {

        //        r = cargarRemito(dataReader);


        //        rs.Add(r);
        //    }
        //    dataReader.Close();

        //    return rs;
        //}

        //private static remitoData cargarRemito(SqlDataReader dataReader)
        //{
        //    remitoData r = new remitoData();
        //    r.DateGeneracion = Convert.ToDateTime(dataReader["fechaGeneracion"].ToString());
        //    r.DateRecibo = DateTime.MinValue;
        //    if (dataReader["fechaRecibo"] != System.DBNull.Value)
        //    {
        //        r.DateRecibo = Convert.ToDateTime(dataReader["fechaRecibo"].ToString());
        //    }
        //    r.localDestino.ID = new Guid(dataReader["idLocalD"].ToString());
        //    r.localOrigen.ID = new Guid(dataReader["idLocalO"].ToString());
        //    r.numeroRemito = Convert.ToInt32(dataReader["numeroRemito"].ToString());
        //    r.vendedor.ID = new Guid(dataReader["idPersonal"].ToString());
        //    r.ID = new Guid(dataReader["id"].ToString());
        //    r.Enable = Convert.ToBoolean(dataReader["anulado"].ToString());
        //    r.cantidadTotal = Convert.ToDecimal(dataReader["cantidadTotal"].ToString());
        //    r.Prefix = Convert.ToInt32(dataReader["Prefix"].ToString());
        //    r.observaciones = dataReader["obs"].ToString();
        //    return r;
        //}

        //public static bool insertar(remitoData r,bool connLocal = true)
        //{
            
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", r.ID));
        //    ParametersList.Add(new SqlParameter("@FechaGeneracion", r.DateGeneracion));
        //    ParametersList.Add(new SqlParameter("@FechaRecibo", r.DateRecibo));
        //    ParametersList.Add(new SqlParameter("@idlocalDestino", r.localDestino.ID));
        //    ParametersList.Add(new SqlParameter("@idlocalOrigen", r.localOrigen.ID));
        //    ParametersList.Add(new SqlParameter("@numeroRemito", r.numeroRemito));
        //    ParametersList.Add(new SqlParameter("@idvendedor", r.vendedor.ID));
        //    ParametersList.Add(new SqlParameter("@anulado", r.Enable));
        //    ParametersList.Add(new SqlParameter("@cantidadTotal", r.cantidadTotal));
        //    ParametersList.Add(new SqlParameter("@Prefix", r.Prefix));
        //    ParametersList.Add(new SqlParameter("@obs", r.observaciones));



        //    return Conexion.ExecuteNonQuery("dbo.remito_Insert", ParametersList, true, connLocal);
        //}
        //public static bool confirmarRecibo(Guid id)
        //{

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));

        //    return Conexion.ExecuteNonQuery("dbo.remito_confirmarRecibo", ParametersList);
        //}
        //public static bool confirmarRecibo(Guid id, DateTime fecha, bool connLocal = true)
        //{

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));
        //    ParametersList.Add(new SqlParameter("@fecha", fecha));

        //    return Conexion.ExecuteNonQuery("dbo.remito_confirmarRecibo_fecha", ParametersList, true, connLocal);
        //}

        //public static remitoData getLast(Guid idLocal,int Prefix, bool connLocal = true)
        //{//busca por idlocalO
            
        //    remitoData r = new remitoData();

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.remitos_getlast", ParametersList, connLocal);
        //    while (dataReader.Read())
        //    {

        //        r = cargarRemito(dataReader);
        //    }
        //    dataReader.Close();

        //    return r;
        //}

        //public static List<remitoData> getOlderThan(DateTime fecha,Guid idlocal,int Prefix,bool connLocal = true)
        //{

        //    List<remitoData> rs = new List<remitoData>();
        //    remitoData r;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@fecha", fecha));
        //    ParametersList.Add(new SqlParameter("@idlocal", idlocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.remitos_getOlderThan", ParametersList, connLocal);
        //    while (dataReader.Read())
        //    {

        //        r = new remitoData();

        //        r = cargarRemito(dataReader);

        //        rs.Add(r);
        //    }
        //    dataReader.Close();

        //    return rs;
        //}

       

        //public static remitoData getbyId(Guid guid)
        //{
        //    remitoData r = new remitoData();

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", guid));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.remitos_getByID", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        r = cargarRemito(dataReader);
        //    }
        //    dataReader.Close();

        //    return r;
        //}

        //public static bool anular(Guid guid)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", guid));

        //    return Conexion.ExecuteNonQuery("dbo.remitos_anular", ParametersList);
        //}

        //public static List<remitoData> getAnulados(Guid guid)
        //{
        //    List<remitoData> rs = new List<remitoData>();
        //    remitoData r;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

        //    ParametersList.Add(new SqlParameter("@id", guid));

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.remitos_getAnulados", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        r = new remitoData();

        //        r = cargarRemito(dataReader);


        //        rs.Add(r);
        //    }
        //    dataReader.Close();

        //    return rs;
        //}



        //public static remitoData getLastLocalRecibido(Guid idLocal, int Prefix, bool connLocal = true)
        //{//busca por idlocalD y recibo 
        //    remitoData r = new remitoData();

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.remitos_getLastLocalRecibido", ParametersList,connLocal);
        //    while (dataReader.Read())
        //    {

        //        r = cargarRemito(dataReader);
        //    }
        //    dataReader.Close();

        //    return r;
        //}
    }
}
