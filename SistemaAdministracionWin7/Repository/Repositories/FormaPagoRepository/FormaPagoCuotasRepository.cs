using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;
using Repository.FormaPagoRepository;

namespace Repository.Repositories.FormaPagoRepository
{
    public class FormaPagoCuotasRepository  : DbRepository, IFormaPagoCuotasRepository
    {
        public FormaPagoCuotasRepository(bool local=true) : base(local)
        {
        }

        public bool InsertDetalle(FormaPagoCuotaData ordenPagoDetalle)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"INSERT INTO [dbo].[formasPagoCuotas]
                           ([id]
                           ,[FatherID]
                           ,[cuota]
                           ,[aumento])
                     VALUES
                           (@id,
                           @FatherID,
                           @cuota,
                           @aumento);
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, ordenPagoDetalle) > 0;
            }
        }

        public List<FormaPagoCuotaData> GetDetalles(Guid idPadre)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"Select 
		                    id,FatherID,cuota,aumento	From formaspagoCuotas	where FatherID = @idPadre";
                IEnumerable<FormaPagoCuotaData> resultado = con.Query<FormaPagoCuotaData>(sql, new { idPadre = idPadre });
                return resultado.ToList();
            }
        }

        public bool UpdateAumento(FormaPagoCuotaData f )
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"UPDATE [dbo].[formasPagoCuotas]
                           SET 
                              [aumento] = @aumento
                         WHERE [FatherID] = @FatherID and cuota = @Cuota;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    aumento = f.Aumento,
                    FatherID = f.FatherID,
                    Cuota = f.Cuota

                }) > 0;
            }
        }

        public bool DeleteCuotas(Guid FatherID)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"delete formasPagoCuotas 
                         WHERE [FatherID] = @FatherID;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { FatherID = FatherID }) > 0;
            }
        }

        public override string DEFAULT_SELECT
        {
            get { throw new NotImplementedException(); }
        }
    }
}
