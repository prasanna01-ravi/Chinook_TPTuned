using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chinook.Data
{
    public class ArtistRepository : BaseRepository<Artist>, IArtistRepostory
    {
        public ArtistRepository(DbSet<Artist> context) : base(context)
        {
        }

        public async Task<List<Artist>> GetAdvancedDet(Expression<Func<Artist, bool>> expression)
        {
            try
            {
                var result = await Context.Include(a => a.Albums).Where(expression).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }
    }
}
