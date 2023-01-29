using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chinook.Data
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        protected DbSet<T> Context { get; set; }

        public BaseRepository(DbSet<T> context)
        {
            Context = context;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            return (await this.Context.AddAsync(entity)).Entity;
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await Context.ToListAsync();
        }

        public virtual async Task<List<T>> GetAllByCondition(Expression<Func<T, bool>> expression)
        {
            return await Context.Where(expression).ToListAsync();
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            return (await Task.Run(() => Context.Update(entity)).ConfigureAwait(true)).Entity;
        }

        public virtual async Task RemoveAsync(T entity)
        {
            await Task.Run(() => Context.Remove(entity)).ConfigureAwait(true);
        }
    }
}
