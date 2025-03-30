using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<(List<User> Users, int TotalCount)> GetAllUsersWithRolesAsync(
         int pageNumber,
         int pageSize,
         string? fullName = null,
         string? email = null,
         string? phone = null,
         EntityStatus? status = null,
         string? role = null
        );
        Task<User?> GetUserByIdWithRolesAsync(Guid userId);
        Task ChangeUserRoleAsync(Guid userId, Guid newRoleId);
    }
}
