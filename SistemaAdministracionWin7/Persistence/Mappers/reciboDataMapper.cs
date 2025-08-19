namespace Persistence
{
    public static class reciboDataMapper
    {

        //public static bool insert(DTO.BusinessEntities.reciboData r)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@anulado", r.Enable));
        //    ParametersList.Add(new SqlParameter("@idcliente", r.tercero.ID));
        //    ParametersList.Add(new SqlParameter("@fecha", r.Date));
        //    ParametersList.Add(new SqlParameter("@Prefix", r.Prefix));
        //    ParametersList.Add(new SqlParameter("@id", r.ID));
        //    ParametersList.Add(new SqlParameter("@total", r.Monto));
        //    ParametersList.Add(new SqlParameter("@Numero", r.Numero));
        //    ParametersList.Add(new SqlParameter("@idlocal", r.Local.ID));
            


        //    return Conexion.ExecuteNonQuery("dbo.[recibo_Insert]", ParametersList);
        //}

        //public static bool insertDetalle(DTO.BusinessEntities.reciboDetalleData det)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idcheque", det.cheque.ID));
        //    ParametersList.Add(new SqlParameter("@idrecibo", det.IDRecibo));
        //    ParametersList.Add(new SqlParameter("@Monto", det.Monto));
        //    ParametersList.Add(new SqlParameter("@idcaja", det.caja.ID));
            

        //    return Conexion.ExecuteNonQuery("dbo.[reciboDetalle_Insert]", ParametersList);
        //}

        //public static reciboData getByID(Guid id) {


        //    reciboData r = new reciboData();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.recibo_SelectRow", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        r = cargoRecibo(dataReader);
                
        //    }
        //    dataReader.Close();
        //    return r;
        
        //}


        //public static List<DTO.BusinessEntities.reciboData> getAll()
        //{
        //    List<reciboData> rs = new List<reciboData>();
        //    reciboData r;
            
        

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.recibo_SelectAll", null);
        //    while (dataReader.Read())
        //    {

        //        r = cargoRecibo(dataReader);
        //        rs.Add(r);
        //    }
        //    dataReader.Close();
        //    return rs;
        //}

        //private static reciboData cargoRecibo(SqlDataReader dataReader)
        //{
        //    reciboData r = new reciboData();
        //    r.Enable = Convert.ToBoolean(dataReader["anulado"].ToString());
        //    r.tercero.ID=new Guid(dataReader["idCliente"].ToString());
        //    r.Date=Convert.ToDateTime(dataReader["fecha"].ToString());
        //    r.Prefix=Convert.ToInt32(dataReader["Prefix"].ToString());
        //    r.ID=new Guid(dataReader["id"].ToString());
        //    r.Monto = Convert.ToDecimal(dataReader["total"].ToString());
        //    r.Numero=Convert.ToInt32(dataReader["Numero"].ToString());
        //    r.Local.ID = new Guid(dataReader["idLocal"].ToString());

        //    return r;
            
        //}
        //public static reciboDetalleData cargoDetall(SqlDataReader dataReader)
        //{
        //    reciboDetalleData r = new reciboDetalleData();
        //    r.cheque.ID= new Guid(dataReader["idcheque"].ToString());
        //    r.IDRecibo = new Guid(dataReader["idrecibo"].ToString());
        //    r.Monto = Convert.ToDecimal(dataReader["Monto"].ToString());
        //    if (dataReader["idcaja"] != System.DBNull.Value)
        //    {

        //        r.caja.ID = new Guid(dataReader["idcaja"].ToString());
        //    }
        //    return r;
        
        //}



        //public static List<reciboDetalleData> getDetalles(Guid guid)
        //{
        //    List<reciboDetalleData> rs = new List<reciboDetalleData>();
        //    reciboDetalleData r;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idrecibo", guid));


        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.reciboDetalle_SelectRow", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        r = cargoDetall(dataReader);
        //        rs.Add(r);
        //    }
        //    dataReader.Close();
        //    return rs;
        //}

        //public static bool anular(Guid guid)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", guid));
            


        //    return Conexion.ExecuteNonQuery("dbo.[recibo_anular]", ParametersList);
        //}

        //public static List<reciboDetalleData> getAllDetalles()
        //{
        //    List<reciboDetalleData> rs = new List<reciboDetalleData>();
        //    reciboDetalleData r;
            


        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.reciboDetalle_SelectAll", null);
        //    while (dataReader.Read())
        //    {

        //        r = cargoDetall(dataReader);
        //        rs.Add(r);
        //    }
        //    dataReader.Close();
        //    return rs;
        //}

        //public static reciboData getLast(Guid idlocal, int Prefix)
        //{

        //    reciboData r = new reciboData();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idlocal", idlocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.recibo_SelectLast", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        r = cargoRecibo(dataReader);

        //    }
        //    dataReader.Close();
        //    return r;
        //}

        //public static List<reciboData> getbyFecha(DateTime desde, DateTime hasta, Guid idlocal)
        //{
        //    List<reciboData> rs = new List<reciboData>();
        //    reciboData r;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@desde", desde));
        //    ParametersList.Add(new SqlParameter("@hasta", hasta));
        //    ParametersList.Add(new SqlParameter("@idlocal", idlocal));


        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.recibo_getbyfecha", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        r = cargoRecibo(dataReader);
        //        rs.Add(r);
        //    }
        //    dataReader.Close();
        //    return rs;
        //}
    }
}
