using System;
using DTO.BusinessEntities;

namespace Repository.ChequeraRepository
{
    public interface IChequeraRepository : IGenericRepository<ChequeraData>
    {
        bool SetearSiguiente(Guid idChequera, string newSiguiente);
    }
}
