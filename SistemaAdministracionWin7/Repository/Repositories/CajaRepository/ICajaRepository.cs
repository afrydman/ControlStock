using System;
using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Repository.Repositories.CajaRepository
{
    public interface ICajaRepository : IGenericRepository<CajaData>,IGenericGetters<CajaData>
    {
        CajaData GetCajabyFecha(Guid idLocal, DateTime fecha);
        List<CajaData> GetByRangoFecha2(DateTime from, DateTime to, Guid idLocal, int prefix);
        CajaData GetCajaInicial(Guid idLocal, DateTime fecha);

       
       
    }
}
