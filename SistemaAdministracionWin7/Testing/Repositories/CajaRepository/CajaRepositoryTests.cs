using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories.CajaRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTO.BusinessEntities;
using Services.CajaService;
using System.Transactions;
namespace Repository.Repositories.CajaRepository.Tests
{
    [TestClass()]
    public class CajaRepositoryTests
    {

        [TestMethod()]
        public void Insert_And_GetLastTest()
        {
            Services.HelperService.getParameters();
                var opts = new TransactionOptions
          {
              IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
          };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                var cajaService = new CajaService(new CajaRepository());

                DateTime fecha = DateTime.Now.Date;
                decimal valor = 100;
                Guid idCaja = Guid.NewGuid();

                bool task = cajaService.CerrarCaja(fecha, valor, idCaja, Services.HelperService.IDLocal);

                Assert.IsTrue(task);

                CajaData ultima = cajaService.GetLast();

                Assert.IsTrue(ultima.Date == fecha);
                Assert.IsTrue(ultima.ID == idCaja);
                Assert.IsTrue(ultima.Monto == valor);
                Assert.IsTrue(ultima.Local.ID == Services.HelperService.IDLocal);
            }

        }

        
        

      

        [TestMethod()]
        public void GetOlderThanTest()
        {

            Services.HelperService.getParameters();
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {

                var cajaService = new CajaService(new CajaRepository());
                decimal valor = 100;

                DateTime fecha = DateTime.Now.Date;
                DateTime fecha2 = DateTime.Now.Date.AddHours(1);
                DateTime fecha3 = DateTime.Now.Date.AddHours(2);
                
                Guid idCaja = Guid.NewGuid();
                Guid idCaja2 = Guid.NewGuid();
                Guid idCaja3 = Guid.NewGuid();


                bool task = cajaService.CerrarCaja(fecha, valor, idCaja, Services.HelperService.IDLocal);
                Assert.IsTrue(task);
                
                task = cajaService.CerrarCaja(fecha2, valor, idCaja2, Services.HelperService.IDLocal);
                Assert.IsTrue(task);
                
                task = cajaService.CerrarCaja(fecha3, valor, idCaja3, Services.HelperService.IDLocal);
                Assert.IsTrue(task);

                List<CajaData> deberianser2 = cajaService.GetOlderThan(fecha.AddSeconds(1), Services.HelperService.IDLocal);
                Assert.IsTrue(deberianser2.Count==2);


                List<CajaData> deberianser1 = cajaService.GetOlderThan(fecha.AddHours(1).AddSeconds(1), Services.HelperService.IDLocal);

                Assert.IsTrue(deberianser1.Count==1);

                List<CajaData> deberianser0 = cajaService.GetOlderThan(fecha.AddHours(2).AddSeconds(1), Services.HelperService.IDLocal);

                Assert.IsTrue(deberianser0.Count == 0);
                
            }
        }

        [TestMethod()]
        public void GetCajabyFechaTest()//isClosed
        {
            Services.HelperService.getParameters();
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var cajaService = new CajaService(new CajaRepository());
                decimal valor = 100;
                decimal valor2 = 200;

                DateTime fecha = DateTime.Now.Date;
                DateTime fechaNO = DateTime.Now.Date.AddDays(-1);
                DateTime fecha2 = DateTime.Now.Date.AddDays(1);
                

                Guid idCaja = Guid.NewGuid();
                Guid idCaja2 = Guid.NewGuid();
                


                bool task = cajaService.CerrarCaja(fecha, valor, idCaja, Services.HelperService.IDLocal);
                Assert.IsTrue(task);

                task = cajaService.CerrarCaja(fecha2, valor2, idCaja2, Services.HelperService.IDLocal);
                Assert.IsTrue(task);

                bool cerradaMasUnDia = cajaService.IsClosed(fecha2, Services.HelperService.IDLocal);
                bool cerradaMenosUnDia = cajaService.IsClosed(fechaNO, Services.HelperService.IDLocal);
                bool cerradaHoy = cajaService.IsClosed(fecha, Services.HelperService.IDLocal);

                Assert.IsTrue(cerradaHoy);
                Assert.IsFalse(cerradaMenosUnDia);
                Assert.IsTrue(cerradaMasUnDia);

            }
        }

        [TestMethod()]
        public void GetCajaInicialTest()
        {
            Services.HelperService.getParameters();
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var cajaService = new CajaService(new CajaRepository());
                decimal valor = 100;
                decimal valor2 = 200;

                DateTime fecha = DateTime.Now.Date;
                
                DateTime fecha2 = DateTime.Now.Date.AddDays(1);


                Guid idCaja = Guid.NewGuid();
                Guid idCaja2 = Guid.NewGuid();
                


                bool task = cajaService.CerrarCaja(fecha, valor, idCaja, Services.HelperService.IDLocal);
                Assert.IsTrue(task);

                task = cajaService.CerrarCaja(fecha2, valor2, idCaja2, Services.HelperService.IDLocal);
                Assert.IsTrue(task);



                CajaData caja = cajaService.GetCajaInicial(fecha.AddDays(1), Services.HelperService.IDLocal);
                CajaData caja2 = cajaService.GetCajaInicial(fecha2.AddDays(1), Services.HelperService.IDLocal);
                

                Assert.IsTrue(caja.Monto==valor);
                Assert.IsTrue(caja2.Monto == valor2);
                

            }
        }

        [TestMethod()]
        public void GetByRangoFechaTest()
        {

            Services.HelperService.getParameters();
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var cajaService = new CajaService(new CajaRepository());
                decimal valor = 100;
                decimal valor2 = 200;

                DateTime fecha = DateTime.Now.Date;
                DateTime fecha2 = DateTime.Now.Date.AddDays(1);
                DateTime fecha3 = DateTime.Now.Date.AddDays(2);


                Guid idCaja = Guid.NewGuid();
                Guid idCaja2 = Guid.NewGuid();
                Guid idCaja3 = Guid.NewGuid();





                bool task = cajaService.CerrarCaja(fecha, valor, idCaja, Services.HelperService.IDLocal);
                Assert.IsTrue(task);

                task = cajaService.CerrarCaja(fecha2, valor2, idCaja2, Services.HelperService.IDLocal);
                Assert.IsTrue(task);

                task = cajaService.CerrarCaja(fecha3, valor, idCaja3, Services.HelperService.IDLocal);
                Assert.IsTrue(task);

                List<CajaData> cajas2 = cajaService.GetByRangoFecha(fecha,fecha.AddDays(1), Services.HelperService.IDLocal);
                List<CajaData> cajas2_bis = cajaService.GetByRangoFecha(fecha.AddDays(1), fecha.AddDays(2), Services.HelperService.IDLocal);
                List<CajaData> cajastrues = cajaService.GetByRangoFecha(fecha, fecha.AddDays(2), Services.HelperService.IDLocal);


                Assert.IsTrue(cajas2.Count == 2);
                Assert.IsTrue(cajas2_bis.Count == 2);
                Assert.IsTrue(cajastrues.Count == 3);
                


            }
        }
    }
}
