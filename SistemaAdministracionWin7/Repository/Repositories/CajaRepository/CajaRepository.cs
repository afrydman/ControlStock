using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO.BusinessEntities;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.CajaRepository
{
    public class CajaRepository : DbRepository, ICajaRepository
    {
        public CajaRepository(bool local=true) : base(local)
        {
        }


          public override string DEFAULT_SELECT
        {
            get { return @" cajas.*, locales.*  from cajas inner join locales on cajas.IDLocal=locales.ID "; }
        }

        
        public bool Insert(CajaData theObject)
        {
            
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"insert into cajas(id,Monto,Date,idLocal)values(@id,@Monto,@fecha,@idLocal );
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new{id=theObject.ID,monto=theObject.Monto,fecha=theObject.Date,idLocal=theObject.Local.ID}) > 0;

            }
        }

        public bool Update(CajaData theObject)
        {
            throw new NotImplementedException();
        }

        public bool Disable(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public bool Enable(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public List<CajaData> GetAll()
        {
            throw new NotImplementedException();
        }

        public CajaData GetByID(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public CajaData GetLast(Guid idLocal, int first)
        {
            string sql = "select top 1 " + DEFAULT_SELECT + " where cajas.idlocal = @id order by cajas.Date desc ";
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                CajaData aux = con.Query<CajaData, LocalData, CajaData>(sql,
                    (c, l) =>
                    {
                        c.Local = l;
                        return c;
                    },new{id=idLocal}
                    ).FirstOrDefault();
                return aux??new CajaData();
            }
        }

        public IEnumerable<CajaData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }


        public List<CajaData> GetBiggerThan(int ultimo, Guid idLocal, int prefix)
        {
            throw new NotImplementedException();
        }

        public List<CajaData> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix)
        {

            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"select " + DEFAULT_SELECT + " where cajas.Date > @fecha and cajas.idlocal = @idlocal";

                IEnumerable<CajaData> resultado = con.Query<CajaData, LocalData, CajaData>(sql, (c, l) =>
                {
                    c.Local = l;
                    return c;
                }, new { idlocal = idLocal, fecha = ultimo });
                return resultado.ToList();
            }
            //
        }

        public CajaData GetCajabyFecha(Guid idLocal, DateTime fecha)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select" + DEFAULT_SELECT + " where cajas.idlocal = @id and DATEADD(dd, 0, DATEDIFF(dd, 0, cajas.Date)) = DATEADD(dd, 0, DATEDIFF(dd, 0, @fecha))";
                return con.Query<CajaData, LocalData, CajaData>(sql, (c, l) =>
                {
                    c.Local = l;
                    return c;
                }, new { id = idLocal, fecha = fecha }).FirstOrDefault() ?? new CajaData();
            }
        }

        public CajaData GetCajaInicial(Guid idLocal, DateTime fecha)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.Query<CajaData, LocalData, CajaData>("select top 1 " + DEFAULT_SELECT + " where cajas.idlocal = @id and cajas.Date  <(CONVERT(Datetime,@fecha)) order by cajas.Date desc", (c, l) =>
                {
                    c.Local = l;
                    return c;
                }, new { id = idLocal, fecha = fecha }).FirstOrDefault() ?? new CajaData();
            }
        }

        public List<CajaData> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int Prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"select " + DEFAULT_SELECT + " where cajas.Date >= @fecha1 and cajas.idlocal = @idlocal and cajas.Date <= @fecha2";

                IEnumerable<CajaData> resultado = con.Query<CajaData, LocalData, CajaData>(sql, (c, l) =>
                {
                    c.Local = l;
                    return c;
                }, new { idLocal = idLocal, fecha1 = fecha1, fecha2 = fecha2 });
                return resultado.ToList();
            }
        }

        public List<CajaData> GetByRangoFecha2(DateTime fecha1, DateTime fecha2, Guid idLocal, int Prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"SELECT *
                            FROM (
                                SELECT *,
                                       ROW_NUMBER() OVER (
                                           PARTITION BY CAST([date] AS DATE)
                                           ORDER BY [date] DESC
                                       ) AS rn
                                FROM [cajas]
                            ) AS ranked
                            WHERE rn = 1 and [date]>=@fecha1 and [date]<=@fecha2 and idLocal=@idLocal";

                IEnumerable<CajaData> resultado = con.Query<CajaData, LocalData, CajaData>(sql, (c, l) =>
                {
                    c.Local = l;
                    return c;
                }, new { idLocal = idLocal, fecha1 = fecha1, fecha2 = fecha2 });
                return resultado.ToList();
            }
        }





    }
}
