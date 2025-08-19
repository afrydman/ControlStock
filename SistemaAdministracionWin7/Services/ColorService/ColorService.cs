using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.ColoresRepository;
using Services.AbstractService;
using Services.Interfaces;

namespace Services.ColorService
{
    public class ColorService : ObjectService<ColorData, IColorRepository>, IDefaultable<ColorData>
    {
        public ColorService(IColorRepository repo)
            : base(repo)
        {
            
        }

        public ColorService(bool local = true)
         {

             _repo = new ColorRepository(local);
         }
        public ColorData GetByCodigo(string codCol)
        {
            try
            {
                return getPropertiesInfo(_repo.GetColorByCodigo(codCol));
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(codCol, "ColorService_GetByCodigo"), true, true);

                throw;
            }
            
        }

        public override List<ColorData> NormalizeList(List<ColorData> list, bool onlyEnable = true)
        {
            if(list!=null && list.Count>0) { 
            if (onlyEnable)
                list = list.FindAll(x => x.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => String.Compare(x.Description, y.Description, StringComparison.Ordinal));

            }
            return list;
        }

        public override ColorData getPropertiesInfo(ColorData n)
        {
            if (IsEmpty(n))
                n = GetDefault();

            return n;
        }

        public ColorData GetDefault()
        {
            ColorData c = new ColorData();
            c.Description = "Sin Informacion";
            c.Enable = true;
            c.Codigo = "999";

            return c;

        }

      
    }
}
