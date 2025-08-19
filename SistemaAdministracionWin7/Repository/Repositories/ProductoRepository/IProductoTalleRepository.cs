using System;
using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Repository.Repositories.ProductoRepository
{
   public  interface IProductoTalleRepository
    {
         List<ProductoTalleData> GetByProducto(Guid idproducto);
         ProductoTalleData GetByProductoTalle(Guid idProducto, int talle);

        bool Insert(ProductoTalleData theObject );
    }
}
