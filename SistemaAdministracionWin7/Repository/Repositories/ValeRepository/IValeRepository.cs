using System;
using DTO.BusinessEntities;

namespace Repository.Repositories.ValeRepository
{
    public interface IValeRepository : IGenericGetterRepository<valeData>
    {
         valeData GetbyVenta(Guid guid);
    }
}
