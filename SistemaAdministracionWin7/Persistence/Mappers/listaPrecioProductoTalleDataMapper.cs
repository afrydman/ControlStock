namespace Persistence
{
    public static class listaPrecioProductoTalleDataMapper
    {
        //public static bool updatePrecio(Guid idlista, Guid idproductotalle, decimal precio,bool connLocal)
        //{

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idlista", idlista));
        //    ParametersList.Add(new SqlParameter("@idproductotalle", idproductotalle));
        //    ParametersList.Add(new SqlParameter("@precio", precio));


        //    return Conexion.ExecuteNonQuery("dbo.lista_precio_productotalle_UpdatePrecio", ParametersList,true, connLocal);

        //}
        //public static double getPrecio(Guid idListaPrecio, Guid idproductoTalle,bool connlocal)
        //{
        //    double coso = -666;

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idListaPrecio", idListaPrecio));
        //    ParametersList.Add(new SqlParameter("@idproductoTalle", idproductoTalle));

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.lista_precio_productotalle_getPrecio", ParametersList, connlocal);
        //    while (dataReader.Read())
        //    {


        //        coso = Convert.ToDouble(dataReader["precio"].ToString());





        //    }
        //    dataReader.Close();




        //    return coso;
        //}

        //public static bool insertPrecio(Guid idlista, Guid idproductotalle, decimal p,bool connLocal)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

            
        //    ParametersList.Add(new SqlParameter("@idLista", idlista));
        //    ParametersList.Add(new SqlParameter("@idProducto", idproductotalle));
        //    ParametersList.Add(new SqlParameter("@precio", p));



        //    return Conexion.ExecuteNonQuery("dbo.lista_precio_productotalle_insert", ParametersList, true, connLocal);
        //}

        //public static List<DTO.BusinessEntities.listaPrecioProductoTalleData> getAll(bool connLocal)
        //{
        //    listaPrecioProductoTalleData obj;
        //    List<listaPrecioProductoTalleData> todos = new List<listaPrecioProductoTalleData>();


        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.lista_precio_producto_talle_getall", null, connLocal);
        //    while (dataReader.Read())
        //    {

        //        obj = new listaPrecioProductoTalleData();

        //        obj.IDLista = new Guid(dataReader["idlista"].ToString());
        //        obj.IDProductoTalle =  new Guid(dataReader["idproductoTalle"].ToString());
        //        obj.precio = Convert.ToDecimal(dataReader["precio"].ToString());

        //        todos.Add(obj);




        //    }
        //    dataReader.Close();




        //    return todos;
            

        //}
    }
}
