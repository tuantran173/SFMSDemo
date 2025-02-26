using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Interfaces
{
    public interface IRoleRepository
    {
        // Lấy role theo Id
        Task<Role?> GetRoleByIdAsync(Guid id);

        // Lấy role theo RoleCode (ví dụ "CUS", "AD", "FO")
        Task<Role?> GetRoleByCodeAsync(string roleCode);
    }
}
