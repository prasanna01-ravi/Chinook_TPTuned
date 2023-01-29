using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chinook.Data
{
    public class UserPlaylistRepository : BaseRepository<UserPlaylist>, IUserPlaylistRepository
    {
        public UserPlaylistRepository(DbSet<UserPlaylist> context) : base(context)
        {
        }

        public async override Task<List<UserPlaylist>> GetAllByCondition(Expression<Func<UserPlaylist, bool>> expression)
        {
            return await Context.Include(up => up.Playlist).Where(expression).ToListAsync();
        }

        public async Task<bool> RemoveAsync(long playlistId, String userId)
        {
            try
            {
                var entity = await Context.Where(up => up.PlaylistId == playlistId && up.UserId.Equals(userId))
                .FirstOrDefaultAsync();

                if (entity != null)
                {
                    Context.Remove(entity);
                    return true;
                }
            }
            catch(Exception e) 
            { 
                
            }
            return false;
        }
    }
}
