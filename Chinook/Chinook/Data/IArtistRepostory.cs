using Chinook.Models;
using System.Linq.Expressions;

namespace Chinook.Data
{
    public interface IArtistRepostory: IBaseRepository<Artist>
    {
        Task<List<Artist>> GetAdvancedDet(Expression<Func<Artist, bool>> expression);
    }
}
