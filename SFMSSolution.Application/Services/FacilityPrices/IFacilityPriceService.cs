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
        Task<ApiResponse<string>> CreatePriceAsync(FacilityPriceCreateRequestDto request);

        Task<(IEnumerable<FacilityPriceDto> Prices, int TotalCount)> GetAllAsync(string? facilityName, int pageNumber, int pageSize);
        Task<(IEnumerable<FacilityPriceDto> Prices, int TotalCount)> GetByOwnerAsync(Guid ownerId, string? facilityName, int pageNumber, int pageSize);
        Task<ApiResponse<string>> UpdatePriceAsync(FacilityPriceUpdateRequestDto request);

        Task<FacilityPriceDto?> GetByIdAsync(Guid id);

        Task<ApiResponse<string>> DeleteAsync(Guid id);
    }
}
