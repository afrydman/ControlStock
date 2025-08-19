using System;
using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Repository.ChequeRepository
{
    public interface IChequeRepository : IGenericRepository<ChequeData>
    {
        int ObtenerUltimoInterno();

        List<DTO.BusinessEntities.ChequeData> GetByChequera(Guid idChequera );

   
    }
}
