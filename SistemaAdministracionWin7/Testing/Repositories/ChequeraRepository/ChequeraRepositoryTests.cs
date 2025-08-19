using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Repository.ChequeraRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using Services.ChequeraService;
using DTO.BusinessEntities;
namespace Repository.ChequeraRepository.Tests
{
    [TestClass()]
    public class ChequeraRepositoryTests
    {
        [TestMethod()]
        public void Insert_Get_SetearSiguienteTest()
        {
            Services.HelperService.getParameters();
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var chequeraService = new ChequeraService(new ChequeraRepository());
                
                ChequeraData chequera = new ChequeraData();
                ChequeraData chequera2 = new ChequeraData();

                Guid id=  Guid.NewGuid();
                chequera.ID =id;
                chequera.CodigoInterno = Convert.ToInt32(chequeraService.GetNextNumberAvailable());
                chequera.Description = "test";
                chequera.Enable=true;
                chequera.Cuenta = new CuentaData();
                chequera.Primero="0001";
                chequera.Siguiente="0001";
                chequera.Ultimo="0100";
                



                bool task =chequeraService.Insert(chequera);
                Assert.IsTrue(task);

                task = chequeraService.SetearSiguiente(chequera);
                    Assert.IsTrue(task);

                    chequera2 = chequeraService.GetByID(id);
                Assert.IsTrue(chequera2.Description!="");

                Assert.IsTrue(Convert.ToInt32(chequera2.Siguiente) == 2);

            }
        }

       

        [TestMethod()]
        public void UpdateTest()
        {

            Services.HelperService.getParameters();
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var chequeraService = new ChequeraService(new ChequeraRepository());

                ChequeraData chequera = new ChequeraData();
                ChequeraData chequera2 = new ChequeraData();

                Guid id = Guid.NewGuid();
                string desc1 = "test";
                string desc2 = "test2";
                chequera.ID = id;
                chequera.CodigoInterno = Convert.ToInt32(chequeraService.GetNextNumberAvailable());
                chequera.Description=desc1;
                chequera.Enable = true;
                chequera.Cuenta = new CuentaData();
                chequera.Primero = "0001";
                chequera.Siguiente = "0001";
                chequera.Ultimo = "0100";




                bool task = chequeraService.Insert(chequera);
                Assert.IsTrue(task);



                chequera2 = chequeraService.GetByID(id);
                Assert.IsTrue(chequera2.Description==desc1);


                chequera.Description = desc2;

               task= chequeraService.Update(chequera);
                Assert.IsTrue(task);

                chequera2 = chequeraService.GetByID(id);
                Assert.IsTrue(chequera2.Description == desc2);


            }
        }

        [TestMethod()]
        public void DisableTest()
        {
            Services.HelperService.getParameters();
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var chequeraService = new ChequeraService(new ChequeraRepository());

                ChequeraData chequera = new ChequeraData();
                ChequeraData chequera2 = new ChequeraData();

                Guid id = Guid.NewGuid();
                string desc1 = "test";
                string desc2 = "test2";
                chequera.ID = id;
                chequera.CodigoInterno = Convert.ToInt32(chequeraService.GetNextNumberAvailable());
                chequera.Description = desc1;
                chequera.Enable = true;
                chequera.Cuenta = new CuentaData();
                chequera.Primero = "0001";
                chequera.Siguiente = "0001";
                chequera.Ultimo = "0100";




                bool task = chequeraService.Insert(chequera);
                Assert.IsTrue(task);

                task=chequeraService.Disable(chequera.ID);
                Assert.IsTrue(task);

                chequera2 = chequeraService.GetByID(id);
                Assert.IsTrue(!chequera2.Enable);



            }
        }

        [TestMethod()]
        public void EnableTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetAllTest()
        {

            Services.HelperService.getParameters();
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var chequeraService = new ChequeraService(new ChequeraRepository());

                ChequeraData chequera = new ChequeraData();
                ChequeraData chequera2 = new ChequeraData();

                Guid id = Guid.NewGuid();
                string desc1 = "test";
                string desc2 = "test2";
                chequera.ID = id;
                chequera.CodigoInterno = Convert.ToInt32(chequeraService.GetNextNumberAvailable());
                chequera.Description = desc1;
                chequera.Enable = true;
                chequera.Cuenta = new CuentaData();
                chequera.Primero = "0001";
                chequera.Siguiente = "0001";
                chequera.Ultimo = "0100";




                bool task = chequeraService.Insert(chequera);
                Assert.IsTrue(task);

             


            }
        }


        [TestMethod()]
        public void GetLastTest()
        {
            Services.HelperService.getParameters();
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var chequeraService = new ChequeraService(new ChequeraRepository());

                ChequeraData chequera = new ChequeraData();
                ChequeraData chequera2 = new ChequeraData();
                int number = 0;
                int number2 = 0;
                Guid id = Guid.NewGuid();
                string desc1 = "test3";
                string desc2 = "test2";
                chequera.ID = id;
                number = Convert.ToInt32(chequeraService.GetNextNumberAvailable());
                chequera.CodigoInterno = number;
                chequera.Description = desc1;
                chequera.Enable = true;
                chequera.Cuenta = new CuentaData();
                chequera.Primero = "0001";
                chequera.Siguiente = "0001";
                chequera.Ultimo = "0100";


                

                bool task = chequeraService.Insert(chequera);
                Assert.IsTrue(task);
                

                number2=Convert.ToInt32(chequeraService.GetNextNumberAvailable());


                Assert.IsTrue(number+1==number2);
            }
        }
    }
}
