using AutoMapper;
using Chinook.ClientModels;
using Chinook.Core;
using Chinook.Data;
using Chinook.Data.UnitOfWork;
using Chinook.Services.Interface;

namespace Chinook.Services.Implementation
{
    public class PlaylistService : BaseService<Playlist, Models.Playlist>, IPlaylistService
    {
        private readonly IBaseRepository<Models.Track> _trackRepository;
        public PlaylistService(IPlaylistRepository repository, ILogger<PlaylistService> logger, IMapper mapper, 
                IUnitOfWork uow, IBaseRepository<Models.Track> trackRepository)
            : base(repository, logger, mapper, uow)
        {
            _trackRepository = trackRepository ?? throw new ArgumentNullException(nameof(trackRepository)); 
        }

        public async Task<bool> AddTrackToPlaylistId(long trackId, long? playlistId, string playListName)
        {
            Logger.LogInformation($"Adding track to playlist; trackId: {trackId}, playlistId:{playlistId}, playListName:{playListName}");
            try
            {
                if (!String.IsNullOrEmpty(playListName))
                {
                    long lastPlaylistId = await ((IPlaylistRepository)Repository).GetLastPlaylistId();
                    playlistId = (await Repository.AddAsync(new Models.Playlist()
                    { Name = playListName, PlaylistId = lastPlaylistId + 1 }))?.PlaylistId;

                    await UOW.Commit();
                }

                if (playlistId != null && playlistId > 0 && trackId > 0)
                {
                    await ((IPlaylistRepository)Repository).AddTrackToPlaylist(playlistId.Value,
                        ((await _trackRepository.GetAllByCondition(x => x.TrackId == trackId)).FirstOrDefault()));
                    await UOW.Commit();
                    return true;
                }
            }
            catch(Exception e)
            {
                Logger.LogError($"Failed to add the track with id {trackId} to playlist Id {playlistId}, {e.Message}", e);
            }
            return false;
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
                        IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId.Equals(userId) &&
                        up.Playlist.Name.Equals(Constant.MyFavTracks))).Any()
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
                        return true;
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
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError($"Failed to rename the play list with id {playlistId}, {e.Message}", e);
            }
            return false;
        }

        public async Task<bool> RemoveTrackFromPlaylist(long trackId, long playlistId)
        {
            Logger.LogInformation($"Remove the track {trackId} from playlist {playlistId}");
            try
            {
                if (playlistId > 0 && trackId > 0)
                {
                    await ((IPlaylistRepository)Repository).RemoveTrackFromPlayList(playlistId, trackId);
                    await UOW.Commit();
                    Logger.LogInformation("Successfully removed the track from playlist");
                    return true;
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
