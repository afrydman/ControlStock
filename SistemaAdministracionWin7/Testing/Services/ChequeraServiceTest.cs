using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Services.ChequeraService;
using Services.ChequeService;

namespace Testing.Services
{
     [TestClass()]
    public class ChequeraServiceTest
    {
         private Fixture fixture = new Fixture();
         [TestMethod]
         public void GetNextNumberAvailableTest()//Testeo que retorne el Codigo de chequera correctamente
         {
              var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

             using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
             {

                 List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                 testList.Add(HelperTesting.ServicesEnum.ChequeraService);
                 fixture = HelperTesting.SetUp(testList, true);


                 var chequeraService = new ChequeraService();
                 ChequeraData chequera = fixture.Create<ChequeraData>();

                 chequera.Enable = true;
                 int primeroInterno = Convert.ToInt32(chequeraService.GetNextNumberAvailable());
                 Guid primeroID = chequera.ID;
                 chequera.CodigoInterno = Convert.ToInt32(chequeraService.GetNextNumberAvailable());

                 Assert.IsTrue(chequeraService.Insert(chequera));

                 
                 
                 
                 chequera = fixture.Create<ChequeraData>();
                 chequera.Enable = true;
                 chequera.CodigoInterno = Convert.ToInt32(chequeraService.GetNextNumberAvailable());


                 Assert.IsTrue(chequera.CodigoInterno == primeroInterno + 1);
                 Assert.IsTrue(chequeraService.Insert(chequera));


                 chequera = fixture.Create<ChequeraData>();
                 chequera.Enable = true;
                 chequera.CodigoInterno = Convert.ToInt32(chequeraService.GetNextNumberAvailable());


                 Assert.IsTrue(chequera.CodigoInterno == primeroInterno + 2);
                 Assert.IsTrue(chequeraService.Insert(chequera));






             }


         }


         [TestMethod]
         public void existeEsteChequeTest()
         {//(Guid idChequera, string numeroVerificar)


             var opts = new TransactionOptions
             {
                 IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
             };

             using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
             {

                 List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                 testList.Add(HelperTesting.ServicesEnum.ChequeraService);
                 testList.Add(HelperTesting.ServicesEnum.ChequeService);
                 fixture = HelperTesting.SetUp(testList, true);

                 string numero = "12333";

                 var chequeraService = new ChequeraService();
                 var chequeService = new ChequeService();
                 
                 ChequeraData chequera = fixture.Create<ChequeraData>();

                 Guid chequeraid = chequera.ID;

                 Assert.IsTrue(chequeraService.Insert(chequera));


                 ChequeData c = fixture.Create<ChequeData>();
                 c.Numero = numero;
                 c.Chequera = chequera;
                 c.Enable = true;
                 
                 Assert.IsFalse(chequeraService.existeEsteCheque(chequera.ID,numero));
                 
                 Assert.IsTrue(chequeService.Insert(c));

                 Assert.IsTrue(chequeraService.existeEsteCheque(chequera.ID, numero));


                 Assert.IsTrue(chequeService.Disable(c.ID));

                 Assert.IsFalse(chequeraService.existeEsteCheque(chequera.ID, numero));


             }



         }

         [TestMethod]
         public void SetearSiguienteTest()
         {
             //(ChequeraData chequera)
             
             var opts = new TransactionOptions
             {
                 IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
             };

             using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
             {

                 List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                 testList.Add(HelperTesting.ServicesEnum.ChequeraService);
                 testList.Add(HelperTesting.ServicesEnum.ChequeService);
                 fixture = HelperTesting.SetUp(testList, true);

                 

                 var chequeraService = new ChequeraService();
                 var chequeService = new ChequeService();

                 ChequeraData chequera = fixture.Create<ChequeraData>();

                 Guid chequeraid = chequera.ID;

                 string primero = "1";
                 chequera.Primero = primero;
                 chequera.Ultimo = "10";
                 chequera.Siguiente = primero;

                 Assert.IsTrue(chequeraService.Insert(chequera));


                 ChequeData c = fixture.Create<ChequeData>();
                 c.Numero = chequera.Siguiente;
                 c.Chequera = chequera;
                 c.Enable = true;


                 Assert.IsTrue(chequeService.Insert(c));

                 Assert.IsTrue(chequeraService.SetearSiguiente(chequera));

                 chequera = chequeraService.GetByID(chequera.ID);


                 Assert.IsTrue(Convert.ToInt32(chequera.Siguiente)==Convert.ToInt32(primero)+1);
                 


             }




         }



    }
}
