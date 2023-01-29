using Chinook.ClientModels;
using System.Linq.Expressions;

namespace Chinook.Services.Interface
{
    public interface IUserPlaylistService : IBaseService<UserPlaylist, Models.UserPlaylist>
    {
        Task<bool> AddToUserPlayList(long playlistId, string userId);
        Task<List<Playlist>> GetAllUserPlaylist(Expression<Func<Models.UserPlaylist, bool>> expression);
        Task<bool> RemoveUserPaylist(long playlistId, string userId);
    }
}
