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
        IUserRepository AdminRepository { get; }
        IBookingRepository BookingRepository { get; }
        IFacilityRepository FacilityRepository { get; }
        IEventRepository EventRepository { get; }
        IFacilityPriceRepository FacilityPriceRepository { get; }
        IPriceRepository PriceRepository { get; }

        // Phương thức commit transaction
        Task<int> CompleteAsync();
    }
}
