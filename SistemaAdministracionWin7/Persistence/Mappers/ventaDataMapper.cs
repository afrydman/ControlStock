namespace Persistence
{
    public static class ventaDataMapper
    {


        //public static ventaData getLast(Guid idLocal, int myprefix,bool connLocal=true)
        //{

        //    ventaData v = new ventaData();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idlocal", idLocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", myprefix));

        //    SqlDataReader dataReader;

        //    dataReader = Conexion.ExcuteReader("dbo.venta_getLast", ParametersList,connLocal);



        //    if (dataReader != null)
        //    {


        //        while (dataReader.Read())
        //        {

        //            v = cargoVenta(dataReader);


        //        }
        //    }
        //    if (dataReader!=null)
        //        dataReader.Close();
        //    return v;
        //}

        //public static int getLastNumber(Guid idLocal,int myprefix)
        //{   
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", myprefix));

        //    SqlDataReader dataReader;   
            
        //    dataReader = Conexion.ExcuteReader("dbo.venta_getLastNumber", ParametersList);
            
            

        //    int Numero = 0;
        //    while (dataReader.Read())
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

        //    dataReader = Conexion.ExcuteReader("dbo.venta_getLastNumber_other", ParametersList);



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



        //public static bool insertVenta(DTO.BusinessEntities.ventaData nuevaVenta, bool connLocal = true)
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
            
        //    ParametersList.Add(new SqlParameter("@cambio", nuevaVenta.cambio));
        //    ParametersList.Add(new SqlParameter("@anulada", nuevaVenta.Enable));
        //    ParametersList.Add(new SqlParameter("@Prefix", nuevaVenta.Prefix));
        //    ParametersList.Add(new SqlParameter("@Clase", (int)nuevaVenta.tipoVenta));
        //    ParametersList.Add(new SqlParameter("@observaciones", nuevaVenta.observaciones));


        //    return Conexion.ExecuteNonQuery("dbo.ventas_Insert", ParametersList, true, connLocal);

           
        //}

        //public static List<DTO.BusinessEntities.ventaData> getByFecha(DateTime fecha1,DateTime fecha2,bool cambio,Guid idlocal)
        //{

        //    List<ventaData> ventas = new List<ventaData>();
        //    ventaData v;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@fechaAyer", fecha1));
        //    ParametersList.Add(new SqlParameter("@fechaMan", fecha2));
        //    ParametersList.Add(new SqlParameter("@idlocal", idlocal));
        //    //SqlDataReader dataReader = Conexion.ExcuteReader("dbo.personal_SelectAll", ParametersList);


        //    SqlDataReader dataReader;
        //    if (cambio)
        //    {
        //        dataReader = Conexion.ExcuteReader("dbo.ventas_getByFecha_cambio", ParametersList);
        //    }
        //    else
        //    {
        //        dataReader = Conexion.ExcuteReader("dbo.ventas_getByFecha", ParametersList);
        //    }
            
            
            
        //    while (dataReader.Read())
        //    {

        //        v = cargoVenta(dataReader);
        //        v.cambio = cambio;

        //        ventas.Add(v);
        //    }
        //    dataReader.Close();




        //    return ventas;
        //}



        //public static ventaData getbyFactura(string nfactura,int Prefix)
        //{
            
        //    ventaData v = new ventaData();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@nfactura", nfactura));
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));
            
        //    //SqlDataReader dataReader = Conexion.ExcuteReader("dbo.personal_SelectAll", ParametersList);
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.ventas_getByFfactura", ParametersList);
        //    while (dataReader.Read())
        //    {
        //        v = cargoVenta(dataReader);
        //    }
        //    dataReader.Close();




        //    return v;
        //}

        //public static bool anularVenta(Guid id,bool connLocal)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));

        //    return Conexion.ExecuteNonQuery("dbo.ventas_anular", ParametersList,true, connLocal);
        //}



        //public static ventaData getbyID(Guid id, bool connLocal = true)
        //{


        //    ventaData v = new ventaData();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", id));


        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.ventas_getById", ParametersList,connLocal);
        //    while (dataReader.Read())
        //    {

        //        v = cargoVenta(dataReader);
                
        //    }
        //    dataReader.Close();




        //    return v;
        //}

        //private static ventaData cargoVenta(SqlDataReader dataReader)
        //{
        //    ventaData v = new ventaData();
        //    v.ID = new Guid(dataReader["id"].ToString());
        //    v.Enable = Convert.ToBoolean(dataReader["anulada"].ToString());
        //    v.descuento = Convert.ToDecimal(dataReader["descuento"].ToString());
        //    v.Date = Convert.ToDateTime(dataReader["fecha"].ToString());
        //    v.cambio = Convert.ToBoolean(dataReader["cambio"].ToString());
        //    v.Numero = Convert.ToInt32(dataReader["nfactura"].ToString());
        //    v.Monto = Convert.ToDecimal(dataReader["subtotal"].ToString());
        //    v.Prefix = Convert.ToInt32(dataReader["Prefix"].ToString());
        //    v.tipoVenta = (Clase)Convert.ToInt32(dataReader["Clase"].ToString());
        //    v.observaciones = dataReader["observaciones"].ToString();

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

        //public static List<ventaData> getBiggerThan(int ultimo, Guid idLocal, int Prefix, bool connLocal = true)
        //{
        //    ventaData v ;
        //    List<ventaData> ventas = new List<ventaData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
            
        //    ParametersList.Add(new SqlParameter("@ultimo", ultimo));
        //    ParametersList.Add(new SqlParameter("@id", idLocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));

        //    SqlDataReader dataReader;

          
        //    dataReader = Conexion.ExcuteReader("dbo.ventas_getbiggerthan", ParametersList,connLocal);
           
            
        //    while (dataReader.Read())
        //    {
        //        v = cargoVenta(dataReader);
                
        //        ventas.Add(v);
        //    }
        //    dataReader.Close();

        //    return ventas;
        //}

        //public static List<ventaData> getModified(Guid idLocal, bool connLocal = true)
        //{
        //    ventaData v;
        //    List<ventaData> ventas = new List<ventaData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));

        //    SqlDataReader dataReader;
          
        //     dataReader = Conexion.ExcuteReader("dbo.ventas_getModified", ParametersList,connLocal);
            
        //    while (dataReader.Read())
        //    {

        //        v = cargoVenta(dataReader);
                

                

        //        ventas.Add(v);


        //    }
        //    dataReader.Close();




        //    return ventas;
        //}

        //public static bool yaviqueestabasmodificadamacho(Guid idlocal,bool connLocal)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
            
        //    ParametersList.Add(new SqlParameter("@id", idlocal));



        //    return Conexion.ExecuteNonQuery("dbo.ventas_marcarModificadas", ParametersList, true, connLocal);
        //}

        //public static bool insertarFormasPago(pagoData formaspago,Guid id,bool connLocal=true)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idformapago", formaspago.FormaPago.ID));
        //    ParametersList.Add(new SqlParameter("@recargo", formaspago.aumento));
        //    ParametersList.Add(new SqlParameter("@lote", formaspago.lote));
        //    ParametersList.Add(new SqlParameter("@cupon", formaspago.cupon));
        //    ParametersList.Add(new SqlParameter("@importe", formaspago.importe));
        //    ParametersList.Add(new SqlParameter("@idVenta", id));
        //    ParametersList.Add(new SqlParameter("@id", formaspago.ID));



        //    return Conexion.ExecuteNonQuery("dbo.ventas_pagos_insert", ParametersList, true, connLocal);
        //}

        //public static List<pagoData> obtenerPagos(Guid id, bool connLocal = true)
        //{
        //    pagoData p;
        //    List<pagoData> pagos = new List<pagoData>();
        //    //List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    //ParametersList.Add(new SqlParameter("@id", id));

            
        //    //SqlDataReader dataReader = Conexion.ExcuteReader("dbo.ventas_getPagos", ParametersList,connLocal);
        //    //while (dataReader.Read())
        //    //{
        //    //    p = new formasPagoCuotasData();


        //    //    p.aumento = Convert.ToDecimal(dataReader["recargo"].ToString());

        //    //    formaPagoData aux = new formaPagoData();
        //    //    aux.ID = new Guid(dataReader["idformapago"].ToString());
        //    //    p.FormaPago = aux;
        //    //    p.lote =  dataReader["lote"].ToString();
        //    //    p.cupon = dataReader["cupon"].ToString();
        //    //    p.importe = Convert.ToDecimal(dataReader["importe"].ToString());
        //    //    p.ID = new Guid(dataReader["id"].ToString());
        //    //    p.IDVenta = new Guid(dataReader["idVenta"].ToString());

                


        //    //    pagos.Add(p);



        //    //}
        //    //dataReader.Close();




        //    return pagos;
        //}

        

        //public static List<ventaData> getVentasPagadasConCC(Guid idCliente, Guid idCC)
        //{

        //    ventaData v;
        //    List<ventaData> ventas = new List<ventaData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idCliente", idCliente));
        //    ParametersList.Add(new SqlParameter("@idCC", idCC));
        //    SqlDataReader dataReader;


            
        //    dataReader = Conexion.ExcuteReader("dbo.ventas_getPagadasCCByCliente", ParametersList);
            
            
        //    while (dataReader.Read())
        //    {

        //        v = cargoVenta(dataReader);
                

        //        ventas.Add(v);


        //    }
        //    dataReader.Close();




        //    return ventas;
        //}

        //public static List<ventaData> getbyCliente(Guid guid)
        //{
        //    ventaData v;
        //    List<ventaData> ventas = new List<ventaData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@idcliente", guid));

        //    SqlDataReader dataReader;

        //    dataReader = Conexion.ExcuteReader("dbo.ventas_getBycliente", ParametersList);

        //    while (dataReader.Read())
        //    {

        //        v = cargoVenta(dataReader);




        //        ventas.Add(v);


        //    }
        //    dataReader.Close();




        //    return ventas;
        //}

        //public static List<pagoData> obtenerTipoPagos(Guid idTipoPago)
        //{

        //    pagoData p;
        //    List<pagoData> pagos = new List<pagoData>();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idTipoPago));


        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.ventas_getTipoPago", ParametersList);
        //    while (dataReader.Read())
        //    {
        //        p = new pagoData();


        //        p.aumento = Convert.ToDecimal(dataReader["recargo"].ToString());

        //        formaPagoData aux = new formaPagoData();
        //        aux.ID = new Guid(dataReader["idformapago"].ToString());
        //        p.FormaPago = aux;
        //        p.lote = dataReader["lote"].ToString();
        //        p.cupon = dataReader["cupon"].ToString();
        //        p.importe = Convert.ToDecimal(dataReader["importe"].ToString());
        //        p.ID = new Guid(dataReader["id"].ToString());
        //        p.IDVenta = new Guid(dataReader["idVenta"].ToString());



        //        pagos.Add(p);



        //    }
        //    dataReader.Close();




        //    return pagos;
        




        //}
    }

    
}
