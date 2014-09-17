using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TestProject.DataAccessLayer;
using TestProject.DataAccessLayer.Entities;
using TestProject.DataAccessLayer.Mappers;

namespace TestProject.UnitTests.Tests
{
    [TestClass]
    public class CountryMapperTests
    {
        private IMapper sut;
        private MapperProvider mapperProvider;
        private Country country;

        [TestInitialize]
        public void Init()
        {
            mapperProvider = new MapperProvider();
            sut = mapperProvider.GetMapper<CountryMapper>();
            country = GetCountry();
        }

        [TestMethod]
        public void ShouldMapCountryToCountryModel()
        {
            var result = (CountryModel)sut.Map(country);
            Assert.AreEqual(result.id, country.ID.ToString());
            Assert.AreEqual(result.name, country.NAME);
        }

        private Country GetCountry()
        {
            return country = new Country()
            {
                ID = 1,
                NAME = "US",
                CountryProvinces = new List<CountryProvince>() { new CountryProvince() { COUNTRY_ID = 1, PROVINCE_ID = 1 } }
            };
        }
    }
}