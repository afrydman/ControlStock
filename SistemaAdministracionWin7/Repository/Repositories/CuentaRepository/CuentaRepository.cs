using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;
using Repository.CuentaRepository;

namespace Repository.Repositories.CuentaRepository
{
    public class CuentaRepository : DbRepository,ICuentaRepository
    {
        public CuentaRepository(bool local=true) : base(local)
        {
        }

        public bool Insert(CuentaData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	INSERT INTO [cuentas]
           ([id]
           ,[idbanco]
           ,[cbu]
           ,[cuenta]
           ,[Description]
           ,[descubierto]
           ,[enable]
           ,[escuentacorriente]
           ,[saldo]
           ,[sucursal]
           ,[tipocuenta],titular)
     VALUES
           (@id
           ,@idbanco
           ,@cbu
           ,@cuenta
           ,@Description
           ,@descubierto
           ,@enable
           ,@escuentacorriente
           ,@saldo
           ,@sucursal
           ,@tipocuenta,@titular);
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    id=theObject.ID,
                    idbanco = theObject.Banco.ID,
                    cbu=theObject.cbu,
                    description = theObject.Description,
                   descubierto = theObject.Descubierto,
                   cuenta = theObject.Cuenta,
                   theObject.Enable,
                   theObject.esCuentaCorriente,
                   saldo = theObject.Saldo,
                   sucursal = theObject.Sucursal,
                   tipoCuenta = theObject.TipoCuenta,
                   titular = theObject.Titular,
                   
 
                    

                }) > 0;
            }
        }

        public bool Update(CuentaData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	UPDATE [dbo].[cuentas]
                        set
                        [idbanco] = @idbanco
                      ,[cbu] = @cbu
                      ,[cuenta] = @cuenta
                      ,[Description] = @Description
                      ,[descubierto] = @descubierto
                      ,[enable] = @enable
                      ,[escuentacorriente] = @escuentacorriente
                      ,[saldo] = @saldo
                      ,[sucursal] = @sucursal
                      ,[tipocuenta] = @tipocuenta
                      ,[titular] = @titular
                 WHERE id=@id;
                
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    id = theObject.ID,
                    idbanco = theObject.Banco.ID,
                    cbu = theObject.cbu,
                    description = theObject.Description,
                    descubierto = theObject.Descubierto,
                    cuenta = theObject.Cuenta,
                    theObject.Enable,
                    theObject.esCuentaCorriente,
                    saldo = theObject.Saldo,
                    sucursal = theObject.Sucursal,
                    tipoCuenta = theObject.TipoCuenta,
                    titular = theObject.Titular,

                }) > 0;
            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update cuentas set enable = '0' where id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject}) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update cuentas set enable = '1' where id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id =idObject}) > 0;
            }
        }

        public List<CuentaData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = DEFAULT_SELECT;
                IEnumerable<CuentaData> resultado = con.Query<CuentaData,BancoData,CuentaData>(sql, (cuenta,  banco) =>
                {
                    cuenta.Banco = banco;
                    return cuenta;
                });
                return resultado.ToList();
            }
        }

        public CuentaData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.Query<CuentaData,BancoData,CuentaData>(DEFAULT_SELECT+ " where cuentas.id = @id", (cuenta,  banco) =>
                {
                    cuenta.Banco = banco;
                    return cuenta;
                },new { id = idObject }).FirstOrDefault()?? new CuentaData();
            }
        }

        public CuentaData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CuentaData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSaldo(Guid idcuenta, decimal p)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update cuentas set saldo = @saldo where id = @idcuenta;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql,new { idcuenta=idcuenta,saldo=p}) > 0;
            }
         }

        public override string DEFAULT_SELECT
        {
            get { return @"SELECT cuentas.*, bancos.*
  FROM [cuentas]
  left join bancos on cuentas.idbanco = bancos.id "; }
        }
    }
}
