using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO;
using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.RemitoRepository
{
    public class RemitoRepository : DbRepository, IRemitoRepository
    {
        public RemitoRepository(bool local=true) : base(local)
        {
        }


        public IEnumerable<RemitoData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            return con.Query<RemitoData, LocalData, LocalData, PersonalData, RemitoData>(sql,
                    (remito, localD, localO, personal) =>
                    {
                        remito.LocalDestino = localD;
                        remito.Local = localO;
                        remito.Vendedor = personal;
                        return remito;
                    }
                     , parameters);
        }

        public bool Insert(RemitoData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [remitos]
           ([id]
           ,[Numero]
           ,[idPersonal]
           ,[idLocalO]
           ,[idLocalD]
           ,[Date]
           ,[fechaRecibo]
			,[Enable],cantidadTotal,Prefix,description)
     VALUES
           (@id
           ,@Numero
           ,@idPersonal
           ,@idlocalOrigen
           ,@idlocalDestino
           ,@Date
           ,@FechaRecibo
,@Enable,@cantidadTotal,@Prefix,@description);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {
                    id = theObject.ID,
                    numero = theObject.Numero,
                    idPersonal = theObject.Vendedor.ID,
                    idlocalOrigen = theObject.Local.ID,
                    idlocalDestino = theObject.LocalDestino.ID,
                    date = theObject.Date,
                    fecharecibo = theObject.FechaRecibo,
                    enable = theObject.Enable,
                    cantidadTotal = theObject.CantidadTotal,
                    prefix = theObject.Prefix,
                    description = theObject.Description
                }) > 0;

            }
        }

        public bool Update(RemitoData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update remitos set Enable = '0'
	                            Where
                            id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id =idObject}) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update remitos set Enable = '1'
	                            Where
                            id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id =idObject}) > 0;
            }
        }

        public List<RemitoData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT ;
                IEnumerable<RemitoData> resultado = con.Query<RemitoData, LocalData, LocalData, PersonalData, RemitoData>(sql,
                    (remito, localD, localO, personal) =>
                    {
                        remito.LocalDestino = localD;
                        remito.Local = localO;
                        remito.Vendedor = personal;
                        return remito;
                    });
                return resultado.ToList();
            }
        }

        public RemitoData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                RemitoData aux = con.Query<RemitoData, LocalData, LocalData, PersonalData,RemitoData>("SELECT " + DEFAULT_SELECT + " Where remitos.id= @id ",
                    (remito, localD, localO, personal) =>
                    {
                        remito.LocalDestino = localD;
                        remito.Local = localO;
                        remito.Vendedor = personal;
                        return remito;
                    }
                    , new { id = idObject }).FirstOrDefault();
                return aux ?? new RemitoData();
            }
        }
        /// <summary>
        /// Get last (Local origen)
        /// </summary>
        /// <param name="idLocal"></param>
        /// <param name="first"></param>
        /// <param name="connLocal"></param>
        /// <returns></returns>
        public RemitoData GetLast(Guid idLocal, int first)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT +
                          " where remitos.idLocalO = @idLocal and remitos.Prefix  = @Prefix and remitos.Numero=(select max(remitos.Numero) from remitos where remitos.idLocalO = @idLocal and remitos.Prefix  = @Prefix) ";
                RemitoData aux = con.Query<RemitoData, LocalData, LocalData, PersonalData, RemitoData>
                    (sql
                    ,(remito, localD, localO, personal) =>
                    {
                        remito.LocalDestino = localD;
                        remito.Local = localO;
                        remito.Vendedor = personal;
                        return remito;
                    },new { idLocal = idLocal, prefix = first }).FirstOrDefault() ;

                return aux ?? new RemitoData();
            }
        }

       

        public List<RemitoData> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int Prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT + "  where remitos.Date >= @fecha1 and remitos.Date <= @fecha2 and remitos.idLocalO = @idLocalOrigen and remitos.Prefix=@Prefix ";
                IEnumerable<RemitoData> resultado = con.Query<RemitoData, LocalData, LocalData, PersonalData, RemitoData>(sql,
                    (remito, localD, localO, personal) =>
                    {
                        remito.LocalDestino = localD;
                        remito.Local = localO;
                        remito.Vendedor = personal;
                        return remito;
                    }, new { fecha1 = fecha1, fecha2 = fecha2, prefix = Prefix, idLocalOrigen = idLocal });
                return resultado.ToList();
            }
        }
        public List<RemitoData> GetBiggerThan(int ultimo, Guid idLocalOrigen, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT + "  where remitos.Numero > @ultimo and remitos.idLocalO = @idLocalOrigen and remitos.Prefix=@Prefix ";
                IEnumerable<RemitoData> resultado = con.Query<RemitoData, LocalData, LocalData, PersonalData, RemitoData>(sql,
                    (remito, localD, localO, personal) =>
                    {
                        remito.LocalDestino = localD;
                        remito.Local = localO;
                        remito.Vendedor = personal;
                        return remito;
                    }, new { ultimo = ultimo, idLocalOrigen = idLocalOrigen, prefix = prefix });
                return resultado.ToList();
            }
        }

        public List<RemitoData> GetOlderThan(DateTime ultimo, Guid idLocalOrigen, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT + "  where remitos.Date > @ultimo and remitos.idLocalO = @idLocalOrigen and remitos.Prefix=@Prefix ";
                IEnumerable<RemitoData> resultado = con.Query<RemitoData, LocalData, LocalData, PersonalData, RemitoData>(sql,
                    (remito, localD, localO, personal) =>
                    {
                        remito.LocalDestino = localD;
                        remito.Local = localO;
                        remito.Vendedor = personal;
                        return remito;
                    },new { ultimo = ultimo, idLocalOrigen = idLocalOrigen, prefix = prefix });
                return resultado.ToList();
            }
        }

        public List<RemitoData> GetByLocalOrigen(Guid idLocalOrigen)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT + " where remitos.idLocalO = @idLocalOrigen ";
                IEnumerable<RemitoData> resultado = con.Query<RemitoData, LocalData, LocalData, PersonalData, RemitoData>(sql, 
                     (remito, localD, localO, personal) =>
                    {
                        remito.LocalDestino = localD;
                        remito.Local = localO;
                        remito.Vendedor = personal;
                        return remito;
                    },new { idLocalOrigen = idLocalOrigen });
                return resultado.ToList();
            }
        }

        public List<RemitoData> GetByLocalDestino(Guid idLocalDestino )
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT + "  where remitos.idLocalD = @idLocalDestino order by date desc ";
                IEnumerable<RemitoData> resultado = con.Query<RemitoData, LocalData, LocalData, PersonalData, RemitoData>(sql, (remito, localD, localO, personal) =>
                    {
                        remito.LocalDestino = localD;
                        remito.Local = localO;
                        remito.Vendedor = personal;
                        return remito;
                    }, new { idLocalDestino = idLocalDestino });
                return resultado.ToList();
            }
        }

        public bool ConfirmarRecibo(Guid id, DateTime fecha )
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	UPDATE [remitos]
                               SET 
                                  [fechaRecibo] = @fecha
                             WHERE
                            id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = id, fecha=fecha }) > 0;
            }
        }


        public List<RemitoData> GetAnulados(Guid idLocalO )
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT + "  where remitos.Enable = '0' and remitos.idLocalO = @idLocalO ";
                IEnumerable<RemitoData> resultado = con.Query<RemitoData, LocalData, LocalData, PersonalData, RemitoData>(sql, (remito, localD, localO, personal) =>
                    {
                        remito.LocalDestino = localD;
                        remito.Local = localO;
                        remito.Vendedor = personal;
                        return remito;
                    }, new { idLocalO = idLocalO });
                return resultado.ToList();
            }
        }

        public RemitoData GetLastLocalRecibido(Guid idLocalDestino, int prefix )
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {

                RemitoData aux =  con.Query<RemitoData, LocalData, LocalData, PersonalData, RemitoData>("SELECT " + DEFAULT_SELECT + "  where" +
                                             " remitos.idLocalD = @idLocalDestino and remitos.Prefix  = @Prefix and remitos.fechaRecibo>CONVERT(datetime,'18010101') " ,
                (remito, localD, localO, personal) =>
                {
                        remito.LocalDestino = localD;
                        remito.Local = localO;
                        remito.Vendedor = personal;
                        return remito;
                    }, new { idLocalDestino = idLocalDestino, prefix = prefix }).FirstOrDefault();

                return aux ?? new RemitoData();
            }
        }


        public override string DEFAULT_SELECT
        {
            get
            {
                return @" remitos.*,localD.*,localO.*,Personal.* from remitos
left join locales localD on remitos.idLocalD = localD.id
left join locales localO on remitos.idLocalO = localO.id
left join Personal on remitos.idPersonal = Personal.id ";
            }
        }
    }
}
