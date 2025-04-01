using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Application.DataTransferObjects.Booking;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFMSSolution.Application.DataTransferObjects.EmailTemplate.Request;
using SFMSSolution.Application.DataTransferObjects.EmailTemplate;

namespace SFMSSolution.Application.Mapping.MappingProfiles
{
    public class EmailTemplateProfile : Profile
    {
        public EmailTemplateProfile()
        {
            CreateMap<EmailTemplate, EmailTemplateDto>();
            CreateMap<EmailTemplateCreateRequestDto, EmailTemplate>();
            CreateMap<EmailTemplateUpdateRequestDto, EmailTemplate>();

        }
    }
}
