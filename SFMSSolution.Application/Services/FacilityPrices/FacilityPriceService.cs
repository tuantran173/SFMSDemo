using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice.Request;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice;
using SFMSSolution.Application.Services.FacilityPrices;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using SFMSSolution.Response;

namespace SFMSSolution.Application.Services
{
    public class FacilityPriceService : IFacilityPriceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FacilityPriceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> AddOrUpdatePriceAsync(FacilityPriceCreateRequestDto request)
        {
            var finalPrice = request.BasePrice * request.Coefficient;

            var newFacilityPrice = new FacilityPrice
            {
                Id = Guid.NewGuid(),
                FacilityTimeSlotId = request.FacilityTimeSlotId,
                Coefficient = request.Coefficient,
                BasePrice = request.BasePrice,
                FinalPrice = finalPrice,
                FacilityType = request.FacilityType,
                CreatedDate = DateTime.UtcNow
            };

            await _unitOfWork.FacilityPriceRepository.AddAsync(newFacilityPrice);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<string>("New price added successfully.");
        }

        public async Task<(IEnumerable<FacilityPriceDto> Prices, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var (entities, totalCount) = await _unitOfWork.FacilityPriceRepository.GetAllWithTimeSlotAsync(pageNumber, pageSize);
            var mapped = _mapper.Map<IEnumerable<FacilityPriceDto>>(entities);
            return (mapped, totalCount);
        }

        public async Task<List<FacilityPriceDto>> GetPricesByTimeSlotAsync(Guid facilityTimeSlotId)
        {
            var prices = await _unitOfWork.FacilityPriceRepository.GetByFacilityTimeSlotIdAsync(facilityTimeSlotId);
            return _mapper.Map<List<FacilityPriceDto>>(prices);
        }

        public async Task<FacilityPriceDto?> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.FacilityPriceRepository.GetByIdWithTimeSlotAsync(id);
            return entity == null ? null : _mapper.Map<FacilityPriceDto>(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _unitOfWork.FacilityPriceRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
