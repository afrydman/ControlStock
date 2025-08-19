using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.FormaPagoRepository;
using Repository.Repositories.FormaPagoRepository;

namespace Services.FormaPagoService
{
    internal class FormaPagoCuotaService
    {
        protected readonly IFormaPagoCuotasRepository _repo;
        public FormaPagoCuotaService(IFormaPagoCuotasRepository repo)
        {
            _repo = repo;
        }

        public FormaPagoCuotaService(bool local = true)
         {
             _repo = new FormaPagoCuotasRepository(local);
         }

        internal bool InsertDetalle(FormaPagoCuotaData detalle)
        {
            try
            {
                return _repo.InsertDetalle(detalle);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(detalle, "Formapagocuota_InsertDetalle"), true, true);

                throw;
            }
            return false;
        }

        internal List<FormaPagoCuotaData> GetDetalles(Guid idPadre)
        {
           


            try
            {
                return _repo.GetDetalles(idPadre);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idPadre, "Formapagocuota_GetDetalles"), true, true);

                throw;
            }
            return null;
        }

        internal bool UpdateAumento(FormaPagoCuotaData f)
        {
            try
            {
                return _repo.UpdateAumento(f);
            }
            catch (Exception e)
            {
                
                           HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(f, "Formapagocuota_UpdateAumento"), true, true);

                throw;
            }
            return false;
        }
    }
}
