using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Repository.Repositories.PedidoRepository
{
    public interface IPedidoDetalleRepository:IGenericChildRepository<pedidoDetalleData>
    {
        List<pedidoDetalleData> GetbyCodigoInterno(string p);
    }
}
