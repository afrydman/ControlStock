using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Services.LocalService;
using Services.RemitoService;

namespace Testing.Services
{
    [TestClass()]
    public class RemitoServiceTest
    {

         private Fixture fixture = new Fixture();

        [TestMethod]
        public void GetByLocalDestinoTest()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.Codigo);

                fixture = HelperTesting.SetUp(testList, true);


                var remitoService = new RemitoService();
                var localService = new LocalService();


                var local1 = fixture.Create<LocalData>();
                local1.Description = "aaaaaaa";
                local1.ID = Guid.NewGuid();
                var local2 = fixture.Create<LocalData>();
                local2.ID = Guid.NewGuid();
                local2.Description = "BBBBBBBBB";

                Assert.IsTrue(localService.Insert(local1));
                Assert.IsTrue(localService.Insert(local2));


                var remito1 = fixture.Create<RemitoData>();
                var remito2 = fixture.Create<RemitoData>();
                var remito3 = fixture.Create<RemitoData>();
                var remito4 = fixture.Create<RemitoData>();



                remito1.LocalDestino = local1;
                remito3.LocalDestino = local1;


                Assert.IsTrue(remitoService.Insert(remito1));
                Assert.IsTrue(remitoService.Insert(remito2));
                Assert.IsTrue(remitoService.Insert(remito3));
                Assert.IsTrue(remitoService.Insert(remito4));

                List<RemitoData> remdb = remitoService.GetByLocalDestino(local1.ID,false,false);


                Assert.IsTrue(remdb != null && remdb.Count == 2);
            }
        }


        [TestMethod]
        public void GetByLocalOrigenTest()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.Codigo);

                fixture = HelperTesting.SetUp(testList, true);


                var remitoService = new RemitoService();
                var localService = new LocalService();


                var local1 = fixture.Create<LocalData>();
                local1.Description = "aaaaaaa";
                local1.ID = Guid.NewGuid();
                var local2 = fixture.Create<LocalData>();
                local2.ID = Guid.NewGuid();
                local2.Description = "BBBBBBBBB";

                Assert.IsTrue(localService.Insert(local1));
                Assert.IsTrue(localService.Insert(local2));


                var remito1 = fixture.Create<RemitoData>();
                var remito2 = fixture.Create<RemitoData>();
                var remito3 = fixture.Create<RemitoData>();
                var remito4 = fixture.Create<RemitoData>();



                remito1.Local= local1;
                remito3.Local= local1;


                Assert.IsTrue(remitoService.Insert(remito1));
                Assert.IsTrue(remitoService.Insert(remito2));
                Assert.IsTrue(remitoService.Insert(remito3));
                Assert.IsTrue(remitoService.Insert(remito4));

                List<RemitoData> remdb = remitoService.GetByLocalOrigen(local1.ID, false, false);


                Assert.IsTrue(remdb != null && remdb.Count == 2);
            }
        }

        [TestMethod]
        public void GetByLocalRecibidoYSinRecibir()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.Codigo);

                fixture = HelperTesting.SetUp(testList, true);


                var remitoService = new RemitoService();
                var localService = new LocalService();


                var local1 = fixture.Create<LocalData>();
                local1.Description = "aaaaaaa";
                local1.ID = Guid.NewGuid();
                var local2 = fixture.Create<LocalData>();
                local2.ID = Guid.NewGuid();
                local2.Description = "BBBBBBBBB";

                Assert.IsTrue(localService.Insert(local1));
                Assert.IsTrue(localService.Insert(local2));


                var remito1 = fixture.Create<RemitoData>();
                var remito2 = fixture.Create<RemitoData>();
                var remito3 = fixture.Create<RemitoData>();
                var remito4 = fixture.Create<RemitoData>();



                remito1.LocalDestino = local1;
                remito1.FechaRecibo = HelperDTO.BEGINNING_OF_TIME_DATE;//sin recibir!
                remito3.LocalDestino = local1;


                Assert.IsTrue(remitoService.Insert(remito1));
                Assert.IsTrue(remitoService.Insert(remito2));
                Assert.IsTrue(remitoService.Insert(remito3));
                Assert.IsTrue(remitoService.Insert(remito4));

                List<RemitoData> remdb = remitoService.getByLocalRecibido(local1.ID, false);


                Assert.IsTrue(remdb != null && remdb.Count == 1);
                Assert.IsTrue(remdb[0].ID==remito3.ID);

                
                
                
                List<RemitoData> remdb2 = remitoService.getByLocalSinRecibir(local1.ID, false);


                Assert.IsTrue(remdb2 != null && remdb2.Count == 1);
                Assert.IsTrue(remdb2[0].ID == remito1.ID);

            }
        }


        [TestMethod]
        public void ConfirmarReciboTest()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.Codigo);

                fixture = HelperTesting.SetUp(testList, true);


                var remitoService = new RemitoService();
                var localService = new LocalService();


                var local1 = fixture.Create<LocalData>();
                local1.Description = "aaaaaaa";
                local1.ID = Guid.NewGuid();
                var local2 = fixture.Create<LocalData>();
                local2.ID = Guid.NewGuid();
                local2.Description = "BBBBBBBBB";

                Assert.IsTrue(localService.Insert(local1));
                Assert.IsTrue(localService.Insert(local2));


                var remito1 = fixture.Create<RemitoData>();
                var remito2 = fixture.Create<RemitoData>();
                var remito3 = fixture.Create<RemitoData>();
                var remito4 = fixture.Create<RemitoData>();



                remito1.LocalDestino = local1;
                remito1.FechaRecibo = HelperDTO.BEGINNING_OF_TIME_DATE;//sin recibir!
                remito3.LocalDestino = local1;


                Assert.IsTrue(remitoService.Insert(remito1));
                Assert.IsTrue(remitoService.Insert(remito2));
                Assert.IsTrue(remitoService.Insert(remito3));
                Assert.IsTrue(remitoService.Insert(remito4));

                List<RemitoData> remdb = remitoService.getByLocalRecibido(local1.ID, false);


                Assert.IsTrue(remdb != null && remdb.Count == 1);
                Assert.IsTrue(remdb[0].ID == remito3.ID);

                Assert.IsTrue(remitoService.confirmarRecibo(remito1.ID));

                remdb = remitoService.getByLocalRecibido(local1.ID, false);


                Assert.IsTrue(remdb != null && remdb.Count == 2);
                



            }
        }

         [TestMethod]
        public void confirmarBajaTest()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.Codigo);

                fixture = HelperTesting.SetUp(testList, true);


                var remitoService = new RemitoService();
                var localService = new LocalService();


                var local1 = fixture.Create<LocalData>();
                local1.Description = "aaaaaaa";
                local1.ID = Guid.NewGuid();
                var local2 = fixture.Create<LocalData>();
                local2.ID = Guid.NewGuid();
                local2.Description = "BBBBBBBBB";

                Assert.IsTrue(localService.Insert(local1));
                Assert.IsTrue(localService.Insert(local2));


                var remito1 = fixture.Create<RemitoData>();
            


                remito1.LocalDestino = local1;
                remito1.FechaRecibo = HelperDTO.BEGINNING_OF_TIME_DATE;//sin recibir!
              

                Assert.IsTrue(remitoService.confirmarBaja(remito1));

              




            }
        }


    }
}
