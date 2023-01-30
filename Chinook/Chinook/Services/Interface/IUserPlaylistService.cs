using Chinook.ClientModels;
using System.Linq.Expressions;

namespace Chinook.Services.Interface
{
    /// <summary>
    /// The IUserPlaylistService
    /// </summary>
    public interface IUserPlaylistService : IBaseService<UserPlaylist, Models.UserPlaylist>
    {
        /// <summary>
        /// Add the playlist to user
        /// </summary>
        /// <param name="userId">The Id of the curent user</param>
        /// <param name="playlistId">The instance of Track</param>
        /// <returns></returns>
        Task<bool> AddToUserPlayList(long playlistId, string userId);

        /// <summary>
        /// Get All the User playlist
        /// </summary>
        /// <param name="expression">The condition to extract user playlist</param>
        Task<List<Playlist>> GetAllUserPlaylist(Expression<Func<Models.UserPlaylist, bool>> expression);

        /// <summary>
        /// Remove the playlist from user
        /// </summary>
        /// <param name="userId">The Id of the curent user</param>
        /// <param name="playlistId">The instance of Track</param>
        Task<bool> RemoveUserPaylist(long playlistId, string userId);
    }
}
