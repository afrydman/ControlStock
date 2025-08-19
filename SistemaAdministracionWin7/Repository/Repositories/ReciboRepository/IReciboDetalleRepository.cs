using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Repository.Repositories.ReciboRepository
{
    public interface IReciboDetalleRepository : IGenericChildRepository<ReciboOrdenPagoDetalleData>
    {
        List<ReciboOrdenPagoDetalleData> GetAll( );
    }
}
