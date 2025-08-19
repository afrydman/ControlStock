using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.LineaRepository
{
    public class LineaRepository : DbRepository,ILineaRepository
    {

        public LineaRepository(bool local = true)
            : base(local)
        {
        }
        public bool Insert(LineaData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	Insert Into linea
		([id],[Description],enable)
	Values
		(@id,@Description,@enable);
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, theObject) > 0;
            }
        }

        public bool Update(LineaData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	Update linea
	                            Set
		
		                            [Description] = @Description
	                            Where		
                            [id] = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, theObject) > 0;
            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[linea]
                set enable = 0 where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { id = idObject }) > 0;

            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[linea]
                set enable = 1 where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { id = idObject }) > 0;

            }
        }

        public List<LineaData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = DEFAULT_SELECT;
                IEnumerable<LineaData> resultado = con.Query<LineaData>(sql);
                return resultado.ToList();
            }
        }

        public LineaData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<LineaData>(DEFAULT_SELECT + @"  Where
	                                    id=@id", new { id = idObject })??new LineaData();
            }
        }

        public LineaData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineaData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }


        public override string DEFAULT_SELECT
        {
            get
            {
                return @"Select * from linea";
            }
        }
    }
}
