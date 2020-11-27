using AutoMapper;
using Domain.Models;
using Repository.Models;

namespace Repository
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EFSong, Song>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name));

            CreateMap<Song, EFSong>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name));

            CreateMap<EFAlbum, Album>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name))
                .ForMember("Picture", t => t.MapFrom(t => t.Picture));

            CreateMap<Album, EFAlbum>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name))
                .ForMember("Picture", t => t.MapFrom(t => t.Picture));

            CreateMap<EFArtist, Artist>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name))
                .ForMember("Picture", t => t.MapFrom(t => t.Picture))
                .ForMember("Description", t => t.MapFrom(t => t.Description));

            CreateMap<Artist, EFArtist>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name))
                .ForMember("Picture", t => t.MapFrom(t => t.Picture))
                .ForMember("Description", t => t.MapFrom(t => t.Description));
            
            CreateMap<EFTag, Tag>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name))
                .ForMember("Description", t => t.MapFrom(t => t.Description));
            
            CreateMap<Tag, EFTag>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name))
                .ForMember("Description", t => t.MapFrom(t => t.Description));
        }
    }
}