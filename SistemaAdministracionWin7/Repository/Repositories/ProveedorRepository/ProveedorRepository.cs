using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.ProveedorRepository
{
    public class ProveedorRepository : DbRepository, IProveedorRepository
    {
        public ProveedorRepository(bool local=true) : base(local)
        {
        }

        public bool Insert(ProveedorData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"INSERT INTO [dbo].[proveedores]
           ([id]
           ,[nombrecontacto]
           ,[cuil]
           ,[telefono]
           ,[Description]
           ,[razonsocial]
           ,[email]
           ,[facebook]
           ,[direccion]
           ,[Codigo]
           ,[descuento]
           ,[IngresosBrutos]
,idcondicioniva
           ,[enable])
     VALUES
           (@id,
           @nombrecontacto,
           @cuil,
           @telefono,
           @Description,
           @razonsocial,
           @email,
           @facebook,
           @direccion,
           @Codigo,
           @descuento,
           @IngresosBrutos
,@idcondicioniva
           ,@enable);
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {

                    id = theObject.ID,
                    theObject.NombreContacto,
                    theObject.cuil,
                    theObject.Telefono,
                    theObject.Description,
                    theObject.RazonSocial,
                    theObject.Email,
                    theObject.Facebook,
                    theObject.Direccion,
                    theObject.Codigo,
                    theObject.Descuento,
                    theObject.IngresosBrutos,
                    idcondicioniva = theObject.CondicionIva.ID,
                    theObject.Enable

                }) > 0;
            }
        }

        public bool Update(ProveedorData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"Update proveedores
	                        Set
	                        nombrecontacto = @nombrecontacto  ,
	                        cuil = @cuil  ,
	                        telefono = @telefono ,
	                        Description = @Description ,
	                        razonsocial = @razonsocial ,
                            email = @email ,
                            facebook = @facebook ,
                            direccion= @direccion ,
                            enable=@enable  ,
                            Codigo=@Codigo,
                            descuento=@descuento,
                            ingresosbrutos=@ingresosbrutos,
idcondicioniva=@idcondicioniva
		                    Where		
                        id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {

                    id = theObject.ID,
                    theObject.NombreContacto,
                    theObject.cuil,
                    theObject.Telefono,
                    theObject.Description,
                    theObject.RazonSocial,
                    theObject.Email,
                    theObject.Facebook,
                    theObject.Direccion,
                    theObject.Codigo,
                    theObject.Descuento,
                    theObject.IngresosBrutos,
                    idcondicioniva = theObject.CondicionIva.ID,
                    theObject.Enable

                }) > 0;
            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	Update proveedores
	                        Set
		                        proveedores.enable = '0'
	
	                        Where		
                        id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject}) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	Update proveedores
	                                Set
		
	                              
			                                enable=1
	                                Where		
                                id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject}) > 0;
            }
        }

        public List<ProveedorData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = DEFAULT_SELECT;
                IEnumerable<ProveedorData> resultado = con.Query<ProveedorData, CondicionIvaData, ProveedorData>
                    (sql,
                        (cliente, condicion) =>
                        {
                            cliente.CondicionIva = condicion;
                            return cliente;
                        });
                return resultado.ToList();
            }
        }

        public ProveedorData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = DEFAULT_SELECT + " Where proveedores.id= @id ";
                return con.Query<ProveedorData, CondicionIvaData, ProveedorData>
                   (sql,
                       (cliente, condicion) =>
                       {
                           cliente.CondicionIva = condicion;
                           return cliente;
                       }, new { id = idObject }).FirstOrDefault() ?? new ProveedorData(); ;
                
                
            }
        }

        public ProveedorData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProveedorData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public ProveedorData GetByCodigo(string cod)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = DEFAULT_SELECT + "Where proveedores.Codigo= @cod ";
                return con.Query<ProveedorData, CondicionIvaData, ProveedorData>
                   (sql,
                       (cliente, condicion) =>
                       {
                           cliente.CondicionIva = condicion;
                           return cliente;
                       }, new { cod = cod }).FirstOrDefault() ?? new ProveedorData(); ;
                

                
            }
        }

        public override string DEFAULT_SELECT
        {
            get { return @"SELECT *  FROM proveedores inner join condicioniva on proveedores.idcondicioniva = condicioniva.id "; }
        }
    }
}
