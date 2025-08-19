using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DTO;
using DTO.BusinessEntities;
using DTO.BusinessEntities.Internals;
using Repository.ConnectionFactoryStuff;

namespace Repository.Repositories.VentaRepository
{
    public class VentaRepository : DbRepository, IVentaRepository
    {
        public VentaRepository(bool local=true) : base(local)
        {
        }

        public bool Insert(VentaData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"Insert Into ventas
		([id],[idlocal],[idvendedor],idcliente,[Date],[Monto],[descuento],[Numero],Enable,cambio,modificada,Prefix,ClaseDocumento,Description,IVA,CAE,CAEVto)
	Values
		(@id,@idlocal,@idvendedor,@idcliente,@Date,@Monto,@descuento,@Numero,@Enable,@cambio,'0',@Prefix,@ClaseDocumento,@Description,@IVA,@CAE,@CAEVto)
;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new
                {
                    theObject.ID,
                    idlocal = theObject.Local.ID,
                    idvendedor = theObject.Vendedor.ID,
                    idcliente = theObject.Cliente.ID,
                    theObject.Date,
                    theObject.Monto,
                    descuento = theObject.Descuento,
                    theObject.Numero,
                    theObject.Enable,
                    cambio = theObject.Cambio,
                    prefix = theObject.Prefix,
                    ClaseDocumento = theObject.ClaseDocumento,
                    theObject.Description,
                    iva = theObject.IVA,
                    theObject.CAE,
                    theObject.CAEVto

                }) > 0;

            }
        }

        public bool Update(VentaData theObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"Update ventas
	Set
		description=@description,
        CAE=@cae,
        CAEVto=@CAEVto
where 
id = @id;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { id = theObject.ID, description=theObject.Description ,cae=theObject.CAE, CAEVto= theObject.CAEVto}) > 0;

            }
        }

        public bool Disable(Guid idObject)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"Update ventas
	Set
		Enable='0', modificada = '1'
	Where		
