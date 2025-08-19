using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Repository;
using Repository.Repositories.NotasRepository;
using Repository.Repositories.OrdenPagoRepository;
using Repository.Repositories.ReciboRepository;
using Repository.Repositories.RemitoRepository;
using Repository.Repositories.VentaRepository;
using Services.AbstractService;
using Services.AdministracionService;
using Services.BancoService;
using Services.ChequeraService;
using Services.ChequeService;
using Services.ClienteService;
using Services.ColorService;
using Services.ComprasProveedorService;
using Services.CuentaService;
using Services.IngresoService;
using Services.LineaService;
using Services.ListaPrecioService;
using Services.LocalService;
using Services.NotaService;

using Services.OrdenPagoService;
using Services.PersonalService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.PuntoControlService;
using Services.ReciboService;
using Services.RemitoService;
using Services.RetiroService;
using Services.TemporadaService;
using Services.TributoService;
using Services.ValeService;
using Services.VentaService;

namespace Testing
{
    [TestClass()]
    public class genericDisableTest
    {


        Fixture fixture = new Fixture();

        [TestMethod]
        public void RetiroServices_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new RetiroService()));
        }

        [TestMethod]
        public void ProveedoresServices_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new ProveedorService()));
        }

        [TestMethod]
        public void IngresoService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new IngresoService()));
        }



        [TestMethod]
        public void TipoIngresoService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.TipoIngresoService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new TipoIngresoService()));
        }


        [TestMethod]
        public void TipoRetiroService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.TipoRetiroService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new TipoRetiroService()));
        }


        [TestMethod]
        public void BancoService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.BancoService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new BancoService()));
        }


        [TestMethod]
        public void ChequeraService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ChequeraService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new ChequeraService()));
        }

        [TestMethod]
        public void ChequeService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new ChequeService()));
        }

        [TestMethod]
        public void ClienteService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new ClienteService()));
        }


        [TestMethod]
        public void ColoresService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new ColorService()));
        }

        [TestMethod]
        public void CuentaService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.CuentaService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new CuentaService()));
        }


        [TestMethod]
        public void LineaService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.LineaService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new LineaService()));
        }

        [TestMethod]
        public void ListaPrecioService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new ListaPrecioService()));
        }


        [TestMethod]
        public void LocalService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();

            testList.Add(HelperTesting.ServicesEnum.LocalService);
            fixture = HelperTesting.SetUp(testList);

            Assert.IsTrue(Disable(new LocalService()));
        }
        [TestMethod]
        public void TeporadaService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.TemporadaService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new TemporadaService()));
        }



        [TestMethod]
        public void ValeService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new ValeService(), 2));//fecha uso y Codigo q no tiene
        }


        [TestMethod]
        public void PersonalService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.PersonalService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new PersonalService(),5));
        }


        [TestMethod]
        public void ProductoService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ProductoService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new ProductoService()));
        }

        [TestMethod]
        public void TributoService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();

            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Disable(new TributoService()));
        }





        [TestMethod]
        public void ComprasProveedoresService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(DisabletWithChildren(new ComprasProveedorService(), 2));//cae no tiene, caevto no tiene 
        }


        [TestMethod]
        public void NotaCreditoClientesService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            
            fixture = HelperTesting.SetUp(testList, true,false,tipoNota.CreditoCliente);
            Assert.IsTrue(DisabletWithChildren(new NotaService(
                new NotaCreditoClienteRepository(), new NotaCreditoClienteDetalleRepository(), new TributoNotaCreditoClientesRepository())));
        }

        [TestMethod]
        public void NotaDebitoClientesService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, true, false, tipoNota.DebitoCliente);
            Assert.IsTrue(DisabletWithChildren(new NotaService(
                new NotaDebitoClienteRepository(), new NotaDebitoClienteDetalleRepository(), new TributoNotaDebitoClientesRepository())));
        }

        [TestMethod]
        public void NotaCreditoCProveedoresService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            
            fixture = HelperTesting.SetUp(testList,false, false, tipoNota.CreditoProveedor);
            Assert.IsTrue(DisabletWithChildren(new NotaService(
                new NotaCreditoProveedoresRepository(), new NotaCreditoProveedoresDetalleRepository(), new TributoNotaCreditoProveedoresRepository())));
        }

        [TestMethod]
        public void NotaDebitoProveedoresService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, false, false, tipoNota.DebitoProveedor);
            Assert.IsTrue(DisabletWithChildren(new NotaService(
                new NotaDebitoProveedoresRepository(), new NotaDebitoProveedoresDetalleRepository(), new TributoNotaDebitoProveedoresRepository())));
        }


        [TestMethod]
        public void OrdenPagoService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList, false);
            Assert.IsTrue(DisabletWithChildren(new OrdenPagoService(
                new OrdenPagoRepository(), new OrdenPagoDetalleRepository()),2));//Cae y caeVto
        }


        [TestMethod]
        public void ReciboService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList, true);
            Assert.IsTrue(DisabletWithChildren(new ReciboService(
                new ReciboRepository(), new ReciboDetalleRepository()),2));//Cae y caeVto
        }


        [TestMethod]
        public void RemitoService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(DisabletWithChildren(new RemitoService(new RemitoRepository(), new RemitoDetalleRepository()), difExpected: 5));// se espera que falle Monto,IVA y  descuento que las heredan pero no corresponde. + cae y caevto
        }

        [TestMethod]
        public void VentaService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(DisabletWithChildren(new VentaService(), difExpected: 1));// se espera que falle pagos ( cuotas ) 
        }


        [TestMethod]
        public void PCService_Disable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(DisabletWithChildren(new PuntoControlService(), difExpected: 0));// se espera que falle pagos ( cuotas ) 
        }

        
        private bool DisabletWithChildren<X, Y, A, B>(FatherService<X, Y, A, B> service, int difExpected = 0)
            //a guy in a wheelchair with his sons and daughters ;)
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
                object1.Enable = true;
                bool task = service.Insert(object1);

                Assert.IsTrue(task);

                task = service.Disable(id,true);

                Assert.IsTrue(task);

                object2 = service.GetByID(id);

                object2.Enable = !object2.Enable;

                List<string> dif = HelperTesting.GetDifferences(object1, object2);

                p = (dif == null || dif.Count - difExpected == 0);

            }
            return p;
        }

        private bool Disable<X, Y>(ObjectService<X, Y> service, int difExpected = 0)
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
                object1.Enable = true;
                bool task = service.Insert(object1);

                Assert.IsTrue(task);

                task = service.Disable(id);

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
