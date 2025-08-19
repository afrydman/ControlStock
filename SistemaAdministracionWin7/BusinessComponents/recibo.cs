namespace BusinessComponents
{
    public static class recibo
    {
        //public static string getNewNumero()
        //{
        //    return getNewNumero(helper.IDLocal, helper.firstNum);
        //}

        //public static string getNewNumero(Guid idlocal, int Prefix)
        //{

        //    reciboData ultimo = reciboDataMapper.getLast(idlocal, Prefix);

        //    if (ultimo.ID == Guid.Empty)
        //    {
        //        ultimo.Numero = 1;
        //        ultimo.Prefix = helper.firstNum;
        //    }
        //    ultimo.Numero++;

        //    return ultimo.Show;
        //}

        //public static bool Insert(DTO.BusinessEntities.reciboData r)
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
        //            conexion.closeConecction(); //Para que se vuelva a abrir dentro de la trans!

        //            foreach (reciboDetalleData det in r.detalles)
        //            {
        //                if (det.cheque.ID != Guid.Empty)
        //                {
        //                    //es un cheque de adeveras
        //                    //chequeData c = BusinessComponents.cheque.getbyId(det.cheque.ID);
        //                    //c.estado = estadoCheque.En_Cartera;

        //                    //task = BusinessComponents.cheque.update(c);
        //                    if (!task)
        //                    {
        //                        return task;
        //                    }
        //                }

        //                task = reciboDataMapper.insertDetalle(det);

        //                if (!task)
        //                {
        //                    return task;
        //                }
        //            }
        //            task = reciboDataMapper.insert(r);
        //            if (task) trans.Complete();
                    


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
        //    List<reciboData> aux = getAll(true).FindAll(delegate(reciboData r ){return r.tercero.ID==idCliente;});
            
        //    aux.Sort(delegate(reciboData x, reciboData y)
        //    {
        //        return DateTime.Compare(x.Fecha, y.Fecha);

        //    });

           

        //    return aux;
        //}

        //public static List<reciboData> getAll() {
        //    return getAll(true);
        //}
        //public static List<reciboData> getAll(bool conDetalles) {

        //     List<reciboData> aux = reciboDataMapper.getAll();

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
        //    return reciboDataMapper.getDetalles(guid);
        //}

        //public static reciboData getbyId(Guid recibo,bool completo)
        //{
        //    reciboData r = reciboDataMapper.getByID(recibo);
        //    r.detalles = reciboDataMapper.getDetalles(recibo);
        //    if (completo)
        //    {
        //        foreach (reciboDetalleData det in r.detalles)
        //        {
        //            if (det.cheque.ID != Guid.Empty)
        //            {
        //                //det.cheque = BusinessComponents.cheque.getbyId(det.cheque.ID);
        //            }
        //        }
        //    }
        //    if (completo)
        //    {
        //        //r.tercero =  new cliente().GetByID(r.tercero.ID);
        //        //r.Local = BusinessComponents.Local.getbyID(r.Local.ID);

        //    }

        //    return r;
        //}

        //public static bool anular(reciboData _recibo)
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
        //            conexion.closeConecction(); //Para que se vuelva a abrir dentro de la trans!

        //            foreach (reciboDetalleData item in _recibo.detalles)
        //            {
        //                if (item.cheque.ID != Guid.Empty)
        //                {
        //                    item.cheque.estado = estadoCheque.Creado;
        //                    //task=BusinessComponents.cheque.update(item.cheque);

        //                    if (!task) return false;
        //                }
        //            }

        //            task = reciboDataMapper.anular(_recibo.ID);
        //            if(task) trans.Complete();
        //        }
        //        catch (Exception)
        //        {
        //            return false;
                    
        //        }
        //    }

        //    return task;
        //}


        //public static reciboData getReciboDeCheque(Guid idcheque)
        //{
        //    List<reciboDetalleData> detalles = getPagosCheque();
        //    reciboDetalleData rr = detalles.Find(delegate(reciboDetalleData r) { return r.cheque.ID == idcheque; });
            
        //    return rr==null?null:getbyId(rr.IDRecibo,true);
            
        //}

        //private static List<reciboDetalleData> getPagosCheque()
        //{
        //    List<reciboDetalleData> detalles = getPagos();
        //    detalles = detalles.FindAll(delegate(reciboDetalleData r) { return r.cheque.ID != Guid.Empty; });

        //    return detalles;

        //}

        //private static List<reciboDetalleData> getPagos()
        //{
        //    return reciboDataMapper.getAllDetalles();
        //}

        //public static List<reciboData> getbyFecha(DateTime fecha, bool completo)
        //{
        //    return getbyFecha(fecha, completo, helper.IDLocal);
        //}
        //public static List<reciboData> getbyFecha(DateTime fecha, bool completo,Guid idlocal)
        //{
        //    List<reciboData> aux = reciboDataMapper.getbyFecha(fecha.Date, fecha.AddDays(1),idlocal);


        //    if (completo)
        //    {
        //        foreach (reciboData r in aux)
        //        {
        //            r.detalles = reciboDataMapper.getDetalles(r.ID);
        //            //r.tercero =  new cliente().GetByID(r.tercero.ID);
        //        }
        //    }

        //    return aux;
        //}
    }
}
