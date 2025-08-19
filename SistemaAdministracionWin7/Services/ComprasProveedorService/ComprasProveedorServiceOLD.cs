using System;
using System.Collections.Generic;
using System.Transactions;
using DTO.BusinessEntities;
using Repository.ComprasProveedoresRepository;
using Repository.Repositories.ComprasProveedoresRepository;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.PersonalRepository;
using Repository.Repositories.ProveedorRepository;
using Repository.Repositories.StockRepository;

namespace Services.ComprasProveedorService
{
   public  class ComprasProveedorServiceOLD
    {
        protected readonly IComprasProveedoresRepository _repo;
        protected readonly ICompraProveedoresDetalleRepository _repoDetalle;
        public ComprasProveedorServiceOLD(IComprasProveedoresRepository repo, ICompraProveedoresDetalleRepository repoDet)
        {
            _repo = repo;
            _repoDetalle = repoDet;
        }

        public ComprasProveedorServiceOLD(bool local = true)
         {
             _repo = new ComprasProveedoresRepository(local);
            _repoDetalle = new CompraProveedoresDetalleRepository(local);
         }
        public List<ComprasProveedoresData> GetbyProveedor(Guid idProveedor, bool enableonly = true)
        {
            List<ComprasProveedoresData> aux = _repo.GetByProveedor(idProveedor);
            

            return NormalizeList(aux,enableonly);
        }

        public List<ComprasProveedoresData> GetAll(bool onlyEnable = true)
        {

            List<ComprasProveedoresData> aux = _repo.GetAll();

            return NormalizeList(aux, onlyEnable);

        }

        public string GetNextNumberAvailable(Guid idLocal, int myprefix, bool completo)
        {
            var aux =  GetLast(idLocal, myprefix);

            aux.Numero++;
            return completo ? aux.NumeroCompleto : aux.Numero.ToString();
        }

        public ComprasProveedoresData GetLast(Guid idlocal, int prefix)
        {
            return _repo.GetLast(idlocal, prefix);
        }

        public bool Enable(Guid idp)
        {
            throw new NotImplementedException();
        }

        public bool Disable(Guid idp, bool updateStock = true)
        {
             var stockService = new StockService.StockService(new StockRepository());
            bool task = false;
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
                        //conexion.closeConecction();//!todo! //Para que se vuelva a abrir dentro de la trans!

                        ComprasProveedoresData c = getPropertiesInfo(new ComprasProveedoresData(idp));

                        foreach (ComprasProveedoresdetalleData item in c.Children)
                        {
                            task = stockService.UpdateStock(item.codigo, item.cantidad * -1, c.Local.ID);//todo! ver ese -1
                            if (!task)
                                break;
                        }

                        task = _repo.Disable(idp);
                        if (task)
                            trans.Complete();
                    }
                    catch (Exception)
                    {
                        return false;

                    }
                    return true;
                }
            }
            return _repo.Disable(idp);
        }

        public bool Insert(ComprasProveedoresData theObject)
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

        public bool Update(ComprasProveedoresData p)
        {
            return _repo.Update(p);//para los comentarios 
        }

        public ComprasProveedoresData GetByID(Guid idp)
        {
            var aux =  _repo.GetByID(idp);
         
            return getPropertiesInfo(aux);
        }

        private List<ComprasProveedoresData> NormalizeList(List<ComprasProveedoresData> ps, bool onlyEnable)
        {
            if (onlyEnable)
                ps = ps.FindAll(data => data.Enable);

            ps.ForEach(n => n = getPropertiesInfo(n));


            ps.Sort((x, y) => x.Numero.CompareTo(y.Numero));

            return ps;
        }

        private ComprasProveedoresData getPropertiesInfo(ComprasProveedoresData compra)
        {

            var proveedoresService = new ProveedorService.ProveedorService(new ProveedorRepository());
            var localService = new LocalService.LocalService(new LocalRepository());
            var personalService = new PersonalService.PersonalService(new PersonalRepository());

            compra.Local = localService.GetByID(compra.Local.ID);
            compra.Proveedor = proveedoresService.GetByID((compra.Proveedor.ID));
            compra.vendedor = personalService.GetByID(compra.vendedor.ID);
            compra.Children = _repoDetalle.GetDetalles(compra.ID);
            

             return compra;
        }


    }
}
