using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.ReciboRepository
{
    public class ReciboDetalleRepository : DbRepository, IReciboDetalleRepository
    {
        public ReciboDetalleRepository(bool local=true) : base(local)
        {
        }

        public bool InsertDetalle(ReciboOrdenPagoDetalleData ordenPagoDetalle)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	INSERT INTO [dbo].[recibo_detalle]
          ([FatherID]
           ,[idcheque]
           ,[Monto],idcuenta)
     VALUES
           (@FatherID
           ,@idcheque
           ,@Monto,@idcuenta);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    fatherId = ordenPagoDetalle.FatherID,
                    idcheque = ordenPagoDetalle.Cheque.ID,
                    monto = ordenPagoDetalle.Monto,
                    idcuenta = ordenPagoDetalle.Cuenta.ID
                }) > 0;
            }
        }

        public List<ReciboOrdenPagoDetalleData> GetDetalles(Guid FatherID)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + "  where recibo_Detalle.FatherID = @FatherID";
                IEnumerable<ReciboOrdenPagoDetalleData> resultado = con.Query<ReciboOrdenPagoDetalleData, ChequeData, CuentaData, ReciboOrdenPagoDetalleData>(sql,
                    (detalle, cheque, cuenta) =>
                    {
                        detalle.Cheque = cheque;
                        detalle.Cuenta = cuenta;
                        return detalle;

                    }, new { FatherID = FatherID });
                return resultado.ToList();
            }
        }
        
        public List<ReciboOrdenPagoDetalleData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT;
                IEnumerable<ReciboOrdenPagoDetalleData> resultado = con.Query<ReciboOrdenPagoDetalleData, ChequeData, CuentaData, ReciboOrdenPagoDetalleData>(sql,
                    (detalle, cheque, cuenta) =>
                    {
                        detalle.Cheque = cheque;
                        detalle.Cuenta = cuenta;
                        return detalle;

                    });
                return resultado.ToList();
            }
        }

        public override string DEFAULT_SELECT
        {
            get
            {
                return @" recibo_Detalle.*, cheques.*, cuentas.* from recibo_Detalle 
left join cheques on recibo_Detalle.idcheque=cheques.id
left join cuentas on recibo_Detalle.idcuenta = cuentas.id ";
            }
        }
    }
}
