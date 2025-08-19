using DTO.BusinessEntities;

namespace Repository.ColoresRepository
{
    public interface IColorRepository :IGenericRepository<ColorData>
    {
        ColorData GetColorByCodigo(string codigoColor);

    }
}
