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
    public class genericGetAllTest
    {
        Fixture fixture = new Fixture();

        [TestMethod]
        public void RetiroServices_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new RetiroService()));
        }


        [TestMethod]
        public void ProveedoresServices_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new ProveedorService()));
        }

        [TestMethod]
        public void IngresoService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new IngresoService()));
        }



        [TestMethod]
        public void TipoIngresoService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.TipoIngresoService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new TipoIngresoService()));
        }


        [TestMethod]
        public void TipoRetiroService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.TipoRetiroService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new TipoRetiroService()));
        }


        [TestMethod]
        public void BancoService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.BancoService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new BancoService()));
        }


        [TestMethod]
        public void ChequeraService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ChequeraService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new ChequeraService()));
        }

        [TestMethod]
        public void ChequeService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new ChequeService()));
        }

        [TestMethod]
        public void ClienteService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new ClienteService()));
        }


        [TestMethod]
        public void ColoresService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new ColorService()));
        }

        [TestMethod]
        public void CuentaService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.CuentaService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new CuentaService()));
        }



        [TestMethod]
        public void LineaService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.LineaService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new LineaService()));
        }

        [TestMethod]
        public void ListaPrecioService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new ListaPrecioService()));
        }


        [TestMethod]
        public void LocalService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();

            testList.Add(HelperTesting.ServicesEnum.LocalService);
            fixture = HelperTesting.SetUp(testList);

            Assert.IsTrue(GetAll(new LocalService()));
        }
        [TestMethod]
        public void TeporadaService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.TemporadaService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new TemporadaService()));
        }

        [TestMethod]
        public void TributoService_Enable()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();

            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new TributoService()));
        }

        [TestMethod]
        public void ValeService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new ValeService(),2));//fecha uso y Codigo q no tiene
        }


        [TestMethod]
        public void PersonalService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.PersonalService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new PersonalService(), 5));
        }


        [TestMethod]
        public void ProductoService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.ProductoService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAll(new ProductoService()));
        }











        [TestMethod]
        public void ComprasProveedoresService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            testList.Add(HelperTesting.ServicesEnum.ComprasProveedoresService);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAllWithChildren(new ComprasProveedorService(), 2));//cae no tiene, caevto no tiene y tributos falla por un tema de implementacion de los tests
        }


        [TestMethod]
        public void NotaCreditoClientesService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, true);
            Assert.IsTrue(GetAllWithChildren(new NotaService(
                new NotaCreditoClienteRepository(), new NotaCreditoClienteDetalleRepository(), new TributoNotaCreditoClientesRepository())));
        }

        [TestMethod]
        public void NotaDebitoClientesService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, true,false,tipoNota.DebitoCliente);
            Assert.IsTrue(GetAllWithChildren(new NotaService(
                new NotaDebitoClienteRepository(), new NotaDebitoClienteDetalleRepository(), new TributoNotaDebitoClientesRepository())));
        }

        [TestMethod]
        public void NotaCreditoCProveedoresService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, false,false,tipoNota.CreditoProveedor);
            Assert.IsTrue(GetAllWithChildren(new NotaService(
                new NotaCreditoProveedoresRepository(), new NotaCreditoProveedoresDetalleRepository(), new TributoNotaCreditoProveedoresRepository())));
        }

        [TestMethod]
        public void NotaDebitoProveedoresService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, false,false,tipoNota.DebitoProveedor);
            Assert.IsTrue(GetAllWithChildren(new NotaService(
                new NotaDebitoProveedoresRepository(), new NotaDebitoProveedoresDetalleRepository(), new TributoNotaDebitoProveedoresRepository())));
        }


        [TestMethod]
        public void OrdenPagoService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.claseDocumento);
            fixture = HelperTesting.SetUp(testList, false);
            
            Assert.IsTrue(GetAllWithChildren(new OrdenPagoService(
                new OrdenPagoRepository(), new OrdenPagoDetalleRepository()),2));//cae y caevto
        }


        [TestMethod]
        public void ReciboService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.claseDocumento);
            fixture = HelperTesting.SetUp(testList, true);
            
            Assert.IsTrue(GetAllWithChildren(new ReciboService(
                new ReciboRepository(), new ReciboDetalleRepository()),2));//cae y caevto
        }


        [TestMethod]
        public void RemitoService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            testList.Add(HelperTesting.ServicesEnum.claseDocumento);
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAllWithChildren(new RemitoService(new RemitoRepository(), new RemitoDetalleRepository()), difExpected: 5));// se espera que falle Monto,IVA y  descuento  cae y caevtoque las heredan pero no corresponde.
        }

        [TestMethod]
        public void PCService_GetAll()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);
            
            fixture = HelperTesting.SetUp(testList);
            Assert.IsTrue(GetAllWithChildren(new PuntoControlService(),
                difExpected: 0));
        }




        private bool GetAllWithChildren<X, Y, A, B>(FatherService<X, Y, A, B> service, int difExpected = 0)
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

                List<X> objectsPre = service.GetAll(false);

                List<X> objectsInitial = fixture.Create<List<X>>();



                objectsInitial[0].Numero = -1;
                objectsInitial[1].Numero = -2;
                objectsInitial[2].Numero = -3;
                objectsInitial[3].Numero = -4;
                objectsInitial[4].Numero = -5;
                objectsInitial[5].Numero = -6;
                objectsInitial[6].Numero = -7;
                objectsInitial[7].Numero = -8;
                objectsInitial[8].Numero = -9;
                objectsInitial[9].Numero = -10;
                


                objectsInitial.ForEach(x => Assert.IsTrue(service.Insert(x)));

                List<X> objectsGets = service.GetAll(false);

                objectsInitial.AddRange(objectsPre);

                objectsInitial.Sort((x, y) => x.Numero.CompareTo(y.Numero));
                objectsGets.Sort((x, y) => x.Numero.CompareTo(y.Numero));

                Assert.IsTrue(objectsGets.Count == objectsInitial.Count);


                List<string> dif;
                for (int i = 0; i < 10; i++)
                {
                    dif = HelperTesting.GetDifferences(objectsInitial[i], objectsGets[i]);
                    p = (dif == null || dif.Count - difExpected <= 0);//debido a que a veces cosas que tienen que fallar coinciden por randomize de datos
                    Assert.IsTrue(p);
                }

            }
            return p;
        }





        private bool GetAll<X, Y>(ObjectService<X, Y> service, int difExpected = 0,bool descriptionSort =true)
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
                List<X> objectsPre = service.GetAll(false);

                List<X> objectsInitial = fixture.Create<List<X>>();


                objectsInitial[0].Description = "a";
                objectsInitial[1].Description = "aa";
                objectsInitial[2].Description = "aaaa";
                objectsInitial[3].Description = "aaaaa";
                objectsInitial[4].Description = "aaaaaa";
                objectsInitial[5].Description = "aaaaaaa";
                objectsInitial[6].Description = "aaaaaaaa";
                objectsInitial[7].Description = "aaaaaaaaa";
                objectsInitial[8].Description = "aaaaaaaaaaa";
                objectsInitial[9].Description = "aaaaaaaaaaaa";
                
                
                objectsInitial.ForEach(x => Assert.IsTrue(service.Insert(x)));

                List<X> objectsGets = service.GetAll(false);
                
                objectsInitial.AddRange(objectsPre);
              
                objectsInitial.Sort((x, y) => System.String.Compare(x.Description, y.Description, System.StringComparison.Ordinal));
                objectsGets.Sort((x, y) => System.String.Compare(x.Description, y.Description, System.StringComparison.Ordinal));
               

                Assert.IsTrue(objectsGets.Count==objectsInitial.Count);


                List<string> dif;
                for (int i = 0; i < 10; i++)
                {
                    dif = HelperTesting.GetDifferences(objectsInitial[i], objectsGets[i]);
                    p = (dif == null || dif.Count - difExpected == 0);
                    Assert.IsTrue(p);
                }

                

            }
            return p;
        }
    }
}
