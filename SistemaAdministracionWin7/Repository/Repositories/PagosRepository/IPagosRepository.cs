using System;
using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Repository.Repositories.PagosRepository
{
  public  interface IPagosRepository : IGenericChildRepository<PagoData>
    {
        List<PagoData> GetPagosByTipo(Guid idTipoPago);
    }
}
