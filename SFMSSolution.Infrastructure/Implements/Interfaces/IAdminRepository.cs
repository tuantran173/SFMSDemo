using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Interfaces
{
    public interface IAdminRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> ChangeUserRoleAsync(Guid userId, Guid newRoleId);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
