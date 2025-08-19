using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Services.FormaPagoService;
using Services.PagoService;

namespace Testing.Services
{

     [TestClass()]
    public class PagoServiceTest
    {
         private Fixture fixture = new Fixture();

         [TestMethod]
         public void InsertAndGet()
         {

             List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
             fixture = HelperTesting.SetUp(testList, true);

             FormaPagoService formaPagoService = new FormaPagoService();
             PagoService pagoService = new PagoService();

             PagoData pago;
             List<PagoData> pagoAux;

             var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

             using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
             {

                 FormaPagoData formaCredito = formaPagoService.GetByID(new Guid("4C338B5D-CF54-4B1B-BFDE-9AE0DD93918E"));

                 FormaPagoData formaDebito = formaPagoService.GetByID(new Guid("DB4EE880-ABA0-469D-A9BC-FE2B32FCABBA"));

                 Guid idVenta1 = Guid.NewGuid();
                 Guid idVenta2 = Guid.NewGuid();

                 //pago efectivo
                 pago = new PagoData();
                 pago.FormaPago = formaDebito;
                 pago.Importe = 20;
                 pago.FatherID = idVenta1;
                 Assert.IsTrue(pagoService.InsertDetalle(pago));





                 pagoAux = pagoService.GetDetalles(idVenta1);


                 Assert.IsTrue(pagoAux != null && pagoAux.Count == 1);

                 int difExpected = 0;
                 bool p = false;



                 List<string> dif = HelperTesting.GetDifferences(pago, pagoAux[0]);

                 p = (dif == null || dif.Count - difExpected == 0);

                 Assert.IsTrue(p);



                 //pago tarjeta

                 int cuotas;

                 cuotas = 2;
                 pago = new PagoData();
                 pago.FormaPago = formaCredito;
                 pago.Importe = 20;
                 pago.Cuotas = cuotas;
                 pago.Cupon = "aaaa";
                 pago.Lote = "bbbb";
                 pago.Recargo = formaCredito.Cuotas[cuotas].Aumento;
                 pago.FatherID = idVenta2;

                 Assert.IsTrue(pagoService.InsertDetalle(pago));

                 pagoAux = pagoService.GetDetalles(idVenta2);


                 Assert.IsTrue(pagoAux != null && pagoAux.Count == 1);


                 difExpected = 1;
                 dif = HelperTesting.GetDifferences(pago, pagoAux[0]);

                 p = (dif == null || dif.Count - difExpected == 0);

                 Assert.IsTrue(p);
             }

         }




         [TestMethod]
         public void GetByTipo()
         {
             List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
             fixture = HelperTesting.SetUp(testList, true);

             FormaPagoService formaPagoService = new FormaPagoService();
             PagoService pagoService = new PagoService();

             PagoData pago;
             List<PagoData> pagoAux;


             var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

             using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
             {
                 FormaPagoData formaCredito = formaPagoService.GetByID(new Guid("4C338B5D-CF54-4B1B-BFDE-9AE0DD93918E"));

                 FormaPagoData formaDebito = formaPagoService.GetByID(new Guid("DB4EE880-ABA0-469D-A9BC-FE2B32FCABBA"));

                 Guid idVenta1 = Guid.NewGuid();
                 Guid idVenta2 = Guid.NewGuid();


                 List<PagoData> pagosDebitoPre = pagoService.GetPagosByTipo(formaDebito.ID);
                 List<PagoData> pagosCreditoPre = pagoService.GetPagosByTipo(formaCredito.ID);

                 //pago efectivo
                 pago = new PagoData();
                 pago.FormaPago = formaDebito;
                 pago.Importe = 20;
                 pago.FatherID = idVenta1;
                 Assert.IsTrue(pagoService.InsertDetalle(pago));
                 pago = new PagoData();
                 pago.FormaPago = formaDebito;
                 pago.Importe = 20;
                 pago.FatherID = idVenta1;
                 Assert.IsTrue(pagoService.InsertDetalle(pago));

                 pago = new PagoData();
                 pago.FormaPago = formaDebito;
                 pago.Importe = 20;
                 pago.FatherID = idVenta1;
                 Assert.IsTrue(pagoService.InsertDetalle(pago));

                 //son 3

                 int cuotas;

                 cuotas = 4;
                 pago = new PagoData();
                 pago.FormaPago = formaCredito;
                 pago.Importe = 20;
                 pago.Cuotas = cuotas;
                 pago.Cupon = "aaaa";
                 pago.Lote = "bbbb";
                 pago.Recargo = formaCredito.Cuotas[cuotas].Aumento;
                 pago.FatherID = idVenta2;

                 Assert.IsTrue(pagoService.InsertDetalle(pago));



                 cuotas = 5;
                 pago = new PagoData();
                 pago.FormaPago = formaCredito;
                 pago.Importe = 20;
                 pago.Cuotas = cuotas;
                 pago.Cupon = "aaaa";
                 pago.Lote = "bbbb";
                 pago.Recargo = formaCredito.Cuotas[cuotas].Aumento;
                 pago.FatherID = idVenta2;

                 Assert.IsTrue(pagoService.InsertDetalle(pago));


                 cuotas = 6;
                 pago = new PagoData();
                 pago.FormaPago = formaCredito;
                 pago.Importe = 20;
                 pago.Cuotas = cuotas;
                 pago.Cupon = "aaaa";
                 pago.Lote = "bbbb";
                 pago.Recargo = formaCredito.Cuotas[cuotas].Aumento;
                 pago.FatherID = idVenta2;

                 Assert.IsTrue(pagoService.InsertDetalle(pago));

                 cuotas = 7;
                 pago = new PagoData();
                 pago.FormaPago = formaCredito;
                 pago.Importe = 20;
                 pago.Cuotas = cuotas;
                 pago.Cupon = "aaaa";
                 pago.Lote = "bbbb";
                 pago.Recargo = formaCredito.Cuotas[cuotas].Aumento;
                 pago.FatherID = idVenta2;

                 Assert.IsTrue(pagoService.InsertDetalle(pago));

                 List<PagoData> pagosAux1 = pagoService.GetPagosByTipo(formaDebito.ID);

                 Assert.IsTrue(pagosAux1 != null);

                 Assert.IsTrue(pagosAux1.Count-pagosDebitoPre.Count == 3);


                 List<PagoData> pagosAux2 = pagoService.GetPagosByTipo(formaCredito.ID);

                 Assert.IsTrue(pagosAux2 != null);

                 Assert.IsTrue(pagosAux2.Count - pagosCreditoPre.Count == 4);
             }

         }

    }
}
