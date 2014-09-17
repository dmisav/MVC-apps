using System;
using TestProject.DataAccessLayer.Entities;
using TestProject.DataAccessLayer.Exceptions;

namespace TestProject.DataAccessLayer.Mappers
{
    public class CountryMapper : IMapper
    {
        public AbstractModel Map<T>(T country) 
        {
            var cntry = country as Country;
            try
            {
                return new CountryModel()
                {
                    id = (cntry as Country).ID.ToString(),
                    name = (cntry as Country).NAME
                };
            }
            catch (Exception e)
            {
                throw new MapperException("Mapping from Country to CountryModel objects failed", e );
            }
        }
    }
}