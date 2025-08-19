using System.Collections.Generic;
using DTO.BusinessEntities;
using Repository.Repositories.TeporadaRepository;
using Services.AbstractService;
using Services.Interfaces;

namespace Services.TemporadaService
{
    public class TemporadaService : ObjectService<TemporadaData, ITemporadaRepository>, IDefaultable<TemporadaData>
    {

        //
        public TemporadaService(ITemporadaRepository repo)
        {
            _repo = repo;
        }
        public TemporadaService(bool local = true)
         {
             _repo = new TemporadaRepository(local);
             
         }
       
    
        public override List<TemporadaData> NormalizeList(List<TemporadaData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(aux => aux.Enable);

            list.Sort((x, y) => System.String.Compare(x.Description, y.Description, System.StringComparison.Ordinal));

            return list;
        }

        public override TemporadaData getPropertiesInfo(TemporadaData theObject)
        {
            return theObject;
        }

        public TemporadaData GetDefault()
        {

            TemporadaData l = new TemporadaData();
            l.Description = "Sin Descripcion";
            l.Enable = true;
            return l;
        
        }
    }
}
