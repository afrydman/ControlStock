using System;
using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Repository.ComprasProveedoresRepository
{
    public interface IComprasProveedoresRepository : IGenericFatherRepository<ComprasProveedoresData>
    {

        List<ComprasProveedoresData> GetByProveedor(Guid idProveedor);
    }
}
