using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.TeporadaRepository
{
   public class TemporadaRepository : DbRepository, ITemporadaRepository
    {
        public TemporadaRepository(bool local=true) : base(local)
        {
        }

        public bool Insert(TemporadaData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"Insert Into temporada
		([id],[Description],enable)
	Values
		(@id,@Description,@enable);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, theObject) > 0;

            }
        }

        public bool Update(TemporadaData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"Update temporada
	Set
		[id] = @id,
		[Description] = @Description
	Where		
	id = @id;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, theObject) > 0;

            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[temporada]
                set enable = 0 where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { id = idObject }) > 0;

            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[temporada]
                set enable = 1 where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { id = idObject }) > 0;

            }
        }

        public List<TemporadaData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select * from temporada";
                IEnumerable<TemporadaData> resultado = con.Query<TemporadaData>(sql);
                return resultado.ToList();
            }

        }

        public TemporadaData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<TemporadaData>("SELECT *  FROM  [temporada] Where id= @id ", new { id = idObject }) ?? new TemporadaData();
            }
        }

        public TemporadaData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

       public IEnumerable<TemporadaData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
       {
           throw new NotImplementedException();
       }

       public override string DEFAULT_SELECT
       {
           get { throw new NotImplementedException(); }
       }
    }
}
