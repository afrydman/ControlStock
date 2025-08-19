using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.BusinessEntities;
using Ploeh.AutoFixture;
using Services.ChequeService;
using Services.CuentaService;

namespace Testing
{
    public static class FactoryTesting
    {
        private static Fixture fixture;
        internal static DTO.BusinessEntities.MovimientoCuentaData GetMovimientoCobroCheque()
        { 
            var testList = new List<HelperTesting.ServicesEnum>();

            
            testList.Add(HelperTesting.ServicesEnum.Codigo);

            fixture = HelperTesting.SetUp(testList);
            MovimientoCuentaData movimientoDepositoConCheque = fixture.Create<MovimientoCuentaData>();



            //no tiene cuenta origen
            movimientoDepositoConCheque.cuentaOrigen = null;
            movimientoDepositoConCheque.cuentaDestino = GetCuentaBanco();

            //el cheque tiene que ser de tercero
            movimientoDepositoConCheque.cheque = ObtenerChequeTercero();
            movimientoDepositoConCheque.Monto = movimientoDepositoConCheque.cheque.Monto;
            return movimientoDepositoConCheque;

        }

        private static CuentaData GetCuentaOtros()
        {
            var testList = new List<HelperTesting.ServicesEnum>();
            var cuentaservice = new CuentaService();
            testList.Add(HelperTesting.ServicesEnum.CuentaService);
            fixture = HelperTesting.SetUp(testList);

            CuentaData aux = fixture.Create<CuentaData>();
            aux.TipoCuenta = TipoCuenta.Otra;
            aux.Descubierto = 0;
            aux.Saldo = 400;

            cuentaservice.Insert(aux);

            return aux;
        }

        private static CuentaData GetCuentaBanco()
        {
             
            var testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.CuentaService);
            var cuentaservice = new CuentaService();

            fixture = HelperTesting.SetUp(testList);

            CuentaData aux = fixture.Create<CuentaData>();
            aux.TipoCuenta=TipoCuenta.Banco;
            aux.Descubierto = 0;
            aux.Saldo = 400;
            
            cuentaservice.Insert(aux);

            return aux;
        }

        private static ChequeData ObtenerChequeTercero()
        {

            var chequeService = new ChequeService();

            ChequeData chequeTercero = chequeService.GetByID(new Guid("553C9621-57D1-47DE-88C7-5FDC48837B04"));

            
            return chequeTercero;

        }

        private static ChequeData ObtenerChequePropio()
        {
            var chequeService = new ChequeService();

            ChequeData chequeTercero = chequeService.GetByID(new Guid("964F1045-278A-4E77-BD3B-0C9FB1DAB7FC"));


            return chequeTercero;
        }


        internal static DTO.BusinessEntities.MovimientoCuentaData GetMovimientoExtraccion()
        {
            var testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.CuentaService);

            fixture = HelperTesting.SetUp(testList);
            MovimientoCuentaData movimientoDepositoSinCheque = fixture.Create<MovimientoCuentaData>();



            movimientoDepositoSinCheque.cheque = null;



            movimientoDepositoSinCheque.cuentaOrigen = GetCuentaBanco();
            movimientoDepositoSinCheque.cuentaDestino = GetCuentaOtros();
            return movimientoDepositoSinCheque;
        }

  



        internal static MovimientoCuentaData GetMovimientoPagoCheque()
        {
            var testList = new List<HelperTesting.ServicesEnum>();


            testList.Add(HelperTesting.ServicesEnum.Codigo);

            fixture = HelperTesting.SetUp(testList);
            MovimientoCuentaData movimientoDepositoConCheque = fixture.Create<MovimientoCuentaData>();



            //no tiene cuenta destinto
            movimientoDepositoConCheque.cuentaDestino = null;
            movimientoDepositoConCheque.cuentaOrigen = GetCuentaBanco();


            //el cheque tiene que ser mio
            movimientoDepositoConCheque.cheque = ObtenerChequePropio();
            movimientoDepositoConCheque.Monto = movimientoDepositoConCheque.cheque.Monto;
            return movimientoDepositoConCheque;





          
        }


        public static MovimientoCuentaData GetMovimientoDeposito()
        {
            var testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.CuentaService);

            fixture = HelperTesting.SetUp(testList);
            MovimientoCuentaData movimientoDepositoSinCheque = fixture.Create<MovimientoCuentaData>();



            movimientoDepositoSinCheque.cheque = null;



            movimientoDepositoSinCheque.cuentaDestino = GetCuentaBanco();
            movimientoDepositoSinCheque.cuentaOrigen = GetCuentaOtros();
            return movimientoDepositoSinCheque;
        }
    }
}
