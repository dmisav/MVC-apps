using System;
using TestProject.DataAccessLayer.Mappers;

namespace TestProject.DataAccessLayer
{
    public class MapperProvider
    {
        public IMapper GetMapper<T>()
        {
            if (typeof(T) == typeof(CountryMapper))
            {
                return new CountryMapper();
            }
            if (typeof(T) == typeof(ProvinceMapper))
            {
                return new ProvinceMapper();
            }
            if (typeof(T) == typeof(UserMapper))
            {
                return new UserMapper();
            }
            else throw new NotImplementedException("Mapper type is not supported");
        }
    }
}