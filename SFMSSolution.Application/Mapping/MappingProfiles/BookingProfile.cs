using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Application.DataTransferObjects.Booking;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFMSSolution.Domain.Enums;

namespace SFMSSolution.Application.Mapping.MappingProfiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingCreateRequestDto, Booking>()
    .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => BookingStatus.Pending))
    .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));


            // Mapping từ BookingCreateRequestDto sang Booking entity
            CreateMap<BookingCreateRequestDto, Booking>();

            // Mapping từ BookingUpdateRequestDto sang Booking entity
            CreateMap<BookingUpdateRequestDto, Booking>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());
        }
    }
}
