using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO.BusinessEntities;
using Repository.ChequeRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.BancoService;
using Services.ChequeService;

namespace Repository.ChequeRepository.Tests
{
    [TestClass()]
    public class ChequeRepositoryTests
    {
  

        [TestMethod()]
        public void Insert_and_Get_Test()
        {
            ChequeData cDTO = new ChequeData();
            ChequeData cDTO2 = new ChequeData();

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                var chequeService = new ChequeService(new ChequeRepository());

                
                cDTO = cargarChequeDefault(1);

                
                bool task = chequeService.Insert(cDTO);

                Assert.IsTrue(task);

                cDTO2 = chequeService.GetByID(cDTO.ID);

                bool p = compareCheques(cDTO, cDTO2);

                Assert.IsTrue(p);

            }
        }
        [TestMethod()]
        public void Insert_and_Next_Test()
        {
            ChequeData cDTO = new ChequeData();
            ChequeData cDTO2 = new ChequeData();

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                var chequeService = new ChequeService(new ChequeRepository());


                cDTO = cargarChequeDefault(1);


                bool task = chequeService.Insert(cDTO);

                Assert.IsTrue(task);

                cDTO2 = cargarChequeDefault(2);

                task = chequeService.Insert(cDTO2);

                Assert.IsTrue(task);

                Guid id1 = cDTO.ID;
                Guid id2= cDTO2.ID;
                cDTO = null;
                cDTO2 = null;


                cDTO = chequeService.GetByID(id1);
                cDTO2 = chequeService.GetByID(id2);


                Assert.IsTrue(cDTO.Interno+1==cDTO2.Interno);
            }
        }

        private bool compareCheques(ChequeData a, ChequeData b)
        {

            if (a.ID != b.ID)
                return false;

            if (a.Description != b.Description)
                return false;

            if (a.Monto != b.Monto)
                return false;

            if (a.Estado != b.Estado)
                return false;

            if (a.Interno != b.Interno)
                return false;

            if (a.Numero != b.Numero)
                return false;



            return true;


        }


        private ChequeData cargarChequeDefault(int p)
        {
          ChequeData c = new ChequeData();
            ChequeraData chequera = new ChequeraData();
            

            switch (p)
            {
                case 1:
                     c.Date = DateTime.Now;
                    c.Description = "desc1";
                    c.Enable = true;
                    c.ID = Guid.NewGuid();
                    c.BancoEmisor = new BancoData();
                    c.Chequera = chequera;
                    c.Estado=estadoCheque.Creado;
                    c.FechaCobro = DateTime.Now.AddDays(30);
                    c.FechaEmision = DateTime.Now;
                    c.Local=new LocalData();
                    c.Monto = 100;
                    c.Numero = "1234";
                    c.Titular = "martin palermo";
                    
                    break;

                case 2:
                     c.Date = DateTime.Now;
                    c.Description = "desc2";
                    c.Enable = true;
                    c.ID = Guid.NewGuid();
                    c.BancoEmisor = new BancoData();
                    c.Chequera = chequera;
                    c.Estado=estadoCheque.Creado;
                    c.FechaCobro = DateTime.Now.AddDays(60);
                    c.FechaEmision = DateTime.Now;
                    c.Local=new LocalData();
                    c.Monto = 5657;
                    c.Numero = "99998";
                    c.Titular = "batitusta";
                    break;
                    

            }
           

            return c;
        }

        
        [TestMethod()]
        public void ObtenerUltimoInternoTest()
        {
            ChequeData cDTO = new ChequeData();
            ChequeData cDTO2 = new ChequeData();

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                Guid idchequera = Guid.NewGuid();
                var chequeService = new ChequeService(new ChequeRepository());


                cDTO = cargarChequeDefault(1);
                cDTO.Chequera.ID = idchequera;

                bool task = chequeService.Insert(cDTO);

                Assert.IsTrue(task);

                cDTO2 = cargarChequeDefault(2);
                cDTO2.Chequera.ID = idchequera;

                task = chequeService.Insert(cDTO2);

                Assert.IsTrue(chequeService.GetNextNumberAvailable() == 3);
            }
        }

        [TestMethod()]
        public void GetByChequeraTest()
        {
            ChequeData cDTO = new ChequeData();
            ChequeData cDTO2 = new ChequeData();

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                Guid idchequera = Guid.NewGuid();
                var chequeService = new ChequeService(new ChequeRepository());


                cDTO = cargarChequeDefault(1);
                cDTO.Chequera.ID = idchequera;

                bool task = chequeService.Insert(cDTO);

                Assert.IsTrue(task);

                cDTO2 = cargarChequeDefault(2);
                cDTO2.Chequera.ID = idchequera;

                task = chequeService.Insert(cDTO2);

                List<ChequeData> todos = chequeService.GetByChequera(idchequera, true);

                Assert.IsTrue(todos.Count == 2);
            }
        }
    }
}
