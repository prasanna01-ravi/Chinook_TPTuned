using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chinook.Data
{
    /// <summary>
    /// The PlaylistRepository
    /// </summary>
    public class PlaylistRepository : BaseRepository<Playlist>, IPlaylistRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistRepository" /> class.
        /// </summary>
        /// <param name="context">The Playlist Context</param>
        public PlaylistRepository(DbSet<Playlist> context) : base(context)
        {
        }

        /// <summary>
        /// Add the track to playlist
        /// </summary>
        /// <param name="playlistId">The Id of the playlist</param>
        /// <param name="track">The instance of Track</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get All the Playlist
        /// </summary>
        /// <returns></returns>
        public async override Task<List<Playlist>> GetAll()
        {
            return await Context.Include(a => a.UserPlaylists).ToListAsync();
        }

        /// <summary>
        /// Get Advanced Details of Playlist By Id
        /// </summary>
        /// <param name="playlistId">The Id of the playlist</param>
        /// <returns></returns>
        public async Task<Playlist> GetAdvancedDetById(long playlistId)
        {
            return await Context.Include(a => a.Tracks).ThenInclude(a => a.Album).ThenInclude(a => a.Artist)
                .Include(a => a.Tracks).ThenInclude(a => a.Playlists).ThenInclude(a => a.UserPlaylists)
                .Where(p => p.PlaylistId == playlistId)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get the Playlist By Id
        /// </summary>
        /// <param name="playlistId">The Id of the playlist</param>
        /// <returns></returns>
        public async Task<Playlist> GetById(long playlistId)
        {
            return await Context.Where(p => p.PlaylistId == playlistId)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get the Id of last playlist
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Remove the track from playlist
        /// </summary>
        /// <param name="playlistId">The Id of the playlist</param>
        /// <param name="trackId">Id of the track</param>
        /// <returns></returns>
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
