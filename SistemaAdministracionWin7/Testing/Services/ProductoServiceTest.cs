using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO.BusinessEntities;
using FizzWare.NBuilder.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Constraints;
using Ploeh.AutoFixture;
using Services.ProductoService;
using Services.ProveedorService;

namespace Testing.Services
{
    [TestClass()]
    public class ProductoServiceTest
    {


        private Fixture fixture = new Fixture();

        [TestMethod]
        public void GenerarCodigoInternoTest()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.Codigo);
                testList.Add(HelperTesting.ServicesEnum.ProveedorService);

                fixture = HelperTesting.SetUp(testList, true);


                var proveedorService = new ProveedorService();
                var productoService = new ProductoService();


                var proveedor1 = fixture.Create<ProveedorData>();
                var proveedor2 = fixture.Create<ProveedorData>();

                proveedor1.Codigo = "0001";
                proveedor2.Codigo = "0202";

                var producto1 = new ProductoData();
                producto1.Description = "test1";
                producto1.Enable = true;
                producto1.CodigoProveedor = "aaaa";
                producto1.Proveedor = proveedor1;

                var producto2 = new ProductoData();
                producto2.Description = "test2";
                producto2.Enable = true;
                producto2.CodigoProveedor = "aaaa";
                producto2.Proveedor = proveedor1;

                var producto3 = new ProductoData();
                producto3.Description = "test3";
                producto3.Enable = true;
                producto3.CodigoProveedor = "bbbb";
                producto3.Proveedor = proveedor2;

                var producto4 = new ProductoData();
                producto4.Description = "test4";
                producto4.Enable = true;
                producto4.CodigoProveedor = "aaaa";
                producto4.Proveedor = proveedor1;


                Assert.IsTrue(proveedorService.Insert(proveedor1));
                Assert.IsTrue(proveedorService.Insert(proveedor2));



                string cod1;
                string cod2;
                string cod3;
                string cod4;

                cod1 = productoService.GenerarCodigoInterno(proveedor1.Codigo);

                Assert.IsTrue(cod1.StartsWith(proveedor1.Codigo));

                producto1.CodigoInterno = cod1;

                Assert.IsTrue(productoService.Insert(producto1));

                cod2 = productoService.GenerarCodigoInterno(proveedor1.Codigo);

                Assert.IsTrue(cod2.StartsWith(proveedor1.Codigo));


                Assert.IsTrue(Convert.ToInt32(cod1) < Convert.ToInt32(cod2));



                cod3 = productoService.GenerarCodigoInterno(proveedor2.Codigo);

                Assert.IsTrue(cod3.StartsWith(proveedor2.Codigo));

                producto3.CodigoInterno = cod3;


                Assert.IsTrue(Convert.ToInt32(cod1) < Convert.ToInt32(cod3));
                    // ya que el proveedor 2 tiene un valor mayor

            }
        }

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
                testList.Add(HelperTesting.ServicesEnum.Codigo);
                testList.Add(HelperTesting.ServicesEnum.ProveedorService);

                fixture = HelperTesting.SetUp(testList, true);


                var proveedorService = new ProveedorService();
                var productoService = new ProductoService();


                var proveedor1 = fixture.Create<ProveedorData>();
                var proveedor2 = fixture.Create<ProveedorData>();

                proveedor1.Codigo = "0001";
                proveedor2.Codigo = "0202";

                var producto1 = new ProductoData();
                producto1.Description = "test1";
                producto1.Enable = true;
                producto1.CodigoProveedor = "aaaa";
                producto1.Proveedor = proveedor1;
                producto1.CodigoInterno = "";

                var producto2 = new ProductoData();
                producto2.Description = "test2";
                producto2.Enable = true;
                producto2.CodigoProveedor = "bbbbb";
                producto2.Proveedor = proveedor1;
                producto2.CodigoInterno = "";

                var producto3 = new ProductoData();
                producto3.Description = "test3";
                producto3.Enable = true;
                producto3.CodigoProveedor = "cccc";
                producto3.Proveedor = proveedor2;
                producto3.CodigoInterno = "";

                var producto4 = new ProductoData();
                producto4.Description = "test4";
                producto4.Enable = true;
                producto4.CodigoProveedor = "dddd";
                producto4.Proveedor = proveedor1;
                producto4.CodigoInterno = "";

                Assert.IsTrue(proveedorService.Insert(proveedor1));
                Assert.IsTrue(proveedorService.Insert(proveedor2));

                Assert.IsTrue(productoService.Insert(producto1));
                Assert.IsTrue(productoService.Insert(producto2));
                Assert.IsTrue(productoService.Insert(producto3));
                Assert.IsTrue(productoService.Insert(producto4));



                List<ProductoData> prodDB1 = productoService.GetbyProveedor(proveedor1.ID, false);
                List<ProductoData> prodDB2 = productoService.GetbyProveedor(proveedor2.ID, false);


                Assert.IsTrue(prodDB1!=null && prodDB1.Count==3);
                Assert.IsTrue(prodDB2 != null && prodDB2.Count == 1);

            }
        }
    }
}
