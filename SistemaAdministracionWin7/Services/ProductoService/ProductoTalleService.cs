using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.Repositories.ProductoRepository;

namespace Services.ProductoService
{
    public class ProductoTalleService
    {
        protected readonly IProductoTalleRepository _repo;
        public ProductoTalleService(IProductoTalleRepository repo)
        {
            _repo = repo;
        }
        public ProductoTalleService(bool local = true)
        {
            _repo = new ProductoTalleRepository(local);

        }

        public List<ProductoTalleData> GetByProducto(Guid idproducto)
        {
            try
            {
                return _repo.GetByProducto(idproducto);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idproducto, "ProductoTalleService_GetByProducto"), true, true);

                throw;

            }


        }

        public ProductoTalleData GetByProductoTalle(Guid idProducto, int talle)
        {
            try
            {
                return _repo.GetByProductoTalle(idProducto, talle);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(idProducto, "ProductoTalleService_GetByProductoTalle"), true, true);
                HelperService.writeLog(ObjectDumperExtensions.DumpToString(talle, "ProductoTalleService_GetByProductoTalle"), true, true);

                throw;

            }
            
        }

        public Guid GetIDByProductoTalle(ProductoTalleData objecto)
        {
            try
            {
                return GetIDByProductoTalle(objecto.IDProducto, objecto.Talle);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(objecto, "ProductoTalleService_GetIDByProductoTalle"), true, true);


                throw;

            }
            
        }

        public Guid GetIDByProductoTalle(Guid idProducto, int talle)
        {
            return (GetByProductoTalle(idProducto, talle)).ID;
        }

        public bool Insert(ProductoTalleData theObject)
        {
            try
            {
                if (theObject.ID == null || theObject.ID == new Guid())
                {
                    theObject.ID = Guid.NewGuid();
                }
                return _repo.Insert(theObject);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(theObject, "ProductoTalleService_Insert"), true, true);


                throw;

            }
            
        }
    }
}
