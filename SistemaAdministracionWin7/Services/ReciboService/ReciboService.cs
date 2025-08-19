using System;
using System.Collections.Generic;
using System.Transactions;

using DTO.BusinessEntities;
using ObjectDumper;
using Repository.ChequeRepository;
using Repository.Repositories.ReciboRepository;
using Services.AbstractService;

namespace Services.ReciboService
{
    public class ReciboService : FatherService<ReciboData, ReciboOrdenPagoDetalleData, IReciboRepository, IReciboDetalleRepository>
       
    {
          public ReciboService(IReciboRepository reciboRepository,IReciboDetalleRepository reciboDetalle):base(reciboRepository,reciboDetalle)
        {
            
        }

        public ReciboService(bool local = true):base()
         {
             _repo = new ReciboRepository(local);
             _repoDetalle = new ReciboDetalleRepository(local);
          
         }
       

        public override bool Insert(ReciboData recibo)
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
                    

                    if (!base.Insert(recibo))
                        return false;

                    foreach (ReciboOrdenPagoDetalleData det in recibo.Children)
                    {
                        if (det.Cheque != null && det.Cheque.ID != Guid.Empty)
                        {
                            ChequeData c = chequeService.GetByID(det.Cheque.ID);
                            c.EstadoCheque = EstadoCheque.EnCartera;
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
                                    ObjectDumperExtensions.DumpToString(recibo, "Recibo_Insert"), true, true);

                    return false;

                }
            }
            return true;
        }

        public override bool Update(ReciboData theObject)
        {
            throw new NotImplementedException();
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

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(id, "Recibo_Disable"), true, true);
            }
            return false;
        }


        public override bool Disable(Guid id, bool UpdateStock)
        {
            return Disable(id);
        }





        public override bool Disable(ReciboData theObject)
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
                            c.EstadoCheque = EstadoCheque.Creado;
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
                                    ObjectDumperExtensions.DumpToString(theObject, "Recibo_Disable"), true, true);

                    return false;

                }
            }

            return task;
        }

        public override bool Enable(ReciboData theObject)
        {
            throw new NotImplementedException();
        }


        public override List<ReciboData> NormalizeList(List<ReciboData> list, bool onlyEnable)
        {
            if(list!=null && list.Count>0) { 
            if (onlyEnable)
                list = list.FindAll(data => data.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            }
            return list;
        }

        public override ReciboData getPropertiesInfo(ReciboData theObject)
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

                HelperService.writeLog(ObjectDumperExtensions.DumpToString(theObject, "Recibo_getPropertiesInfo"), true, true);


                throw;

            }
           

        }

        public ReciboOrdenPagoDetalleData getChildPropertiesInfo(ReciboOrdenPagoDetalleData child)
        {
            var chequeService = new ChequeService.ChequeService();
            var cuentaService = new CuentaService.CuentaService();

            if (child.Cheque != null && child.Cheque.ID != Guid.Empty) {
                child.Cheque = chequeService.GetByID(child.Cheque.ID);
            }
            else
            {
                child.Cheque = new ChequeData();
            }

            if (child.Cuenta != null && child.Cuenta.ID != Guid.Empty) {
                child.Cuenta = cuentaService.GetByID(child.Cuenta.ID);
            }
            else
            {
                child.Cuenta = new CuentaData();
            }

            return child;
        }

        public List<ReciboData> GetbyCliente(Guid idCliente,bool enableOnly=true)
        {
            try
            {


                List<ReciboData> aux = GetAll(enableOnly).FindAll(r => r.tercero.ID == idCliente);

                return NormalizeList(aux, enableOnly);
            }
            catch (Exception e)
            {

                
                         HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idCliente, "Recibo_GetbyCliente"), true, true);

                throw;

            }
        }

    
       
     


        public ReciboData GetReciboDeCheque(Guid idcheque)
        {
            try
            {


                List<ReciboOrdenPagoDetalleData> detalles = getPagosCheque();
                ReciboOrdenPagoDetalleData rr =
                    detalles.Find(delegate(ReciboOrdenPagoDetalleData r) { return r.Cheque.ID == idcheque; });

                return rr == null ? null : getPropertiesInfo(GetByID(rr.FatherID));
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idcheque, "Recibo_GetReciboDeCheque"), true, true);

                throw;

            }
        }

        public List<ReciboOrdenPagoDetalleData> getPagosCheque()
        {
            try
            {


                List<ReciboOrdenPagoDetalleData> detalles = getPagos();
                detalles =
                    detalles.FindAll(
                        delegate(ReciboOrdenPagoDetalleData r) { return r.Cheque != null && r.Cheque.ID != Guid.Empty; });

                return detalles;
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

               

                throw;

            }

        }

        private  List<ReciboOrdenPagoDetalleData> getPagos()
        {
            try
            {


                return _repoDetalle.GetAll();
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                

                throw;

            }

        }

     
    }
}
