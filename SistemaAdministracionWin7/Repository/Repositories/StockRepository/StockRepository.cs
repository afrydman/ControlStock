using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.StockRepository
{
   public  class StockRepository : DbRepository, IStockRepository
    {
        public StockRepository(bool local=true) : base(local)
        {
        }



        public IEnumerable<StockData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            return
                con
                    .Query
                    <StockData, LocalData, ColorData, ProductoData, ProveedorData, LineaData, TemporadaData, StockData>(
                        sql,
                        (stock, local, color, producto, proveedor, linea, temporada) =>
                        {
                            stock.Local = local;
                            stock.Color = color;
                            producto.Proveedor = proveedor;
                            producto.Linea = linea;
                            producto.Temporada = temporada;
                            stock.Producto = producto;
                            return stock;
                        },
                        parameters);
        }

        public StockData GetStock(Guid idProducto, Guid idColor, int talle, Guid idLocal)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + DEFAULT_SELECT + " Where stock.[idlocal] = @idlocal and stock.[idproducto]=@idproducto and stock.[idcolor]=@idcolor and stock.[talle]=@talle ";

                StockData aux = OperatorGiveMeData(con,sql,new { idlocal = idLocal, idproducto = idProducto, idcolor = idColor, talle = talle }).FirstOrDefault();

                return aux ?? new StockData();
            }
        }

        public bool UpdateStock(Guid idProducto, Guid idColor, int talle, Guid idLocal, decimal newStock)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"Update stock
	                        Set
		                        [stock] = @stock
	                        Where		
		                        [idlocal] = @idlocal and
		                        [idproducto] = @idproducto  and
		                        [idcolor] = @idcolor and
		                        [talle] = @talle;
                                SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { idlocal = idLocal, idproducto = idProducto, idcolor = idColor, talle = talle, stock =newStock}) > 0;

            }
        }

        public List<StockData> GetAllbyLocalAndProducto(Guid idlocal, Guid idProducto)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + " where stock.idlocal= @idlocal and stock.idproducto=@idproducto";

                IEnumerable<StockData> resultado = OperatorGiveMeData(con, sql, new { idlocal = idlocal, idproducto = idProducto });
                    
                return resultado.ToList();
            }
        }

        public List<StockData> GetAllbyProducto(Guid idproducto)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + " where stock.[idproducto]=@idproducto";
                IEnumerable<StockData> resultado = OperatorGiveMeData(con, sql, new { idproducto = idproducto });
                    
                return resultado.ToList();
            }
        }

        public bool InsertStock(Guid idProducto, Guid idColor, int talle, Guid idLocal, decimal newStock)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"Insert Into stock
		([idlocal],[idproducto],[idcolor],[talle],[stock],id)
	Values
		(@idlocal,@idproducto,@idcolor,@talle,@stock,newid());
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new{idlocal=idLocal, idproducto=idProducto, idcolor=idColor,talle=talle,stock=newStock}) > 0;
            
            }

         }
        

        public List<detalleStockData> GetDetalleStock(string codigo, Guid idlocal)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"--baja de remitos
                            select remito_detalle.codigo ,remitos.Date as fecha,'Baja Stock x Baja Stock - Remito ' + CAST(remitos.numero AS VARCHAR)as descripcion,-1*Cantidad as Cantidad
                            from remito_detalle 
                            inner join remitos on remito_detalle.FatherID = remitos.ID 
                            where remito_detalle.codigo like @Codigo and  idLocalO = @idlocal and idLocalD != @idlocal and remitos.Enable = '1'
                            union all
                            
                            --alta por cambio
                            select venta_detalle.Codigo,ventas.Date as fecha, 'Alta Stock x Ingreso cambio' as descripcion,-1*venta_detalle.Cantidad as Cantidad
                            from venta_detalle 
                            inner join ventas on venta_detalle.FatherID = ventas.ID 
                            where venta_detalle.Codigo like @Codigo and Cantidad < 0 and ventas.IDlocal = @idlocal and ventas.Enable = '1'
                            union all
                            
                            --baja por venta
                            select venta_detalle.Codigo,ventas.Date as fecha,'Baja Stock x venta' as descripcion ,-1*venta_detalle.Cantidad  as Cantidad
                            from venta_detalle 
                            inner join ventas on venta_detalle.FatherID = ventas.ID 
                            where venta_detalle.Codigo like @Codigo and Cantidad > 0 and ventas.IDlocal = @idlocal and ventas.Enable = '1'
                            union all
                            
                            --alta por remito
                            select remito_detalle.codigo ,remitos.Date as fecha ,'Alta Stock x Alta Stock - Remito ' + CAST(remitos.numero AS VARCHAR) as descripcion  ,Cantidad
                            from remito_detalle 
                            inner join remitos on remito_detalle.FatherID = remitos.ID 
                            where remito_detalle.codigo like @Codigo and  idLocalD = @idlocal and fechaRecibo > '2000-01-01' and remitos.Enable = '1'

                            union all

                            select puntosControl_detalle.codigo as codigo, puntosControl.Date as fecha, 'Punto de control - '+ CAST(puntosControl.prefix AS VARCHAR) +'-'+ CAST(puntosControl.numero AS VARCHAR) as descripcion ,puntosControl_detalle.cantidad
                            from puntosControl
                            inner join puntosControl_detalle on puntosControl.id = puntosControl_detalle.FatherID
                            where puntosControl_detalle.codigo like @codigo and puntosControl.idLocal = @idLocal  and puntoscontrol.Enable = '1'
                


                        ";
                IEnumerable<detalleStockData> resultado = con.Query<detalleStockData>(sql, new { codigo = codigo, idLocal = idlocal });
                return resultado.ToList();
            }
        }

        public List<StockData> GetAll(Guid idlocal)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + " where stock.idlocal= @idlocal";
                IEnumerable<StockData> resultado = OperatorGiveMeData(con, sql, new {idlocal = idlocal});
                    
                return resultado.ToList();
            }
        }

     

       public override string DEFAULT_SELECT
       {
           get
           {
               return @"   stock.*,locales.*,colores.*,productos.*,proveedores.*,linea.*,temporada.* from stock
left join locales on stock.idlocal = locales.id
left join productos on stock.idproducto = productos.id
left join colores on stock.idcolor = colores.id
left join proveedores on productos.idproveedor = proveedores.id
left join linea on productos.idlinea = linea.id
left join temporada on productos.idtemporada = temporada.id  ";
           }
       }
    }
}
