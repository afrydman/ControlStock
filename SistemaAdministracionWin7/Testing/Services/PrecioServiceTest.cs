using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Services;
using Services.PrecioService;
using Services.ProductoService;

namespace Testing.Services
{

    [TestClass()]
    public class PrecioServiceTest
    {
        private Fixture fixture = new Fixture();




        [TestMethod]
        public void InsertAndGet_Test()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                fixture = HelperTesting.SetUp(testList, true);

                PrecioService precioService = new PrecioService();

                

                ProductoData producto =  fixture.Create<ProductoData>();


                Assert.IsTrue(new ProductoService().Insert(producto));

                ProductoTalleService productoTalleService = new ProductoTalleService();


                List<ProductoTalleData> productosTalles = productoTalleService.GetByProducto(producto.ID);

                List<ListaPrecioProductoTalleData> precios  = new List<ListaPrecioProductoTalleData>();
                List<ListaPrecioProductoTalleData> preciosDB = new List<ListaPrecioProductoTalleData>();

                ListaPrecioProductoTalleData precio;

                decimal aux = HelperService.ConvertToDecimalSeguro("1.2");
                foreach (var productoTalleData in productosTalles)
                {
                    precio = new ListaPrecioProductoTalleData();
                    precio.FatherID= new Guid("9113D691-2AF5-4B93-A095-5D9A2D5CFD59");
                    precio.ProductoTalle = productoTalleData;
                    precio.Precio =aux;
                    Assert.IsTrue(precioService.InsertOrUpdate(precio));
                    precios.Add(precio);
                    aux++;
                }

                
                foreach (var productoTalleData in productosTalles)
                {
                    precio = new ListaPrecioProductoTalleData();
                    precio.FatherID = new Guid("9113D691-2AF5-4B93-A095-5D9A2D5CFD59");
                    precio.ProductoTalle = productoTalleData;
                    precio.Precio = precioService.GetPrecio(new Guid("9113D691-2AF5-4B93-A095-5D9A2D5CFD59"),
                        productoTalleData.ID);
                 preciosDB.Add(precio);
                }

                precios.Sort((x, y) => x.Precio.CompareTo(y.Precio));
                preciosDB.Sort((x, y) => x.Precio.CompareTo(y.Precio));

                int difExpected = 0;
                List<string> dif;
                bool p;
                for (int i = 0; i < HelperService.TallesPorProductoDefault; i++)
                {
                    dif = HelperTesting.GetDifferences(precios[i], preciosDB[i]);
                    p = (dif == null || dif.Count - difExpected == 0);
                    Assert.IsTrue(p);
                }

                
                
             
            }

        }


        [TestMethod]
        public void InsertAndUpdateAndGet_Test()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                fixture = HelperTesting.SetUp(testList, true);

                PrecioService precioService = new PrecioService();



                ProductoData producto = fixture.Create<ProductoData>();


                Assert.IsTrue(new ProductoService().Insert(producto));

                ProductoTalleService productoTalleService = new ProductoTalleService();


                List<ProductoTalleData> productosTalles = productoTalleService.GetByProducto(producto.ID);

                List<ListaPrecioProductoTalleData> precios = new List<ListaPrecioProductoTalleData>();
                List<ListaPrecioProductoTalleData> preciosDB = new List<ListaPrecioProductoTalleData>();

                ListaPrecioProductoTalleData precio;

                decimal aux = HelperService.ConvertToDecimalSeguro("1.2");
                foreach (var productoTalleData in productosTalles)
                {
                    precio = new ListaPrecioProductoTalleData();
                    precio.FatherID = new Guid("9113D691-2AF5-4B93-A095-5D9A2D5CFD59");
                    precio.ProductoTalle = productoTalleData;
                    precio.Precio = aux;
                    Assert.IsTrue(precioService.InsertOrUpdate(precio));
                    //precios.Add(precio);
                    aux++;
                }

                foreach (var productoTalleData in productosTalles)
                {
                    precio = new ListaPrecioProductoTalleData();
                    precio.FatherID = new Guid("9113D691-2AF5-4B93-A095-5D9A2D5CFD59");
                    precio.ProductoTalle = productoTalleData;
                    precio.Precio = aux;
                    Assert.IsTrue(precioService.InsertOrUpdate(precio));
                    precios.Add(precio);
                    aux++;
                }

                foreach (var productoTalleData in productosTalles)
                {
                    precio = new ListaPrecioProductoTalleData();
                    precio.FatherID = new Guid("9113D691-2AF5-4B93-A095-5D9A2D5CFD59");
                    precio.ProductoTalle = productoTalleData;
                    precio.Precio = precioService.GetPrecio(new Guid("9113D691-2AF5-4B93-A095-5D9A2D5CFD59"),
                        productoTalleData.ID);
                    preciosDB.Add(precio);
                }

                precios.Sort((x, y) => x.Precio.CompareTo(y.Precio));
                preciosDB.Sort((x, y) => x.Precio.CompareTo(y.Precio));

                int difExpected = 0;
                List<string> dif;
                bool p;
                for (int i = 0; i < HelperService.TallesPorProductoDefault; i++)
                {
                    dif = HelperTesting.GetDifferences(precios[i], preciosDB[i]);
                    p = (dif == null || dif.Count - difExpected == 0);
                    Assert.IsTrue(p);
                }
                



            }

        }

    }
}
