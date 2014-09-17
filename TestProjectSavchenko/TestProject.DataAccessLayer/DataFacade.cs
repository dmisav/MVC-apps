using System;
using System.Collections.Generic;
using System.Linq;
using TestProject.DataAccessLayer.Entities;
using TestProject.DataAccessLayer.Exceptions;
using TestProject.DataAccessLayer.Mappers;

namespace TestProject.DataAccessLayer
{

    public class MapperProvider
    {
        public IMapper GetMapper<T>()
       {
           if(typeof(T) == typeof(CountryMapper))
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

    public class DataFacade
    {
        private IMapper countryMapper;
        private IMapper provinceMapper;
        private IMapper userMapper;

        public DataFacade(MapperProvider mappperProvider)
        {
            this.countryMapper = mappperProvider.GetMapper<CountryMapper>();
            this.provinceMapper = mappperProvider.GetMapper<ProvinceMapper>();
            this.userMapper = mappperProvider.GetMapper<UserMapper>();
        }

        public List<CountryModel> GetCountries()
        {
            var countryList = new List<CountryModel>();
            using (var db = new TestTaskDBEntities())
            {
                foreach (Country dbCountry in db.Countries)
                {
                    var country = (CountryModel)countryMapper.Map(dbCountry);
                    countryList.Add(country);
                }
            }

            return countryList;
        }

        public List<ProvinceModel> GetProvinces(int countryId)
        {
            var provinceList = new List<ProvinceModel>();
            using (var db = new TestTaskDBEntities())
            {
                var provincesInCountry = from provinces in db.Provinces
                                         from countryProvinces in provinces.CountryProvinces
                                         where countryProvinces.COUNTRY_ID == countryId
                                         select provinces;
                foreach (Province dbProvince in provincesInCountry)
                {
                    var province = (ProvinceModel)provinceMapper.Map(dbProvince);
                    provinceList.Add(province);
                }
            }
            return provinceList;
        }

        public void SignUpUser(string email, string password)
        {
            try
            {
                GetUserByEmail(email);
            }
            catch (NonExistingUserException exception)
            {
                try
                {
                    User user = new User() { EMAIL = email, PASSWORD = password };
                    using (var db = new TestTaskDBEntities())
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(String.Format("Not Able to Add User: {0}", e.Message));
                }
            }
        }

        public UserModel GetUserByEmail(string email)
        {
            using (var db = new TestTaskDBEntities())
            {
                var user = db.Users.FirstOrDefault(item => item.EMAIL == email);
                if (user == null) throw new NonExistingUserException("Given User does not exist");
                return (UserModel)userMapper.Map<User>(user);
            }
        }

        public UserModel GetUserByUserID(int userId)
        {
            using (var db = new TestTaskDBEntities())
            {
                var user = db.Users.Find(userId);
                if (user == null) throw new NonExistingUserException("Given User does not exist");
                return (UserModel)userMapper.Map<User>(user);
            }
        }

        public void SaveUser(UserModel userModel)
        { 
             
        }

    }
}