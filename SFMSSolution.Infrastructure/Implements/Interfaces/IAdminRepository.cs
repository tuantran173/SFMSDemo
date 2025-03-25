using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Interfaces
{
    public interface IAdminRepository : IGenericRepository<User>
    {
        Task<(List<User> Users, int TotalCount)> GetAllUsersWithRolesAsync(int pageNumber, int pageSize);
        Task<User?> GetUserByIdWithRolesAsync(Guid userId);
        Task ChangeUserRoleAsync(Guid userId, Guid newRoleId);
    }
}
