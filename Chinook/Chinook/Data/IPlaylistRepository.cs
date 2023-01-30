using Chinook.Models;

namespace Chinook.Data
{
    /// <summary>
    /// The IPlaylistRepository
    /// </summary>
    public interface IPlaylistRepository: IBaseRepository<Playlist>
    {
        /// <summary>
        /// Add the track to playlist
        /// </summary>
        /// <param name="playlistId">The Id of the playlist</param>
        /// <param name="track">The instance of Track</param>
        /// <returns></returns>
        Task<bool> AddTrackToPlaylist(long playlistId, Track track);

        /// <summary>
        /// Get Advanced Details of Playlist By Id
        /// </summary>
        /// <param name="playlistId">The Id of the playlist</param>
        /// <returns></returns>
        Task<Playlist> GetAdvancedDetById(long playlistId);

        /// <summary>
        /// Get the Playlist By Id
        /// </summary>
        /// <param name="playlistId">The Id of the playlist</param>
        /// <returns></returns>
        Task<Playlist> GetById(long playlistId);

        /// <summary>
        /// Get the Id of last playlist
        /// </summary>
        /// <returns></returns>
        Task<long> GetLastPlaylistId();

        /// <summary>
        /// Remove the track to playlist
        /// </summary>
        /// <param name="playlistId">The Id of the playlist</param>
        /// <param name="trackId">Id of the track</param>
        /// <returns></returns>
        Task<bool> RemoveTrackFromPlayList(long playlistId, long trackId);
    }
}
