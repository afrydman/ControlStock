namespace Persistence
{
    public class localDataMapper
    {
       
        //public static bool getAvailability(Guid idLocal) {


        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.cajas_was_closed_today", ParametersList);

        //    bool availability = false;
        //    while (dataReader.Read())
        //    {

        //        if (dataReader["availability"] != System.DBNull.Value)
        //        {

        //            availability = (dataReader["availability"].ToString() == "1");
                    
        //        }

        //    }
        //    dataReader.Close();
        //    return availability;
        
        //}


        //public static cajaData getCajaInicial(Guid idLocal,DateTime fecha)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));
        //    ParametersList.Add(new SqlParameter("@fecha", fecha.Date));
        //    cajaData c = new cajaData();
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.local_getCaja_2", ParametersList);

            
        //    while (dataReader.Read())
        //    {

        //        if (dataReader["caja"] != System.DBNull.Value)
        //        {
                    
        //            c.Monto= Convert.ToDecimal(dataReader["caja"].ToString());
        //            c.Date = Convert.ToDateTime(dataReader["fecha"].ToString());
        //            c.Local.ID = idLocal;
        //        }

        //    }
        //    dataReader.Close();
        //    return c;
        //}

        

        //public static List<localData> getAll(bool connLocal = true)
        //{
        //    List<localData> locales = new List<localData>();
        //    localData Local;



        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.locales_SelectAll", null, connLocal);
        //    while (dataReader.Read())
        //    {

        //        Local = cargoLocal(dataReader);
                   

        //        locales.Add(Local);
        //    }
        //    dataReader.Close();
        //    return locales;
        //}

        //private static localData cargoLocal(SqlDataReader dataReader)
        //{
        //    localData Local = new localData();
        //    Local.desc = dataReader["descripcion"].ToString();
        //    Local.ID = new Guid(dataReader["id"].ToString());
        //    //Local.caja= Convert.ToDouble(dataReader["caja"].ToString());
        //    Local.cod = dataReader["codigo"].ToString();
        //    Local.direccion = dataReader["direccion"].ToString();
        //    Local.telefono = dataReader["telefono"].ToString(); ;
        //    Local.email = dataReader["email"].ToString(); ;
        //    Local.nombre = dataReader["nombre"].ToString(); ;
        //    Local.DateStock = Convert.ToDateTime(dataReader["fechaStock"].ToString()); ;
        //    return Local;
        //}





        //public static localData getbyID(Guid idLocal)
        //{

        //    localData Local = new localData();

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.locales_SelectRow", ParametersList);
        //    while (dataReader.Read())
        //    {


        //        Local = cargoLocal(dataReader);
                
        //    }
        //    dataReader.Close();
        //    return Local;
        //}

        //public static bool cerrarCaja(Guid idlocal, decimal final, DateTime fecha, Guid id, bool connLocal = true)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idlocal));
            
        //    ParametersList.Add(new SqlParameter("@final", final));
        //    ParametersList.Add(new SqlParameter("@fecha", fecha));
        //    ParametersList.Add(new SqlParameter("@idcaja", id));


        //    return Conexion.ExecuteNonQuery("dbo.locales_setCaja_2", ParametersList, true, connLocal);

        //}

        //public static List<cajaData> getOlderCajaThan(DateTime ultimaCaja, Guid idlocal, bool connLocal = true)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@fecha", ultimaCaja));
        //    ParametersList.Add(new SqlParameter("@idlocal", idlocal));

        //    List<cajaData> cajas = new List<cajaData>();
        //    cajaData c;
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.locales_getOlderCajaThan", ParametersList, connLocal);


        //    while (dataReader.Read())
        //    {
        //        c = new cajaData();
        //        if (dataReader["caja"] != System.DBNull.Value)
        //        {

        //            c.Monto = Convert.ToDecimal(dataReader["caja"].ToString());
        //            c.Date = Convert.ToDateTime(dataReader["fecha"].ToString());
        //            c.ID = new Guid(dataReader["id"].ToString());
        //            c.Local.ID = idlocal;


        //        }
        //        cajas.Add(c);
        //    }
        //    dataReader.Close();
        //    return cajas;
        //}

        //public static cajaData getLastCaja(Guid idLocal, bool connLocal = true)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));
        //    cajaData c = new cajaData();
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.locales_getLastCaja", ParametersList, connLocal);


        //    while (dataReader.Read())
        //    {

        //        if (dataReader["Monto"] != System.DBNull.Value)
        //        {

        //            c.Monto = Convert.ToDecimal(dataReader["Monto"].ToString());
        //            c.Date = Convert.ToDateTime(dataReader["fecha"].ToString());
        //            c.ID = new Guid(dataReader["id"].ToString());
        //            c.Local.ID = idLocal;
                    

        //        }

        //    }
        //    dataReader.Close();
        //    return c;
        //}



        //public static cajaData getCajabyFecha(Guid idLocal,DateTime fecha)
        //{

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));
        //    ParametersList.Add(new SqlParameter("@fecha", fecha));
        //    cajaData c = new cajaData();
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.locales_getCajabyfecha", ParametersList);


        //    while (dataReader.Read())
        //    {

        //        if (dataReader["caja"] != System.DBNull.Value)
        //        {

        //            c.Monto = Convert.ToDecimal(dataReader["caja"].ToString());
        //            c.Date = Convert.ToDateTime(dataReader["fecha"].ToString());
        //            c.ID = new Guid(dataReader["id"].ToString());
        //            c.Local.ID = idLocal;

        //        }

        //    }
        //    dataReader.Close();
        //    return c;
        //}

        //public static bool insert(localData l,bool connLocal = true)
        //{
            
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", l.ID));
        //    ParametersList.Add(new SqlParameter("@direccion", l.direccion));
        //    ParametersList.Add(new SqlParameter("@telefono", l.telefono));
        //    ParametersList.Add(new SqlParameter("@descripcion", l.desc));
        //    ParametersList.Add(new SqlParameter("@codigo", l.cod));
        //    ParametersList.Add(new SqlParameter("@nombre", l.nombre));
        //    ParametersList.Add(new SqlParameter("@email", l.email));
        //    ParametersList.Add(new SqlParameter("@fechaStock", l.DateStock));


        //    return Conexion.ExecuteNonQuery("dbo.locales_insert", ParametersList, true, connLocal);

        //}


        //public static bool update(localData l)
        //{

        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", l.ID));
        //    ParametersList.Add(new SqlParameter("@direccion", l.direccion));
        //    ParametersList.Add(new SqlParameter("@telefono", l.telefono));
        //    ParametersList.Add(new SqlParameter("@descripcion", l.desc));
        //    ParametersList.Add(new SqlParameter("@codigo", l.cod));
        //    ParametersList.Add(new SqlParameter("@nombre", l.nombre));
        //    ParametersList.Add(new SqlParameter("@email", l.email));
        //    ParametersList.Add(new SqlParameter("@fechaStock", l.DateStock));



        //   return Conexion.ExecuteNonQuery("dbo.locales_update", ParametersList);

        //}
    }
}

