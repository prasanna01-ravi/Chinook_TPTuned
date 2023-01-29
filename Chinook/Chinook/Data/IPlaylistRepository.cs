using Chinook.Models;

namespace Chinook.Data
{
    public interface IPlaylistRepository: IBaseRepository<Playlist>
    {
        Task<bool> AddTrackToPlaylist(long playlistId, Track track);
        Task<Playlist> GetAdvancedDetById(long playlistId);
        Task<Playlist> GetById(long playlistId);
        Task<long> GetLastPlaylistId();
        Task<bool> RemoveTrackFromPlayList(long playlistId, long trackId);
    }
}
