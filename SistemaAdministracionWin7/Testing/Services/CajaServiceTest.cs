using System;
using System.Collections.Generic;
using System.Transactions;
using DTO.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.CajaService;

namespace Testing.Services
{
    [TestClass()]
    public class CajaServiceTest
    {

        [TestMethod]
        public void CerrarCajaAndGetTest()
        {
            var cajaService = new CajaService();

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                DateTime future_time = new DateTime(2020,10,5,10,5,0);
                decimal money = new decimal(200.2);
                Guid idCaja = Guid.NewGuid();
                Guid idLocal = new Guid("9C5596C1-A290-4C41-9E91-9EE105F647FB");
                bool task;
                task = cajaService.CerrarCaja(future_time, money, idCaja, idLocal);

                Assert.IsTrue(task);

                CajaData cajaManana = cajaService.GetCajaInicial(future_time.AddDays(1), idLocal);


                Assert.IsTrue(cajaManana.ID == idCaja);



            }

        }

        [TestMethod]
        public void Cerrarx2Test()
        {
            var cajaService = new CajaService();

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                DateTime future_time = new DateTime(2020, 10, 5, 10, 5, 0);
                decimal money = new decimal(200.2);
                Guid idCaja = Guid.NewGuid();
                Guid idLocal = new Guid("9C5596C1-A290-4C41-9E91-9EE105F647FB");
                bool task;
                task = cajaService.CerrarCaja(future_time, money, idCaja, idLocal);

                Assert.IsTrue(task);


                Assert.IsTrue(cajaService.IsClosed(future_time,idLocal));

                Assert.IsTrue(!cajaService.IsClosed(future_time.AddDays(1), idLocal));





            }

        }


        [TestMethod]
        public void GetOlderstte()
        {
            var cajaService = new CajaService();

            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                DateTime future_time = new DateTime(2020, 10, 5, 10, 5, 0);
                decimal money = new decimal(200.2);
                Guid idCaja = Guid.NewGuid();
                Guid idLocal = new Guid("9C5596C1-A290-4C41-9E91-9EE105F647FB");
                bool task;

                int moneyAdd = 20;
                task = cajaService.CerrarCaja(future_time, money, idCaja, idLocal);
                Assert.IsTrue(task);


                idCaja = Guid.NewGuid();
                future_time = future_time.AddDays(1);
                money += moneyAdd;
                task = cajaService.CerrarCaja(future_time, money, idCaja, idLocal);
                Assert.IsTrue(task);


                idCaja = Guid.NewGuid();
                future_time = future_time.AddDays(1);
                money += moneyAdd;
                task = cajaService.CerrarCaja(future_time, money, idCaja, idLocal);
                Assert.IsTrue(task);


                List<CajaData> x = cajaService.GetOlderThan(new DateTime(2020, 10, 5, 10, 4, 0), idLocal);
                
                Assert.IsTrue(x.Count==3);

                Assert.IsTrue(x[2].Monto==money);
                money -= moneyAdd;


                Assert.IsTrue(x[1].Monto == money);
                money -= moneyAdd;


                Assert.IsTrue(x[0].Monto == money);
                




            }

        }

    }
}