id = @id;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new{id=idObject}) > 0;

            }
        }

        public bool Enable(Guid idObject)
        {
            throw new NotImplementedException();
        }

        public List<VentaData> GetAll()
        {
            throw new NotImplementedException();
        }




        public VentaData GetByID(Guid idObject)
        {
            var sql = "SELECT " + DEFAULT_SELECT + " Where ventas.id= @id ";
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                return con.Query<VentaData, LocalData, PersonalData, ClienteData, CondicionIvaData, VentaData>(sql,
                        (venta, local, personal, cliente, condicionIva) =>
                        {
                            venta.Local = local;
                            venta.Vendedor = personal;
                            cliente.CondicionIva = condicionIva;
                            venta.Cliente = cliente;
                            return venta;
                        }
                        ,new { id = idObject }).FirstOrDefault() ?? new VentaData();
            }
        }

        public VentaData GetLast(Guid idLocal, int prefix)
        {
            var sql = "select  top 1 " + DEFAULT_SELECT +
                      " where ventas.idlocal = @idLocal and ventas.Prefix = @Prefix and ventas.Numero = (select  max(Numero) as 'Numero' from ventas where ventas.idlocal = @idlocal and ventas.Prefix = @Prefix  )";

            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                VentaData aux = con.Query<VentaData, LocalData, PersonalData, ClienteData, CondicionIvaData, VentaData>(sql,
                        (venta, local, personal, cliente, condicionIva) =>
                        {
                            venta.Local = local;
                            venta.Vendedor = personal;
                            cliente.CondicionIva = condicionIva;
                            venta.Cliente = cliente;
                            return venta;
                        }
                    , new { idLocal = idLocal, prefix = prefix }).FirstOrDefault();
                return aux ?? new VentaData();
            }
           
        }

        public IEnumerable<VentaData> OperatorGiveMeData(IDbConnection con, string sql, object parameters)
        {
            throw new NotImplementedException();
        }

        public List<VentaData> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, int Prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT +
                          " where " +
                          " ventas.Date >= @fecha1 and ventas.Date <= @fecha2  and ventas.idlocal = @idLocal ";
                IEnumerable<VentaData> resultado = con.Query<VentaData, LocalData, PersonalData, ClienteData, CondicionIvaData, VentaData>(sql,
                        (venta, local, personal, cliente, condicionIva) =>
                        {
                            venta.Local = local;
                            venta.Vendedor = personal;
                            cliente.CondicionIva = condicionIva;
                            venta.Cliente = cliente;
                            return venta;
                        }, new { fecha1 = fecha1, fecha2 = fecha2, idLocal = idLocal });
                return resultado.ToList();
            }
        }

        public List<VentaData> GetBiggerThan(int ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + " where ventas.idLocal = @idLocal and ventas.Numero > @ultimo  and ventas.Prefix =@Prefix";
                IEnumerable<VentaData> resultado = con.Query<VentaData, LocalData, PersonalData, ClienteData, CondicionIvaData, VentaData>(sql,
                        (venta, local, personal, cliente, condicionIva) =>
                        {
                            venta.Local = local;
                            venta.Vendedor = personal;
                            cliente.CondicionIva = condicionIva;
                            venta.Cliente = cliente;
                            return venta;
                        }, new { ultimo = ultimo, prefix = prefix, idLocal = idLocal });
                return resultado.ToList();
            }
        }

        public List<VentaData> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + " where ventas.idLocal = @idLocal and ventas.Date > @ultimo  and ventas.Prefix =@Prefix";
                IEnumerable<VentaData> resultado = con.Query<VentaData, LocalData, PersonalData, ClienteData, CondicionIvaData, VentaData>(sql,
                        (venta, local, personal, cliente, condicionIva) =>
                        {
                            venta.Local = local;
                            venta.Vendedor = personal;
                            cliente.CondicionIva = condicionIva;
                            venta.Cliente = cliente;
                            return venta;
                        }, new { ultimo = ultimo, prefix = prefix, idLocal = idLocal });
                return resultado.ToList();
            }
        }

        public List<VentaData> GetModified(Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + " where ventas.Local = @id and ventas.modificada = 1 ";
                IEnumerable<VentaData> resultado = con.Query<VentaData, LocalData, PersonalData, ClienteData, CondicionIvaData, VentaData>(sql,
                        (venta, local, personal, cliente, condicionIva) =>
                        {
                            venta.Local = local;
                            venta.Vendedor = personal;
                            cliente.CondicionIva = condicionIva;
                            venta.Cliente = cliente;
                            return venta;
                        }, new { idLocal = idLocal });
                return resultado.ToList();
            }
        }

        public bool MarkSeen(Guid idLocal, int prefix)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sqlEnvio = @"update ventas set modificada = 0 where idlocal = @id ;
            SELECT @@ROWCOUNT;";
                return con.QueryFirstOrDefault<int>(sqlEnvio, new { id = idLocal }) > 0;

            }
        }

        public List<VentaData> getVentasPagadasConCC(Guid idCliente, Guid idCC)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT + " where pagos.IDFormaPago = @idCC and ventas.IDCliente = @idCliente";
                IEnumerable<VentaData> resultado = con.Query<VentaData, LocalData, PersonalData, ClienteData,CondicionIvaData, VentaData>(sql,
                        (venta, local, personal, cliente,condicionIva) =>
                        {
                            venta.Local = local;
                            venta.Vendedor = personal;
                            cliente.CondicionIva = condicionIva;
                            venta.Cliente = cliente;
                            return venta;
                        }, new { idCC = idCC, idCliente = idCliente });
                return resultado.ToList();
            }
        }


        public List<VentaData> getbyCliente(Guid idCliente)
        {
            using (var con = ConnectionFactory.CreateConnection(_connectionString))
            {
                var sql = "select " + DEFAULT_SELECT +" where ventas.idcliente=@idCliente";
                IEnumerable<VentaData> resultado = con.Query<VentaData, LocalData, PersonalData, ClienteData,CondicionIvaData, VentaData>(sql,
                        (venta, local, personal, cliente,condicionIva) =>
                        {
                            venta.Local = local;
                            venta.Vendedor = personal;
                            cliente.CondicionIva = condicionIva;
                            venta.Cliente = cliente;
                            return venta;
                        },
                        new { idCliente = idCliente });
                return resultado.ToList();
            }
        }

        public List<PagoData> obtenerTipoPagos(Guid idTipoPago)
        {
            throw new NotImplementedException();
        }

        public override string DEFAULT_SELECT
        {
            get
            {
                return @" [ventas].*,
		locales.*,
		Personal.*,
		clientes.*,condicioniva.*
  FROM [dbo].[ventas]
  inner join locales on ventas.idlocal = locales.id
  inner join Personal on ventas.idvendedor = Personal.id
  inner join clientes on ventas.idCliente = clientes.id
inner join condicioniva on clientes.idcondicioniva = condicioniva.id
   ";
            }
        }
    }
}
