using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Repository;
using Services.AbstractService;
using Services.AdministracionService;
using Services.BancoService;
using Services.ChequeraService;
using Services.ClienteService;
using Services.ColorService;
using Services.CuentaService;
using Services.LineaService;
using Services.ListaPrecioService;
using Services.LocalService;
using Services.PersonalService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.TemporadaService;
using Services.TributoService;
using Services.ValeService;
using Services.VentaService;

namespace Testing.Generics
{
    [TestClass()]
    public class genericUpdateTest
    {
        Fixture fixture = new Fixture();

        [TestMethod]
        public void TipoIngresoService_Update()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.TipoIngresoService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateGenericObject(new TipoIngresoService()));
        }


        [TestMethod]
        public void TipoRetiroService_Update()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.TipoRetiroService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateGenericObject(new TipoRetiroService()));
        }

        [TestMethod]
        public void BancoService_Update()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.BancoService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateGenericObject(new BancoService()));
        }

        [TestMethod]
        public void LineaService_Update()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.LineaService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateGenericObject(new LineaService()));
        }

        [TestMethod]
        public void ListaPrecioService_Update()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ListaPrecioService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateGenericObject(new ListaPrecioService()));
        }


        [TestMethod]
        public void ColorService_Update()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ColorService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateGenericObject(new ColorService()));
        }

        [TestMethod]
        public void TemporadaService_Update()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.TemporadaService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateGenericObject(new TemporadaService()));
        }


        [TestMethod]
        public void ClienteService_Update()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ClienteService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateComplet(new ClienteService(), difExpected: 3));
        }

        [TestMethod]
        public void LocalService_Update()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.LocalService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateComplet(new LocalService(), difExpected: 0));
        }

        [TestMethod]
        public void VentaService_Update()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateComplet(new VentaService(), difExpected: 10));//duduso este test. Solo se tiene que updetear la description + cae y cae vto.
        }


        [TestMethod]
        public void PersonalService_Update()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.PersonalService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateComplet(new PersonalService(), difExpected: 5));
        }


        [TestMethod]
        public void TributoService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();

            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateGenericObject(new TributoService()));
        }


        [TestMethod]
        public void ProveedorService_Update()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ProveedorService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateComplet(new ProveedorService(), difExpected: 0));
        }

        [TestMethod]
        public void ProductoService_Update()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ProductoService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateComplet(new ProductoService(), difExpected: 0));
        }

        [TestMethod]
        public void CuentaService_Update()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ClienteService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateComplet(new CuentaService()));
        }

        [TestMethod]
        public void ChequeraService_Update()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ChequeraService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(UpdateComplet(new ChequeraService(), difExpected: 1));//el Numero interno no se updetea
        }


        private bool UpdateComplet<X, Y>(ObjectService<X, Y> service, int difExpected = 0)
            where X : GenericObject, new()
            where Y : IGenericRepository<X>
        {
            bool p = false;

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                X object2 = null;
                Guid id = Guid.NewGuid();

                X object1 = fixture.Create<X>();
                object1.ID = id;
                object1.Enable = false;
                bool task = service.Insert(object1);

                Assert.IsTrue(task);

                object1 = fixture.Create<X>();
                object1.ID = id;
                task = service.Update(object1);

                Assert.IsTrue(task);

                object2 = service.GetByID(id);


                List<string> dif = HelperTesting.GetDifferences(object1, object2);

                p = (dif == null || dif.Count - difExpected == 0);

            }
            return p;
        }

        private bool UpdateGenericObject<X, Y>(ObjectService<X, Y> service, int difExpected = 0)
            where X : GenericObject, new()
            where Y : IGenericRepository<X>
        {
            bool p = false;

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                X object2 = null;
                Guid id = Guid.NewGuid();

                X object1 = fixture.Create<X>();
                object1.ID = id;
                object1.Enable = false;
                bool task = service.Insert(object1);

                Assert.IsTrue(task);

                object1.Description = "nueva";
                task = service.Update(object1);

                Assert.IsTrue(task);

                object2 = service.GetByID(id);


                List<string> dif = HelperTesting.GetDifferences(object1, object2);

                p = (dif == null || dif.Count - difExpected == 0);

            }
            return p;
        }
    }
}
