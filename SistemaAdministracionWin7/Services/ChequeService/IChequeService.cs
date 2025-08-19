using System;
using System.Collections.Generic;
using DTO.BusinessEntities;

namespace Services.ChequeService
{
    interface IChequeService
    {
        int GetNextNumberAvailable();

        List<ChequeData> GetByChequera(Guid idChequera, bool onlyEnable, List<EstadoCheque> estados, bool completo);

     
        bool MarcarComo(ChequeData cheque,EstadoCheque nuevoEstado, DateTime? fecha = null, string obs = "");

        List<ChequeData> GetChequesTercero(bool onlyEnable, List<EstadoCheque> estados, bool completo);

        List<ChequeData> GetChequesUtilizables(bool onlyEnable);

        bool InternNumberIsValid(string interno);
    }
}
