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
            var templates = await _emailTemplateService.GetAllTemplatesAsync();
            return Ok(templates);
        }

        // GET: api/email-templates/{id}
        [HttpGet("{id:guid}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var template = await _emailTemplateService.GetByIdAsync(id);
            if (template == null)
                return NotFound("Email template not found.");
            return Ok(template);
        }

        // POST: api/email-templates
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create([FromBody] EmailTemplateCreateRequestDto request)
        {
            var result = await _emailTemplateService.CreateAsync(request);
            return result ? Ok("Template created successfully.") : BadRequest("Failed to create template.");
        }

        // PUT: api/email-templates
        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Update([FromBody] EmailTemplateUpdateRequestDto request)
        {
            var result = await _emailTemplateService.UpdateAsync(request);
            return result ? Ok("Template updated successfully.") : NotFound("Template not found.");
        }

        // DELETE: api/email-templates/{id}
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _emailTemplateService.DeleteAsync(id);
            return result ? Ok("Template deleted successfully.") : NotFound("Template not found.");
        }
    }
}
