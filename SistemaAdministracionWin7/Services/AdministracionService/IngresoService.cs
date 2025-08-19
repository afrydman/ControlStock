using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.AdministracionRepository;
using Repository.Repositories.AdministracionRepository;
using Repository.Repositories.LocalRepository;
using Repository.Repositories.PersonalRepository;
using Services.AbstractService;
using Services.AdministracionService;

namespace Services.IngresoService
{
    public class IngresoService : ObjectGetterService<IngresoData, IIngresoRepository>, IGenericServiceSyncStuff<IngresoData>
    {

         public IngresoService(IIngresoRepository repo)
        {
            _repo = repo;
        }
         public IngresoService(bool local=true)
         {
             _repo = new IngresoRepository(local);
         }


       public override List<IngresoData> NormalizeList(List<IngresoData> aux, bool onlyEnable = true)
       {
           if (onlyEnable)
               aux = aux.FindAll(n => n.Enable);

           aux.ForEach(n => n = getPropertiesInfo(n));

           aux.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

           return aux;
       }

       public override IngresoData getPropertiesInfo(IngresoData aux)
       {
           return aux;
        }



        public List<IngresoData> GetModified(Guid idLocal, int prefix)
        {
            try
            {
                return NormalizeList(_repo.GetModified(idLocal, prefix));
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idLocal, "IngresoService_GetModified"), true, true);

                throw;

            }
            
        }

        public bool MarkSeen(Guid idLocal, int prefix)
        {
           
            try
            {
                return _repo.MarkSeen(idLocal, prefix);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idLocal, "IngresoService_MarkSeen"), true, true);

                throw;

            }
        }
    }
}
