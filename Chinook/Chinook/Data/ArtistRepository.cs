using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chinook.Data
{
    /// <summary>
    /// The ArtistRepository
    /// </summary>
    public class ArtistRepository : BaseRepository<Artist>, IArtistRepostory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistRepository" /> class.
        /// </summary>
        /// <param name="context">The Artist Context</param>
        public ArtistRepository(DbSet<Artist> context) : base(context)
        {
        }

        /// <summary>
        /// Get the advanced details of Authors
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns></returns>
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
