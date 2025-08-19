using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.PedidoRepository
{
   public class PedidoDetalleRepository : DbRepository, IPedidoDetalleRepository
    {
        public PedidoDetalleRepository(bool local=true) : base(local)
        {
        }

        public bool InsertDetalle(pedidoDetalleData ordenPagoDetalle)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[pedido_detalle]
           ([idpedido]
           ,[Codigo],precio
           ,[Cantidad])
     VALUES
           (@idpedido
           ,@Codigo,@precio
           ,@Cantidad);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, ordenPagoDetalle) > 0;

            }
        }

        public List<pedidoDetalleData> GetDetalles(Guid idPadre)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"SELECT *
                              FROM pedido_detalle where idpedido = @idPadre
	                           ";
                IEnumerable<pedidoDetalleData> resultado = con.Query<pedidoDetalleData>(sql, new { idPadre = idPadre });
                return resultado.ToList();
            }
        }

        public List<pedidoDetalleData> GetbyCodigoInterno(string codigo)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"SELECT *
                              FROM pedido_detalle where Codigo like Codigo
	                           ";
                IEnumerable<pedidoDetalleData> resultado = con.Query<pedidoDetalleData>(sql, new { codigo = codigo });
                return resultado.ToList();
            }
        }

       public override string DEFAULT_SELECT
       {
           get { throw new NotImplementedException(); }
       }
    }
}
