using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.ChequeRepository;
using Repository.Repositories.AdministracionRepository;
using Repository.Repositories.CuentaRepository;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.MovimientoRepository;


namespace Services.MovimientoCuentaService
{
    public class MovimientoCuentaService : IMovimientoCuentaService
    {
        protected readonly IMovimientoRepository _repo;
        public MovimientoCuentaService(IMovimientoRepository movimientoCuentaRepository)
        {
            _repo = movimientoCuentaRepository;
        }

        public MovimientoCuentaService(bool local = true)
        {
            _repo = new MovimientoRepository(local);
        }
        public bool Insert(MovimientoCuentaData movimiento)
        {

            double aux = 0;
            if (movimiento.ID == Guid.Empty)
                movimiento.ID = Guid.NewGuid();

            var chequeService = new ChequeService.ChequeService(new ChequeRepository());
            var cuentaService = new CuentaService.CuentaService(new CuentaRepository());

            var retiroService = new RetiroService.RetiroService(new RetiroRepository());
            var ingresoService = new IngresoService.IngresoService(new IngresoRepository());
            bool task = false;
            ChequeData c = null;
            var opts = new TransactionOptions
                  {
                      IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                  };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {


                switch (movimiento.TipoMovimiento)
                {
                    case MovimientoCuentaData.TipoMovimientoCuenta.CobroCheque:
                        c = chequeService.GetByID(movimiento.cheque.ID);

                        c.EstadoCheque = EstadoCheque.Depositado;

                        task = cuentaService.UpdateSaldo(movimiento.cuentaDestino.ID, movimiento.Monto);
                        if (task)
                        {
                            task = chequeService.Update(c);
                        }


                        break;

                    case MovimientoCuentaData.TipoMovimientoCuenta.PagoCheque:
                        c = chequeService.GetByID(movimiento.cheque.ID);

                        c.EstadoCheque = EstadoCheque.Acreditado;

                        task = cuentaService.UpdateSaldo(movimiento.cuentaOrigen.ID, -1 * (movimiento.Monto));
                        if (task)
                        {
                            task = chequeService.Update(c);
                        }


                        break;


                    case MovimientoCuentaData.TipoMovimientoCuenta.IngresoCajaChica:
                        IngresoData ingresoAux = new IngresoData();

                        IngresoData ingresoNuevo = new IngresoData();

                        ingresoNuevo.Date = DateTime.Now;
                        ingresoNuevo.fechaUso = DateTime.Now;
                        ingresoNuevo.Local.ID = movimiento.Local == null ? HelperService.IDLocal : movimiento.Local.ID;
                        ingresoNuevo.Enable = true;
                        ingresoNuevo.Monto = movimiento.Monto;
                        ingresoNuevo.ID = movimiento.ID;

                        ingresoNuevo.Personal.ID = Guid.Empty;
                        ingresoNuevo.Prefix = movimiento.Prefix;



                        ingresoAux = ingresoService.GetLast(movimiento.Local.ID, movimiento.Prefix);
                        ingresoNuevo.Numero = ++ingresoAux.Numero;
                        ingresoNuevo.Description = "Movimiento de: " + movimiento.cuentaOrigen.Show;
                        ingresoNuevo.TipoIngreso.ID = HelperService.idextraccioCuenta;

                        task = cuentaService.UpdateSaldo(movimiento.cuentaOrigen.ID, -1 * (movimiento.Monto));

                        if (task)
                        {
                            task = ingresoService.Insert(ingresoNuevo);
                        }



                        break;

                    case MovimientoCuentaData.TipoMovimientoCuenta.RetiroCajaChica:
                        RetiroData retiroAux = new RetiroData();
                        RetiroData retiroNuevo = new RetiroData();

                        retiroNuevo.Date = DateTime.Now;
                        retiroNuevo.fechaUso = DateTime.Now;
                        retiroNuevo.Local.ID = movimiento.Local == null ? HelperService.IDLocal : movimiento.Local.ID;
                        retiroNuevo.Enable = true;
                        retiroNuevo.Monto = movimiento.Monto;
                        retiroNuevo.ID = movimiento.ID;

                        retiroNuevo.Personal.ID = Guid.Empty;
                        retiroNuevo.Prefix = movimiento.Prefix;

                        retiroAux = retiroService.GetLast(movimiento.Local.ID, movimiento.Prefix);

                        retiroNuevo.Description = "Movimiento a: " + movimiento.cuentaDestino.Show;
                        retiroNuevo.TipoRetiro.ID = HelperService.iddepositoCuenta;
                        retiroNuevo.Numero = ++retiroAux.Numero;


                        task = cuentaService.UpdateSaldo(movimiento.cuentaDestino.ID, (movimiento.Monto));

                        if (task)
                        {
                            task = retiroService.Insert(retiroNuevo);
                        }



                        break;


                    case MovimientoCuentaData.TipoMovimientoCuenta.SinDefinir:
                        return false;
                        break;


                }
                try
                {
                    if (task) task = _repo.Insert(movimiento);

                    if (task) trans.Complete();
                }
                catch (Exception e)
                {
                    HelperService.WriteException(e);

                    HelperService.writeLog(
                                    ObjectDumperExtensions.DumpToString(movimiento, "Movimiento_insert"), true, true);

                    throw;
                }

            }
            return task;





            //if (movimiento.cheque.ID != Guid.Empty)
            //{//veo si es un pago o cobro de cheque y actualizo ese estado

            //    var opts = new TransactionOptions
            //      {
            //          IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            //      };

            //    using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            //    {


            //        //o es un deposito  de un cheque de otro a mi cuenta o estoy pagando un cheque mio.
            //        ChequeData c = chequeService.GetByID(movimiento.cheque.ID);

            //        if (c.Chequera.ID == Guid.Empty)
            //        {
            //            //es de tercero y lo estoy cobrando en una cuenta bancaria
            //            c.EstadoCheque = estadoCheque.Depositado;

            //            task = cuentaService.UpdateSaldo(movimiento.cuentaDestino.ID, movimiento.Monto);
            //        }
            //        else
            //        {
            //            //es mio  y lo estoy pagando desde mi cuenta bancaria
            //            c.EstadoCheque = estadoCheque.Acreditado;
            //            task = cuentaService.UpdateSaldo(movimiento.cuentaOrigen.ID, -1 * (movimiento.Monto));
            //        }
            //        if (task)
            //        {
            //            task = chequeService.Update(c);
            //        }
            //        if (task) trans.Complete();
            //    }
            //}
            //else
            //{
            //    //es una extraccion o deposito de efectivo

            //    var opts = new TransactionOptions
            //    {
            //        IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            //    };

            //    using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            //    {
            //        //bool task;
            //        var retiroService = new RetiroService.RetiroService(new RetiroRepository());
            //        var ingresoService = new IngresoService.IngresoService(new IngresoRepository());


            //        if (movimiento.cuentaOrigen != null && movimiento.cuentaOrigen.ID != Guid.Empty)
            //            movimiento.cuentaOrigen = cuentaService.GetByID(movimiento.cuentaOrigen.ID);

            //        if (movimiento.cuentaDestino != null && movimiento.cuentaDestino.ID != Guid.Empty)
            //            movimiento.cuentaDestino = cuentaService.GetByID(movimiento.cuentaDestino.ID);



            //        if (movimiento.cuentaDestino.ID != movimiento.cuentaOrigen.ID)
            //        {
            //            // no creacion

            //            if (movimiento.cuentaDestino.TipoCuenta == TipoCuenta.Otra)
            //            {
            //                //banco -> caja chica
            //                IngresoData ingresoAux = new IngresoData();

            //                IngresoData ingresoNuevo = new IngresoData();

            //                ingresoNuevo.Date = DateTime.Now;
            //                ingresoNuevo.fechaUso = DateTime.Now;
            //                ingresoNuevo.Local.ID = HelperService.IDLocal;
            //                ingresoNuevo.Enable = true;
            //                ingresoNuevo.Monto = movimiento.Monto;
            //                ingresoNuevo.ID = movimiento.ID;

            //                ingresoNuevo.Personal.ID = Guid.Empty;
            //                ingresoNuevo.Prefix = HelperService.Prefix;



            //                ingresoAux = ingresoService.GetLast(HelperService.IDLocal, HelperService.Prefix);
            //                ingresoNuevo.Numero = ++ingresoAux.Numero;
            //                ingresoNuevo.Description = "Movimiento de: " + movimiento.cuentaOrigen.Show;
            //                ingresoNuevo.TipoIngreso.ID = HelperService.idextraccioCuenta;

            //                cuentaService.UpdateSaldo(movimiento.cuentaOrigen.ID, -1 * (movimiento.Monto));
            //                ingresoService.Insert(ingresoNuevo);
            //            }
            //            else
            //            {
            //                //caja chica -> banco

            //                RetiroData retiroAux = new RetiroData();
            //                RetiroData retiroNuevo = new RetiroData();

            //                retiroNuevo.Date = DateTime.Now;
            //                retiroNuevo.fechaUso = DateTime.Now;
            //                retiroNuevo.Local.ID = HelperService.IDLocal;
            //                retiroNuevo.Enable = true;
            //                retiroNuevo.Monto = movimiento.Monto;
            //                retiroNuevo.ID = movimiento.ID;

            //                retiroNuevo.Personal.ID = Guid.Empty;
            //                retiroNuevo.Prefix = HelperService.Prefix;

            //                retiroAux = retiroService.GetLast(HelperService.IDLocal, HelperService.Prefix);
            //                cuentaService.UpdateSaldo(movimiento.cuentaDestino.ID, (movimiento.Monto));
            //                retiroNuevo.Description = "Movimiento a: " + movimiento.cuentaDestino.Show;
            //                retiroNuevo.TipoRetiro.ID = HelperService.iddepositoCuenta;
            //                retiroNuevo.Numero = ++retiroAux.Numero;
            //                retiroService.Insert(retiroNuevo);
            //            }
            //        }
            //    }

            //}



        }

