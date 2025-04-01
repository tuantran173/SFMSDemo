using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Application.DataTransferObjects.Booking;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Mapping.MappingProfiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.Facility.Name))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.FacilityTimeSlot.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.FacilityTimeSlot.EndTime));

            // Mapping từ BookingCreateRequestDto sang Booking entity
            CreateMap<BookingCreateRequestDto, Booking>();

            // Mapping từ BookingUpdateRequestDto sang Booking entity
            CreateMap<BookingUpdateRequestDto, Booking>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());
        }
    }
}
