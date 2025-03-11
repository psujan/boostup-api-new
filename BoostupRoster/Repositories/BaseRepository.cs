using Boostup.API.Data;
using Boostup.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boostup.API.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public virtual async Task<T?> Add(T t)
        {
           dbContext.Set<T>().Add(t);
           await dbContext.SaveChangesAsync();
           return t;

        }

        public virtual async Task<T?> Delete(int id)
        {
            var row = await dbContext.Set<T>().FindAsync(id);
            if(row == null)
            {
                 throw new Exception("Row Not Found");
            }
            dbContext.Set<T>().Remove(row);
            await dbContext.SaveChangesAsync();
            return row;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> GetById(int id)
        {
           return await dbContext.Set<T>().FindAsync(id);
        }

        //public Task<T?> Update(int id, T t)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
