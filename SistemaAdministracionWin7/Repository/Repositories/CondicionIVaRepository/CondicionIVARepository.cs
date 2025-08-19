using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.BancosRepository
{
    public class CondicionIVARepository : DbRepository, IGenericRepository<CondicionIvaData>
    {
        public CondicionIVARepository(bool local = true) : base(local) { }




        public bool Insert(CondicionIvaData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[CondicionIva]
           ([id]
           ,[Description]
           ,[enable])
     VALUES
           (@id
           ,@Description
           ,@enable);SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, theObject) > 0;

            }
        }

        public bool Update(CondicionIvaData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[CondicionIva]
                Set Description = @Description, enable = @enable where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, theObject) > 0;

            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[CondicionIva]
                set enable = 0 where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new{id=idObject}) > 0;

            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[CondicionIva]
                set  enable =1 where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new{id=idObject}) > 0;

            }
        }

        public List<CondicionIvaData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT ;
                IEnumerable<CondicionIvaData> resultado = con.Query<CondicionIvaData>(sql);
                return resultado.ToList();
            }
        }

        public CondicionIvaData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<CondicionIvaData>("select " + DEFAULT_SELECT + " where id = @id", new { id = idObject }) ?? new CondicionIvaData();
            }
        }

        public CondicionIvaData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CondicionIvaData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public override string DEFAULT_SELECT
        {
            get { return " * from CondicionIva"; }
        }
    }
}
