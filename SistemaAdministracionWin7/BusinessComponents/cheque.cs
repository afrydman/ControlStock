namespace BusinessComponents
{
    public static class cheque
    {
    //    public static int getnuevoInterno()
    //    {
    //        int ultimo = chequeDataMapper.obtenerUltimoInterno();
    //        if (ultimo==-1)
    //        {
    //            return 1;
    //        }
    //        else
    //        {
    //            return ++ultimo;
    //        }
    //    }

    //    public static bool insert(DTO.BusinessEntities.chequeData c)
    //    {
    //        if (c.ID==null || c.ID ==Guid.Empty)
    //        {
    //            c.ID = Guid.NewGuid();
    //        }
    //        return chequeDataMapper.insert(c);
    //    }
    //    public static List<DTO.BusinessEntities.chequeData> getByChequera(Guid idChequera, bool completo)
    //    {
    //        return getByChequera(idChequera, true, null, completo);
    //    }
    //    public static List<DTO.BusinessEntities.chequeData> getByChequera(Guid idChequera, bool onlyEnable, List<estadoCheque> estados, bool completo)
    //    {
    //        List<DTO.BusinessEntities.chequeData> aux = chequeDataMapper.getByChequera(idChequera);

    //        if (onlyEnable)
    //        {
    //            aux = aux.FindAll(delegate(chequeData cc) { return !cc.anulado; });
    //        }
    //        if (estados!=null)
    //        {
    //            aux = aux.FindAll(delegate(chequeData c) { return estados.Contains(c.estado); });
    //        }
    //        if (completo)
    //        {
    //            foreach (chequeData c in aux)
    //            {
    //                c.bancoEmisor = BusinessComponents.banco.getbyId(c.bancoEmisor.ID);
    //                c.chequera = BusinessComponents.chequera.getbyid(c.chequera.ID, true);
    //                //c.chequera.cuenta = BusinessComponents.cuenta.getbyId(c.chequera.cuenta.ID);
    //            }
    //        }

    //        aux.Sort(delegate(chequeData x, chequeData y)
    //        {
    //            return x.interno.CompareTo(y.interno);

    //        });

    //        return aux;
    //    }
    //    public static chequeData getbyId(Guid guid) {
    //        return getbyId(guid, false);
    //    }
    //    public static chequeData getbyId(Guid guid,bool complete)
    //    {
    //        chequeData aux  = chequeDataMapper.getbyId(guid);
    //        if (complete)
    //{
    //    aux.bancoEmisor = BusinessComponents.banco.getbyId(aux.bancoEmisor.ID);
    //    aux.chequera = BusinessComponents.chequera.getbyid(aux.chequera.ID, true);
    //}
    //        return aux;
    //    }

    //    public static bool marcarRechazado(chequeData _c,DateTime? fecha = null, string obs = "")
    //    {
    //        if (fecha==null)
    //        {
    //            fecha = DateTime.Now;
    //        }
    //        if (obs!= "")
    //        {
    //            _c.observaciones += "\n \n " + obs;
    //        }


    //        _c.estado = estadoCheque.Rechazado;
    //        _c.fecha_anulado_rechazado = fecha.Value;
    //        return update(_c);
    //    }

    //    public static bool update(chequeData _c)
    //    {
    //        return chequeDataMapper.update(_c);
    //    }

    //    public static bool marcarAnulado(chequeData _c,DateTime? fecha = null, string obs = "")
    //    {
    //        if (fecha == null)
    //        {
    //            fecha = DateTime.Now;
    //        }
    //        if (obs != "")
    //        {
    //            _c.observaciones += "\n \n " + obs;
    //        }
    //        _c.estado = estadoCheque.Anulado;
    //        _c.anulado = true;
    //        _c.fecha_anulado_rechazado = fecha.Value;
    //        return update(_c);
    //    }



    //    public static List<chequeData> getChequesTercero(bool onlyEnable, List<estadoCheque> estados) {
    //        return getChequesTercero(onlyEnable, estados, true);
    //    }

    //    public static List<chequeData> getChequesTercero(bool onlyEnable,List<estadoCheque> estados,bool completo)
    //    {
    //        List<chequeData> cheques = getByChequera(Guid.Empty, onlyEnable,estados,completo);



    //        cheques.Sort(delegate(chequeData x, chequeData y)
    //        {
                
    //            return x.interno.CompareTo(y.interno);

    //        });
    //        return cheques; 
    //    }



      
    //    public static List<chequeData> getChequesUtilizables(bool onlyEnable)
    //    {
    //        List<chequeraData> chequeras = BusinessComponents.chequera.getAll(true);
    //        List<estadoCheque> estados = new List<estadoCheque>();
            
    //        estados.Add(estadoCheque.En_Cartera);

    //        List<chequeData> cheques = getChequesTercero(onlyEnable,estados,true);

    //        estados.Add(estadoCheque.Creado);
    //        foreach (chequeraData chequera in chequeras)
    //        {
    //            cheques.AddRange(getByChequera(chequera.ID, true, estados, onlyEnable));
    //        }

    //        cheques.Sort(delegate(chequeData x, chequeData y)
    //        {
    //            return x.interno.CompareTo(y.interno);

    //        });
            
    //        return cheques;
           
    //    }

    //    public static bool internovalido(string interno)
    //    {
    //        List<chequeData> todos = chequeDataMapper.getAll();

    //        chequeData c = todos.Find(delegate(chequeData x) { return x.interno.ToString() == interno && !x.anulado; });


    //        return c == null;
    //    }

    //    public static bool marcarEntregado(chequeData _c,DateTime? fecha = null, string obs = "")
    //    {

    //        if (fecha == null)
    //        {
    //            fecha = DateTime.Now;
    //        }
    //        if (obs != "")
    //        {
    //            _c.observaciones += "\n \n " + obs;
    //        }
    //        _c.estado = estadoCheque.EntregadoSinOpago;
    //        _c.fecha_anulado_rechazado = fecha.Value;
    //        return update(_c);
    //    }
    }
}
