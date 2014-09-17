using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TestProject.DataAccessLayer;
using TestProject.DataAccessLayer.Entities;
using TestProject.DataAccessLayer.Mappers;

namespace TestProject.UnitTests.Tests
{
    [TestClass]
    public class ProvinceMapperTests
    {
        private MapperProvider mapperProvider;
        private IMapper sut;
        private Province province;

        [TestInitialize]
        public void Init()
        {
            mapperProvider = new MapperProvider();
            sut = mapperProvider.GetMapper<ProvinceMapper>();
            province = GetProvince();
        }

        [TestMethod]
        public void ShouldMapProvinceToProvinceModel()
        {
            var result = (ProvinceModel)sut.Map(province);
            Assert.AreEqual(result.id, province.ID.ToString());
            Assert.AreEqual(result.name, province.NAME);
            Assert.AreEqual(result.countryId, province.CountryProvinces.First().COUNTRY_ID.ToString());
        }

        private Province GetProvince()
        {
            return province = new Province()
            {
                ID = 1,
                NAME = "NewYork",
                CountryProvinces = new List<CountryProvince>() { new CountryProvince() { COUNTRY_ID = 1, PROVINCE_ID = 1 } }
            };
        }
    }
}