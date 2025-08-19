using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Services;
using Services.FormaPagoService;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Testing.Services
{

    [TestClass()]
    public class FormaPagoServiceTest
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


                FormaPagoData formaDebito = new FormaPagoData();
                FormaPagoData formaAux = new FormaPagoData();



                FormaPagoService formaPagoService = new FormaPagoService();

                Guid idDebito = Guid.NewGuid();


                //test una       NO     tarjeta de credito
                formaDebito.Description = "debito 1";
                formaDebito.Enable = true;
                formaDebito.Credito = false;
                formaDebito.ID = idDebito;


                Assert.IsTrue(formaPagoService.Insert(formaDebito));

                int difExpected = 0;
                bool p = false;
                formaAux = formaPagoService.GetByID(idDebito);


                List<string> dif = HelperTesting.GetDifferences(formaDebito, formaAux);

                p = (dif == null || dif.Count - difExpected == 0);

                Assert.IsTrue(p);




                Guid idCredito = Guid.NewGuid();


                //test una            tarjeta de credito

                FormaPagoData formaCredito = new FormaPagoData();
                formaCredito.Description = "credito 1";
                formaCredito.Enable = true;
                formaCredito.Credito = true;
                formaCredito.ID = idCredito;

                formaCredito.Cuotas = fixture.Create<List<FormaPagoCuotaData>>();


                Assert.IsTrue(formaPagoService.Insert(formaCredito));

                formaAux = formaPagoService.GetByID(idCredito);


                dif = HelperTesting.GetDifferences(formaCredito, formaAux);

                p = (dif == null || dif.Count - difExpected == 0);

                Assert.IsTrue(p);
            }

        }

        [TestMethod]
        public void Disable_Test()
        {
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {


                List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
                fixture = HelperTesting.SetUp(testList, true);


                FormaPagoData formaDebito = new FormaPagoData();
                FormaPagoData formaAux = new FormaPagoData();



                FormaPagoService formaPagoService = new FormaPagoService();

                Guid idDebito = Guid.NewGuid();


                //test una       NO     tarjeta de credito
                formaDebito.Description = "debito 1";
                formaDebito.Enable = true;
                formaDebito.Credito = false;
                formaDebito.ID = idDebito;


                Assert.IsTrue(formaPagoService.Insert(formaDebito));


                Assert.IsTrue(formaPagoService.Disable(formaDebito));


                int difExpected = 0;
                bool p = false;


                formaAux = formaPagoService.GetByID(idDebito);
                Assert.IsTrue(formaAux.Enable == false);


                formaAux.Enable = !formaAux.Enable; // lo doy vuelta
                List<string> dif = HelperTesting.GetDifferences(formaDebito, formaAux);

                p = (dif == null || dif.Count - difExpected == 0);

                Assert.IsTrue(p);
            }

        }


        [TestMethod]
        public void Enable_Test()
        {


            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, true);


            FormaPagoData formaDebito = new FormaPagoData();
            FormaPagoData formaAux = new FormaPagoData();

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {


                FormaPagoService formaPagoService = new FormaPagoService();

                Guid idDebito = Guid.NewGuid();


                //test una       NO     tarjeta de credito
                formaDebito.Description = "debito 1";
                formaDebito.Enable = false;
                formaDebito.Credito = false;
                formaDebito.ID = idDebito;


                Assert.IsTrue(formaPagoService.Insert(formaDebito));


                Assert.IsTrue(formaPagoService.Enable(formaDebito));

                int difExpected = 0;
                bool p = false;


                formaAux = formaPagoService.GetByID(idDebito);
                Assert.IsTrue(formaAux.Enable == true);

                formaAux.Enable = !formaAux.Enable; // lo doy vuelta
                List<string> dif = HelperTesting.GetDifferences(formaDebito, formaAux);

                p = (dif == null || dif.Count - difExpected == 0);

                Assert.IsTrue(p);
            }
        }


        [TestMethod]
        public void GetAll_Test()
        {

            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            testList.Add(HelperTesting.ServicesEnum.FormaPagoService);
            fixture = HelperTesting.SetUp(testList, true);
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {


                List<FormaPagoData> formas = new List<FormaPagoData>();

                formas = fixture.Create<List<FormaPagoData>>();
                ;
                

                FormaPagoService formaPagoService = new FormaPagoService();

                List<FormaPagoData> formasPre = formaPagoService.GetAll(false);

                foreach (var fp in formas)
                {
                    Assert.IsTrue(formaPagoService.Insert(fp));
                }




                List<FormaPagoData> Gets = formaPagoService.GetAll(false);


                formas.AddRange(formasPre);

                formas.Sort((x, y) => x.ID.CompareTo(y.ID));
                
                Gets.Sort((x, y) => x.ID.CompareTo(y.ID));



                Assert.IsTrue(formas.Count == Gets.Count);//saco tmb la tarjeta


                int difExpected = 0;
                List<string> dif;
                bool p;
                for (int i = 0; i < formas.Count-1; i++)
                {
                    dif = HelperTesting.GetDifferences(formas[i], Gets[i]);
                    p = (dif == null || dif.Count - difExpected == 0);
                    Assert.IsTrue(p);
                }

            }


        }

        [TestMethod]
        public void Update_Test()
        {


            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, true);


            FormaPagoData formaDebito = new FormaPagoData();
            FormaPagoData formaAux = new FormaPagoData();

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {


                FormaPagoService formaPagoService = new FormaPagoService();

                Guid idDebito = Guid.NewGuid();


                //test una       NO     tarjeta de credito
                formaDebito.Description = "debito 1";
                formaDebito.Enable = false;
                formaDebito.Credito = false;
                formaDebito.ID = idDebito;


                Assert.IsTrue(formaPagoService.Insert(formaDebito));

                formaDebito.Description = "aaaaaaaaaaaaaaaax";
                formaDebito.Credito = true;
                formaDebito.Cuotas = new List<FormaPagoCuotaData>();
                

                

                Assert.IsTrue(formaPagoService.Update(formaDebito));

                int difExpected = 0;
                bool p = false;


                formaAux = formaPagoService.GetByID(idDebito);
                

                
                List<string> dif = HelperTesting.GetDifferences(formaDebito, formaAux);

                p = (dif == null || dif.Count - difExpected == 0);

                Assert.IsTrue(p);
            }
        }




        /// 1- inserto una tarjeta de credito y despues le cambio los valores.
        /// 2- inserto un debito y despues lo paso a credito con valores.
        /// 3- inserto credito, lo actualizo a debito y despues a credito.
            
        [TestMethod]
        public void Update_Aumentos_Caso_1_Test()
        {



            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, true);


            
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {



           
                FormaPagoData tarjetaCredito = new FormaPagoData();
                FormaPagoData tarjetaCreditoGet = new FormaPagoData();
               

                FormaPagoService formaPagoService = new FormaPagoService();

                Guid idCredito = Guid.NewGuid();
                List<string> dif;

                //test una       NO     tarjeta de credito
                tarjetaCredito.Description = "Credito 1";
                tarjetaCredito.Enable = true;
                tarjetaCredito.Credito = true;
                tarjetaCredito.ID = idCredito;

                tarjetaCredito.Cuotas = fixture.Create<List<FormaPagoCuotaData>>();



                Assert.IsTrue(formaPagoService.Insert(tarjetaCredito));

                tarjetaCreditoGet = formaPagoService.GetByID(idCredito);
                
                int difExpected = 0;
                bool p = false;

                dif = HelperTesting.GetDifferences(tarjetaCredito, tarjetaCreditoGet);
               
                p = (dif == null || dif.Count - difExpected == 0);

                Assert.IsTrue(p);



                tarjetaCredito.Cuotas.ForEach(data => data.Aumento = data.Aumento*2 );


                Assert.IsTrue(formaPagoService.Update(tarjetaCredito));
                tarjetaCreditoGet = formaPagoService.GetByID(idCredito);

                dif = HelperTesting.GetDifferences(tarjetaCredito, tarjetaCreditoGet);

                p = (dif == null || dif.Count - difExpected == 0);

                Assert.IsTrue(p);

       

            }

        }

        [TestMethod]
        public void Update_Aumentos_Caso_2_Test()
        {



            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, true);



            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {




                FormaPagoData tarjetaCredito = new FormaPagoData();
                FormaPagoData tarjetaCreditoGet = new FormaPagoData();


                FormaPagoService formaPagoService = new FormaPagoService();

                Guid idCredito = Guid.NewGuid();
                List<string> dif;

                //test una       NO     tarjeta de credito
                tarjetaCredito.Description = "Credito 1";
                tarjetaCredito.Enable = true;
                tarjetaCredito.Credito = false;
                tarjetaCredito.ID = idCredito;

                //tarjetaCredito.cuotas = null;


                Assert.IsTrue(formaPagoService.Insert(tarjetaCredito));

                tarjetaCreditoGet = formaPagoService.GetByID(idCredito);


                int difExpected = 0;
                bool p = false;

                
                dif = HelperTesting.GetDifferences(tarjetaCredito, tarjetaCreditoGet);

                p = (dif == null || dif.Count - difExpected == 0);

                
                Assert.IsTrue(p);




                tarjetaCredito.Cuotas = fixture.Create<List<FormaPagoCuotaData>>();
                tarjetaCredito.Credito = true;


                Assert.IsTrue(formaPagoService.Update(tarjetaCredito));
                
                tarjetaCreditoGet = formaPagoService.GetByID(idCredito);

                dif = HelperTesting.GetDifferences(tarjetaCredito, tarjetaCreditoGet);

                p = (dif == null || dif.Count - difExpected == 0);

                Assert.IsTrue(p);



            }

        }

        [TestMethod]
        public void Update_Aumentos_Caso_3_Test()
        {



            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, true);



            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {




                FormaPagoData tarjetaCredito = new FormaPagoData();
                FormaPagoData tarjetaCreditoGet = new FormaPagoData();


                FormaPagoService formaPagoService = new FormaPagoService();

                Guid idCredito = Guid.NewGuid();
                List<string> dif;

                //test una       NO     tarjeta de credito
                tarjetaCredito.Description = "Credito 1";
                tarjetaCredito.Enable = true;
                tarjetaCredito.Credito = true;
                tarjetaCredito.ID = idCredito;

                tarjetaCredito.Cuotas = fixture.Create<List<FormaPagoCuotaData>>(); 


                Assert.IsTrue(formaPagoService.Insert(tarjetaCredito));

                tarjetaCreditoGet = formaPagoService.GetByID(idCredito);


                int difExpected = 0;
                bool p = false;


                dif = HelperTesting.GetDifferences(tarjetaCredito, tarjetaCreditoGet);

                p = (dif == null || dif.Count - difExpected == 0);


                Assert.IsTrue(p);




                tarjetaCredito.Cuotas = new List<FormaPagoCuotaData>();// fixture.Create<List<FormaPagoCuotaData>>();
                tarjetaCredito.Credito = false;


                Assert.IsTrue(formaPagoService.Update(tarjetaCredito));

                tarjetaCreditoGet = formaPagoService.GetByID(idCredito);

                dif = HelperTesting.GetDifferences(tarjetaCredito, tarjetaCreditoGet);

                p = (dif == null || dif.Count - difExpected == 0);

                Assert.IsTrue(p);





                tarjetaCredito.Cuotas =  fixture.Create<List<FormaPagoCuotaData>>();
                tarjetaCredito.Credito = true;

                Assert.IsTrue(formaPagoService.Update(tarjetaCredito));

                tarjetaCreditoGet = formaPagoService.GetByID(idCredito);

                dif = HelperTesting.GetDifferences(tarjetaCredito, tarjetaCreditoGet);

                p = (dif == null || dif.Count - difExpected == 0);

                Assert.IsTrue(p);
            }

        }


    }
}
