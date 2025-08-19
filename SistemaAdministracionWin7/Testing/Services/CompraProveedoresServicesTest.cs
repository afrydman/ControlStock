using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Services.ComprasProveedorService;
using Services.ProveedorService;

namespace Testing.Services
{

    [TestClass()]
    public class CompraProveedoresServicesTest
    {

        private Fixture fixture = new Fixture();

        [TestMethod]
        public void GetbyProveedorTest()
        {


            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {


                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.ProveedorService);
                testList.Add(HelperTesting.ServicesEnum.Codigo);
                fixture = HelperTesting.SetUp(testList, true);

                var comprasProveedoresServices = new ComprasProveedorService();


                var proveedorService = new ProveedorService();


                var proveedor1 = fixture.Create<ProveedorData>();
                var proveedor2 = fixture.Create<ProveedorData>();

                Assert.IsTrue(proveedorService.Insert(proveedor1));
                Assert.IsTrue(proveedorService.Insert(proveedor2));



                var compra1 = fixture.Create<ComprasProveedoresData>();
                var compra2 = fixture.Create<ComprasProveedoresData>();
                var compra3 = fixture.Create<ComprasProveedoresData>();


                compra1.Proveedor = proveedor1;
                compra2.Proveedor = proveedor1;
                compra3.Proveedor = proveedor2;


                Assert.IsTrue(comprasProveedoresServices.Insert(compra1));
                Assert.IsTrue(comprasProveedoresServices.Insert(compra2));
                Assert.IsTrue(comprasProveedoresServices.Insert(compra3));



                List<ComprasProveedoresData> compraDBp1 = comprasProveedoresServices.GetbyProveedor(proveedor1.ID, false);

                List<ComprasProveedoresData> compraDBp2 = comprasProveedoresServices.GetbyProveedor(proveedor2.ID, false);



                Assert.IsTrue(compraDBp1 != null && compraDBp1.Count==2);
                Assert.IsTrue(compraDBp2 != null && compraDBp2.Count == 1);

            }
        }
    }
}
