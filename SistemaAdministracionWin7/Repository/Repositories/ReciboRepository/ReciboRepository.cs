using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO;
using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.ReciboRepository
{
   public class ReciboRepository : DbRepository, IReciboRepository
    {
        public ReciboRepository(bool local=true) : base(local)
        {
        }
        public IEnumerable<ReciboData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            return con.Query<ReciboData, ClienteData, LocalData, PersonalData, CondicionIvaData, ReciboData>(sql,
                     (op, proveedor, local, personal, condicionIva) =>
                     {
                         proveedor.CondicionIva = condicionIva;
                         op.tercero = proveedor;
                         op.Local = local;
                         op.Vendedor = personal;
                         return op;

                     }
                   , parameters);
        }

        public bool Insert(ReciboData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	INSERT INTO [dbo].[recibo]
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
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    id = theObject.ID,
                    date = theObject.Date,
                    numero = theObject.Numero,
                    prefix = theObject.Prefix,
                    idcliente = theObject.tercero.ID,
                    monto = theObject.Monto,
                    enable = theObject.Enable,
                    idlocal = theObject.Local.ID,
                    descuento = theObject.Descuento,
                    iva = theObject.IVA,
                    idpersonal = theObject.Vendedor.ID,
                    Description = theObject.Description


                }) > 0;
            }
        }

        public bool Update(ReciboData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update recibo set Enable = '0' where id  = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id =idObject}) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update recibo set Enable = '1' where id  = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject }) > 0;
            }
        }

        public List<ReciboData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "Select " + @DEFAULT_SELECT;
                IEnumerable<ReciboData> resultado = OperatorGiveMeData(con, sql, null);
                    
                  
                return resultado.ToList();
            }
        }

        public ReciboData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql ="SELECT " + @DEFAULT_SELECT + " Where re.id= @id ";
                ReciboData aux = OperatorGiveMeData(con, sql,  new { id = idObject }).FirstOrDefault();
                  
                return aux ?? new ReciboData();
            }
        }
        //
        public ReciboData GetLast(Guid idLocal, int Prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {

                var sql = @"select  top 1 " + DEFAULT_SELECT +
                          @"  where idlocal = @idlocal and Prefix = @Prefix and Numero = (select  max(Numero) as 'Numero' 
                                        from recibo where idlocal = @idLocal and Prefix = @Prefix  )";
                ReciboData aux = OperatorGiveMeData(con, sql, new { idLocal = idLocal, Prefix = Prefix }).FirstOrDefault();
                
              
                return aux ?? new ReciboData();
            }
        }

    

       public List<ReciboData> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int Prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + @" Where re.idLocal = @idLocal and re.Date>=@fecha1 and re.Date<=@fecha2 and re.Prefix=@Prefix";
                IEnumerable<ReciboData> resultado = OperatorGiveMeData(con, sql, new { idLocal = idLocal, fecha1 = fecha1, fecha2 = fecha2, prefix = Prefix });

                return resultado.ToList();
            }
        }

        public List<ReciboData> GetBiggerThan(int ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + @" Where re.idLocal = @idLocal and re.Numero >@ultimo and re.Prefix = @Prefix";
                IEnumerable<ReciboData> resultado = OperatorGiveMeData(con, sql, new { ultimo = ultimo, idLocal = idLocal, prefix = prefix });

                return resultado.ToList();
            }
        }

        public List<ReciboData> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + @" Where re.idLocal = @idLocal and re.Date >@ultimo and re.Prefix = @Prefix";
                IEnumerable<ReciboData> resultado = OperatorGiveMeData(con, sql,
                    new { ultimo = ultimo, idLocal = idLocal, prefix = prefix });
                   
                return resultado.ToList();
            }
        }

       public override string DEFAULT_SELECT
       {
           get
           {
               return @"  re.*,p.*,locales.*,Personal.* ,condicioniva.*
from recibo re
left join clientes p on re.idCliente = p.id
left join locales on re.idlocal = locales.id
left join Personal on re.idpersonal = Personal.id 
inner join condicioniva on p.idcondicioniva = condicioniva.id
";
           }
       }
    }
}
