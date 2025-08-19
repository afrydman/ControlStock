using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Repository;
using Repository.Repositories.NotasRepository;
using Repository.Repositories.VentaRepository;
using Services.AbstractService;
using Services.ComprasProveedorService;
using Services.IngresoService;
using Services.NotaService;
using Services.OrdenPagoService;
using Services.PuntoControlService;
using Services.ReciboService;
using Services.RemitoService;
using Services.RetiroService;
using Services.ValeService;
using Services.VentaService;

namespace Testing.Generics.Getters
{

      [TestClass()]
    public class GenericGetOlderFatherTest
    {
        Fixture fixture = new Fixture();

        [TestMethod]
        public void VentaServices_GetOlderFather()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetOlderFather(new VentaService());
        }


        [TestMethod]
        public void RemitoServices_GetOlderFather()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetOlderFather(new RemitoService());
        }


        [TestMethod]
        public void ReciboServices_GetOlderFather()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetOlderFather(new ReciboService());
        }


        [TestMethod]
        public void OrdenPagoServices_GetOlderFather()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetOlderFather(new OrdenPagoService());
        }


        [TestMethod]
        public void ComprasProveedorServices_GetOlderFather()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetOlderFather(new ComprasProveedorService());
        }
        [TestMethod]
        public void NotaCreditoCliente_GetBIgger()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetOlderFather(new NotaService(new NotaCreditoClienteRepository(), new NotaCreditoClienteDetalleRepository(), new TributoNotaCreditoClientesRepository()), difExpected: 0);// se espera que falle pagos..
        }

        [TestMethod]
        public void NotaCreditoProveedor_GetBIgger()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList, false);
            GetOlderFather(new NotaService(new NotaCreditoProveedoresRepository(), new NotaCreditoProveedoresDetalleRepository(), new TributoNotaCreditoProveedoresRepository()), difExpected: 0);// se espera que falle pagos..
        }

        [TestMethod]
        public void NotaDebitoCliente_GetBIgger()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetOlderFather(new NotaService(new NotaDebitoClienteRepository(), new NotaDebitoClienteDetalleRepository(), new TributoNotaDebitoClientesRepository()), difExpected: 0);// se espera que falle pagos..
        }

        [TestMethod]
        public void NotaDebitoProveedor_GetBIgger()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList, false);
            GetOlderFather(new NotaService(new NotaDebitoProveedoresRepository(), new NotaDebitoProveedoresDetalleRepository(), new TributoNotaDebitoProveedoresRepository()), difExpected: 0);// se espera que falle pagos..
        }


        [TestMethod]
        public void Ingresos_GetOlder()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetOlder(new IngresoService());
        }


        [TestMethod]
        public void retiros_GetOlder()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetOlder(new RetiroService());
        }
        [TestMethod]
        public void vales_GetOlder()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetOlder(new ValeService());
        }

        [TestMethod]
        public void PuntoControl_GetOlder()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetOlderFather(new PuntoControlService());
        }


        private void GetOlder<X, Y>(ObjectGetterService<X, Y> service, int difExpected = 0)

            where X : MovimientoEnCajaData

            where Y : IGenericGetterRepository<X>
        {
            LocalData local = fixture.Create<LocalData>();
            int prefix = 1;
            DateTime Date1 = new DateTime(1900, 10, 10);
            DateTime Date5 = new DateTime(1905, 10, 10);
            DateTime Date10 = new DateTime(1910, 10, 10);


            X object1 = fixture.Create<X>();
            X object2 = fixture.Create<X>();



            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                object1.Local = local;
                object1.Prefix = prefix;
                object1.Date = Date5;

                object2.Local = local;
                object2.Prefix = prefix;
                object2.Date = Date10;


                bool task = service.Insert(object1);

                Assert.IsTrue(task);

                List<X> itemsBIggersNullexpected = service.GetOlderThan(Date10, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected == null || itemsBIggersNullexpected.Count == 0);



                task = service.Insert(object2);

                Assert.IsTrue(task);

                itemsBIggersNullexpected = service.GetOlderThan(Date10, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected == null || itemsBIggersNullexpected.Count == 0);

                itemsBIggersNullexpected = service.GetOlderThan(Date5, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected != null && itemsBIggersNullexpected.Count == 1);

                itemsBIggersNullexpected = service.GetOlderThan(Date1, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected != null && itemsBIggersNullexpected.Count == 2);

            }
        }

          private void GetOlderFather<X, Y, A, B>(FatherService<X, Y, A, B> service, int difExpected = 0)

            where X : DocumentoGeneralData<Y>
            where Y : ChildData
            where A : IGenericFatherRepository<X>
            where B : IGenericChildRepository<Y>
        {

            LocalData local = fixture.Create<LocalData>();
            int prefix = 1;
            DateTime Date1 = new DateTime(1900, 10, 10);
            DateTime Date5 = new DateTime(1905, 10, 10);
            DateTime Date10 = new DateTime(1910, 10, 10);


            X object1 = fixture.Create<X>();
            X object2 = fixture.Create<X>();



            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                object1.Local = local;
                object1.Prefix = prefix;
                object1.Date = Date5;

                object2.Local = local;
                object2.Prefix = prefix;
                object2.Date = Date10;


                bool task = service.Insert(object1);

                Assert.IsTrue(task);

                List<X> itemsBIggersNullexpected = service.GetOlderThan(Date10, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected == null || itemsBIggersNullexpected.Count == 0);



                task = service.Insert(object2);

                Assert.IsTrue(task);

                itemsBIggersNullexpected = service.GetOlderThan(Date10, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected == null || itemsBIggersNullexpected.Count == 0);

                itemsBIggersNullexpected = service.GetOlderThan(Date5, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected != null && itemsBIggersNullexpected.Count == 1);

                itemsBIggersNullexpected = service.GetOlderThan(Date1, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected != null && itemsBIggersNullexpected.Count == 2);

            }


        }
    }
}
