using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Repository;
using Repository.Repositories.OrdenPagoRepository;
using Repository.Repositories.ReciboRepository;
using Services.AbstractService;
using Services.AdministracionService;
using Services.BancoService;
using Services.ChequeraService;
using Services.ChequeService;
using Services.ClienteService;
using Services.ColorService;
using Services.CuentaService;
using Services.IngresoService;
using Services.LineaService;
using Services.ListaPrecioService;
using Services.LocalService;
using Services.OrdenPagoService;
using Services.PersonalService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.ReciboService;
using Services.RetiroService;
using Services.TemporadaService;
using Services.TributoService;
using Services.ValeService;

namespace Testing
{
    [TestClass()]
    public class genericEnableTest
    {


        Fixture fixture = new Fixture();

        [TestMethod]
        public void RetiroServices_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new RetiroService()));
        }

        [TestMethod]
        public void ProveedoresServices_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.condicionIvaService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new ProveedorService()));
        }

        [TestMethod]
        public void IngresoService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new IngresoService()));
        }



        [TestMethod]
        public void TipoIngresoService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.TipoIngresoService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new TipoIngresoService()));
        }


        [TestMethod]
        public void TipoRetiroService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.TipoRetiroService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new TipoRetiroService()));
        }


        [TestMethod]
        public void BancoService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.BancoService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new BancoService()));
        }


        [TestMethod]
        public void ChequeraService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ChequeraService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new ChequeraService()));
        }

        [TestMethod]
        public void ChequeService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new ChequeService()));
        }

        [TestMethod]
        public void ClienteService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            testList.Add(HelperTesting.ServicesEnum.condicionIvaService);
            Assert.IsTrue(Enable(new ClienteService()));
        }


        [TestMethod]
        public void ColoresService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new ColorService()));
        }

        [TestMethod]
        public void CuentaService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.CuentaService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new CuentaService()));
        }


        [TestMethod]
        public void LineaService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.LineaService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new LineaService()));
        }

        [TestMethod]
        public void ListaPrecioService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new ListaPrecioService()));
        }


        [TestMethod]
        public void LocalService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();

            testList.Add(HelperTesting.ServicesEnum.LocalService);
            fixture = HelperTesting.SetUp(testList);

            Assert.IsTrue(Enable(new LocalService()));
        }
        [TestMethod]
        public void TeporadaService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.TemporadaService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new TemporadaService()));
        }



        [TestMethod]
        public void ValeService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new ValeService(), 2));//fecha uso y Codigo q no tiene
        }


        [TestMethod]
        public void PersonalService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.PersonalService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new PersonalService(),5));
        }


        [TestMethod]
        public void ProductoService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ProductoService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new ProductoService()));
        }



        [TestMethod]
        public void TributoService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Enable(new TributoService()));
        }





       

        [TestMethod]
        public void OrdenPagoService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, false);
            Assert.IsTrue(EnabletWithChildren(new OrdenPagoService(
                new OrdenPagoRepository(), new OrdenPagoDetalleRepository()), 2));//cae y caevto
        }


        [TestMethod]
        public void ReciboService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, true);
            Assert.IsTrue(EnabletWithChildren(new ReciboService(
                new ReciboRepository(), new ReciboDetalleRepository()),2));//cae y caevto
        }


     



        private bool EnabletWithChildren<X, Y, A, B>(FatherService<X, Y, A, B> service, int difExpected = 0)
            where X : DocumentoGeneralData<Y>
            where Y : ChildData
            where A : IGenericFatherRepository<X>
            where B : IGenericChildRepository<Y>
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

                task = service.Enable(id);

                Assert.IsTrue(task);

                object2 = service.GetByID(id);

                object2.Enable = !object2.Enable;

                List<string> dif = HelperTesting.GetDifferences(object1, object2);

                p = (dif == null || dif.Count - difExpected == 0);

            }
            return p;
        }

        private bool Enable<X, Y>(ObjectService<X, Y> service, int difExpected = 0)
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

                task = service.Enable(id);

                Assert.IsTrue(task);

                object2 = service.GetByID(id);

                object2.Enable = !object2.Enable;

                List<string> dif = HelperTesting.GetDifferences(object1, object2);

                p = (dif == null || dif.Count - difExpected == 0);

            }
            return p;
        }
    }
}
