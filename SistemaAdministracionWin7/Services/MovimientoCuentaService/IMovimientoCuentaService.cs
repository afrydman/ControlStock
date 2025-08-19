using System;
using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Services.MovimientoCuentaService
{
    interface IMovimientoCuentaService
    {
        bool Insert(MovimientoCuentaData movimiento);

        string GetNextNumberAvailable(bool completo, Guid idLocal, int first);

        MovimientoCuentaData GetLast(Guid idlocal, int first);

        List<MovimientoCuentaData> GetbyCajaDestino(Guid idCajaDestino);

        List<MovimientoCuentaData> GetbyCajaOrigen(Guid idCajaOrigen);

        MovimientoCuentaData GetById(Guid guid);

        bool Disable(Guid idMovimiento);

        MovimientoCuentaData GetbyCheque(Guid idcheque);
    }
}
