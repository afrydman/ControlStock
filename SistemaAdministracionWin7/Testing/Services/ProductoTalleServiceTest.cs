using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Internal;
using Ploeh.AutoFixture;
using Services;
using Services.ProductoService;
using Services.StockService;

namespace Testing.Services
{

    [TestClass()]
    public class ProductoTalleServiceTest
    {
        private Fixture fixture = new Fixture();




        [TestMethod]
        public void InsertAndGet_Sin_Metros_Test()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                
                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.ProductoService);
                fixture = HelperTesting.SetUp(testList, true);

                
                ProductoData producto =  fixture.Create<ProductoData>();


                Assert.IsTrue(new ProductoService().Insert(producto));


                
                ProductoTalleService productoTalleService = new ProductoTalleService();


                List<ProductoTalleData> productosTalles = productoTalleService.GetByProducto(producto.ID);


                Assert.IsTrue(productosTalles != null && productosTalles.Count == HelperService.TallesPorProductoDefault+1);

                List<ProductoTalleData> productosTallesAux = new List<ProductoTalleData>();

                for (int i = 0; i < HelperService.TallesPorProductoDefault; i++)
                {
                    productosTallesAux.Add(productoTalleService.GetByProductoTalle(producto.ID,i));
                }

                
                productosTalles.Sort((x, y) => x.Talle.CompareTo(y.Talle));
                productosTallesAux.Sort((x, y) => x.Talle.CompareTo(y.Talle));






                int difExpected = 0;
                List<string> dif;
                bool p;
                for (int i = 0; i < HelperService.TallesPorProductoDefault; i++)
                {
                    dif = HelperTesting.GetDifferences(productosTalles[i], productosTallesAux[i]);
                    p = (dif == null || dif.Count - difExpected == 0);
                    Assert.IsTrue(p);
                }

                ProductoTalleData aux;
                for (int i = 0; i < HelperService.TallesPorProductoDefault; i++)
                {
                    aux =
                        productosTalles.Find(
                            data => data.ID == productoTalleService.GetIDByProductoTalle(producto.ID, i));
                    Assert.IsTrue(aux!=null && aux.Talle==i);

                    aux = null;
                }




            }

        }



        [TestMethod]
        public void InsertAndGet_CON_Metros_Test()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                //testList.Add(HelperTesting.ServicesEnum.ProductoService);
                fixture = HelperTesting.SetUp(testList, true,true);

                StockMetrosService stockMetrosService = new StockMetrosService();


                HelperService.haymts = true;

                ProductoData producto = fixture.Create<ProductoData>();
                ColorData color =   fixture.Create<ColorData>();
                string mts1 = "121,2";
                string mts2 = "12,2";
                string mts3 = "0,221";
                string mts4 = "13.68";


                string cod1;
                string cod2;
                string cod3;
                string cod4;

                Assert.IsTrue(new ProductoService().Insert(producto));


                ProductoTalleService productoTalleService = new ProductoTalleService();
                List<ProductoTalleData> productosTallesPre = productoTalleService.GetByProducto(producto.ID);

                cod1 = stockMetrosService.obtenerCodigo(producto.CodigoInterno, color.Codigo, mts1);
                cod2 = stockMetrosService.obtenerCodigo(producto.CodigoInterno, color.Codigo, mts2);
                cod3 = stockMetrosService.obtenerCodigo(producto.CodigoInterno, color.Codigo, mts3);
                cod4 = stockMetrosService.obtenerCodigo(producto.CodigoInterno, color.Codigo, mts4);
                




                List<ProductoTalleData> productosTalles = productoTalleService.GetByProducto(producto.ID);


                Assert.IsTrue(productosTalles != null && productosTalles.Count - productosTallesPre.Count== 4 );

                List<ProductoTalleData> productosTallesAux = new List<ProductoTalleData>();

                for (int i = 0; i < 5; i++)
                {
                    productosTallesAux.Add(productoTalleService.GetByProductoTalle(producto.ID, i));
                }


                productosTalles.Sort((x, y) => x.Talle.CompareTo(y.Talle));
                productosTallesAux.Sort((x, y) => x.Talle.CompareTo(y.Talle));






                int difExpected = 0;
                List<string> dif;
                bool p;
                for (int i = 0; i < 5; i++)
                {
                    dif = HelperTesting.GetDifferences(productosTalles[i], productosTallesAux[i]);
                    p = (dif == null || dif.Count - difExpected == 0);
                    Assert.IsTrue(p);
                }

                ProductoTalleData aux;
                for (int i = 0; i < 5; i++)
                {
                    aux =
                        productosTalles.Find(
                            data => data.ID == productoTalleService.GetIDByProductoTalle(producto.ID, i));
                    Assert.IsTrue(aux != null && aux.Talle == i);

                    aux = null;
                }




            }

        }

    


    }
}
