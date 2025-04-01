using SFMSSolution.Application.DataTransferObjects.EmailTemplate.Request;
using SFMSSolution.Application.DataTransferObjects.EmailTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Services.EmailTemplates
{
    public interface IEmailTemplateService
    {
        Task<IEnumerable<EmailTemplateDto>> GetAllTemplatesAsync();
        Task<EmailTemplateDto?> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(EmailTemplateCreateRequestDto dto);
        Task<bool> UpdateAsync(EmailTemplateUpdateRequestDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> SendFromTemplateAsync(string templateName, string toEmail, Dictionary<string, string> placeholders);
    }
}
