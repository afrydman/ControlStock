using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.ColoresRepository
{
    public class TributoRepository : DbRepository, IGenericRepository<TributoData>
    {
        public TributoRepository(bool local=true) : base(local) { }



        public bool Insert(TributoData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"Insert Into [FE_Tributo] ([id],[Description],[idAfip],enable) Values (@id,@Description,@idAfip,@enable);SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, theObject) > 0;

            }
        }

        public bool Update(TributoData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"Update FE_Tributo
	                Set
		
		                [Description] = @Description,
		                [idAfip] = @idAfip,
                        [enable]=@enable
	                Where		
	                [id] = @id;SELECT @@ROWCOUNT;";

                return con.QueryFirstOrDefault<int>(sql,theObject) > 0;

            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"Update FE_Tributo Set enable = '0' Where [id] = @id;SELECT @@ROWCOUNT;";
                
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject}) > 0;

            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"Update FE_Tributo Set enable = '1' Where [id] = @id;SELECT @@ROWCOUNT;";

                return con.QueryFirstOrDefault<int>(sql, new { id = idObject}) > 0;

            }
        }

        public List<TributoData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = DEFAULT_SELECT;
                IEnumerable<TributoData> resultado = con.Query<TributoData>(sql);
                return resultado.ToList();
            }
        }

        public TributoData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<TributoData>(DEFAULT_SELECT + " where id=@idtributo", new { idtributo = idObject }) ?? new TributoData();
            }
        }

        public TributoData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TributoData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }


        public override string DEFAULT_SELECT
        {
            get { return @"Select * From [FE_Tributo] "; }
        }
    }
}
