using System;
using System.Collections.Generic;
using System.Globalization;
using System.Transactions;

using DTO.BusinessEntities;
using Repository.ClienteRepository;
using Repository.Repositories.PagosRepository;
using Repository.Repositories.PersonalRepository;
using Repository.Repositories.VentaRepository;

namespace Services.VentaService
{
    public class VentaServiceOLD :IGenericService<ventaData>, IGenericServiceGetter<ventaData>, IGenericServiceSyncStuff<ventaData>
   {
       protected readonly IVentaRepository _repo;
       protected readonly IVentaDetalleRepository _repoDetalles;
      
        public VentaServiceOLD(IVentaRepository repo,IVentaDetalleRepository detalle)
        {
            _repo = repo;
            _repoDetalles = detalle;
        }

        public VentaServiceOLD(bool local = true)
         {
             _repo = new VentaRepository(local);
             _repoDetalles = new VentaDetalleRepository(local);
         }

        public bool Insert(ventaData nuevaVenta, bool updateStock)
        {
            var pagoService = new PagoService.PagoService(new PagosRepository());
           bool task = false;
            Guid idVenta;
           var opts = new TransactionOptions
           {
               IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
           };

           using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
           {

               try
               {
                   //conexion.closeConecction();//!todo! //Para que se vuelva a abrir dentro de la trans!
                   if (nuevaVenta.ID == new Guid())
                   {
                       idVenta = Guid.NewGuid();
                       nuevaVenta.ID = idVenta;
                   }
                   else
                   {
                       idVenta = nuevaVenta.ID;
                   }
                   foreach (PagoData f in nuevaVenta.pagos)
                   {
                       f.FatherID = idVenta;

                       task = pagoService.InsertDetalle(f);
                       if (!task) return false;

                   }
                   //
                   // !!!! no poner nada con venta.escambio pq la cantidad ya la guarda ( negativo si es cambio)!!!!
                   //
                   foreach (ventaDetalleData d in nuevaVenta.Children)
                   {
                       task = _repoDetalles.InsertDetalle(d);
                       if (!task) return false;

                       //if (updateStock)
                         //  task = stock.actualizarStock(d.codigo, -1 * d.cantidad, HelperService.IDLocal, true);//todo!


                       if (!task) return false;
                   }
                   task = _repo.Insert(nuevaVenta);
                   if (task) trans.Complete();
               }
               catch (Exception)
               {
                   return false;

               }
           }
           return task;

       }

        public bool Insert(ventaData theObject)
        {
            return Insert(theObject, false);
        }

        public bool Update(ventaData theObject)
       {
           throw new NotImplementedException();
       }

        public bool Disable(ventaData theObject)
        {
            return Disable(theObject, false);
        }

        public bool Disable(ventaData theObject,bool updateStock )
       {
           bool task = false;
           var opts = new TransactionOptions
           {
               IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
           };

           using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
           {

               try
               {
                   //conexion.closeConecction();//!todo! //Para que se vuelva a abrir dentro de la trans!

                   //if (updateStock)
                   //{
                   //    foreach (ventaDetalleData vd in v.detalles)
                   //    {
                   //        task = stock.actualizarStock(vd.codigo, vd.cantidad, v.Local.ID);
                   //        if (!task) return false;
                   //    }
                   //}//todo!
                   task = _repo.Disable(theObject.ID);
                   if (task) trans.Complete();

               }
               catch (Exception)
               {

                   return false;
               }
           }


           return task;
       }

       public bool Enable(ventaData theObject)
       {
           throw new NotImplementedException();
       }

       public List<ventaData> GetAll(bool onlyEnable = true)
       {
           throw new NotImplementedException();
       }


