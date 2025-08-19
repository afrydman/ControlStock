using System;
using System.Data;
using System.Linq;
using Dapper;

namespace Repository.ConnectionFactoryStuff
{
   public static class Extensions
    {
        public static T ExecuteAndGetId<T>(this IDbConnection con, string sql, object param = null, IDbTransaction trans = null, bool buffered = true, int? timeout = null, CommandType? commandType = null)
        {
            string syntaxLastId = String.Empty;

            
            syntaxLastId = "SELECT SCOPE_IDENTITY();";

            return con.Query<T>(sql + "; " + syntaxLastId, param, trans, buffered, timeout, commandType).Single();
        }
    }
}
