namespace Persistence
{
    public   class notaCreditoClientesDataMapper 
    {


    //    public static bool insertDetalle(DTO.BusinessEntities.notaDetalleData d)
    //    {
    //        return notaDataMapper.insertDetalle(d, "dbo.notaCreditoClientesDetalles_Insert");
    //    }

    //    public static DTO.BusinessEntities.notaData getLast(int Prefix, Guid idlocal)
    //    {
    //        return notaDataMapper.getLast(Prefix, idlocal, "dbo.notaCreditoClientes_GetLast");
    //    }


    //    public static bool Anular(Guid id) 
    //    {
    //        return notaDataMapper.Anular(id, "dbo.notaCreditoClientes_Anular");
    //    }


    //    public static notaData GetById(Guid id, bool completo) {


    //        notaData aux = notaDataMapper.getbyId(id, "dbo.notaCreditoClientes_getById",true);

    //        if (completo)
    //        {
    //            aux.detalles = notaDataMapper.getDetalleByIdNota(id, "dbo.notaCreditoClientesDetalles_GetbyIdNota");
    //        }

    //        return aux;
    //    }


    //    public static bool insert(DTO.BusinessEntities.notaData mov)
    //    {
    //        return notaDataMapper.insert(mov, "dbo.notaCreditoClientes_Insert");
    //    }

    //    public static List<notaData> getbyTercero(Guid idProveedor, bool completo)
    //    {

    //        List<notaData> aux = notaDataMapper.getByTercero(idProveedor, "dbo.notaCreditoClientes_getbytercero");

    //        if (completo)
    //        {
    //            foreach (notaData item in aux)
    //            {
    //                item.detalles = notaDataMapper.getDetalleByIdNota(item.ID, "dbo.notaCreditoClientesDetalles_GetbyIdNota");
    //            }

    //        }

    //        return aux;

    //    }


    //    public static List<notaData> getbyFecha(System.Data.SqlTypes.SqlDateTime desde, System.Data.SqlTypes.SqlDateTime hasta, Guid idlocal, bool completo)
    //    {
    //        List<notaData> aux = notaDataMapper.getbyFecha(desde, hasta, idlocal, "dbo.notaCreditoClientes_getbyfecha");

    //        if (completo)
    //        {
    //            foreach (notaData item in aux)
    //            {
    //                item.detalles = notaDataMapper.getDetalleByIdNota(item.ID, "dbo.notaCreditoProveedoresDetalles_GetbyIdNota");
    //            }

    //        }

    //        return aux;
    //    }

      
    //}

    //public class notaDebitoClientesDataMapper
    //{


    //    public static bool insertDetalle(DTO.BusinessEntities.notaDetalleData d)
    //    {
    //        return notaDataMapper.insertDetalle(d, "dbo.notaDebitoClientesDetalles_Insert");
    //    }

    //    public static DTO.BusinessEntities.notaData getLast(int Prefix, Guid idlocal)
    //    {
    //        return notaDataMapper.getLast(Prefix, idlocal, "dbo.notaDebitoClientes_GetLast");
    //    }

    //    public static bool insert(DTO.BusinessEntities.notaData mov)
    //    {
    //        return notaDataMapper.insert(mov, "dbo.notaDebitoClientes_Insert");
    //    }

    //    public static notaData GetById(Guid id, bool completo)
    //    {
    //        notaData aux = notaDataMapper.getbyId(id, "dbo.notaDebitoClientes_getById", true);

    //        if (completo)
    //        {
    //            aux.detalles = notaDataMapper.getDetalleByIdNota(id, "dbo.notaDebitoClientesDetalles_GetbyIdNota");
    //        }

    //        return aux;
    //    }

    //    public static bool Anular(Guid id)
    //    {
    //        return notaDataMapper.Anular(id, "dbo.notaDebitoClientes_Anular");
    //    }


    //    public static List<notaData> getbyTercero(Guid idProveedor, bool completo)
    //    {

    //        List<notaData> aux = notaDataMapper.getByTercero(idProveedor, "dbo.notaDebitoClientes_getbytercero");

    //        if (completo)
    //        {
    //            foreach (notaData item in aux)
    //            {
    //                item.detalles = notaDataMapper.getDetalleByIdNota(item.ID, "dbo.notaDebitoClientesDetalles_GetbyIdNota");
    //            }

