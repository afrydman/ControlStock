namespace Persistence
{
    public class ingresoDataMapper
    {
        //public static List<retiroData> getRetirosByFecha(Guid idLocal, DateTime ayer, DateTime manana)
        //{

        //    List<retiroData> retiros = new List<retiroData>();
        //    retiroData r;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idLocal));
        //    ParametersList.Add(new SqlParameter("@fechaAyer", ayer));
        //    ParametersList.Add(new SqlParameter("@fechaManana", manana));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.ingresos_getByFecha", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        r = CargoData(dataReader);
        //        retiros.Add(r);
        //    }
        //    dataReader.Close();




        //    return retiros;
        //}

        ////public static retiroData getRetiro(Guid idvendedor, Guid idLocal, string codigo)
        ////{

        ////    retiroData r = new retiroData();
        ////    r.Monto = -1;
        ////    List<SqlParameter> ParametersList = new List<SqlParameter>();
        ////    ParametersList.Add(new SqlParameter("@idvendedor", idvendedor));
        ////    ParametersList.Add(new SqlParameter("@idlocal", idLocal));
        ////    ParametersList.Add(new SqlParameter("@codigo", codigo));
        ////    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.ingresos_getRetiro", ParametersList);
        ////    while (dataReader.Read())
        ////    {

        ////        r = CargoData(dataReader);
            
        ////    }
        ////    dataReader.Close();




        ////    return r;
        ////}

        //public static bool retiroEfectuado(string codigo)
        //{
            
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@codigo", codigo));

        //    return Conexion.ExecuteNonQuery("dbo.ingresos_efectuado", ParametersList);
            

        //}

        //public static bool insert(retiroData r,bool connLocal)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

        //    ParametersList.Add(new SqlParameter("@idLocal", r.Local.ID));
        //    ParametersList.Add(new SqlParameter("@idPersonal", r.Personal.ID));
        //    ParametersList.Add(new SqlParameter("@fecha", r.Date));
        //    ParametersList.Add(new SqlParameter("@Monto", r.Monto));
        //    ParametersList.Add(new SqlParameter("@codigo", r.codigo));
        //    ParametersList.Add(new SqlParameter("@fechauso", r.DateUso));
        //    ParametersList.Add(new SqlParameter("@idTipo", r.tipoRetiro.ID));
        //    ParametersList.Add(new SqlParameter("@desc", r.desc));
        //    ParametersList.Add(new SqlParameter("@id", r.ID));
        //    ParametersList.Add(new SqlParameter("@anulada", r.anulada));
        //    ParametersList.Add(new SqlParameter("@Numero", r.Numero));
        //    ParametersList.Add(new SqlParameter("@Prefix", r.Prefix));





        //    return Conexion.ExecuteNonQuery("dbo.ingresos_insert", ParametersList,true,connLocal);
        //}

        //public static retiroData getLast(Guid idlocal,int Prefix,bool connLocal)
        //{

        //    retiroData r = new retiroData();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idlocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.ingresos_getLast", ParametersList, connLocal);
        //    while (dataReader.Read())
        //    {


        //        r = CargoData(dataReader);
        //    }
        //    dataReader.Close();


        //    return r;
        //}

        //public static List<retiroData> getOlderThan(DateTime fecha,Guid idLocal,int Prefix,bool connLocal)
        //{
        //    List<retiroData> retiros = new List<retiroData>();
        //    retiroData r;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@fecha", fecha));
        //    ParametersList.Add(new SqlParameter("@idlocal", idLocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.ingresos_getOlder", ParametersList,connLocal);
        //    while (dataReader.Read())
        //    {
        //        r = CargoData(dataReader); 
        //        retiros.Add(r);
        //    }
        //    dataReader.Close();




        //    return retiros;
        //}

        //public static bool delete(Guid id,bool connLocal)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

        //    ParametersList.Add(new SqlParameter("@id", id));

        //    return Conexion.ExecuteNonQuery("dbo.ingresos_delete", ParametersList,true,connLocal);
        //}

        //public static List<retiroData> getmodiffied(Guid idlocal,int Prefix,bool connLocal)
        //{

        //    List<retiroData> retiros = new List<retiroData>();
        //    retiroData r;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idlocal));
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.ingresos_getMod", ParametersList,connLocal);
        //    while (dataReader.Read())
        //    {

        //        r = CargoData(dataReader); 
        //        retiros.Add(r);
        //    }
        //    dataReader.Close();




        //    return retiros;
        //}

        //public static bool yaviqueestabasmodificadamacho(Guid id,int Prefix,bool connLocal)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

        //    ParametersList.Add(new SqlParameter("@id", id));
        //    ParametersList.Add(new SqlParameter("@Prefix", Prefix));
        //    return Conexion.ExecuteNonQuery("dbo.ingresos_yavi", ParametersList,true,connLocal);
        //}

        //public static retiroData getbyID(Guid guid)
        //{


        //    retiroData r = new retiroData();
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", guid));
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.ingresos_getbyid", ParametersList);
        //    while (dataReader.Read())
        //    {


        //        r = CargoData(dataReader);
        //    }
        //    dataReader.Close();


        //    return r;
        //}
        //private static retiroData CargoData(SqlDataReader dataReader)
        //{
        //    retiroData r = new retiroData();
        //    r.ID = new Guid(dataReader["id"].ToString());

        //    localData l = new localData();
        //    l.ID = new Guid(dataReader["idLocal"].ToString());
        //    r.Local = l;

        //    personalData p = new personalData();
        //    p.ID = new Guid(dataReader["idPersonal"].ToString());
        //    r.Personal = p;

        //    r.Monto = Convert.ToDecimal(dataReader["Monto"].ToString());

        //    r.DateUso = Convert.ToDateTime(dataReader["fechaUso"]);
        //    r.Date = Convert.ToDateTime(dataReader["fecha"]);
        //    r.tipoRetiro.ID = new Guid(dataReader["idTipoRetiro"].ToString());
        //    r.desc = dataReader["descripcion"].ToString();
        //    r.anulada = Convert.ToBoolean(dataReader["anulado"].ToString());
        //    r.Numero = Convert.ToInt32(dataReader["Numero"].ToString());
        //    r.Prefix = Convert.ToInt32(dataReader["Prefix"].ToString());
        //    return r;
        //}
    
    }


}
