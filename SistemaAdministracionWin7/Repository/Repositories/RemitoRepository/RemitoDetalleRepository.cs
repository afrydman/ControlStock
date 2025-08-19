using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.RemitoRepository
{
    public class RemitoDetalleRepository : DbRepository, IGenericChildRepository<remitoDetalleData>, IRemitoDetalleRepository
    {
        public RemitoDetalleRepository(bool local = true)
            : base(local)
        {
        }
        public bool InsertDetalle(remitoDetalleData ordenPagoDetalle)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[remito_detalle]
           ([FatherID]
           ,[Codigo],Cantidad)
     VALUES
           (@FatherID
          ,@Codigo,@Cantidad);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, ordenPagoDetalle) > 0;

            }
        }

        public List<remitoDetalleData> GetDetalles(Guid idPadre)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT [FatherID] ,[Codigo],Cantidad FROM [remito_detalle] Where FatherID = @FatherID ";
                IEnumerable<remitoDetalleData> resultado = con.Query<remitoDetalleData>(sql, new { FatherID = idPadre });
                return resultado.ToList();
            }
        }

        public List<remitoDetalleData> getbyProveedor(string codProveedor)
        {
            throw new NotImplementedException();
        }

        public override string DEFAULT_SELECT
        {
            get { throw new NotImplementedException(); }
        }
    }
}
