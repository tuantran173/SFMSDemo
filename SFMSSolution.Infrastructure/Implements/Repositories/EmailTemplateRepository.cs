using Microsoft.EntityFrameworkCore;
using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Database.AppDbContext;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Repositories
{
    public class EmailTemplateRepository : GenericRepository<EmailTemplate>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(SFMSDbContext context) : base(context) { }

        public async Task<EmailTemplate?> GetByTemplateNameAsync(string templateName)
        {
            return await _context.EmailTemplates.FirstOrDefaultAsync(e => e.TemplateName == templateName);
        }
    }
}

