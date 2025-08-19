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

namespace Testing.Services
{
    [TestClass()]

    public class ChequeServiceTest
    {

        private Fixture fixture = new Fixture();

        [TestMethod]
        public void GetNextNumberAvailableTest() //Testeo internos
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                
                fixture = HelperTesting.SetUp(testList, true);

                var chequeService = new ChequeService();
                ChequeData cheque = fixture.Create <ChequeData>();


                int primero = chequeService.GetNextNumberAvailable();
                cheque.Interno = primero;
                cheque.Enable = true;


                Assert.IsTrue(chequeService.Insert(cheque));


                Assert.IsTrue(chequeService.GetNextNumberAvailable()==primero+1);


            }
        }

        [TestMethod]
        public void GetByChequeraTest() 
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();

                fixture = HelperTesting.SetUp(testList, true);

                var chequeService = new ChequeService();
                List<ChequeData> cheques = fixture.Create<List<ChequeData>>();
                
                Guid idChequera = cheques[0].Chequera.ID;//debe de ser el mismo id para todos


                List<ChequeData> chequesPre = chequeService.GetByChequera(idChequera, false);
                foreach (var c in cheques)
                {
                    c.Interno = 0;
                    Assert.IsTrue(chequeService.Insert(c));    
                }

                List<ChequeData> chequesGet = chequeService.GetByChequera(idChequera, false);


                Assert.IsTrue(chequesGet.Count == cheques.Count+chequesPre.Count);





            }

        }


        [TestMethod]
        public void GetByTerceroTest()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();

                fixture = HelperTesting.SetUp(testList, true);

                var chequeService = new ChequeService();
                List<ChequeData> cheques = fixture.Create<List<ChequeData>>();

                Guid idChequera = cheques[0].Chequera.ID;//debe de ser el mismo id para todos


                List<ChequeData> chequesPre = chequeService.GetByChequera(Guid.Empty, false);
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.Interno = 0;
                    Assert.IsTrue(chequeService.Insert(c));
                }

                List<ChequeData> chequesGet = chequeService.GetChequesTercero(false);


                Assert.IsTrue(chequesGet.Count == cheques.Count + chequesPre.Count);





            }

        }

        [TestMethod]
        public void InternNumberIsValidTest()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();

                fixture = HelperTesting.SetUp(testList, true);

                var chequeService = new ChequeService();
                List<ChequeData> cheques = fixture.Create<List<ChequeData>>();

                Guid idChequera = cheques[0].Chequera.ID;//debe de ser el mismo id para todos
                int primerInterno = chequeService.GetNextNumberAvailable();

                List<ChequeData> chequesPre = chequeService.GetByChequera(Guid.Empty, false);
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }


                int nextInterno = chequeService.GetNextNumberAvailable();
                Assert.IsTrue(primerInterno + 10 == nextInterno);


            }

        }



        [TestMethod]
        public void GetChequesUtilizablesTest()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();

                fixture = HelperTesting.SetUp(testList, true);

                var chequeService = new ChequeService();
                List<ChequeData> cheques = fixture.Create<List<ChequeData>>();

                Guid idChequera = cheques[0].Chequera.ID;//debe de ser el mismo id para todos
                int primerInterno = chequeService.GetNextNumberAvailable();

                List<ChequeData> chequesPre = chequeService.GetByChequera(Guid.Empty, false);
                List<ChequeData> chequesUtilizablesPre = chequeService.GetChequesUtilizables(true);
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque=EstadoCheque.EnCartera;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }//10 en cartera de tercero
                
                
                cheques = fixture.Create<List<ChequeData>>();
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque = EstadoCheque.Acreditado;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }

                cheques = fixture.Create<List<ChequeData>>();
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque = EstadoCheque.Anulado;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }

                cheques = fixture.Create<List<ChequeData>>();
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque = EstadoCheque.Creado;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }

                cheques = fixture.Create<List<ChequeData>>();
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque = EstadoCheque.Depositado;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }

                cheques = fixture.Create<List<ChequeData>>();
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque = EstadoCheque.Entregado;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }

                cheques = fixture.Create<List<ChequeData>>();
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque = EstadoCheque.EntregadoSinOpago;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }

                cheques = fixture.Create<List<ChequeData>>();
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque = EstadoCheque.Rechazado;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }

                
                
                List<ChequeData> chequesDB = chequeService.GetChequesUtilizables(true);
                Assert.IsTrue(chequesDB.Count - chequesUtilizablesPre.Count== 10);




                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque = EstadoCheque.EnCartera;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }//10 en cartera de tercero


                cheques = fixture.Create<List<ChequeData>>();
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque = EstadoCheque.Acreditado;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }

                cheques = fixture.Create<List<ChequeData>>();
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque = EstadoCheque.Anulado;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }

                cheques = fixture.Create<List<ChequeData>>();
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque = EstadoCheque.Creado;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }

                cheques = fixture.Create<List<ChequeData>>();
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque = EstadoCheque.Depositado;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }

                cheques = fixture.Create<List<ChequeData>>();
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque = EstadoCheque.Entregado;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }

                cheques = fixture.Create<List<ChequeData>>();
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque = EstadoCheque.EntregadoSinOpago;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }

                cheques = fixture.Create<List<ChequeData>>();
                foreach (var c in cheques)
                {
                    c.Chequera.ID = Guid.Empty;
                    c.EstadoCheque = EstadoCheque.Rechazado;
                    c.Interno = 0;
                    c.Enable = true;
                    Assert.IsTrue(chequeService.Insert(c));
                }
                chequesDB = chequeService.GetChequesUtilizables(true);
                Assert.IsTrue(chequesDB.Count - chequesUtilizablesPre.Count == 20);

            }

        }


        [TestMethod]
        public void MarcarComoTest()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();

                fixture = HelperTesting.SetUp(testList, true);

                var chequeService = new ChequeService();
                ChequeData cheque = fixture.Create<ChequeData>();

                
                int primerInterno = chequeService.GetNextNumberAvailable();

                var estado1 = EstadoCheque.Acreditado;
                var estado2 = EstadoCheque.EntregadoSinOpago;
                cheque.EstadoCheque = estado1;
                

                Assert.IsTrue(chequeService.Insert(cheque));

                Assert.IsTrue(chequeService.MarcarComo(cheque,estado2,null,"nueva obs"));
                

                ChequeData chequeDB = chequeService.GetByID(cheque.ID);
                Assert.IsTrue(chequeDB.EstadoCheque == estado2);

                int difExpected = 0;//la fecha deberia ser, pero la pisa cuando marca.
                bool p;

                List<string> dif = HelperTesting.GetDifferences(cheque, chequeDB);

                p = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(p);
                
                
                
                
                


            }

        }
    }
}
