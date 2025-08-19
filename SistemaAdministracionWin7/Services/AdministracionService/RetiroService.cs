using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.AdministracionRepository;
using Repository.Repositories.AdministracionRepository;
using Services.AbstractService;

namespace Services.RetiroService
{
    public class RetiroService : ObjectGetterService<RetiroData, IRetiroRepository>, IGenericServiceSyncStuff<RetiroData>
    {


        public RetiroService(IRetiroRepository repo)
        {
            _repo = repo;
        }
        public RetiroService(bool local = true)
         {
             _repo = new RetiroRepository(local);
            
         }

     

        public override bool Enable(RetiroData theObject)
        {
            throw new NotImplementedException();
        }





        public override List<RetiroData> NormalizeList(List<RetiroData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(data => data.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            return list;
        }

        public override RetiroData getPropertiesInfo(RetiroData aux)
        {
            
            return aux;
        }

      

        public List<RetiroData> GetModified(Guid idLocal, int prefix)
        {
            
            try
            {
                return _repo.GetModified(idLocal, prefix);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idLocal, "RetiroService_GetModified"), true, true);

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
                                ObjectDumperExtensions.DumpToString(idLocal, "RetiroService_MarkSeen"), true, true);

                throw;

            }
        }
    }
}
