using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Services.ChequeService;
using Services.OrdenPagoService;
using Services.ProveedorService;

namespace Testing.Services
{
    [TestClass()]
    public class OrdenPagoServiceTest
    {
        private Fixture fixture = new Fixture();
        [TestMethod]
        public void GetOrdenQueEntregoChequeTest()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                testList.Add(HelperTesting.ServicesEnum.ChequeService);

                fixture = HelperTesting.SetUp(testList, true);



                var ordenPagoService = new OrdenPagoService();
                var chequeService = new ChequeService();

                var cheque = fixture.Create<ChequeData>();
                var orden = fixture.Create<OrdenPagoData>();
                var cuenta = fixture.Create<CuentaData>();
                
                Guid idchque = cheque.ID;
                Guid idorden = orden.ID;


                ReciboOrdenPagoDetalleData unDetalle = new ReciboOrdenPagoDetalleData();
                unDetalle.Cheque = cheque;
                unDetalle.Cuenta = cuenta;
                unDetalle.FatherID = orden.ID;
                unDetalle.Monto = cheque.Monto;

                orden.Children = new List<ReciboOrdenPagoDetalleData>();
                orden.Children.Add(unDetalle);

                
                Assert.IsTrue(chequeService.Insert(cheque));
                Assert.IsTrue(ordenPagoService.Insert(orden));
                


                OrdenPagoData ordenDB = ordenPagoService.GetOrdenQueEntregoCheque(idchque);

                int difExpected = 2;//cae y cae vto
                List<string> dif = HelperTesting.GetDifferences(orden, ordenDB);

                bool p = (dif == null || dif.Count - difExpected == 0);

                Assert.IsTrue(p);





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
                
                testList.Add(HelperTesting.ServicesEnum.ProveedorService);

                fixture = HelperTesting.SetUp(testList, true);



                var ordenPagoService = new OrdenPagoService();
                var proveedorService = new ProveedorService();

                var proveedor = fixture.Create<ProveedorData>();
                var orden = fixture.Create<OrdenPagoData>();
                
                orden.Children.ForEach(data => data.Cheque=new ChequeData());
                Guid idProvedor = proveedor.ID;
                orden.Tercero = proveedor;


                Assert.IsTrue(proveedorService.Insert(proveedor));
                Assert.IsTrue(ordenPagoService.Insert(orden));



                List<OrdenPagoData> ordenDB = ordenPagoService.GetbyProveedor(idProvedor);

                Assert.IsTrue(ordenDB!=null && ordenDB.Count==1);

                int difExpected = 2;//cae y cae vto
                List<string> dif = HelperTesting.GetDifferences(orden, ordenDB[0]);

                bool p = (dif == null || dif.Count - difExpected == 0);

                Assert.IsTrue(p);





            }
        }

    }
}
