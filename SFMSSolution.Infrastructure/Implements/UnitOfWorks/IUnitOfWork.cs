using SFMSSolution.Infrastructure.Implements.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        // Khai báo các repository property
        IAdminRepository AdminRepository { get; }
        IAuthRepository AuthRepository { get; }
        IBookingRepository BookingRepository { get; }
        IFacilityRepository FacilityRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }

        // Phương thức commit transaction
        Task<int> CompleteAsync();
    }
}
