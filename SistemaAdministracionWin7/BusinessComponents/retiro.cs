namespace BusinessComponents
{
    public class retiro
    {
      //  internal static decimal getRetirosbyFecha_AUX(Guid idLocal, DateTime ayer, DateTime manana)
        //{
        //    List<retiroData> retiros = retiroDataMapper.getRetirosByFecha(idLocal, ayer, manana);
        //    decimal Monto = 0;
        //    foreach (retiroData r in  retiros)
        //    {
        //        if (r.fechaUso!=null)
        //            Monto += r.Monto;
        //    }
        //    return Monto;
        //}

        ////public static decimal obtenerRetiro(Guid idvendedor, Guid idLocal, string codigo)
        //{
        //    retiroData r = retiroDataMapper.getRetiro(idvendedor, idLocal, codigo);

        //    if (r.Monto>0 && r.fechaUso!=null)
        //    {
        //        retiroEfectuado(codigo);
        //    }

        //    return r.Monto;
        //}

        //public static bool retiroEfectuado(string codigo) 
        //{
        //    return retiroDataMapper.retiroEfectuado(codigo);
        //}

        //public static bool insertarRetiro(retiroData r, bool connLocal = true) 
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

        //    return retiroDataMapper.insert(r, connLocal);


        
        //}


        //public static  List<retiroData> getByFecha(DateTime ayer, DateTime manana, Guid idLocal)
        //{
            
        //    List<retiroData> auxList = retiroDataMapper.getRetirosByFecha(idLocal, ayer, manana);

        //    foreach (retiroData aux in auxList)
        //    {
        //        aux.Personal = Personal.getPersonalbyId(aux.Personal.ID);
        //        aux.tipoRetiro= tipoRetiro.getbyId(aux.tipoRetiro.ID);
        //     //   aux.Local = Local.getbyID(aux.Local.ID);
        //    }
        //    return auxList;
            
        //}

        //public static retiroData getLast(Guid idLocal, int Prefix, bool connLocal = true)
        //{
        //    retiroData aux = retiroDataMapper.getLast(idLocal, Prefix, connLocal);

        //    if (aux.Prefix==0)
        //    {
        //        aux.Prefix = helper.firstNum;
        //    }
        //    return aux;
        //}
        //public static retiroData getLast()
        //{
        //    return retiroDataMapper.getLast(helper.IDLocal,helper.firstNum);
        //}

        //public static List<retiroData> getOlderThan(retiroData ultimoRetiro,Guid idLocal,int Prefix)
        //{
        //    return retiroDataMapper.getOlderThan(ultimoRetiro.fecha.AddSeconds(1), idLocal, Prefix);
        //}
        //public static List<retiroData> getOlderThan(retiroData ultimoRetiro)
        //{
        //    return retiroDataMapper.getOlderThan(ultimoRetiro.fecha.AddSeconds(1),helper.IDLocal,helper.firstNum);
        //}

        //public static bool delete(Guid id, bool connLocal = true)
        //{
        //    return retiroDataMapper.delete(id, connLocal);
        //}

        //public static List<retiroData> getModified()
        //{
        //    return retiroDataMapper.getmodiffied(helper.IDLocal,helper.firstNum);
        //}

        //public static List<retiroData> getModified(Guid idLocal, int Prefix, bool connLocal = true)
        //{
        //    return retiroDataMapper.getmodiffied(idLocal, Prefix, connLocal);
        //}

        //public static bool yaviqueestabasmodificadamacho()
        //{
        //    return yaviqueestabasmodificadamacho(helper.IDLocal, helper.firstNum);
        //}
        //public static bool yaviqueestabasmodificadamacho(Guid idLocal, int Prefix, bool connLocal = true)
        //{
        //    return retiroDataMapper.yaviqueestabasmodificadamacho(idLocal, Prefix, connLocal);
        //}

        //public static List<retiroData> getOlderThan(DateTime dateTime)
        //{
        //    return retiroDataMapper.getOlderThan(dateTime.AddSeconds(1), helper.IDLocal,helper.firstNum);
        //}
        //public static List<retiroData> getOlderThan(DateTime dateTime, Guid idLocal, int Prefix, bool connLocal = true)
        //{
        //    return retiroDataMapper.getOlderThan(dateTime.AddSeconds(1), idLocal,Prefix,connLocal);
        //}
    }
}
