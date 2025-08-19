using System.Collections.Generic;
using DTO.BusinessEntities;
using Repository;
using Repository.ColoresRepository;
using Services.AbstractService;

namespace Services.TributoService
{
    public class TributoService : ObjectService<TributoData, IGenericRepository<TributoData>>
    {
        public TributoService(IGenericRepository<TributoData> repo)
            : base(repo)
        {}
        public TributoService(bool local = true)
         {
             _repo = new TributoRepository(local);
         }
      

        public override List<TributoData> NormalizeList(List<TributoData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(x => x.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => System.String.Compare(x.Description, y.Description, System.StringComparison.Ordinal));

            return list;
        }

        public override TributoData getPropertiesInfo(TributoData n)
        {
            return n;
        }
    }
}
