using System;
using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Repository.Repositories.NotasRepository
{
    public interface INotaRepository : IGenericFatherRepository<NotaData>
    {
       List<NotaData> GetbyTercero(Guid idProveedor);

    }
}
