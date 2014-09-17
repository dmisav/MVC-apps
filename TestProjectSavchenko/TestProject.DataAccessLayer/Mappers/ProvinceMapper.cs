using System;
using System.Linq;
using TestProject.DataAccessLayer.Entities;
using TestProject.DataAccessLayer.Exceptions;

namespace TestProject.DataAccessLayer.Mappers
{
    public class ProvinceMapper:IMapper
    {
        public AbstractModel Map<T>(T province)
        {
            var prov = province as Province;
            try
            {
                return new ProvinceModel()
                {
                    id = prov.ID.ToString(),
                    name = prov.NAME,
                    countryId = prov.CountryProvinces.FirstOrDefault().COUNTRY_ID.ToString()
                };
            }
            catch (Exception e)
            {
                throw new MapperException("Mapping from Province to ProvinceModel objects failed", e);
            }
        }
    }
}