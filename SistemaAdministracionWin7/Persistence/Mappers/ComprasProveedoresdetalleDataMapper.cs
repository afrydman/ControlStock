namespace Persistence
{
    public class ComprasProveedoresdetalleDataMapper
    {
        //public static List<DTO.BusinessEntities.ComprasProveedoresdetalleData> getByCompra(Guid idCompra)
        //{

        //    List<ComprasProveedoresdetalleData> ds = new List<ComprasProveedoresdetalleData>();
        //    ComprasProveedoresdetalleData d;
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();
        //    ParametersList.Add(new SqlParameter("@id", idCompra));
            
        //    SqlDataReader dataReader = Conexion.ExcuteReader("dbo.comprasProveedores_detalle_getByCompra", ParametersList);
        //    while (dataReader != null && dataReader.Read())
        //    {

        //        d = CargoDetalle(dataReader);

        //        ds.Add(d);
        //    }
        //    dataReader.Close();

        //    return ds;

        //}



        //public static bool insertar(ComprasProveedoresdetalleData rd)
        //{
        //    List<SqlParameter> ParametersList = new List<SqlParameter>();

        //    ParametersList.Add(new SqlParameter("@idCompra", rd.padreID));
        //    ParametersList.Add(new SqlParameter("@codigoBarras", rd.codigo));
        //    ParametersList.Add(new SqlParameter("@cantidad", rd.cantidad));
        //    ParametersList.Add(new SqlParameter("@preciounidad", rd.precioUnidad));
        //    ParametersList.Add(new SqlParameter("@precioextra", rd.precioExtra));
        //    ParametersList.Add(new SqlParameter("@alicuota", rd.alicuota));

            

        //   return  Conexion.ExecuteNonQuery("dbo.comprasProveedores_detalle_Insert", ParametersList);
        //}

        //private static ComprasProveedoresdetalleData CargoDetalle(SqlDataReader dataReader)
        //{
        //    ComprasProveedoresdetalleData detalle = new ComprasProveedoresdetalleData();

        //    detalle.padreID = new Guid(dataReader["idCompra"].ToString());
        //    detalle.precioUnidad = Convert.ToDecimal(dataReader["preciounidad"].ToString());
        //    detalle.alicuota = Convert.ToDecimal(dataReader["alicuota"].ToString());
        //    detalle.codigo = dataReader["codigo"].ToString();
        //    detalle.cantidad = Convert.ToDecimal(dataReader["cantidad"].ToString());
        //    detalle.precioExtra = Convert.ToDecimal(dataReader["precioextra"].ToString());
        //    return detalle;
        //}

        
    }
}
