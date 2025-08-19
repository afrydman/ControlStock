using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.BusinessEntities;
using Repository;

namespace Services.AbstractService
{



    public  abstract class ObjectGetterService<T,X>:ObjectService<T, X>, IGenericServiceGetter<T> 
        where T : MovimientoEnCajaData
        where X : IGenericGetterRepository<T>
    {

          public ObjectGetterService(X repo)
        {
            _repo = repo;
            
        }



          public ObjectGetterService() { }


        public List<T> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int prefix, bool enableOnly = true)
        {
            
            List<T> auxList = _repo.GetByRangoFecha(fecha1, fecha2, idLocal, prefix);


            return NormalizeList(auxList, enableOnly);
        }

        public List<T> GetByFecha(DateTime fecha1, Guid idLocal, int prefix, bool enableOnly = true)
        {
            List<T> auxList = _repo.GetByRangoFecha(fecha1.Date, fecha1.Date.AddDays(1), idLocal, prefix);


            return NormalizeList(auxList, enableOnly);
        }

        public List<T> GetBiggerThan(int ultimo, Guid idLocal, int prefix, bool enableOnly = true)
        {
            List<T> auxList = _repo.GetBiggerThan(ultimo, idLocal, prefix);


            return NormalizeList(auxList, enableOnly);
        }

        public List<T> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix, bool enableOnly = true)
        {
            List<T> auxList = _repo.GetOlderThan(ultimo, idLocal, prefix);


            return NormalizeList(auxList, enableOnly);
        }
    }
}
