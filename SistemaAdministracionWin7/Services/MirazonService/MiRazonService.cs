using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository;
using Repository.Repositories.LocalRepository;
using Services.AbstractService;

namespace Services.LocalService
{
    public class MiRazonService : ObjectService<MiRazonData, IGenericRepository<MiRazonData>>
    {

        public MiRazonService(IGenericRepository<MiRazonData> repo)
             : base(repo)
        {
            
        }
         public MiRazonService(bool local = true)
         {
             _repo = new MiRazonRepository(local);
         }
         public override List<MiRazonData> NormalizeList(List<MiRazonData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(x => x.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => System.String.Compare(x.Description, y.Description, System.StringComparison.Ordinal));

            return list;
        }

         public override MiRazonData getPropertiesInfo(MiRazonData theObject)
        {
            return theObject;
        }

        public MiRazonData GetByID()
        {
            List<MiRazonData> aux = null;
            try
            {
                aux = _repo.GetAll();

                if (aux != null && aux.Count > 0)
                {
                    return aux[0];
                }
                return null;
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(aux, "MirazonService_GetByID"), true, true);

                throw;
            }
           
        }
    }
}
