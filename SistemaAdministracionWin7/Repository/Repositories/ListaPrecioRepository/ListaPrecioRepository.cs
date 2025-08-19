using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;
using Repository.ListaPrecioRepository;

namespace Repository.Repositories.ListaPrecioRepository
{
   public class ListaPrecioRepository : DbRepository, IListaPrecioRepository
    {
        public ListaPrecioRepository(bool local=true) : base(local)
        {
        }

        public bool Insert(listaPrecioData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	Insert Into lista_precio
		([id],[Description],[enable])
	Values
		(@id,@Description,@enable);

                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, theObject) > 0;
            }
        }

        public bool Update(listaPrecioData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	Update lista_precio
	Set
		[id] = @id,
		[Description] = @Description,enable=@enable
	where id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, theObject) > 0;
            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	Update lista_precio
	Set
		enable='0'
	Where		
id=@id;                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject }) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	Update lista_precio
	                        Set
		                        enable='1'
	                        Where		
                        id=@id
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject }) > 0;
            }
        }

        public List<listaPrecioData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select * from lista_precio";
                IEnumerable<listaPrecioData> resultado = con.Query<listaPrecioData>(sql);
                return resultado.ToList();
            }
        }

        public listaPrecioData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<listaPrecioData>("select * from lista_precio where id = @id", new { id = idObject }) ?? new listaPrecioData();
            }
        }

        public listaPrecioData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

       public IEnumerable<listaPrecioData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
       {
           throw new NotImplementedException();
       }

       public override string DEFAULT_SELECT
       {
           get { throw new NotImplementedException(); }
       }
    }
}
