using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Services;
using Services.ColorService;
using Services.ComprasProveedorService;
using Services.LocalService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.RemitoService;
using Services.StockService;
using Services.VentaService;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Testing.Flows
{
    [TestClass()]
    public class FlujosStockTest
    {

        /*
         * 
         *                                      cant esperado
         * Stock	Compra stock Proveedores;	100	    100
                                        Venta	2	    98
                                        venta	50	    48
                               ingreso cambio	5	    53
                               alta manual	    23	    76
                               baja manual	    50	    26
         * 
         * 
         */


          private Fixture fixture = new Fixture();

        [TestMethod]
        public void Test1()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {


                

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.Codigo);
                testList.Add(HelperTesting.ServicesEnum.ColorService);
                testList.Add(HelperTesting.ServicesEnum.ProveedorService);
                testList.Add(HelperTesting.ServicesEnum.LocalService);

                fixture = HelperTesting.SetUp(testList, true);


                var proveedorService = new ProveedorService();
                var colorService = new ColorService();
                var productoSerivce = new ProductoService();
                var comprasProveedorService = new ComprasProveedorService();
                var stockService = new StockService();
                var localService = new LocalService();




                LocalData local = fixture.Create<LocalData>();
                Assert.IsTrue(localService.Insert(local));


                ColorData colorNuevo = new ColorData();
                colorNuevo.Description = "blanco";
                colorNuevo.Codigo = "666";


                Assert.IsTrue(colorService.Insert(colorNuevo));

                ProveedorData proveedorNuevo = fixture.Create<ProveedorData>();
                proveedorNuevo.Codigo = "9090";
                proveedorNuevo.RazonSocial = "flujo test";


                Assert.IsTrue(proveedorService.Insert(proveedorNuevo));

                ProductoData productoNuevo = new ProductoData();
                productoNuevo.Proveedor = proveedorNuevo;
                productoNuevo.CodigoInterno = "";
                productoNuevo.Description = "Producto test";

                Assert.IsTrue(productoSerivce.Insert(productoNuevo));

                productoNuevo = productoSerivce.GetByID(productoNuevo.ID);//para obtener el Codigo



                string codigoTest =stockService.GetCodigoBarraDinamico(productoNuevo, colorNuevo, "10");


                decimal stock0 = stockService.GetStockTotal(codigoTest, local.ID);

                Assert.IsTrue(stock0==HelperDTO.STOCK_MINIMO_INVALIDO);



                var compraProveedores = fixture.Create<ComprasProveedoresData>();

                compraProveedores.Local = local;

                ComprasProveedoresdetalleData detalle1 = new ComprasProveedoresdetalleData();
                compraProveedores.Children = new List<ComprasProveedoresdetalleData>();

                detalle1.Cantidad = 100;
                detalle1.Codigo = codigoTest;
                compraProveedores.Children .Add(detalle1);


                Assert.IsTrue(comprasProveedorService.Insert(compraProveedores));


                decimal stock1 = stockService.GetStockTotal(codigoTest, local.ID);

                Assert.IsTrue(stock1 == 100);


                var ventaService = new VentaService();

                var venta = fixture.Create<DTO.BusinessEntities.VentaData>();

                venta.Local = local;
                venta.Children = new List<VentaDetalleData>();

                VentaDetalleData detVenta1 = new VentaDetalleData();

                detVenta1.Cantidad = 2;
                detVenta1.Codigo = codigoTest;
                venta.Children.Add(detVenta1);

                Assert.IsTrue(ventaService.Insert(venta));


                decimal stock2 = stockService.GetStockTotal(codigoTest, local.ID);

                Assert.IsTrue(stock2 == stock1-2);


                var venta2 = fixture.Create<DTO.BusinessEntities.VentaData>();

                venta2.Local = local;
                venta2.Children = new List<VentaDetalleData>();

                VentaDetalleData detVenta2 = new VentaDetalleData();

                detVenta2.Cantidad = 50;
                detVenta2.Codigo = codigoTest;
                venta2.Children.Add(detVenta2);

                Assert.IsTrue(ventaService.Insert(venta2));


                decimal stock3 = stockService.GetStockTotal(codigoTest, local.ID);

                Assert.IsTrue(stock3 == stock2 - 50);





                var venta3 = fixture.Create<DTO.BusinessEntities.VentaData>();

                venta3.Local = local;
                venta3.Children = new List<VentaDetalleData>();

                VentaDetalleData detVenta3 = new VentaDetalleData();

                detVenta3.Cantidad = -5;
                detVenta3.Codigo = codigoTest;
                venta3.Children.Add(detVenta3);
                venta3.Cambio = true;
                Assert.IsTrue(ventaService.Insert(venta3));


                decimal stock4 = stockService.GetStockTotal(codigoTest, local.ID);

                Assert.IsTrue(stock4 == stock3+ 5);





                var remitoService = new RemitoService();


                var remito1 = fixture.Create<RemitoData>();
                remito1.LocalDestino = local;
                remito1.Local= local;

                remito1.Children = new List<remitoDetalleData>();
                remitoDetalleData detRemi1 = new remitoDetalleData();

                detRemi1.Cantidad = 23;
                detRemi1.Codigo = codigoTest;

                remito1.Children.Add(detRemi1);

                Assert.IsTrue(remitoService.Insert(remito1));

                Assert.IsTrue(remitoService.confirmarRecibo(remito1.ID));


                decimal stock5 = stockService.GetStockTotal(codigoTest, local.ID);

                Assert.IsTrue(stock5 == stock4 +23);





                var remito2 = fixture.Create<RemitoData>();
                remito2.LocalDestino = local;
                remito2.Local = local;

                remito1.Children = new List<remitoDetalleData>();
                remitoDetalleData detRemi2 = new remitoDetalleData();

                detRemi2.Cantidad = 50;
                detRemi2.Codigo = codigoTest;

                remito2.Children.Add(detRemi2);

                Assert.IsTrue(remitoService.Insert(remito2));

                Assert.IsTrue(remitoService.confirmarBaja(remito2));

                decimal stock6 = stockService.GetStockTotal(codigoTest, local.ID);

                Assert.IsTrue(stock6 == stock5 - 50);
            }
        }

        [TestMethod]
        public void Test4()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {




                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.Codigo);
                testList.Add(HelperTesting.ServicesEnum.ColorService);
                testList.Add(HelperTesting.ServicesEnum.ProveedorService);
                testList.Add(HelperTesting.ServicesEnum.LocalService);

                fixture = HelperTesting.SetUp(testList, true);


                var proveedorService = new ProveedorService();
                var colorService = new ColorService();
                var productoSerivce = new ProductoService();
                var comprasProveedorService = new ComprasProveedorService();
                var stockService = new StockService();
                var localService = new LocalService();




                LocalData localACA = fixture.Create<LocalData>();
                localACA.ID = HelperService.IDLocal;


                Assert.IsTrue(localService.Insert(localACA));


                LocalData localALLA = fixture.Create<LocalData>();
                localALLA.ID = Guid.NewGuid();
                Assert.IsTrue(localService.Insert(localALLA));

                ColorData colorNuevo = new ColorData();
                colorNuevo.Description = "blanco";
                colorNuevo.Codigo = "666";


                Assert.IsTrue(colorService.Insert(colorNuevo));

                ProveedorData proveedorNuevo = fixture.Create<ProveedorData>();
                proveedorNuevo.Codigo = "9090";
                proveedorNuevo.RazonSocial = "flujo test";


                Assert.IsTrue(proveedorService.Insert(proveedorNuevo));

                ProductoData productoNuevo = new ProductoData();
                productoNuevo.Proveedor = proveedorNuevo;
                productoNuevo.CodigoInterno = "";
                productoNuevo.Description = "Producto test";

                Assert.IsTrue(productoSerivce.Insert(productoNuevo));

                productoNuevo = productoSerivce.GetByID(productoNuevo.ID);//para obtener el Codigo



                string codigoTest = stockService.GetCodigoBarraDinamico(productoNuevo, colorNuevo, "10");


                decimal stock0 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock0 == HelperDTO.STOCK_MINIMO_INVALIDO);



                var compraProveedores = fixture.Create<ComprasProveedoresData>();

                compraProveedores.Local = localACA;
                compraProveedores.Proveedor = proveedorNuevo;
                compraProveedores.Enable = true;

                ComprasProveedoresdetalleData detalle1 = new ComprasProveedoresdetalleData();
                compraProveedores.Children = new List<ComprasProveedoresdetalleData>();


                detalle1.Cantidad = 100;
                detalle1.Codigo = codigoTest;
                compraProveedores.Children.Add(detalle1);


                Assert.IsTrue(comprasProveedorService.Insert(compraProveedores));


                decimal stock1 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock1 == 100);





                var ventaService = new VentaService();

                var venta = fixture.Create<DTO.BusinessEntities.VentaData>();

                venta.Local = localACA;
                venta.Children = new List<VentaDetalleData>();
                venta.Enable = true;
                VentaDetalleData detVenta1 = new VentaDetalleData();

                detVenta1.Cantidad = 2;
                detVenta1.Codigo = codigoTest;
                venta.Children.Add(detVenta1);

                Assert.IsTrue(ventaService.Insert(venta));


                decimal stock2 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock2 == stock1 - 2);


                var venta2 = fixture.Create<DTO.BusinessEntities.VentaData>();

                venta2.Local = localACA;
                venta2.Children = new List<VentaDetalleData>();
                venta2.Enable = true;
                VentaDetalleData detVenta2 = new VentaDetalleData();

                detVenta2.Cantidad = 50;
                detVenta2.Codigo = codigoTest;
                venta2.Children.Add(detVenta2);

                Assert.IsTrue(ventaService.Insert(venta2));


                decimal stock3 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock3 == stock2 - 50);





                var venta3 = fixture.Create<DTO.BusinessEntities.VentaData>();

                venta3.Local = localACA;
                venta3.Children = new List<VentaDetalleData>();
                venta3.Enable = true;
                VentaDetalleData detVenta3 = new VentaDetalleData();

                detVenta3.Cantidad = -5;
                detVenta3.Codigo = codigoTest;
                venta3.Children.Add(detVenta3);
                venta3.Cambio = true;
                Assert.IsTrue(ventaService.Insert(venta3));


                decimal stock4 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock4 == stock3 + 5);





                var remitoService = new RemitoService();


                var remito1 = fixture.Create<RemitoData>();
                remito1.LocalDestino = localACA;
                remito1.Local = localACA;
                remito1.Enable = true;
                remito1.Children = new List<remitoDetalleData>();
                remitoDetalleData detRemi1 = new remitoDetalleData();

                detRemi1.Cantidad = 23;
                detRemi1.Codigo = codigoTest;

                remito1.Children.Add(detRemi1);

                Assert.IsTrue(remitoService.Insert(remito1));

                Assert.IsTrue(remitoService.confirmarRecibo(remito1.ID));


                decimal stock5 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock5 == stock4 + 23);





                var remito2 = fixture.Create<RemitoData>();
                remito2.LocalDestino = localALLA;
                remito2.Local = localACA;
                remito2.Enable = true;
                
                remitoDetalleData detRemi2 = new remitoDetalleData();

                detRemi2.Cantidad = 50;
                detRemi2.Codigo = codigoTest;

                remito2.Children.Add(detRemi2);

                Assert.IsTrue(remitoService.Insert(remito2));

                Assert.IsTrue(remitoService.confirmarBaja(remito2));

                decimal stock6 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock6 == stock5 - 50);





                Assert.IsTrue(remitoService.Disable(remito2.ID,true));

                decimal stock7 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock7 == stock5);





                Assert.IsTrue(remitoService.Disable(remito1.ID,  true));

                decimal stock8 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock8 == stock4);

                



                Assert.IsTrue(ventaService.Disable(venta3.ID,true));

                decimal stock9 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock9 == stock3);





                Assert.IsTrue(ventaService.Disable(venta2.ID, true));

                decimal stock10 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock10 == stock2);





                Assert.IsTrue(ventaService.Disable(venta.ID, true));

                decimal stock11 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock11 == stock1);




                Assert.IsTrue(comprasProveedorService.Disable(compraProveedores.ID,true));

                decimal stock12 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock12 == 0 || stock12 == HelperDTO.STOCK_MINIMO_INVALIDO);




            }
        }

        [TestMethod]
        public void Test8()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {




                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.Codigo);
                testList.Add(HelperTesting.ServicesEnum.ColorService);
                testList.Add(HelperTesting.ServicesEnum.ProveedorService);
                testList.Add(HelperTesting.ServicesEnum.LocalService);

                fixture = HelperTesting.SetUp(testList, true);


                var proveedorService = new ProveedorService();
                var colorService = new ColorService();
                var productoSerivce = new ProductoService();
                var comprasProveedorService = new ComprasProveedorService();
                var stockService = new StockService();
                var stockMetrosService = new StockMetrosService();
                var localService = new LocalService();



                HelperService.haymts = true;



                LocalData localACA = fixture.Create<LocalData>();
                localACA.ID = HelperService.IDLocal;

                Assert.IsTrue(localService.Insert(localACA));

                LocalData localAllA = fixture.Create<LocalData>();
                localAllA.ID = Guid.NewGuid();

                Assert.IsTrue(localService.Insert(localAllA));

                ColorData colorNuevo = new ColorData();
                colorNuevo.Description = "blanco";
                colorNuevo.Codigo = "666";


                Assert.IsTrue(colorService.Insert(colorNuevo));

                ProveedorData proveedorNuevo = fixture.Create<ProveedorData>();
                proveedorNuevo.Codigo = "9090";
                proveedorNuevo.RazonSocial = "flujo test";


                Assert.IsTrue(proveedorService.Insert(proveedorNuevo));

                ProductoData productoNuevo = new ProductoData();
                productoNuevo.Proveedor = proveedorNuevo;
                productoNuevo.CodigoInterno = "";
                productoNuevo.Description = "Producto test";
                productoNuevo.Enable = true;
                Assert.IsTrue(productoSerivce.Insert(productoNuevo));

                productoNuevo = productoSerivce.GetByID(productoNuevo.ID);//para obtener el Codigo

                

                //string codigoTest = stockService.GetCodigoBarraDinamico(productoNuevo, colorNuevo, "10");
                string codigoTest = stockMetrosService.obtenerCodigo(productoNuevo.CodigoInterno, colorNuevo.Codigo,
                    "10,23", true);

                

                decimal stock0 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock0 == HelperDTO.STOCK_MINIMO_INVALIDO);



                var compraProveedores = fixture.Create<ComprasProveedoresData>();

                compraProveedores.Local = localACA;
                compraProveedores.Proveedor = proveedorNuevo;
                compraProveedores.Enable = true;

                ComprasProveedoresdetalleData detalle1 = new ComprasProveedoresdetalleData();
                compraProveedores.Children = new List<ComprasProveedoresdetalleData>();


                detalle1.Cantidad = 100;
                detalle1.Codigo = codigoTest;
                compraProveedores.Children.Add(detalle1);


                Assert.IsTrue(comprasProveedorService.Insert(compraProveedores));


                decimal stock1 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock1 == 100);





                var ventaService = new VentaService();

                var venta = fixture.Create<DTO.BusinessEntities.VentaData>();

                venta.Local = localACA;
                venta.Children = new List<VentaDetalleData>();

                VentaDetalleData detVenta1 = new VentaDetalleData();

                detVenta1.Cantidad = 2;
                detVenta1.Codigo = codigoTest;
                venta.Children.Add(detVenta1);
                venta.Enable = true;
                Assert.IsTrue(ventaService.Insert(venta));


                decimal stock2 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock2 == stock1 - 2);


                var venta2 = fixture.Create<DTO.BusinessEntities.VentaData>();

                venta2.Local = localACA;
                venta2.Children = new List<VentaDetalleData>();
                venta2.Enable = true;
                VentaDetalleData detVenta2 = new VentaDetalleData();

                detVenta2.Cantidad = 50;
                detVenta2.Codigo = codigoTest;
                venta2.Children.Add(detVenta2);

                Assert.IsTrue(ventaService.Insert(venta2));


                decimal stock3 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock3 == stock2 - 50);





                var venta3 = fixture.Create<DTO.BusinessEntities.VentaData>();

                venta3.Local = localACA;
                venta3.Children = new List<VentaDetalleData>();
                venta3.Enable = true;
                VentaDetalleData detVenta3 = new VentaDetalleData();

                detVenta3.Cantidad = -5;
                detVenta3.Codigo = codigoTest;
                venta3.Children.Add(detVenta3);
                venta3.Cambio = true;
                Assert.IsTrue(ventaService.Insert(venta3));


                decimal stock4 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock4 == stock3 + 5);





                var remitoService = new RemitoService();


                var remito1 = fixture.Create<RemitoData>();
                remito1.LocalDestino = localACA;
                remito1.Local = localACA;
                remito1.Enable = true;
                remito1.Children = new List<remitoDetalleData>();
                remitoDetalleData detRemi1 = new remitoDetalleData();

                detRemi1.Cantidad = 23;
                detRemi1.Codigo = codigoTest;

                remito1.Children.Add(detRemi1);

                Assert.IsTrue(remitoService.Insert(remito1));

                Assert.IsTrue(remitoService.confirmarRecibo(remito1.ID));


                decimal stock5 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock5 == stock4 + 23);





                var remito2 = fixture.Create<RemitoData>();
                remito2.LocalDestino = localAllA;
                remito2.Local = localACA;
                remito2.Enable = true;

                
                remitoDetalleData detRemi2 = new remitoDetalleData();

                detRemi2.Cantidad = 50;
                detRemi2.Codigo = codigoTest;

                remito2.Children.Add(detRemi2);

                Assert.IsTrue(remitoService.Insert(remito2));

                Assert.IsTrue(remitoService.confirmarBaja(remito2));

                decimal stock6 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock6 == stock5 - 50);





                Assert.IsTrue(remitoService.Disable(remito2.ID,  true));

                decimal stock7 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock7 == stock5);





                Assert.IsTrue(remitoService.Disable(remito1.ID,  true));

                decimal stock8 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock8 == stock4);





                Assert.IsTrue(ventaService.Disable(venta3.ID, true));

                decimal stock9 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock9 == stock3);





                Assert.IsTrue(ventaService.Disable(venta2.ID, true));

                decimal stock10 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock10 == stock2);





                Assert.IsTrue(ventaService.Disable(venta.ID, true));

                decimal stock11 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock11 == stock1);




                Assert.IsTrue(comprasProveedorService.Disable(compraProveedores.ID, true));

                decimal stock12 = stockService.GetStockTotal(codigoTest, localACA.ID);

                Assert.IsTrue(stock12 == 0 || stock12 == HelperDTO.STOCK_MINIMO_INVALIDO);




            }
        }






    }
}
