using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Services.ChequeService;
using Services.ClienteService;
using Services.ReciboService;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Testing.Services
{
    [TestClass()]
    public class ReciboServiceTest
    {

        private Fixture fixture = new Fixture();

        [TestMethod]
        public void GetbyClienteTest()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.ClienteService);

                fixture = HelperTesting.SetUp(testList, true);


                var clienteService = new ClienteService();
                var reciboService = new ReciboService();


                ClienteData cliente1 = fixture.Create<ClienteData>();
                ClienteData cliente2 = fixture.Create<ClienteData>();

                Assert.IsTrue(clienteService.Insert(cliente1));
                Assert.IsTrue(clienteService.Insert(cliente2));

                ReciboData r1 = fixture.Create<ReciboData>();
                ReciboData r2 = fixture.Create<ReciboData>();
                ReciboData r3 = fixture.Create<ReciboData>();
                ReciboData r4 = fixture.Create<ReciboData>();

                r1.tercero = cliente1;
                r2.tercero = cliente1;
                r3.tercero = cliente1;
                r4.tercero = cliente2;


                Assert.IsTrue(reciboService.Insert(r1));
                Assert.IsTrue(reciboService.Insert(r2));
                Assert.IsTrue(reciboService.Insert(r3));
                Assert.IsTrue(reciboService.Insert(r4));



                List<ReciboData> reciboDb1 = reciboService.GetbyCliente(cliente1.ID, false);
                List<ReciboData> reciboDb2 = reciboService.GetbyCliente(cliente2.ID, false);


                Assert.IsTrue(reciboDb1!=null && reciboDb1.Count==3);
                Assert.IsTrue(reciboDb2 != null && reciboDb2.Count == 1);
            }
        }


        [TestMethod]
        public void GetByChequeTest()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.ClienteService);
                testList.Add(HelperTesting.ServicesEnum.ChequeService);

                fixture = HelperTesting.SetUp(testList, true);


                var clienteService = new ClienteService();
                var reciboService = new ReciboService();
                var chequeService = new ChequeService();



                ClienteData cliente1 = fixture.Create<ClienteData>();
                ClienteData cliente2 = fixture.Create<ClienteData>();
                
                ChequeData cheque1 = fixture.Create<ChequeData>();
                ChequeData cheque2 = fixture.Create<ChequeData>();
                

                Assert.IsTrue(chequeService.Insert(cheque1));
                Assert.IsTrue(chequeService.Insert(cheque2));

                Assert.IsTrue(clienteService.Insert(cliente1));
                Assert.IsTrue(clienteService.Insert(cliente2));



                ReciboData r1 = fixture.Create<ReciboData>();
                ReciboData r2 = fixture.Create<ReciboData>();
                ReciboData r3 = fixture.Create<ReciboData>();
                ReciboData r4 = fixture.Create<ReciboData>();

                r1.tercero = cliente1;
                r2.tercero = cliente1;
                r3.tercero = cliente1;
                r4.tercero = cliente2;


                r1.Children[0].Cheque = cheque1;
                ReciboOrdenPagoDetalleData d1 = r1.Children[0];
                r1.Children = new List<ReciboOrdenPagoDetalleData>();
                r1.Children.Add(d1);

                r4.Children = new List<ReciboOrdenPagoDetalleData>();
                r2.Children = new List<ReciboOrdenPagoDetalleData>();

                r3.Children[0].Cheque = cheque2;
                ReciboOrdenPagoDetalleData d2 = r3.Children[0];
                r3.Children = new List<ReciboOrdenPagoDetalleData>();
                r3.Children.Add(d2);

                Assert.IsTrue(reciboService.Insert(r1));
                Assert.IsTrue(reciboService.Insert(r2));
                Assert.IsTrue(reciboService.Insert(r3));
                Assert.IsTrue(reciboService.Insert(r4));



                ReciboData reciboDb1 = reciboService.GetReciboDeCheque(cheque1.ID);
                ReciboData reciboDb2 = reciboService.GetReciboDeCheque(cheque2.ID);


                Assert.IsTrue(reciboDb1 != null && reciboDb1.ID == r1.ID);
                Assert.IsTrue(reciboDb2 != null && reciboDb2.ID == r3.ID);
                
            }
        }
    }
}
