using Chinook.ClientModels;
using System.Linq.Expressions;

namespace Chinook.Services.Interface
{
    public interface IArtistService : IBaseService<Artist, Models.Artist>
    {
        Task<List<Artist>> GetAdvancedDet(Expression<Func<Models.Artist, bool>> expression);
    }
}
