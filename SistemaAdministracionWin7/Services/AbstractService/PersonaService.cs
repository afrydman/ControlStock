using System;
using System.Collections.Generic;
using System.Linq;
using DTO.BusinessEntities;
using Repository;
using Services.AbstractService;
using Services.Interfaces;

namespace Services.AdministracionService
{
    public abstract class PersonaService<T, X> : ObjectService<T,X> where X : IGenericRepository<T> where T:PersonaData
    {
        protected PersonaService(X repo) : base(repo)
        {
        }
        protected PersonaService()
            : base()
        {
        }
        

        public override List<T> NormalizeList(List<T> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(x => x.Enable);

            if (list != null && list.Any() && list.Count> 0) { 
             list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => System.String.Compare(x.RazonSocial, y.RazonSocial, System.StringComparison.Ordinal));
            }
            return list;
        }

        public abstract void GetCC(PersonaData persona, out DateTime maxDateRecibo, out DateTime maxDateVenta,out decimal subt);


       
    }
}
