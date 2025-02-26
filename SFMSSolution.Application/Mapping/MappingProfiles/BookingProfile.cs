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
            // Mapping từ Booking entity sang BookingDto
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // Mapping từ BookingCreateRequestDto sang Booking entity
            CreateMap<BookingCreateRequestDto, Booking>();

            // Mapping từ BookingUpdateRequestDto sang Booking entity
            CreateMap<BookingUpdateRequestDto, Booking>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());
        }
    }
}
