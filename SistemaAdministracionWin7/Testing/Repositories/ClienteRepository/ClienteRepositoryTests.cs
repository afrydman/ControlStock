using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DTO.BusinessEntities;
using Repository.ClienteRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.ChequeraService;
using Services.ClienteService;

namespace Repository.ClienteRepository.Tests
{
    [TestClass()]
    public class ClienteRepositoryTests
    {


        [TestMethod()]
        public void InsertTest()
        {
            Services.HelperService.getParameters();
            var opts = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var trans = new TransactionScope(TransactionScopeOption.Required, opts))
            {
                var clienteService = new ClienteService(new ClienteRepository());

                ClienteData cliente = new ClienteData();
                ClienteData cliente2 = new ClienteData();


                cliente = CargarCliente();



                bool task = clienteService.Insert(cliente);
                Assert.IsTrue(task);

               // task = clienteService.SetearSiguiente(chequera);
                Assert.IsTrue(task);

                //cliente2 = clienteService.GetbyID(id, true);
                Assert.IsTrue(cliente2.Description != "");


            }
        }

        private ClienteData CargarCliente()
        {
            ClienteData c = new ClienteData();
            c.ID = Guid.NewGuid();
            c.Description = "jose";
            c.Enable = true;
            c.cuil = "12";
            c.Direccion = "222";
            c.Descuento = "x";
            c.Email = "xxxx";
            c.Facebook = "asdasd";
            c.IngresoBruto = "xxxxxxxxx";
            c.NombreContacto = "xxaxxax";
            c.Description="soy una lavadora";
            c.RazonSocial = "sdasd";
            c.Telefono = "sssssssssssss";

            return c;


        }

        [TestMethod()]
        public void UpdateTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DisableTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void EnableTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetByIDTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetLastTest()
        {
            throw new NotImplementedException();
        }
    }
}
