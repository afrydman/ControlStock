using System;
using System.Collections.Generic;
using System.Transactions;

using DTO.BusinessEntities;
using ObjectDumper;
using Repository.FormaPagoRepository;
using Repository.Repositories.FormaPagoRepository;

namespace Services.FormaPagoService
{
    public class FormaPagoService
    {
        protected readonly IFormaPagoRepository _repo;
        protected readonly IFormaPagoCuotasRepository _repoDetalle;
        public FormaPagoService(IFormaPagoRepository repo, IFormaPagoCuotasRepository detalle)
        {
            _repo = repo;
            _repoDetalle = detalle;
        }

        public FormaPagoService(bool local = true)
        {
            _repo = new FormaPagoRepository(local);
            _repoDetalle = new FormaPagoCuotasRepository(local);
        }


        public bool Insert(FormaPagoData theObject)
        {
            bool tsk;
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };
            Guid idPadre;
            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                try
                {
                    
                    idPadre = theObject.ID;
                    if (theObject.ID == null || theObject.ID == new Guid())
                    {
                        idPadre = Guid.NewGuid();
                        theObject.ID = idPadre;
                    }
                    if (theObject.Credito)
                    {
                        if (!InsertDetalles(theObject))
                            return false;
                    }

                    if (_repo.Insert(theObject))
                    {
                        trans.Complete();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    HelperService.WriteException(e);

                    HelperService.writeLog(
                                    ObjectDumperExtensions.DumpToString(theObject, "Formapago_Insert"), true, true);

                    throw;
                }
            }
            return false;
        }

        private bool InsertDetalles(FormaPagoData theObject)
        {
            if (theObject.Cuotas == null)
                return false;

            foreach (var cuota in theObject.Cuotas)
            {
                cuota.FatherID = theObject.ID;

                if (cuota.ID == null || cuota.ID == new Guid())
                    cuota.ID = Guid.NewGuid();


                try
                {
                    if (!_repoDetalle.InsertDetalle(cuota))
                        return false;
                }
                catch (Exception e)
                {
                    HelperService.WriteException(e);

                    HelperService.writeLog(
                                    ObjectDumperExtensions.DumpToString(theObject, "FormapagoDetalle_Insert"), true, true);

                    throw;
                }
                
            }
            return true;
        }

        public bool Update(FormaPagoData theObject)
        {
            bool tsk;
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                try
                {
                    
                    FormaPagoData theDBObject = GetByID(theObject.ID);
                    if (theObject.Credito)
                    {

                        //busco en db si era credito,  updatear las cuotas....


                        if (theDBObject.Credito)
                        {


                            if (theObject.Cuotas == null)
                                return false;


                            foreach (var cuota in theObject.Cuotas)
                            {
                                cuota.FatherID = theObject.ID;
                                tsk = _repoDetalle.UpdateAumento(cuota);
                                if (!tsk)
                                    return false;
                            }
                        }
                        else
                        {//busco en db si era debito,  crear las cuotas....
                            if (!InsertDetalles(theObject))
                                return false;
                        }

                    }
                    else
                    {//ahora es debito


                        if (theDBObject.Credito)
                        {//si era credito borrar las cuotas

                            if (!_repoDetalle.DeleteCuotas(theObject.ID))
                                return false;
                        }//si era debito , no hay que hacer nada referido a las cuotas
                    }

                    tsk = _repo.Update(theObject);
                    if (tsk) trans.Complete();
                }
                catch (Exception e)
                {
                    HelperService.WriteException(e);

                    HelperService.writeLog(
                                    ObjectDumperExtensions.DumpToString(theObject, "Formapagocuota_UpdateAumento"), true, true);

                    throw;
                }
            }
            return tsk;
        }

        public bool Disable(FormaPagoData theObject)
        {
            try
            {
                return _repo.Disable(theObject.ID);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(theObject, "Formapagocuota_Disable"), true, true);

                throw;
            }
            
        }

        public bool Enable(FormaPagoData theObject)
        {
           
            try
            {
                return _repo.Enable(theObject.ID);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(theObject, "Formapagocuota_Enable"), true, true);

                throw;
            }
        }

        public List<FormaPagoData> GetAll(bool onlyEnable = true)
        {
           
            try
            {
                var aux = _repo.GetAll();


                return NormalizeList(aux, onlyEnable);
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(onlyEnable, "Formapagocuota_GetAll"), true, true);

                throw;
            }
        }

        public FormaPagoData GetByID(Guid idObject)
        {
            FormaPagoData aux = new FormaPagoData();


            try
            {
                aux = _repo.GetByID(idObject);

                aux = getPropertiesInfo(aux);
            }
            
                 catch (Exception e)
                {
                    HelperService.WriteException(e);

                    HelperService.writeLog(
                                    ObjectDumperExtensions.DumpToString(idObject, "Formapago_GetByID"), true, true);

                    throw;
                }
            
            return aux;
        }



        public FormaPagoData GetLast(Guid idLocal, int first)
        {
            try
            {
                return getPropertiesInfo(_repo.GetLast(idLocal, first));
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idLocal, "Formapagocuota_GetLast"), true, true);

                throw;
            }
           
        }


        public List<FormaPagoData> NormalizeList(List<FormaPagoData> ps, bool onlyEnable = true)
        {
            if (onlyEnable)
                ps = ps.FindAll(data => data.Enable);

            ps.ForEach(n => n = getPropertiesInfo(n));


            ps.Sort((x, y) => x.Description.CompareTo(y.Description));

            return ps;
        }

        public FormaPagoData getPropertiesInfo(FormaPagoData compra)
        {
            try
            {
                compra.Cuotas = compra.Credito ? _repoDetalle.GetDetalles(compra.ID) : new List<FormaPagoCuotaData>();// re importante che

                return compra;
            }
            catch (Exception e)
            {
                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(compra, "Formapagocuota_getPropertiesInfo"), true, true);

                throw;
            }
           
        }
    }
}
