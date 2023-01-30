using Chinook.Models;

namespace Chinook.Data
{
    /// <summary>
    /// The IUserPlaylistRepository
    /// </summary>
    public interface IUserPlaylistRepository: IBaseRepository<UserPlaylist>
    {
        /// <summary>
        /// Remove the play list from user list
        /// </summary>
        /// <param name="playlistId">The Id of the playlist</param>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        Task<bool> RemoveAsync(long playlistId, String userId);
    }
}
