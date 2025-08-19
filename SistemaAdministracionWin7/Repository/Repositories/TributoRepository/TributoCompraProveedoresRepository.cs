using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.VentaRepository
{
    public class TributoCompraProveedoresRepository : DbRepository, IGenericChildRepository<TributoNexoData>
    {
        public TributoCompraProveedoresRepository(bool local = true)
            : base(local)
        {
        }

        public bool InsertDetalle(TributoNexoData detalle)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[compraproveedores_tributo]
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
                    "select * from compraproveedores_tributo inner join FE_Tributo on compraproveedores_tributo.idTributo = FE_Tributo.id where FatherID = @FatherID";

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

