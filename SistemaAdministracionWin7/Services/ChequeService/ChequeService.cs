using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.ChequeraRepository;
using Repository.ChequeRepository;
using Repository.Repositories.BancosRepository;
using Repository.Repositories.CuentaRepository;
using Services.AbstractService;

namespace Services.ChequeService
{
    public class ChequeService : ObjectService<ChequeData, IChequeRepository>,IChequeService
    {


        public ChequeService(IChequeRepository chequeRepository)
        {
            _repo = chequeRepository;
        }
        public ChequeService(bool local = true)
         {
             _repo = new ChequeRepository(local);
         }
        public int GetNextNumberAvailable()
        {
            int ultimo = _repo.ObtenerUltimoInterno();//devuelve el proximo interno. 0 fucks sobre si hay anulados 
            if (ultimo == -1)
            {
                return 1;
            }
            return ++ultimo;
        }

        public override bool Insert(ChequeData cheque)
        {

            if (cheque.ID == null || cheque.ID == Guid.Empty)
            {
                cheque.ID = Guid.NewGuid();
            }
            if (cheque.Interno==0)
            {
                cheque.Interno=GetNextNumberAvailable();
            }
            return base.Insert(cheque);
        }

        public List<ChequeData> GetByChequera(Guid idChequera, bool onlyEnable, List<EstadoCheque> estados=null, bool completo=true)
        {


           
            try
            {
                List<DTO.BusinessEntities.ChequeData> aux = _repo.GetByChequera(idChequera);

                return NormalizeList(aux, onlyEnable, estados);
            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(idChequera, "ChequeService_GetByChequera"), true, true);

                throw;

            }

        }






        public List<ChequeData> NormalizeList(List<ChequeData> list, bool onlyEnable = true, List<EstadoCheque> estados = null)
        {

            if (onlyEnable)
                list = list.FindAll(data => data.Enable);

            if (estados != null)
                list = list.FindAll(c => estados.Contains(c.EstadoCheque));

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => x.Interno.CompareTo(y.Interno));


            return list;

        }

        public override List<ChequeData> NormalizeList(List<ChequeData> list, bool onlyEnable = true)
        {
            return NormalizeList(list, onlyEnable, null);
        }

        public override ChequeData getPropertiesInfo(ChequeData c)
        {
        //    var bancoService = new BancoService.BancoService(new BancoRepository());
        //    var cuentaService = new CuentaService.CuentaService(new CuentaRepository());
        //    var chequeraService = new ChequeraService.ChequeraService(new ChequeraRepository());

        //    if (c.BancoEmisor!=null && c.BancoEmisor.ID!=Guid.Empty)
        //    {
        //        c.BancoEmisor = bancoService.GetByID(c.BancoEmisor.ID);    
        //    }
            //if (c.Chequera != null && c.Chequera.ID != Guid.Empty)
            //{
            //    c.Chequera = chequeraService.GetByID(c.Chequera.ID);
            //}
            //if (c.Chequera.Cuenta != null && c.Chequera.Cuenta.ID != Guid.Empty)
            //{
            //    c.Chequera.Cuenta = cuentaService.GetByID(c.Chequera.Cuenta.ID); 
            //}

       
           

            return c;
        }

        public List<ChequeData> GetChequesTercero(bool onlyEnable, List<EstadoCheque> estados = null, bool completo = true)
        {
            
            List<ChequeData> cheques = GetByChequera(Guid.Empty, onlyEnable, estados, completo);

            
            return NormalizeList(cheques); 
        }

        /// <summary>
        /// Devuelve los cheques de tercero en estado == En_cartera y los cheques propios con estado == Creado o estado==En_Cartera
        /// </summary>
        /// <param name="onlyEnable"></param>
        /// <returns></returns>
        public List<ChequeData> GetChequesUtilizables(bool onlyEnable=true)
        {
            var chequeraService = new ChequeraService.ChequeraService(new ChequeraRepository());
            
            List<ChequeraData> chequeras = chequeraService.GetAll(onlyEnable);
            List<EstadoCheque> estados = new List<EstadoCheque>();

            estados.Add(EstadoCheque.EnCartera);

            List<ChequeData> cheques = GetChequesTercero(onlyEnable, estados, true);
            estados.Add(EstadoCheque.Creado);
            foreach (ChequeraData chequera in chequeras)
            {
                cheques.AddRange(GetByChequera(chequera.ID, true, estados, onlyEnable));
            }

            return NormalizeList(cheques, onlyEnable, estados);
        }

        public bool InternNumberIsValid(string interno)
        {
            List<ChequeData> todos = _repo.GetAll();

            ChequeData c = todos.Find(delegate(ChequeData x) { return x.Interno.ToString() == interno && x.Enable; });


            return c == null;
        }


        public bool MarcarComo(ChequeData cheque, EstadoCheque nuevoEstado, DateTime? fecha = null, string obs = "")
        {
            if (fecha == null)
            {
                fecha = DateTime.Now;
            }
            if (obs != "")
            {
                cheque.Description += "\n \n " + obs;
            }

            if (nuevoEstado == EstadoCheque.Anulado)
                cheque.Enable = false;

            cheque.EstadoCheque = nuevoEstado;

            cheque.FechaAnuladooRechazado = fecha.Value;
        
            return Update(cheque);

        }






        public List<ChequeData> GetByRangoFecha(DateTime fecha1, DateTime fecha2, Guid idLocal, bool enableOnly = true)
        {
            throw new NotImplementedException();
        }

        public List<ChequeData> GetBiggerThan(int ultimo, Guid idLocal, int prefix)
        {
            throw new NotImplementedException();
        }

        public List<ChequeData> GetOlderThan(DateTime ultimo, Guid idLocal, int prefix)
        {
            throw new NotImplementedException();
        }

        public List<ChequeData> GetModified(Guid idLocal, int prefix)
        {
            throw new NotImplementedException();
        }

        public bool MarkSeen(Guid idLocal, int prefix)
        {
            throw new NotImplementedException();
        }
    }
}
