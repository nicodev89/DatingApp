using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, MemberDto>()
            .ForMember(destination => destination.Age, origin => origin.MapFrom(s => s.DateOfBirth.CalculateAge()))
            .ForMember(destination => destination.PhotoUrl, origin => origin.MapFrom(s => s.Photos.FirstOrDefault(photo => photo.IsMain)!.Url));

            CreateMap<Photo, PhotoDto>();
        }
    }
}