using Chinook.ClientModels;

namespace Chinook.Services.Interface
{
    /// <summary>
    /// The IPlaylistService
    /// </summary>
    public interface IPlaylistService : IBaseService<Playlist, Models.Playlist>
    {
        /// <summary>
        /// Add the track to playlist
        /// </summary>
        /// <param name="trackId">The Id of the Track</param>
        /// <param name="playlistId">The id of the playlist</param>
        /// <param name="playListName">Name of the playlist</param>
        /// <returns></returns>
        Task<bool> AddTrackToPlaylistId(long trackId, long? playlistId, string playListName);

        /// <summary>
        /// Get All Playlist
        /// </summary>
        /// <param name="userId">The UserId of the current user</param>
        /// <returns></returns>
        Task<List<Playlist>> GetAllAsync(string userId);

        /// <summary>
        /// Get the Playlist by Id
        /// </summary>
        /// <param name="playListId">The Id of the playlist</param>
        /// <param name="userId">The UserId of the current user</param>
        /// <returns></returns>
        Task<Playlist> GetById(long playListId, string userId);

        /// <summary>
        /// Rename the playlist
        /// </summary>
        /// <param name="playlistId">The Id of the playlist</param>
        /// <param name="playlistName">The new name of the playlist</param>
        /// <returns></returns>
        Task<bool> RenamePlaylist(long playlistId, string playlistName);

        /// <summary>
        /// Remove the playlist
        /// </summary>
        /// <param name="playlistId">The Id of the playlist</param>
        /// <returns></returns>
        Task<bool> RemovePlaylist(long playlistId);

        /// <summary>
        /// Remove the track to playlist
        /// </summary>
        /// <param name="trackId">The Id of the Track</param>
        /// <param name="playlistId">The id of the playlist</param>
        /// <returns></returns>
        Task<bool> RemoveTrackFromPlaylist(long trackId, long playlistId);
    }
}
