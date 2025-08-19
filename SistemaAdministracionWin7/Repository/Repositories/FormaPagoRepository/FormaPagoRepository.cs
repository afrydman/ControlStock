using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;
using Repository.FormaPagoRepository;

namespace Repository.Repositories.FormaPagoRepository
{
    public class FormaPagoRepository:DbRepository,IFormaPagoRepository
    {
        public FormaPagoRepository(bool local=true) : base(local)
        {
        }

        public bool Insert(FormaPagoData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	Insert Into formaspago
		([id],[Description],Enable,credito)
	Values
		(@id,@Description,@enable,@credito);
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, theObject) > 0;
            }
        }

        public bool Update(FormaPagoData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	Update formaspago
	Set
	
		[Description] = @Description,
        Enable = @Enable,
		credito = @credito
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
                var sql = @"	update formaspago set Enable = '0' where id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject }) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update formaspago set Enable = '1' where id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject }) > 0;
            }
        }

        public List<FormaPagoData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select * from formaspago";
                IEnumerable<FormaPagoData> resultado = con.Query<FormaPagoData>(sql);
                return resultado.ToList();
            }
        }

        public FormaPagoData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<FormaPagoData>("select * from formaspago where id = @id", new { id = idObject }) ?? new FormaPagoData();
            }
        }

        public FormaPagoData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FormaPagoData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public override string DEFAULT_SELECT
        {
            get { throw new NotImplementedException(); }
        }
    }
}
