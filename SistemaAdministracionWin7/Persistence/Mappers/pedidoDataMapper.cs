namespace Persistence
{
    public static class pedidoDataMapper
    {

        //public static int getLastNumber(Guid idLocal,int myprefix)
        //{   
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", myprefix));

        //    SqlDataReader dataReader;   
            
        //    dataReader = Conexion.ExcuteReader("dbo.pedido_getLastNumber", ParametersList);
            

        //    int Numero = 0;
        //    while (dataReader != null && dataReader.Read())
        //    {

        //        if (dataReader["Numero"] != System.DBNull.Value)
        //        {
        //            Numero = Convert.ToInt32(dataReader["Numero"].ToString());
        //        }
        //        else
        //        {
        //            Numero=1;
        //        }
                

        //    }
        //    dataReader.Close();
        //    return Numero;

        //}


        //public static int getLastNumberOther(Guid idLocal, int myprefix)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", myprefix));

        //    SqlDataReader dataReader;

        //    dataReader = Conexion.ExcuteReader("dbo.pedido_getLastNumber_other", ParametersList);



        //    int Numero = 0;
        //    while (dataReader.Read())
        //    {

        //        if (dataReader["Numero"] != System.DBNull.Value)
        //        {
        //            Numero = Convert.ToInt32(dataReader["Numero"].ToString());
        //        }
        //        else
        //        {
        //            Numero = 1;
        //        }


        //    }
        //    dataReader.Close();
        //    return Numero;

        //}



        //public static bool insertPedido(DTO.BusinessEntities.pedidoData nuevaVenta)
        //{
            
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", nuevaVenta.ID));
        //    ParametersList.Add(new SqlParameter("@idlocal", nuevaVenta.Local.ID));
        //    ParametersList.Add(new SqlParameter("@idvendedor", nuevaVenta.vendedor.ID));
        //    ParametersList.Add(new SqlParameter("@idCliente", nuevaVenta.tercero.ID));
        //    ParametersList.Add(new SqlParameter("@fecha", nuevaVenta.Date));
        //    ParametersList.Add(new SqlParameter("@subtotal", nuevaVenta.Monto));
        //    ParametersList.Add(new SqlParameter("@descuento", nuevaVenta.descuento));
        //    ParametersList.Add(new SqlParameter("@nfactura", nuevaVenta.Numero));
        //    ParametersList.Add(new SqlParameter("@anulada", nuevaVenta.Enable));
        //    ParametersList.Add(new SqlParameter("@Prefix", nuevaVenta.Prefix));
        //    ParametersList.Add(new SqlParameter("@completo", nuevaVenta.completo));

        //    return Conexion.ExecuteNonQuery("dbo.pedido_Insert", ParametersList);

           
        //}

        //public static List<DTO.BusinessEntities.pedidoData> getByFecha(DateTime fecha1, DateTime fecha2, Guid idlocal)
        //{

        //    List<pedidoData> ventas = new List<pedidoData>();
        //    pedidoData v;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@fechaAyer", fecha1));
        //    ParametersList.Add(new SqlParameter("@fechaMan", fecha2));
        //    ParametersList.Add(new SqlParameter("@idlocal", idlocal));
        //    //SqlDataReader dataReader = Conexion.ExcuteReader("dbo.personal_SelectAll", ParametersList);


        //    SqlDataReader dataReader;
           
           
        //        dataReader = Conexion.ExcuteReader("dbo.pedido_getByFecha", ParametersList);
            
            
            
            
        //    while (dataReader.Read())
        //    {

        //        v = cargoVenta(dataReader);
              

        //        ventas.Add(v);
        //    }
        //    dataReader.Close();




        //    return ventas;
        //}



       

        //public static bool anularPedido(Guid id)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));

        //    return Conexion.ExecuteNonQuery("dbo.pedido_anular", ParametersList);
        //}

        //public static bool actualizar(pedidoData nuevaVenta)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", nuevaVenta.ID));
        //    ParametersList.Add(new SqlParameter("@idlocal", nuevaVenta.Local.ID));
        //    ParametersList.Add(new SqlParameter("@idvendedor", nuevaVenta.vendedor.ID));
        //    ParametersList.Add(new SqlParameter("@idCliente", nuevaVenta.tercero.ID));
        //    ParametersList.Add(new SqlParameter("@fecha", nuevaVenta.Date));
        //    ParametersList.Add(new SqlParameter("@subtotal", nuevaVenta.Monto));
        //    ParametersList.Add(new SqlParameter("@descuento", nuevaVenta.descuento));
        //    ParametersList.Add(new SqlParameter("@nfactura", nuevaVenta.Numero));
        //    ParametersList.Add(new SqlParameter("@anulada", nuevaVenta.Enable));
        //    ParametersList.Add(new SqlParameter("@Prefix", nuevaVenta.Prefix));
        //    ParametersList.Add(new SqlParameter("@completo", nuevaVenta.completo));

        //    return Conexion.ExecuteNonQuery("dbo.pedido_Update", ParametersList);
        //}

        //public static pedidoData getbyID(Guid id)
        //{


        //    pedidoData v = new pedidoData();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));


        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.pedido_getById", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        v = cargoVenta(dataReader);
                
        //    }
        //    dataReader.Close();




        //    return v;
        //}

        //private static pedidoData cargoVenta(SqlDataReader dataReader)
        //{
        //    pedidoData v = new pedidoData();
        //    v.ID = new Guid(dataReader["id"].ToString());
        //    v.Enable = Convert.ToBoolean(dataReader["anulada"].ToString());
        //    v.descuento = Convert.ToDecimal(dataReader["descuento"].ToString());
        //    v.Date = Convert.ToDateTime(dataReader["fecha"].ToString());
        //    //v.cambio = Convert.ToBoolean(dataReader["cambio"].ToString());
        //    v.Numero = Convert.ToInt32(dataReader["nfactura"].ToString());
        //    v.Monto = Convert.ToDecimal(dataReader["subtotal"].ToString());
        //    v.Prefix = Convert.ToInt32(dataReader["Prefix"].ToString());
        //    v.completo = Convert.ToBoolean(dataReader["completo"].ToString());

        //    localData l = new localData();
        //    l.ID = new Guid(dataReader["idlocal"].ToString());
        //    v.Local = l;

        //    personalData p = new personalData();
        //    p.ID = new Guid(dataReader["idvendedor"].ToString());
        //    v.vendedor = p;

        //    proveedorData cliente = new proveedorData();
        //    cliente.ID = new Guid(dataReader["idCliente"].ToString());
        //    v.tercero = cliente;

        //    return v;
           
        //}

        //public static List<pedidoData> getBiggerThan(int ultimo, Guid idLocal, int Prefix)
        //{
        //    pedidoData v;
        //    List<pedidoData> ventas = new List<pedidoData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
            
        //    ParametersList.Add(new SqlParameter("@ultimo", ultimo));
        //    ParametersList.Add(new SqlParameter("@id", idLocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));

        //    SqlDataReader dataReader;

          
        //    dataReader = Conexion.ExcuteReader("dbo.pedido_getbiggerthan", ParametersList);
           
            
        //    while (dataReader.Read())
        //    {
        //        v = cargoVenta(dataReader);
                
        //        ventas.Add(v);
        //    }
        //    dataReader.Close();

        //    return ventas;
        //}

        //public static List<pedidoData> getModified(Guid idLocal)
        //{
        //    pedidoData v;
        //    List<pedidoData> ventas = new List<pedidoData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));

        //    SqlDataReader dataReader;
          
        //     dataReader = Conexion.ExcuteReader("dbo.pedido_getModified", ParametersList);
            
        //    while (dataReader.Read())
        //    {

        //        v = cargoVenta(dataReader);
                

                

        //        ventas.Add(v);


        //    }
        //    dataReader.Close();




        //    return ventas;
        //}

        //public static bool yaviqueestabasmodificadamacho(Guid idlocal)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
            
        //    ParametersList.Add(new SqlParameter("@id", idlocal));



        //    return Conexion.ExecuteNonQuery("dbo.pedido_marcarModificadas", ParametersList);
        //}










        //public static List<pedidoData> getall()
        //{
        //    pedidoData v;
        //    List<pedidoData> ventas = new List<pedidoData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        

        //    SqlDataReader dataReader;

        //    dataReader = Conexion.ExcuteReader("dbo.pedido_SelectAll", null);

        //    while (dataReader.Read())
        //    {

        //        v = cargoVenta(dataReader);




        //        ventas.Add(v);


        //    }
        //    dataReader.Close();




        //    return ventas;
        //}

        //public static bool marcarCompleto(Guid guid,bool completo)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

        //    ParametersList.Add(new SqlParameter("@id", guid));
        //    ParametersList.Add(new SqlParameter("@completo", completo));


        //    return Conexion.ExecuteNonQuery("dbo.pedido_marcarCompleto", ParametersList);
        //}
    }

    
}
