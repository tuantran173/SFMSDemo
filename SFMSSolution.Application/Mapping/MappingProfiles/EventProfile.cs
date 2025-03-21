using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Event.Request;
using SFMSSolution.Application.DataTransferObjects.Event;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Mapping.MappingProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDto>();
            CreateMap<EventCreateRequestDto, Event>();
            CreateMap<EventUpdateRequestDto, Event>();
        }
    }
}
