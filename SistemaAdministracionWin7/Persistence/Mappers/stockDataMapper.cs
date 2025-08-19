namespace Persistence
{
    public static class stockDataMapper
    {
        //public static object getStock(Guid idProducto, Guid idColor, int talle, Guid idLocal)
        //{
        //    object stock = -666;
            
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@IDProducto", idProducto));
        //    ParametersList.Add(new SqlParameter("@IDColor", idColor));
        //    ParametersList.Add(new SqlParameter("@talle", talle));
        //    ParametersList.Add(new SqlParameter("@IDLocal", idLocal));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.stock_getStock", ParametersList);

        //    while (dataReader.Read())
        //    {

        //        if (dataReader["stock"] != System.DBNull.Value)
        //        {
                    
                    
        //            stock = dataReader["stock"];
        //        }

        //    }
        //    dataReader.Close();

        //    return stock;


        //}

        //public static bool updateStock(Guid idProducto, Guid idColor, int talle, Guid idLocal, decimal newStock)
        //{


        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idlocal", idLocal));
        //    ParametersList.Add(new SqlParameter("@idproducto", idProducto));
        //    ParametersList.Add(new SqlParameter("@idcolor", idColor));
        //    ParametersList.Add(new SqlParameter("@talle", talle));
        //    ParametersList.Add(new SqlParameter("@stock", newStock));

        //    return Conexion.ExecuteNonQuery("dbo.stock_Update", ParametersList);


        //}
        //public static List<stockData> getAllbyLocalAndProducto(Guid idlocal, Guid idProducto) { 
        
        // stockData s;
        //    List<stockData> stocks = new List<stockData>();

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idlocal", idlocal));
        //    ParametersList.Add(new SqlParameter("@idproducto", idProducto));


        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.stock_getByLocalAndProducto", ParametersList);

        //    while (dataReader.Read())
        //    {
        //        s = new stockData();
        //        s.ID = new Guid(dataReader["id"].ToString());
        //        s.color.ID = new Guid(dataReader["idcolor"].ToString());
        //        s.producto.ID = idProducto;
        //        s.Local.ID = new Guid(dataReader["idlocal"].ToString());
        //        s.stock = Convert.ToInt32(dataReader["stock"].ToString());
        //        s.talle = Convert.ToInt32(dataReader["talle"].ToString());
        //        stocks.Add(s);
        //    }
        //    dataReader.Close();

        //    return stocks;

        
        //}

        //public static List<DTO.BusinessEntities.stockData> getAllbyProducto(Guid idproducto)
        //{

        //    stockData s;
        //    List<stockData> stocks = new List<stockData>();

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@IDProducto", idproducto));
            

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.Stock_getAllproducto", ParametersList);

        //    while (dataReader.Read())
        //    {
        //        s = new stockData();
        //        s.ID = new Guid(dataReader["id"].ToString());
        //        s.color.ID = new Guid(dataReader["idcolor"].ToString());
        //        s.producto.ID = idproducto;
        //        s.Local.ID = new Guid(dataReader["idlocal"].ToString());
        //        s.stock = Convert.ToDecimal(dataReader["stock"].ToString());
        //        s.talle = Convert.ToInt32(dataReader["talle"].ToString());
        //        stocks.Add(s);
        //    }
        //    dataReader.Close();

        //    return stocks;



        //}

        //public static bool insertStock(Guid idProducto, Guid idColor, int talle, Guid idLocal, decimal newStock)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idlocal", idLocal));
        //    ParametersList.Add(new SqlParameter("@idproducto", idProducto));
        //    ParametersList.Add(new SqlParameter("@idcolor", idColor));
        //    ParametersList.Add(new SqlParameter("@talle", talle));
        //    ParametersList.Add(new SqlParameter("@stock", newStock));

        //    return Conexion.ExecuteNonQuery("dbo.[stock_Insert]", ParametersList);
            
        //}



        //public static List<detalleStockData> getDetalleStock(string codigo, Guid idlocal) 
        //{
        //    detalleStockData s;
        //    List<detalleStockData> stocks = new List<detalleStockData>();

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idlocal", idlocal));
        //    ParametersList.Add(new SqlParameter("@codigo", codigo));


        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.web_stock", ParametersList);

        //    while (dataReader.Read())
        //    {
        //        s = new detalleStockData();
        //        //s.codigo = new Guid(dataReader["idcolor"].ToString());
        //        s.codigo = dataReader["codigo"].ToString();
        //        s.cantidad = Convert.ToDecimal(dataReader["cantidad"].ToString());
        //        s.Description= dataReader["motivo"].ToString();
        //        s.Date = Convert.ToDateTime(dataReader["fecha"].ToString());
        //        stocks.Add(s);
        //    }
        //    dataReader.Close();

        //    return stocks;
        
        
        //}

        //public static List<stockData> getAll(Guid idlocal)
        //{


        //    stockData s;
        //    List<stockData> stocks = new List<stockData>();

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idlocal", idlocal));


        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.stock_getByLocal", ParametersList);

        //    while (dataReader!=null && dataReader.Read())
        //    {
        //        s = new stockData();
        //        s.ID = new Guid(dataReader["id"].ToString());
        //        s.color.ID = new Guid(dataReader["idcolor"].ToString());
        //        s.producto.ID = new Guid(dataReader["idproducto"].ToString());
        //        s.Local.ID = idlocal;
        //        s.stock = Convert.ToDecimal(dataReader["stock"].ToString());
        //        s.talle = Convert.ToInt32(dataReader["talle"].ToString());
        //        stocks.Add(s);
        //    }
        //    dataReader.Close();

        //    return stocks;



        //}

        
    }
}
