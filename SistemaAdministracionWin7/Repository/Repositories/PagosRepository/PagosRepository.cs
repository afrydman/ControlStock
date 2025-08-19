using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.PagosRepository
{
    public class PagosRepository : DbRepository, IPagosRepository
    {
        public PagosRepository(bool local=true) : base(local)
        {
        }


        public bool InsertDetalle(PagoData ordenPagoDetalle)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[pagos]
           (
           [idFormaPago]
           ,[recargo]
           ,[lote]
           ,[cupon]
           ,[importe],FatherID,cuotas)
     VALUES
           (
            @idformapago
           ,@recargo
           ,@lote
           ,@cupon
           ,@importe,@FatherID,@cuotas);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio,
                new{
                    
                    idformapago = ordenPagoDetalle.FormaPago.ID,
                    recargo = ordenPagoDetalle.Recargo,
                    lote = ordenPagoDetalle.Lote,
                    cupon = ordenPagoDetalle.Cupon,
                    importe = ordenPagoDetalle.Importe,
                    ordenPagoDetalle.FatherID,
                    ordenPagoDetalle.Cuotas
                }
            ) > 0;

            }
        }

        public List<PagoData> GetDetalles(Guid FatherID)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "Select " + DEFAULT_SELECT + " where pagos.FatherID = @FatherID";
                IEnumerable<PagoData> resultado = con.Query<PagoData, FormaPagoData,PagoData>(sql,
                    (pago, formapago) =>
                    {
                        pago.FormaPago = formapago;
                        return pago;
                    }, new { FatherID = FatherID });
                return resultado.ToList();
            }
        }

        public List<PagoData> GetPagosByTipo(Guid idTipoPago)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + " where pagos.idFormaPago = @idTipoPago";
                IEnumerable<PagoData> resultado = con.Query<PagoData, FormaPagoData, PagoData>(sql, 
                     (pago, formapago) =>
                    {
                        pago.FormaPago = formapago;
                        return pago;
                    },
                    new { idTipoPago = idTipoPago });
                return resultado.ToList();
            }
        }

        public override string DEFAULT_SELECT
        {
            get
            {
                return @" [pagos].[cuotas],[pagos].[idFormaPago],[pagos].[recargo],[pagos].[lote],[pagos].[cupon],[pagos].[importe],[pagos].[FatherID],
		formaspago.id,formaspago.description,formaspago.enable,formaspago.credito
  FROM [dbo].[pagos]
  inner join formaspago on pagos.idFormaPago = formaspago.id  ";
            }
        }
    }
}
