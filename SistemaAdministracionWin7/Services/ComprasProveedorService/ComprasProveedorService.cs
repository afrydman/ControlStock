using System;
using System.Collections.Generic;
using System.Transactions;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository;
using Repository.ComprasProveedoresRepository;
using Repository.Repositories.ComprasProveedoresRepository;
using Repository.Repositories.PagosRepository;
using Repository.Repositories.VentaRepository;
using Services.AbstractService;
using Services.TributoService;

namespace Services.ComprasProveedorService
{
    public class ComprasProveedorService : FatherService<ComprasProveedoresData, ComprasProveedoresdetalleData, IComprasProveedoresRepository, IGenericChildRepository<ComprasProveedoresdetalleData>>
   {
        public ComprasProveedorService(IComprasProveedoresRepository repo, ICompraProveedoresDetalleRepository repoDet):base(repo,repoDet)
        {
            _repo = repo;
            _repoDetalle = repoDet;
        }

        public ComprasProveedorService(bool local = true)
            : base()
         {
             _repo = new ComprasProveedoresRepository(local);
            _repoDetalle = new CompraProveedoresDetalleRepository(local);
         }
        public List<ComprasProveedoresData> GetbyProveedor(Guid idProveedor, bool enableonly = true)
        {
            
            List<ComprasProveedoresData> aux = _repo.GetByProveedor(idProveedor);
            
             return NormalizeList(aux,enableonly);
        }


        public override bool Insert(ComprasProveedoresData nuevaCompra)
        {

          
            var TributoNexoService = new TributoNexoService(new TributoCompraProveedoresRepository());

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                try
                {
                    if (!base.Insert(nuevaCompra))
                        return false;

                    foreach (TributoNexoData f in nuevaCompra.Tributos)
                    {
                        f.FatherID = nuevaCompra.ID;
                        if (!TributoNexoService.InsertDetalle(f)) return false;
                    }


                    trans.Complete();

                }
                catch (Exception e)
                {

                    HelperService.WriteException(e);

                    HelperService.writeLog(
                                    ObjectDumperExtensions.DumpToString(nuevaCompra, "Compra_Insert"), true, true);


                    throw;

                }
            }
            return true;
        }

        public override List<ComprasProveedoresData> NormalizeList(List<ComprasProveedoresData> ps, bool onlyEnable)
        {
            if (onlyEnable)
                ps = ps.FindAll(data => data.Enable);

            ps.ForEach(n => n = getPropertiesInfo(n));


            ps.Sort((x, y) => x.Numero.CompareTo(y.Numero));

            return ps;
        }

        public  override ComprasProveedoresData getPropertiesInfo(ComprasProveedoresData compra)
        {

            var tributoService = new TributoService.TributoNexoService(new TributoCompraProveedoresRepository());


            //compra.Tributos = tributoService.GetDetalles(compra.ID);
            compra.Children = _repoDetalle.GetDetalles(compra.ID);

            return compra;
        }


    }
}
