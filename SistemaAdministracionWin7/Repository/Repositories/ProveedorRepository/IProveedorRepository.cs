using DTO.BusinessEntities;

namespace Repository.Repositories.ProveedorRepository
{
    public interface IProveedorRepository : IGenericRepository<ProveedorData>
    {
        ProveedorData GetByCodigo(string cod);
    }
}
