using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Admin.Request;
using SFMSSolution.Application.DataTransferObjects.Admin;
using SFMSSolution.Domain.Entities;
using System.Linq;

namespace SFMSSolution.Application.Mapping.MappingProfiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : string.Empty))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status == Domain.Enums.EntityStatus.Active));

            CreateMap<UpdateUserRequestDto, User>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsActive ? Domain.Enums.EntityStatus.Active : Domain.Enums.EntityStatus.Inactive));
        }
    }
}
