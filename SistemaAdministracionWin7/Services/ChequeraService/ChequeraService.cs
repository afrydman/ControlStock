using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using ObjectDumper;
using Repository.ChequeraRepository;
using Repository.ChequeRepository;
using Repository.Repositories.BancosRepository;
using Repository.Repositories.CuentaRepository;
using Services.AbstractService;

namespace Services.ChequeraService
{
    public class ChequeraService :   ObjectService<ChequeraData, IChequeraRepository>, IChequeraService
    {
    
        public ChequeraService(IChequeraRepository chequeraRepository)
        {
            _repo = chequeraRepository;
        }

         public ChequeraService(bool local = true)
         {
             _repo = new ChequeraRepository(local);
         }
      

        public override List<ChequeraData> NormalizeList(List<ChequeraData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(x => x.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => x.CodigoInterno.CompareTo(y.CodigoInterno));
            
            return list;
        }

        public override ChequeraData getPropertiesInfo(ChequeraData c)
        {
            var bancoService = new BancoService.BancoService(new BancoRepository());
            var cuentaService = new CuentaService.CuentaService(new CuentaRepository());

            
            if (c.Cuenta != null && c.Cuenta.ID != Guid.Empty)
                c.Cuenta = cuentaService.GetByID(c.Cuenta.ID);

            if (c.Cuenta != null && c.Cuenta.Banco != null && c.Cuenta.Banco.ID != Guid.Empty)
                c.Cuenta.Banco = bancoService.GetByID(c.Cuenta.Banco.ID);
            

            return c;
        }

        
        public string GetNextNumberAvailable()
        {

            List<ChequeraData> cs = null;
            try
            {
                cs = GetAll(false);

                if (cs != null && cs.Count > 0)
                {//ver si es el ultimo o el primero!
                    return (cs[cs.Count - 1].CodigoInterno + 1).ToString("0000");
                }
                return "0001";

            }
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(cs, "ChequeraService_GetNextNumberAvailable"), true, true);

                throw;

            }
        }

     

        public bool existeEsteCheque(Guid idChequera, string numeroVerificar)
        {
            var chequeService = new ChequeService.ChequeService(new ChequeRepository());

            List<ChequeData> chequesChequera = chequeService.GetByChequera(idChequera, false);

            return (chequesChequera.Find(c => c.Enable && c.Numero == numeroVerificar))!=null;
            
        }

        public bool SetearSiguiente(ChequeraData chequera)//de mis propias chequeras
        {

            try
            {


                int len1 = 0;
                int len2 = 0;
                len1 = chequera.Ultimo.Length;


                string newSiguiente = "0"; // (Convert.ToInt32(chequera.Siguiente) + 1).ToString();
                var chequeService = new ChequeService.ChequeService(new ChequeRepository());


                List<ChequeData> chequesChequera = chequeService.GetByChequera(chequera.ID, true);

                chequesChequera.Sort((x, y) => Convert.ToInt32(x.Numero).CompareTo(Convert.ToInt32(y.Numero)));
                    //porque los cheques propios tienen todos interno = 1.

                var last = Convert.ToInt32(chequesChequera[0].Numero) - 1;
                foreach (var chequeData in chequesChequera)
                {

                    if (last + 1 == Convert.ToInt32(chequeData.Numero))
                    {
                        last = Convert.ToInt32(chequeData.Numero);
                    }
                    else
                    {
                        newSiguiente = (last + 1).ToString();
                        if (
                            chequesChequera.Find(
                                data => Convert.ToInt32(data.Numero) == Convert.ToInt32(newSiguiente) && data.Enable) ==
                            null) //verifico que no este entre todos el potencial siguiente.
                            break;
                    }
                }

                if (newSiguiente == "0")
                {
                    newSiguiente = (last + 1).ToString();
                }

                len2 = newSiguiente.Length;

                string aux = "";
                for (int i = 0; i < len1 - len2; i++)
                {
                    aux += "0";
                }
                aux += newSiguiente;

                return _repo.SetearSiguiente(chequera.ID, aux);
            } 
            catch (Exception e)
            {

                HelperService.WriteException(e);

                HelperService.writeLog(
                                ObjectDumperExtensions.DumpToString(chequera, "ChequeraService_SetearSiguiente"), true, true);

                throw;

            }
        }
    }
}
