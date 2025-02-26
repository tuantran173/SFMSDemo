using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Auth;
using SFMSSolution.Application.DataTransferObjects.Auth.Request;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Mapping.MappingProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<User, AuthResponseDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src =>
                    src.UserRoles != null && src.UserRoles.Any()
                        ? src.UserRoles.First().Role.Name
                        : string.Empty));

            CreateMap<RegisterRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Hash mật khẩu sau
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // ID tự động sinh
        }
    }
}
