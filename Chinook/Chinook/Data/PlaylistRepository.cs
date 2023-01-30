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

        public async Task<bool> AddTrackToPlaylist(long playlistId, Track track)
        {
            try
            {
                if (playlistId > 0)
                {
                    var playList = await Context.Include<Playlist, ICollection<Track>>(p => p.Tracks)
                        .Where(p => p.PlaylistId == playlistId).FirstOrDefaultAsync();

                    if (playList != null && track != null)
                    {
                        playList.Tracks.Add(track);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
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

        public async Task<long> GetLastPlaylistId()
        {
            try
            {
                return (await Context.OrderByDescending(p => p.PlaylistId)
                       .FirstOrDefaultAsync())?.PlaylistId ?? 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<bool> RemoveTrackFromPlayList(long playlistId, long trackId)
        {
            try
            {
                if (playlistId > 0 && trackId > 0)
                {
                    var playList = await Context.Include<Playlist, ICollection<Track>>(p => p.Tracks)
                        .Where(p => p.PlaylistId == playlistId).FirstOrDefaultAsync();

                    if (playList != null)
                    {
                        var tracks = playList.Tracks.Where(p => p.TrackId != trackId).ToList();
                        playList.Tracks = tracks;
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
