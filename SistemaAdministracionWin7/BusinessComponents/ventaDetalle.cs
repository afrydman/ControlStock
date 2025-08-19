namespace BusinessComponents
{
    public class ventaDetalle
    {
        //public static bool insertDetalle(List<DTO.BusinessEntities.ventaDetalleData> detalles, bool connLocal = true)
        //{
        //    bool tsk;
        //    foreach (ventaDetalleData vd in detalles)
        //    {
        //        tsk = insertDetalle(vd,connLocal);
        //        if (!tsk)
        //            return false;
        //    }
        //    return true;
        //}

        //public static bool insertDetalle(DTO.BusinessEntities.ventaDetalleData detalle, bool connLocal = true)
        //{
        //    return ventaDetalleDataMapper.insertDetalle(detalle,connLocal);
        //}

        //public static List<DTO.BusinessEntities.ventaDetalleData> getById(Guid id, bool connLocal = true)
        //{
        //    return ventaDetalleDataMapper.getbyID(id,connLocal);
        //}

        //public static List<DTO.BusinessEntities.ventaDetalleData> getbyCodigoInterno(string p)
        //{
        //    return getbyCodigoInterno(p, false);
        //}

        //public static List<DTO.BusinessEntities.ventaDetalleData> getbyCodigoInterno(string p,bool soloVenta)
        //{
        //    List<ventaDetalleData> lista = ventaDetalleDataMapper.getbyCodigoInterno(p);

        //    if (soloVenta)
        //    {//solo devuelvo los q tengan precio >0
        //        lista = lista.FindAll(delegate(ventaDetalleData v)
        //        {
        //            return v.precioUnidad > 0;
        //        });
               
        //    }
        //    return lista;
            
        //}
    }
}
