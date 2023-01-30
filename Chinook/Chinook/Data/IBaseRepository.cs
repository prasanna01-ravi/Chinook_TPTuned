using System.Linq.Expressions;

namespace Chinook.Data
{
    /// <summary>
    /// The IBaseRepository
    /// </summary>
    public interface IBaseRepository<T>
        where T : class
    {
        /// <summary>
        /// Add the entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Get All the instances
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAll();

        /// <summary>
        /// Get all the instance by condition
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns></returns>
        Task<List<T>> GetAllByCondition(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Update the entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Remove the entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        Task RemoveAsync(T entity);
    }
}
