using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Services;
using Services.ColorService;
using Services.StockService;

namespace Testing.Services
{

    [TestClass()]
    public class StockMetrosServiceTest
    {
        private Fixture fixture = new Fixture();



        [TestMethod]
        public void ObtenerTodosByProductoColor_Test()
        {




            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, true);



            StockMetrosService stockMetrosService = new StockMetrosService();

            ColorData color = fixture.Create<ColorData>(); ;
            ProductoData producto = fixture.Create<ProductoData>();


            string metros1 = "22.2";
            string metros2 = "11,1";


            string codigo1;
            string codigo2;
            string codigo3;


            decimal m1;
            decimal m2;
            decimal m3;

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                codigo1 = stockMetrosService.obtenerCodigo(producto.CodigoInterno, color.Codigo, metros1, true);

                codigo2 = stockMetrosService.obtenerCodigo(producto.CodigoInterno, color.Codigo, metros2, true);

                Assert.IsTrue(codigo2 ==
                              stockMetrosService.obtenerCodigo(producto.CodigoInterno, color.Codigo, metros2, true));

                ColorData colorAux = new ColorData();
                colorAux.Codigo = "666";
                color.Description = "auxilar";


                Assert.IsTrue(new ColorService().Insert(colorAux));


                codigo3 = stockMetrosService.obtenerCodigo(producto.CodigoInterno, colorAux.Codigo, metros1, true);


                m1 = stockMetrosService.obtenerMetrosPorTalle(producto.CodigoInterno, color.Codigo,
                    Convert.ToInt32(codigo1.Substring(10, 2)));


                Assert.IsTrue(m1 == HelperService.ConvertToDecimalSeguro(metros1));

                m2 = stockMetrosService.obtenerMetrosPorTalle(producto.CodigoInterno, color.Codigo,
                    Convert.ToInt32(codigo2.Substring(10, 2)));
                Assert.IsTrue(m2 == HelperService.ConvertToDecimalSeguro(metros2));

                m3 = stockMetrosService.obtenerMetrosPorTalle(producto.CodigoInterno, colorAux.Codigo,
                    Convert.ToInt32(codigo3.Substring(10, 2)));



