using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Transactions;
using TestProject.DataAccessLayer;
using TestProject.DataAccessLayer.Entities;
using TestProject.DataAccessLayer.Mappers;

namespace TestProject.UnitTests.Tests
{
    [TestClass]
    public class DataFacadeTests
    {
        private DataFacade sut;
        MapperProvider mapperProvider;

        [TestInitialize]
        public void Init()
        {
            mapperProvider = new MapperProvider();
            sut = new DataFacade(mapperProvider);
        }

        [TestMethod]
        public void ShouldGetAllzCountries()
        {
            var countryCount = sut.GetCountries().Count;
            Assert.IsTrue(countryCount > 0);
        }

        [TestMethod]
        public void ShouldGetAllProvinces()
        {
            var countryId = Convert.ToInt32(sut.GetCountries().First().id);
            var provinceCount = sut.GetProvinces(countryId).Count;
            Assert.IsTrue(provinceCount > 0);
        }

        [TestMethod]
        public void ShouldSignUpUser()
        {
            var email = "someEmail@email.com";
            var password = "a1";
            UserModel user;
            using (TransactionScope scope = new TransactionScope())
            {
                sut.SignUpUser(email, password);
                user = sut.GetUserByEmail(email);
            }
            Assert.IsNotNull(user);
        }
    }
}