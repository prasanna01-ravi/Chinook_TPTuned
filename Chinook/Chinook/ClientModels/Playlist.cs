namespace Chinook.ClientModels;

public class Playlist: BaseClientModel
{
    public long PlaylistId { get; set; }
    public string Name { get; set; }
    public bool IsUserPlaylist { get; set; }
    public List<PlaylistTrack> Tracks { get; set; }
}