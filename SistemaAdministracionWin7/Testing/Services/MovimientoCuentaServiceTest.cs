using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using DTO;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Services;
using Services.ChequeService;
using Services.CuentaService;
using Services.IngresoService;
using Services.MovimientoCuentaService;
using Services.RetiroService;

namespace Testing.Services
{
    [TestClass]
    public class MovimientoCuentaServiceTest
    {
        private Fixture fixture = new Fixture();

        [TestMethod]
        public void TestInsertMovimientoPagoCheque()
        {
            var movimientoService = new MovimientoCuentaService();
            var testList = new List<HelperTesting.ServicesEnum>();

            testList.Add(HelperTesting.ServicesEnum.Codigo);
            
            fixture = HelperTesting.SetUp(testList, true);

            var opts = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var movimiento = new MovimientoCuentaData();

                Guid id = Guid.NewGuid();
                var chequeService = new ChequeService();

                movimiento = FactoryTesting.GetMovimientoPagoCheque();


                EstadoCheque estadoChequePreInsert = movimiento.cheque.EstadoCheque;
                decimal saldoCheque = movimiento.cheque.Monto;
                decimal saldoCuentaOrigen = movimiento.cuentaOrigen.Saldo;
                
                
                movimiento.ID = id;
                

                Assert.IsTrue(movimientoService.Insert(movimiento));
               
                
                MovimientoCuentaData object2 = movimientoService.GetById(id);
               
                EstadoCheque estadoChequePostInsert = object2.cheque.EstadoCheque;
                
                //ver que differencias me interesan saber, porque el insert genera un cambio de cuenta( saldo) y cheque ( estado)
             

              
                List<string> dif = HelperTesting.GetDifferences(movimiento.Personal, object2.Personal, getChildrensDiff: true);
                int difExpected = 0;
                bool p = (dif == null || dif.Count - difExpected == 0);

                dif = HelperTesting.GetDifferences(movimiento.Local, object2.Local, getChildrensDiff: false);
                difExpected = 0;
                 p = (dif == null || dif.Count - difExpected == 0);
                
                 dif = HelperTesting.GetDifferences(movimiento.cheque, object2.cheque, getChildrensDiff: true);
                 difExpected = 1;//el estado
                 p = (dif == null || dif.Count - difExpected == 0);

                Assert.AreEqual(object2.cheque.EstadoCheque,EstadoCheque.Acreditado);

                 dif = HelperTesting.GetDifferences(movimiento.cuentaDestino, object2.cuentaDestino, getChildrensDiff: true);
                 difExpected = 1;//el saldo
                 p = (dif == null || dif.Count - difExpected == 0);



                Assert.AreEqual(estadoChequePostInsert,EstadoCheque.Acreditado);
                Assert.AreEqual(object2.cuentaOrigen.Saldo, saldoCuentaOrigen-saldoCheque);

                Assert.IsTrue(p);
            }
        }


