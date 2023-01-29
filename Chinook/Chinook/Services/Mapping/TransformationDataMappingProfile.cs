using AutoMapper;

namespace Chinook.Services.Mapping
{
    public class TransformationDataMappingProfile: Profile
    {
        public TransformationDataMappingProfile()
        {
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
