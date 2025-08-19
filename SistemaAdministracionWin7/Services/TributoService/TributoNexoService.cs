using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository;
using Repository.ColoresRepository;
using Repository.Repositories.VentaRepository;
using Services.AbstractService;

namespace Services.TributoService
{
    public class TributoNexoService : IGenericChildService<TributoNexoData>
    {

        protected readonly IGenericChildRepository<TributoNexoData> _repo;


      
        public TributoNexoService(IGenericChildRepository<TributoNexoData> repo)
        {
            _repo = repo;
        }


        public bool InsertDetalle(TributoNexoData detalle)
        {
            try
            {


                return _repo.InsertDetalle(detalle);
            }
            catch (Exception e)
            {

                
                         HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(detalle, "Tributo_InsertDetalle"), true, true);

                throw;

            }
        }

        public List<TributoNexoData> GetDetalles(Guid idPadre)
        {
            try
            {


                return _repo.GetDetalles(idPadre);
            }
            catch (Exception e)
            {

                
                         HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idPadre, "Tributo_GetDetalles"), true, true);

                throw;

            }
        }
        public static List<TributoNexoData> setDescriptions(List<TributoNexoData> list)
        {
            foreach (var tributoNexoData in list)
            {
                tributoNexoData.SetDescription(tributoNexoData.Tributo.Description);
            }
            return list;
        }
    }
}
