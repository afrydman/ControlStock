using System;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IPersona<T>
    {
        List<T> GetAll(bool onlyEnable);
        bool Enable(T theObject);

        bool Disable(T theObject);

        bool Insert(T theObject);

        bool Update(T theObject);

        T GetByID(Guid idp);
    }
}
