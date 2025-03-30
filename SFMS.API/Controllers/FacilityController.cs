using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.Facility.Request;
using SFMSSolution.Application.Services.Facilities;

namespace SFMSSolution.API.Controllers
{
    [Authorize(Roles = "Admin,Owner")]
    [ApiController]
    [Route("api/[controller]")]
    public class FacilityController : ControllerBase
    {
        private readonly IFacilityService _facilityService;

        public FacilityController(IFacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        [AllowAnonymous]
        [HttpGet("get-facility-by-id/{id:Guid}")]
        public async Task<IActionResult> GetFacility(Guid id)
        {
            var facility = await _facilityService.GetFacilityAsync(id);
            if (facility == null)
                return NotFound("Facility not found.");
            return Ok(facility);
        }

        [AllowAnonymous]
        [HttpGet("filter-facility")]
        public async Task<IActionResult> FilterFacilities(
            [FromQuery] Guid? categoryId,
            [FromQuery] string? location,
            [FromQuery] string? startTime,
            [FromQuery] string? endTime,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            TimeSpan? parsedStartTime = null;
            TimeSpan? parsedEndTime = null;

            if (!string.IsNullOrWhiteSpace(startTime) && TimeSpan.TryParse(startTime, out var sTime))
                parsedStartTime = sTime;

            if (!string.IsNullOrWhiteSpace(endTime) && TimeSpan.TryParse(endTime, out var eTime))
                parsedEndTime = eTime;

            var (facilities, totalCount) = await _facilityService.FilterFacilitiesAsync(
                categoryId, location, parsedStartTime, parsedEndTime, pageNumber, pageSize);

            return Ok(new
            {
                Data = facilities,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            });
        }

        [AllowAnonymous]
        [HttpGet("get-all-facilities")]
        public async Task<IActionResult> GetAllFacilities([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (facilities, totalCount) = await _facilityService.GetAllFacilitiesAsync(pageNumber, pageSize);
            return Ok(new
            {
                Data = facilities,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            });
        }

        [AllowAnonymous]
        [HttpGet("search-facility")]
        public async Task<IActionResult> SearchFacilitiesByName(
            [FromQuery] string name,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var (facilities, totalCount) = await _facilityService.SearchFacilitiesByNameAsync(name, pageNumber, pageSize);
            return Ok(new
            {
                Data = facilities,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            });
        }

        [HttpPost("create-facility")]
        public async Task<IActionResult> CreateFacility([FromBody] FacilityCreateRequestDto request)
        {
            var result = await _facilityService.CreateFacilityAsync(request);
            if (!result)
                return BadRequest("Failed to create facility.");
            return Ok("Facility created successfully.");
        }

        [HttpPut("update-facility/{id:Guid}")]
        public async Task<IActionResult> UpdateFacility(Guid id, [FromBody] FacilityUpdateRequestDto request)
        {
            var result = await _facilityService.UpdateFacilityAsync(id, request);
            if (!result)
                return NotFound("Facility not found or update failed.");
            return Ok("Facility updated successfully.");
        }

        [HttpDelete("delete-facility/{id:Guid}")]
        public async Task<IActionResult> DeleteFacility(Guid id)
        {
            var result = await _facilityService.DeleteFacilityAsync(id);
            if (!result)
                return NotFound("Facility not found or deletion failed.");
            return Ok("Facility deleted successfully.");
        }
    }
}
