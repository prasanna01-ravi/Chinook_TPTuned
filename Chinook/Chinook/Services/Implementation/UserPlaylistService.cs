using AutoMapper;
using Chinook.ClientModels;
using Chinook.Data;
using Chinook.Data.UnitOfWork;
using Chinook.Services.Interface;
using System.Linq.Expressions;

namespace Chinook.Services.Implementation
{
    /// <summary>
    /// The UserPlaylistService
    /// </summary>
    public class UserPlaylistService : BaseService<UserPlaylist, Models.UserPlaylist>, IUserPlaylistService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserPlaylistService" /> class.
        /// </summary>
        /// <param name="repository">The IArtistRepostory</param>
        /// <param name="logger">The ArtistService Logger</param>
        /// <param name="mapper">The IMapper</param>
        /// <param name="uow">The Unit Of Work</param>
        public UserPlaylistService(IUserPlaylistRepository repository, ILogger<UserPlaylistService> logger,
            IMapper mapper, IUnitOfWork uow) : base(repository, logger, mapper, uow)
        {
        }

        /// <summary>
        /// Add the playlist to user
        /// </summary>
        /// <param name="userId">The Id of the curent user</param>
        /// <param name="playlistId">The instance of Track</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get All the User playlist
        /// </summary>
        /// <param name="expression">The condition to extract user playlist</param>
        public async Task<List<Playlist>> GetAllUserPlaylist(Expression<Func<Models.UserPlaylist, bool>> expression)
        {
            return Mapper.Map<List<Playlist>>((await Repository.GetAllByCondition(expression)).Select(up => up.Playlist).ToList());
        }

        /// <summary>
        /// Remove the playlist from user
        /// </summary>
        /// <param name="userId">The Id of the curent user</param>
        /// <param name="playlistId">The instance of Track</param>
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
