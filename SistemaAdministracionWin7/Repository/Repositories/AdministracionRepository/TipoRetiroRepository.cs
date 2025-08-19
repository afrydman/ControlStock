using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.AdministracionRepository;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.AdministracionRepository
{
    public class TipoRetiroRepository : DbRepository, ITipoRetiroRepository
    {
        public bool Insert(TipoRetiroData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[tipoRetiro]
           ([id]
           ,[Description],enable)
     VALUES
           (@id
           ,@Description,@enable);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, theObject) > 0;

            }
        }

        public bool Update(TipoRetiroData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"UPDATE [dbo].[tipoRetiro]
   SET [Description] = @Description
 WHERE  [id] = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    id = theObject.ID,
                    Description = theObject.Description
                }) > 0;
            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"UPDATE [dbo].[tipoRetiro]
   SET [enable] = 0
 WHERE  [id] = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    id =idObject,

                }) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"UPDATE [dbo].[tipoRetiro]
   SET [enable] = 1
 WHERE  [id] = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    id = idObject,

                }) > 0;
            }
        }

        public List<TipoRetiroData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select * from tipoRetiro";
                IEnumerable<TipoRetiroData> resultado = con.Query<TipoRetiroData>(sql);
                return resultado.ToList();
            }
        }

        public TipoRetiroData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<TipoRetiroData>("SELECT *  FROM tipoRetiro  Where id= @id ", new { id = idObject }) ?? new TipoRetiroData();
            }
        }

        public TipoRetiroData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TipoRetiroData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public override string DEFAULT_SELECT
        {
            get { throw new NotImplementedException(); }
        }
    }
}
