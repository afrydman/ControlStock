using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.MovimientoRepository
{
    public class MovimientoRepository : DbRepository, IMovimientoRepository
    {
        public MovimientoRepository(bool local=true) : base(local)
        {
        }

        public bool Insert(MovimientoCuentaData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[movimientoscuenta]
           ([id]
           ,[idcuentaOrigen]
           ,[idcuentaDestino]
           ,[idcheque]
           ,[Date]
           ,[Monto]
           ,[Description]
           ,[Prefix]
           ,[Numero]
           ,[idlocal]
           ,[Enable],[idpersonal])
     VALUES
           (@id
           ,@idcuentaOrigen
           ,@idcuentaDestino
           ,@idcheque
           ,@Date
           ,@Monto
           ,@Description
           ,@Prefix
           ,@Numero
           ,@idlocal
           ,@Enable,@idpersonal);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {
                    theObject.ID,
                    idcuentaOrigen = theObject.cuentaOrigen==null?Guid.Empty:theObject.cuentaOrigen.ID,
                    idcuentadestino = theObject.cuentaDestino == null ? Guid.Empty : theObject.cuentaDestino.ID,
                    idcheque=theObject.cheque == null ? Guid.Empty :theObject.cheque.ID,
                    date=theObject.Date,
                    monto = theObject.Monto,
                    theObject.Description,
                    theObject.Prefix,
                    theObject.Numero,
                    idlocal=theObject.Local.ID,
                    theObject.Enable,
                    idpersonal=theObject.Personal.ID
                }) > 0;

            }
        }

        public bool Update(MovimientoCuentaData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update [movimientoscuenta] set Enable ='0' where id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject }) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public List<MovimientoCuentaData> GetAll()
        {
            throw new NotImplementedException();
        }


        //[movimientoscuenta].*, corigen.*,cdestino.*,cheques.*,locales.* 
        public MovimientoCuentaData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT + "  Where movimientoscuenta.id= @id ";
                MovimientoCuentaData aux = con.Query<MovimientoCuentaData, CuentaData, CuentaData, ChequeData, LocalData, PersonalData, MovimientoCuentaData>(sql
                    , (movimiento, corigen, cdestino, cheque, local, personal) =>
                    {
                        movimiento.cuentaOrigen = corigen;
                        movimiento.cuentaDestino = cdestino;
                        movimiento.cheque = cheque;
                        movimiento.Local = local;
                        movimiento.Personal = personal;
                        return movimiento;
                    }, new { id = idObject }, splitOn: "id,id,id,id").FirstOrDefault();

                return aux ?? new MovimientoCuentaData();
            }
        }

        public MovimientoCuentaData GetLast(Guid idLocal, int Prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"select " + DEFAULT_SELECT + @" where movimientoscuenta.Prefix=@Prefix and movimientoscuenta.idlocal=@idlocal  and movimientoscuenta.Numero = (
select max(Numero) as 'Numero' from [movimientoscuenta] where Prefix=@Prefix and idlocal=@idlocal )";
                MovimientoCuentaData aux = con.Query<MovimientoCuentaData, CuentaData, CuentaData, ChequeData, LocalData, PersonalData, MovimientoCuentaData>(sql
                    , (movimiento, corigen, cdestino, cheque, local, personal) =>
                    {
                        movimiento.cuentaOrigen = corigen;
                        movimiento.cuentaDestino = cdestino;
                        movimiento.cheque = cheque;
                        movimiento.Local = local;
                        movimiento.Personal = personal;
                        return movimiento;
                    },
                    new { idLocal = idLocal, Prefix = Prefix }, splitOn: "id,id,id,id").FirstOrDefault();


                return aux ?? new MovimientoCuentaData();
            }
        }

        public IEnumerable<MovimientoCuentaData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public List<MovimientoCuentaData> GetbyCajaDestino(Guid idcuentadestino)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + " where movimientoscuenta.idcuentaDestino = @idcuentadestino";
                IEnumerable<MovimientoCuentaData> resultado = con.Query<MovimientoCuentaData, CuentaData, CuentaData, ChequeData, LocalData, PersonalData, MovimientoCuentaData>(sql
                    , (movimiento, corigen, cdestino, cheque, local, personal) =>
                    {
                        movimiento.cuentaOrigen = corigen;
                        movimiento.cuentaDestino = cdestino;
                        movimiento.cheque = cheque;
                        movimiento.Local = local;
                        movimiento.Personal = personal;
                        return movimiento;
                    }, new { idcuentadestino = idcuentadestino }, splitOn: "id,id,id,id");
                return resultado.ToList();
            }
        }

        public List<MovimientoCuentaData> GetbyCajaOrigen(Guid idcuentaOrigen)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + " where movimientoscuenta.idcuentaOrigen = @idcuentaOrigen";
                IEnumerable<MovimientoCuentaData> resultado = con.Query<MovimientoCuentaData, CuentaData, CuentaData, ChequeData, LocalData, PersonalData, MovimientoCuentaData>(sql
                    , (movimiento, corigen, cdestino, cheque, local, personal) =>
                    {
                        movimiento.cuentaOrigen = corigen;
                        movimiento.cuentaDestino = cdestino;
                        movimiento.cheque = cheque;
                        movimiento.Local = local;
                        movimiento.Personal = personal;
                        return movimiento;
                    }, new { idcuentaOrigen = idcuentaOrigen }, splitOn: "id,id,id,id");
                return resultado.ToList();
            }
        }

     

        public MovimientoCuentaData GetbyCheque(Guid idcheque)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {


                var sql = "select " + DEFAULT_SELECT + "  where movimientoscuenta.idcheque = @idcheque ";
                MovimientoCuentaData aux = con.Query<MovimientoCuentaData, CuentaData, CuentaData, ChequeData, LocalData,PersonalData, MovimientoCuentaData>(sql
                    , (movimiento, corigen, cdestino, cheque, local,personal) =>
                    {
                        movimiento.cuentaOrigen = corigen;
                        movimiento.cuentaDestino = cdestino;
                        movimiento.cheque = cheque;
                        movimiento.Local = local;
                        movimiento.Personal = personal;
                        return movimiento;
                    }, new { idcheque = idcheque }, splitOn: "id,id,id,id").FirstOrDefault();

                return aux ?? new MovimientoCuentaData();
            }
        }

        public override string DEFAULT_SELECT
        {
            get { return @"[movimientoscuenta].*, corigen.*,cdestino.*,cheques.*,locales.*, personal.*
  FROM [movimientoscuenta]
  left join cuentas corigen on movimientoscuenta.idcuentaOrigen = corigen.id
  left join cuentas cdestino on movimientoscuenta.idcuentaDestino = cdestino.id
  left join cheques on movimientoscuenta.idcheque = cheques.id
  left join locales on movimientoscuenta.idlocal = locales.id
left join personal on movimientoscuenta.idpersonal = personal.id "; }
        }
    }
}
