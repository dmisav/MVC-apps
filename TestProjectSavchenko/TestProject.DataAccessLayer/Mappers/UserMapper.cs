using System;
using TestProject.DataAccessLayer.Entities;
using TestProject.DataAccessLayer.Exceptions;
using System.Linq;

namespace TestProject.DataAccessLayer.Mappers
{
    public class UserMapper:IMapper
    {
        public AbstractModel Map<T>(T user)
        {
            var usr = user as User;
            try
            {
                return new UserModel()
                {
                    id = usr.ID.ToString(),
                    email = usr.EMAIL,
                    password = usr.PASSWORD,
                    provinceId = usr.PROVINCE.ToString(),
                    countryId = usr.Province1 == null ? null : usr.Province1.CountryProvinces.FirstOrDefault().COUNTRY_ID.ToString()
                };
            }
            catch (Exception e)
            {
                throw new MapperException("Mapping from User UserModel objects failed", e);
            }
        }
    }
}