using System;
using System.Collections.Generic;
using DTO.BusinessEntities;
using Repository.CuentaRepository;
using Repository.Repositories.BancosRepository;
using Repository.Repositories.CuentaRepository;
using Services.AbstractService;

namespace Services.CuentaService
{
    public class CuentaService : ObjectService<CuentaData, ICuentaRepository>
    {
      
        public CuentaService(ICuentaRepository cuentaRepository):base(cuentaRepository)
        {
            _repo = cuentaRepository;
        }
        public CuentaService(bool local = true)
         {
             _repo = new CuentaRepository(local);
         }

        public override List<CuentaData> NormalizeList(List<CuentaData> list, bool onlyEnable = true)
        {
            if (onlyEnable)
                list = list.FindAll(data => data.Enable);

            list.ForEach(n => n = getPropertiesInfo(n));

            list.Sort((x, y) => System.String.Compare(x.Cuenta, y.Cuenta, System.StringComparison.Ordinal));


            return list;
        }

        public override CuentaData getPropertiesInfo(CuentaData theObject)
        {
            var bancoService = new BancoService.BancoService(new BancoRepository());


            if (theObject.Banco != null)
                theObject.Banco = bancoService.GetByID(theObject.Banco.ID);

            return theObject;
        }

       
        public List<CuentaData> GetCuentasByTipo(TipoCuenta tipo, bool onlyEnable = true)
        {
            List<CuentaData> aux = GetAll(onlyEnable);

            aux = aux.FindAll(c => c.TipoCuenta == tipo);

            return aux;
        }

        public bool UpdateSaldo(Guid cuenta, decimal saldoModificar, bool agregar = true)
        {
            
            if (agregar)
                saldoModificar += GetByID(cuenta).Saldo;
            
           
            try
            {
                return _repo.UpdateSaldo(cuenta, saldoModificar);
            }
            catch (Exception e)
            {
                HelperService.writeLog(
                          e.Message + Environment.NewLine + Environment.NewLine + e.StackTrace
                          + Environment.NewLine + "saldoModificar:" + saldoModificar
                          + Environment.NewLine + "cuenta:" + cuenta.ToString()
                          + Environment.NewLine + "agregar:" + agregar.ToString(), true, true);

                throw;


            }

            return false;
        }
    }
}
