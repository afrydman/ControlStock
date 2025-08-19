namespace Persistence
{
    public class remitoDetalleDataMapper
    {
        //public static List<DTO.BusinessEntities.remitoDetalleData> getByRemito(Guid idRemito)
        //{

        //    List<remitoDetalleData> ds = new List<remitoDetalleData>();
        //    remitoDetalleData d;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idRemito));
            
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.remito_detalle_getByRemito", ParametersList);
        //    while (dataReader != null && dataReader.Read())
        //    {

        //        d = new remitoDetalleData();

        //        d.IDRemito = idRemito;
        //        d.codigoBarras = dataReader["codigoBarras"].ToString();
        //        d.cantidad = System.DBNull.Value != dataReader["cantidad"] ? Convert.ToDecimal(dataReader["cantidad"].ToString()) : -1;
                

        //        ds.Add(d);
        //    }
        //    dataReader.Close();

        //    return ds;

        //}

       

        //public static bool insertar(remitoDetalleData rd,bool connLocal = true)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

        //    ParametersList.Add(new SqlParameter("@idRemito", rd.IDRemito));
        //    ParametersList.Add(new SqlParameter("@codigoBarras", rd.codigoBarras));
        //    ParametersList.Add(new SqlParameter("@cantidad", rd.cantidad));


        //    return Conexion.ExecuteNonQuery("dbo.remito_detalle_Insert", ParametersList, true, connLocal);
        //}

        //public static List<remitoDetalleData> getbyProveedor(string codProveedor)
        //{
        //    List<remitoDetalleData> ds = new List<remitoDetalleData>();
        //    remitoDetalleData d;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@codProveedor", codProveedor+"%"));

        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.remito_detalle_getByProveedor", ParametersList);
        //    while (dataReader.Read())
        //    {

        //        d = new remitoDetalleData();

        //        d.IDRemito = new Guid(dataReader["idremito"].ToString());
        //        d.codigoBarras = dataReader["codigoBarras"].ToString();
        //        d.cantidad = System.DBNull.Value != dataReader["cantidad"] ? Convert.ToDecimal(dataReader["cantidad"].ToString()) : -1;

        //        ds.Add(d);
        //    }
        //    dataReader.Close();

        //    return ds;
        //}
    }
}
