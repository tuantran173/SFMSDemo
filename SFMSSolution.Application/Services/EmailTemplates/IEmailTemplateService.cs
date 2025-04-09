using SFMSSolution.Application.DataTransferObjects.EmailTemplate;
using SFMSSolution.Application.DataTransferObjects.EmailTemplate.Request;
using SFMSSolution.Response;

namespace SFMSSolution.Application.Services.EmailTemplates
{
    public interface IEmailTemplateService
    {
        Task<ApiResponse<IEnumerable<EmailTemplateDto>>> GetAllTemplatesAsync();
        Task<ApiResponse<EmailTemplateDto>> GetByIdAsync(Guid id);
        Task<ApiResponse<string>> CreateAsync(EmailTemplateCreateRequestDto dto);
        Task<ApiResponse<string>> UpdateAsync(EmailTemplateUpdateRequestDto dto);
        Task<ApiResponse<string>> DeleteAsync(Guid id);
        Task<ApiResponse<string>> SendFromTemplateAsync(string templateName, string toEmail, Dictionary<string, string> placeholders);
    }
}
