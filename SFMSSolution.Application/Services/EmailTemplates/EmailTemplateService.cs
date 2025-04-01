using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.EmailTemplate.Request;
using SFMSSolution.Application.DataTransferObjects.EmailTemplate;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Application.Extensions;
using SFMSSolution.Application.ExternalService.Email;

namespace SFMSSolution.Application.Services.EmailTemplates
{
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

        public async Task<IEnumerable<EmailTemplateDto>> GetAllTemplatesAsync()
        {
            var templates = await _unitOfWork.EmailTemplateRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmailTemplateDto>>(templates);
        }

        public async Task<EmailTemplateDto?> GetByIdAsync(Guid id)
        {
            var template = await _unitOfWork.EmailTemplateRepository.GetByIdAsync(id);
            return template == null ? null : _mapper.Map<EmailTemplateDto>(template);
        }

        public async Task<bool> CreateAsync(EmailTemplateCreateRequestDto dto)
        {
            var template = _mapper.Map<EmailTemplate>(dto);
            await _unitOfWork.EmailTemplateRepository.AddAsync(template);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(EmailTemplateUpdateRequestDto dto)
        {
            var template = await _unitOfWork.EmailTemplateRepository.GetByIdAsync(dto.Id);
            if (template == null) return false;

            template.TemplateName = dto.TemplateName;
            template.Subject = dto.Subject;
            template.Body = dto.Body;
            template.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.EmailTemplateRepository.UpdateAsync(template);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _unitOfWork.EmailTemplateRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> SendFromTemplateAsync(string templateName, string toEmail, Dictionary<string, string> placeholders)
        {
            var template = await _unitOfWork.EmailTemplateRepository.GetByTemplateNameAsync(templateName);
            if (template == null) return false;

            var subject = EmailTemplateHelper.ApplyTemplate(template.Subject, placeholders);
            var body = EmailTemplateHelper.ApplyTemplate(template.Body, placeholders);

            return await _emailService.SendEmailAsync(toEmail, subject, body);
        }
    }
}
