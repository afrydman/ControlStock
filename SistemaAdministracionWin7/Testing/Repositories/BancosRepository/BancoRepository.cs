using System;
using System.Collections.Generic;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Repositories.BancosRepository;
using Services.BancoService;

namespace Testing.Repositories.BancosRepository
{
    [TestClass()]
    public class BancoRepositoryTest
    {
        [TestMethod()]
        public void Banco_InsertAndGetTest()
        {
            BancoData bDTO = new BancoData();
            BancoData bDTO2 = new BancoData();


            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                    var bancoService = new BancoService(new BancoRepository());

                    Guid id = Guid.NewGuid();

                    bDTO.ID = id;
                    bDTO.Enable = true;
                    bDTO.Description = "Prueba";

                    bool task = bancoService.Insert(bDTO);

                    Assert.IsTrue(task);

                    bDTO2 = bancoService.GetByID(id);


                    bool p = compareBancos(bDTO, bDTO2);

                    Assert.IsTrue(p);
            }

        }
        private bool compareBancos(BancoData bDTO, BancoData bDTO2)
        {

            return bDTO.ID == bDTO2.ID && bDTO.Description == bDTO2.Description && bDTO.Enable == bDTO2.Enable;
        }

        [TestMethod()]
        public void Bancos_InsertUpdateTest()
        {

            BancoData bDTO = new BancoData();
            BancoData bDTO2 = new BancoData();


            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {



                var bancoService = new BancoService(new BancoRepository());


                     const string name1="prueba1";
                     const string name2="prueba2";

                    Guid id = Guid.NewGuid();


                    bDTO.ID = id;
                    bDTO.Enable = true;
                    bDTO.Description = name1;


                    bDTO.ID = id;
                    bDTO.Enable = true;
                    bDTO.Description = name2;

                    bool task = bancoService.Insert(bDTO);

                    


                    Assert.IsTrue(task);


                    bDTO.Description=name2;

                    task = bancoService.Update(bDTO);

                    bDTO2 = bancoService.GetByID(id);

                    
                    bool p = compareBancos(bDTO, bDTO2);

                    Assert.IsTrue(p);
               

            }
        }

        [TestMethod()]
        public void Bancos_DisableTest()
        {
            BancoData bDTO = new BancoData();
            BancoData bDTO2 = new BancoData();


            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {



                var bancoService = new BancoService(new BancoRepository());


                    const string name1 = "prueba1";
                   

                    Guid id = Guid.NewGuid();


                    bDTO.ID = id;
                    bDTO.Enable = true;
                    bDTO.Description = name1;


                    bool task = bancoService.Insert(bDTO);



                    Assert.IsTrue(task);
                    
                    

                    task = bancoService.Disable(bDTO);

                    bDTO2 = bancoService.GetByID(id);

                   

                    Assert.IsTrue(bDTO2.Enable==false);
                

            }
        }

        [TestMethod()]
        public void Bancos_EnableTest()
        {
            BancoData bDTO = new BancoData();
            BancoData bDTO2 = new BancoData();


            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {



                var bancoService = new BancoService(new BancoRepository());


                    const string name1 = "prueba1";


                    Guid id = Guid.NewGuid();


                    bDTO.ID = id;
                    bDTO.Enable = false;
                    bDTO.Description = name1;


                    bool task = bancoService.Insert(bDTO);



                    Assert.IsTrue(task);



                    task = bancoService.Enable(bDTO);

                    bDTO2 = bancoService.GetByID(id);



                    Assert.IsTrue(bDTO2.Enable == true);
                

            }
        }

        [TestMethod()]
        public void Bancos_GetAllTest()
        {



            BancoData bDTO = new BancoData();
            BancoData bDTO2 = new BancoData();

            List<BancoData> bancos1 = new List<BancoData>();
            List<BancoData> bancos2 = new List<BancoData>();
            
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {



                var bancoService = new BancoService(new BancoRepository());


                const string name1 = "prueba1";


                bancos1 = bancoService.GetAll(false);

                Guid id = Guid.NewGuid();


                bDTO.ID = id;
                bDTO.Enable = false;
                bDTO.Description = name1;


                bool task = bancoService.Insert(bDTO);



                Assert.IsTrue(task);


                bancos2 = bancoService.GetAll(false);







                Assert.IsTrue(bancos2.Count==bancos1.Count+1);


            }
        }


    }
}
