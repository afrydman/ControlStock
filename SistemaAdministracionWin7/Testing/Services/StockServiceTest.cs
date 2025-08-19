using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Services.ProductoService;
using Services.StockService;

namespace Testing.Services
{

    [TestClass()]
    public class StockServiceTest
    {
        private Fixture fixture = new Fixture();


        private StockService stockService = new StockService();




        [TestMethod]
        public void getAlldistintctProducts()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                fixture = HelperTesting.SetUp(testList, true);

                var productoService = new ProductoService();



                decimal stock_1;
                decimal stock_2;
                decimal stock_3;
                StockData unStock_1 = fixture.Build<StockData>().Create();
                StockData unStock_2 = fixture.Build<StockData>().Create();
                StockData unStock_3 = fixture.Build<StockData>().Create();

                unStock_1.Producto = productoService.GetByID(new Guid("C29DF36A-67DC-4BA2-AD3E-5A2C6C7C6112"));
                unStock_2.Producto = productoService.GetByID(new Guid("3EC55B8E-3259-4589-A577-D9B691F82A65"));
                unStock_3.Producto = productoService.GetByID(new Guid("7ED20CE7-9DF3-4D3D-A640-CC0BC3C342D1"));

                stock_1 = unStock_1.Stock;
                stock_2 = unStock_2.Stock;
                stock_3 = unStock_3.Stock;


                List<StockData> aca = new List<StockData>() { unStock_1, unStock_2, unStock_3 };


                List<StockData> pre1 = stockService.getAlldistintctProducts(unStock_1.Producto.ID, false);
                List<StockData> pre2 = stockService.getAlldistintctProducts(unStock_2.Producto.ID, false);
                List<StockData> pre3 = stockService.getAlldistintctProducts(unStock_3.Producto.ID, false);
                
                Assert.IsTrue(stockService.Insert(unStock_1, stock_1, unStock_1.Local.ID));
                Assert.IsTrue(stockService.Insert(unStock_2, stock_2, unStock_2.Local.ID));
                Assert.IsTrue(stockService.Insert(unStock_3, stock_3, unStock_3.Local.ID));


                
                List<StockData> All = stockService.getAlldistintctProducts( unStock_1.Producto.ID,
                    false);

                Assert.IsTrue(All.Count == 1 + pre1.Count);

                All.AddRange(stockService.getAlldistintctProducts( unStock_2.Producto.ID,
                    false));

                Assert.IsTrue(All.Count == 2+pre1.Count+pre2.Count);


                All.AddRange(stockService.getAlldistintctProducts( unStock_3.Producto.ID,
                    false));

                aca.AddRange(pre1);
                aca.AddRange(pre2);
                aca.AddRange(pre3);

                All.Sort((x, y) => Convert.ToInt64(x.Codigo).CompareTo(Convert.ToInt64(y.Codigo)));
                aca.Sort((x, y) => Convert.ToInt64(x.Codigo).CompareTo(Convert.ToInt64(y.Codigo)));


                Assert.IsTrue(All.Count == 3 + pre1.Count + pre2.Count+pre3.Count);

