using System.Collections.Generic;
using DTO.BusinessEntities;
using Repository.Repositories.LocalRepository;
using Services.AbstractService;

namespace Services.LocalService
{
    public class LocalService : ObjectService<LocalData, ILocalRepository>
    {
        
         public LocalService(ILocalRepository repo)
             : base(repo)
        {
            
        }
         public LocalService(bool local = true)
         {
             _repo = new LocalRepository(local);
         }
        public override List<LocalData> NormalizeList(List<LocalData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(x => x.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => System.String.Compare(x.Description, y.Description, System.StringComparison.Ordinal));

            return list;
        }

        public override LocalData getPropertiesInfo(LocalData theObject)
        {
            return theObject;
        }
    }
}
