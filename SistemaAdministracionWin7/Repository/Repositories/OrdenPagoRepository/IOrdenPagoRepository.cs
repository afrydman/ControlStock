using System;
using DTO.BusinessEntities;

namespace Repository.Repositories.OrdenPagoRepository
{
    public interface IOrdenPagoRepository : IGenericFatherRepository<OrdenPagoData>
    {
        OrdenPagoData getOrdenByCheque(Guid idcheque);


    }
}
