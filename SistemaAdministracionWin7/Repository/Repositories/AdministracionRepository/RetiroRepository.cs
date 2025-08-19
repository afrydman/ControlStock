using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO;
using DTO.BusinessEntities;
using Repository.AdministracionRepository;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.AdministracionRepository
{
    public class RetiroRepository : DbRepository, IRetiroRepository
    {
        public RetiroRepository(bool local = true)
            : base(local)
        {
        }



        public bool Insert(RetiroData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [retiros]
                               ([idLocal]
                               ,[idPersonal]
                               ,[Date]
                               ,[Monto]
                               ,[Codigo]
                               ,[fechaUso]
			                    ,[idTipoRetiro]
			                    ,[Description],id,Enable,modificado,Numero,Prefix
                                )
                         VALUES
                               (@idLocal
                               ,@idPersonal
                               ,@Date
                               ,@Monto
                               ,@Codigo
                               ,@fechauso,
                               @idTipo,@desc,@id,@Enable,'0',@Numero,@Prefix);
                                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {
                    idLocal = theObject.Local.ID,
                    idPersonal = theObject.Personal.ID,
                    Date = theObject.Date,
                    monto = theObject.Monto,
                    codigo = theObject.codigo,
                    fechauso = theObject.fechaUso,
                    idTipo = theObject.TipoRetiro.ID,
                    desc = theObject.Description,
                    id = theObject.ID,
                    enable = theObject.Enable,
                    numero = theObject.Numero,
                    prefix = theObject.Prefix
                }) > 0;

            }
        }

        public bool Update(RetiroData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"update 
                    retiros set Enable = '0'
                        ,modificado= '1'
	                    Where
                        id = @id
                        SELECT @@ROWCOUNT";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    id = idObject
                }) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"update 
                    retiros set Enable = '1'
                        ,modificado= '1'
	                    Where
                        id = @id
                        SELECT @@ROWCOUNT";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    id = idObject
                }) > 0;
            }
        }

        public List<RetiroData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT;
                IEnumerable<RetiroData> resultado = con.Query<RetiroData, LocalData, PersonalData, TipoRetiroData, RetiroData>(sql,
(retiro, local, personal, tipo) =>
{
    retiro.Local = local;
    retiro.Personal = personal;
    retiro.TipoRetiro = tipo;
    return retiro;
});
                return resultado.ToList();
            }
        }

        public RetiroData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                RetiroData aux = con.Query<RetiroData, LocalData, PersonalData, TipoRetiroData, RetiroData>(
@"SELECT " + DEFAULT_SELECT + @"  Where retiros.id= @id ",
(retiro, local, personal, tipo) =>
{
    retiro.Local = local;
    retiro.Personal = personal;
    retiro.TipoRetiro = tipo;
    return retiro;
},
                new { id = idObject }).FirstOrDefault();

                return aux ?? new RetiroData();
            }
        }

        public RetiroData GetLast(Guid idLocal, int first)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.Query<RetiroData, LocalData, PersonalData, TipoRetiroData, RetiroData>("select top 1 " + DEFAULT_SELECT + @"  where retiros.idLocal = @id and retiros.Prefix = @Prefix order by retiros.Numero desc  ",
(retiro, local, personal, tipo) =>
{
    retiro.Local = local;
    retiro.Personal = personal;
    retiro.TipoRetiro = tipo;
    return retiro;
}, new { id = idLocal, prefix = first }).FirstOrDefault() ?? new RetiroData();
            }
        }

        public IEnumerable<RetiroData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public List<RetiroData> GetModified(Guid idLocal, int first)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + @"  where retiros.modificado = '1' and retiros.idLocal = @id and retiros.Prefix=@Prefix";
                IEnumerable<RetiroData> resultado = con.Query<RetiroData, LocalData, PersonalData, TipoRetiroData, RetiroData>(sql,
(retiro, local, personal, tipo) =>
{
    retiro.Local = local;
    retiro.Personal = personal;
    retiro.TipoRetiro = tipo;
    return retiro;
}, new { id = idLocal, prefix = first });
                return resultado.ToList();
            }
        }

        public bool MarkSeen(Guid idLocal, int first)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"update retiros set modificado = '0' where idLocal = @id and Prefix=@Prefix; 
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    id = idLocal,
                    prefix = first
                }) > 0;
            }
        }

        public List<RetiroData> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int Prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"SELECT " + DEFAULT_SELECT + @" 
	                        Where
                            idLocal = @id and retiros.Date>=@fechaAyer and retiros.Date<=@fechaManana";

                IEnumerable<RetiroData> resultado = con.Query<RetiroData, LocalData, PersonalData, TipoRetiroData, RetiroData>(sql,
(retiro, local, personal, tipo) =>
{
    retiro.Local = local;
    retiro.Personal = personal;
    retiro.TipoRetiro = tipo;
    return retiro;
}, new { id = idLocal, fechaAyer = fecha1, fechaManana = fecha2 });
                return resultado.ToList();
            }
        }

        public List<RetiroData> GetBiggerThan(int ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"select " + DEFAULT_SELECT + @"  where retiros.Numero > @ultimo and retiros.idLocal = @idlocal and retiros.Prefix = @Prefix";

                IEnumerable<RetiroData> resultado = con.Query<RetiroData, LocalData, PersonalData, TipoRetiroData, RetiroData>(sql,
(retiro, local, personal, tipo) =>
{
    retiro.Local = local;
    retiro.Personal = personal;
    retiro.TipoRetiro = tipo;
    return retiro;
}, new { idLocal = idLocal, Prefix = prefix, ultimo = ultimo });
                return resultado.ToList();
            }
        }


        public List<RetiroData> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"select " + DEFAULT_SELECT + @"  where retiros.Date > @fecha and retiros.idLocal = @idlocal and retiros.Prefix = @Prefix";

                IEnumerable<RetiroData> resultado = con.Query<RetiroData, LocalData, PersonalData, TipoRetiroData, RetiroData>(sql,
(retiro, local, personal, tipo) =>
{
    retiro.Local = local;
    retiro.Personal = personal;
    retiro.TipoRetiro = tipo;
    return retiro;
}, new { idLocal = idLocal, prefix = prefix, fecha = ultimo });
                return resultado.ToList();
            }
        }

        public override string DEFAULT_SELECT
        {
            get
            {
                return @" retiros.*,
        locales.*,
		Personal.*,
		tiporetiro.* FROM [dbo].[retiros]

        left join locales on retiros.idlocal=locales.id
        left  join Personal on retiros.idpersonal= Personal.id
        left join tiporetiro on retiros.idtiporetiro = tiporetiro.id";
            }
        }
    }
}
