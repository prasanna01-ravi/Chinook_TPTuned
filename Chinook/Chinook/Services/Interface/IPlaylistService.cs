using Chinook.ClientModels;

namespace Chinook.Services.Interface
{
    public interface IPlaylistService : IBaseService<Playlist, Models.Playlist>
    {
        Task<List<Playlist>> GetAllAsync(string userId);
        Task<Playlist> GetById(long playListId, string userId);
        Task<bool> RenamePlaylist(long playlistId, string playlistName);
        Task<bool> RemovePlaylist(long playlistId);
    }
}
