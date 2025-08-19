namespace BusinessComponents
{
    public static class ordePago
    {
        //public static string getNewNumero()
        //{

        //    return getNewNumero(helper.IDLocal, helper.firstNum);
        //}

        //public static string getNewNumero(Guid idlocal,int Prefix)
        //{

        //    reciboData ultimo = ordenPagoDataMapper.getLast(idlocal, Prefix);

        //    if (ultimo.ID == Guid.Empty)
        //    {
        //        ultimo.Numero = 1;
        //        ultimo.Prefix = 1;
        //    }
        //    ultimo.Numero++;

        //    return ultimo.Show;

        //}

        //public static bool Insert(DTO.BusinessEntities.reciboData r)
        //{
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

        //            foreach (reciboDetalleData det in r.detalles)
        //            {
        //                if (det.cheque.ID != Guid.Empty)
        //                {
        //                    //es un cheque de adeveras
        //                    chequeData c = BusinessComponents.cheque.getbyId(det.cheque.ID);
        //                    c.estado = estadoCheque.Entregado;

        //                    task = BusinessComponents.cheque.update(c);
        //                    if (!task)
        //                    {
        //                        return task;
        //                    }
        //                }
        //                task = ordenPagoDataMapper.insertDetalle(det);

        //                if (!task)
        //                {
        //                    return task;
        //                }

                       
        //            }
        //            task = ordenPagoDataMapper.insert(r);
        //            if (task)
        //                trans.Complete();
        //        }
        //        catch (Exception)
        //        {

        //            return task;
        //        }
        //    }
        //    return task;
        //}

        //public static List<reciboData> getbyCliente(Guid idCliente)
        //{
        //    List<reciboData> aux = getAll(true).FindAll(delegate(reciboData r) { return r.tercero.ID == idCliente; });

        //    aux.Sort(delegate(reciboData x, reciboData y)
        //    {
        //        return DateTime.Compare(x.Fecha, y.Fecha);

        //    });



        //    return aux;
        //}

        //public static List<reciboData> getAll()
        //{
        //    return getAll(true);
        //}
        //public static List<reciboData> getAll(bool conDetalles)
        //{

        //    List<reciboData> aux = ordenPagoDataMapper.getAll();

        //    if (conDetalles)
        //    {
        //        foreach (reciboData r in aux)
        //        {
        //            r.detalles = cargoDetalles(r.ID);
        //        }
        //    }

        //    return aux;

        //}

        //private static List<reciboDetalleData> cargoDetalles(Guid guid)
        //{
        //    return ordenPagoDataMapper.getDetalles(guid);
        //}

        //public static bool anular(reciboData opago)
        //{
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

        //            foreach (reciboDetalleData item in opago.detalles)
        //            {
        //                if (item.cheque.ID != Guid.Empty)
        //                {
        //                    item.cheque.estado = estadoCheque.En_Cartera;
        //                    task=BusinessComponents.cheque.update(item.cheque);
        //                    if (!task) return false;
        //                }
        //            }
        //            task = ordenPagoDataMapper.anular(opago.ID);
        //            if(task)
        //                trans.Complete();
        //        }
        //        catch (Exception)
        //        {

        //            return task;
        //        }
        //    }
        //    return task;
        //}

        //public static reciboData getById(Guid opago,bool completo)
        //{
        //    reciboData r = ordenPagoDataMapper.getByID(opago);
        //    r.detalles = ordenPagoDataMapper.getDetalles(opago);
        //    if (completo)
        //    {
        //        foreach (reciboDetalleData det in r.detalles)
        //        {
        //            if (det.cheque.ID!=Guid.Empty)
        //            {
        //                det.cheque = BusinessComponents.cheque.getbyId(det.cheque.ID);
        //            }
        //        }   
        //    }
        //    if (completo)
        //    {
        //        r.tercero =  new cliente().GetByID(r.tercero.ID);
        //        r.Local = BusinessComponents.Local.getbyID(r.Local.ID);
                
        //    }

        //    return r;
        //}

        //public static reciboData getOrdenQueEntregoCheque(Guid idcheque,bool getCompleto)
        //{
        //    reciboData  aux = ordenPagoDataMapper.getOrdenByCheque(idcheque);
        //    if (getCompleto)
        //    {
        //        aux.tercero = new proveedor().GetByID(aux.tercero.ID);
        //        aux.detalles = ordenPagoDataMapper.getDetalles(aux.ID);

        //    }

        //    return aux;
            
        //}
        //public static List<reciboData> getByFecha(DateTime fecha, bool completo)
        //{

        //    return getByFecha(fecha, completo, helper.IDLocal);
        //}

        //public static List<reciboData> getByFecha(DateTime fecha,bool completo,Guid idlocal)
        //{
        //    List<reciboData> aux = ordenPagoDataMapper.getbyFecha(fecha.Date, fecha.AddDays(1), idlocal);

        //    if (completo)
        //    {
        //        foreach (reciboData r in aux)
        //        {
        //            r.detalles = ordenPagoDataMapper.getDetalles(r.ID);
        //            r.tercero = new proveedor().GetByID(r.tercero.ID);
        //        }
        //    }
        //    return aux;
        //}
    }
}
