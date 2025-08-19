using System;
using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Repository.Repositories.RemitoRepository
{
    public interface IRemitoRepository : IGenericFatherRepository<RemitoData>
    {

        List<RemitoData> GetByLocalOrigen(Guid idLocal);
        List<RemitoData> GetByLocalDestino(Guid idLocal );
        bool ConfirmarRecibo(Guid id, DateTime fecha );

        List<RemitoData> GetAnulados(Guid idLocal);

        RemitoData GetLastLocalRecibido(Guid idLocal, int prefix );

        
    }
}
