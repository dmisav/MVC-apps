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

        [TestMethod]
        public void ShouldSaveUserProvince()
        {
            var newUser = GetUserModel();
            var someCountryId = GetSomeCountryID();
            var someprovinceID = GetsomeProvinceId(someCountryId);
            using (TransactionScope scope = new TransactionScope())
            {
                sut.SignUpUser(newUser.email, newUser.password);
                newUser = sut.GetUserByEmail(newUser.email);
                sut.SaveUserProvince(Convert.ToInt32(newUser.id), someprovinceID);
                newUser = sut.GetUserByEmail(newUser.email);
            }
            Assert.IsTrue(!String.IsNullOrEmpty(newUser.provinceId));
        }

        private UserModel GetUserModel()
        {
            return new UserModel() { email = "randomMail@.randomMail@.com", password = "randomPassword123" };
        }

        private int GetSomeCountryID()
        {
            return Convert.ToInt32(sut.GetCountries().FirstOrDefault().id);
        }

        private int GetsomeProvinceId(int countryId)
        {
            return Convert.ToInt32(sut.GetProvinces(countryId).FirstOrDefault().id);
        }

    }
}