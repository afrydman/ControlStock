using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository.ConnectionFactoryStuff;

namespace Repository.ClienteRepository
{
    public class ClienteRepository : DbRepository,IClienteRepository
    {
        public ClienteRepository(bool local=true) : base(local)
        {
        }

        public override string DEFAULT_SELECT
        {
            get { return @"SELECT *  FROM clientes inner join condicioniva on clientes.idcondicioniva = condicioniva.id "; }
        }

        public bool Insert(ClienteData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"INSERT INTO [dbo].[clientes]
                           ([id]
                           ,[nombrecontacto]
                           ,[cuil]
                           ,[telefono]
                           ,[Description]
                           ,[razonsocial]
                           ,[email]
                           ,[facebook]
                           ,[direccion]
           ,IngresosBrutos,idcondicioniva
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
                           @direccion
            ,@IngresosBrutos,@idcondicioniva
                           ,@enable);
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    id=theObject.ID,
                    theObject.NombreContacto,
                    theObject.cuil,
                    theObject.Telefono,
                    theObject.Description,
                    theObject.RazonSocial,
                    theObject.Email,
                    theObject.Facebook,
                    theObject.Direccion,
                    theObject.IngresosBrutos,
                    idcondicioniva=theObject.CondicionIva.ID,
                    theObject.Enable



                }) > 0;
            }
        }

        public bool Update(ClienteData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"Update clientes
	                        Set
	                        nombrecontacto = @nombrecontacto  ,
	                        cuil = @cuil  ,
	                        telefono = @telefono ,
	                        Description = @Description ,
	                        razonsocial = @razonsocial ,
                            email = @email ,
                            facebook = @facebook ,
                            direccion= @direccion ,
idcondicioniva=@idcondicioniva,
IngresosBrutos=@IngresosBrutos,
                            enable=@enable 
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
                var sql = @"	Update clientes
	                        Set
		                        clientes.enable = '0'
	
	                        Where		
                        id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new {id=idObject}) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	Update clientes
	                        Set
		                        clientes.enable = '1'
	
	                        Where		
                        id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject }) > 0;
            }
        }

        public List<ClienteData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = DEFAULT_SELECT;
                IEnumerable<ClienteData> resultado = con.Query<ClienteData, CondicionIvaData, ClienteData>
                    (sql,
                        (cliente, condicion) =>
                        {
                            cliente.CondicionIva = condicion;
                            return cliente;
                        });
                ;
                return resultado.ToList();
            }
        }

        public ClienteData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {

                return con.Query<ClienteData, CondicionIvaData, ClienteData>
                    ( DEFAULT_SELECT + " Where clientes.id= @id ",
                        (cliente, condicion) =>
                        {
                            cliente.CondicionIva = condicion;
                            return cliente;
                        }
                        , new { id = idObject }).FirstOrDefault() ?? new ClienteData();

            }


        }

        public ClienteData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClienteData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }
    }
}
