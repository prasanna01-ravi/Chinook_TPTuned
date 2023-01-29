namespace Chinook.ClientModels
{
    public class UserPlaylist: BaseClientModel
    {
        public long PlaylistId { get; set; }
        public string Name { get; set; }
        public bool IsUserPlaylist { get; set; }
    }
}
