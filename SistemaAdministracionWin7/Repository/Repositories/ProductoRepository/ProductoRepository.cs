using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.ProductoRepository
{
  public   class ProductoRepository : DbRepository, IProductoRepository
    {
        public ProductoRepository(bool local=true) : base(local)
        {
        }


        public IEnumerable<ProductoData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            return con.Query<ProductoData, ProveedorData, LineaData, TemporadaData,CondicionIvaData, ProductoData>(
                sql,
                (producto, proveedor, linea, temporada,condicion) =>
                {
                    proveedor.CondicionIva = condicion;
                    producto.Proveedor = proveedor;
                    producto.Linea = linea;
                    producto.Temporada = temporada;
                    return producto;

                }, parameters);
        }


        public bool Insert(ProductoData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"Insert Into productos
		([id],[idproveedor],[Description],[codigoproveedor],[idlinea],[idtemporada],[codigointerno],enable)
	Values
		(@id,@idproveedor,@Description,@codigoproveedor,@idlinea,@idtemporada,@codigointerno,@enable)
;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {
                    id = theObject.ID,
                    idproveedor =theObject.Proveedor.ID,
                    description = theObject.Description,
                    
                    codigoProveedor = theObject.CodigoProveedor,
                    idlinea=theObject.Linea.ID,
                    idtemporada=theObject.Temporada.ID,
                    codigointerno=theObject.CodigoInterno,
                    enable=theObject.Enable

                }) > 0;

            }
        }

        public bool Update(ProductoData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"Update productos
	                            Set
		
		                            [idproveedor] = @idproveedor,
		                            [Description] = @Description,
		                            
		                            [codigoproveedor] = @codigoproveedor,
		                            [idlinea] = @idlinea,
		                            [idtemporada] = @idtemporada,
		                            [codigointerno] = @codigointerno
		
	                            Where		
                            id = @id;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {
                    id = theObject.ID,
                    idproveedor = theObject.Proveedor.ID,
                    description = theObject.Description,
                    
                    codigoProveedor = theObject.CodigoProveedor,
                    idlinea = theObject.Linea.ID,
                    idtemporada = theObject.Temporada.ID,
                    codigointerno = theObject.CodigoInterno,
                    enable = theObject.Enable

                }) > 0;

            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"	update productos
		set enable = '0'
	where id = @id;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { id =idObject}) > 0;

            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"	update productos
		set enable = '1'
	where id = @id;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { id = idObject }) > 0;

            }
        }

        public List<ProductoData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + @DEFAULT_SELECT;
                IEnumerable<ProductoData> resultado = OperatorGiveMeData(con, sql, null);
                    
                return resultado.ToList();
            }
        }

        public ProductoData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "SELECT " + @DEFAULT_SELECT + " Where productos.id= @id ";
                ProductoData aux = OperatorGiveMeData(con, sql, new { id = idObject }).FirstOrDefault();

                return aux ?? new ProductoData();

            }
        }

        public ProductoData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

    

      public List<ProductoData> GetProductoByCodigoInterno(string codigoInternoProducto, bool uselike)
        {
            string sqlWithLike = @"SELECT " + @DEFAULT_SELECT + string.Format(" Where productos.codigointerno like '{0}%'", codigoInternoProducto); 
            string sqlWithOUTLike = @"SELECT " + @DEFAULT_SELECT + string.Format(" Where productos.codigointerno = '{0}'", codigoInternoProducto);
            
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {

                IEnumerable<ProductoData> resultado = OperatorGiveMeData(con, uselike ? sqlWithLike : sqlWithOUTLike,null);
                    
                return resultado.ToList();
            }
        }

        public List<ProductoData> GetProductosByProveedor(Guid idproveedor)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"SELECT " + @DEFAULT_SELECT + " 	Where productos.idproveedor = @id";
                IEnumerable<ProductoData> resultado = OperatorGiveMeData(con, sql, new { id = idproveedor });
                    
                   
                    
                return resultado.ToList();
            }
        }

      public override string DEFAULT_SELECT
      {
          get
          {
              return @"
 productos.*,proveedores.*,linea.*,temporada.*,condicioniva.*
from productos
inner join proveedores on productos.idproveedor = proveedores.id
inner join condicioniva on proveedores.idcondicioniva = condicioniva.id
left join linea on productos.idlinea= linea.id
left join temporada on productos.idtemporada = temporada.id ";

          }
      }
    }
}
