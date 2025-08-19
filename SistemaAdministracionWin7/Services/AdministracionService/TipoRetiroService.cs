using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.AdministracionRepository;
using Repository.Repositories.AdministracionRepository;
using Services.AbstractService;

namespace Services.AdministracionService
{
    public class TipoRetiroService : ObjectService<TipoRetiroData, ITipoRetiroRepository>
   {


        public TipoRetiroService(ITipoRetiroRepository repo)
        {
            _repo = repo;
        }
        public TipoRetiroService(bool local = true)
         {
             _repo = new TipoRetiroRepository();
         }
      
        public List<TipoRetiroData> GetAll(bool onlyEnable = true, bool soloRetiroManual = true)
        {
          


            try
            {
                List<TipoRetiroData> a = _repo.GetAll();
                if (soloRetiroManual)
                {
                    a = a.FindAll(delegate(TipoRetiroData aux) { return aux.ID != HelperService.idrecibo && aux.ID != HelperService.idextraccioCuenta; });
                }

                return NormalizeList(a, onlyEnable);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(onlyEnable, "TipoRetiro_GetAll"), true, true);

                throw;

            }
        }

       public override List<TipoRetiroData> NormalizeList(List<TipoRetiroData> list, bool onlyEnable = true)
       {
           if (onlyEnable)
               list = list.FindAll(aux => aux.Enable);

           list.Sort((x,y)=>System.String.Compare(x.Description, y.Description, System.StringComparison.Ordinal));

           return list;
       }

       public override TipoRetiroData getPropertiesInfo(TipoRetiroData n)
       {
           return n;
       }
   }
}
