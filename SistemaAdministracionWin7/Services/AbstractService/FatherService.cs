using System;
using System.Collections.Generic;
using System.Transactions;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository;

namespace Services.AbstractService
{
    public abstract class FatherService<T, B, X, Y> : ObjectService<T, X>, IGenericChildService<B>, IGetNextNumberAvailable<T>, IGenericServiceGetter<T>
        where T : DocumentoGeneralData<B>
        where B : ChildData
        where X : IGenericFatherRepository<T>
        where Y : IGenericChildRepository<B>
    {


        internal Y _repoDetalle;

        public FatherService(X repo, Y repoDetalle)
        {
            _repo = repo;
            _repoDetalle = repoDetalle;
        }

        public FatherService() { }


        public virtual bool InsertDetalle(B detalle)
        {
            try
            {
                return _repoDetalle.InsertDetalle(detalle);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);


                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(detalle, "FatherService_insertDetalle"), true, true);

                throw;

            }

        }

        public virtual List<B> GetDetalles(Guid idPadre)
        {
            try
            {
                return _repoDetalle.GetDetalles(idPadre);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idPadre, "FatherService_GetDetalles"), true, true);

                throw;

            }

        }



        public override bool Insert(T theObject)
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };
            Guid idPadre;
            var stockService = new StockService.StockService();
            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                try
                {
                    idPadre = theObject.ID;
                    if (idPadre == null || idPadre == new Guid())
                    {
                        idPadre = Guid.NewGuid();
                        theObject.ID = idPadre;
                    }

                    foreach (var det in theObject.Children)
                    {
                        det.FatherID = idPadre;
                        if (!InsertDetalle(det))
                            return false;
                    }

                    var theObjectVenta = theObject as VentaData;
                    var theObjectCompra = theObject as ComprasProveedoresData;

                    int i = 0;//verifico si es una venta o no para saber si al insertar tengo que sumar o restar stock

                    if (theObjectVenta == null && theObjectCompra != null)
                        i = 1;

                    else if (theObjectVenta != null && theObjectCompra == null)
                    {
                        i = -1;
                    }


                    //No lo debe de realizar: 
                    //Remito ( pq la creacion no significa movimiento de stock)
                    //Notas
                    //Punto Control

                    //Si deben.
                    //Venta
                    //CompraP


                    //todo! esto la puede cagar al realizar la sync?
                    if (theObjectVenta != null || theObjectCompra != null)
                    {
                        foreach (var item in theObject.Children)
                        {
                            IGetteableCodigoAndCantidad xxx = item as IGetteableCodigoAndCantidad;
                            if (xxx != null)
                            {
                                if (!stockService.UpdateStock(xxx.GetCodigo(), HelperService.ConvertToDecimalSeguro(xxx.GetCantidad()) * i, theObject.Local.ID, add: true))
                                    return false;
                            }
                        }
                    }


                    if (_repo.Insert(theObject))
                    {
                        trans.Complete();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    HelperService.WriteException(e);

                    HelperService.writeLog(ObjectDumperExtensions.DumpToString(theObject, "FatherService_Insert"), true, true);
                    return false;
                }

            }

            return false;
        }



        public override bool Disable(Guid id)
        {
            throw new NotImplementedException("A father has not call this method without say what to do with the stock");
        }
        public override bool Disable(T theObject)
        {
            throw new NotImplementedException("A father has not call this method without say what to do with the stock");
        }

        public virtual bool Disable(Guid idp, bool updateStock)
        {
            /*//Lo usa, 
                        * Ventas.
                        * Compras
                        * Remitos          
                        
             * */


            

            var stockService = new StockService.StockService();//??????!Todo!// esto la puede cagar al realizar la sync?


            bool task = false;
            T theObject = GetByID(idp);

            if (!theObject.Enable)
                return false;
            

            if (updateStock)
            {

                var opts = new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                };

                using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
                {
                    try
                    {
                        var theObjectVenta = theObject as VentaData;
                        var theObjectCompra = theObject as ComprasProveedoresData;
                        var theObjectRemito = theObject as RemitoData;


                        int i = 0;//verifico si es una venta o no para saber si al insertar tengo que sumar o restar stock


                        if (theObjectVenta == null && theObjectCompra != null)// OPuesto a Insert el valor de I
                            i = -1;

                        else if (theObjectVenta != null && theObjectCompra == null)
                        {
                            i = 1;
                        }
                        else
                        {//es un remito.

                            /* es una alta si:
                               local destino = idlocal


                               es una baja si:
                               local destino != idlocal


                               Si anulas una alta, se resta stock
                               si anulas una baja, se suma stock
                            */
                            bool alta = theObjectRemito.LocalDestino.ID == HelperService.IDLocal;

                            i = alta ? -1 : 1;

                        }

                        foreach (var item in theObject.Children)
                        {
                            var xxx = item as IGetteableCodigoAndCantidad;
                            if (xxx != null)
                            {
                                if (!stockService.UpdateStock(xxx.GetCodigo(), HelperService.ConvertToDecimalSeguro(xxx.GetCantidad()) * i, theObject.Local.ID))
                                    return false;
                            }
                        }

                        task = _repo.Disable(theObject.ID);
                        if (task)
                            trans.Complete();
                    }
                    catch (Exception e)
                    {
                        HelperService.WriteException(e);
                        HelperService.writeLog(ObjectDumperExtensions.DumpToString(idp, "FatherService_Disable"), true, true);

                        throw;
                    }
                    return true;
                }
            }
            return _repo.Disable(theObject.ID);
        }


        public override T GetLast(Guid idlocal, int prefix)
        //no necesito los detalles porque lo utiliza para saber el proximo Numero
        {//dejo el override para recordarlo
            try
            {
                return _repo.GetLast(idlocal, prefix);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idlocal, "FatherService_GetLast"), true, true);

                throw;

            }

        }

        public string GetNextNumberAvailable(Guid idLocal, int myprefix, bool completo)
        {
            var aux = GetLast(idLocal, myprefix);

            if (aux.ID == Guid.Empty)
            {
                aux.Numero = 1;
                aux.Prefix = HelperService.Prefix;
            }
            aux.Numero++;
            return completo ? aux.NumeroCompleto : aux.Numero.ToString();
        }


        public List<T> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int prefix, bool enableOnly = true)
        {



            try
            {
                List<T> auxList = _repo.GetByRangoFecha(fecha1.Date, fecha2.Date.AddDays(1), idLocal, prefix);


                return NormalizeList(auxList, enableOnly);

            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(fecha1, "FatherService_GetByRangoFecha"), true, true);

                throw;

            }
        }

        public List<T> GetByFecha(DateTime fecha1, Guid idLocal, int prefix, bool enableOnly = true)
        {



            try
            {
                List<T> auxList = _repo.GetByRangoFecha(fecha1.Date, fecha1.Date.AddDays(1), idLocal, prefix);


                return NormalizeList(auxList, enableOnly);

            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(fecha1, "FatherService_GetByFecha"), true, true);

                throw;

            }
        }

        public List<T> GetBiggerThan(int ultimo, Guid idLocal, int prefix, bool enableOnly = true)
        {
            try
            {
                List<T> auxList = _repo.GetBiggerThan(ultimo, idLocal, prefix);


                return NormalizeList(auxList, enableOnly);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(ultimo, "FatherService_GetBiggerThan"), true, true);

                throw;

            }

        }

        public List<T> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix, bool enableOnly = true)
        {
            try
            {
                List<T> auxList = _repo.GetOlderThan(ultimo, idLocal, prefix);


                return NormalizeList(auxList, enableOnly);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(ultimo, "FatherService_GetOlderThan"), true, true);

                throw;

            }

        }
    }
}
