namespace BusinessComponents
{
    public static class vale
    {
        //public static valeData getByID(Guid id) {

        //    return valeDataMapper.getByID(id);
        //}

        //public static bool insertarVale(valeData r,bool connLocal = true)
        //{

        //    Guid id;
        //    if (r.ID == new Guid())
        //    {
        //        id = Guid.NewGuid();
        //        r.ID = id;
        //    }
        //    else
        //    {
        //        id = r.ID;
        //    }

        //    return valeDataMapper.insert(r, connLocal);



        //}


        //public static List<valeData> getByFecha(SqlDateTime ayer, SqlDateTime manana, Guid idLocal, int Prefix, bool connLocal = true)
        //{

        //    List<valeData> auxList = valeDataMapper.getValeByFecha(idLocal, ayer, manana, Prefix, connLocal);

        //    return auxList;

        //}

        //public static int getLast(Guid idLocal,int Prefix,bool connLocal=true)
        //{
        //    return valeDataMapper.getLast(idLocal, Prefix, connLocal);
        //}




        //internal static bool verificaryanular(Guid guid)
        //{
        //    valeData v = valeDataMapper.getbyVenta(guid);
        //    return anular(v);
        //}

        //private static bool anular(valeData v)
        //{
        //    return anular(v.ID);
        //}

        //public static bool anular(Guid guid,bool connLocal = true)
        //{
        //    return valeDataMapper.anular(guid,connLocal);
        //}

        //public static List<valeData> getAnulados(Guid idlocal, int Prefix,bool connLocal = true)
        //{
        //    List<valeData> aux = getByFecha(SqlDateTime.MinValue, SqlDateTime.MaxValue, idlocal, Prefix, connLocal);

        //    aux = aux.FindAll(delegate(valeData v) { return v.anulado; });

        //    return aux;
        //}



        //public static List<valeData> getBigger(int ultima, Guid idlocal, int Prefix,bool connLocal = true)
        //{
        //    return valeDataMapper.getBigger(ultima, idlocal, Prefix, connLocal);
        //}
    }
}
