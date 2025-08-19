namespace Persistence
{
    public static class productoTalleDataMapper
    {
    //    public static List<DTO.BusinessEntities.productoTalleData> getByProducto(Guid idproducto)
    //    {
    //        productoTalleData coso;
    //        List<productoTalleData> cosos = new List<productoTalleData>();
    //        List<SqlParameter> ParametersList = new List<SqlParameter>();
    //        ParametersList.Add(new SqlParameter("@id", idproducto));
            
    //        SqlDataReader dataReader = Conexion.ExcuteReader("dbo.producto_talle_getbyProducto", ParametersList);
    //        while (dataReader.Read())
    //        {
    //            coso = new productoTalleData();

    //            coso.IDProducto = idproducto;
    //            coso.Talle = Convert.ToInt32(dataReader["talle"].ToString());
    //            coso.ID = new Guid(dataReader["id"].ToString());


    //            cosos.Add(coso);


    //        }
    //        dataReader.Close();




    //        return cosos;
    //    }

    //    public static Guid getIDByProductotalle(Guid idproducto, int talle)
    //    {
    //        Guid coso = new Guid();
            
    //        List<SqlParameter> ParametersList = new List<SqlParameter>();
    //        ParametersList.Add(new SqlParameter("@idproducto", idproducto));
    //        ParametersList.Add(new SqlParameter("@talle", talle));

    //        SqlDataReader dataReader = Conexion.ExcuteReader("dbo.producto_talle_getbyProducto_talle", ParametersList);
    //        while (dataReader.Read())
    //        {
                
                
    //            coso = new Guid(dataReader["id"].ToString());


                


    //        }
    //        dataReader.Close();




    //        return coso;
    //    }

    //    public static bool insert(Guid id, Guid idProducto, int p, bool connLocal = true)
    //    {
    //        List<SqlParameter> ParametersList = new List<SqlParameter>();
    //        ParametersList.Add(new SqlParameter("@id", id));
    //        ParametersList.Add(new SqlParameter("@idP", idProducto));
    //        ParametersList.Add(new SqlParameter("@talle", p));




    //        return Conexion.ExecuteNonQuery("dbo.productotalle_Insert", ParametersList, true, connLocal);
    //    }

    //    public static List<productoTalleData> getall(bool connLocal = true)
    //    {
    //        productoTalleData coso;
    //        List<productoTalleData> cosos = new List<productoTalleData>();
            

    //        SqlDataReader dataReader = Conexion.ExcuteReader("dbo.producto_talle_getAll", null,connLocal);
    //        while (dataReader.Read())
    //        {
    //            coso = new productoTalleData();

    //            coso.IDproducto = new Guid(dataReader["idproducto"].ToString()); 
    //            coso.talle = Convert.ToInt32(dataReader["talle"].ToString());
    //            coso.ID = new Guid(dataReader["id"].ToString());


    //            cosos.Add(coso);


    //        }
    //        dataReader.Close();




    //        return cosos;
    //    }
    }
}
