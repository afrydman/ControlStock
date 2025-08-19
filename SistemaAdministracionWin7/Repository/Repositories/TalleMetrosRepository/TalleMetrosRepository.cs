using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.TalleMetrosRepository
{
public     class TalleMetrosRepository : DbRepository, ITalleMetrosRepository
    {
        public TalleMetrosRepository(bool local=true) : base(local)
        {
        }


        public string GetTalle(Guid idProducto, Guid idColor, decimal metros)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<string>("select talle from metrosTalle where idproducto = @idproducto and idcolor = @idcolor and metros = @metros"
                    , new { idproducto = idProducto, idcolor = idColor,metros=metros });
            }
        }

    
        public bool InsertTalleMetros(Guid idProducto, Guid idColor, decimal metros, string talle, int talledec)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"INSERT INTO [dbo].[metrosTalle]
           ([idproducto]
           ,[idcolor]
           ,[talle]
           ,[metros],talledec)
     VALUES
           (@idproducto
           ,@idcolor
           ,@talle
           ,@metros,@talledec)
;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { idproducto = idProducto, idcolor = idColor, talle = talle, metros = metros, talledec = talledec }) > 0;

            }
        }

        public int ObtengoUltimoTalle(Guid idProducto, Guid idColor)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                int talledec = con.Query<int>("select ISNULL(max(talledec),0) as talle from metrosTalle where idproducto=@idproducto and idcolor=@idcolor"
                    , new { idproducto = idProducto, idcolor = idColor}).FirstOrDefault();

                return talledec;
            }
        }

        public decimal GetMetros(Guid idProducto, Guid idColor, int talle)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<decimal>("select metros from metrosTalle where idproducto=@idproducto and idcolor=@idcolor and talledec = @talle"
                    , new { idproducto = idProducto, idcolor = idColor, talle = talle });
            }
        }

        public Dictionary<string, decimal> ObtenerTodoByProductoColor(Guid idProducto, Guid idColor)
        {
        

            Dictionary<string, decimal> result = new Dictionary<string, decimal>();
            decimal auxDec = 0;
            string auxString = "";
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {


                var db = con.Query(  //el talle igual ni lo mira.
                    "select talle,metros from metrosTalle where idproducto=@idproducto and idcolor=@idcolor  "

                    , param: new { idproducto = idProducto, idcolor = idColor })
                    .ToList();

                foreach (var item in db)
                {
                    auxDec = 0;
                    auxString = "";
                    auxDec = (decimal)item.metros;
                    auxString = Convert.ToString(item.talle);
                    result[auxString] = auxDec;


                }

                return result;
            }

        }

        public Dictionary<decimal, string> ObtenerTodoByProducto(Guid idProducto)
        {
            Dictionary<decimal, string> result = new Dictionary<decimal, string>();
            decimal auxDec = 0;
            string auxString = "";
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {


                var db = con.Query(  //el talle igual ni lo mira.
                    "select metros,talleDec from metrosTalle where idproducto=@idproducto  "
                    
                    ,param: new { idproducto = idProducto })
                    .ToList();

                foreach (var item in db)
                {
                     auxDec = 0;
                     auxString = "";
                     auxDec = (decimal)item.metros;
                    auxString = Convert.ToString(item.talleDec);
                    result[auxDec] = auxString;


                }

                return result;
            }
        }

    public override string DEFAULT_SELECT
    {
        get { throw new NotImplementedException(); }
    }
    }
}
