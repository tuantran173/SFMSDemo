using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.EmailTemplate.Request;
using SFMSSolution.Application.DataTransferObjects.EmailTemplate;
using SFMSSolution.Application.Extensions;
using SFMSSolution.Application.ExternalService.Email;
using SFMSSolution.Application.Services.EmailTemplates;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using SFMSSolution.Response;

public class EmailTemplateService : IEmailTemplateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public EmailTemplateService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task<ApiResponse<IEnumerable<EmailTemplateDto>>> GetAllTemplatesAsync()
    {
        var templates = await _unitOfWork.EmailTemplateRepository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<EmailTemplateDto>>(templates);
        return new ApiResponse<IEnumerable<EmailTemplateDto>>(dtos);
    }

    public async Task<ApiResponse<EmailTemplateDto>> GetByIdAsync(Guid id)
    {
        var template = await _unitOfWork.EmailTemplateRepository.GetByIdAsync(id);
        if (template == null)
            return new ApiResponse<EmailTemplateDto>("Template not found.");

        var dto = _mapper.Map<EmailTemplateDto>(template);
        return new ApiResponse<EmailTemplateDto>(dto);
    }

    public async Task<ApiResponse<string>> CreateAsync(EmailTemplateCreateRequestDto dto)
    {
        var template = _mapper.Map<EmailTemplate>(dto);
        await _unitOfWork.EmailTemplateRepository.AddAsync(template);
        await _unitOfWork.CompleteAsync();
        return new ApiResponse<string>(true, "Template created successfully.");
    }

    public async Task<ApiResponse<string>> UpdateAsync(EmailTemplateUpdateRequestDto dto)
    {
        var template = await _unitOfWork.EmailTemplateRepository.GetByIdAsync(dto.Id);
        if (template == null)
            return new ApiResponse<string>("Template not found.");

        template.TemplateName = dto.TemplateName;
        template.Subject = dto.Subject;
        template.Body = dto.Body;
        template.UpdatedDate = DateTime.UtcNow;

        await _unitOfWork.EmailTemplateRepository.UpdateAsync(template);
        await _unitOfWork.CompleteAsync();
        return new ApiResponse<string>(true, "Template updated successfully.");
    }

    public async Task<ApiResponse<string>> DeleteAsync(Guid id)
    {
        var template = await _unitOfWork.EmailTemplateRepository.GetByIdAsync(id);
        if (template == null)
            return new ApiResponse<string>("Template not found.");

        await _unitOfWork.EmailTemplateRepository.DeleteAsync(id);
        await _unitOfWork.CompleteAsync();
        return new ApiResponse<string>(true, "Template deleted successfully.");
    }

    public async Task<ApiResponse<string>> SendFromTemplateAsync(string templateName, string toEmail, Dictionary<string, string> placeholders)
    {
        var template = await _unitOfWork.EmailTemplateRepository.GetByTemplateNameAsync(templateName);
        if (template == null)
            return new ApiResponse<string>("Email template not found.");

        var subject = EmailTemplateHelper.ApplyTemplate(template.Subject, placeholders);
        var body = EmailTemplateHelper.ApplyTemplate(template.Body, placeholders);

        var success = await _emailService.SendEmailAsync(toEmail, subject, body);
        if (!success)
            return new ApiResponse<string>("Failed to send email.");

        return new ApiResponse<string>(true, "Email sent successfully.");
    }
}
