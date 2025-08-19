using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.PedidoRepository
{
    public class PedidoRepository : DbRepository, IPedidoRepository
    {
        public PedidoRepository(bool local=true) : base(local)
        {
        }

        public bool Insert(pedidoData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"Insert Into pedido
		([id],[idlocal],[idvendedor],idcliente,[fecha],[subtotal],[descuento],[nfactura],Enable,completo,modificada,Prefix)
	Values
		(@id,@idlocal,@idvendedor,@idcliente,@fecha,@subtotal,@descuento,@nfactura,@Enable,@completo,'0',@Prefix)
;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, theObject) > 0;

            }
        }

        public bool Update(pedidoData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	Update pedido
	                            Set
		                            Enable='0', modificada = '1'
	                            Where		
                            id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject }) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public List<pedidoData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select * from pedido ";
                IEnumerable<pedidoData> resultado = con.Query<pedidoData>(sql);
                return resultado.ToList();
            }
        }

        public pedidoData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<pedidoData>("SELECT *  FROM [pedido]  Where id= @id ", new { id = idObject }) ?? new pedidoData();
            }
        }

        public pedidoData GetLast(Guid idLocal, int first)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<pedidoData>(@"SELECT *From pedido
	                                            Where
                                            [idlocal] = @id and Prefix = @Prefix", new { idlocal = idLocal, prefix = first }) ?? new pedidoData();
            }
        }

        public IEnumerable<pedidoData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public List<pedidoData> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int Prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"Select 	*	From pedido
	                        Where
                        fecha > @fechaAyer and fecha < @fechaMan   and idlocal = @idlocal  order by nfactura desc";
                IEnumerable<pedidoData> resultado = con.Query<pedidoData>(sql, new { fechaAyer = fecha1, fechaMan = fecha2, idlocal = idLocal });
                return resultado.ToList();
            }
        }

        public List<pedidoData> GetBiggerThan(int ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select * from pedido where idlocal = @idlocal and nfactura > @ultimo  and Prefix =@Prefix";
                IEnumerable<pedidoData> resultado = con.Query<pedidoData>(sql, new { idlocal = idLocal, ultimo = ultimo, prefix = prefix });
                return resultado.ToList();
            }
        }

        public List<pedidoData> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix)
        {
           throw new NotImplementedException();
        }

        public List<pedidoData> GetModified(Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select * from pedido where idlocal = @id and modificada = 1 ";
                IEnumerable<pedidoData> resultado = con.Query<pedidoData>(sql, new { idlocal = idLocal });
                return resultado.ToList();
            }
        }

        public bool MarkSeen(Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update pedido set modificada = 0 where idlocal = @id and Prefix=@Prefix
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = idLocal, prefix=prefix }) > 0;
            }
        }

        public bool MarcarCompleto(Guid guid, bool completo)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update pedido set completo = @completo where id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id = guid }) > 0;
            }
        }

        public override string DEFAULT_SELECT
        {
            get { throw new NotImplementedException(); }
        }
    }
}
