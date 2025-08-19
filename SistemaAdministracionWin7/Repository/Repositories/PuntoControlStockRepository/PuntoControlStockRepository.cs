using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.PuntoControlStockRepository
{
    public class PuntoControlStockRepository : DbRepository, IPuntoControlStockRepository
    {
        public PuntoControlStockRepository(bool local=true) : base(local)
        {
        }


        public override string DEFAULT_SELECT
        {
            get
            {
                return @" puntosControl.*,locales.*,Personal.* from puntosControl
left join locales  on puntosControl.idLocal = locales.id
left join Personal on puntosControl.idPersonal = Personal.id ";
            }
        }

        public bool Insert(PuntoControlStockData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[puntosControl]
           ([id]
           ,[Description]
           ,[Enable]
           ,[Date]
           ,[idPersonal]
           ,[idLocal]
           ,[numero]
           ,[prefix])
     VALUES
           (@id
           ,@Description
           ,@Enable
            ,@Date
           ,@idPersonal
           ,@idLocal
           ,@numero
           ,@prefix
            );
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {
                    id = theObject.ID,
                    numero = theObject.Numero,
                    idPersonal = theObject.Vendedor.ID,
                    idLocal = theObject.Local.ID,
                    date = theObject.Date,
                    enable = theObject.Enable,
                    prefix = theObject.Prefix,
                    description = theObject.Description
                }) > 0;

            }
        }

        public bool Update(PuntoControlStockData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update puntosControl set Enable = '0'
	                            Where
                            id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject }) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update puntosControl set Enable = '1'
	                            Where
                            id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject }) > 0;
            }
        }

        public List<PuntoControlStockData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT;
                IEnumerable<PuntoControlStockData> resultado = OperatorGiveMeData(con, sql, null);
                return resultado.ToList();
            }
        }

        public PuntoControlStockData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT + "  Where puntosControl.id= @id ";
                PuntoControlStockData aux = OperatorGiveMeData(con, sql, new { id = idObject }).FirstOrDefault();


                if (aux == null) return new PuntoControlStockData();
                
                return aux;
            }
        }

        public PuntoControlStockData GetLast(Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT +
                          " where puntosControl.idLocal = @idLocal and puntosControl.Prefix  = @Prefix and puntosControl.Numero=(select max(puntosControl.Numero) from puntosControl where puntosControl.idLocal = @idLocal and puntosControl.Prefix  = @Prefix) ";
                PuntoControlStockData aux = OperatorGiveMeData(con, sql, new { idLocal = idLocal, prefix = prefix }).FirstOrDefault();

                return aux ?? new PuntoControlStockData();
            }
        }

        public IEnumerable<PuntoControlStockData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            return con.Query<PuntoControlStockData, LocalData,  PersonalData, PuntoControlStockData>(sql,
                    (pc, local,  personal) =>
                    {
                        pc.Local = local;
                        pc.Vendedor = personal;
                        return pc;
                    }
                     , parameters);
        }

        public List<PuntoControlStockData> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int Prefix)
        {
           
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT + "  where puntosControl.Date >= @fecha1 and puntosControl.Date <= @fecha2 and puntosControl.idLocal = @idLocal and puntosControl.Prefix=@Prefix ";
                IEnumerable<PuntoControlStockData> resultado = OperatorGiveMeData(con, sql, new {fecha1=fecha1,fecha2=fecha2,idLocal=idLocal,Prefix=Prefix});
                return resultado.ToList();
            }
        }

        public List<PuntoControlStockData> GetBiggerThan(int ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT + "   where puntosControl.Numero > @ultimo and puntosControl.idLocal = @idLocal and puntosControl.Prefix=@Prefix ";
                IEnumerable<PuntoControlStockData> resultado = OperatorGiveMeData(con, sql, new { ultimo = ultimo,  idLocal = idLocal, Prefix = prefix });
                return resultado.ToList();
            }
        }

        public List<PuntoControlStockData> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT + "   where puntosControl.Date > @ultimo and puntosControl.idLocal = @idLocal and puntosControl.Prefix=@Prefix ";
                IEnumerable<PuntoControlStockData> resultado = OperatorGiveMeData(con, sql, new { ultimo = ultimo, idLocal = idLocal, Prefix = prefix });
                return resultado.ToList();
            }
        }
    }
}
