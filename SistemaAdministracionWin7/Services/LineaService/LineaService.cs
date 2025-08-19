using System.Collections.Generic;
using DTO.BusinessEntities;
using Repository.LineaRepository;
using Services.AbstractService;
using Services.Interfaces;

namespace Services.LineaService
{
    public class LineaService : ObjectService<LineaData, ILineaRepository>, IDefaultable<LineaData>
    { 
        public LineaService(ILineaRepository repo):base(repo)
        {
          
        }
        public LineaService(bool local = true)
         {
             _repo = new LineaRepository(local);
         }

        public override List<LineaData> NormalizeList(List<LineaData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(x => x.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => System.String.Compare(x.Description, y.Description, System.StringComparison.Ordinal));

            return list;
        }

        public override LineaData getPropertiesInfo(LineaData theObject)
        {
            if (IsEmpty(theObject))
                theObject = GetDefault();

            return theObject;
        }

        public LineaData GetDefault()
        {
            LineaData l = new LineaData();
            l.Description = "Sin Descripcion";
            l.Enable = true;
            return l;
        }

       
    }
}
