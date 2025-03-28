﻿using AutoMapper;
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
            CreateMap<FacilityPrice, FacilityPriceDto>();
        }
    }
}
