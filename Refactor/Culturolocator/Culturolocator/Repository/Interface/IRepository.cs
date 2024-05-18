using Culturolocator.Models;

namespace Culturolocator.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(Guid? id);
        Task<T> Insert(T entity);
        Task<List<T>> InsertMany(List<T> entities);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);

        Task<T> GetByName(string name);
    }
}
