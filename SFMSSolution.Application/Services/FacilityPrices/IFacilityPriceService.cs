using SFMSSolution.Application.DataTransferObjects.FacilityPrice;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Services.FacilityPrices
{
    public interface IFacilityPriceService
    {
        Task<bool> AddOrUpdatePriceAsync(FacilityPriceCreateRequestDto request);
        Task<List<FacilityPriceDto>> GetPricesByTimeSlotAsync(Guid facilityTimeSlotId);
    }
}
