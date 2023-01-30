using AutoMapper;

namespace Chinook.Services.Mapping
{
    public class TransformationDataMappingProfile: Profile
    {
        public TransformationDataMappingProfile()
        {
            CreateMap<Models.Artist, ClientModels.Artist>()
                .ForMember(x => x.AlbumCount, y => y.MapFrom(z => (z.Albums != null ? z.Albums.Count : 0)))
                .ReverseMap();

            CreateMap<Models.Playlist, ClientModels.Playlist>()
                .ForMember(x => x.Tracks, y => y.Ignore())
                .ForMember(x => x.IsUserPlaylist, y => y.Ignore())
                .ReverseMap();

            CreateMap<Models.Track, ClientModels.PlaylistTrack>()
                .ForMember(x => x.AlbumTitle, y => y.MapFrom(z => (z.Album != null? z.Album.Title : "")))
                .ForMember(x => x.ArtistName, y => y.MapFrom(z => (z.Album != null && z.Album.Artist != null)? z.Album.Artist.Name : ""))
                .ReverseMap();

            CreateMap<Models.UserPlaylist, ClientModels.UserPlaylist>()
                .ReverseMap();
        }
    }
}