        [TestMethod]
        public void TestDisableMovimientoPagoCheque()
        {
            var movimientoService = new MovimientoCuentaService();
            var testList = new List<HelperTesting.ServicesEnum>();

            testList.Add(HelperTesting.ServicesEnum.Codigo);

            fixture = HelperTesting.SetUp(testList, true);

            var opts = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var movimiento = new MovimientoCuentaData();

                Guid id = Guid.NewGuid();
                

                movimiento = FactoryTesting.GetMovimientoPagoCheque();


                EstadoCheque estadoChequePreInsert = movimiento.cheque.EstadoCheque;
                decimal saldoCheque = movimiento.cheque.Monto;
                decimal saldoCuentaOrigen = movimiento.cuentaOrigen.Saldo;


                movimiento.ID = id;


                Assert.IsTrue(movimientoService.Insert(movimiento));


                MovimientoCuentaData object2 = movimientoService.GetById(id);

                EstadoCheque estadoChequePostInsert = object2.cheque.EstadoCheque;

                //ver que differencias me interesan saber, porque el insert genera un cambio de cuenta( saldo) y cheque ( estado)



                List<string> dif = HelperTesting.GetDifferences(movimiento.Personal, object2.Personal,
                    getChildrensDiff: true);
                int difExpected = 0;
                bool p = (dif == null || dif.Count - difExpected == 0);

                dif = HelperTesting.GetDifferences(movimiento.Local, object2.Local, getChildrensDiff: false);
                difExpected = 0;
                p = (dif == null || dif.Count - difExpected == 0);

                dif = HelperTesting.GetDifferences(movimiento.cheque, object2.cheque, getChildrensDiff: true);
                difExpected = 1; //el estado
                p = (dif == null || dif.Count - difExpected == 0);

                Assert.AreEqual(object2.cheque.EstadoCheque, EstadoCheque.Acreditado);

                dif = HelperTesting.GetDifferences(movimiento.cuentaDestino, object2.cuentaDestino,
                    getChildrensDiff: true);
                difExpected = 1; //el saldo
                p = (dif == null || dif.Count - difExpected == 0);



                Assert.AreEqual(estadoChequePostInsert, EstadoCheque.Acreditado);
                Assert.AreEqual(object2.cuentaOrigen.Saldo, saldoCuentaOrigen - saldoCheque);

                Assert.IsTrue(p);




                Assert.IsTrue(movimientoService.Disable(id));


                object2 = movimientoService.GetById(id);

                Assert.AreEqual(object2.cheque.EstadoCheque,EstadoCheque.Entregado);

                Assert.AreEqual(object2.cuentaOrigen.Saldo, saldoCuentaOrigen);

                Assert.AreEqual(object2.Enable,!movimiento.Enable);


            }
        }


        [TestMethod]
        public void TestInsertMovimientoCobroCheque()
        {
            var movimientoService = new MovimientoCuentaService();
            var testList = new List<HelperTesting.ServicesEnum>();

            testList.Add(HelperTesting.ServicesEnum.Codigo);

            fixture = HelperTesting.SetUp(testList, true);

            var opts = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var movimiento = new MovimientoCuentaData();

                Guid id = Guid.NewGuid();
                var chequeService = new ChequeService();

                movimiento = FactoryTesting.GetMovimientoCobroCheque();

                decimal saldoCheque = movimiento.cheque.Monto;
                decimal saldoCuentaDestino = movimiento.cuentaDestino.Saldo;


                movimiento.ID = id;


                Assert.IsTrue(movimientoService.Insert(movimiento));


                MovimientoCuentaData object2 = movimientoService.GetById(id);

                //ver que differencias me interesan saber, porque el insert genera un cambio de cuenta( saldo) y cheque ( estado)

                List<string> dif = HelperTesting.GetDifferences(movimiento.Personal, object2.Personal, getChildrensDiff: true);
                int difExpected = 0;
                bool p = (dif == null || dif.Count - difExpected == 0);

                dif = HelperTesting.GetDifferences(movimiento.Local, object2.Local, getChildrensDiff: false);
                difExpected = 0;
                p = (dif == null || dif.Count - difExpected == 0);

                dif = HelperTesting.GetDifferences(movimiento.cheque, object2.cheque, getChildrensDiff: true);
                difExpected = 1;//el estado
                p = (dif == null || dif.Count - difExpected == 0);

                Assert.AreEqual(object2.cheque.EstadoCheque, EstadoCheque.Depositado);



                dif = HelperTesting.GetDifferences(movimiento.cuentaDestino, object2.cuentaDestino, getChildrensDiff: true);
                difExpected = 1;//el saldo
                p = (dif == null || dif.Count - difExpected == 0);



                Assert.AreEqual(object2.cuentaDestino.Saldo, saldoCuentaDestino + saldoCheque);

                Assert.IsTrue(p);
            }
        }




        [TestMethod]
        public void TestDisableMovimientoCobroCheque()
        {
            var movimientoService = new MovimientoCuentaService();
            var testList = new List<HelperTesting.ServicesEnum>();

            testList.Add(HelperTesting.ServicesEnum.Codigo);

            fixture = HelperTesting.SetUp(testList, true);

            var opts = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var movimiento = new MovimientoCuentaData();

                Guid id = Guid.NewGuid();
                var chequeService = new ChequeService();

                movimiento = FactoryTesting.GetMovimientoCobroCheque();

                decimal saldoCheque = movimiento.cheque.Monto;
                decimal saldoCuentaDestino = movimiento.cuentaDestino.Saldo;


                movimiento.ID = id;


                Assert.IsTrue(movimientoService.Insert(movimiento));


                MovimientoCuentaData object2 = movimientoService.GetById(id);

                //ver que differencias me interesan saber, porque el insert genera un cambio de cuenta( saldo) y cheque ( estado)



                List<string> dif = HelperTesting.GetDifferences(movimiento.Personal, object2.Personal, getChildrensDiff: true);
                int difExpected = 0;
                bool p = (dif == null || dif.Count - difExpected == 0);

                dif = HelperTesting.GetDifferences(movimiento.Local, object2.Local, getChildrensDiff: false);
                difExpected = 0;
                p = (dif == null || dif.Count - difExpected == 0);

                dif = HelperTesting.GetDifferences(movimiento.cheque, object2.cheque, getChildrensDiff: true);
                difExpected = 1;//el estado
                p = (dif == null || dif.Count - difExpected == 0);

                Assert.AreEqual(object2.cheque.EstadoCheque, EstadoCheque.Depositado);



                dif = HelperTesting.GetDifferences(movimiento.cuentaDestino, object2.cuentaDestino, getChildrensDiff: true);
                difExpected = 1;//el saldo
                p = (dif == null || dif.Count - difExpected == 0);



                Assert.AreEqual(object2.cuentaDestino.Saldo, saldoCuentaDestino + saldoCheque);

                Assert.IsTrue(p);




                Assert.IsTrue(movimientoService.Disable(id));


                object2 = movimientoService.GetById(id);

                Assert.IsTrue(object2.cheque.EstadoCheque==EstadoCheque.EnCartera);

                Assert.IsTrue(!object2.Enable);

                Assert.IsTrue(object2.cuentaDestino.Saldo == saldoCuentaDestino);
            }
        }



        [TestMethod]
        public void TestInsertMovimientoRetiroBancoACaja()
        {
            var movimientoService = new MovimientoCuentaService();
            var testList = new List<HelperTesting.ServicesEnum>();

            testList.Add(HelperTesting.ServicesEnum.Codigo);

            fixture = HelperTesting.SetUp(testList, true);

            var opts = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var movimiento = new MovimientoCuentaData();

                Guid id = Guid.NewGuid();
                var cuentaService = new CuentaService();

                movimiento = FactoryTesting.GetMovimientoExtraccion();

                
                decimal saldoCuentaDestino = movimiento.cuentaDestino.Saldo;
                decimal saldoCuentaOrigen = movimiento.cuentaOrigen.Saldo;
                decimal Monto = 999;
                movimiento.Monto = Monto;

                movimiento.ID = id;


                Assert.IsTrue(movimientoService.Insert(movimiento));


                MovimientoCuentaData object2 = movimientoService.GetById(id);

                //ver que differencias me interesan saber, porque el insert genera un cambio de cuenta( saldo) y cheque ( estado)



                List<string> dif = HelperTesting.GetDifferences(movimiento.Personal, object2.Personal, getChildrensDiff: true);
                int difExpected = 0;
                bool p = (dif == null || dif.Count - difExpected == 0);

                dif = HelperTesting.GetDifferences(movimiento.Local, object2.Local, getChildrensDiff: false);
                difExpected = 0;
                p = (dif == null || dif.Count - difExpected == 0);

                dif = HelperTesting.GetDifferences(movimiento.cheque, object2.cheque, getChildrensDiff: true);
                difExpected = 0;//el estado
                p = (dif == null || dif.Count - difExpected == 0);

                



                dif = HelperTesting.GetDifferences(movimiento.cuentaDestino, object2.cuentaDestino, getChildrensDiff: true);
                difExpected = 0;
                p = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(p);

                dif = HelperTesting.GetDifferences(movimiento.cuentaOrigen, object2.cuentaOrigen, getChildrensDiff: true);
                difExpected = 1;//el saldo
                p = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(p);


                var ingresoService = new IngresoService();
                IngresoData ingreso = ingresoService.GetLast(movimiento.Local.ID, movimiento.Prefix);



                Assert.AreEqual(ingreso.Monto, Monto);
                Assert.AreEqual(object2.cuentaOrigen.Saldo, saldoCuentaOrigen - Monto);

                
            }
        }



        [TestMethod]
        public void TestDisabletMovimientoRetiroBancoACaja()
        {
            var movimientoService = new MovimientoCuentaService();
            var testList = new List<HelperTesting.ServicesEnum>();

            testList.Add(HelperTesting.ServicesEnum.Codigo);

            fixture = HelperTesting.SetUp(testList, true);

            var opts = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var movimiento = new MovimientoCuentaData();

                Guid id = Guid.NewGuid();
                var cuentaService = new CuentaService();

                movimiento = FactoryTesting.GetMovimientoExtraccion();


                decimal saldoCuentaDestino = movimiento.cuentaDestino.Saldo;
                decimal saldoCuentaOrigen = movimiento.cuentaOrigen.Saldo;
                decimal Monto = 999;
                movimiento.Monto = Monto;

                movimiento.ID = id;


                Assert.IsTrue(movimientoService.Insert(movimiento));


                MovimientoCuentaData object2 = movimientoService.GetById(id);

                //ver que differencias me interesan saber, porque el insert genera un cambio de cuenta( saldo) y cheque ( estado)



                List<string> dif = HelperTesting.GetDifferences(movimiento.Personal, object2.Personal, getChildrensDiff: true);
                int difExpected = 0;
                bool p = (dif == null || dif.Count - difExpected == 0);

                dif = HelperTesting.GetDifferences(movimiento.Local, object2.Local, getChildrensDiff: false);
                difExpected = 0;
                p = (dif == null || dif.Count - difExpected == 0);

                dif = HelperTesting.GetDifferences(movimiento.cheque, object2.cheque, getChildrensDiff: true);
                difExpected = 0;//el estado
                p = (dif == null || dif.Count - difExpected == 0);





                dif = HelperTesting.GetDifferences(movimiento.cuentaDestino, object2.cuentaDestino, getChildrensDiff: true);
                difExpected = 0;
                p = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(p);

                dif = HelperTesting.GetDifferences(movimiento.cuentaOrigen, object2.cuentaOrigen, getChildrensDiff: true);
                difExpected = 1;//el saldo
                p = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(p);


                var ingresoService = new IngresoService();
                IngresoData ingreso = ingresoService.GetLast(movimiento.Local.ID, movimiento.Prefix);



                Assert.AreEqual(ingreso.Monto, Monto);
                Assert.AreEqual(object2.cuentaOrigen.Saldo, saldoCuentaOrigen - Monto);





                Assert.IsTrue(movimientoService.Disable(id));


                object2 = movimientoService.GetById(id);

                ingreso = ingresoService.GetLast(movimiento.Local.ID, movimiento.Prefix);

                Assert.IsTrue(!object2.Enable);

                Assert.IsTrue(object2.cuentaOrigen.Saldo == saldoCuentaOrigen);

                Assert.IsTrue(!ingreso.Enable);

            }
        }


        [TestMethod]
        public void TestInsertMovimientoRetiroCajaABanco()
        {
            var movimientoService = new MovimientoCuentaService();
            var testList = new List<HelperTesting.ServicesEnum>();

            testList.Add(HelperTesting.ServicesEnum.Codigo);

            fixture = HelperTesting.SetUp(testList, true);

            var opts = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var movimiento = new MovimientoCuentaData();

                Guid id = Guid.NewGuid();
                var cuentaService = new CuentaService();

                movimiento = FactoryTesting.GetMovimientoDeposito();


                decimal saldoCuentaDestino = movimiento.cuentaDestino.Saldo;
                decimal saldoCuentaOrigen = movimiento.cuentaOrigen.Saldo;
                decimal Monto = 999;
                movimiento.Monto = Monto;

                movimiento.ID = id;
                
                Assert.IsTrue(movimientoService.Insert(movimiento));


                MovimientoCuentaData object2 = movimientoService.GetById(id);

                //ver que differencias me interesan saber, porque el insert genera un cambio de cuenta( saldo) y cheque ( estado)


                List<string> dif = HelperTesting.GetDifferences(movimiento.Personal, object2.Personal, getChildrensDiff: true);
                int difExpected = 0;
                bool p = (dif == null || dif.Count - difExpected == 0);

                dif = HelperTesting.GetDifferences(movimiento.Local, object2.Local, getChildrensDiff: false);
                difExpected = 0;
                p = (dif == null || dif.Count - difExpected == 0);

                dif = HelperTesting.GetDifferences(movimiento.cheque, object2.cheque, getChildrensDiff: true);
                difExpected = 0;//el estado
                p = (dif == null || dif.Count - difExpected == 0);


                dif = HelperTesting.GetDifferences(movimiento.cuentaDestino, object2.cuentaDestino, getChildrensDiff: true);
                difExpected = 1;//el saldo
                p = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(p);


                dif = HelperTesting.GetDifferences(movimiento.cuentaOrigen, object2.cuentaOrigen, getChildrensDiff: true);
                difExpected = 0;
                p = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(p);
                

                var retiroService = new RetiroService();
                RetiroData retiro = retiroService.GetLast(movimiento.Local.ID, movimiento.Prefix);

                Assert.AreEqual(retiro.Monto, Monto);
                Assert.AreEqual(object2.cuentaDestino.Saldo, saldoCuentaDestino + Monto);
            }
        }



        [TestMethod]
        public void TestDisableMovimientoRetiroCajaABanco()
        {
            var movimientoService = new MovimientoCuentaService();
            var testList = new List<HelperTesting.ServicesEnum>();

            testList.Add(HelperTesting.ServicesEnum.Codigo);

            fixture = HelperTesting.SetUp(testList, true);

            var opts = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var movimiento = new MovimientoCuentaData();

                Guid id = Guid.NewGuid();
                var cuentaService = new CuentaService();

                movimiento = FactoryTesting.GetMovimientoDeposito();


                decimal saldoCuentaDestino = movimiento.cuentaDestino.Saldo;
                decimal saldoCuentaOrigen = movimiento.cuentaOrigen.Saldo;
                decimal Monto = 999;
                movimiento.Monto = Monto;

                movimiento.ID = id;

                Assert.IsTrue(movimientoService.Insert(movimiento));


                MovimientoCuentaData object2 = movimientoService.GetById(id);

                //ver que differencias me interesan saber, porque el insert genera un cambio de cuenta( saldo) y cheque ( estado)


                List<string> dif = HelperTesting.GetDifferences(movimiento.Personal, object2.Personal, getChildrensDiff: true);
                int difExpected = 0;
                bool p = (dif == null || dif.Count - difExpected == 0);

                dif = HelperTesting.GetDifferences(movimiento.Local, object2.Local, getChildrensDiff: false);
                difExpected = 0;
                p = (dif == null || dif.Count - difExpected == 0);

                dif = HelperTesting.GetDifferences(movimiento.cheque, object2.cheque, getChildrensDiff: true);
                difExpected = 0;//el estado
                p = (dif == null || dif.Count - difExpected == 0);


                dif = HelperTesting.GetDifferences(movimiento.cuentaDestino, object2.cuentaDestino, getChildrensDiff: true);
                difExpected = 1;//el saldo
                p = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(p);


                dif = HelperTesting.GetDifferences(movimiento.cuentaOrigen, object2.cuentaOrigen, getChildrensDiff: true);
                difExpected = 0;
                p = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(p);


                var retiroService = new RetiroService();
                RetiroData retiro = retiroService.GetLast(movimiento.Local.ID, movimiento.Prefix);

                Assert.AreEqual(retiro.Monto, Monto);
                Assert.AreEqual(object2.cuentaDestino.Saldo, saldoCuentaDestino + Monto);

                Assert.IsTrue(movimientoService.Disable(id));


                
                object2 = movimientoService.GetById(id);

                Assert.IsTrue(!object2.Enable);

                Assert.IsTrue(object2.cuentaDestino.Saldo == saldoCuentaDestino);

                retiro = retiroService.GetLast(movimiento.Local.ID, movimiento.Prefix);

                Assert.IsTrue(!retiro.Enable);


            }
        }


        [TestMethod]
        public void TestNextNumber()
        {
            var movimientoService = new MovimientoCuentaService();
            var testList = new List<HelperTesting.ServicesEnum>();

            testList.Add(HelperTesting.ServicesEnum.Codigo);

            fixture = HelperTesting.SetUp(testList, true);

            var opts = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var movimiento = FactoryTesting.GetMovimientoPagoCheque();

                Guid id = Guid.NewGuid();
                movimiento.Numero = Convert.ToInt32(movimientoService.GetNextNumberAvailable(false, HelperService.IDLocal, HelperService.Prefix));
                movimiento.ID = id;
                movimiento.Prefix = HelperService.Prefix;
                

              



                Assert.IsTrue(movimientoService.Insert(movimiento));
               
                var movimiento2 = new MovimientoCuentaData();

                Guid id2 = Guid.NewGuid();
                movimiento2.Numero = Convert.ToInt32(movimientoService.GetNextNumberAvailable(false, movimiento.Local.ID, HelperService.Prefix));
                movimiento2.ID = id2;



                Assert.AreEqual(movimiento2.Numero, movimiento.Numero + 1);



            }
        }

     
      


        [TestMethod]
        public void GetLast()
         
        {
            List<HelperTesting.ServicesEnum> testList = new List<HelperTesting.ServicesEnum>();
            fixture = HelperTesting.SetUp(testList, false);
            

            bool p = false;

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                var service = new MovimientoCuentaService();
                LocalData Local = fixture.Create<LocalData>();
                int prefix = 1;

                MovimientoCuentaData object1 = service.GetLast(Local.ID, prefix);


                Assert.IsTrue(object1.Numero > 0);//el primero siempre tiene que ser al menos 1
                Assert.IsTrue(object1.Prefix == prefix);




                Guid id = Guid.NewGuid();

                MovimientoCuentaData object2 = FactoryTesting.GetMovimientoPagoCheque();
                object2.ID = id;
                object2.Numero = Convert.ToInt32(service.GetLast(Local.ID, prefix).Numero) + 1;//hermoso
                object2.Prefix = prefix;
                object2.Local = Local;

                Assert.IsTrue(service.Insert(object2));

              
                MovimientoCuentaData object3 = service.GetLast(Local.ID, prefix);

                Assert.IsTrue(object3.Numero > object1.Numero);


                int difExpected = 0;

                List<string> dif = HelperTesting.GetDifferences(object3.Personal, object2.Personal, getChildrensDiff: false);
                difExpected = 0;
                p = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(p);

                dif = HelperTesting.GetDifferences(object3.Local, object2.Local, getChildrensDiff: false);
                difExpected = 0;
                p = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(p);

                dif = HelperTesting.GetDifferences(object3.cheque, object2.cheque, getChildrensDiff: false);
                difExpected = 0;
                p = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(p);

                dif = HelperTesting.GetDifferences(object3.cuentaDestino, object2.cuentaDestino, getChildrensDiff: false);
                difExpected = 1;//el saldo?
                p = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(p);

                dif = HelperTesting.GetDifferences(object3.cuentaOrigen, object2.cuentaOrigen, getChildrensDiff: false);
                difExpected = 1;//el saldo?
                p = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(p);

                dif = HelperTesting.GetDifferences(object3, object2, getChildrensDiff: false);
                difExpected = 2;//fecha uso y Codigo por herencia
                p = (dif == null || dif.Count - difExpected == 0);
                Assert.IsTrue(p);

                Assert.IsTrue(p);

            }
            
        }
    }
}