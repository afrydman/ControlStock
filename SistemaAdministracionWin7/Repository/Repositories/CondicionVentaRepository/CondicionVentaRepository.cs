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
    public class CondicionVentaRepository : DbRepository, IGenericRepository<CondicionVentaData>
    {
        public CondicionVentaRepository(bool local = true) : base(local) { }




        public bool Insert(CondicionVentaData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[CondicionVenta]
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

        public bool Update(CondicionVentaData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[CondicionVenta]
                Set Description = @Description, enable = @enable where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, theObject) > 0;

            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[CondicionVenta]
                set enable = 0 where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new{id=idObject}) > 0;

            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[CondicionVenta]
                set  enable =1 where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new{id=idObject}) > 0;

            }
        }

        public List<CondicionVentaData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT ;
                IEnumerable<CondicionVentaData> resultado = con.Query<CondicionVentaData>(sql);
                return resultado.ToList();
            }
        }

        public CondicionVentaData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<CondicionVentaData>("select " + DEFAULT_SELECT + " where id = @id", new { id = idObject }) ?? new CondicionVentaData();
            }
        }

        public CondicionVentaData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CondicionVentaData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public override string DEFAULT_SELECT
        {
            get { return " * from CondicionVenta"; }
        }
    }
}
