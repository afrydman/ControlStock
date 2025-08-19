using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.Repositories.PagosRepository;

namespace Services.PagoService
{
   public class PagoService : IGenericChildService<PagoData>
    {
        protected readonly IPagosRepository _repo;
        public PagoService(IPagosRepository repo)
        {
            _repo = repo;
        
        }
        public PagoService(bool local = true)
         {
             _repo = new PagosRepository(local);
              
         }

        public bool InsertDetalle(PagoData detalle)
        {
            try
            {


                return _repo.InsertDetalle(detalle);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(detalle, "Pago_InsertDetalle"), true, true);

                throw;

            }
        }

        public List<PagoData> GetDetalles(Guid idPadre)
        {
            try
            {


                return _repo.GetDetalles(idPadre);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idPadre, "Pago_GetDetalles"), true, true);

                throw;

            }
        }

     

       public List<PagoData> GetPagosByTipo(Guid idTipoPago)
        {

           try
           {
               return _repo.GetPagosByTipo(idTipoPago);
           }
           catch (Exception e)
           {

               HelperService.WriteException(e);

               HelperService.writeLog(
                               ObjectDumperExtensions.DumpToString(idTipoPago, "Pago_GetPagosByTipo"), true, true);

               throw;

           }
            
        }
    }
}