                Assert.IsTrue(m3 == HelperService.ConvertToDecimalSeguro(metros1));

            }

        }



        [TestMethod]
        public void ObtenerMetros_Test()
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, true);

          
            
            StockMetrosService stockMetrosService = new StockMetrosService();

            ColorData color = fixture.Create<ColorData>(); ;
            ProductoData producto = fixture.Create<ProductoData>();


            string metros1 = "22.2";
            string metros2 = "11,1";
            

            string codigo1;
            string codigo2;
            string codigo3;


            decimal m1;
            decimal m2;
            decimal m3;

             var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                codigo1 = stockMetrosService.obtenerCodigo(producto.CodigoInterno, color.Codigo, metros1, true);

                codigo2 = stockMetrosService.obtenerCodigo(producto.CodigoInterno, color.Codigo, metros2, true);

                Assert.IsTrue(codigo2 ==
                              stockMetrosService.obtenerCodigo(producto.CodigoInterno, color.Codigo, metros2, true));

                ColorData colorAux = new ColorData();
                colorAux.Codigo = "666";
                color.Description = "auxilar";


                Assert.IsTrue(new ColorService().Insert(colorAux));


                codigo3 = stockMetrosService.obtenerCodigo(producto.CodigoInterno, colorAux.Codigo, metros1, true);


                m1 = stockMetrosService.obtenerMetrosPorTalle(producto.CodigoInterno, color.Codigo,
                    Convert.ToInt32(codigo1.Substring(10, 2)));


                Assert.IsTrue(m1 == HelperService.ConvertToDecimalSeguro(metros1));

                m2 = stockMetrosService.obtenerMetrosPorTalle(producto.CodigoInterno, color.Codigo,
                    Convert.ToInt32(codigo2.Substring(10, 2)));
                Assert.IsTrue(m2 ==HelperService.ConvertToDecimalSeguro(metros2));

                m3 = stockMetrosService.obtenerMetrosPorTalle(producto.CodigoInterno, colorAux.Codigo,
                    Convert.ToInt32(codigo3.Substring(10, 2)));



                Assert.IsTrue(m3 == HelperService.ConvertToDecimalSeguro(metros1));

            }

        }



        /// <summary>
        /// Testea los metodos que convierten de int a 61 ( se usa para los ultimos 2 digitos en el Codigo, en el caso que el sistema funcione con mts)
        /// </summary>
        [TestMethod]
        public void transformoValores_Test()
        {
            var alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var q = alphabet.Select(x => x.ToString());
            int size = 2;
            for (int i = 0; i < size - 1; i++)
                q = q.SelectMany(x => alphabet, (x, y) => x + y);

            StockMetrosService stockMetrosService = new StockMetrosService();
            string result_from_61_to_dec;
            string result_from_dec_to_61;

            foreach (var codigo61 in q)
            {
                result_from_61_to_dec = stockMetrosService.from61ToDec(codigo61);
                result_from_dec_to_61 = stockMetrosService.decTo61(Convert.ToInt32(result_from_61_to_dec),true);

                if (codigo61 != result_from_dec_to_61)
                {
                    Assert.Fail("no son todos iguales !");
                }
                Assert.IsTrue(codigo61 == result_from_dec_to_61);
            }

            

        }

        [TestMethod]
        public void obtenerCodigo_Test()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                fixture = HelperTesting.SetUp(testList, true);

                
                StockMetrosService stockMetrosService = new StockMetrosService();
                
                ColorData color = fixture.Create<ColorData>(); ;
                ProductoData producto = fixture.Create<ProductoData>(); 


                string metros1 = "11";
                string metros2 = "11.1";
                string metros3 = "12,3";


                string codigo1;
                string codigo2;
                string codigo3;
                
                codigo1=stockMetrosService.obtenerCodigo(producto.CodigoInterno, color.Codigo, metros1, true);

                //verifico que traiga el que acabo de generar
                Assert.IsTrue(codigo1 == stockMetrosService.obtenerCodigo(producto.CodigoInterno, color.Codigo, metros1, true));

                codigo2 = stockMetrosService.obtenerCodigo(producto.CodigoInterno, color.Codigo, metros2, true);
                Assert.IsTrue(codigo2 == stockMetrosService.obtenerCodigo(producto.CodigoInterno, color.Codigo, metros2, true));

                ColorData colorAux = new ColorData();
                colorAux.Codigo = "666";
                color.Description = "auxilar";
                
                
                Assert.IsTrue(new ColorService().Insert(colorAux));


                codigo3 = stockMetrosService.obtenerCodigo(producto.CodigoInterno, colorAux.Codigo, metros1, true);
                
                //verifico que el contador vaya a 1 para un color nuevo.
                Assert.IsTrue(codigo3.Substring(10,2)=="01");



                

            }
        }

        //        }
    //    }

    //    [TestMethod]
    //    public void getAllbyLocalAndProducto()
    //    {
    //        var opts = new TransactionOptions
    //        {
    //            IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
    //        };

    //        using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
    //        {

    //            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
    //            fixture = HelperTesting.SetUp(testList, true);

    //            var productoService = new ProductoService();



    //            decimal stock_1;
    //            decimal stock_2;
    //            decimal stock_3;
    //            StockData unStock_1 = fixture.Build<StockData>().Create();
    //            StockData unStock_2 = fixture.Build<StockData>().Create();
    //            StockData unStock_3 = fixture.Build<StockData>().Create();

    //            unStock_1.producto = productoService.GetByID(new Guid("C29DF36A-67DC-4BA2-AD3E-5A2C6C7C6112"));
    //            unStock_2.producto = productoService.GetByID(new Guid("3EC55B8E-3259-4589-A577-D9B691F82A65"));
    //            unStock_3.producto = productoService.GetByID(new Guid("7ED20CE7-9DF3-4D3D-A640-CC0BC3C342D1"));

    //            stock_1 = unStock_1.stock;
    //            stock_2 = unStock_2.stock;
    //            stock_3 = unStock_3.stock;


    //            List<StockData> aca = new List<StockData>() { unStock_1, unStock_2, unStock_3 };

    //            Assert.IsTrue(stockService.Insert(unStock_1, stock_1, unStock_1.Local.ID));
    //            Assert.IsTrue(stockService.Insert(unStock_2, stock_2, unStock_2.Local.ID));
    //            Assert.IsTrue(stockService.Insert(unStock_3, stock_3, unStock_3.Local.ID));



    //            List<StockData> All = stockService.getAllbyLocalAndProducto(unStock_1.Local.ID, unStock_1.producto.ID,
    //                false);

    //            Assert.IsTrue(All.Count==1);

    //            All.AddRange(stockService.getAllbyLocalAndProducto(unStock_2.Local.ID, unStock_2.producto.ID,
    //                false));

    //            Assert.IsTrue(All.Count == 2);


    //            All.AddRange(stockService.getAllbyLocalAndProducto(unStock_3.Local.ID, unStock_3.producto.ID,
    //                false));

    //            All.Sort((x, y) => Convert.ToInt64(x.Codigo).CompareTo(Convert.ToInt64(y.Codigo)));
    //            aca.Sort((x, y) => Convert.ToInt64(x.Codigo).CompareTo(Convert.ToInt64(y.Codigo)));


    //            Assert.IsTrue(All.Count == 3);

    //            int difExpected = 2;
    //            List<string> dif 
    //            bool p;
    //            for (int i = 0; i < 3; i++)
    //            {
    //                dif = HelperTesting.GetDifferences<StockData>(aca[i], All[i]);
    //                p = (dif == null || dif.Count - difExpected == 0);
    //                Assert.IsTrue(p);
    //            }



    //        }
    //    }

    //    [TestMethod]
    //    public void GetAll()
    //    {
    //        var opts = new TransactionOptions
    //        {
    //            IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
    //        };

    //        using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
    //        {

    //            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
    //            fixture = HelperTesting.SetUp(testList, true);

    //            var productoService = new ProductoService();



    //            decimal stock_1;
    //            decimal stock_2;
    //            decimal stock_3;
    //            StockData unStock_1 = fixture.Build<StockData>().Create();
    //            StockData unStock_2 = fixture.Build<StockData>().Create();
    //            StockData unStock_3 = fixture.Build<StockData>().Create();

    //            unStock_1.producto = productoService.GetByID(new Guid("C29DF36A-67DC-4BA2-AD3E-5A2C6C7C6112"));
    //            unStock_2.producto = productoService.GetByID(new Guid("3EC55B8E-3259-4589-A577-D9B691F82A65"));
    //            unStock_3.producto = productoService.GetByID(new Guid("7ED20CE7-9DF3-4D3D-A640-CC0BC3C342D1"));

    //            stock_1 = unStock_1.stock;
    //            stock_2 = unStock_2.stock;
    //            stock_3 = unStock_3.stock;


    //            List<StockData> aca = new List<StockData>(){unStock_1,unStock_2,unStock_3};

    //            Assert.IsTrue(stockService.Insert(unStock_1, stock_1, unStock_1.Local.ID));
    //            Assert.IsTrue(stockService.Insert(unStock_2, stock_2, unStock_2.Local.ID));
    //            Assert.IsTrue(stockService.Insert(unStock_3, stock_3, unStock_3.Local.ID));



    //            List<StockData> All = stockService.GetAll(false, unStock_1.Local.ID);




    //            All.Sort((x, y) => Convert.ToInt64(x.Codigo).CompareTo(Convert.ToInt64(y.Codigo)));
    //            aca.Sort((x, y) => Convert.ToInt64(x.Codigo).CompareTo(Convert.ToInt64(y.Codigo)));


    //            Assert.IsTrue(All.Count==3);

    //            int difExpected = 2;
    //            List<string> dif 
    //            bool p;
    //            for (int i = 0; i < 3; i++)
    //            {
    //                dif = HelperTesting.GetDifferences<StockData>(aca[i], All[i]);
    //                p = (dif == null || dif.Count - difExpected == 0);
    //                Assert.IsTrue(p);
    //            }



    //        }
    //    }

    //    [
    //        TestMethod]
    //    public void ObtenerProducto()
    //    {
    //         var opts = new TransactionOptions
    //        {
    //            IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
    //        };

    //        using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
    //        {

    //            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
    //            fixture = HelperTesting.SetUp(testList, true);


                

    //            StockData unStock = fixture.Build<StockData>().Create();

    //            string codigoStock = unStock.Codigo;
    //            Guid Local = unStock.Local.ID;
    //            decimal stockTotal = unStock.stock;

    //            Assert.IsTrue(stockService.Insert(unStock, stockTotal, Local));


    //            StockData buscandoStock = stockService.obtenerProducto(codigoStock, Local);

    //            int difExpected = 2;//ID y null en metros61
    //            var dif = HelperTesting.GetDifferences<StockData>(unStock, buscandoStock);
    //            bool pp = (dif == null || dif.Count - difExpected == 0);
    //            Assert.IsTrue(pp);
    //        }
    //    }
    




    //[TestMethod]
    //     public void InsertAndGet()
    //     {
    //     var opts = new TransactionOptions
    //        {
    //            IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
    //        };

    //    using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
    //    {
    //        List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
    //        fixture = HelperTesting.SetUp(testList, true);


    //        fixture.Customizations.Add(
    //            new RandomNumericSequenceGenerator(0, 99, 200));

    //        StockData unStock = fixture.Build<StockData>().Without(p => p.metros).Create();

    //        string codigoStock = unStock.Codigo;
    //        Guid Local = unStock.Local.ID;
    //        decimal stockTotal = 100;

    //        Assert.IsTrue(stockService.Insert(unStock, stockTotal, Local));



    //        decimal stockDb = stockService.GetStockTotal(codigoStock, Local);


    //        Assert.IsTrue(stockDb == stockTotal);
    //    }
    //     }

    //     [TestMethod]
    //     public void InsertAndUpdateandGet()
    //     {

    //          var opts = new TransactionOptions
    //        {
    //            IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
    //        };

    //         using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
    //         {
    //             List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
    //             fixture = HelperTesting.SetUp(testList, true);


    //             fixture.Customizations.Add(
    //                 new RandomNumericSequenceGenerator(0, 99, 200));

    //             StockData unStock = fixture.Build<StockData>().Without(p => p.metros).Create();

    //             string codigoStock = unStock.Codigo;
    //             Guid Local = unStock.Local.ID;
    //             decimal StockInicial = 100;
    //             decimal addstock = 10;
    //             Assert.IsTrue(stockService.Insert(unStock, StockInicial, Local));



    //             decimal stockDb = stockService.GetStockTotal(codigoStock, Local);


    //             Assert.IsTrue(stockDb == StockInicial);


    //             Assert.IsTrue(stockService.UpdateStock(codigoStock, addstock, Local, true));

    //             stockDb = stockService.GetStockTotal(codigoStock, Local);

    //             Assert.IsTrue(stockDb == (StockInicial + addstock));

    //             Assert.IsTrue(stockService.UpdateStock(codigoStock, addstock, Local, false));

    //             stockDb = stockService.GetStockTotal(codigoStock, Local);

    //             Assert.IsTrue(stockDb == addstock);

    //             decimal ventastock = -1;
    //             Assert.IsTrue(stockService.UpdateStock(codigoStock, ventastock, Local, true));
    //             stockDb = stockService.GetStockTotal(codigoStock, Local);

    //             Assert.IsTrue((stockDb) == (addstock + ventastock));
    //         }
    //     }


         //setDinamicallyStock(string Codigo, Guid Local, bool onlyEnable = true)
    }
}
