using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.NotasRepository
{
   public  class NotaCreditoProveedoresDetalleRepository : DbRepository, INotaDetalleRepository
    {
        public NotaCreditoProveedoresDetalleRepository(bool local = true)
            : base(local)
        {
        }

        public bool InsertDetalle(NotaDetalleData ordenPagoDetalle)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[notaCreditoProveedores_detalles]
           ([FatherID]
           ,[Description]
           ,[cantidad]
           ,[PrecioUnidad],alicuota,bonificacion)
     VALUES
           (@FatherID
           ,@Description
           ,@cantidad
           ,@PrecioUnidad,@alicuota,@bonificacion);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, ordenPagoDetalle) > 0;

            }
        }

        public List<NotaDetalleData> GetDetalles(Guid idnota)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select * from notaCreditoProveedores_detalles where FatherID = @FatherID";
                IEnumerable<NotaDetalleData> resultado = con.Query<NotaDetalleData>(sql, new { FatherID = idnota });
                return resultado.ToList();
            }
        }

       public override string DEFAULT_SELECT
       {
           get { throw new NotImplementedException(); }
       }
    }
}
