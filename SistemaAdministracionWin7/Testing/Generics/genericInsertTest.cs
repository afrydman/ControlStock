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
    public class genericInsertTest
    {

     
        Fixture fixture = new Fixture();

        [TestMethod]
        public void RetiroServices_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.RetiroService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new RetiroService()));
        }

        [TestMethod]
        public void ProveedoresServices_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ProveedorService);
            //testList.Add(HelperTesting.ServicesEnum.condicionIvaService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new ProveedorService()));
        }

        [TestMethod]
        public void IngresoService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.IngresoService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new IngresoService()));
        }



        [TestMethod]
        public void TipoIngresoService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.TipoIngresoService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new TipoIngresoService()));
        }


        [TestMethod]
        public void TipoRetiroService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.TipoRetiroService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new TipoRetiroService()));
        }


        [TestMethod]
        public void BancoService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.BancoService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new BancoService()));
        }


        [TestMethod]
        public void ChequeraService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ChequeraService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new ChequeraService()));
        }

        [TestMethod]
        public void ChequeService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ChequeService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new ChequeService()));
        }

        [TestMethod]
        public void ClienteService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ClienteService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new ClienteService(), difExpected: 3));
        }


        [TestMethod]
        public void ColoresService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ColorService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new ColorService()));
        }

        [TestMethod]
        public void CuentaService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.CuentaService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new CuentaService()));
        }


        [TestMethod]
        public void LineaService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.LineaService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new LineaService()));
        }

        [TestMethod]
        public void ListaPrecioService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ListaPrecioService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new ListaPrecioService()));
        }


        [TestMethod]
        public void LocalService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();

            testList.Add(HelperTesting.ServicesEnum.LocalService);
            fixture=HelperTesting.SetUp(testList);

            Assert.IsTrue(Insert(new LocalService()));
        }
        [TestMethod]
        public void TeporadaService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.TemporadaService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new TemporadaService()));
        }



        [TestMethod]
        public void ValeService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new ValeService(),2));//fecha uso y Codigo q no tiene
        }


        [TestMethod]
        public void PersonalService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.PersonalService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new PersonalService(),5));//debido a herencia: Codigo, descuento, razon social  y cod_raz, condicioniva
        }


        [TestMethod]
        public void ProductoService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ProductoService);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new ProductoService()));
        }


        [TestMethod]
        public void TributoService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(Insert(new TributoService()));
        }



        [TestMethod]
        public void ComprasProveedoresService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture=HelperTesting.SetUp(testList);
            Assert.IsTrue(InsertWithChildren(new ComprasProveedorService(),2));//cae no tiene, caevto no tiene 
        }


        [TestMethod]
        public void NotaCreditoClientesService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture=HelperTesting.SetUp(testList, true,false,tipoNota.CreditoCliente);
            Assert.IsTrue(InsertWithChildren(new NotaService(
                new NotaCreditoClienteRepository(), new NotaCreditoClienteDetalleRepository(), new TributoNotaCreditoClientesRepository())));
        }

        [TestMethod]
        public void NotaDebitoClientesService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture=HelperTesting.SetUp(testList, true,false,tipoNota.DebitoCliente);
            Assert.IsTrue(InsertWithChildren(new NotaService(
                new NotaDebitoClienteRepository(), new NotaDebitoClienteDetalleRepository(), new TributoNotaDebitoClientesRepository())));
        }

        [TestMethod]
        public void NotaCreditoCProveedoresService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture=HelperTesting.SetUp(testList, false,false,tipoNota.CreditoProveedor);
            Assert.IsTrue(InsertWithChildren(new NotaService(
                new NotaCreditoProveedoresRepository(), new NotaCreditoProveedoresDetalleRepository(), new TributoNotaCreditoProveedoresRepository())));
        }

        [TestMethod]
        public void NotaDebitoProveedoresService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture=HelperTesting.SetUp(testList, false,false,tipoNota.DebitoProveedor);
            Assert.IsTrue(InsertWithChildren(new NotaService(
                new NotaDebitoProveedoresRepository(), new NotaDebitoProveedoresDetalleRepository(), new TributoNotaDebitoProveedoresRepository())));
        }


        [TestMethod]
        public void OrdenPagoService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture=HelperTesting.SetUp(testList, false);
            Assert.IsTrue(InsertWithChildren(new OrdenPagoService(
                new OrdenPagoRepository(), new OrdenPagoDetalleRepository()),2));//cae y cae vto
        }


        [TestMethod]
        public void ReciboService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture=HelperTesting.SetUp(testList, true);
            Assert.IsTrue(InsertWithChildren(new ReciboService(
                new ReciboRepository(), new ReciboDetalleRepository()), 2));//cae y cae vto
        }


        [TestMethod]
        public void RemitoService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(InsertWithChildren(new RemitoService(new RemitoRepository(), new RemitoDetalleRepository()), difExpected: 5));// se espera que falle Monto,IVA y  descuento que las heredan pero no corresponde. cae y cae vto
        }


        [TestMethod]
        public void VentasService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            //testList.Add(HelperTesting.ServicesEnum.TributoService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(InsertWithChildren(new VentaService(), difExpected: 1));//las cuotas de las formas de pago. 
        }

        [TestMethod]
        public void PuntoControlService_Insert()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            //testList.Add(HelperTesting.ServicesEnum.TributoService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(InsertWithChildren(new PuntoControlService(), difExpected: 0));//las cuotas de las formas de pago. 
        }
 
      

     
        
        private bool InsertWithChildren<X, Y, A, B>(FatherService<X, Y, A, B> service, int difExpected =0)
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

                object1.Children = fixture.Create<List<Y>>();
               
                bool task = service.Insert(object1);

                Assert.IsTrue(task);
                object2 = service.GetByID(id);

                List<string> dif = HelperTesting.GetDifferences(object1, object2);

                p = (dif == null || dif.Count - difExpected == 0);

            }
            return p;
        }





        private bool Insert<X, Y>(ObjectService<X, Y> service, int difExpected = 0)
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

                bool task = service.Insert(object1);

                Assert.IsTrue(task);

                object2 = service.GetByID(id);

                List<string> dif = HelperTesting.GetDifferences(object1, object2);

                p= (dif == null || dif.Count-difExpected == 0);

            }
            return p;
        }


    }
}
