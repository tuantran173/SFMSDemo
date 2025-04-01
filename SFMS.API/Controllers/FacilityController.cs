using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.Facility.Request;
using SFMSSolution.Application.Services.Facilities;
using System.Security.Claims;

namespace SFMSSolution.API.Controllers
{
    [ApiController]
    [Route("api/facility")]
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
        [HttpGet("get-all-facilities")]
        public async Task<IActionResult> GetAllFacilities([FromQuery] string? name, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (facilities, totalCount) = await _facilityService.GetAllFacilitiesAsync(name, pageNumber, pageSize);
            return Ok(new
            {
                Data = facilities,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            });
        }

        [Authorize(Policy = "Owner")]
        [HttpGet("get-facilities-by-owner")]
        public async Task<IActionResult> GetFacilitiesByOwner(
            [FromQuery] string? name,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var ownerIdStr = User.FindFirstValue("sub");
            if (!Guid.TryParse(ownerIdStr, out var ownerId))
                return Unauthorized("Invalid owner ID.");

            var (facilities, totalCount) = await _facilityService.GetFacilitiesByOwnerAsync(ownerId, name, pageNumber, pageSize);

            return Ok(new
            {
                Data = facilities,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            });
        }

        [Authorize(Policy = "Owner")]
        [HttpPost("create-facility")]
        public async Task<IActionResult> CreateFacility([FromBody] FacilityCreateRequestDto request)
        {
            // Nếu bạn muốn set OwnerId từ token ở controller
            var ownerIdStr = User.FindFirstValue("sub");
            if (!Guid.TryParse(ownerIdStr, out var ownerId))
                return Unauthorized("Invalid owner ID.");

            request.OwnerId = ownerId;

            var result = await _facilityService.CreateFacilityAsync(request);
            if (!result)
                return BadRequest("Failed to create facility.");
            return Ok("Facility created successfully.");
        }

        [Authorize(Policy = "Owner")]
        [HttpPut("update-facility/{id:Guid}")]
        public async Task<IActionResult> UpdateFacility([FromBody] FacilityUpdateRequestDto request)
        {
            var result = await _facilityService.UpdateFacilityAsync(request);
            if (!result)
                return NotFound("Facility not found or update failed.");
            return Ok("Facility updated successfully.");
        }

        [Authorize(Policy = "Owner")]
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
