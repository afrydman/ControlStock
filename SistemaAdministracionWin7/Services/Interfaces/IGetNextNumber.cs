using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.AbstractService
{
    public interface IGetNextNumberAvailable<T> : IGenericService<T>
    {
        string GetNextNumberAvailable(Guid idLocal, int myprefix, bool completo);
    }
}
