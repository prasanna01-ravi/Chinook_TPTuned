using System.Linq.Expressions;

namespace Chinook.Data
{
    public interface IBaseRepository<T>
        where T : class
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> GetAll();
        Task<List<T>> GetAllByCondition(Expression<Func<T, bool>> expression);
        Task<T> UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}
