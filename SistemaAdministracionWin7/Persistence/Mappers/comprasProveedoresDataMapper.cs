namespace Persistence
{
    public static class comprasProveedoresDataMapper
    {

        //public static bool insert(comprasProveedoresData r)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", r.ID));
        //    ParametersList.Add(new SqlParameter("@fecha", r.Date));
        //    ParametersList.Add(new SqlParameter("@fechaFactura", r.FechaFactura));

        //    ParametersList.Add(new SqlParameter("@idLocal", r.Local.ID));
        //    ParametersList.Add(new SqlParameter("@Monto", r.Monto));
        //    ParametersList.Add(new SqlParameter("@idProveedor", r.tercero.ID));
        //    ParametersList.Add(new SqlParameter("@idPersonal", r.vendedor.ID));
        //    ParametersList.Add(new SqlParameter("@anulada", r.Enable));

        //    ParametersList.Add(new SqlParameter("@obs", r.Description));
        //    ParametersList.Add(new SqlParameter("@Numero", r.Numero));
        //    ParametersList.Add(new SqlParameter("@Prefix", r.Prefix));
        //    ParametersList.Add(new SqlParameter("@iibb", r.iibb));
        //    ParametersList.Add(new SqlParameter("@iibbProv", r.iibbProv));
        //    ParametersList.Add(new SqlParameter("@descuento", r.descuento));


        //    return Conexion.ExecuteNonQuery("dbo.comprasProveedores_insert", ParametersList);
        //}

        //public static bool anular(Guid id)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));

        //    return Conexion.ExecuteNonQuery("dbo.comprasProveedores_anular", ParametersList);
        //}



        //public static comprasProveedoresData getById(Guid id)
        //{

        //    comprasProveedoresData compra = new comprasProveedoresData();
            
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.comprasProveedores_getbyID", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        compra = cargarDatos(dataReader);
        //    }
        //    dataReader.Close();




        //    return compra;
        //}

        //private static comprasProveedoresData cargarDatos(SqlDataReader dataReader)
        //{
        //    comprasProveedoresData compra = new comprasProveedoresData();
        //    compra.ID = new Guid(dataReader["id"].ToString()); ;
        //    compra.Date = Convert.ToDateTime(dataReader["Fecha"].ToString()); ;
        //    compra.FechaFactura= Convert.ToDateTime(dataReader["FechaFactura"].ToString()); ;

        //    compra.Local.ID = new Guid(dataReader["idLocal"].ToString()); ;
        //    compra.tercero.ID = new Guid(dataReader["idProveedor"].ToString()); ;
        //    compra.vendedor.ID = new Guid(dataReader["idPersonal"].ToString()); ;
        //    compra.Monto = Convert.ToDecimal(dataReader["Monto"].ToString());
        //    compra.Enable = Convert.ToBoolean(dataReader["anulada"].ToString());
        //    compra.observaciones = dataReader["obs"].ToString();
        //    compra.Numero = Convert.ToInt32(dataReader["Numero"].ToString());
        //    compra.Prefix = Convert.ToInt32(dataReader["Prefix"].ToString());
        //    compra.iibb = Convert.ToDecimal(dataReader["iibb"].ToString());
        //    compra.iibbProv = Convert.ToDecimal(dataReader["iibbProv"].ToString());
        //    if (dataReader["descuento"] != System.DBNull.Value)
        //        compra.descuento = Convert.ToDecimal(dataReader["descuento"].ToString());



        //    return compra;
        //}

        //public static List<comprasProveedoresData> getByProveedor(Guid id)
        //{

        //    comprasProveedoresData compra;
        //    List<comprasProveedoresData> cs = new List<comprasProveedoresData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idProveedor", id));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.comprasProveedores_getbyProveedor", ParametersList);
        //    while (dataReader.Read())
        //    {
        //        compra = cargarDatos(dataReader); 
        //        cs.Add(compra);
        //    }
        //    dataReader.Close();

        //    return cs;
        //}

        ////

     

        //public static List<comprasProveedoresData> getAll()
        //{
        //    comprasProveedoresData compra;
        //    List<comprasProveedoresData> cs = new List<comprasProveedoresData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.comprasProveedores_getAll", ParametersList);
        //    while (dataReader.Read())
        //    {
        //        compra = cargarDatos(dataReader);
        //        cs.Add(compra);
        //    }
        //    dataReader.Close();

        //    return cs;
        //}

        //public static bool update(comprasProveedoresData r)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", r.ID));
        //    ParametersList.Add(new SqlParameter("@fecha", r.Date));
        //    ParametersList.Add(new SqlParameter("@idLocal", r.Local.ID));
        //    ParametersList.Add(new SqlParameter("@Monto", r.Monto));
        //    ParametersList.Add(new SqlParameter("@idProveedor", r.tercero.ID));
        //    ParametersList.Add(new SqlParameter("@idPersonal", r.vendedor.ID));
        //    ParametersList.Add(new SqlParameter("@anulado", r.Enable));
        //    ParametersList.Add(new SqlParameter("@obs", r.observaciones));
        //    ParametersList.Add(new SqlParameter("@Prefix", r.Prefix));
        //    ParametersList.Add(new SqlParameter("@Numero", r.Numero));
        //    ParametersList.Add(new SqlParameter("@iibb", r.iibb));
        //    ParametersList.Add(new SqlParameter("@iibbProv", r.iibbProv));
        //    ParametersList.Add(new SqlParameter("@fechafact", r.FechaFactura));

        //    return Conexion.ExecuteNonQuery("dbo.comprasProveedores_update", ParametersList);
        //}

        //public static comprasProveedoresData getlast(Guid idlocal, int Prefix)
        //{
        //    comprasProveedoresData compra  = new comprasProveedoresData();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idlocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));
            
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.comprasProveedores_getLast", ParametersList);
        //    while (dataReader!=null && dataReader.Read())
        //    {
        //        compra = cargarDatos(dataReader);
                
        //    }
        //    if (dataReader!=null)
        //    {
        //        dataReader.Close();    
        //    }
            

        //    return compra;
        //}
    }
}
