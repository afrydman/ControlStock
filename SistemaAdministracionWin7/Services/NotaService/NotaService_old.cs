using System;
using System.Collections.Generic;
using System.Transactions;

using DTO.BusinessEntities;
using Repository.ClienteRepository;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.NotasRepository;
using Repository.Repositories.PersonalRepository;
using Repository.Repositories.ProveedorRepository;

namespace Services.NotaService.Cliente.Credito
{
   public class NotaService_old : IGenericService<NotaData>
    {
        //ObjectService<chequeData, IChequeRepository>
        protected readonly INotaRepository _repo;
        protected readonly INotaDetalleRepository _repoDetalle;
        public NotaService_old(INotaRepository repo, INotaDetalleRepository repoDetalle)
        {
            _repo = repo;
            _repoDetalle = repoDetalle;
        }


        public bool Insert(NotaData theObject)
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
                    //conexion.closeConecction();//!todo! //Para que se vuelva a abrir dentro de la trans! //todo! que cierre la conexion correspondiente!

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
                    //todo! agregar log
                    return false;
                }

            }

            return true;
        }

        public bool Update(NotaData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Disable(NotaData theObject)
        {
            return _repo.Disable(theObject.ID);
        }

        public bool Enable(NotaData theObject)
        {
            throw new NotImplementedException();
        }

    

       public List<NotaData> GetAll(bool onlyEnable = true)
        {
            var aux = _repo.GetAll();

            return NormalizeList(aux,onlyEnable);
        }

       

        public NotaData GetByID(Guid idObject)
        {
            
            var aux = _repo.GetByID(idObject);

            aux = getPropertiesInfo(aux);
            
            return aux;
        }

        public NotaData GetLast(Guid idLocal, int first)
        {

            var aux = _repo.GetLast(idLocal, first);
            aux = getPropertiesInfo(aux);

            return aux;
        }

       public List<NotaData> NormalizeList(List<NotaData> aux, bool onlyEnable = true)
       {
           if (onlyEnable)
               aux = aux.FindAll(n => n.Enable);


           
            aux.ForEach(n => n = getPropertiesInfo(n));

            aux.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

           return aux;

       }

       public NotaData getPropertiesInfo(NotaData aux)
       {
           var localService = new LocalService.LocalService(new LocalRepository());
           var personalService = new PersonalService.PersonalService(new PersonalRepository());
           var clienteService = new ClienteService.ClienteService(new ClienteRepository());
           var proveedorService = new ProveedorService.ProveedorService(new ProveedorRepository());


           aux.Children = _repoDetalle.GetDetalles(aux.ID);

           aux.vendedor = personalService.GetByID(aux.vendedor.ID);
           aux.Local = localService.GetByID(aux.Local.ID);

           if (aux.tipo == tipoNota.CreditoCliente || aux.tipo == tipoNota.DebitoCliente)
           {
               aux.tercero = clienteService.GetByID(aux.tercero.ID);
           }
           else
           {
               aux.tercero = proveedorService.GetByID(aux.tercero.ID);
           }







           return aux;
       }

       public Type GetTypeRepo()
       {
           throw new NotImplementedException();
       }

       public List<NotaData> GetByTercero(Guid idCliente, bool completo, bool onlyEnable = true)
        {
            List<NotaData> aux = _repo.GetbyTercero(idCliente);
         
         

            return NormalizeList(aux,onlyEnable);
        }

        public string GetNextNumberAvailable(bool completo)
        {
            NotaData aux = GetLast(HelperService.IDLocal, HelperService.Prefix);
            aux.Numero++;
            if (completo)
            {
                return aux.NumeroCompleto;
            }
            else
            {
                return aux.Numero.ToString();
            }
        }
    }
}
