using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice.Request;
using SFMSSolution.Application.Services.FacilityPrices;

namespace SFMSSolution.API.Controllers
{
    
    [ApiController]
    [Route("api/facility-price")]
    public class FacilityPriceController : ControllerBase
    {
        private readonly IFacilityPriceService _facilityPriceService;

        public FacilityPriceController(IFacilityPriceService facilityPriceService)
        {
            _facilityPriceService = facilityPriceService;
        }

        [HttpPost("add-or-update-price")]
        public async Task<IActionResult> AddOrUpdatePrice([FromBody] FacilityPriceCreateRequestDto request)
        {
            var result = await _facilityPriceService.AddOrUpdatePriceAsync(request);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpGet("get-price-by-timeslot/{timeSlotId:guid}")]
        public async Task<IActionResult> GetByTimeSlot(Guid timeSlotId)
        {
            var prices = await _facilityPriceService.GetPricesByTimeSlotAsync(timeSlotId);
            return Ok(prices);
        }

        [HttpGet("get-al-price")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (data, totalCount) = await _facilityPriceService.GetAllAsync(pageNumber, pageSize);
            return Ok(new
            {
                Data = data,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            });
        }

        [HttpGet("get-price-by-id/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _facilityPriceService.GetByIdAsync(id);
            return result == null ? NotFound("Facility price not found.") : Ok(result);
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _facilityPriceService.DeleteAsync(id);
            return deleted ? Ok("Deleted successfully.") : BadRequest("Failed to delete.");
        }
    }
}
