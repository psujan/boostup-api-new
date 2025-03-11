namespace Boostup.API.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(int id);
        Task<T?> Delete(int id);
        Task<T?> Add(T t);
    }
}
