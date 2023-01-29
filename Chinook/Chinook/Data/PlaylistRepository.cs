using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chinook.Data
{
    public class PlaylistRepository : BaseRepository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(DbSet<Playlist> context) : base(context)
        {
        }

        public async override Task<List<Playlist>> GetAll()
        {
            return await Context.Include(a => a.UserPlaylists).ToListAsync();
        }

        public async Task<Playlist> GetAdvancedDetById(long playlistId)
        {
            return await Context.Include(a => a.Tracks).ThenInclude(a => a.Album).ThenInclude(a => a.Artist)
                .Include(a => a.Tracks).ThenInclude(a => a.Playlists).ThenInclude(a => a.UserPlaylists)
                .Where(p => p.PlaylistId == playlistId)
                .FirstOrDefaultAsync();
        }

        public async Task<Playlist> GetById(long playlistId)
        {
            return await Context.Where(p => p.PlaylistId == playlistId)
                .FirstOrDefaultAsync();
        }
    }
}
