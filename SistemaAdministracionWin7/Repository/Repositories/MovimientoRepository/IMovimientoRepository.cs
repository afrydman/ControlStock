using System;
using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Repository.Repositories.MovimientoRepository
{
    public interface IMovimientoRepository : IGenericRepository<MovimientoCuentaData>
    {
        List<DTO.BusinessEntities.MovimientoCuentaData> GetbyCajaDestino(Guid id);
        List<MovimientoCuentaData> GetbyCajaOrigen(Guid id);


        MovimientoCuentaData GetbyCheque(Guid guid);
    }
}
