namespace BusinessComponents
{
    public static class notaCreditoClientes
    {
        //public static string obtenerNro(bool completo)
        //{
        //    notaData aux = getLast(helper.firstNum, helper.IDLocal);
        //    aux.Numero++;
        //    if (completo)
        //    {
        //        return aux.Show;
        //    }
        //    else
        //    {
        //        return aux.Numero.ToString();
        //    }
        //}

        //private static notaData getLast(int Prefix, Guid idlocal)
        //{
           
        //    return notaCreditoClientesDataMapper.getLast(Prefix, idlocal);
        //}

        //public static  bool insert(notaData mov)
        //{
        //      bool task = false;
        //     var opts = new TransactionOptions
        //            {
        //                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        //            };

        //    using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
        //    {
               

        //        try
        //        {
        //            conexion.closeConecction(); //Para que se vuelva a abrir dentro de la trans!

        //            foreach (notaDetalleData d in mov.detalles)
        //            {
        //                bool b = notaCreditoClientesDataMapper.insertDetalle(d);
        //                if (!b)
        //                {
        //                    return false;
        //                }
        //            }
        //            task = notaCreditoClientesDataMapper.insert(mov);
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

        //public static notaData getByID(Guid id, bool completo) {

        //    notaData n = notaCreditoClientesDataMapper.GetById(id, completo);

        //    if (completo)
        //    {
                
        //      //  n.Local = BusinessComponents.Local.getbyID(n.Local.ID);
                
        //        //n.tercero =  new cliente().GetByID(n.tercero.ID);
        //        n.vendedor = BusinessComponents.Personal.getPersonalbyId(n.vendedor.ID);
        //    }

        //    return n;
        
        //}

        //public static bool anular(Guid id)
        //{
        //    return notaCreditoClientesDataMapper.Anular(id);
        //}

        //public static List<notaData> getByTercero(Guid idCliente, bool completo)
        //{
        //    return notaCreditoClientesDataMapper.getbyTercero(idCliente, completo);
        //}

        //public static List<notaData> getbyfecha(System.Data.SqlTypes.SqlDateTime desde, System.Data.SqlTypes.SqlDateTime hasta, Guid idlocal, bool p)
        //{
        //    return notaCreditoClientesDataMapper.getbyFecha(desde, hasta, idlocal,p);
        //}
    }













    public static class notaDebitoClientes
    {
        //public static string obtenerNro(bool completo)
        //{
        //    notaData aux = getLast(helper.firstNum, helper.IDLocal);
        //    aux.Numero++;
        //    if (completo)
        //    {
        //        return aux.Show;
        //    }
        //    else
        //    {
        //        return aux.Numero.ToString();
        //    }
        //}

        //private static notaData getLast(int Prefix, Guid idlocal)
        //{

        //    return notaDebitoClientesDataMapper.getLast(Prefix, idlocal);
        //}

        //public static bool insert(notaData mov)
        //{
        //       bool task = false;
        //     var opts = new TransactionOptions
        //            {
        //                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        //            };

        //    using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
        //    {
               

        //        try
        //        {
        //            conexion.closeConecction(); //Para que se vuelva a abrir dentro de la trans!

        //            foreach (notaDetalleData d in mov.detalles)
        //            {
        //                bool b = notaDebitoClientesDataMapper.insertDetalle(d);
        //                if (!b)
        //                {
        //                    return false;
        //                }
        //            }
        //            task = notaDebitoClientesDataMapper.insert(mov);
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

        //public static notaData getByID(Guid id, bool completo)
        //{

        //    notaData n = notaDebitoClientesDataMapper.GetById(id, completo);

        //    if (completo)
        //    {
        //       // n.Local = BusinessComponents.Local.getbyID(n.Local.ID);
        //       // n.tercero =  new cliente().GetByID(n.tercero.ID);
        //        n.vendedor = BusinessComponents.Personal.getPersonalbyId(n.vendedor.ID);
        //    }

        //    return n;

        //}

        //public static bool anular(Guid id)
        //{
        //    return notaDebitoClientesDataMapper.Anular(id);
        //}

        //public static List<notaData> getByTercero(Guid idCliente, bool p)
        //{
        //    return notaDebitoClientesDataMapper.getbyTercero(idCliente,p);
        //}
    }
}