    //        }

    //        return aux;

    //    }
    //}


    ///// <summary>
    ///// ///////////////////////////////////////////////////////////////////////////////////////////
    ///// 
    ///// 
    ///// 



    //public class notaCreditoProveedoresDataMapper
    //{


    //    public static bool insertDetalle(DTO.BusinessEntities.notaDetalleData d)
    //    {
    //        return notaDataMapper.insertDetalle(d, "dbo.notaCreditoProveedoresDetalles_Insert");
    //    }

    //    public static DTO.BusinessEntities.notaData getLast(int Prefix, Guid idlocal)
    //    {
    //        return notaDataMapper.getLast(Prefix, idlocal, "dbo.notaCreditoProveedores_GetLast");
    //    }

    //    public static bool insert(DTO.BusinessEntities.notaData mov)
    //    {
    //        return notaDataMapper.insert(mov, "dbo.notaCreditoProveedores_Insert");
    //    }

    //    public static notaData GetById(Guid id, bool completo)
    //    {
    //         notaData aux = notaDataMapper.getbyId(id, "dbo.notaCreditoProveedores_getById", true);

    //        if (completo)
    //        {
    //            aux.detalles = notaDataMapper.getDetalleByIdNota(id, "dbo.notaCreditoProveedoresDetalles_GetbyIdNota");
    //        }

    //        return aux;
    //    }

    //    public static bool Anular(Guid id)
    //    {
    //        return notaDataMapper.Anular(id, "dbo.notaCreditoProveedores_Anular");
    //    }

    //    public static List<notaData> getbyTercero(Guid idProveedor,bool completo)
    //    {
           
    //        List<notaData> aux = notaDataMapper.getByTercero(idProveedor, "dbo.notaCreditoProveedores_getbytercero");

    //        if (completo)
    //        {
    //            foreach (notaData item in aux)
    //            {
    //                item.detalles = notaDataMapper.getDetalleByIdNota(item.ID, "dbo.notaCreditoProveedoresDetalles_GetbyIdNota");  
    //            }
                
    //        }

    //        return aux;

    //    }

    //    public static List<notaData> getbyfecha(System.Data.SqlTypes.SqlDateTime desde, System.Data.SqlTypes.SqlDateTime hasta, Guid idlocal, bool completo)
    //    {
    //        List<notaData> aux = notaDataMapper.getbyFecha(desde,hasta,idlocal, "dbo.notaCreditoProveedores_getbyfecha");

    //        if (completo)
    //        {
    //            foreach (notaData item in aux)
    //            {
    //                item.detalles = notaDataMapper.getDetalleByIdNota(item.ID, "dbo.notaCreditoProveedoresDetalles_GetbyIdNota");
    //            }

    //        }

    //        return aux;
    //    }
    //}

    //public class notaDebitoProveedoresDataMapper
    //{


    //    public static bool insertDetalle(DTO.BusinessEntities.notaDetalleData d)
    //    {
    //        return notaDataMapper.insertDetalle(d, "dbo.notaDebitoProveedoresDetalles_Insert");
    //    }

    //    public static DTO.BusinessEntities.notaData getLast(int Prefix, Guid idlocal)
    //    {
    //        return notaDataMapper.getLast(Prefix, idlocal, "dbo.notaDebitoProveedores_GetLast");
    //    }

    //    public static bool insert(DTO.BusinessEntities.notaData mov)
    //    {
    //        return notaDataMapper.insert(mov, "dbo.notaDebitoProveedores_Insert");
    //    }

    //    public static notaData GetById(Guid id, bool completo)
    //    {
    //        notaData aux = notaDataMapper.getbyId(id, "dbo.notaDebitoProveedores_getById", true);

    //        if (completo)
    //        {
    //            aux.detalles = notaDataMapper.getDetalleByIdNota(id, "dbo.notaDebitoProveedoresDetalles_GetbyIdNota");
    //        }

    //        return aux;
    //    }

    //    public static bool Anular(Guid id)
    //    {
    //        return notaDataMapper.Anular(id, "dbo.notaDebitoProveedores_Anular");
    //    }

