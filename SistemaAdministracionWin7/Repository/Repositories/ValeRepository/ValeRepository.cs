using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.ValeRepository
{
    public class ValeRepository : DbRepository, IValeRepository
    {
        public ValeRepository(bool local=true) : base(local)
        {
        }

        public bool Insert(valeData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[vales]
           ([id],[Numero],[Date],[esCambio],[idAsoc],[Monto],[Enable],[idlocal],Prefix,Description,idpersonal)
     VALUES
           (@id
           ,@Numero
           ,@Date
           ,@esCambio
           ,@idAsoc
           ,@Monto
           ,@Enable
           ,@idlocal,@Prefix,@Description,@idpersonal);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {
                    theObject.ID,
                    theObject.Numero,
                    theObject.Date,
                    esCambio = theObject.EsCambio,
                    theObject.idAsoc,
                    monto = theObject.Monto,
                    theObject.Enable,
                    idlocal=theObject.Local.ID,
                    theObject.Prefix,
                    theObject.Description,
                    idpersonal=theObject.Personal.ID

                }) > 0;

            }
        }

        public bool Update(valeData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"update vales set Enable = '0' where id = @id;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new{id = idObject}) > 0;

            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"update vales set Enable = '1' where id = @id;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { id = idObject }) > 0;

            }
        }

        public List<valeData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	select  " + DEFAULT_SELECT;
                IEnumerable<valeData> resultado = con.Query<valeData, LocalData, PersonalData, valeData>(sql,
                   (vale, local, personal) =>
                   {
                       vale.Local = local;
                       vale.Personal = personal;
                       return vale;
                   });
                return resultado.ToList();
            }
        }

        public valeData GetByID(Guid idObject)
        {
            var sql = "SELECT  " + DEFAULT_SELECT + @" Where vales.id= @id ";
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                valeData aux = con.Query<valeData, LocalData, PersonalData, valeData>(sql,
                   (vale, local, personal) =>
                   {
                       vale.Local = local;
                       vale.Personal = personal;
                       return vale;
                   },
                    new { id = idObject }).FirstOrDefault();

                return aux ?? new valeData();
            }
        }

        public valeData GetLast(Guid idLocal, int first)
        {

            var sql = "select  top 1   " + DEFAULT_SELECT + @" where idlocal = @idLocal and Prefix = @Prefix 
                and vales.Numero= ( select  max(Numero) as 'Numero' from vales where idlocal = @idLocal and Prefix = @Prefix )";
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {

                valeData aux = con.Query<valeData, LocalData, PersonalData, valeData>(sql,
                   (vale, local, personal) =>
                   {
                       vale.Local = local;
                       vale.Personal = personal;
                       return vale;
                   },
                   new { idLocal = idLocal, prefix = first }).FirstOrDefault();

                return aux ?? new valeData();
                

            }
        }

        public IEnumerable<valeData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public List<valeData> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int Prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	select "+DEFAULT_SELECT+@"
                                    where
                                    vales.Date >= @fechaAyer and vales.Date <= @fechaMan  and vales.idLocal = @idLocal  and vales.Prefix = @Prefix";
                IEnumerable<valeData> resultado = con.Query<valeData, LocalData, PersonalData, valeData>(sql,
                   (vale, local, personal) =>
                   {
                       vale.Local = local;
                       vale.Personal = personal;
                       return vale;
                   }, new { fechaAyer = fecha1, fechaMan = fecha2, idLocal = idLocal,prefix= Prefix });
                return resultado.ToList();
            }
        }

        public List<valeData> GetBiggerThan(int ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	select " + DEFAULT_SELECT + @"
                                    where
                                    vales.Numero > @ultimo and vales.idLocal = @idLocal and vales.Prefix = @Prefix";
                IEnumerable<valeData> resultado = con.Query<valeData, LocalData, PersonalData, valeData>(sql,
                   (vale, local, personal) =>
                   {
                       vale.Local = local;
                       vale.Personal = personal;
                       return vale;
                   }, new { ultimo = ultimo,  idLocal = idLocal , prefix = prefix });
                return resultado.ToList();
            }
        }

        public List<valeData> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	select " + DEFAULT_SELECT + @"
                                    where
                                    vales.Date > @ultimo and vales.idLocal = @idLocal and vales.Prefix = @Prefix";
                IEnumerable<valeData> resultado = con.Query<valeData, LocalData, PersonalData, valeData>(sql,
                   (vale, local, personal) =>
                   {
                       vale.Local = local;
                       vale.Personal = personal;
                       return vale;
                   }, new { ultimo = ultimo, idLocal = idLocal, prefix = prefix });
                return resultado.ToList();
            }
        }

        public valeData GetbyVenta(Guid idventa)
        {

            var sql = "select " + DEFAULT_SELECT + " where vales.idAsoc = @idventa";
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {

                valeData aux = con.Query<valeData, LocalData,PersonalData, valeData>(sql,
                   (vale, local,personal) =>
                   {
                       vale.Local = local;
                       vale.Personal = personal;
                       return vale;
                   },
                   new { idventa = idventa}).FirstOrDefault();

                return aux ?? new valeData();


            }
        }

        public override string DEFAULT_SELECT
        {
            get { return @"  vales.*, locales.*,personal.*
                            FROM [vales]
                            left join locales on vales.idlocal = locales.id
                            left join personal on vales.idpersonal = personal.id"
                            ; }
        }
    }
}
