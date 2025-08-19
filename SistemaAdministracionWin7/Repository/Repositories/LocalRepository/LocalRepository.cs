using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.LocalRepository
{
    public class LocalRepository : DbRepository,ILocalRepository
    {
        public LocalRepository(bool local=true) : base(local)
        {
        }

        public bool Insert(LocalData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"Insert Into locales
		([id],[direccion],[telefono],[Description],[Codigo],nombre,email,fechaStock,enable)
	Values
		(@id,@direccion,@telefono,@Description,@Codigo,@nombre,@email,@fechaStock,@enable);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, theObject) > 0;

            }
        }

        public bool Update(LocalData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"Update locales
	Set
		[id] = @id,
		[direccion] = @direccion,
		[telefono] = @telefono,
		[Description] = @Description,
		[Codigo] = @Codigo,
            nombre=@nombre,
email=@email,
fechaStock=@fechaStock
	Where		
id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, theObject) > 0;
            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[locales]
                set enable = 0 where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { id = idObject }) > 0;

            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[locales]
                set enable = 1 where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { id = idObject }) > 0;

            }
        }

        public List<LocalData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select * from locales";
                IEnumerable<LocalData> resultado = con.Query<LocalData>(sql);
                return resultado.ToList();
            }
        }

        public LocalData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<LocalData>("SELECT *  FROM locales  Where id= @id ", new {id = idObject})??new LocalData();
            }
        }

        public LocalData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LocalData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public override string DEFAULT_SELECT
        {
            get { throw new NotImplementedException(); }
        }
    }
}
