using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<bool> UpdateUserAsync(User user);
    }
}
