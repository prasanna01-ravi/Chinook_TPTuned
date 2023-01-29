using AutoMapper;
using Chinook.ClientModels;
using Chinook.Data;
using Chinook.Data.UnitOfWork;
using Chinook.Pages;
using Chinook.Services.Interface;

namespace Chinook.Services.Implementation
{
    public class PlaylistService : BaseService<Playlist, Models.Playlist>, IPlaylistService
    {
        public PlaylistService(IPlaylistRepository repository, ILogger<PlaylistService> logger, IMapper mapper, IUnitOfWork uow)
            : base(repository, logger, mapper, uow)
        {
        }

        public async Task<List<Playlist>> GetAllAsync(string userId)
        {
            Logger.LogInformation($"Get all playlist information; user id: {userId}");
            if (!String.IsNullOrEmpty(userId))
            {
                return (await ((IPlaylistRepository)Repository).GetAll()).Select(t => new Playlist()
                {
                    Name = t.Name,
                    PlaylistId = t.PlaylistId,
                    IsUserPlaylist = t.UserPlaylists.Any(up => up.UserId.Equals(userId))
                }).ToList();
            }
            return null;
        }

        public async Task<Playlist> GetById(long playListId, string userId)
        {
            Logger.LogInformation($"Get Detailed information of Playlist by Id; Input received for {playListId} & {userId}");
            if (playListId > 0 && !String.IsNullOrEmpty(userId))
            {
                var playList = (await ((IPlaylistRepository)Repository).GetAdvancedDetById(playListId));
                if (playList != null && playList.Tracks != null)
                {
                    var result = Mapper.Map<Playlist>(playList);

                    result.Tracks = playList.Tracks.Select(t => new ClientModels.PlaylistTrack()
                    {
                        AlbumTitle = t?.Album?.Title ?? "",
                        ArtistName = t?.Album?.Artist?.Name ?? "",
                        TrackId = t.TrackId,
                        TrackName = t.Name,
                        IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == userId &&
                        up.Playlist.Name == "Favorites")).Any()
                    }).ToList();

                    return result;
                }
                else
                {
                    Logger.LogError($"Playlist not found for {playListId}");
                }
            }
            else
            {
                Logger.LogError($"Invalid input for {playListId} & {userId}");                
            }
            return null;
        }

        public async Task<bool> RenamePlaylist(long playlistId, string playlistName)
        {
            Logger.LogInformation($"Rename the playlist {playlistId} to {playlistName}");
            try
            {
                if (playlistId > 0 && !String.IsNullOrEmpty(playlistName))
                {
                    var playList = (await ((IPlaylistRepository)Repository).GetById(playlistId));
                    if(playList != null)
                    {
                        playList.Name = playlistName;
                        await Repository.UpdateAsync(playList);
                        await UOW.Commit();
                        Logger.LogInformation("Successfully renamed the playlist");
                    }                    
                }
            }
            catch(Exception e)
            {
                Logger.LogError($"Failed to rename the play list with id {playlistId}, {e.Message}", e);
            }
            return false;
        }

        public async Task<bool> RemovePlaylist(long playlistId)
        {
            Logger.LogInformation($"Removing the playlist {playlistId}");
            try
            {
                if (playlistId > 0)
                {
                    var playList = (await((IPlaylistRepository)Repository).GetById(playlistId));
                    if (playList != null)
                    {
                        await Repository.RemoveAsync(playList);
                        await UOW.Commit();
                        Logger.LogInformation("Successfully removed the playlist");
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError($"Failed to rename the play list with id {playlistId}, {e.Message}", e);
            }
            return false;
        }
    }
}
