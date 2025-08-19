using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Repository.Repositories.VentaRepository
{
    public interface IVentaDetalleRepository : IGenericChildRepository<VentaDetalleData>
    {

        List<VentaDetalleData> GetByCodigoInterno(string codigo);
    }
}
