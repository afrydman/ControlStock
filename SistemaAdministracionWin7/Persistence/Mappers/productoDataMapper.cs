namespace Persistence
{
    public class productoDataMapper
    {

        //public static productoData getProductoByCodigoInterno(string codigoInternoProducto)
        //{
        //    productoData p = new productoData(); ;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@ID", codigoInternoProducto));
            

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.productos_SelectRow_byCodigo", ParametersList);
        //    while (dataReader.Read())
        //    {
              

        //        p = cargarProducto(dataReader);
        //    }
        //    dataReader.Close();
        //    return p;
        //}

        //private static productoData cargarProducto(SqlDataReader dataReader)
        //{
        //    productoData p = new productoData();
        //    p.ID = new Guid(dataReader["id"].ToString());
        //    p.Description = dataReader["descripcion"].ToString();

        //    lineaData lin = new lineaData();
        //    lin.ID = new Guid(dataReader["idlinea"].ToString());

        //    temporadaData temp = new temporadaData();
        //    temp.ID = new Guid(dataReader["idtemporada"].ToString());

        //    p.linea = lin;
        //    p.temporada = temp;
        //    p.enable = Convert.ToBoolean(dataReader["enable"].ToString());
        //    p.codigoInterno = dataReader["codigoInterno"].ToString();
            
        //    p.codigoProveedor = dataReader["codigoProveedor"].ToString();
            
        //    p.proveedor.ID = new Guid(dataReader["idproveedor"].ToString());
            
        //    return p;
        //}
        ////public static double getPrecio(Guid idProducto, Guid idLista)
        ////{
        ////    double precio=0;
        ////    productoData p = new productoData(); ;
        ////    List<SqlParameter> ParametersList = new List<SqlParameter>();
        ////    ParametersList.Add(new SqlParameter("@IDProducto", idProducto));
        ////    ParametersList.Add(new SqlParameter("@IDLista", idLista));
        ////    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.lista_precio_articulo_getPrecio", ParametersList);

        ////    while (dataReader.Read())
        ////    {
                
        ////        precio = Convert.ToDouble(dataReader["precio"].ToString());
                

        ////    }
        ////    dataReader.Close();
        ////    return precio;
            
        ////}

        //public static List<productoData> getAll(bool connLocal = true)
        //{
        //    productoData p;
        //    List<productoData> ps = new List<productoData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();



        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.productos_SelectAll", null,connLocal);
        //    while (dataReader.Read())
        //    {
              
        //        p = cargarProducto(dataReader);




        //        ps.Add(p);
        //    }
        //    dataReader.Close();
        //    return ps;
        //}

        //public static List<productoData> getProductosByProveedor(string codigoInterno)
        //{
        //    productoData p;
        //    List<productoData> ps = new List<productoData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", codigoInterno + "%"));


        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.productos_getbycodigoInterno", ParametersList);
        //    while (dataReader.Read())
        //    {
        //        p = cargarProducto(dataReader);


        //        ps.Add(p);
        //    }
        //    dataReader.Close();
        //    return ps;
        //}

        //public static bool disable(Guid idproducto)
        //{

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idproducto));
        //    return Conexion.ExecuteNonQuery("dbo.productos_disable", ParametersList);
        //}
        
        //public static productoData getbyID(Guid guid)
        //{
        //    productoData p = new productoData(); ;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@ID", guid));


        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.productos_SelectRow", ParametersList);
        //    while (dataReader.Read())
        //    {
        //        p = cargarProducto(dataReader);
        //    }
        //    dataReader.Close();
        //    return p;
        //}

        //public static bool insert(productoData p, bool connLocal = true)
        //{

            
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", p.ID));
        //    ParametersList.Add(new SqlParameter("@idproveedor", p.proveedor.ID));
        //    ParametersList.Add(new SqlParameter("@descripcion", p.Description.Trim()));
        //    ParametersList.Add(new SqlParameter("@foto", ""));
        //    ParametersList.Add(new SqlParameter("@codigoproveedor", p.codigoProveedor.Trim()));
        //    ParametersList.Add(new SqlParameter("@idlinea", p.linea.ID));
        //    ParametersList.Add(new SqlParameter("@idtemporada", p.temporada.ID));
        //    ParametersList.Add(new SqlParameter("@codigointerno", p.codigoInterno));




        //    return Conexion.ExecuteNonQuery("dbo.productos_Insert", ParametersList, true, connLocal);
        //}

        //public static List<productoData> getProductosByProveedor(Guid guid)
        //{
        //    productoData p;
        //    List<productoData> ps = new List<productoData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@ID", guid));


        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.productos_getbyIdProveedor", ParametersList);
        //    while (dataReader.Read())
        //    {
        //        p = cargarProducto(dataReader); ;


        //        ps.Add(p);
        //    }
        //    dataReader.Close();
        //    return ps;
        //}

        //public static bool update(productoData p, bool connLocal = true)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", p.ID));
        //    ParametersList.Add(new SqlParameter("@idproveedor", p.proveedor.ID));
        //    ParametersList.Add(new SqlParameter("@descripcion", p.Description));
        //    ParametersList.Add(new SqlParameter("@foto", ""));
        //    ParametersList.Add(new SqlParameter("@codigoproveedor", p.codigoProveedor));
        //    ParametersList.Add(new SqlParameter("@idlinea", p.linea.ID));
        //    ParametersList.Add(new SqlParameter("@idtemporada", p.temporada.ID));
        //    ParametersList.Add(new SqlParameter("@codigointerno", p.codigoInterno));





        //    return Conexion.ExecuteNonQuery("dbo.productos_Update", ParametersList, true, connLocal);
        //}

        //public static bool enable(Guid guid)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", guid));
        //    return Conexion.ExecuteNonQuery("dbo.productos_enable", ParametersList);
        //}
    }
}

