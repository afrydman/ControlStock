using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDefaultable<T>
    {
        T GetDefault();

        bool IsEmpty(T objectToCheck);
    }
}
