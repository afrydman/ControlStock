using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.PersonalRepository
{
    public class PersonalRepository : DbRepository, IPersonalRepository
    {
        public PersonalRepository(bool local=true) : base(local)
        {
        }

        public bool Insert(PersonalData theObject)
        {
            
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[Personal]
                           ([id]
                           ,[nombrecontacto]
                           ,[cuil]
                           ,[telefono]
                           ,[Description]
                           ,[razonsocial]
                           ,[email]
                           ,[facebook]
                           ,[direccion]
           
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
           
                           ,@enable);
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, theObject) > 0;

            }
        }

        public bool Update(PersonalData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"Update Personal
	                        Set
	                        nombrecontacto = @nombrecontacto  ,
	                        cuil = @cuil  ,
	                        telefono = @telefono ,
	                        Description = @Description ,
	                        razonsocial = @razonsocial ,
                            email = @email ,
                            facebook = @facebook ,
                            direccion= @direccion ,
                            enable=@enable 
		                    Where		
                        id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, theObject) > 0;

            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	Update Personal
	                        Set
		                        Personal.enable = '0'
	
	                        Where		
                        id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject }) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	Update Personal
	                        Set
		                        Personal.enable = '1'
	
	                        Where		
                        id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id =idObject }) > 0;
            }
        }

        public List<PersonalData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"SELECT *
                              FROM Personal
	                           ";
                IEnumerable<PersonalData> resultado = con.Query<PersonalData>(sql);
                return resultado.ToList();
            }
        }

        public PersonalData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<PersonalData>("SELECT *  FROM [Personal]  Where id= @id ", new { id = idObject }) ?? new PersonalData();
            }
        }

        public PersonalData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonalData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public List<PersonalData> GetPersonalbyLocal(Guid idLocal)
        {
            throw new NotImplementedException();
        }

        public override string DEFAULT_SELECT
        {
            get { throw new NotImplementedException(); }
        }
    }
}
