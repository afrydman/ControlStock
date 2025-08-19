using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.VentaRepository
{
   public class VentaDetalleRepository : DbRepository, IVentaDetalleRepository
    {
        public VentaDetalleRepository(bool local=true) : base(local)
        {
        }

        public bool InsertDetalle(VentaDetalleData detalle)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[venta_detalle]
                   ([FatherID]
                   ,[Codigo]
                   ,[precioUnidad],[Cantidad],alicuota,bonificacion)
             VALUES
                   (@FatherID
                   ,@Codigo
                   , @precioUnidad,@Cantidad,@alicuota,@bonificacion);
                    SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {
                    detalle.FatherID,
                    codigo = detalle.Codigo,
                    precioUnidad = detalle.PrecioUnidad,
                    cantidad = detalle.Cantidad,
                    alicuota = detalle.Alicuota,
                    bonificacion = detalle.Bonificacion

                }) > 0;

            }
        }

        public List<VentaDetalleData> GetDetalles(Guid FatherID)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select * from venta_detalle where FatherID = @FatherID";
                IEnumerable<VentaDetalleData> resultado = con.Query<VentaDetalleData>(sql, new { FatherID = FatherID });
                return resultado.ToList();
            }
        }

        public List<VentaDetalleData> GetByCodigoInterno(string codigo)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = string.Format("select * from venta_detalle where Codigo like '{0}%'", codigo);
                IEnumerable<VentaDetalleData> resultado = con.Query<VentaDetalleData>(sql);
                return resultado.ToList();
            }
        }

       public override string DEFAULT_SELECT
       {
           get { throw new NotImplementedException(); }
       }
    }
}
