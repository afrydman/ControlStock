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
    public class TipoIngresoRepository : DbRepository, ITipoIngresoRepository
    {
        public bool Insert(TipoIngresoData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[tipoIngreso]
           ([id]
           ,[Description],enable)
     VALUES
           (@id
           ,@Description,@enable);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, theObject) > 0;

            }
        }

        public bool Update(TipoIngresoData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"UPDATE [dbo].[tipoIngreso]
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
                var sql = @"UPDATE [dbo].[tipoIngreso]
   SET [enable] = 0
 WHERE  [id] = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    id = idObject,
                    
                }) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"UPDATE [dbo].[tipoIngreso]
   SET [enable] = 1
 WHERE  [id] = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    id = idObject,

                }) > 0;
            }
        }

        public List<TipoIngresoData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select * from tipoingreso";
                IEnumerable<TipoIngresoData> resultado = con.Query<TipoIngresoData>(sql);
                return resultado.ToList();
            }
        }

        public TipoIngresoData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<TipoIngresoData>("SELECT *  FROM tipoIngreso  Where id= @id ", new { id = idObject }) ?? new TipoIngresoData();
            }
        }

        public TipoIngresoData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TipoIngresoData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public override string DEFAULT_SELECT
        {
            get { throw new NotImplementedException(); }
        }
    }
}
