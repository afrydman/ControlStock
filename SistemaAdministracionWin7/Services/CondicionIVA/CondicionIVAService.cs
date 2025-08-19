using System.Collections.Generic;
using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository;
using Repository.Repositories.BancosRepository;
using Services.AbstractService;

namespace Services.BancoService
{
    public class CondicionIVAService : ObjectService<CondicionIvaData, IGenericRepository<CondicionIvaData>>
    {


        public CondicionIVAService(IGenericRepository<CondicionIvaData> condicionIVARepository)
            : base(condicionIVARepository)
        {
          
        }
        public CondicionIVAService(bool local = true)
            : base()
        {
           _repo = new CondicionIVARepository(local);
            
        }


        public override List<CondicionIvaData> NormalizeList(List<CondicionIvaData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(x => x.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => System.String.Compare(x.Description, y.Description, System.StringComparison.Ordinal));

            //list.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            return list;
        }

        public override CondicionIvaData getPropertiesInfo(CondicionIvaData n)
        {
            return n;
        }
    }
}
