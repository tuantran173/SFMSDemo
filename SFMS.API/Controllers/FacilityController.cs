using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.Facility.Request;
using SFMSSolution.Application.Services.Facilities;

namespace SFMSSolution.API.Controllers
{
    [Authorize(Roles = "Facility Owner")]
    [ApiController]
    [Route("api/[controller]")]
    public class FacilityController : ControllerBase
    {
        private readonly IFacilityService _facilityService;

        public FacilityController(IFacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetFacility(Guid id)
        {
            var facility = await _facilityService.GetFacilityAsync(id);
            if (facility == null)
                return NotFound("Facility not found.");
            return Ok(facility);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFacilities()
        {
            var facilities = await _facilityService.GetAllFacilitiesAsync();
            return Ok(facilities);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFacility([FromBody] FacilityCreateRequestDto request)
        {
            var result = await _facilityService.CreateFacilityAsync(request);
            if (!result)
                return BadRequest("Failed to create facility.");
            return Ok("Facility created successfully.");
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateFacility(Guid id, [FromBody] FacilityUpdateRequestDto request)
        {
            var result = await _facilityService.UpdateFacilityAsync(id, request);
            if (!result)
                return NotFound("Facility not found or update failed.");
            return Ok("Facility updated successfully.");
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteFacility(Guid id)
        {
            var result = await _facilityService.DeleteFacilityAsync(id);
            if (!result)
                return NotFound("Facility not found or deletion failed.");
            return Ok("Facility deleted successfully.");
        }
    }

}