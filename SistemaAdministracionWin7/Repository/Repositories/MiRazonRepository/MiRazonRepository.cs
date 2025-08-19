using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.LocalRepository
{
    public class MiRazonRepository : DbRepository, IGenericRepository<MiRazonData>
    {
        public MiRazonRepository(bool local = true)
            : base(local)
        {
        }

        public bool Insert(MiRazonData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [mirazon]
           ([razonsocial]
           ,[domicilio]
           ,[idcondicioniva]
           ,[cuit]
           ,[ingresosbrutos]
           ,[inicioactividad]
           ,[id]
           ,[Enable]
           ,[Description])
     VALUES
           (@razonsocial
           ,@domicilio
           ,@idcondicioniva
           ,@cuit
           ,@ingresosbrutos
           ,@inicioactividad
           ,@id
           ,@Enable
           ,@Description);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {

                    razonsocial=theObject.RazonSocial,
                    domicilio = theObject.Domicilio,
                    idcondicioniva = theObject.CondicionIva.ID,
                    cuit = theObject.Cuit,
                    ingresosbrutos = theObject.IngresosBrutos,
                    inicioactividad = theObject.inicioActividad,
                    id = theObject.ID,
                    Enable = theObject.Enable,
                    Description = theObject.Description,

                }) > 0;

            }
        }

        public bool Update(MiRazonData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"Update mirazon
		                        SET [razonsocial] = @razonsocial
                              ,[domicilio] = @domicilio
                              ,[idcondicioniva] = @idcondicioniva
                              ,[cuit] = @cuit
                              ,[ingresosbrutos] = @ingresosbrutos
                              ,[inicioactividad] = @inicioactividad
                              ,[Enable] = @Enable
                              ,[Description] = @Description
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
                var sqlEnvio = @"UPDATE [dbo].[mirazon]
                set enable = 0 where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { id = idObject }) > 0;

            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[mirazon]
                set enable = 1 where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { id = idObject }) > 0;

            }
        }

        public List<MiRazonData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"SELECT [mirazon].* , condicioniva.*
                          FROM [mirazon]
                          inner join condicioniva on mirazon.idcondicioniva = condicioniva.id";
                IEnumerable<MiRazonData> resultado = con.Query<MiRazonData, CondicionIvaData, MiRazonData>(sql,
                        (razon, condicion) =>
                        {
                            razon.CondicionIva = condicion;
                            return razon;
                        });
                return resultado.ToList();
            }
        }

        public MiRazonData GetByID(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public MiRazonData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MiRazonData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public override string DEFAULT_SELECT
        {
            get { throw new NotImplementedException(); }
        }
    }
}
