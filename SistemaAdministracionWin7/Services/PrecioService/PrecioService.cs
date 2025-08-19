using System;
using System.Collections.Generic;
using System.Data;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository;
using Repository.Repositories.PrecioRepository;

namespace Services.PrecioService
{
    public class PrecioService : IGenericRepository<ListaPrecioProductoTalleData>
    {

        //
        protected readonly IPrecioRepository _repo;
        public PrecioService(IPrecioRepository repo)
        {
            _repo = repo;
            
        }


        public PrecioService(bool local = true)
         {
             _repo = new PrecioRepository(local);
           
              
         }

        public bool Insert(ListaPrecioProductoTalleData theObject)
        {
            try
            {


                return _repo.Insert(theObject);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(theObject, "ListaPrecioProductoTalleData_Insert"), true, true);

                throw;

            }
        }

        public bool Update(ListaPrecioProductoTalleData theObject)
        {
            try
            {


                return _repo.Update(theObject);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(theObject, "ListaPrecioProductoTalleData_Update"), true, true);

                throw;

            }
        }

        public bool Disable(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public bool Enable(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public List<ListaPrecioProductoTalleData> GetAll()
        {
            try
            {
                return _repo.GetAll();
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                throw;

            }
            
        }

        public ListaPrecioProductoTalleData GetByID(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public ListaPrecioProductoTalleData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListaPrecioProductoTalleData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public decimal GetPrecio(Guid idLista, Guid idProductoTalle)
        {

            try
            {


                ListaPrecioProductoTalleData aux = _repo.GetPrecio(idLista, idProductoTalle);
                return aux.NotInDB ? 0 : aux.Precio;
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idProductoTalle, "ListaPrecioProductoTalleData_GetPrecio"), true, true);

                throw;

            }
        }

        public  bool InsertOrUpdate(ListaPrecioProductoTalleData precio)
        {

            try
            {
                ListaPrecioProductoTalleData aux = _repo.GetPrecio(precio.FatherID, precio.ProductoTalle.ID);

                if (!aux.NotInDB)
                    return Update(precio);

                return Insert(precio);
            
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(precio, "ListaPrecioProductoTalleData_InsertOrUpdate"), true, true);

                throw;

            }
           
        }
    }
}
