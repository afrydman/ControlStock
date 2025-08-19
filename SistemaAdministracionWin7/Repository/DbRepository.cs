using System;
using System.Configuration;

namespace Repository
{

    public abstract class DbRepository
    {
        protected readonly string _connectionString;

       

        public abstract string DEFAULT_SELECT { get; }

        public virtual string tipoRepository { get { return this.ToString(); } }

        protected DbRepository(bool useLocal=true)
        {
            string connString = "";


            connString = useLocal
                ? ConfigurationManager.ConnectionStrings["Local"].ConnectionString
                : ConfigurationManager.ConnectionStrings["remote"].ConnectionString;

            if (String.IsNullOrEmpty(connString))
                throw new ArgumentNullException("connectionString", "No puede usar un DbRepository sin indicar un connectionString.");

            _connectionString = connString;
        }
    }

    //public abstract class DbRepository2<T>: IGenericRepository<T> where T:GenericObject
    //{
    //    protected readonly string _connectionString;


    //    public virtual string tipoRepository { get { return this.ToString(); } }

    //    protected DbRepository2(bool useLocal=true)
    //    {
    //        string connString = "";


    //        connString = useLocal
    //            ? ConfigurationManager.ConnectionStrings["Local"].ConnectionString
    //            : ConfigurationManager.ConnectionStrings["remote"].ConnectionString;

    //        if (String.IsNullOrEmpty(connString))
    //            throw new ArgumentNullException("connectionString", "No puede usar un DbRepository sin indicar un connectionString.");

    //        _connectionString = connString;
    //    }


    //    public abstract bool Insert(T theObject);
    //    public abstract bool Update(T theObject);
    //    public abstract bool Disable(T theObject);
    //    public abstract bool Enable(T theObject);
    //    public abstract List<T> GetAll();
    //    public abstract T GetByID(Guid idObject);
    //    public abstract T GetLast(Guid idLocal, int Prefix);
    //}

    

}
