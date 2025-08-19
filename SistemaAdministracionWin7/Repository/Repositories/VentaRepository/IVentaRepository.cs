using System;
using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Repository.Repositories.VentaRepository
{
    public interface IVentaRepository : IGenericFatherRepository<VentaData>,IGenericSyncStuff<VentaData>
    {
        
        
       

        List<VentaData> getbyCliente(Guid guid);

        List<PagoData> obtenerTipoPagos(Guid idTipoPago);
    }
}
