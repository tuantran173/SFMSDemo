using FluentValidation;
using SFMSSolution.Application.DataTransferObjects.EmailTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Extensions.Validations
{
    public class EmailTemplateValidator : AbstractValidator<EmailTemplateDto>
    {
        public EmailTemplateValidator()
        {
            RuleFor(x => x.TemplateName)
                .NotEmpty().WithMessage("Template name is required.");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Subject is required.");

            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Body is required.");
        }
    }
}
