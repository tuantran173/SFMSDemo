using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.EmailTemplate.Request;
using SFMSSolution.Application.Services.EmailTemplates;

namespace SFMSSolution.API.Controllers
{
    [ApiController]
    [Route("api/email-templates")]
    public class EmailTemplateController : ControllerBase
    {
        private readonly IEmailTemplateService _emailTemplateService;

        public EmailTemplateController(IEmailTemplateService emailTemplateService)
        {
            _emailTemplateService = emailTemplateService;
        }

        // GET: api/email-templates
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _emailTemplateService.GetAllTemplatesAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // GET: api/email-templates/{id}
        [HttpGet("{id:guid}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _emailTemplateService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // POST: api/email-templates
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create([FromBody] EmailTemplateCreateRequestDto request)
        {
            var response = await _emailTemplateService.CreateAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // PUT: api/email-templates
        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Update([FromBody] EmailTemplateUpdateRequestDto request)
        {
            var response = await _emailTemplateService.UpdateAsync(request);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // DELETE: api/email-templates/{id}
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _emailTemplateService.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
