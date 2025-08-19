using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Services.ComprasProveedorService;
using Services.CuentaService;
using Services.ProveedorService;

namespace Testing.Services
{

    [TestClass()]
    public class CuentaServiceTest
    {

        private Fixture fixture = new Fixture();

        [TestMethod]
        public void GetByTipoTest()
        {


            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {


                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.CuentaService);
                
                fixture = HelperTesting.SetUp(testList, true);

                var cuentaService = new CuentaService();


                var cuentaData1 = fixture.Create<CuentaData>();
                var cuentaData2 = fixture.Create<CuentaData>();
                var cuentaData3 = fixture.Create<CuentaData>();
                var cuentaData4 = fixture.Create<CuentaData>();
                

                cuentaData1.TipoCuenta = TipoCuenta.Banco;
                cuentaData2.TipoCuenta = TipoCuenta.Cartera;
                cuentaData3.TipoCuenta = TipoCuenta.Otra;
                cuentaData4.TipoCuenta = TipoCuenta.Tarjeta;


                List<CuentaData> PrecuentasDb1 = cuentaService.GetCuentasByTipo(TipoCuenta.Banco);
                List<CuentaData> PrecuentasDb2 = cuentaService.GetCuentasByTipo(TipoCuenta.Cartera);
                List<CuentaData> PrecuentasDb3 = cuentaService.GetCuentasByTipo(TipoCuenta.Otra);
                List<CuentaData> PrecuentasDb4 = cuentaService.GetCuentasByTipo(TipoCuenta.Tarjeta);


                int pre1 = 0;
                int pre2 = 0;
                int pre3 = 0;
                int pre4 = 0;

                if (PrecuentasDb1 != null) pre1 = PrecuentasDb1.Count;
                if (PrecuentasDb2 != null) pre2 = PrecuentasDb2.Count;
                if (PrecuentasDb3 != null) pre3 = PrecuentasDb3.Count;
                if (PrecuentasDb4 != null) pre4 = PrecuentasDb4.Count;

                Assert.IsTrue(cuentaService.Insert(cuentaData1));
                Assert.IsTrue(cuentaService.Insert(cuentaData2));
                Assert.IsTrue(cuentaService.Insert(cuentaData3));
                Assert.IsTrue(cuentaService.Insert(cuentaData4));


                List<CuentaData> cuentasDb1 = cuentaService.GetCuentasByTipo(TipoCuenta.Banco,false);
                List<CuentaData> cuentasDb2 = cuentaService.GetCuentasByTipo(TipoCuenta.Cartera, false);
                List<CuentaData> cuentasDb3 = cuentaService.GetCuentasByTipo(TipoCuenta.Otra, false);
                List<CuentaData> cuentasDb4 = cuentaService.GetCuentasByTipo(TipoCuenta.Tarjeta, false);



                Assert.IsTrue(cuentasDb1!=null && cuentasDb1.Count-pre1==1 && cuentasDb1[0].TipoCuenta==TipoCuenta.Banco);
                Assert.IsTrue(cuentasDb2 != null && cuentasDb2.Count - pre2 == 1 && cuentasDb2[0].TipoCuenta == TipoCuenta.Cartera);
                Assert.IsTrue(cuentasDb3 != null && cuentasDb3.Count - pre3 == 1 && cuentasDb3[0].TipoCuenta == TipoCuenta.Otra);
                Assert.IsTrue(cuentasDb4 != null && cuentasDb4.Count - pre4 == 1 && cuentasDb4[0].TipoCuenta == TipoCuenta.Tarjeta);



            }
        }

        [TestMethod]
        public void UpdateSaldoTest()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {


                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.CuentaService);

                fixture = HelperTesting.SetUp(testList, true);

                var cuentaService = new CuentaService();

                var cuentaData1 = fixture.Create<CuentaData>();
                var saldo1 = 1000;
                var saldo2 = 1000;
                var saldo3 = 5000;

                var cuentaDB = new CuentaData();

                Guid idCuenta = Guid.NewGuid();
                cuentaData1.ID = idCuenta;
                cuentaData1.Saldo = saldo1;

                Assert.IsTrue(cuentaService.Insert(cuentaData1));


                Assert.IsTrue(cuentaService.UpdateSaldo(idCuenta,saldo2,true));

                cuentaDB = cuentaService.GetByID(idCuenta);


                Assert.IsTrue(cuentaDB.Saldo==saldo1+saldo2);

                Assert.IsTrue(cuentaService.UpdateSaldo(idCuenta, saldo3, false));

                cuentaDB = cuentaService.GetByID(idCuenta);


                Assert.IsTrue(cuentaDB.Saldo == saldo3);

            }
        }
    }
}
