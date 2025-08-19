using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO;
using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository.ComprasProveedoresRepository;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.ComprasProveedoresRepository
{
    public class ComprasProveedoresRepository : DbRepository, IComprasProveedoresRepository
    {
        public ComprasProveedoresRepository(bool local=true) : base(local)
        {
        }

        public override string DEFAULT_SELECT
        {
            get
            {
                return @"  cp.*, p.*, l.*, pp.*, condicioniva.*
  FROM [dbo].[comprasProveedores] cp
  inner join  Personal p on cp.idpersonal = p.id
  inner join locales l on cp.idLocal = l.id
  inner join proveedores pp on cp.idProveedor = pp.id
inner join condicioniva on pp.idcondicioniva = condicioniva.id";
            }
        }

        public bool Insert(ComprasProveedoresData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	INSERT INTO [dbo].[comprasProveedores]
           ([id]
           ,[idPersonal]
           ,[idLocal]
           ,[idProveedor]
           ,[Date]
           ,[Monto]
           ,[Enable],Description,Numero,Prefix,fechaFactura,descuento,ClaseDocumento,IVA)
     VALUES
           (@id
           ,@idPersonal
           ,@idLocal
           ,@idProveedor
           ,@Date
           ,@Monto
           ,@Enable,@Description,@Numero,@Prefix,@fechaFactura,@descuento,@ClaseDocumento,@IVA);
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new
                {
                    idPersonal=theObject.Vendedor.ID,
                    idLocal = theObject.Local.ID,
                    idProveedor=theObject.Proveedor.ID,
                    Date= theObject.Date,
                    monto = theObject.Monto,
                    Enable = theObject.Enable,
                    Description = theObject.Description,
                    numero = theObject.Numero,
                    prefix=theObject.Prefix,
                    
                    fechafactura=theObject.FechaFactura,
                    descuento=theObject.Descuento,
                    ClaseDocumento = theObject.ClaseDocumento,
                    id = theObject.ID,
                    iva = theObject.IVA

                }) > 0;
            }
        }

        public bool Update(ComprasProveedoresData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	UPDATE [dbo].[comprasProveedores]
   SET 
		Description = @Description
 WHERE [id] = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, theObject) > 0;
            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"	update comprasProveedores set enable = '0' where id = @id;
                        SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sql, new { id =idObject }) > 0;
            }
        }

        public bool Enable(Guid idObject)
        {
            throw new Exception(Messages.MethodNotImplemented);
            return false;
        }

        public List<ComprasProveedoresData> GetAll()
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT +" order by cp.Date desc";
                IEnumerable<ComprasProveedoresData> resultado =
                    con.Query<ComprasProveedoresData, PersonalData, LocalData, ProveedorData,CondicionIvaData, ComprasProveedoresData>
                        (sql, (cp, pers, l, prov,condicion) =>
                        {
                            cp.Local = l;
                            cp.Vendedor = pers;
                            prov.CondicionIva = condicion;
                            cp.Proveedor = prov;
                            return cp;
                        });
                return resultado.ToList();
            }
        }

        public ComprasProveedoresData GetByID(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + " where cp.id = @id ";
                ComprasProveedoresData aux = con.Query<ComprasProveedoresData, PersonalData, LocalData, ProveedorData,CondicionIvaData, ComprasProveedoresData>
                       (sql, (cp, pers, l, prov, condicion) =>
                       {
                           cp.Local = l;
                           cp.Vendedor = pers;
                           prov.CondicionIva = condicion;
                           cp.Proveedor = prov;
                           return cp;
                        }, new { id = idObject }).FirstOrDefault();
                return aux ?? new ComprasProveedoresData();

            }
        }

        public ComprasProveedoresData GetLast(Guid idLocal, int first)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                string sql = @"select top 1 " + DEFAULT_SELECT + "  where cp.idlocal = @id and cp.Prefix = @Prefix order by cp.Numero desc  ";
                ComprasProveedoresData aux =
                   con.Query<ComprasProveedoresData, PersonalData, LocalData, ProveedorData, CondicionIvaData, ComprasProveedoresData>
                       (sql, (cp, pers, l, prov, condicion) =>
                       {
                           cp.Local = l;
                           cp.Vendedor = pers;
                           prov.CondicionIva = condicion;
                           cp.Proveedor = prov;
                           return cp;
                        }, new {id = idLocal, prefix = first}).FirstOrDefault();
                return aux ?? new ComprasProveedoresData();
            }
        }

        public IEnumerable<ComprasProveedoresData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public List<ComprasProveedoresData> GetByProveedor(Guid idProveedor)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql =  @"select " + DEFAULT_SELECT + " where cp.idProveedor = @idProveedor";
                IEnumerable<ComprasProveedoresData> resultado = con.Query<ComprasProveedoresData, PersonalData, LocalData, ProveedorData, CondicionIvaData, ComprasProveedoresData>
                       (sql, (cp, pers, l, prov, condicion) =>
                       {
                           cp.Local = l;
                           cp.Vendedor = pers;
                           prov.CondicionIva = condicion;
                           cp.Proveedor = prov;
                           return cp;
                        }, new { idProveedor = idProveedor },splitOn: "id,id");
                return resultado.ToList();
            }
        }

      

        public List<ComprasProveedoresData> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int Prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"select " + DEFAULT_SELECT + " where cp.idlocal = @idlocal and cp.Date>=@fecha1 and cp.Date<=@fecha2 and cp.Prefix=@Prefix";
                IEnumerable<ComprasProveedoresData> resultado = con.Query<ComprasProveedoresData, PersonalData, LocalData, ProveedorData, CondicionIvaData, ComprasProveedoresData>
                       (sql, (cp, pers, l, prov, condicion) =>
                       {
                           cp.Local = l;
                           cp.Vendedor = pers;
                           prov.CondicionIva = condicion;
                           cp.Proveedor = prov;
                           return cp;
                        }, new { fecha1 = fecha1, idLocal = idLocal, prefix = Prefix, fecha2 = fecha2 }, splitOn: "id,id");
                return resultado.ToList();
            }
        }

        public List<ComprasProveedoresData> GetBiggerThan(int ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"select " + DEFAULT_SELECT + " where cp.idlocal = @idlocal and cp.Numero>@ultimo and cp.Prefix=@Prefix";
                IEnumerable<ComprasProveedoresData> resultado = con.Query<ComprasProveedoresData, PersonalData, LocalData, ProveedorData, CondicionIvaData, ComprasProveedoresData>
                       (sql, (cp, pers, l, prov, condicion) =>
                       {
                           cp.Local = l;
                           cp.Vendedor = pers;
                           prov.CondicionIva = condicion;
                           cp.Proveedor = prov;
                           return cp;
                        }, new { ultimo = ultimo, idLocal = idLocal, prefix = prefix }, splitOn: "id,id");
                return resultado.ToList();
            }
        }

        public List<ComprasProveedoresData> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = @"select " + DEFAULT_SELECT + " where cp.idlocal = @idlocal and cp.Date>@ultimo and cp.Prefix=@Prefix";
                IEnumerable<ComprasProveedoresData> resultado = con.Query<ComprasProveedoresData, PersonalData, LocalData, ProveedorData, CondicionIvaData, ComprasProveedoresData>
                       (sql, (cp, pers, l, prov, condicion) =>
                       {
                           cp.Local = l;
                           cp.Vendedor = pers;
                           prov.CondicionIva = condicion;
                           cp.Proveedor = prov;
                           return cp;
                        }, new { ultimo = ultimo, idLocal = idLocal, prefix = prefix }, splitOn: "id,id");
                return resultado.ToList();
            }
        }
    }
}

