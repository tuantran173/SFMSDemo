using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.User;
using SFMSSolution.Application.DataTransferObjects.User.Request;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Mapping.MappingProfiles
{
    public class UserProfileProfile : Profile
    {
        public UserProfileProfile()
        {
            CreateMap<User, ProfileResponseDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src =>
                    src.UserRoles != null && src.UserRoles.Any()
                        ? src.UserRoles.First().Role.Name
                        : string.Empty));

            CreateMap<UpdateProfileRequestDto, User>();
        }
    }
}
