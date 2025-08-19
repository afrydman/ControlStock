using System;
using DTO.BusinessEntities;

namespace Services.ChequeraService
{
    public interface IChequeraService
    {


        string GetNextNumberAvailable();

        bool existeEsteCheque(Guid idChequera, string numeroVerificar);

        bool SetearSiguiente(ChequeraData chequera);

    }
}
