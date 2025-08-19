using System;
using DTO.BusinessEntities;

namespace Repository.FormaPagoRepository
{
    public interface IFormaPagoCuotasRepository : IGenericChildRepository<FormaPagoCuotaData>
    {

        bool UpdateAumento(DTO.BusinessEntities.FormaPagoCuotaData f );
        bool DeleteCuotas(Guid FatherID);

    }
}
