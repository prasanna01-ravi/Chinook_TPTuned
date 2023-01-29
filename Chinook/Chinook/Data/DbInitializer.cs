using Chinook.Core;

namespace Chinook.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ChinookContext context)
        {
            long favouritePlaylistId = Constant.PlaylistId;
            if (!context.Playlists.Any(p => p.Name.Equals(Constant.MyFavTracks)))
            {
                context.Playlists.Add(new Models.Playlist { PlaylistId = favouritePlaylistId, Name = Constant.MyFavTracks });
                context.SaveChanges();
            }

            List<String> userIds = context.Users.Select(u => u.Id).ToList();
            List<String> favouritePlaylistUserIds = context.UserPlaylists.Where(u => u.PlaylistId == favouritePlaylistId)
                .Select(u => u.UserId).ToList();

            if (userIds.Count != favouritePlaylistUserIds.Count)
            {
                List<Models.UserPlaylist> userplayList = new List<Models.UserPlaylist>();
                foreach (string userId in userIds.Except(favouritePlaylistUserIds))
                {
                    userplayList.Add(new Models.UserPlaylist { PlaylistId = favouritePlaylistId, UserId = userId });
                }
                if (userplayList.Count > 0)
                {
                    context.UserPlaylists.AddRange(userplayList);
                    context.SaveChanges();
                }
            }
        }
    }
}
