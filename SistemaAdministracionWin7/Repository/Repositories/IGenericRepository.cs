using System;
using System.Collections.Generic;
using System.Data;

namespace Repository
{
    public interface IGenericRepository<T> //: IGenericGetters<T>
    {
        bool Insert(T theObject);

        bool Update(T theObject);

        bool Disable(Guid idObject);

        bool Enable(Guid idObject);

        List<T> GetAll();

        T GetByID(Guid idObject);

        T GetLast(Guid idLocal, int first);

        IEnumerable<T> OperatorGiveMeData(IDbConnection con, string sql, object parameters);
    }

    public interface IGenericGetterRepository<T> : IGenericRepository<T>, IGenericGetters<T>
    {

    }


    public interface IGenericFatherRepository<T> :  IGenericRepository<T>,IGenericGetters<T>
    {

    }

    public  interface IGenericChildRepository<T>
    {
        bool InsertDetalle(T ordenPagoDetalle);

        List<T> GetDetalles(Guid idPadre);

       
    }

    public interface IGenericGetters<T>
    {
        List<T> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int Prefix);

        List<T> GetBiggerThan(int ultimo, Guid idLocal, int prefix);
        List<T> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix);
    }

    public interface IGenericSyncStuff<T>
    {
        List<T> GetModified(Guid idLocal,int prefix);

        bool MarkSeen(Guid idLocal,int prefix);

    }

}
