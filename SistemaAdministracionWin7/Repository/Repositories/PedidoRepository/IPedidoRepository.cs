using System;
using DTO.BusinessEntities;

namespace Repository.Repositories.PedidoRepository
{
    public interface IPedidoRepository : IGenericRepository<pedidoData>, IGenericGetters<pedidoData>, IGenericSyncStuff<pedidoData>
    {
        
        bool MarcarCompleto(Guid guid, bool completo);
    }
}
