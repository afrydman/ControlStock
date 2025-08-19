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
    public class GenericGetRangeDateTest
    {
        Fixture fixture = new Fixture();

        [TestMethod]
        public void VentaServices_GetRangeDate()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetRangeDateFather(new VentaService());
        }

        [TestMethod]
        public void RemitoServices_GetRangeDate()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetRangeDateFather(new RemitoService());
        }


        [TestMethod]
        public void ReciboServices_GetRangeDate()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetRangeDateFather(new ReciboService());
        }


        [TestMethod]
        public void OrdenPagoServices_GetRangeDate()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetRangeDateFather(new OrdenPagoService());
        }


        [TestMethod]
        public void ComprasProveedorServices_GetRangeDate()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetRangeDateFather(new ComprasProveedorService());
        }
        [TestMethod]
        public void NotaCreditoCliente_GetRangeDate()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetRangeDateFather(new NotaService(new NotaCreditoClienteRepository(), new NotaCreditoClienteDetalleRepository(), new TributoNotaCreditoClientesRepository()), difExpected: 0);// se espera que falle pagos..
        }

        [TestMethod]
        public void NotaCreditoProveedor_GetRangeDate()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList, false);
            GetRangeDateFather(new NotaService(new NotaCreditoProveedoresRepository(), new NotaCreditoProveedoresDetalleRepository(), new TributoNotaCreditoProveedoresRepository()), difExpected: 0);// se espera que falle pagos..
        }

        [TestMethod]
        public void NotaDebitoCliente_GetRangeDate()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetRangeDateFather(new NotaService(new NotaDebitoClienteRepository(), new NotaDebitoClienteDetalleRepository(), new TributoNotaDebitoClientesRepository()), difExpected: 0);// se espera que falle pagos..
        }

        [TestMethod]
        public void NotaDebitoProveedor_GetRangeDate()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList, false);
            GetRangeDateFather(new NotaService(new NotaDebitoProveedoresRepository(), new NotaDebitoProveedoresDetalleRepository(), new TributoNotaDebitoProveedoresRepository()), difExpected: 0);// se espera que falle pagos..
        }

        [TestMethod]
        public void PuntoControl_GetRangeDate()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList, false);
            GetRangeDateFather(new PuntoControlService(), difExpected: 0);
        }


        [TestMethod]
        public void Retiroervices_GetRangeDate()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetRangeDate(new RetiroService());
        }


        [TestMethod]
        public void Ingresorvices_GetRangeDate()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetRangeDate(new IngresoService());
        }

        [TestMethod]
        public void ValeService_GetRangeDate()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            GetRangeDate(new ValeService());
        }



        private void GetRangeDate<X, Y>(ObjectGetterService<X, Y> service, int difExpected = 0)

            where X : MovimientoEnCajaData

            where Y : IGenericGetterRepository<X>
        {

            LocalData local = fixture.Create<LocalData>();
            int prefix = 1;
            DateTime Date1 = new DateTime(1900, 10, 10);
            DateTime Date5 = new DateTime(1905, 10, 10);
            DateTime Date10 = new DateTime(1910, 10, 10);
            DateTime Date20 = new DateTime(1920, 10, 10);
            DateTime Date21 = new DateTime(1921, 10, 10);


            X object1 = fixture.Create<X>();
            X object2 = fixture.Create<X>();
            X object3 = fixture.Create<X>();



            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                object1.Local = local;
                object1.Prefix = prefix;
                object1.Date = Date21;

                object2.Local = local;
                object2.Prefix = prefix;
                object2.Date = Date10;


                bool task = service.Insert(object1);

                Assert.IsTrue(task);

                List<X> itemsBIggersNullexpected = service.GetByRangoFecha(Date1, Date20, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected == null || itemsBIggersNullexpected.Count == 0);



                task = service.Insert(object2);

                Assert.IsTrue(task);

                itemsBIggersNullexpected = service.GetByRangoFecha(Date1, Date20, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected != null && itemsBIggersNullexpected.Count == 1);

                itemsBIggersNullexpected = service.GetByRangoFecha(Date1, Date21, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected != null && itemsBIggersNullexpected.Count == 2);

                itemsBIggersNullexpected = service.GetByRangoFecha(Date10, Date21, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected != null && itemsBIggersNullexpected.Count == 2);

            }
        }


        private void GetRangeDateFather<X, Y, A, B>(FatherService<X, Y, A, B> service, int difExpected = 0)

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
            DateTime Date20 = new DateTime(1920, 10, 10);
            DateTime Date21 = new DateTime(1921, 10, 10);


            X object1 = fixture.Create<X>();
            X object2 = fixture.Create<X>();
            X object3 = fixture.Create<X>();



            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                object1.Local = local;
                object1.Prefix = prefix;
                object1.Date = Date21;

                object2.Local = local;
                object2.Prefix = prefix;
                object2.Date = Date10;


                bool task = service.Insert(object1);

                Assert.IsTrue(task);

                List<X> itemsBIggersNullexpected = service.GetByRangoFecha(Date1,Date20, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected == null || itemsBIggersNullexpected.Count == 0);



                task = service.Insert(object2);

                Assert.IsTrue(task);

                itemsBIggersNullexpected = service.GetByRangoFecha(Date1, Date20, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected != null && itemsBIggersNullexpected.Count == 1);

                itemsBIggersNullexpected = service.GetByRangoFecha(Date1, Date21, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected != null && itemsBIggersNullexpected.Count == 2);

                itemsBIggersNullexpected = service.GetByRangoFecha(Date10, Date21, local.ID, prefix, false);

                Assert.IsTrue(itemsBIggersNullexpected != null && itemsBIggersNullexpected.Count == 2);

            }


        }
    }
}
