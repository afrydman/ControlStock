using System.Collections.Generic;
using DTO.BusinessEntities;
using Repository.ListaPrecioRepository;
using Repository.Repositories.ListaPrecioRepository;
using Services.AbstractService;

namespace Services.ListaPrecioService
{
    public class ListaPrecioService : ObjectService<listaPrecioData, IListaPrecioRepository>
    {
     
        public ListaPrecioService(IListaPrecioRepository repo):base(repo)
        {
     
        }
        public ListaPrecioService(bool local = true)
         {
             _repo = new ListaPrecioRepository(local);
         }

        public override List<listaPrecioData> NormalizeList(List<listaPrecioData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(x => x.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => System.String.Compare(x.Description, y.Description, System.StringComparison.Ordinal));

            return list;
        }

        public override listaPrecioData getPropertiesInfo(listaPrecioData theObject)
        {
            return theObject;
        }


    }
}
