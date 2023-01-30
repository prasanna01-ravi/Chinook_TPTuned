using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chinook.Data
{
    /// <summary>
    /// The UserPlaylistRepository
    /// </summary>
    public class UserPlaylistRepository : BaseRepository<UserPlaylist>, IUserPlaylistRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserPlaylistRepository" /> class.
        /// </summary>
        /// <param name="context">The UserPlaylist Context</param>
        public UserPlaylistRepository(DbSet<UserPlaylist> context) : base(context)
        {
        }

        /// <summary>
        /// Get All UserPlaylist by Expression
        /// </summary>
        /// <param name="expression">The condtiont to meet the user playlist</param>
        /// <returns></returns>
        public async override Task<List<UserPlaylist>> GetAllByCondition(Expression<Func<UserPlaylist, bool>> expression)
        {
            return await Context.Include(up => up.Playlist).Where(expression).ToListAsync();
        }

        /// <summary>
        /// Remove the play list from user list
        /// </summary>
        /// <param name="playlistId">The Id of the playlist</param>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
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
