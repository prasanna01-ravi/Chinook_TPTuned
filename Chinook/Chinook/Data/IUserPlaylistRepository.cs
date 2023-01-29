using Chinook.Models;

namespace Chinook.Data
{
    public interface IUserPlaylistRepository: IBaseRepository<UserPlaylist>
    {
        Task<bool> RemoveAsync(long playlistId, String userId);
    }
}
