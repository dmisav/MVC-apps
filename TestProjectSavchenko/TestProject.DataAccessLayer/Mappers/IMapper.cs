using TestProject.DataAccessLayer.Entities;

namespace TestProject.DataAccessLayer.Mappers
{
    public interface IMapper
    {
        AbstractModel Map<T>(T entity);
    }
}