namespace BusinessComponents
{
    public static class comprasProveedores
    {
        //public static bool nueva(DTO.BusinessEntities.comprasProveedoresData r)
        //{
        //    bool tsk;
        //    stockData s;
        //    bool task = false;
        //    var opts = new TransactionOptions
        //           {
        //               IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        //           };

        //    using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
        //    {
        //        try
        //        {
        //            conexion.closeConecction(); //Para que se vuelva a abrir dentro de la trans!
        //            foreach (ComprasProveedoresdetalleData item in r.detalles)
        //            {
        //                s = BusinessComponents.stock.obtenerProducto(item.codigo);

        //                //valido si existe ese stock, lo creo o lo updateo!
        //                if (BusinessComponents.stock.GetStockTotal(s) >= 0)
        //                {
        //                    tsk = BusinessComponents.stock.UpdateStock(s, item.cantidad);
        //                } 
        //                else
        //                {
        //                    tsk = BusinessComponents.stock.insertStock(s, item.cantidad);
        //                }
        //                if (!tsk)
        //                {
        //                    return tsk;
        //                }

        //                tsk = ComprasProveedoresdetalleDataMapper.insertar(item);
        //                if (!tsk)
        //                {
        //                    return tsk;
        //                }
        //            }
        //            tsk = comprasProveedoresDataMapper.insert(r);
        //            if (!tsk)
        //            {
        //                return tsk;
        //            }
        //            trans.Complete();
        //        }
        //        catch (Exception)
        //        {

        //            return false;
        //        }


        //    }
        //    return tsk;

        //}
        //public static List<comprasProveedoresData> getbyProveedor(Guid idProveedor)
        //{
        //    return getbyProveedor(idProveedor, false);

        //}
        //public static List<comprasProveedoresData> getbyProveedor(Guid idProveedor, bool enableonly)
        //{
        //    List<comprasProveedoresData> aux = comprasProveedoresDataMapper.getByProveedor(idProveedor);

        //    if (enableonly)
        //    {
        //        aux = aux.FindAll(delegate(comprasProveedoresData c) { return c.anulado == false; });
        //    }

        //    return aux;

        //}

        //public static comprasProveedoresData getByID(Guid aux)
        //{
        //    return comprasProveedoresDataMapper.getById(aux);
        //}

        //public static List<ComprasProveedoresdetalleData> getDetalles(Guid guid)
        //{
        //    return ComprasProveedoresdetalleDataMapper.getByCompra(guid);
        //}

        //public static bool anular(Guid guid)
        //{
        //    return anular(guid, false);
        //}
        //public static bool anular(Guid guid, bool updateStock)
        //{
        //    bool task = false;
        //    if (updateStock)
        //    {

        //        var opts = new TransactionOptions
        //               {
        //                   IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        //               };

        //        using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
        //        {



        //            try
        //            {
        //                conexion.closeConecction(); //Para que se vuelva a abrir dentro de la trans!

        //                comprasProveedoresData c = getByID(guid);

        //                List<ComprasProveedoresdetalleData> detalles = getDetalles(guid);

        //                foreach (ComprasProveedoresdetalleData item in detalles)
        //                {
        //                    task = stock.actualizarStock(item.codigo, item.cantidad * -1, c.Local.ID);
        //                    if (!task)
        //                        break;
        //                }

        //                task = comprasProveedoresDataMapper.anular(guid);
        //                if (task)
        //                    trans.Complete();
        //            }
        //            catch (Exception)
        //            {
        //                return false;

        //            }
        //        }
        //    }
        //    return task;
        ////}

        //public static List<comprasProveedoresData> getAll()
        //{
        //    List<comprasProveedoresData> list = comprasProveedoresDataMapper.getAll();

        //    foreach (comprasProveedoresData c in list)
        //    {
        //        c.detalles = getDetalles(c.ID);
        //    }
        //    return list;
        //}

        //public static comprasProveedoresData getlast(Guid idlocal, int Prefix)
        //{
        //    return comprasProveedoresDataMapper.getlast(idlocal, Prefix);
        //}

        //public static string getNumero(Guid idLocal, int myprefix, bool completo)
        //{
        //    comprasProveedoresData aux = getlast(idLocal, myprefix);
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


        //public static bool update(comprasProveedoresData compra)
        //{
        //    return comprasProveedoresDataMapper.update(compra);
        //}
    }
}
