namespace BusinessComponents
{
    public class formaPago
    {

        //public static List<formaPagoData> getAll()
        //{

        //    return getAll(true);
        //}
        //public static List<formaPagoData> getAll( bool connLocal = true)
        //{
        //    List<formaPagoData> fpsAux = formaPagoDataMapper.getAll(connLocal);
        //    foreach (formaPagoData f in fpsAux)
        //    {
        //        if (f.credito)
        //        {
        //            f.cuotas = formaPagoCuotasDataMapper.getCuotas(f.ID);//cambiar
                    
        //        }

        //    }

        //    List<formaPagoData> fps = fpsAux.FindAll(delegate(formaPagoData f)
        //    {
        //        return !f.anulado;
        //    });

        //    return fps;



        //}
        //public static formaPagoData getbyID(Guid id)
        //{
          
        //        formaPagoData f = formaPagoDataMapper.getById(id);

        //    if (f.credito)
        //    {
        //        f.cuotas = formaPagoCuotasDataMapper.getCuotas(f.ID);
        //    }

        //    return f;

        //}
   


        //public static bool anular(Guid idfp)
        //{
        //    return formaPagoDataMapper.anular(idfp);
        //}

        //public static bool newFp(formaPagoData f, bool connLocal = true)
        //{
        //    bool tsk;
        //    var opts = new TransactionOptions
        //           {
        //               IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        //           };

        //    using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
        //    {
        //        try
        //        {
        //            conexion.closeConecction(connLocal); //Para que se vuelva a abrir dentro de la trans!

        //            if (f.ID == null || f.ID == new Guid())
        //            {
        //                f.ID = Guid.NewGuid();
        //                if (f.credito)
        //                {
        //                    List<Guid> newsids = new List<Guid>();

        //                    for (int i = 0; i < 12; i++)
        //                    {
        //                        newsids.Add(Guid.NewGuid());
        //                    }
                            
        //                }
        //            }
        //            if (f.credito)
        //            {
        //                tsk = formaPagoCuotasDataMapper.insertAumento(f, connLocal);
        //                if (!tsk)
        //                {
        //                    return false;
        //                }
        //            }
        //            tsk = formaPagoDataMapper.insert(f, connLocal);
        //            if (tsk) trans.Complete();
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //    }
        //    return tsk;

        //}

        //public static bool UpdateFp(formaPagoData f, bool connLocal = true)
        //{
        //    bool tsk;
        //    var opts = new TransactionOptions
        //           {
        //               IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        //           };

        //    using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
        //    {
        //        try
        //        {
        //            conexion.closeConecction(connLocal); //Para que se vuelva a abrir dentro de la trans!

        //            if (f.credito)
        //            {
        //                tsk = formaPagoCuotasDataMapper.updateAumento(f, connLocal);
        //                if (!tsk)
        //                {
        //                    return false;
        //                }
        //            }
        //            tsk = formaPagoDataMapper.update(f, connLocal);
        //            if (tsk) trans.Complete();
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //    }
        //    return tsk;
        //}
    }
}
