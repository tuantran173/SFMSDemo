using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Admin.Request;
using SFMSSolution.Application.DataTransferObjects.Admin;
using SFMSSolution.Domain.Entities;
using System.Linq;
using SFMSSolution.Domain.Enums;

namespace SFMSSolution.Application.Mapping.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status == EntityStatus.Active));

            CreateMap<UpdateUserRequestDto, User>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsActive ? EntityStatus.Active : EntityStatus.Inactive));
        }
    }
}
