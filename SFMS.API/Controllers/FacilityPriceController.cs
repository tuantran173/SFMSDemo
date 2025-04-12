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

        [HttpPost("create-price")]
        [Authorize(Policy = "Owner")]
        public async Task<IActionResult> Create([FromBody] FacilityPriceCreateRequestDto request)
        {
            var result = await _facilityPriceService.CreatePriceAsync(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("update-price")]
        [Authorize(Policy = "Owner")]
        public async Task<IActionResult> UpdatePrice([FromBody] FacilityPriceUpdateRequestDto request)
        {
            var result = await _facilityPriceService.UpdatePriceAsync(request);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        //[HttpGet("list-price")]
        //[Authorize(Policy = "Owner")]
        //public async Task<IActionResult> GetAll(
        //    [FromQuery] string? name,
        //    [FromQuery] int pageNumber = 1,
        //    [FromQuery] int pageSize = 10)
        //{
        //    var (data, total) = await _facilityPriceService.GetAllAsync(name, pageNumber, pageSize);
        //    return Ok(new
        //    {
        //        Data = data,
        //        TotalCount = total,
        //        CurrentPage = pageNumber,
        //        PageSize = pageSize
        //    });
        //}

        [HttpGet("list-price")]
        [Authorize(Policy = "Owner")]
        public async Task<IActionResult> GetByOwner(
    [FromQuery] string? facilityName,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
        {
            var ownerIdClaim = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(ownerIdClaim) || !Guid.TryParse(ownerIdClaim, out var ownerId))
                return Unauthorized();

            var (prices, totalCount) = await _facilityPriceService.GetByOwnerAsync(ownerId, facilityName, pageNumber, pageSize);

            return Ok(new
            {
                Data = prices,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            });
        }

        [HttpGet("get-price-by-id/{id:Guid}")]
        [Authorize(Policy = "Owner")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _facilityPriceService.GetByIdAsync(id);
            return result == null ? NotFound("Price not found") : Ok(result);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "Owner")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _facilityPriceService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
