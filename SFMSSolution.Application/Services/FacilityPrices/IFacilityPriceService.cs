using SFMSSolution.Application.DataTransferObjects.FacilityPrice;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice.Request;
using SFMSSolution.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Services.FacilityPrices
{
    public interface IFacilityPriceService
    {
        Task<ApiResponse<string>> AddOrUpdatePriceAsync(FacilityPriceCreateRequestDto request);
        Task<(IEnumerable<FacilityPriceDto> Prices, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<List<FacilityPriceDto>> GetPricesByTimeSlotAsync(Guid facilityTimeSlotId);
        Task<FacilityPriceDto?> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
