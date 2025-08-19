using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Transactions;

using DTO.BusinessEntities;
using ObjectDumper;
using Repository;
using Repository.ClienteRepository;
using Repository.Repositories.PagosRepository;
using Repository.Repositories.PersonalRepository;
using Repository.Repositories.VentaRepository;
using Services.AbstractService;
using Services.BancoService;
using Services.ColorService;
using Services.TributoService;

namespace Services.VentaService
{
    public class VentaService : FatherService<VentaData, VentaDetalleData, IVentaRepository, IVentaDetalleRepository>, IGenericServiceSyncStuff<VentaData>
    {
        public VentaService(IVentaRepository repo, IVentaDetalleRepository detalle)
        {
            _repo = repo;
            _repoDetalle = detalle;
        }

        public VentaService(bool local = true)
        {
            _repo = new VentaRepository(local);
            _repoDetalle = new VentaDetalleRepository(local);
        }



        public override bool Insert(VentaData nuevaVenta)
        {

            var pagoService = new PagoService.PagoService(new PagosRepository());
            var TributoVentaService = new TributoNexoService(new TributoVentaRepository());

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                try
                {

                    if (!base.Insert(nuevaVenta))
                        return false;

                    foreach (PagoData f in nuevaVenta.Pagos)
                    {
                        f.FatherID = nuevaVenta.ID;
                        if (!pagoService.InsertDetalle(f)) return false;
                    }
                    foreach (TributoNexoData f in nuevaVenta.Tributos)
                    {
                        f.FatherID = nuevaVenta.ID;
                        if (!TributoVentaService.InsertDetalle(f)) return false;
                    }


                    trans.Complete();

                }
                catch (Exception e)
                {


                    HelperService.WriteException(e);

                    HelperService.writeLog(
                                    ObjectDumperExtensions.DumpToString(nuevaVenta, "Venta_Insert"), true, true);


                    throw;

                }
            }
            return true;
        }





        public override bool Update(VentaData theObject)
        {
            try
            {
                
                return _repo.Update(theObject);
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(theObject, "Venta_Update"), true, true);


                throw;

            }
            
        }

        public override bool Disable(VentaData theObject)
        {
            try
            {
                return Disable(theObject.ID, false);
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(theObject, "Venta_Disable"), true, true);

                throw;

            }
           
        }


        public override bool Enable(VentaData theObject)
        {
            throw new NotImplementedException();
        }

        public override List<VentaData> GetAll(bool onlyEnable = true)
        {
            throw new NotImplementedException();
        }


        public override List<VentaData> NormalizeList(List<VentaData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(data => data.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            return list;
        }

        public override VentaData getPropertiesInfo(VentaData n)
        {
            var pagosService = new PagoService.PagoService();
            //var tributoService = new TributoService.TributoNexoService(new TributoVentaRepository());

            

            try
            {
                if(n.ID==Guid.Empty)
                    n = new VentaData();
                else { 
                n.Children = _repoDetalle.GetDetalles(n.ID);
                n.Pagos = pagosService.GetDetalles(n.ID);
                //n.Tributos = tributoService.GetDetalles(n.ID);
                }
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(n, "VentaService_getPropertiesInfo"), true, true);

                throw;

            }

          
           
           return n;
       }


        public override VentaData GetByID(Guid idObject)
       {
            
            VentaData v = new VentaData();
            try
            {
                 v = _repo.GetByID(idObject);
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(v, "Venta_GetByID"), true, true);

                throw;

            }
            
            

           return getPropertiesInfo(v);
       }


        public List<VentaData> getCuentaCorrientebyCliente(Guid idCliente, bool onlyEnable=true)
        {


            List<VentaData> aux = getbyCliente(idCliente, onlyEnable);

            return aux.FindAll(data => data.Pagos.Any(pagoData => pagoData.FormaPago.ID == HelperService.idCC));
        }

        public List<VentaData> getbyCliente(Guid guid, bool onlyEnable=true)
       {
           try
           {
               return NormalizeList(_repo.getbyCliente(guid), onlyEnable);
           }
           catch (Exception e)
           {

               
                      HelperService.WriteException(e);

               HelperService.writeLog(
                               ObjectDumperExtensions.DumpToString(guid, "Ventas_getbyCliente"), true, true);

               throw;

           }
           
       }


   
        public List<VentaData> GetByRangoFecha(DateTime fecha1)
        {
            return GetByFecha(fecha1, HelperService.IDLocal, HelperService.Prefix);
        }







        public List<VentaData> GetModified(Guid idLocal, int prefix)
        {
            try
            {
                return _repo.GetModified(idLocal, prefix);
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idLocal, "Ventas_getModified"), true, true);

                throw;

            }
           
        }

        public bool MarkSeen(Guid idLocal, int prefix)
        {
           
            try
            {
                return _repo.MarkSeen(idLocal, prefix);
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idLocal, "Ventas_MarkSeen"), true, true);

                throw;

            }
        }


        public List<VentaData> GetVentasConDetalle(string substring, Guid idlocal)
        {
            List<VentaData> ventas = new List<VentaData>();
         
            try
            {
                List<VentaDetalleData> detalles = _repoDetalle.GetByCodigoInterno(substring);

                List<Guid> idVentas = detalles.Select(x => x.FatherID).ToList();

                VentaData venta;
                foreach (var id in idVentas)
                {
                    venta = GetByID(id);
                    ventas.Add(venta);
                }

                ventas = ventas.FindAll(v => v.Local.ID == idlocal);
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(substring, "Venta_GetVentasConDetalle"), true, true);

                throw;

            }

          
           
            return ventas;

        }

        public List<VentaDetalleData> GetDetallesDescriptionAndSetCantidad(Guid idFather)
        {

            try
            {
                List<VentaDetalleData> childs = _repoDetalle.GetDetalles(idFather);

                StockService.StockService stockService = new StockService.StockService();

                StockData astock;
                foreach (VentaDetalleData detalle in childs)
                {
                    astock = stockService.obtenerProducto(detalle.Codigo);
                    
                    if (astock.Metros > 0)
                        detalle.Cantidad = astock.Metros * detalle.Cantidad;

                    detalle.SetSubtotal(CalcularSubtotalDetalle(detalle));//para el print
                    detalle.SetSubtotalConIva(CalcularSubtotalIVADetalle(detalle));//para el print
                    detalle.SetDescription(astock.Producto.Description + "-" + astock.Color.Description);//para el print

                }

                return childs;
            }
            catch (Exception e)
            {

                
                       HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idFather, "Ventas_GetDetallesDescriptionAndSetCantidad"), true, true);

                throw;

            }
           
        }

        private decimal CalcularSubtotalIVADetalle(VentaDetalleData detalle)
        {
            decimal subtotalBonificacion = CalcularSubtotalDetalle(detalle);
            return ((((detalle.Alicuota * subtotalBonificacion) / 100)) + subtotalBonificacion);
        }

        private decimal CalcularSubtotalDetalle(VentaDetalleData detalle)
        {
            decimal subtotal = detalle.PrecioUnidad * detalle.Cantidad;

            decimal Bonificacion = HelperService.ConvertToDecimalSeguro((subtotal * detalle.Bonificacion) / 100);
            return (subtotal - Bonificacion);


        }



      

        public void PrepareBeforePrint(VentaData aVenta)
        {
            aVenta.Children = GetDetallesDescriptionAndSetCantidad(aVenta.ID);
            aVenta.Tributos = TributoNexoService.setDescriptions(aVenta.Tributos);
        }
    }
}
