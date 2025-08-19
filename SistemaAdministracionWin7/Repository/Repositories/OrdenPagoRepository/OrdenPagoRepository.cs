using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO;
using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.OrdenPagoRepository
{
    public class OrdenPagoRepository : DbRepository, IOrdenPagoRepository
    {
        public OrdenPagoRepository(bool local=true) : base(local)
        {
        }

        public bool Insert(OrdenPagoData theObject)
        {
            
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[opago]
           ([id]
           ,[Date]
           ,[Numero]
           ,[Prefix]
           ,[idCliente]
           ,[Monto]
           ,[Enable],idlocal,descuento,IVA,idpersonal,Description)
     VALUES
           (@id
           ,@Date
           ,@Numero
           ,@Prefix
           ,@idCliente
           ,@Monto
           ,@Enable,@idlocal,@descuento,@IVA,@idpersonal,@Description);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {
                    id = theObject.ID,
                    date = theObject.Date,
                    numero = theObject.Numero,
                    prefix = theObject.Prefix,
                    idcliente = theObject.Tercero.ID,
                    monto = theObject.Monto,
                    enable = theObject.Enable,
                    idlocal=theObject.Local.ID,
                    descuento = theObject.Descuento,
                    iva = theObject.IVA,
                    idpersonal = theObject.Vendedor.ID,
                    Description = theObject.Description

                }) > 0;

            }
        }

        public bool Update(OrdenPagoData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update opago set Enable = '0' where id  = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id =idObject }) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update opago set Enable = '1' where id  = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject }) > 0;
            }
        }

        public List<OrdenPagoData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "Select " + @DEFAULT_SELECT;
                IEnumerable<OrdenPagoData> resultado = OperatorGiveMeData(con, sql,null);
                 
                return resultado.ToList();
            }
        }

        public OrdenPagoData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + @DEFAULT_SELECT + "Where op.id= @id ";
                OrdenPagoData aux = OperatorGiveMeData(con, sql, new { id = idObject }).FirstOrDefault();
                    
                return aux ?? new OrdenPagoData();
            }
        }

        public OrdenPagoData GetLast(Guid idLocal, int first)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql =@"SELECT  top 1" + @DEFAULT_SELECT + @" where op.idlocal = @idlocal and op.Prefix = @Prefix and op.Numero = 
                                            (select  max(Numero) as 'Numero' from opago where idlocal = @idlocal and Prefix = @Prefix  ) ";
                return OperatorGiveMeData(con, sql, new { idlocal = idLocal, prefix = first }).FirstOrDefault() ?? new OrdenPagoData();
                    
                 
            }
        }

        public IEnumerable<OrdenPagoData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            return con.Query<OrdenPagoData, ProveedorData, LocalData, PersonalData, CondicionIvaData,OrdenPagoData>(sql,
                  (op, proveedor, local, personal,condicionIVa) =>
                  {
                      proveedor.CondicionIva = condicionIVa;
                     op.Tercero = proveedor;
                     op.Local = local;
                     op.Vendedor = personal;
                     return op;
                 }
                 , parameters);
        }

        public OrdenPagoData getOrdenByCheque(Guid idcheque)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"SELECT " + @DEFAULT_SELECT + @" where op.id = (
                                            select FatherID from opago_detalle where idcheque = @idcheque) ";

                return OperatorGiveMeData(con,sql,new { idcheque = idcheque }).FirstOrDefault() ?? new OrdenPagoData();
                
            }
        }


        public List<OrdenPagoData> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int Prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"SELECT " + DEFAULT_SELECT + @"
	                            Where
                             op.idLocal = @idlocal and op.Date>=@desde and op.Date<=@hasta and op.Prefix=@Prefix";
                IEnumerable<OrdenPagoData> resultado = OperatorGiveMeData(con, sql,
                     new { idlocal = idLocal, desde = fecha1, hasta = fecha2 ,prefix=Prefix});

                  
                return resultado.ToList();
            }
        }

        public List<OrdenPagoData> GetBiggerThan(int ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"SELECT " + DEFAULT_SELECT+ @"
                              where
                             op.idLocal = @idlocal and op.Numero>@ultimo and op.Prefix = @Prefix";
                IEnumerable<OrdenPagoData> resultado = OperatorGiveMeData(con, sql,new {ultimo = ultimo, idLocal = idLocal, prefix = prefix});

                return resultado.ToList();
            }
        }

        public List<OrdenPagoData> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"SELECT " + DEFAULT_SELECT + @"
                              where
                             op.idLocal = @idlocal and op.Date>@ultimo and op.Prefix = @Prefix";
                IEnumerable<OrdenPagoData> resultado = OperatorGiveMeData(con, sql, new { ultimo = ultimo, idLocal = idLocal, prefix = prefix });
                  
                return resultado.ToList();
            }
        }

        public override string DEFAULT_SELECT
        {
            get
            {
                return @"  op.*,p.*,locales.*,Personal.*, condicioniva.*
from opago op
left join proveedores p on op.idCliente = p.id
left join locales on op.idlocal = locales.id
left join Personal on op.idpersonal = Personal.id
 inner join condicioniva on p.idcondicioniva = condicioniva.id
";
            }
        }
    }
}