        public string GetNextNumberAvailable(bool completo, Guid idLocal, int first)
        {
            MovimientoCuentaData aux = GetLast(idLocal, first);
            aux.Numero++;
            if (completo)
            {
                return aux.Show;
            }
            return aux.Numero.ToString();
        }

        public MovimientoCuentaData GetLast(Guid idlocal, int first)
        {
            try
            {


                return getPropertiesInfo(_repo.GetLast(idlocal, first));
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idlocal, "Movimiento_GetLast"), true, true);

                throw;

            }
        }

        public List<MovimientoCuentaData> NormalizeList(List<MovimientoCuentaData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(data => data.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            return list;
        }

        public MovimientoCuentaData getPropertiesInfo(MovimientoCuentaData m)
        {
            var cuentaService = new CuentaService.CuentaService(new CuentaRepository());
            var localService = new LocalService.LocalService(new LocalRepository());
            var bancoService = new BancoService.BancoService();
            var chequeraService = new ChequeraService.ChequeraService();
            var chequeService = new ChequeService.ChequeService();


            if (m.Local != null && m.Local.ID != Guid.Empty)
                m.Local = localService.GetByID(m.Local.ID);

            if (m.cheque != null && m.cheque.ID != Guid.Empty)
                m.cheque = chequeService.GetByID(m.cheque.ID);


            if (m.cuentaDestino != null && m.cuentaDestino.ID != Guid.Empty)
                m.cuentaDestino = cuentaService.GetByID(m.cuentaDestino.ID);

            if (m.cuentaOrigen != null && m.cuentaOrigen.ID != Guid.Empty)
                m.cuentaOrigen = cuentaService.GetByID(m.cuentaOrigen.ID);




            return m;
        }
        public MovimientoCuentaData GetById(Guid guid)
        {
            MovimientoCuentaData m = null;

            try
            {
                 m= _repo.GetByID(guid);

                return getPropertiesInfo(m);

            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(guid, "MovimientoCuentaService_GetByID"), true, true);

                throw;
            }

        }

        public bool Disable(Guid idMovimiento)
        {

            MovimientoCuentaData movimiento = GetById(idMovimiento);
            var chequeService = new ChequeService.ChequeService(new ChequeRepository());
            var ingresoService = new IngresoService.IngresoService();
            var retiroService = new RetiroService.RetiroService(new RetiroRepository());
            var cuentaService = new CuentaService.CuentaService(new CuentaRepository());
            bool task = false;
            ChequeData c = null;
            var opts = new TransactionOptions
                 {
                     IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                 };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                switch (movimiento.TipoMovimiento)
                {
                    case MovimientoCuentaData.TipoMovimientoCuenta.IngresoCajaChica:
                        task = ingresoService.Disable(idMovimiento);
                        if (task)
                            task = cuentaService.UpdateSaldo(movimiento.cuentaOrigen.ID, 1 * (movimiento.Monto));
                        //le devuelvo el monto a la cuenta origen.

                        break;

                    case MovimientoCuentaData.TipoMovimientoCuenta.RetiroCajaChica:

                        task = retiroService.Disable(idMovimiento);
                        if (task)
                            task = cuentaService.UpdateSaldo(movimiento.cuentaDestino.ID, -1 * (movimiento.Monto));
                        //le saco el monto a la cuenta destino.

                        break;



                    case MovimientoCuentaData.TipoMovimientoCuenta.CobroCheque:
                        c = chequeService.GetByID(movimiento.cheque.ID);
                        c.EstadoCheque = EstadoCheque.EnCartera;

                        task = cuentaService.UpdateSaldo(movimiento.cuentaDestino.ID, -1 * movimiento.Monto);
                        if (task)
                            task = chequeService.Update(c);



                        break;

                    case MovimientoCuentaData.TipoMovimientoCuenta.PagoCheque:
                        c = chequeService.GetByID(movimiento.cheque.ID);

                        c.EstadoCheque = EstadoCheque.Entregado;

                        task = cuentaService.UpdateSaldo(movimiento.cuentaOrigen.ID, (movimiento.Monto));
                        if (task)
                            task = chequeService.Update(c);
                        break;

                }

                try
                {
                    if (task) task = _repo.Disable(movimiento.ID);

                    if (task) trans.Complete();
                }
                catch (Exception e)
                {
                    HelperService.WriteException(e);

                    HelperService.writeLog(
                                    ObjectDumperExtensions.DumpToString(movimiento, "Movimiento_Disable"), true, true);

                    throw;

                }


            }


            return task;
        }




        public List<MovimientoCuentaData> GetbyCajaDestino(Guid idCajaDestino)
        {
            List<MovimientoCuentaData> aux = null;
            try
            {
                aux = _repo.GetbyCajaDestino(idCajaDestino);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idCajaDestino, "Movimiento_GetbyCajaDestino"), true, true);

                throw;
            }



            return NormalizeList(aux, false);
        }

        public List<MovimientoCuentaData> GetbyCajaOrigen(Guid idCajaOrigen)
        {
            List<MovimientoCuentaData> aux = null;


            try
            {
                aux = _repo.GetbyCajaOrigen(idCajaOrigen);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idCajaOrigen, "Movimiento_GetbyCajaOrigen"), true, true);

                throw;
            }



            return NormalizeList(aux, false);
        }


        public MovimientoCuentaData GetbyCheque(Guid idcheque)
        {
            MovimientoCuentaData aux = null;


            try
            {
                aux = _repo.GetbyCheque(idcheque);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idcheque, "Movimiento_GetbyCheque"), true, true);

                throw;
            }


            return getPropertiesInfo(aux);
        }
    }
}
