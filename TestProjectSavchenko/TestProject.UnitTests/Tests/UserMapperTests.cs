using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TestProject.DataAccessLayer;
using TestProject.DataAccessLayer.Entities;
using TestProject.DataAccessLayer.Mappers;

namespace TestProject.UnitTests.Tests
{
    [TestClass]
    public class UserMapperTests
    {
        private IMapper sut;
        private User user;
        private MapperProvider mapperProvider;

        [TestInitialize]
        public void Init()
        {
            sut = mapperProvider.GetMapper<UserMapper>();
            user = GetUser();
        }

        [TestMethod]
        public void ShouldMapUserToUserModel()
        {
            var result = (UserModel)sut.Map(user);
            Assert.AreEqual(result.id, user.ID.ToString());
            Assert.AreEqual(result.email, user.EMAIL);
            Assert.AreEqual(result.provinceId, user.PROVINCE.ToString());
        }

        private User GetUser()
        {
            return user = new User()
            {
                ID = 1,
                EMAIL = "someMail",
                PASSWORD = "SomePassword",
                PROVINCE = 1,
                Province1 = new Province() { CountryProvinces = new List<CountryProvince>() { new CountryProvince() { COUNTRY_ID = 1, PROVINCE_ID = 1 } } }
            };
        }
    }
}