                int difExpected = 0;
                List<string> dif;
                bool p;
                for (int i = 0; i < 3; i++)
                {
                    dif = HelperTesting.GetDifferences(aca[i], All[i]);
                    p = (dif == null || dif.Count - difExpected == 0);
                    Assert.IsTrue(p);
                }



            }
        }

        [TestMethod]
        public void getAllbyLocalAndProducto()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                fixture = HelperTesting.SetUp(testList, true);

                var productoService = new ProductoService();



                decimal stock_1;
                decimal stock_2;
                decimal stock_3;
                StockData unStock_1 = fixture.Build<StockData>().Create();
                StockData unStock_2 = fixture.Build<StockData>().Create();
                StockData unStock_3 = fixture.Build<StockData>().Create();

                unStock_1.Producto = productoService.GetByID(new Guid("C29DF36A-67DC-4BA2-AD3E-5A2C6C7C6112"));
                unStock_2.Producto = productoService.GetByID(new Guid("3EC55B8E-3259-4589-A577-D9B691F82A65"));
                unStock_3.Producto = productoService.GetByID(new Guid("7ED20CE7-9DF3-4D3D-A640-CC0BC3C342D1"));

                stock_1 = unStock_1.Stock;
                stock_2 = unStock_2.Stock;
                stock_3 = unStock_3.Stock;


                List<StockData> aca = new List<StockData>() { unStock_1, unStock_2, unStock_3 };

                Assert.IsTrue(stockService.Insert(unStock_1, stock_1, unStock_1.Local.ID));
                Assert.IsTrue(stockService.Insert(unStock_2, stock_2, unStock_2.Local.ID));
                Assert.IsTrue(stockService.Insert(unStock_3, stock_3, unStock_3.Local.ID));



                List<StockData> All = stockService.getAllbyLocalAndProducto(unStock_1.Local.ID, unStock_1.Producto.ID,
                    false);

                Assert.IsTrue(All.Count==1);

                All.AddRange(stockService.getAllbyLocalAndProducto(unStock_2.Local.ID, unStock_2.Producto.ID,
                    false));

                Assert.IsTrue(All.Count == 2);


                All.AddRange(stockService.getAllbyLocalAndProducto(unStock_3.Local.ID, unStock_3.Producto.ID,
                    false));

                All.Sort((x, y) => Convert.ToInt64(x.Codigo).CompareTo(Convert.ToInt64(y.Codigo)));
                aca.Sort((x, y) => Convert.ToInt64(x.Codigo).CompareTo(Convert.ToInt64(y.Codigo)));


                Assert.IsTrue(All.Count == 3);

                int difExpected = 0;
                List<string> dif;
                bool p;
                for (int i = 0; i < 3; i++)
                {
                    dif = HelperTesting.GetDifferences(aca[i], All[i]);
                    p = (dif == null || dif.Count - difExpected == 0);
                    Assert.IsTrue(p);
                }



            }
        }

        [TestMethod]
        public void GetAll()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                fixture = HelperTesting.SetUp(testList, true);

                var productoService = new ProductoService();



                decimal stock_1;
                decimal stock_2;
                decimal stock_3;
                StockData unStock_1 = fixture.Build<StockData>().Create();
                StockData unStock_2 = fixture.Build<StockData>().Create();
                StockData unStock_3 = fixture.Build<StockData>().Create();

                unStock_1.Producto = productoService.GetByID(new Guid("C29DF36A-67DC-4BA2-AD3E-5A2C6C7C6112"));
                unStock_2.Producto = productoService.GetByID(new Guid("3EC55B8E-3259-4589-A577-D9B691F82A65"));
                unStock_3.Producto = productoService.GetByID(new Guid("7ED20CE7-9DF3-4D3D-A640-CC0BC3C342D1"));

                stock_1 = unStock_1.Stock;
                stock_2 = unStock_2.Stock;
                stock_3 = unStock_3.Stock;


                List<StockData> aca = new List<StockData>(){unStock_1,unStock_2,unStock_3};

                Assert.IsTrue(stockService.Insert(unStock_1, stock_1, unStock_1.Local.ID));
                Assert.IsTrue(stockService.Insert(unStock_2, stock_2, unStock_2.Local.ID));
                Assert.IsTrue(stockService.Insert(unStock_3, stock_3, unStock_3.Local.ID));



                List<StockData> All = stockService.GetAll(false, unStock_1.Local.ID);




                All.Sort((x, y) => Convert.ToInt64(x.Codigo).CompareTo(Convert.ToInt64(y.Codigo)));
                aca.Sort((x, y) => Convert.ToInt64(x.Codigo).CompareTo(Convert.ToInt64(y.Codigo)));


                Assert.IsTrue(All.Count==3);

                int difExpected = 0;
                List<string> dif;
                bool p;
                for (int i = 0; i < 3; i++)
                {
                    dif = HelperTesting.GetDifferences(aca[i], All[i]);
                    p = (dif == null || dif.Count - difExpected == 0);
                    Assert.IsTrue(p);
                }



            }
        }

        [
            TestMethod]
        public void ObtenerProducto()
        {
             var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                fixture = HelperTesting.SetUp(testList, true);


                

                StockData unStock = fixture.Build<StockData>().Create();

                string codigoStock = unStock.Codigo;
                Guid idLocal = unStock.Local.ID;
                decimal stockTotal = unStock.Stock;

                Assert.IsTrue(stockService.Insert(unStock, stockTotal, idLocal));


                StockData buscandoStock = stockService.obtenerProducto(codigoStock, idLocal);

                int difExpected = 0;//ID 
                var dif = HelperTesting.GetDifferences(unStock, buscandoStock);
                bool pp = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(pp);
            }
        }
    




    [TestMethod]
         public void InsertAndGet()
         {
         var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

        using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, true);


            fixture.Customizations.Add(
                new RandomNumericSequenceGenerator(0, 99, 200));

            StockData unStock = fixture.Build<StockData>().Without(p => p.Metros).Create();

            string codigoStock = unStock.Codigo;
            Guid idLocal = unStock.Local.ID;
            decimal stockTotal = 100;

            Assert.IsTrue(stockService.Insert(unStock, stockTotal, idLocal));



            decimal stockDb = stockService.GetStockTotal(codigoStock, idLocal);


            Assert.IsTrue(stockDb == stockTotal);
        }
         }

         [TestMethod]
         public void InsertAndUpdateandGet()
         {

              var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

             using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
             {
                 List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                 fixture = HelperTesting.SetUp(testList, true);


                 fixture.Customizations.Add(
                     new RandomNumericSequenceGenerator(0, 99, 200));

                 StockData unStock = fixture.Build<StockData>().Without(p => p.Metros).Create();

                 string codigoStock = unStock.Codigo;
                 Guid idLocal = unStock.Local.ID;
                 decimal StockInicial = 100;
                 decimal addstock = 10;
                 Assert.IsTrue(stockService.Insert(unStock, StockInicial, idLocal));



                 decimal stockDb = stockService.GetStockTotal(codigoStock, idLocal);


                 Assert.IsTrue(stockDb == StockInicial);


                 Assert.IsTrue(stockService.UpdateStock(codigoStock, addstock, idLocal, true));

                 stockDb = stockService.GetStockTotal(codigoStock, idLocal);

                 Assert.IsTrue(stockDb == (StockInicial + addstock));

                 Assert.IsTrue(stockService.UpdateStock(codigoStock, addstock, idLocal, false));

                 stockDb = stockService.GetStockTotal(codigoStock, idLocal);

                 Assert.IsTrue(stockDb == addstock);

                 decimal ventastock = -1;
                 Assert.IsTrue(stockService.UpdateStock(codigoStock, ventastock, idLocal, true));
                 stockDb = stockService.GetStockTotal(codigoStock, idLocal);

                 Assert.IsTrue((stockDb) == (addstock + ventastock));
             }
         }


         //setDinamicallyStock(string Codigo, Guid Local, bool onlyEnable = true)
    }
}