    //    public static List<notaData> getbyTercero(Guid idProveedor, bool completo)
    //    {
    //        List<notaData> aux = notaDataMapper.getByTercero(idProveedor, "dbo.notaDebitoProveedores_getbytercero");

    //        if (completo)
    //        {
    //            foreach (notaData item in aux)
    //            {
    //                item.detalles = notaDataMapper.getDetalleByIdNota(item.ID, "dbo.notaDebitoProveedoresDetalles_GetbyIdNota");
    //            }

    //        }

    //        return aux;
    //    }
    //}
    ///// </summary>


    //public  class notaDataMapper
    //{

    //    public static DTO.BusinessEntities.notaData getbyId(Guid id, string sp,bool credito)
    //    {

    //        notaData nota = new notaData();

    //        List<SqlParameter> ParametersList = new List<SqlParameter>();
           
    //        ParametersList.Add(new SqlParameter("@id", id));

    //        SqlDataReader dataReader = Conexion.ExcuteReader(sp, ParametersList);

    //        while (dataReader.Read())
    //        {
    //            nota = cargoNota(dataReader,credito);
                

    //        }
    //        dataReader.Close();




    //        return nota;
    //    }

    //    public static DTO.BusinessEntities.notaData getLast(int Prefix, Guid idlocal, string sp)
    //    {


    //        notaData nota = new notaData() ;
            
    //        List<SqlParameter> ParametersList = new List<SqlParameter>();
    //        ParametersList.Add(new SqlParameter("@Prefix", Prefix));
    //        ParametersList.Add(new SqlParameter("@idlocal", idlocal));

    //        SqlDataReader dataReader = Conexion.ExcuteReader(sp, ParametersList);

    //        while (dataReader!=null && dataReader.Read())
    //        {
    //            nota = cargoNota(dataReader,false);
                
    //        }
    //        if (dataReader!=null )
    //        {
    //            dataReader.Close();
    
    //        }
            



    //        return nota;
    //    }

    //    private static notaData cargoNota(SqlDataReader dataReader,bool credito)
    //    {
    //        notaData n = new notaData();
    //        n.Enable = Convert.ToBoolean(dataReader["anulada"].ToString());
    //        n.Clase = (Clase)Convert.ToInt32(dataReader["Clase"].ToString());
    //        n.Date = Convert.ToDateTime(dataReader["fecha"].ToString());
    //        n.Prefix = Convert.ToInt32(dataReader["Prefix"].ToString());
    //        n.ID = new Guid(dataReader["id"].ToString());
    //        n.iva = Convert.ToDecimal(dataReader["iva"].ToString());
    //        n.Local.ID = new Guid(dataReader["idlocal"].ToString());
    //        n.Monto = Convert.ToDecimal(dataReader["Monto"].ToString());
    //        n.Numero = Convert.ToInt32(dataReader["Numero"].ToString());
    //        n.observaciones = dataReader["obs"].ToString();
    //        n.tercero.ID = new Guid(dataReader["idcliente"].ToString());
            
    //        n.vendedor.ID = new Guid(dataReader["idpersonal"].ToString());

    //        //n.tipo = credito ? tipoNota.Credito : tipoNota.Debito;

    //        return n;
    //    }

    //    public static List<notaDetalleData> getDetalleByIdNota(Guid idnota, string sp)
    //    { 
    //        List<notaDetalleData> ds = new List<notaDetalleData>();
    //     notaDetalleData d = new notaDetalleData() ;
            
    //        List<SqlParameter> ParametersList = new List<SqlParameter>();
            
    //        ParametersList.Add(new SqlParameter("@idnota", idnota));

    //        SqlDataReader dataReader = Conexion.ExcuteReader(sp, ParametersList);

    //        while (dataReader.Read())
    //        {
    //            d = cargoNotaDetalle(dataReader);
    //            ds.Add(d);
                
    //        }
    //        dataReader.Close();




    //        return ds;
        
    //    }

    //    private static notaDetalleData cargoNotaDetalle(SqlDataReader dataReader)
    //    {
    //        notaDetalleData d = new notaDetalleData();
    //        d.unitario = Convert.ToDecimal(dataReader["unitario"].ToString());
    //        d.IDNota = new Guid(dataReader["idnota"].ToString()); ;
    //        d.cantidad = Convert.ToDecimal(dataReader["cantidad"].ToString());
    //        d.desc = dataReader["descripcion"].ToString() ;
    //        d.alicuota = Convert.ToDecimal(dataReader["alicuota"].ToString());


