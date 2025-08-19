using System;
using System.Collections.Generic;
using System.Transactions;

using DTO.BusinessEntities;
using Repository.ChequeRepository;
using Repository.ClienteRepository;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.PersonalRepository;
using Repository.Repositories.ReciboRepository;

namespace Services.ReciboService
{
    public class ReciboService_old :  IGenericService<ReciboData>
    {
        protected readonly IReciboDetalleRepository _repoDetalle;
        protected readonly IReciboRepository _repo;
        public ReciboService_old(IReciboRepository reciboRepository,IReciboDetalleRepository reciboDetalle)
        {
            _repo = reciboRepository;
            _repoDetalle = reciboDetalle;
        }

        public ReciboService_old(bool local = true)
         {
             _repo = new ReciboRepository(local);
             _repoDetalle = new ReciboDetalleRepository(local);
         }
        public string GetNextNumberAvailable(bool completo, Guid idLocal, int first)
        {
            ReciboData ultimo = GetLast(idLocal, first);

            if (ultimo.ID == Guid.Empty)
            {
                ultimo.Numero = 1;
                ultimo.prefix = 1;
            }
            ultimo.Numero++;

            return completo ? ultimo.NumeroCompleto : ultimo.Numero.ToString();
        }

        public bool Insert(ReciboData recibo)
        {

            bool task = false;


            var chequeService = new ChequeService.ChequeService(new ChequeRepository());

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                try
                {
                    //conexion.closeConecction();//!todo! //Para que se vuelva a abrir dentro de la trans!

                    foreach (ReciboOrdenPagoDetalleData det in recibo.Children)
                    {
                        if (det.cheque.ID != Guid.Empty)
                        {

                            ChequeData c = chequeService.GetByID(det.cheque.ID);
                            c.Estado = estadoCheque.En_Cartera;
                            task = chequeService.Update(c);

                            if (!task)
                                return task;
                            
                        }

                        task = _repoDetalle.InsertDetalle(det);

                        if (!task)
                            return task;

                    }
                    task = _repo.Insert(recibo);
                    if (task) trans.Complete();
                }
                catch (Exception)
                {
                    return task;

                }
            }
            return task;
        }

        public bool Update(ReciboData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Disable(ReciboData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Enable(ReciboData theObject)
        {
            throw new NotImplementedException();
        }

        public List<ReciboData> GetAll(bool onlyEnable = true)
        {
            throw new NotImplementedException();
        }

        public List<ReciboData> NormalizeList(List<ReciboData>  list,bool onlyEnable)
        {
            if (onlyEnable)
                list = list.FindAll(data => data.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

            return list;
        }

        public ReciboData getPropertiesInfo(ReciboData n)
        {
            var clienteService = new ClienteService.ClienteService(new ClienteRepository());
            var localService = new LocalService.LocalService(new LocalRepository());
            var personalService = new PersonalService.PersonalService(new PersonalRepository());
            var chequeService = new ChequeService.ChequeService(new ChequeRepository());


            n.tercero = clienteService.GetByID(n.tercero.ID);
            n.vendedor = personalService.GetByID(n.vendedor.ID);
            n.Local = localService.GetByID(n.Local.ID);

            n.Children = _repoDetalle.GetDetalles(n.ID);

            foreach (var det in n.Children)
            {
                if (det.cheque.ID != Guid.Empty)
                {
                    det.cheque = chequeService.GetByID(det.cheque.ID);
                }
            }
            return n;
        }

        public Type GetTypeRepo()
        {
            throw new NotImplementedException();
        }

        public List<ReciboData> GetbyCliente(Guid idCliente,bool enableOnly=true)
        {
            List<ReciboData> aux = GetAll().FindAll(r => r.tercero.ID == idCliente);

            return NormalizeList(aux, enableOnly);
        }

        public List<ReciboData> GetAll()
        {
            List<ReciboData> aux = _repo.GetAll();
            return NormalizeList(aux, false);

            
        }

        public ReciboData GetByID(Guid idObject)
        {
            return getPropertiesInfo(_repo.GetByID(idObject));
        }

        public ReciboData GetLast(Guid idLocal, int first)
        {
            return getPropertiesInfo(_repo.GetLast(idLocal, first));
        }

        public List<ReciboData> GetOlderThan(bool conDetalles, Guid idLocal, int first)
        {
            return _repo.GetOlderThan(GetLast(idLocal, first).Date, idLocal, first);
        }

        public bool Anular(ReciboData recibo)
        {
            var chequeService = new ChequeService.ChequeService(new ChequeRepository());

            bool task = false;

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                try
                {
                    //conexion.closeConecction();//!todo! //Para que se vuelva a abrir dentro de la trans!

                    foreach (ReciboOrdenPagoDetalleData item in recibo.Children)
                    {
                        if (item.cheque.ID != Guid.Empty)
                        {
                            ChequeData c = chequeService.GetByID(item.cheque.ID);
                            c.Estado = estadoCheque.Creado;
                            task = chequeService.Update(c);


                            if (!task) return false;
                        }
                    }

                    task = _repo.Disable(recibo.ID);
                    if (task) trans.Complete();
                }
                catch (Exception)
                {
                    return false;

                }
            }

            return task;
        }

        public ReciboData GetById(Guid opago, bool completo)
        {
            ReciboData aux = _repo.GetByID(opago);
           

            
            return getPropertiesInfo(aux);
        }

        public ReciboData GetReciboDeCheque(Guid idcheque)
        {
            List<ReciboOrdenPagoDetalleData> detalles = getPagosCheque();
            ReciboOrdenPagoDetalleData rr = detalles.Find(delegate(ReciboOrdenPagoDetalleData r) { return r.cheque.ID == idcheque; });

            return rr == null ? null : getPropertiesInfo(GetById(rr.FatherID, true));
        }

        public List<ReciboOrdenPagoDetalleData> getPagosCheque()
        {
            List<ReciboOrdenPagoDetalleData> detalles = getPagos();
            detalles = detalles.FindAll(delegate(ReciboOrdenPagoDetalleData r) { return r.cheque.ID != Guid.Empty; });

            return detalles;

        }

        private  List<ReciboOrdenPagoDetalleData> getPagos()
        {

            return _repoDetalle.GetAll();

        }

        public  List<ReciboData> getbyFecha(DateTime fecha, Guid idlocal,bool onlyEnable=true)
        {
            List<ReciboData> aux = _repo.GetByRangoFecha(fecha.Date.AddDays(-1), fecha.Date.AddHours(23), idlocal);


            return NormalizeList(aux, onlyEnable);
        }
    }
}
