using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.VentaRepository
{
    public class TributoVentaRepository : DbRepository, IGenericChildRepository<TributoNexoData>
    {
        public TributoVentaRepository(bool local = true)
            : base(local)
        {
        }

        public bool InsertDetalle(TributoNexoData detalle)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[venta_tributo]
                   ([FatherID]
                   ,[idTributo]
                   ,[importe],Alicuota,Base)
             VALUES
                   (@FatherID
                   ,@idTributo
                   ,@importe,@alicuota,@base);
                    SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {
                    detalle.FatherID,
                    idTributo = detalle.Tributo.ID,
                    detalle.Importe,
                    Alicuota = detalle.Alicuota,
                    Base = detalle.Base
                }) > 0;

            }
        }

        public List<TributoNexoData> GetDetalles(Guid FatherID)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {


                var sql =
                    "select * from venta_tributo inner join FE_Tributo on venta_tributo.idTributo = FE_Tributo.id where FatherID = @FatherID";

                IEnumerable<TributoNexoData> resultado = con.Query<TributoNexoData, TributoData, TributoNexoData>(
                    sql,
                    (tributoVenta, tributo) =>
                    {
                        tributoVenta.Tributo = tributo;

                        return tributoVenta;
                    }, new { FatherID = FatherID });
                return resultado.ToList();
            }
        }


        public override string DEFAULT_SELECT
        {
            get { throw new NotImplementedException(); }
        }
    }

}

