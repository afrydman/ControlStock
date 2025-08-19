using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.ChequeRepository
{
    public class ChequeRepository : DbRepository,IChequeRepository
    {
        public ChequeRepository(bool local=true) : base(local)
        {
        }
        public override string DEFAULT_SELECT
        {
            get
            {
                return @" select cheques.*,b.*,chequera.*,cuentas.*,c.*,locales.*
                  FROM [cheques]
                  left  join bancos  b on cheques.idbanco=b.id
                  left join chequera on cheques.idchequera  = chequera.id 
                  left join cuentas on chequera.idCuenta = cuentas.id
                  left join bancos c on cuentas.idbanco = c.id
                  left join locales on cheques.idlocal = locales.id ";
            }
        }


        public bool Insert(ChequeData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"INSERT INTO [dbo].[cheques]
           ([id]
           ,[idbanco]
           ,[interno]
           ,[Numero]
           ,[Date]
           ,[fechaEmision]
           ,[fechaCobro]
           ,[titular]
           ,[EstadoCheque]
           ,[Monto]
           ,[Enable]
           ,[idlocal]
           ,[Description]
           ,[idchequera],fechaanuladoorechazado)
     VALUES
           (@id
           ,@idbanco
           ,@interno
           ,@Numero
           ,@Date
           ,@fechaEmision
           ,@fechaCobro
           ,@titular
           ,@EstadoCheque
           ,@Monto
           ,@Enable
           ,@idlocal
           ,@Description
           ,@idchequera,@fechaanuladoorechazado);
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql,new
                {
                    id = theObject.ID,
                    idbanco = theObject.BancoEmisor.ID,
                    interno = theObject.Interno,
                    numero = theObject.Numero,
                    Date = theObject.Date,
                    fechaemision = theObject.FechaEmision,
                    fechacobro = theObject.FechaCobro,
                    titular = theObject.Titular,
                    EstadoCheque = theObject.EstadoCheque,
                    monto = theObject.Monto,
                    enable = theObject.Enable,
                    idlocal = theObject.Local.ID,
                    Description = theObject.Description,
                    idchequera = theObject.Chequera.ID,
                    fechaanuladoorechazado=theObject.FechaAnuladooRechazado
                }) > 0;
            }
        }

        public bool Update(ChequeData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"UPDATE [dbo].[cheques]
                   SET 
                      [idbanco] = @idbanco
                      ,[interno] = @interno
                      ,[Numero] = @Numero
                      ,[Date] = @Date
                      ,[fechaEmision] = @fechaEmision
                      ,[fechaCobro] = @fechaCobro
                      ,[titular] = @titular
                      ,[EstadoCheque] = @EstadoCheque
                      ,[Monto] = @Monto
                      ,[Enable] = @Enable
                      ,[idlocal] = @idlocal
                      ,[Description] = @Description
                      ,[idchequera] = @idchequera
                      ,[fechaanuladoorechazado] = @fechaanuladoorechazado
                        WHERE id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql,  new
                {
                    id = theObject.ID,
                    idbanco = theObject.BancoEmisor.ID,
                    interno = theObject.Interno,
                    numero = theObject.Numero,
                    Date = theObject.Date,
                    fechaemision = theObject.FechaEmision,
                    fechacobro = theObject.FechaCobro,
                    titular = theObject.Titular,
                    EstadoCheque = theObject.EstadoCheque,
                    monto = theObject.Monto,
                    enable = theObject.Enable,
                    idlocal = theObject.Local.ID,
                    Description = theObject.Description,
                    idchequera = theObject.Chequera == null ? Guid.Empty : theObject.Chequera.ID,
                    fechaanuladoorechazado = theObject.FechaAnuladooRechazado
                }) > 0;
            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[cheques]
                set enable = 0 where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { id = idObject }) > 0;

            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[cheques]
                set enable = 1 where id = @id;
                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { id = idObject }) > 0;

            }
        }

        public List<ChequeData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql =  DEFAULT_SELECT;
                IEnumerable<ChequeData> resultado = con.Query<ChequeData, BancoData, ChequeraData, CuentaData, BancoData,LocalData,ChequeData>(sql,
                    (cheque, b1, chequera, cuenta, b2,local) =>
                    {


                        cheque.BancoEmisor = b1;
                        if (chequera != null)
                        {
                            chequera.Cuenta = cuenta;
                            cuenta.Banco = b2;
                        }
                        else
                        {
                            chequera = new ChequeraData();
                        }


                        cheque.Chequera = chequera;
                        cheque.Local = local;
                        return cheque;
                    }, splitOn: "id,id,id,id");
                return resultado.ToList();
            }
        }

        public ChequeData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.Query<ChequeData, BancoData, ChequeraData, CuentaData, BancoData, LocalData,ChequeData>(DEFAULT_SELECT + "  Where cheques.id= @id ", (cheque, b1, chequera, cuenta, b2,local) =>
                {

                    cheque.BancoEmisor = b1;
                    if (chequera != null)
                    {
                        chequera.Cuenta = cuenta;
                        cuenta.Banco = b2;
                    }
                    else
                    {
                        chequera=new ChequeraData();
                    }
                    
                    
                    cheque.Chequera = chequera;
                    cheque.Local = local;
                    return cheque;
                }, new { id = idObject }, splitOn: "id,id,id,id").FirstOrDefault() ?? new ChequeData();
            }
        }

        public ChequeData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChequeData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public int ObtenerUltimoInterno()
        {//
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<int>("select ISNULL(max(interno),0) as 'ultimo' from cheques where Enable = '1' ");
            }
        }

        public List<ChequeData> GetByChequera(Guid idChequera)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = DEFAULT_SELECT + " where cheques.idChequera = @idchequera ";
                if (idChequera!=Guid.Empty)
                {
                    sql += " and chequera.id = @idchequera";
                }
                IEnumerable<ChequeData> resultado = con.Query<ChequeData, BancoData, ChequeraData, CuentaData, BancoData,LocalData, ChequeData>(sql,
                    (cheque, b1, chequera, cuenta, b2,local) =>
                    {
                        cheque.BancoEmisor = b1;
                        if (chequera != null)
                        {
                            chequera.Cuenta = cuenta;
                            cuenta.Banco = b2;
                        }
                        else
                        {
                            chequera = new ChequeraData();
                        }


                        cheque.Chequera = chequera;
                        cheque.Local = local;
                        return cheque;
                    }, new { idChequera = idChequera }, splitOn: "id,id,id,id");
                return resultado.ToList();
            }
        }

  
    }
}
