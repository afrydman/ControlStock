using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Services;
using Services.FormaPagoService;
using Services.VentaService;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Testing.Services
{
    [TestClass()]
    public class VentaServiceTest
    {

         private Fixture fixture = new Fixture();

        [TestMethod]
         public void getCuentaCorrientebyClienteTest()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.Codigo);
                
                fixture = HelperTesting.SetUp(testList, true);

                var ventaService = new VentaService();
                

                var venta = fixture.Create<VentaData>();
                var cliente = fixture.Create<ClienteData>();

                venta.Pagos = new List<PagoData>();
                PagoData p = new PagoData();

                p.FormaPago = new FormaPagoService().GetByID(HelperService.idCC);

                p.Importe = venta.Monto;

                venta.Pagos.Add(p);
                venta.Cliente = cliente;

                Assert.IsTrue(ventaService.Insert(venta));


                List<VentaData> ventaDB = ventaService.getCuentaCorrientebyCliente(cliente.ID);


                Assert.IsTrue(ventaDB!=null&& ventaDB.Count==1);
                bool pe;
                int difExpected = 0;//pagos y tributo por ser childrens loquillos
                List<string> dif = HelperTesting.GetDifferences(venta, ventaDB[0]);

                pe = (dif == null || dif.Count - difExpected == 0);

                Assert.IsTrue(pe);

            }
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void transactionIsWorking()
        {

            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.Codigo);

            fixture = HelperTesting.SetUp(testList, true);

            var ventaService = new VentaService();

            Guid idventa = Guid.NewGuid();
            
            var venta = fixture.Create<VentaData>();
            venta.ID = idventa;
            venta.Cliente = fixture.Create<ClienteData>();
            venta.Pagos = new List<PagoData>();
            
            PagoData p = new PagoData();



            p.FormaPago = null;//para que explote...

            p.Importe = venta.Monto;
            
            venta.Pagos.Add(p);


            venta.Numero = 6666;
            Assert.IsFalse(ventaService.Insert(venta));

            //Assert.(ventaService.GetByID(idventa).Date==DTO.HelperDTO.BEGINNING_OF_TIME_DATE);





        }
    }
}
