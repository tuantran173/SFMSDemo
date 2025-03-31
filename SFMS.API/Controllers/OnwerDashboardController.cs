using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.Services.Reports;

namespace SFMSSolution.API.Controllers
{
    [Authorize(Policy = "Admin,Owner")]
    [Route("api/owner/reports")]
    [ApiController]
    public class OwnerDashboardController : ControllerBase
    {
        private readonly IReportService _reportService;

        public OwnerDashboardController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("general")]
        public async Task<IActionResult> GetGeneralReport()
        {
            var result = await _reportService.GetGeneralReportAsync();
            return Ok(result);
        }

        [HttpGet("facility-report/{facilityId}")]
        public async Task<IActionResult> GetFacilityReport(Guid facilityId)
        {
            var result = await _reportService.GetFacilityReportAsync(facilityId);
            return Ok(result);
        }

        [HttpGet("top-booked-facilities")]
        public async Task<IActionResult> GetTopBookedFacilities()
        {
            var result = await _reportService.GetTopBookedFacilitiesAsync();
            return Ok(result);
        }
    }
}
