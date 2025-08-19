using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Repository.Repositories.NotasRepository;
using Repository.Repositories.VentaRepository;
using Services.ClienteService;
using Services.NotaService;
using Services.ProveedorService;

namespace Testing.Services
{
    [TestClass()]
    public class NotaServiceTest
    {
        private Fixture fixture = new Fixture();




        [TestMethod]
        public void GetByTercero_Nota_Credito_cliente()
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
                var cliente = fixture.Create<ClienteData>();
                Guid idCliente = cliente.ID;
                Assert.IsTrue(clienteService.Insert(cliente));
                
                var notaService = new NotaService(new NotaCreditoClienteRepository(),new NotaCreditoClienteDetalleRepository(),new TributoNotaCreditoClientesRepository());
                var notadata = fixture.Create<NotaData>();
                notadata.tercero = cliente;

                Assert.IsTrue(notaService.Insert(notadata));



                List<NotaData> dbList = notaService.GetByTercero(idCliente, true, false);


                Assert.IsTrue(dbList!=null && dbList.Count==1);


            }
        }

        [TestMethod]
        public void GetByTercero_Nota_Debito_cliente()
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
                var cliente = fixture.Create<ClienteData>();
                Guid idCliente = cliente.ID;
                Assert.IsTrue(clienteService.Insert(cliente));

                var notaService = new NotaService(new NotaDebitoClienteRepository(), new NotaDebitoClienteDetalleRepository(),new TributoNotaDebitoClientesRepository());
                var notadata = fixture.Create<NotaData>();
                notadata.tercero = cliente;

                Assert.IsTrue(notaService.Insert(notadata));



                List<NotaData> dbList = notaService.GetByTercero(idCliente, true, false);


                Assert.IsTrue(dbList != null && dbList.Count == 1);


            }
        }

        [TestMethod]
        public void GetByTercero_Nota_Credito_Proveedor()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.ProveedorService);

                fixture = HelperTesting.SetUp(testList, false);

                var proveedorService = new ProveedorService();
                var proveedor = fixture.Create<ProveedorData>();
                Guid idProveedor = proveedor.ID;
                Assert.IsTrue(proveedorService.Insert(proveedor));

                var notaService = new NotaService(new NotaCreditoProveedoresRepository(), new NotaCreditoProveedoresDetalleRepository(), new TributoNotaCreditoProveedoresRepository());
                var notadata = fixture.Create<NotaData>();
                notadata.tercero = proveedor;

                Assert.IsTrue(notaService.Insert(notadata));



                List<NotaData> dbList = notaService.GetByTercero(idProveedor, true, false);


                Assert.IsTrue(dbList != null && dbList.Count == 1);


            }
        }

        [TestMethod]
        public void GetByTercero_Nota_Debito_proveedor()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.ProveedorService);

                fixture = HelperTesting.SetUp(testList, false);

                var proveedorService = new ProveedorService();
                var proveedor = fixture.Create<ProveedorData>();
                Guid idProveedor = proveedor.ID;
                Assert.IsTrue(proveedorService.Insert(proveedor));

                var notaService = new NotaService(new NotaDebitoProveedoresRepository(), new NotaDebitoProveedoresDetalleRepository(), new TributoNotaDebitoProveedoresRepository());
                var notadata = fixture.Create<NotaData>();
                notadata.tercero = proveedor;

                Assert.IsTrue(notaService.Insert(notadata));



                List<NotaData> dbList = notaService.GetByTercero(idProveedor, true, false);


                Assert.IsTrue(dbList != null && dbList.Count == 1);


            }
        }
    }
}
