using System;
using System.Collections.Generic;
using System.Transactions;

using DTO.BusinessEntities;
using ObjectDumper;
using Repository;
using Repository.ChequeRepository;
using Repository.Repositories.OrdenPagoRepository;
using Services.AbstractService;

namespace Services.OrdenPagoService
{
    public class OrdenPagoService : FatherService<OrdenPagoData, ReciboOrdenPagoDetalleData, IOrdenPagoRepository, IGenericChildRepository<ReciboOrdenPagoDetalleData>>
    {


            public OrdenPagoService(IOrdenPagoRepository opRepository,IOrdenPagoDetalleRepository repoD):base(opRepository,repoD)
        {
            
        }

             public OrdenPagoService(bool local = true):base()
         {
             _repo = new OrdenPagoRepository(local);
                 _repoDetalle = new OrdenPagoDetalleRepository(local);
         }



             public override bool Insert(OrdenPagoData OrdenPago)
             {



                 var chequeService = new ChequeService.ChequeService(new ChequeRepository());

                 var opts = new TransactionOptions
                 {
                     IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                 };

                 using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
                 {

                     try
                     {
                         

                         if (!base.Insert(OrdenPago))
                             return false;

                         foreach (ReciboOrdenPagoDetalleData det in OrdenPago.Children)
                         {
                             if (det.Cheque!= null && det.Cheque.ID != Guid.Empty)
                             {
                                 ChequeData c = chequeService.GetByID(det.Cheque.ID);
                                 c.EstadoCheque = EstadoCheque.Entregado;
                                 if (!chequeService.Update(c))
                                     return false;
                             }
                         }
                         trans.Complete();

                     }
                     catch (Exception e)
                     {

                         HelperService.WriteException(e);

                         HelperService.writeLog(
                                         ObjectDumperExtensions.DumpToString(OrdenPago, "OrdenPago_Insert"), true, true);


                         throw;

                     }
                 }
                 return true;
             }




             public override bool Disable(Guid id)
             {
                 try
                 {
                     return Disable(GetByID(id));
                 }
                 catch (Exception e)
                 {
                     HelperService.WriteException(e);

                     HelperService.writeLog(ObjectDumperExtensions.DumpToString(id, "OrdenPago_Disable"), true, true);
                 }
                 return false;
             }


             public override bool Disable(Guid id, bool UpdateStock)
             {
                 return Disable(id);
             }





             public override bool Disable(OrdenPagoData theObject)
             {
                 var chequeService = new ChequeService.ChequeService(new ChequeRepository());

                 bool task = false;

                 var opts = new TransactionOptions
                 {
                     IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                 };

                 using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
                 {

                     try
                     {
                         

                         foreach (ReciboOrdenPagoDetalleData item in theObject.Children)
                         {
                             if (item.Cheque != null && item.Cheque.ID != Guid.Empty)
                             {
                                 ChequeData c = chequeService.GetByID(item.Cheque.ID);
                                 c.EstadoCheque = EstadoCheque.EnCartera;
                                 task = chequeService.Update(c);


                                 if (!task) return false;
                             }
                         }

                         task = _repo.Disable(theObject.ID);
                         if (task) trans.Complete();
                     }
                     catch (Exception e)
                     {
                         HelperService.WriteException(e);

                         HelperService.writeLog(
                                         ObjectDumperExtensions.DumpToString(theObject, "OrdenPago_Disable"), true, true);

                         throw;

                     }
                 }

                 return task;
             }

        
        public override List<OrdenPagoData> NormalizeList(List<OrdenPagoData> aux, bool onlyEnable = true)
        {
            if(aux!= null && aux.Count>0) { 
            if (onlyEnable)
                aux = aux.FindAll(n => n.Enable);

            aux.ForEach(n => n = getPropertiesInfo(n));

            aux.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            }
            return aux;
        }

        public override OrdenPagoData getPropertiesInfo(OrdenPagoData theObject)
        {
            try
            {
                theObject.Children = _repoDetalle.GetDetalles(theObject.ID);
                theObject.Children.ForEach(x => getChildPropertiesInfo(x));
                return theObject;
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                    ObjectDumperExtensions.DumpToString(theObject, "OrdenPagoData_getPropertiesInfo"), true, true);

                throw;
            }
           
            
        }

        public  ReciboOrdenPagoDetalleData getChildPropertiesInfo(ReciboOrdenPagoDetalleData child)
        {
            var chequeService = new ChequeService.ChequeService();
            var cuentaService = new CuentaService.CuentaService();
            if (child.Cheque != null && child.Cheque.ID!=Guid.Empty)
                child.Cheque = chequeService.GetByID(child.Cheque.ID);

            if (child.Cuenta != null && child.Cuenta.ID != Guid.Empty)
                child.Cuenta = cuentaService.GetByID(child.Cuenta.ID);

            return child;
        }

        public OrdenPagoData GetOrdenQueEntregoCheque(Guid idcheque)
        {
            OrdenPagoData aux = new OrdenPagoData();
            


            try
            {
                aux = _repo.getOrdenByCheque(idcheque);

            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(aux, "Orden_GetOrdenQueEntregoCheque"), true, true);
                
                throw;

            }

            return getPropertiesInfo(aux);
        }

        public List<OrdenPagoData> GetbyProveedor(Guid idProveedor, bool enableOnly=true)
        {
            List<OrdenPagoData> aux = new List<OrdenPagoData>();

            try
            {

                aux = GetAll(enableOnly).FindAll(r => r.Tercero.ID == idProveedor);

            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(aux, "OrdenPago_GetbyProveedor"), true, true);

                throw;

            }

            return NormalizeList(aux, enableOnly);
        }

       
    }
}
