using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.PuntoControlStockRepository
{
    public class PuntoControlStockDetalleRepository : DbRepository, IGenericChildRepository<PuntoControlStockDetalleData>, IPuntoControlStockDetalleRepository
    {
        public PuntoControlStockDetalleRepository(bool local = true)
            : base(local)
        {
        }

        public override string DEFAULT_SELECT
        {
            get { throw new NotImplementedException(); }
        }


        public bool InsertDetalle(PuntoControlStockDetalleData PuntoControlStockDetalle)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[puntosControl_detalle]
           ([FatherID]
           ,[Codigo],Cantidad)
     VALUES
           (@FatherID
          ,@Codigo,@Cantidad);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, PuntoControlStockDetalle) > 0;

            }
        }

        public List<PuntoControlStockDetalleData> GetDetalles(Guid idPadre)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT [FatherID] ,[Codigo],Cantidad FROM [puntosControl_detalle] Where FatherID = @FatherID ";
                IEnumerable<PuntoControlStockDetalleData> resultado = con.Query<PuntoControlStockDetalleData>(sql, new { FatherID = idPadre });
                return resultado.ToList();
            }
        }
    }
}
