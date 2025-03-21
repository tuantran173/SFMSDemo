using SFMSSolution.Infrastructure.Database.AppDbContext;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using SFMSSolution.Infrastructure.Implements.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SFMSDbContext _context;

        // Khai báo các private field cho repository
        private IAdminRepository _adminRepository;
        private IAuthRepository _authRepository;
        private IBookingRepository _bookingRepository;
        private IFacilityRepository _facilityRepository;
        private IRoleRepository _roleRepository;
        private IUserProfileRepository _userProfileRepository;
        private IUserTokenRepository _userTokenRepository;
        private IEventRepository _eventRepository;
        private IFacilityPriceRepository _facilityPriceRepository;
        private IPriceRepository _priceRepository;

        public UnitOfWork(SFMSDbContext context)
        {
            _context = context;
        }

        // Triển khai property cho mỗi repository, sử dụng lazy initialization
        public IAdminRepository AdminRepository
            => _adminRepository ??= new AdminRepository(_context);

        public IAuthRepository AuthRepository
            => _authRepository ??= new AuthRepository(_context);

        public IBookingRepository BookingRepository
            => _bookingRepository ??= new BookingRepository(_context);

        public IFacilityRepository FacilityRepository
            => _facilityRepository ??= new FacilityRepository(_context);

        public IRoleRepository RoleRepository
            => _roleRepository ??= new RoleRepository(_context);

        public IUserProfileRepository UserProfileRepository
            => _userProfileRepository ??= new UserProfileRepository(_context);
        public IUserTokenRepository UserTokenRepository
           => _userTokenRepository ??= new UserTokenRepository(_context);
        public IEventRepository EventRepository
           => _eventRepository ??= new EventRepository(_context);
        public IFacilityPriceRepository FacilityPriceRepository
            => _facilityPriceRepository ??= new FacilityPriceRepository(_context);
        public IPriceRepository PriceRepository
            => _priceRepository ??= new PriceRepository(_context);

        // Triển khai phương thức CompleteAsync
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Triển khai IDisposable
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
