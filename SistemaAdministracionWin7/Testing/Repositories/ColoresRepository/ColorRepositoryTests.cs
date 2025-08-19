using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.BusinessEntities;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.ColoresRepository;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Repository.ColoresRepository.Tests
{
    [TestClass()]
    public class ColorRepositoryTests
    {


        [TestMethod()]
        public void InsertTest()
        {
            var color = Builder<ColorData>.CreateNew().With(x=>x.ID=Guid.NewGuid()).Build();
            var color2 = Builder<ColorData>.CreateNew().With(x => x.ID = Guid.NewGuid()).Build();

            color.Description += "a";


            Assert.IsTrue(color.Description=="e");
            throw new NotImplementedException();
        }

        [Test()]
        public void UpdateTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void DisableTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void EnableTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void GetAllTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void GetByIDTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void GetLastTest()
        {
            throw new NotImplementedException();
        }

        [Test()]
        public void GetColorByCodigoTest()
        {
            throw new NotImplementedException();
        }
    }
}
