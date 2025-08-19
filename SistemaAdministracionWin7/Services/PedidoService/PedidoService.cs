using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.ClienteRepository;
using Repository.Repositories.PedidoRepository;
using System.Transactions;
using Repository.Repositories.PersonalRepository;

namespace Services.PedidoService
{
    public class PedidoService : IGenericService<pedidoData>
    {
           protected readonly IPedidoRepository _repo;
        protected readonly IPedidoDetalleRepository _repoDetalle;
        public PedidoService(IPedidoRepository repo, IPedidoDetalleRepository repoDetalle)
        {
            _repo = repo;
            _repoDetalle = repoDetalle;
        }

        public PedidoService(bool local = true)
         {
             _repo = new PedidoRepository(local);
            _repoDetalle = new PedidoDetalleRepository(local);
              
         }
        public bool Insert(pedidoData theObject)
        {

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };
            Guid idPadre;
            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                try
                {

                    

                    idPadre = theObject.ID;
                    if (idPadre == new Guid())
                    {
                        idPadre = Guid.NewGuid();
                        theObject.ID = idPadre;
                    }

                    foreach (var det in theObject.Children)
                    {
                        det.FatherID = idPadre;
                        _repoDetalle.InsertDetalle(det);
                    }

                    _repo.Insert(theObject);
                    trans.Complete();
                }
                catch (Exception e)
                {
                    HelperService.WriteException(e);

                    HelperService.writeLog(ObjectDumperExtensions.DumpToString(theObject, "Pedido_Insert"), true, true);
                    return false;
                }

            }

            return true;



        }

        public bool Update(pedidoData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Disable(pedidoData theObject)
        {
            return _repo.Disable(theObject.ID);
        }

        public bool Enable(pedidoData theObject)
        {
            throw new NotImplementedException();
        }

        public List<pedidoData> GetAll(bool onlyEnable = true)
        {
            var aux = _repo.GetAll();


            return NormalizeList(aux);

        }

        public pedidoData GetByID(Guid idObject)
        {
            var aux = _repo.GetByID(idObject);

            return getPropertiesInfo(aux);
        }

        public string GetNextNumberAvailable(Guid idLocal, int myprefix, bool completo)
        {
            pedidoData aux = GetLast(idLocal, myprefix);
            aux.Numero++;
            if (completo)
            {
                return aux.NumeroCompleto;
            }
            return aux.Numero.ToString();

        }

        public pedidoData GetLast(Guid idLocal, int first)
        {
            var aux= _repo.GetLast(idLocal, first);

            

            return getPropertiesInfo(aux);
        }

        public List<pedidoData> NormalizeList(List<pedidoData> list, bool onlyEnable = true)
        {

            if (onlyEnable)
                list = list.FindAll(data => data.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            return list;
        }

        public pedidoData getPropertiesInfo(pedidoData aux)
        {
            var clienteService = new ClienteService.ClienteService(new ClienteRepository());
            var vendedorService = new PersonalService.PersonalService(new PersonalRepository());

            aux.Cliente = clienteService.GetByID(aux.Cliente.ID);
            aux.Vendedor = vendedorService.GetByID(aux.Vendedor.ID);
            aux.Children = _repoDetalle.GetDetalles(aux.ID);

            return aux;
        }

        public Type GetTypeRepo()
        {
            return _repo.GetType();
        }

        public  bool marcarComo(Guid guid, bool completo)
        {
            pedidoData aux = GetByID(guid);
            aux.completo = completo;

            return Update(aux);
        }
    }
}
