using System;
using System.Collections.Generic;

namespace Services
{
    public interface IGenericService<T>
    {
        bool Insert(T theObject);

        bool Update(T theObject);

        bool Disable(T theObject);

        bool Enable(T theObject);

        List<T> GetAll(bool onlyEnable=true);

        T GetByID(Guid idObject);

        T GetLast(Guid idLocal, int first);

        List<T> NormalizeList(List<T> ps, bool onlyEnable = true);

        T getPropertiesInfo(T n);

        Type GetTypeRepo();
    }

    public interface IGenericChildService<T>
    {
        bool InsertDetalle(T detalle);

        List<T> GetDetalles(Guid idPadre);

       
    }

    public interface IGenericServiceGetter<T>
    {
        List<T> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int prefix, bool enableOnly = true);
        List<T> GetByFecha(DateTime fecha1, Guid idLocal, int prefix, bool enableOnly = true);

        List<T> GetBiggerThan(int ultimo, Guid idLocal, int prefix, bool enableOnly = true);
        List<T> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix, bool enableOnly = true);
    }

    public interface IGenericServiceSyncStuff<T>
    {
        List<T> GetModified(Guid idLocal, int prefix);

        bool MarkSeen(Guid idLocal, int prefix);

    }
}
