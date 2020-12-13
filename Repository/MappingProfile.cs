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
                .ForMember("Name", t => t.MapFrom(t => t.Name))
                .ForMember("FilePath", t => t.MapFrom(t => t.FilePath));

            CreateMap<Song, EFSong>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name))
                .ForMember("FilePath", t => t.MapFrom(t => t.FilePath));

            CreateMap<EFAlbum, Album>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name))
                .ForMember("ImagePath", t => t.MapFrom(t => t.ImagePath));

            CreateMap<Album, EFAlbum>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name))
                .ForMember("ImagePath", t => t.MapFrom(t => t.ImagePath));

            CreateMap<EFArtist, Artist>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name))
                .ForMember("ImagePath", t => t.MapFrom(t => t.ImagePath))
                .ForMember("Description", t => t.MapFrom(t => t.Description));

            CreateMap<Artist, EFArtist>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name))
                .ForMember("ImagePath", t => t.MapFrom(t => t.ImagePath))
                .ForMember("Description", t => t.MapFrom(t => t.Description));

            CreateMap<EFTag, Tag>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name))
                .ForMember("UserId", t => t.MapFrom(t => t.UserId))
                .ForMember("Verified", t => t.MapFrom(t => t.Verified));
                
            CreateMap<Tag, EFTag>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name))
                .ForMember("UserId", t => t.MapFrom(t => t.UserId))
                .ForMember("Verified", t => t.MapFrom(t => t.Verified));

            CreateMap<EFUser, User>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("UserName", t => t.MapFrom(t => t.UserName))
                .ForMember("Email", t => t.MapFrom(t => t.Email))
                .ForMember("EmailConfirmed", t => t.MapFrom(t => t.EmailConfirmed))
                .ForMember("PasswordHash", t => t.MapFrom(t => t.PasswordHash));
            
            CreateMap<EFUserRole, UserRole>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name));
            
            CreateMap<UserRole, EFUserRole>()
                .ForMember("Id", t => t.MapFrom(t => t.Id))
                .ForMember("Name", t => t.MapFrom(t => t.Name));
            
        }
    }
}