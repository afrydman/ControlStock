using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.ChequeraRepository
{
    public class ChequeraRepository : DbRepository,IChequeraRepository
    {

        public ChequeraRepository(bool local=true) : base(local)
        {
        }
        
        public override string DEFAULT_SELECT
        {
            get
            {
                return @" chequera.*,cuentas.*,bancos.*
                      FROM [chequera]
                      inner join cuentas on chequera.idCuenta = cuentas.id
                      inner join bancos on cuentas.idbanco=bancos.id  ";
            }
        }
        public bool SetearSiguiente(Guid idChequera, string newSiguiente)
        {
            //
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"update chequera set siguiente = @siguiente where id = @id; 
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    id = idChequera,siguiente=newSiguiente
                }) > 0;
            }
        }

        public bool Insert(ChequeraData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[chequera]
           ([id]
           ,[idCuenta]
           ,[primero]
           ,[ultimo]
           ,[codigoInterno]
           ,[Description]
           ,[enable]
           ,[siguiente])
     VALUES
           (@id
           ,@idCuenta
           ,@primero
           ,@ultimo
           ,@codigoInterno
           ,@Description
           ,@enable
           ,@siguiente);SELECT @@ROWCOUNT;";



                
                return con.QueryFirstOrDefault<int>(sqlEnvio,
                new{
                    id=theObject.ID,idCuenta=theObject.Cuenta.ID,primero=theObject.Primero,ultimo=theObject.Ultimo,
                    codigoInterno=theObject.CodigoInterno,Description=theObject.Description,Enable=theObject.Enable
                    ,siguiente = theObject.Siguiente
                }) > 0;

            }
        }

        public bool Update(ChequeraData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"
                                   UPDATE chequera
set Description=@Description, primero=@primero, ultimo=@ultimo, siguiente=@siguiente,enable=@enable
where id=@id;SELECT @@ROWCOUNT;";




                return con.QueryFirstOrDefault<int>(sqlEnvio,
                new
                {
                    id = theObject.ID,
                    
                    primero = theObject.Primero,
                    ultimo = theObject.Ultimo,
                    Description = theObject.Description,
                    Enable = theObject.Enable
                 ,
                    siguiente = theObject.Siguiente
                }) > 0;

            }
        }

        public bool Disable(Guid idObject)
        {
            //
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"update chequera set enable = '0' where chequera.id = @id; 
                        SELECT @@ROWCOUNT;";
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
                var sql = @"update chequera set enable = '1' where chequera.id = @id; 
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    id = idObject
                }) > 0;
            }
        }

        public List<ChequeraData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT ;
                IEnumerable<ChequeraData> resultado = con.Query<ChequeraData, CuentaData, BancoData, ChequeraData>(sql, (ch, cu,ban) =>
                {
                    cu.Banco = ban;
                    ch.Cuenta = cu;
                    return ch;
                });
                return resultado.ToList();
            }
        }




        public ChequeraData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.Query<ChequeraData, CuentaData, BancoData, ChequeraData>("select " + DEFAULT_SELECT + " where chequera.id = @id", (ch, cu, ban) =>
                {
                    cu.Banco = ban;
                    ch.Cuenta = cu;
                    return ch;
                }, new { id = idObject }, splitOn: "id,id").FirstOrDefault() ?? new ChequeraData();
            }
        }

        public ChequeraData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChequeraData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }
    }
}
