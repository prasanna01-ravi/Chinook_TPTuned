using Chinook.ClientModels;
using System.Linq.Expressions;

namespace Chinook.Services.Interface
{
    public interface IBaseService<TObject, TEntity>
        where TObject : class, BaseClientModel
        where TEntity : class
    {
        Task<List<TObject>> GetAllAsync();
        Task<List<TObject>> GetAllByCondtionAsync(Expression<Func<TEntity, bool>> expression);
    }
}
