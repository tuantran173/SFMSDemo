using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice.Request;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Mapping.MappingProfiles
{
    public class FacilityPriceProfile : Profile
    {
        public FacilityPriceProfile()
        {
            CreateMap<FacilityPriceCreateRequestDto, FacilityPrice>();
            CreateMap<FacilityPrice, FacilityPriceDto>()
             .ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.Facility.Name))
             .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Facility.Address))
             .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Facility.ImageUrl))
             .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.FacilityTimeSlot.StartTime))
             .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.FacilityTimeSlot.EndTime));
        }
    }
}
