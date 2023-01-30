namespace Chinook.ClientModels
{
    public class Artist: BaseClientModel
    {
        public long ArtistId { get; set; }
        public string? Name { get; set; }
        public int? AlbumCount { get; set; }
    }
}
