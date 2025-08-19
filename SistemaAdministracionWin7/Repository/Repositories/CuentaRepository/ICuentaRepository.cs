using System;
using DTO.BusinessEntities;

namespace Repository.CuentaRepository
{
   public  interface ICuentaRepository : IGenericRepository<CuentaData>
    {
     

        bool UpdateSaldo(Guid idcuenta, decimal p);

    }
}
