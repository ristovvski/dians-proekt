using Culturolocator.Data;
using Culturolocator.Models;
using Culturolocator.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Culturolocator.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly DataContext DataContext;
        private DbSet<T> entities;

        public Repository(DataContext context)
        {
            DataContext = context;
            entities = context.Set<T>();
        }

        public async Task<T> Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            await DataContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Get(Guid? id)
        {
            return await entities.FirstAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetByName(string name)
        {
            return await entities.SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<T> Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Add(entity);
            await DataContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> InsertMany(List<T> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                throw new ArgumentNullException("entities");
            }

            entities.AddRange(entities);
            await DataContext.SaveChangesAsync();
            return entities;
        }

        public async Task<T> Update(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            await DataContext.SaveChangesAsync();
            return entity;
        }
    }
}
