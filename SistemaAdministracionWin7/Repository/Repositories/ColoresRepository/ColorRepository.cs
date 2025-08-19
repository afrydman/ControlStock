using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.ColoresRepository
{
    public class ColorRepository : DbRepository,IColorRepository
    {
        public ColorRepository(bool local=true) : base(local) { }
        


        public bool Insert(ColorData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"Insert Into colores ([id],[Description],[Codigo],enable) Values (@id,@Description,@Codigo,@enable);SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, theObject) > 0;

            }
        }

        public bool Update(ColorData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"Update colores
	                Set
		
		                [Description] = @Description,
		                [Codigo] = @Codigo,
                        [enable]=@enable
	                Where		
	                [id] = @id;SELECT @@ROWCOUNT;";

                return con.QueryFirstOrDefault<int>(sql,theObject) > 0;

            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"Update colores Set enable = '0' Where [id] = @id;SELECT @@ROWCOUNT;";
                
                return con.QueryFirstOrDefault<int>(sql, new { id = idObject}) > 0;

            }
        }

        public bool Enable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"Update colores Set enable = '1' Where [id] = @id;SELECT @@ROWCOUNT;";

                return con.QueryFirstOrDefault<int>(sql, new { id = idObject}) > 0;

            }
        }

        public List<ColorData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = DEFAULT_SELECT;
                IEnumerable<ColorData> resultado = con.Query<ColorData>(sql);
                return resultado.ToList();
            }
        }

        public ColorData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<ColorData>(DEFAULT_SELECT+ " where id=@idColor", new { idColor = idObject })?? new ColorData();
            }
        }

        public ColorData GetLast(Guid idLocal, int first)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ColorData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public ColorData GetColorByCodigo(string codigoColor)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<ColorData>(DEFAULT_SELECT+" Where Codigo = @Codigo", new { codigo = codigoColor }) ?? new ColorData();
            }
        }

        public override string DEFAULT_SELECT
        {
            get { return @"Select [id],[Description],[Codigo],enable From colores "; }
        }
    }
}
