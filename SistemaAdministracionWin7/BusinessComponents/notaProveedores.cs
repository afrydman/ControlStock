namespace BusinessComponents
{
    public static class notaCreditoProveedores
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
           
        //    return notaCreditoProveedoresDataMapper.getLast(Prefix, idlocal);
        //}

        //public static  bool insert(notaData mov)
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

        //            foreach (notaDetalleData d in mov.detalles)
        //            {
        //                bool b = notaCreditoProveedoresDataMapper.insertDetalle(d);
        //                if (!b)
        //                {
        //                    return false;
        //                }
        //            }
        //            task = notaCreditoProveedoresDataMapper.insert(mov);
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

        //    notaData n = notaCreditoProveedoresDataMapper.GetById(id, completo);

        //    if (completo)
        //    {
        //       // n.Local = BusinessComponents.Local.getbyID(n.Local.ID);
        //       // n.tercero = new proveedor().GetByID(n.tercero.ID);
        //        n.vendedor = BusinessComponents.Personal.getPersonalbyId(n.vendedor.ID);
        //    }

        //    return n;
        
        //}

        //public static bool anular(Guid id)
        //{
        //    return notaCreditoProveedoresDataMapper.Anular(id);
        //}

        //public static List<notaData> getByTercero(Guid idProveedor,bool completo)
        //{
        //    return notaCreditoProveedoresDataMapper.getbyTercero(idProveedor, completo);
        //}

        //public static List<notaData> getbyfecha(System.Data.SqlTypes.SqlDateTime desde, System.Data.SqlTypes.SqlDateTime hasta, Guid idlocal, bool p)
        //{
        //    return notaCreditoProveedoresDataMapper.getbyfecha(desde, hasta, idlocal, p);
        //}
    }













    public static class notaDebitoProveedores
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

        //    return notaDebitoProveedoresDataMapper.getLast(Prefix, idlocal);
        //}

        //public static bool insert(notaData mov)
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
        //                bool b = notaDebitoProveedoresDataMapper.insertDetalle(d);
        //                if (!b)
        //                {
        //                    return false;
        //                }
        //            }
        //            task = notaDebitoProveedoresDataMapper.insert(mov);
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

        //public static notaData getByID(Guid id, bool completo)
        //{

        //    notaData n = notaDebitoProveedoresDataMapper.GetById(id, completo);

        //    if (completo)
        //    {
        //       //n.Local = BusinessComponents.Local.getbyID(n.Local.ID);
        //        //n.tercero = new proveedor().GetByID(n.tercero.ID);
        //        n.vendedor = BusinessComponents.Personal.getPersonalbyId(n.vendedor.ID);
        //    }

        //    return n;

        //}

        //public static bool anular(Guid id)
        //{
        //    return notaDebitoProveedoresDataMapper.Anular(id);
        //}

        //public static List<notaData> getByTercero(Guid idProveedor,bool completo)
        //{
        //    return notaDebitoProveedoresDataMapper.getbyTercero(idProveedor, completo);
        //}
    }
}
