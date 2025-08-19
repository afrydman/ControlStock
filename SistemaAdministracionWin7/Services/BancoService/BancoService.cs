using System.Collections.Generic;
using DTO.BusinessEntities;
using Repository;
using Repository.Repositories.BancosRepository;
using Services.AbstractService;

namespace Services.BancoService
{
    public class BancoService : ObjectService<BancoData, IGenericRepository<BancoData>>
    {

        
        public BancoService(IGenericRepository<BancoData> bancoRepository)
            : base(bancoRepository)
        {
          
        }
        public BancoService(bool local=true) : base()
        {
           _repo = new BancoRepository(local);
            
        }


        public override List<BancoData> NormalizeList(List<BancoData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(x => x.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => System.String.Compare(x.Description, y.Description, System.StringComparison.Ordinal));

            //list.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            return list;
        }

        public override BancoData getPropertiesInfo(BancoData n)
        {
            return n;
        }
    }
}
