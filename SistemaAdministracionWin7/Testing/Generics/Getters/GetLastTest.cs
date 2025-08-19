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
using Services.VentaService;

namespace Testing.Generics
{
    [TestClass()]
    public class GetLastTest
    {
        Fixture fixture = new Fixture();




        [TestMethod]
        public void ComprasProveedoresService_GetLast()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetLastFather(new ComprasProveedorService(), 4));//la diff sera los children, ya que get last no tiene pq traerlos. + cae y cae vto q no hay
        }


        [TestMethod]
        public void NotaCreditoClientesService_GetLast()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList,true,false,tipoNota.CreditoCliente);
            Assert.IsTrue(GetLastFather(new NotaService(new NotaCreditoClienteRepository(), new NotaCreditoClienteDetalleRepository(),new TributoNotaCreditoClientesRepository()), 0));
        }

        [TestMethod]
        public void NotaDebitoClientesService_GetLast()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList,true,false,tipoNota.DebitoCliente);
            Assert.IsTrue(GetLastFather(new NotaService(
                new NotaDebitoClienteRepository(), new NotaDebitoClienteDetalleRepository(), new TributoNotaDebitoClientesRepository()), 0));
        }


        [TestMethod]
        public void NotaCreditoCProveedoresService_GetLast()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList,false,false,tipoNota.CreditoProveedor);
            Assert.IsTrue(GetLastFather(new NotaService(
                new NotaCreditoProveedoresRepository(), new NotaCreditoProveedoresDetalleRepository(), new TributoNotaCreditoProveedoresRepository()), 0));
        }


        [TestMethod]
        public void NotaDebitoProveedoresService_GetLast()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList, false,false,tipoNota.DebitoProveedor);
            Assert.IsTrue(GetLastFather(new NotaService(
                new NotaDebitoProveedoresRepository(), new NotaDebitoProveedoresDetalleRepository(), new TributoNotaDebitoProveedoresRepository())));
        }


        [TestMethod]
        public void OrdenPagoService_GetLast()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetLastFather(new OrdenPagoService(), 3));//la diff sera los children, ya que get last no tiene pq traerlos.+ cae y cae vto q no hay
        }
        [TestMethod]
        public void ReciboService_GetLast()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetLastFather(new ReciboService(), 3));//la diff sera los children, ya que get last no tiene pq traerlos. + cae y cae vto q no hay
        }


        [TestMethod]
        public void RemitoService_GetLast()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetLastFather(new RemitoService(), 6));//la diff sera los children, ya que get last no tiene pq traerlos. y 3 prop de clase remito + cae y cae vto q no hay
        }

        [TestMethod]
        public void VentaService_GetLast()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetLastFather(new VentaService(), 3));//la diff sera los children, ya que get last no tiene pq traerlos. y los pagos q son otros childrens y tributos
        }

        [TestMethod]
        public void PuntoControlService_GetLast()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetLastFather(new PuntoControlService(), 0));//la diff sera los children, ya que get last no tiene pq traerlos. y los pagos q son otros childrens y tributos
        }


        [TestMethod]
        public void Retiro_GetLast()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetLast(new RetiroService(), 0));
        }
        [TestMethod]
        public void Ingreso_GetLast()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetLast(new IngresoService(), 0));
        }
        private bool GetLast<X, Y>(ObjectService<X, Y> service, int difExpected = 0)
            where X : MovimientoEnCajaData
            where Y : IGenericRepository<X>
            
        {

            bool p = false;

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {


                LocalData Local = fixture.Create<LocalData>();
                int prefix = 1;

                X object1 = service.GetLast(Local.ID, prefix);


                Assert.IsTrue(object1.Numero > 0);//el primero siempre tiene que ser al menos 1
                Assert.IsTrue(object1.Prefix == prefix);




                Guid id = Guid.NewGuid();

                X object2 = fixture.Create<X>();
                object2.ID = id;
                object2.Numero = Convert.ToInt32(service.GetLast(Local.ID, prefix).Numero)+1;//hermoso
                object2.Prefix = prefix;
                object2.Local = Local;

                Assert.IsTrue(service.Insert(object2));


                X object3 = service.GetLast(Local.ID, prefix);

                Assert.IsTrue(object3.Numero > object1.Numero);

                List<string> dif = HelperTesting.GetDifferences(object2, object3);

                p = (dif == null || dif.Count - difExpected == 0);

            }
            return p;
        }


        private bool GetLastFather<X, Y, A, B>(FatherService<X, Y, A, B> service, int difExpected = 0)
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

               
                LocalData local = fixture.Create<LocalData>();
                int prefix = 1;

                X object1 = service.GetLast(local.ID,prefix);


                Assert.IsTrue(object1.Numero>0);//el primero siempre tiene que ser al menos 1
                Assert.IsTrue(object1.Prefix == prefix);




                Guid id = Guid.NewGuid();

                X object2 = fixture.Create<X>();
                object2.ID = id;
                object2.Numero = Convert.ToInt32(service.GetNextNumberAvailable(local.ID, prefix, false));
                object2.Prefix = prefix;
                object2.Local = local;

                Assert.IsTrue(service.Insert(object2));


                X object3 = service.GetLast(local.ID, prefix);


                Assert.IsTrue(object3.Numero>object1.Numero);


                List<string> dif = HelperTesting.GetDifferences(object2, object3);

                p = (dif == null || dif.Count - difExpected == 0);

            }
            return p;
        }

    }
}
