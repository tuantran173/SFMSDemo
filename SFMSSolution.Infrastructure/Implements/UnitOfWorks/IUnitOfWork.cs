using SFMSSolution.Infrastructure.Implements.Interfaces;
using SFMSSolution.Infrastructure.Implements.Repositories;
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
        IUserTokenRepository UserTokenRepository { get; }
        IEventRepository EventRepository { get; }
        IFacilityPriceRepository FacilityPriceRepository { get; }
        IPriceRepository PriceRepository { get; }

        // Phương thức commit transaction
        Task<int> CompleteAsync();
    }
}
