using Chinook.Models;

namespace Chinook.Data
{
    public interface IPlaylistRepository: IBaseRepository<Playlist>
    {
        Task<Playlist> GetAdvancedDetById(long playlistId);
        Task<Playlist> GetById(long playlistId);
    }
}
