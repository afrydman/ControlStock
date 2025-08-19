namespace BusinessComponents
{
    public class remito
    {


        //public static List<remitoData> getByLocal(Guid idLocal)
        //{

        //    return getByLocal(idLocal, true);
        //}

        //public static List<remitoData> getByLocal(Guid idLocal, bool noSync, bool connLocal = true)
        //{//son getbylocaldestino
        //    List<DTO.BusinessEntities.remitoData> rs = remitoDataMapper.getByLocal(idLocal,connLocal);

            
        //    foreach (remitoData r in rs)
        //    {
        //        r.detalles = remitoDetalleDataMapper.getByRemito(r.ID);


        //        if (noSync)
        //        {
        //            //if (r.localOrigen != null)
        //            //    r.localOrigen = Local.getbyID(r.localOrigen.ID);
        //            //if (r.localDestino != null)
        //            //    r.localDestino = Local.getbyID(r.localDestino.ID);
        //        }

        //    }



        //    rs.Sort(delegate(remitoData x, remitoData y)
        //    {//inverso?
        //        return x.FechaGeneracion.CompareTo(y.FechaGeneracion);
        //    });


        //    return rs;

        //}


        //public static List<remitoData> getByLocalOrigen(Guid idLocal)
        //{

        //    return getByLocalOrigen(idLocal, true);
        //}
        //public static List<remitoData> getByLocalOrigen(Guid idLocal, bool noSync)
        //{
        //    List<DTO.BusinessEntities.remitoData> rs = remitoDataMapper.getByLocalOrigen(idLocal);

        //    foreach (remitoData r in rs)
        //    {
        //        r.detalles = remitoDetalleDataMapper.getByRemito(r.ID);
        //        //r.localDestino = Local.getbyID(r.localDestino.ID);
        //        //r.localOrigen = Local.getbyID(r.localOrigen.ID);
        //        if (noSync)
        //        {
        //            //if (r.localOrigen != null)
        //            //    r.localOrigen = Local.getbyID(r.localOrigen.ID);
        //            //if (r.localDestino != null)
        //            //    r.localDestino = Local.getbyID(r.localDestino.ID);
        //        }
        //    }
        //    rs.Sort(delegate(remitoData x, remitoData y)
        //    {//inverso?
        //        return x.FechaGeneracion.CompareTo(y.FechaGeneracion);
        //    });


        //    return rs;

        //}

        //public static List<remitoData> getByLocalSinRecibir(Guid idLocal)
        //{

        //    return getByLocalSinRecibir(idLocal, true);
        //}
        //public static List<remitoData> getByLocalSinRecibir(Guid idLocal, bool noSync,bool connLocal = true)
        //{


        //    return getByLocal(idLocal, noSync, connLocal).FindAll(
        //                                    delegate(remitoData r)
        //                                    {
        //                                        return r.FechaRecibo == DateTime.Parse("01/01/1800") && !r.anulado;
        //                                    }
        //                                   );
        //}

        //public static List<remitoData> getByLocalRecibido(Guid idLocal)
        //{

        //    return getByLocalRecibido(idLocal, true);
        //}

        //public static List<remitoData> getByLocalRecibido(Guid idLocal, bool noSync,bool connLocal = true)
        //{


        //    return getByLocal(idLocal, noSync,connLocal).FindAll(
        //                                    delegate(remitoData r)
        //                                    {
        //                                        return r.FechaRecibo != DateTime.Parse("01/01/1800"); ;
        //                                    }
        //                                   );
        //}



        //public static bool confirmarRecibo(Guid id, DateTime fecha, bool connLocal = true)
        //{
        //    return remitoDataMapper.confirmarRecibo(id, fecha, connLocal);
        //}
        //public static bool confirmarRecibo(Guid id)
        //{

        //    List<remitoDetalleData> ds = remitoDetalleDataMapper.getByRemito(id);
        //    bool task = false;
        //     var opts = new TransactionOptions
        //            {
        //                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        //            };

        //    using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
        //    {
               

        //        try
        //        {
        //            conexion.closeConecction(); //Para que se vuelva a abrir dentro de la trans!


        //            foreach (remitoDetalleData d in ds)
        //            {
        //             task=  stock.actualizarStock(d.codigoBarras, d.cantidad);
        //             if (!task)
        //                 break;
        //            }

        //            if (!task)
        //                return false;
        //            task = remitoDataMapper.confirmarRecibo(id);
        //            if(task)
        //                trans.Complete();
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //    }

        //    return task;

        //}
        //public static remitoData GetByID(Guid id)
        //{

        //    return GetByID(id, true);
        //}
        //public static remitoData GetByID(Guid id, bool noSync)
        //{
        //    remitoData r = remitoDataMapper.getbyId(id);

        //    r.detalles = remitoDetalleDataMapper.getByRemito(r.ID);


        //    //if (noSync)
        //    //{
        //    //    if (r.localOrigen != null)
        //    //        r.localOrigen = Local.getbyID(r.localOrigen.ID);
        //    //    if (r.localDestino != null)
        //    //        r.localDestino = Local.getbyID(r.localDestino.ID);
        //    //}

        //    return r;
        //}

        //public static bool generarNuevo(remitoData r,bool connLocal = true)
        //{
        //     bool task = false;

        //     var opts = new TransactionOptions
        //            {
        //                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        //            };

        //    using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
        //    {
        //        try
        //        {
        //            conexion.closeConecction(connLocal); //Para que se vuelva a abrir dentro de la trans!



        //            foreach (remitoDetalleData rd in r.detalles)
        //            {
        //                rd.IDRemito = r.ID;
        //                task = remitoDetalleDataMapper.insertar(rd, connLocal);
        //                if (!task)
        //                    break;
                        
        //            }

        //            if (!task)
        //                return false;
        //            task = remitoDataMapper.insertar(r, connLocal);
                    
        //            if (task)
        //                trans.Complete();
        //        }
        //        catch (Exception)
        //        {

        //            return false;
        //        }
               
        //    }
        //    return task;


        ////}
        //public static remitoData getLast()
        //{
        //    return getLast(helper.IDLocal, helper.firstNum);
        //}
        //public static remitoData getLast(Guid id, int Prefix,bool connLocal = true)
        //{//busca por idlocalO
        //    return remitoDataMapper.getLast(id, Prefix, connLocal);

        ////}
        //public static List<remitoData> getOlderThan(remitoData ultimoRemito)
        //{

        //    return getOlderThan(ultimoRemito, helper.IDLocal, helper.firstNum);
        //}

        //public static List<remitoData> getOlderThan(remitoData ultimoRemito, Guid idLocal, int Prefix)
        //{
        //    return getOlderThan(ultimoRemito, idLocal, true, Prefix);
        //}


        //public static List<remitoData> getOlderThan(remitoData ultimoRemito, Guid idLocal, bool noSync, int Prefix,bool connLocal =  true)
        //{
        //    List<remitoData> remitos = remitoDataMapper.getOlderThan(ultimoRemito.FechaGeneracion.AddSeconds(1), idLocal, Prefix, connLocal);

        //    foreach (remitoData r in remitos)
        //    {

        //        r.detalles = remitoDetalleDataMapper.getByRemito(r.ID);


        //        //if (noSync)
        //        //{
        //        //    if (r.localOrigen != null)
        //        //        r.localOrigen = Local.getbyID(r.localOrigen.ID);
        //        //    if (r.localDestino != null)
        //        //        r.localDestino = Local.getbyID(r.localDestino.ID);
        //        //}
        //    }


        //    return remitos;
        //}

        //public static List<remitoData> getCompras(DateTime dateTime)
        //{

        //    return getCompras(dateTime, true);
        //}

        //public static List<remitoData> getCompras(DateTime dateTime, bool noSync)
        //{
        //    List<remitoData> remitos = remitoDataMapper.getCompras(dateTime);

        //    foreach (remitoData r in remitos)
        //    {
        //        r.detalles = remitoDetalleDataMapper.getByRemito(r.ID);

        //        if (noSync)
        //        {
        //            if (r.localOrigen != null)
        //                r.localOrigen = Local.getbyID(r.localOrigen.ID);
        //            if (r.localDestino != null)
        //                r.localDestino = Local.getbyID(r.localDestino.ID);
        //        }
        //    }
        //    return remitos;
        //}

        //public static bool anular(Guid guid, bool actualizarStock, bool esAlta)
        //{

        //    List<remitoDetalleData> detallesAnular = remitoDetalleDataMapper.getByRemito(guid);

        //    bool task = false;
        //    if (actualizarStock)
        //    {
        //        try
        //        {
        //            var opts = new TransactionOptions
        //            {
        //                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        //            };

        //            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
        //            {
        //                conexion.closeConecction();//Para que se vuelva a abrir dentro de la trans!
                        
        //                foreach (remitoDetalleData r in detallesAnular)
        //                {
        //                    if (esAlta)
        //                    {
        //                        //si era un alta, resto stock, si era una baja , lo sumo
        //                        task=stock.actualizarStock(r.codigoBarras, -1 * r.cantidad);
        //                    }
        //                    else
        //                    {
        //                        task = stock.actualizarStock(r.codigoBarras, 1 * r.cantidad);
        //                    }
        //                    if (!task) return false;

        //                }
        //                task = remitoDataMapper.anular(guid);
        //                if(task) trans.Complete();
        //            }
        //        }
        //        catch (Exception)
        //        {

        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        task = remitoDataMapper.anular(guid);
        //    }

        //    return task;
        //}




        //public static List<remitoData> getAnuladosByOrigen(Guid guid)
        //{

        //    return getAnuladosByOrigen(guid, true);
        //}

        //public static List<remitoData> getAnuladosByOrigen(Guid guid, bool noSync)
        //{

        //    List<DTO.BusinessEntities.remitoData> rs = remitoDataMapper.getAnulados(guid);

        //    foreach (remitoData r in rs)
        //    {
        //        r.detalles = remitoDetalleDataMapper.getByRemito(r.ID);


        //        //if (noSync)
        //        //{
        //        //    if (r.localOrigen != null)
        //        //        r.localOrigen = Local.getbyID(r.localOrigen.ID);
        //        //    if (r.localDestino != null)
        //        //        r.localDestino = Local.getbyID(r.localDestino.ID);
        //        //}
        //    }


        //    return rs;
        //}
        //public static string getnumero()
        //{
        //    return getnumero(helper.IDLocal, helper.firstNum);
        //}
        //public static string getnumero(Guid id, int Prefix)
        //{
        //    //Guid aux = new Guid("C6AEE8F5-366A-4beb-87F3-3E5536F4F117");
        //    //id = aux;
        //    remitoData r = getLastNumero(id, Prefix);


        //    r.numeroRemito = r.numeroRemito += 1;

        //    //if (r == null || r.FechaGeneracion == DateTime.Parse("01/01/1800"))
        //    //{//no hay remitos  y este es el primero que creo
        //    //    return Local.getbyID(helper.IDLocal).cod + "-" + helper.firstNum.ToString("0000") + "-" + "00000001";
        //    //}

        //    return r.Show;
        //}

        //private static remitoData getLastNumero(Guid id, int Prefix)
        //{
        //    //remitoData r = getLast(id, Prefix);

        //    //r.localOrigen = Local.getbyID(r.localOrigen.ID);
        //    //r.localDestino = Local.getbyID(r.localDestino.ID);
        //    //return r;
        //    return null;
        //}

        //public static bool confirmarBaja(remitoData r)
        //{
        //    try
        //    {
        //        bool task = false;

        //        var opts = new TransactionOptions
        //        {
        //            IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        //        };

        //        using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
        //        {
        //            conexion.closeConecction(); //Para que se vuelva a abrir dentro de la trans!

        //            foreach (remitoDetalleData row in r.detalles)
        //            {

        //                task=stock.actualizarStock(row.codigoBarras, -1 * row.cantidad);
        //                if (!task) return false;
        //            }
        //            trans.Complete();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }
        //    return true;

        //}

        //public static remitoData getLastLocalRecibido(Guid idLocal, int Prefix,bool connLocal = true)
        //{
        //    return remitoDataMapper.getLastLocalRecibido(idLocal, Prefix,connLocal);
        //}
    }
}
