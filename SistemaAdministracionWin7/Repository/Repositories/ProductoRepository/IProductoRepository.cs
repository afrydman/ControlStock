using System;
using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Repository.Repositories.ProductoRepository
{
   public  interface IProductoRepository : IGenericRepository<ProductoData>
    {
       List<ProductoData> GetProductoByCodigoInterno(string codigoInternoProducto, bool uselike);
       List<ProductoData> GetProductosByProveedor(Guid idproveedor);
    }
}
