using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chinook.Data
{
    /// <summary>
    /// The Base Repository
    /// </summary>
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        protected DbSet<T> Context { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository" /> class.
        /// </summary>
        /// <param name="context">The Entity Context</param>
        public BaseRepository(DbSet<T> context)
        {
            Context = context;
        }

        /// <summary>
        /// Add the entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public virtual async Task<T> AddAsync(T entity)
        {
            return (await this.Context.AddAsync(entity)).Entity;
        }

        /// <summary>
        /// Get All the instances
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<T>> GetAll()
        {
            return await Context.ToListAsync();
        }

        /// <summary>
        /// Get all the instance by condition
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetAllByCondition(Expression<Func<T, bool>> expression)
        {
            return await Context.Where(expression).ToListAsync();
        }

        /// <summary>
        /// Update the entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public virtual async Task<T> UpdateAsync(T entity)
        {
            return (await Task.Run(() => Context.Update(entity)).ConfigureAwait(true)).Entity;
        }

        /// <summary>
        /// Remove the entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(T entity)
        {
            await Task.Run(() => Context.Remove(entity)).ConfigureAwait(true);
        }
    }
}
