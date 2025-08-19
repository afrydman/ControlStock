using System;
using DTO.BusinessEntities;

namespace Repository.Repositories.PrecioRepository
{
    public interface IPrecioRepository : IGenericRepository<ListaPrecioProductoTalleData>
    {
        ListaPrecioProductoTalleData GetPrecio(Guid idLista, Guid idProductoTalle);

    }
}
