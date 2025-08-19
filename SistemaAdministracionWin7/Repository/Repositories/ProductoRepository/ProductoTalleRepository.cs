using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.ProductoRepository
{
    public class ProductoTalleRepository:DbRepository,IProductoTalleRepository
    {
        public ProductoTalleRepository(bool local = true)
            : base(local)
        {
        }

        public bool Insert(ProductoTalleData theObject)
        {
            
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"
INSERT INTO [dbo].[producto_talle]
           ([id]
           ,[idProducto]
           ,[talle])
     VALUES
           (@id
           ,@idProducto
           ,@talle)
;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, theObject) > 0;

            }
        }

        public List<ProductoTalleData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"select * from [producto_talle]";
                IEnumerable<ProductoTalleData> resultado = con.Query<ProductoTalleData>(sql);
                return resultado.ToList();
            }
        }

        public List<ProductoTalleData> GetByProducto(Guid idProducto)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                IEnumerable<ProductoTalleData> resultado = con.Query<ProductoTalleData>(@"SELECT [id]
                                              ,[idProducto]
                                              ,[talle]
                                          FROM [dbo].[producto_talle]
                                        where idproducto = @id", new { id = idProducto });
                return resultado.ToList();
            }
        }




        public ProductoTalleData GetByProductoTalle(Guid idProducto, int talle)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<ProductoTalleData>(@"SELECT [id]
                                              ,[idProducto]
                                              ,[talle]
                                          FROM [dbo].[producto_talle]
                                        where idproducto = @id and talle = @talle", new { id = idProducto, talle = talle }) ?? new ProductoTalleData();
            }
        }

        public override string DEFAULT_SELECT
        {
            get { throw new NotImplementedException(); }
        }
    }
}
