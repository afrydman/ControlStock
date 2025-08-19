using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Repository.Repositories.RemitoRepository
{
    public interface IRemitoDetalleRepository : IGenericChildRepository<remitoDetalleData>
    {
        List<remitoDetalleData> getbyProveedor(string codProveedor);

        
    }
}
