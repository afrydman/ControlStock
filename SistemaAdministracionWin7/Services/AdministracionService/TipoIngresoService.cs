using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.AdministracionRepository;
using Repository.Repositories.AdministracionRepository;
using Services.AbstractService;

namespace Services.AdministracionService
{
    public class TipoIngresoService : ObjectService<TipoIngresoData, ITipoIngresoRepository>
    {
    
        public TipoIngresoService(ITipoIngresoRepository repo)
        {
            _repo = repo;
        }
        public TipoIngresoService(bool local = true)
         {
             _repo = new TipoIngresoRepository();
         }

        public List<TipoIngresoData> GetAll(bool onlyEnable = true,bool soloRetiroManual = true)
        {
            try
            {
                List<TipoIngresoData> a = _repo.GetAll();

                if (soloRetiroManual)
                {
                    a = a.FindAll(delegate(TipoIngresoData aux) { return aux.ID != HelperService.idrecibo && aux.ID != HelperService.idextraccioCuenta; });
                }

                return NormalizeList(a, onlyEnable);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(onlyEnable, "TipoIngresoData_GetAll"), true, true);

                throw;

            }
           
        }

   
        public override List<TipoIngresoData> NormalizeList(List<TipoIngresoData> list, bool onlyEnable)
        {
            if (onlyEnable)
                list = list.FindAll(aux => aux.Enable);

            list.Sort((x, y) => System.String.Compare(x.Description, y.Description, System.StringComparison.Ordinal));

            return list;
        }

        public override TipoIngresoData getPropertiesInfo(TipoIngresoData n)
        {
            return n;
        }
    }
}
