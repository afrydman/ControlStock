using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ComprasProveedoresRepository;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.ComprasProveedoresRepository
{
    public class CompraProveedoresDetalleRepository : DbRepository, ICompraProveedoresDetalleRepository
    {
        public CompraProveedoresDetalleRepository(bool local=true) : base(local)
        {
        }

        public bool InsertDetalle(ComprasProveedoresdetalleData ordenPagoDetalle)
        {
            
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	INSERT INTO [dbo].[comprasProveedores_detalle]
                                ([fatherid],[Codigo],Cantidad,preciounidad,precioextra,alicuota,[bonificacion])
                                VALUES
                                (@fatherid,@Codigo,@Cantidad,@preciounidad,@precioextra,@alicuota,@bonificacion);
                                 
                                SELECT @@ROWCOUNT;";

                return con.QueryFirstOrDefault<int>(sql, ordenPagoDetalle) > 0;
            }
        }

        public List<ComprasProveedoresdetalleData> GetDetalles(Guid idPadre)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = DEFAULT_SELECT + @" Where
                            FatherID = @id";
                IEnumerable<ComprasProveedoresdetalleData> resultado = con.Query<ComprasProveedoresdetalleData>(sql, new { id = idPadre });
                return resultado.ToList();
            }
        }

   

        public override string DEFAULT_SELECT
        {
            get { return @"SELECT *
                            FROM [comprasProveedores_detalle] "; }
        }
    }
}
