using System.Collections.Generic;
using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository;
using Repository.Repositories.BancosRepository;
using Services.AbstractService;

namespace Services.BancoService
{
    public class CondicionVentaService : ObjectService<CondicionVentaData, IGenericRepository<CondicionVentaData>>
    {


        public CondicionVentaService(IGenericRepository<CondicionVentaData> condicionIVARepository)
            : base(condicionIVARepository)
        {
          
        }
        public CondicionVentaService(bool local = true)
            : base()
        {
           _repo = new CondicionVentaRepository(local);
            
        }


        public override List<CondicionVentaData> NormalizeList(List<CondicionVentaData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(x => x.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => System.String.Compare(x.Description, y.Description, System.StringComparison.Ordinal));

            return list;
        }

        public override CondicionVentaData getPropertiesInfo(CondicionVentaData n)
        {
            return n;
        }
    }
}
