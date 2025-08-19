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
using Repository.Repositories.NotasRepository;
using Repository.Repositories.VentaRepository;
using Services;
using Services.ClienteService;
using Services.ComprasProveedorService;
using Services.FormaPagoService;
using Services.NotaService;
using Services.OrdenPagoService;
using Services.ProveedorService;
using Services.ReciboService;
using Services.VentaService;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Testing.Flows
{
    [TestClass()]
    public class FlujoCCTest
    {
        private Fixture fixture = new Fixture();

        [TestMethod]
        public void Test001_CuentaCorrienteCliente()
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
                testList.Add(HelperTesting.ServicesEnum.ClienteService);
                //testList.Add(HelperTesting.ServicesEnum.LocalService);

                fixture = HelperTesting.SetUp(testList, true);


                var clienteService = new ClienteService();
                var cliente1 = fixture.Create<ClienteData>();
                var formaPagoService = new FormaPagoService();
                var notaCreditoClienteService = new NotaService(new NotaCreditoClienteRepository(),new NotaCreditoClienteDetalleRepository(),new TributoNotaCreditoClientesRepository());
                var notaDebitoClienteService  = new NotaService(new NotaDebitoClienteRepository(),new NotaDebitoClienteDetalleRepository(), new TributoNotaDebitoClientesRepository());


                Assert.IsTrue(clienteService.Insert(cliente1));

                DateTime maxFechaRecibo;
                DateTime maxFechaVenta;
                decimal cc;


                decimal montoVenta1 = 100;
                decimal montoVenta2 = 50;

                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);
                
                Assert.IsTrue(cc==0);
                Assert.IsTrue(maxFechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaVenta == HelperDTO.BEGINNING_OF_TIME_DATE);

                
                DateTime fechaVenta1 = new DateTime(2017,10,10);
                DateTime fechaVenta2 = new DateTime(2020, 10, 10);

                var ventaService = new VentaService();
                var venta1 = fixture.Create<VentaData>();

                venta1.Cliente = cliente1;
                venta1.Monto = montoVenta1;
                venta1.Descuento = 0;//check this
                venta1.Date = fechaVenta1;
                venta1.Pagos = new List<PagoData>();
                PagoData unPago = new PagoData();
                unPago.FormaPago = formaPagoService.GetByID(HelperService.idCC);
                unPago.Importe = 100;
                venta1.Pagos.Add(unPago);
                venta1.Enable = true;

                Assert.IsTrue(ventaService.Insert(venta1));


                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc*-1 == montoVenta1);
                Assert.IsTrue(maxFechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaVenta == fechaVenta1);




                var venta2 = fixture.Create<VentaData>();

                venta2.Cliente = cliente1;
                venta2.Monto = montoVenta2;
                venta2.Descuento = 0;//check this
                venta2.Date = fechaVenta2;
                venta2.Pagos = new List<PagoData>();
                PagoData unPago2 = new PagoData();
                unPago2.FormaPago = formaPagoService.GetByID(HelperService.idCC);
                unPago2.Importe = montoVenta2;
                venta2.Pagos.Add(unPago2);
                venta2.Enable = true;

                Assert.IsTrue(ventaService.Insert(venta2));



                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc * -1 == montoVenta1+montoVenta2);
                Assert.IsTrue(maxFechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaVenta == fechaVenta2);


                var notaCredito = fixture.Create<NotaData>();

                var montoNotaCredito = 33;

                notaCredito.Monto = montoNotaCredito;
                notaCredito.tercero = cliente1;
                notaCredito.Enable = true;

                Assert.IsTrue(notaCreditoClienteService.Insert(notaCredito));

                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc * -1 == montoVenta1 + montoVenta2-montoNotaCredito);
                Assert.IsTrue(maxFechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaVenta == fechaVenta2);


                var notaDebito = fixture.Create<NotaData>();

                var montoNotaDebito = 80;

                notaDebito.Monto = montoNotaDebito;
                notaDebito.tercero = cliente1;
                notaDebito.Enable = true;

                Assert.IsTrue(notaDebitoClienteService.Insert(notaDebito));


                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc * -1 == montoVenta1 + montoVenta2 - montoNotaCredito+montoNotaDebito);
                Assert.IsTrue(maxFechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaVenta == fechaVenta2);


                var reciboService = new ReciboService();

                var recibo1 = fixture.Create<ReciboData>();

                var montoRecibo = 90;
                var fechaRecibo = new DateTime(2016, 2, 15);
                recibo1.tercero = cliente1;
                recibo1.Enable = true;
                recibo1.Monto = montoRecibo;
                recibo1.Date = fechaRecibo;

                Assert.IsTrue(reciboService.Insert(recibo1));


                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc * -1 == montoVenta1 + montoVenta2 - montoNotaCredito + montoNotaDebito -montoRecibo);
                Assert.IsTrue(maxFechaRecibo == fechaRecibo);
                Assert.IsTrue(maxFechaVenta == fechaVenta2);


            }
        }


        [TestMethod]
        public void Test002_CuentaCorrienteClienteConAnulacion()
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
                testList.Add(HelperTesting.ServicesEnum.ClienteService);
                //testList.Add(HelperTesting.ServicesEnum.LocalService);

                fixture = HelperTesting.SetUp(testList, true);


                var clienteService = new ClienteService();
                var cliente1 = fixture.Create<ClienteData>();
                var formaPagoService = new FormaPagoService();
                var notaCreditoClienteService = new NotaService(new NotaCreditoClienteRepository(), new NotaCreditoClienteDetalleRepository(), new TributoNotaCreditoClientesRepository());
                var notaDebitoClienteService = new NotaService(new NotaDebitoClienteRepository(),
                    new NotaDebitoClienteDetalleRepository(), new TributoNotaDebitoClientesRepository());





                Assert.IsTrue(clienteService.Insert(cliente1));

                DateTime maxFechaRecibo;
                DateTime maxFechaVenta;
                decimal cc;


                decimal montoVenta1 = 100;
                decimal montoVenta2 = 50;

                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc == 0);
                Assert.IsTrue(maxFechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaVenta == HelperDTO.BEGINNING_OF_TIME_DATE);


                DateTime fechaVenta1 = new DateTime(2017, 10, 10);
                DateTime fechaVenta2 = new DateTime(2020, 10, 10);

                var ventaService = new VentaService();
                var venta1 = fixture.Create<VentaData>();

                venta1.Cliente = cliente1;
                venta1.Monto = montoVenta1;
                venta1.Descuento = 0;//check this
                venta1.Date = fechaVenta1;
                venta1.Pagos = new List<PagoData>();
                PagoData unPago = new PagoData();
                unPago.FormaPago = formaPagoService.GetByID(HelperService.idCC);
                unPago.Importe = 100;
                venta1.Pagos.Add(unPago);
                venta1.Enable = true;

                Assert.IsTrue(ventaService.Insert(venta1));


                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc * -1 == montoVenta1);
                Assert.IsTrue(maxFechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaVenta == fechaVenta1);




                var venta2 = fixture.Create<VentaData>();

                venta2.Cliente = cliente1;
                venta2.Monto = montoVenta2;
                venta2.Descuento = 0;//check this
                venta2.Date = fechaVenta2;
                venta2.Pagos = new List<PagoData>();
                PagoData unPago2 = new PagoData();
                unPago2.FormaPago = formaPagoService.GetByID(HelperService.idCC);
                unPago2.Importe = montoVenta2;
                venta2.Pagos.Add(unPago2);
                venta2.Enable = true;

                Assert.IsTrue(ventaService.Insert(venta2));



                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc * -1 == montoVenta1 + montoVenta2);
                Assert.IsTrue(maxFechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaVenta == fechaVenta2);






                var notaCredito = fixture.Create<NotaData>();

                var montoNotaCredito = 33;

                notaCredito.Monto = montoNotaCredito;
                notaCredito.tercero = cliente1;
                notaCredito.Enable = true;

                Assert.IsTrue(notaCreditoClienteService.Insert(notaCredito));



                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc * -1 == montoVenta1 + montoVenta2 - montoNotaCredito);
                Assert.IsTrue(maxFechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaVenta == fechaVenta2);







                var notaDebito = fixture.Create<NotaData>();

                var montoNotaDebito = 80;

                notaDebito.Monto = montoNotaDebito;
                notaDebito.tercero = cliente1;
                notaDebito.Enable = true;

                Assert.IsTrue(notaDebitoClienteService.Insert(notaDebito));



                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc * -1 == montoVenta1 + montoVenta2 - montoNotaCredito + montoNotaDebito);
                Assert.IsTrue(maxFechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaVenta == fechaVenta2);






                var reciboService = new ReciboService();

                var recibo1 = fixture.Create<ReciboData>();

                var montoRecibo = 90;
                var fechaRecibo = new DateTime(2016, 2, 15);
                recibo1.tercero = cliente1;
                recibo1.Enable = true;
                recibo1.Monto = montoRecibo;
                recibo1.Date = fechaRecibo;

                Assert.IsTrue(reciboService.Insert(recibo1));


                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc * -1 == montoVenta1 + montoVenta2 - montoNotaCredito + montoNotaDebito - montoRecibo);
                Assert.IsTrue(maxFechaRecibo == fechaRecibo);
                Assert.IsTrue(maxFechaVenta == fechaVenta2);





                Assert.IsTrue(reciboService.Disable(recibo1));


                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc * -1 == montoVenta1 + montoVenta2 - montoNotaCredito + montoNotaDebito );
                Assert.IsTrue(maxFechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaVenta == fechaVenta2);




                Assert.IsTrue(notaDebitoClienteService.Disable(notaDebito));

                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc * -1 == montoVenta1 + montoVenta2 - montoNotaCredito);
                Assert.IsTrue(maxFechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaVenta == fechaVenta2);



                Assert.IsTrue(notaCreditoClienteService.Disable(notaCredito));

                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc * -1 == montoVenta1 + montoVenta2 );
                Assert.IsTrue(maxFechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaVenta == fechaVenta2);





                Assert.IsTrue(ventaService.Disable(venta2));

                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc * -1 == montoVenta1 );
                Assert.IsTrue(maxFechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsFalse(maxFechaVenta == fechaVenta2);
                Assert.IsTrue(maxFechaVenta == fechaVenta1);

                Assert.IsTrue(ventaService.Disable(venta1));

                clienteService.GetCC(cliente1, out maxFechaRecibo, out maxFechaVenta, out  cc);

                Assert.IsTrue(cc == 0);
                Assert.IsTrue(maxFechaRecibo == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaVenta == HelperDTO.BEGINNING_OF_TIME_DATE);
                
            }
        }


        [TestMethod]
        public void Test003_CuentaCorriente_Proveedor()
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
                //testList.Add(HelperTesting.ServicesEnum.LocalService);

                fixture = HelperTesting.SetUp(testList, true);


                var proveedorService = new ProveedorService();
                var proveedor1 = fixture.Create<ProveedorData>();
                var formaPagoService = new FormaPagoService();
                var notaCreditoProveedorService = new NotaService(new NotaCreditoProveedoresRepository(), new NotaCreditoProveedoresDetalleRepository(), new TributoNotaCreditoProveedoresRepository());
                var notaDebitoProveedorService = new NotaService(new NotaDebitoProveedoresRepository(), new NotaDebitoProveedoresDetalleRepository(), new TributoNotaDebitoProveedoresRepository());


                Assert.IsTrue(proveedorService.Insert(proveedor1));

                DateTime maxFechaPago;
                DateTime maxFechaCompra;
                decimal cc;


                decimal montocompraa1 = 100;
                decimal monto = 50;

                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc == 0);
                Assert.IsTrue(maxFechaPago == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaCompra == HelperDTO.BEGINNING_OF_TIME_DATE);


                DateTime fechaCompra1 = new DateTime(2017, 10, 10);
                DateTime fechaCompra2 = new DateTime(2020, 10, 10);

                var compraProveedoresService = new ComprasProveedorService();
                var compra1 = fixture.Create<ComprasProveedoresData>();

                compra1.Proveedor = proveedor1;
                compra1.Monto = montocompraa1;
                compra1.Descuento = 0;//check this
                compra1.Date = fechaCompra1;
                
                compra1.Enable = true;

                Assert.IsTrue(compraProveedoresService.Insert(compra1));


                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc * -1 == montocompraa1);
                Assert.IsTrue(maxFechaPago == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaCompra == fechaCompra1);




                var compra2 = fixture.Create<ComprasProveedoresData>();

                compra2.Proveedor = proveedor1;
                compra2.Monto = monto;
                compra2.Descuento = 0;//check this
                compra2.Date = fechaCompra2;
                
                compra2.Enable = true;

                Assert.IsTrue(compraProveedoresService.Insert(compra2));



                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc * -1 == montocompraa1 + monto);
                Assert.IsTrue(maxFechaPago == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaCompra == fechaCompra2);


                var notaCredito = fixture.Create<NotaData>();

                var montoNotaCredito = 33;

                notaCredito.Monto = montoNotaCredito;
                notaCredito.tercero = proveedor1;
                notaCredito.Enable = true;

                Assert.IsTrue(notaCreditoProveedorService.Insert(notaCredito));

                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc * -1 == montocompraa1 + monto - montoNotaCredito);
                Assert.IsTrue(maxFechaPago == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaCompra == fechaCompra2);


                var notaDebito = fixture.Create<NotaData>();

                var montoNotaDebito = 80;

                notaDebito.Monto = montoNotaDebito;
                notaDebito.tercero = proveedor1;
                notaDebito.Enable = true;

                Assert.IsTrue(notaDebitoProveedorService.Insert(notaDebito));


                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc * -1 == montocompraa1 + monto - montoNotaCredito + montoNotaDebito);
                Assert.IsTrue(maxFechaPago == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaCompra == fechaCompra2);


                var ordenPagoService = new OrdenPagoService();

                var ordenPago1 = fixture.Create<OrdenPagoData>();

                var montoRecibo = 90;
                var fechaOpago = new DateTime(2016, 2, 15);
                ordenPago1.Tercero = proveedor1;
                ordenPago1.Enable = true;
                ordenPago1.Monto = montoRecibo;
                ordenPago1.Date = fechaOpago;

                Assert.IsTrue(ordenPagoService.Insert(ordenPago1));


                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc * -1 == montocompraa1 + monto - montoNotaCredito + montoNotaDebito - montoRecibo);
                Assert.IsTrue(maxFechaPago == fechaOpago);
                Assert.IsTrue(maxFechaCompra == fechaCompra2);


            }
        }

        [TestMethod]
        public void Test004_CuentaCorriente_Proveedores_ConAnulacion()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {



                IEnumerable<int> numbers = Enumerable.Range(0, 10);
                IEnumerable<string> evens = from num in numbers  select "i";


                
                string a = evens.ToString()+ "a";

                a += "x";

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.Codigo);
                testList.Add(HelperTesting.ServicesEnum.ColorService);
                testList.Add(HelperTesting.ServicesEnum.ProveedorService);
                //testList.Add(HelperTesting.ServicesEnum.LocalService);

                fixture = HelperTesting.SetUp(testList, true);


                var proveedorService = new ProveedorService();
                var proveedor1 = fixture.Create<ProveedorData>();
                var formaPagoService = new FormaPagoService();
                var notaCreditoProveedorService = new NotaService(new NotaCreditoProveedoresRepository(), new NotaCreditoProveedoresDetalleRepository(), new TributoNotaCreditoProveedoresRepository());
                var notaDebitoProveedorService = new NotaService(new NotaDebitoProveedoresRepository(), new NotaDebitoProveedoresDetalleRepository(), new TributoNotaDebitoProveedoresRepository());





                Assert.IsTrue(proveedorService.Insert(proveedor1));

                DateTime maxFechaPago;
                DateTime maxFechaCompra;
                decimal cc;


                decimal montocompraa1 = 100;
                decimal montoCompra2 = 50;

                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc == 0);
                Assert.IsTrue(maxFechaPago == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaCompra == HelperDTO.BEGINNING_OF_TIME_DATE);


                DateTime fechaVenta1 = new DateTime(2017, 10, 10);
                DateTime fechaVenta2 = new DateTime(2020, 10, 10);

                var compraProveedoresService = new ComprasProveedorService();
                var compra1 = fixture.Create<ComprasProveedoresData>();

                compra1.Proveedor = proveedor1;
                compra1.Monto = montocompraa1;
                compra1.Descuento = 0;//check this
                compra1.Date = fechaVenta1;
               
                compra1.Enable = true;

                Assert.IsTrue(compraProveedoresService.Insert(compra1));


                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc * -1 == montocompraa1);
                Assert.IsTrue(maxFechaPago == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaCompra == fechaVenta1);




                var compra2 = fixture.Create<ComprasProveedoresData>();

                compra2.Proveedor = proveedor1;
                compra2.Monto = montoCompra2;
                compra2.Descuento = 0;//check this
                compra2.Date = fechaVenta2;
               
                compra2.Enable = true;

                Assert.IsTrue(compraProveedoresService.Insert(compra2));



                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc * -1 == montocompraa1 + montoCompra2);
                Assert.IsTrue(maxFechaPago == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaCompra == fechaVenta2);






                var notaCredito = fixture.Create<NotaData>();

                var montoNotaCredito = 33;

                notaCredito.Monto = montoNotaCredito;
                notaCredito.tercero = proveedor1;
                notaCredito.Enable = true;

                Assert.IsTrue(notaCreditoProveedorService.Insert(notaCredito));



                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc * -1 == montocompraa1 + montoCompra2 - montoNotaCredito);
                Assert.IsTrue(maxFechaPago == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaCompra == fechaVenta2);







                var notaDebito = fixture.Create<NotaData>();

                var montoNotaDebito = 80;

                notaDebito.Monto = montoNotaDebito;
                notaDebito.tercero = proveedor1;
                notaDebito.Enable = true;

                Assert.IsTrue(notaDebitoProveedorService.Insert(notaDebito));



                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc * -1 == montocompraa1 + montoCompra2 - montoNotaCredito + montoNotaDebito);
                Assert.IsTrue(maxFechaPago == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaCompra == fechaVenta2);






                var ordenPagoService = new OrdenPagoService();

                var ordenPago1 = fixture.Create<OrdenPagoData>();

                var montoRecibo = 90;
                var fechaRecibo = new DateTime(2016, 2, 15);
                ordenPago1.Tercero = proveedor1;
                ordenPago1.Enable = true;
                ordenPago1.Monto = montoRecibo;
                ordenPago1.Date = fechaRecibo;

                Assert.IsTrue(ordenPagoService.Insert(ordenPago1));


                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc * -1 == montocompraa1 + montoCompra2 - montoNotaCredito + montoNotaDebito - montoRecibo);
                Assert.IsTrue(maxFechaPago == fechaRecibo);
                Assert.IsTrue(maxFechaCompra == fechaVenta2);





                Assert.IsTrue(ordenPagoService.Disable(ordenPago1));


                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc * -1 == montocompraa1 + montoCompra2 - montoNotaCredito + montoNotaDebito);
                Assert.IsTrue(maxFechaPago == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaCompra == fechaVenta2);




                Assert.IsTrue(notaDebitoProveedorService.Disable(notaDebito));

                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc * -1 == montocompraa1 + montoCompra2 - montoNotaCredito);
                Assert.IsTrue(maxFechaPago == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaCompra == fechaVenta2);



                Assert.IsTrue(notaCreditoProveedorService.Disable(notaCredito));

                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc * -1 == montocompraa1 + montoCompra2);
                Assert.IsTrue(maxFechaPago == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaCompra == fechaVenta2);





                Assert.IsTrue(compraProveedoresService.Disable(compra2.ID,true));

                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc * -1 == montocompraa1);
                Assert.IsTrue(maxFechaPago == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsFalse(maxFechaCompra == fechaVenta2);
                Assert.IsTrue(maxFechaCompra == fechaVenta1);

                Assert.IsTrue(compraProveedoresService.Disable(compra1.ID,true));

                proveedorService.GetCC(proveedor1, out maxFechaPago, out maxFechaCompra, out  cc);

                Assert.IsTrue(cc == 0);
                Assert.IsTrue(maxFechaPago == HelperDTO.BEGINNING_OF_TIME_DATE);
                Assert.IsTrue(maxFechaCompra == HelperDTO.BEGINNING_OF_TIME_DATE);

            }
        }
    }
}
