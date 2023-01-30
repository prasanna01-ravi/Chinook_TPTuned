using Chinook.ClientModels;
using System.Linq.Expressions;

namespace Chinook.Services.Interface
{
    /// <summary>
    /// The IBaseService
    /// </summary>
    public interface IBaseService<TObject, TEntity>
        where TObject : class, BaseClientModel
        where TEntity : class
    {
        /// <summary>
        /// Get all the instances
        /// </summary>
        /// <returns></returns>
        Task<List<TObject>> GetAllAsync();

        /// <summary>
        /// Get the advanced details of instances
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns></returns>
        Task<List<TObject>> GetAllByCondtionAsync(Expression<Func<TEntity, bool>> expression);
    }
}