    //        return d;
    //    }
        

    //    public static bool insertDetalle(DTO.BusinessEntities.notaDetalleData d,string sp)
    //    {
    //        List<SqlParameter> ParametersList = new List<SqlParameter>();
    //        ParametersList.Add(new SqlParameter("@idnota", d.IDNota));
    //        ParametersList.Add(new SqlParameter("@cantidad", d.cantidad));
    //        ParametersList.Add(new SqlParameter("@descripcion", d.desc));
    //        ParametersList.Add(new SqlParameter("@unitario", d.unitario));
    //        ParametersList.Add(new SqlParameter("@alicuota", d.alicuota));
            

    //        return Conexion.ExecuteNonQuery(sp, ParametersList);
    //    }

    //    public static bool insert(DTO.BusinessEntities.notaData n,string sp)
    //    {
    //        List<SqlParameter> ParametersList = new List<SqlParameter>();
    //        ParametersList.Add(new SqlParameter("@id", n.ID));
    //        ParametersList.Add(new SqlParameter("@anulada", n.Enable));
    //        ParametersList.Add(new SqlParameter("@Clase", n.Clase));
    //        ParametersList.Add(new SqlParameter("@fecha", n.Date));
    //        ParametersList.Add(new SqlParameter("@Prefix", n.Prefix));
    //        ParametersList.Add(new SqlParameter("@iva", n.iva));
    //        ParametersList.Add(new SqlParameter("@idlocal", n.Local.ID));
    //        ParametersList.Add(new SqlParameter("@Monto", n.Monto));
    //        ParametersList.Add(new SqlParameter("@Numero", n.Numero));
    //        ParametersList.Add(new SqlParameter("@obs", n.observaciones));
    //        ParametersList.Add(new SqlParameter("@idcliente", n.tercero.ID));
            
    //        ParametersList.Add(new SqlParameter("@idpersonal", n.vendedor.ID));




    //        return Conexion.ExecuteNonQuery(sp, ParametersList);
    //    }

    //    internal static bool Anular(Guid id, string sp)
    //    {
    //        List<SqlParameter> ParametersList = new List<SqlParameter>();
    //        ParametersList.Add(new SqlParameter("@id", id));
    //        return Conexion.ExecuteNonQuery(sp, ParametersList);
    //    }

    //    internal static List<notaData> getByTercero(Guid idProveedor,string sp)
    //    {
    //        List<notaData> ns = new List<notaData>();
    //        notaData nota;

    //        List<SqlParameter> ParametersList = new List<SqlParameter>();

    //        ParametersList.Add(new SqlParameter("@idtercero", idProveedor));

    //        SqlDataReader dataReader = Conexion.ExcuteReader(sp, ParametersList);

    //        while (dataReader!=null && dataReader.Read())
    //        {
                
    //            nota = cargoNota(dataReader, true);
    //            ns.Add(nota);

    //        }
    //        if( dataReader!=null)
    //            dataReader.Close();




    //        return ns;
    //    }

    //    internal static List<notaData> getbyFecha(System.Data.SqlTypes.SqlDateTime desde, System.Data.SqlTypes.SqlDateTime hasta, Guid idlocal, string sp)
    //    {
    //         List<notaData> ns = new List<notaData>();
    //        notaData nota;

    //        List<SqlParameter> ParametersList = new List<SqlParameter>();

    //        ParametersList.Add(new SqlParameter("@desde", desde));
    //        ParametersList.Add(new SqlParameter("@hasta", hasta));
    //        ParametersList.Add(new SqlParameter("@idlocal", idlocal));

    //        SqlDataReader dataReader = Conexion.ExcuteReader(sp, ParametersList);

    //        while (dataReader!=null && dataReader.Read())
    //        {
                
    //            nota = cargoNota(dataReader, true);
    //            ns.Add(nota);

    //        }
    //        if( dataReader!=null)
    //            dataReader.Close();




    //        return ns;
    //    }
        
    }
}
