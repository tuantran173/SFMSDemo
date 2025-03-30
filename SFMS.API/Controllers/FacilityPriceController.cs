using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice.Request;
using SFMSSolution.Application.Services.FacilityPrices;

namespace SFMSSolution.API.Controllers
{
    [Authorize("Admin, Owner")]
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityPriceController : ControllerBase
    {
        private readonly IFacilityPriceService _facilityPriceService;

        public FacilityPriceController(IFacilityPriceService facilityPriceService)
        {
            _facilityPriceService = facilityPriceService;
        }

        // API để thêm hoặc cập nhật giá cho khung giờ cụ thể
        [HttpPost("add-or-update")]
        public async Task<IActionResult> AddOrUpdatePrice([FromBody] FacilityPriceCreateRequestDto request)
        {
            var result = await _facilityPriceService.AddOrUpdatePriceAsync(request);
            if (result)
                return Ok(new { message = "Facility Price added/updated successfully" });

            return BadRequest(new { message = "Failed to add/update Facility Price" });
        }

        // Lấy tất cả các giá áp dụng cho một khung giờ cụ thể
        [HttpGet("get-by-timeslot/{facilityTimeSlotId}")]
        public async Task<IActionResult> GetPricesByTimeSlot(Guid facilityTimeSlotId)
        {
            var prices = await _facilityPriceService.GetPricesByTimeSlotAsync(facilityTimeSlotId);
            return Ok(prices);
        }
    }
}
