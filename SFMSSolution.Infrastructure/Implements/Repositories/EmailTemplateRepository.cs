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

        public async Task<EmailTemplate?> GetTemplateByNameAsync(Guid ownerId, string templateName)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.OwnerId == ownerId && e.TemplateName == templateName);
        }

        public async Task<IEnumerable<EmailTemplate>> GetAllTemplatesByOwnerAsync(Guid ownerId)
        {
            return await _dbSet.Where(e => e.OwnerId == ownerId).ToListAsync();
        }
    }
}

