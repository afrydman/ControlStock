using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Services;
using Services.ValeService;

namespace Testing.Services
{
 [TestClass()]
    public class ValeServiceTest
    {

      private Fixture fixture = new Fixture();

     [TestMethod]
     public void InsertFromVenta()
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

             decimal value = 200;
             var ventaCambio = fixture.Create<VentaData>();

             var valeService = new ValeService();

             ventaCambio.Monto = value*-1;

             int ValeNum =valeService.GenerarNuevo(ventaCambio);


             Assert.IsTrue(ValeNum>-1);

             valeData valeDB = valeService.GetbyVenta(ventaCambio.ID);


             Assert.IsTrue(valeDB.Monto == value);


         }
     }

     [TestMethod]
     public void InsertFromIngreso()
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

             decimal value = 200;
             var Ingreso = fixture.Create<IngresoData>();

             var valeService = new ValeService();

             Ingreso.Monto = value ;

             int ValeNum = valeService.GenerarNuevo(Ingreso);


             Assert.IsTrue(ValeNum > -1);

        


         }
     }

    }
}
