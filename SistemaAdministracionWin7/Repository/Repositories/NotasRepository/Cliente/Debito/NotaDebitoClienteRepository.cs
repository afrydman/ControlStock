using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO;
using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.NotasRepository
{
    public class NotaDebitoClienteRepository: DbRepository,INotaRepository
    {
        public NotaDebitoClienteRepository(bool local = true)
            : base(local)
        {
        }

        public IEnumerable<NotaData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            return con.Query<NotaData, LocalData, PersonalData, ClienteData, CondicionIvaData, NotaData>(sql,
                (nota, local, personal, cliente, condicionIva) =>
                {
                    nota.Local = local;
                    nota.Vendedor = personal;
                    cliente.CondicionIva = condicionIva;
                    nota.tercero = cliente;
                    return nota;
                }, parameters);
        }

        public bool Insert(NotaData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[notadebitoClientes]
           ([id]
           ,[idlocal]
           ,[Numero]
           ,[Prefix]
           ,[ClaseDocumento]
           ,[idpersonal]
           ,[Date]
           ,[Enable]
           ,[IVA]
           ,[Monto]
           ,[Description]
           ,[idcliente],descuento,CAE,CAEVto)
     VALUES
           (@id
           ,@idlocal
           ,@Numero
           ,@Prefix
           ,@ClaseDocumento
           ,@idpersonal
           ,@Date
           ,@Enable
           ,@IVA
           ,@Monto
           ,@Description
           ,@idcliente,@descuento,@CAE,@CAEVto);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {
                    id = theObject.ID,
                    idLocal = theObject.Local.ID,
                    numero = theObject.Numero,
                    prefix = theObject.Prefix,
                    ClaseDocumento =theObject.ClaseDocumento,
                    idpersonal = theObject.Vendedor.ID,
                    theObject.Date,
                    theObject.Enable,
                    iva = theObject.IVA,
                    monto = theObject.Monto,
                    theObject.Description,
                    idcliente = theObject.tercero.ID,
                    descuento = theObject.Descuento,
                    CAE = theObject.CAE,
                    CAEVto = theObject.CAEVto
                }) > 0;

            }
        }

        public bool Update(NotaData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update notaDebitoclientes set Enable = '0' where id  = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject }) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public List<NotaData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT;
                IEnumerable<NotaData> resultado = OperatorGiveMeData(con, sql, null);
                  

                List<NotaData> resultado2 = resultado.ToList();

                resultado2.ForEach(x => x.tipo = tipoNota.DebitoCliente);

                return resultado2;
            }
        }

        public NotaData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT + "  Where nota.id= @id ";
                NotaData aux = OperatorGiveMeData(con, sql,  new { id = idObject }).FirstOrDefault();


                if (aux == null) return new NotaData();
                aux.tipo = tipoNota.DebitoCliente;
                return aux;
            }
        }

        public NotaData GetLast(Guid idLocal, int Prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"select top 1 " + DEFAULT_SELECT + @"   where nota.Prefix=@Prefix and nota.idlocal=@idlocal  and nota.Numero = (
                            select max(Numero) as 'Numero' from notaDebitoclientes where Prefix=@Prefix and idlocal=@idLocal ) ";

                NotaData aux = OperatorGiveMeData(con, sql, new {idLocal = idLocal, Prefix = Prefix}).FirstOrDefault();

                if (aux == null) return new NotaData();
                aux.tipo = tipoNota.DebitoCliente;
                return aux;
            }
        }

   
        public List<NotaData> GetbyTercero(Guid idCliente)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + @" where nota.idcliente = @idCliente";
                IEnumerable<NotaData> resultado = OperatorGiveMeData(con, sql, new {idCliente = idCliente});

                    
                List<NotaData> resultado2 = resultado.ToList();

                resultado2.ForEach(x => x.tipo = tipoNota.DebitoCliente);

                return resultado2;
            }
        }

        public override string DEFAULT_SELECT
        {
            get
            {
                return @"  nota.*, locales.*,Personal.*, clientes.*, condicioniva.*
                        from notaDebitoclientes nota
                        inner join locales on nota.idlocal = locales.id
                        inner join Personal on nota.idpersonal = Personal.id
                        inner join clientes on nota.idcliente = clientes.id 
                        inner join condicioniva on clientes.idcondicioniva = condicioniva.id";
            }
        }

        public List<NotaData> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int Prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + @" Where nota.idLocal = @idLocal and nota.Date >= @fecha1 and nota.Date <= @fecha2 and nota.Prefix = @Prefix";
                IEnumerable<NotaData> resultado = OperatorGiveMeData(con, sql, new { fecha1 = fecha1, idLocal = idLocal, prefix = Prefix, fecha2 = fecha2 });
                    
                return resultado.ToList();
            }
        }

        public List<NotaData> GetBiggerThan(int ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + @" Where nota.idLocal = @idLocal and nota.Numero >@ultimo and nota.Prefix = @Prefix";
                IEnumerable<NotaData> resultado = OperatorGiveMeData(con, sql, new { ultimo = ultimo, idLocal = idLocal, prefix = prefix });
                    
                return resultado.ToList();
            }
        }

        public List<NotaData> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + @" Where nota.idLocal = @idLocal and nota.Date >@ultimo and nota.Prefix = @Prefix";
                IEnumerable<NotaData> resultado = OperatorGiveMeData(con, sql,  new { ultimo = ultimo, idLocal = idLocal, prefix = prefix });
                   
                return resultado.ToList();
            }
        }
    }
}
