using AutoMapper;
using Chinook.ClientModels;
using Chinook.Data;
using Chinook.Data.UnitOfWork;
using Chinook.Services.Interface;
using System.Linq.Expressions;

namespace Chinook.Services.Implementation
{
    public class UserPlaylistService : BaseService<UserPlaylist, Models.UserPlaylist>, IUserPlaylistService
    {
        public UserPlaylistService(IUserPlaylistRepository repository, ILogger<UserPlaylistService> logger,
            IMapper mapper, IUnitOfWork uow) : base(repository, logger, mapper, uow)
        {
        }

        public async Task<bool> AddToUserPlayList(long playlistId, string userId)
        {
            Logger.LogInformation($"Add playlist to user specific playlist {playlistId} & {userId}");
            try
            {
                if (playlistId > 0 && !String.IsNullOrEmpty(userId))
                {
                    await Repository.AddAsync(new Models.UserPlaylist() { PlaylistId = playlistId, UserId = userId });
                    await UOW.Commit();
                    return true;
                }
            }
            catch(Exception ex)
            {
                Logger.LogError($"Failed to add user play list {ex.Message}", ex);
            }
            return false;
        }

        public async Task<List<Playlist>> GetAllUserPlaylist(Expression<Func<Models.UserPlaylist, bool>> expression)
        {
            return Mapper.Map<List<Playlist>>((await Repository.GetAllByCondition(expression)).Select(up => up.Playlist).ToList());
        }

        public async Task<bool> RemoveUserPaylist(long playlistId, string userId)
        {
            Logger.LogInformation($"Remove playlist to user specific playlist {playlistId} & {userId}");
            try
            {
                if (playlistId > 0 && !String.IsNullOrEmpty(userId))
                {
                    await ((IUserPlaylistRepository)Repository).RemoveAsync(playlistId, userId);
                    await UOW.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to add user play list {ex.Message}", ex);
            }
            return false;
        }
    }
}
