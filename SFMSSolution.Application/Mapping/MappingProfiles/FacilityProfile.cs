using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Facility.Request;
using SFMSSolution.Application.DataTransferObjects.Facility;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Mapping.MappingProfiles
{
    public class FacilityProfile : Profile
    {
        public FacilityProfile()
        {
            CreateMap<Facility, FacilityDto>();
            CreateMap<FacilityCreateRequestDto, Facility>();
            CreateMap<Facility, FacilityDto>()
    .ForMember(dest => dest.OwnerPhone, opt => opt.MapFrom(src => src.Owner.Phone));
        }

    }

}
