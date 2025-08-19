using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.PrecioRepository
{
    public class PrecioRepository : DbRepository, IPrecioRepository
    {
        public PrecioRepository(bool local=true) : base(local)
        {
        }

        public bool Insert(ListaPrecioProductoTalleData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[lista_precio_productotalle]
           ([FatherID]
           ,[idproductoTalle]
           ,[precio])
     VALUES
           (@FatherID
           ,@idproductoTalle
           ,@precio);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {
                    idproductoTalle=theObject.ProductoTalle.ID,
                    precio = theObject.Precio,
                    FatherID = theObject.FatherID//la lista precio.
                }) > 0;

            }
        }

        public bool Update(ListaPrecioProductoTalleData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"UPDATE [dbo].[lista_precio_productotalle]
   SET 
      [precio] = @precio
where FatherID = @FatherID and idproductoTalle = @idproductotalle;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {
                    idproductoTalle = theObject.ProductoTalle.ID,
                    precio = theObject.Precio,
                    FatherID = theObject.FatherID
                }) > 0;

            }
        }

        public bool Disable(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public bool Enable(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public List<ListaPrecioProductoTalleData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"SELECT "+DEFAULT_SELECT;
                IEnumerable<ListaPrecioProductoTalleData> resultado = con.Query<ListaPrecioProductoTalleData, ProductoTalleData, ListaPrecioProductoTalleData>(sql,
                    (precio, productotalle) =>
                    {
                        precio.ProductoTalle = productotalle;
                        return precio;

                    });
                return resultado.ToList();
            }
        }

        public ListaPrecioProductoTalleData GetByID(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public ListaPrecioProductoTalleData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListaPrecioProductoTalleData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public ListaPrecioProductoTalleData GetPrecio(Guid FatherID, Guid idProductoTalle)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"SELECT " + DEFAULT_SELECT + " where lista_precio_productotalle.FatherID = @FatherID and lista_precio_productotalle.idproductoTalle = @idProductoTalle ";
                return con.Query<ListaPrecioProductoTalleData, ProductoTalleData, ListaPrecioProductoTalleData>(sql,
                    (precio, productotalle) =>
                    {
                        precio.ProductoTalle = productotalle;
                        return precio;

                    },
                    new
                    {
                        FatherID = FatherID, 
                        idProductoTalle = idProductoTalle 
                    
                    }).FirstOrDefault()?? new ListaPrecioProductoTalleData();
            }
        }

        public override string DEFAULT_SELECT
        {
            get
            {
                return @"  lista_precio_productotalle.FatherID,lista_precio_productotalle.idproductoTalle,lista_precio_productotalle.precio,
 producto_talle.id,producto_talle.idProducto,producto_talle.talle
FROM lista_precio_productotalle
inner join producto_talle on lista_precio_productotalle.idproductoTalle = producto_talle.id ";
            }
        }
    }
}