        public List<ventaData> NormalizeList(List<ventaData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(data => data.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            return list;
        }

        public   ventaData getPropertiesInfo(ventaData n)
       {
           var clienteService = new ClienteService.ClienteService(new ClienteRepository());
           var pagosService = new PagoService.PagoService(new PagosRepository());
            var vendedorService = new PersonalService.PersonalService(new PersonalRepository());


            n.vendedor = vendedorService.GetByID(n.vendedor.ID);
           n.Children = _repoDetalles.GetDetalles(n.ID);
           n.Cliente = clienteService.GetByID(n.Cliente.ID);
           n.pagos = pagosService.GetDetalles(n.ID);

           return n;
       }

        public Type GetTypeRepo()
        {
            throw new NotImplementedException();
        }


        public ventaData GetByID(Guid idObject)
       {
           ventaData v = _repo.GetByID(idObject);

           return getPropertiesInfo(v);
       }

       public ventaData GetLast(Guid idLocal, int first)
       {
           return getPropertiesInfo(_repo.GetLast(idLocal, first));
       }

       public  string GetNextNumberAvailable(Guid idLocal, int myprefix, bool completo)
       {
           ventaData aux = GetLast(idLocal, myprefix);
           aux.Numero++;
           if (completo)
           {
               return aux.NumeroCompleto;
           }
           return aux.Numero.ToString();
           
       }
       public  decimal calcularTotal(List<PagoData> pagos)
       {
           decimal total = 0;
           foreach (PagoData fp in pagos)
           {
               total += (fp.Importe + (fp.Importe * fp.Recargo / 100));

           }
           return total;
       }
       //public  List<ventaData> getCuentaCorrientebyCliente(Guid idCliente)
       //{


       //    return NormalizeList(_repo.getVentasPagadasConCC(idCliente, HelperService.idCC),false);
       //}

       public  List<ventaData> getbyCliente(Guid guid)
       {
           return NormalizeList(_repo.getbyCliente(guid),false);
       }


       public  bool validarTrial(DateTime fecha, Guid idlocal, string global, int maxventas = 9)//todo! sacar de aca no?
       {

           CultureInfo provider = CultureInfo.InvariantCulture;

           string format = "MMddyyyy";
           DateTime result = DateTime.ParseExact(global, format, provider);
           bool ok = true;

           if (DateTime.Now > result)
           {


               List<ventaData> ventas = GetByRangoFecha(fecha,  idlocal);

               if (ventas != null && ventas.Count > maxventas)
                   ok = false;
           }
           return ok;
       }

        public List<ventaData> GetByRangoFecha(DateTime fecha1)
        {
            return GetByRangoFecha(fecha1, HelperService.IDLocal);
        }

        public List<ventaData> GetByRangoFecha(DateTime fecha1,  Guid idLocal)
        {
         
            return GetByRangoFecha(fecha1.Date, fecha1.Date.AddDays(1), idLocal);
        }


 
        public List<ventaData> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, bool enableOnly = true)
        {
            if (idLocal == Guid.Empty)
                idLocal = HelperService.IDLocal;


            fecha1 = fecha1.Date;
            fecha2 = fecha2.Date.AddHours(23).AddMinutes(59);


            List<DTO.BusinessEntities.ventaData> ventas = _repo.GetByRangoFecha(fecha1, fecha2, idLocal);



            return NormalizeList(ventas, enableOnly);
        }

        public List<ventaData> GetBiggerThan(int ultimo, Guid idLocal, int prefix)
        {
            List<ventaData> ventas = _repo.GetBiggerThan(ultimo, idLocal, prefix);
            return NormalizeList(ventas,false);
        }

        public List<ventaData> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix)
        {
            throw new NotImplementedException();
        }

        public List<ventaData> GetModified(Guid idLocal, int prefix)
        {
            return _repo.GetModified(idLocal, prefix);
        }

        public bool MarkSeen(Guid idLocal, int prefix)
        {
            return _repo.MarkSeen(idLocal, prefix);
        }
        public  List<PagoData> obtenerTipoPagos(Guid idTipoPago)
        {
            return _repo.obtenerTipoPagos(idTipoPago);
        }

        public List<ventaData> GetVentasConDetalle(string substring)
        {
            throw new NotImplementedException();
        }
   }
}
