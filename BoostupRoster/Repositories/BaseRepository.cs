using Boostup.API.Data;
using Boostup.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boostup.API.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext dbContext;

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

        // Implement Update in Derived Class
        // To Handle ForeignKey and Other Properties Modification If Needed
        public virtual async Task<T?> Update(int id , T updatedT)
        {
           throw new NotImplementedException();
        }

        public virtual async Task<T?> GetById(int id)
        {
           return await dbContext.Set<T>().FindAsync(id);
        }
        public virtual async Task<IEnumerable<T>?> GetPaginated(int pageNumber , int pageSize)
        {
           return await dbContext.Set<T>().Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }
    }
}
