using Chinook.Models;
using System.Linq.Expressions;

namespace Chinook.Data
{
    /// <summary>
    /// The IArtistRepostory
    /// </summary>
    public interface IArtistRepostory: IBaseRepository<Artist>
    {
        /// <summary>
        /// Get the advanced details of Authors
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns></returns>
        Task<List<Artist>> GetAdvancedDet(Expression<Func<Artist, bool>> expression);
    }
}
