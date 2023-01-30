using Chinook.ClientModels;
using System.Linq.Expressions;

namespace Chinook.Services.Interface
{
    /// <summary>
    /// The IArtistService
    /// </summary>
    public interface IArtistService : IBaseService<Artist, Models.Artist>
    {
        /// <summary>
        /// Get the advanced details of Authors
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns></returns>
        Task<List<Artist>> GetAdvancedDet(Expression<Func<Models.Artist, bool>> expression);
    }
}
