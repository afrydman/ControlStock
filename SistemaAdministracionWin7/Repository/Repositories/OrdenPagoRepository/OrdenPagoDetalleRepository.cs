using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.OrdenPagoRepository
{
    public class OrdenPagoDetalleRepository : DbRepository,IOrdenPagoDetalleRepository
    {
        public OrdenPagoDetalleRepository(bool local=true) : base(local)
        {
        }

        public bool InsertDetalle(ReciboOrdenPagoDetalleData ordenPagoDetalle)
        {
            
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[opago_detalle]
           ([FatherID]
           ,[idcheque]
           ,[Monto],idcuenta)
     VALUES
           (@FatherID
           ,@idcheque
           ,@Monto,@idcuenta);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
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
                var sql = "select " + DEFAULT_SELECT + "  where opago_detalle.FatherID = @FatherID";
                IEnumerable<ReciboOrdenPagoDetalleData> resultado = con.Query<ReciboOrdenPagoDetalleData, ChequeData, CuentaData, ReciboOrdenPagoDetalleData>(sql,
                    (detalle, cheque, cuenta) =>
                    {
                        if(cheque==null)
                            cheque = new ChequeData();
                        if(cuenta==null)
                            cuenta = new CuentaData();
                        detalle.Cheque = cheque;
                        detalle.Cuenta = cuenta;
                        return detalle;

                    },new { FatherID = FatherID });
                return resultado.ToList();
            }

        }

        public override string DEFAULT_SELECT
        {
            get
            {
                return @" opago_detalle.*, cheques.*, cuentas.* from opago_detalle 
left join cheques on opago_detalle.idcheque=cheques.id
left join cuentas on opago_detalle.idcuenta = cuentas.id ";
            }
        }
    }
}
