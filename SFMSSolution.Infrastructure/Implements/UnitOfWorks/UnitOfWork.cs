using Microsoft.AspNetCore.Identity;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Database.AppDbContext;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using SFMSSolution.Infrastructure.Implements.Repositories;
using System;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SFMSDbContext _context;
        private readonly UserManager<User> _userManager;

        // Khai báo các private field cho repository
        private IUserRepository _adminRepository;
        private IBookingRepository _bookingRepository;
        private IFacilityRepository _facilityRepository;
        private IEventRepository _eventRepository;
        private IFacilityPriceRepository _facilityPriceRepository;
        private IFacilityTimeSlotRepository _facilityTimeSlotRepository;
        private IEmailTemplateRepository _emailTemplateRepository;

        public UnitOfWork(SFMSDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Triển khai property cho mỗi repository, sử dụng lazy initialization
        public IUserRepository AdminRepository
            => _adminRepository ??= new UserRepository(_context, _userManager);

        public IBookingRepository BookingRepository
            => _bookingRepository ??= new BookingRepository(_context);

        public IFacilityRepository FacilityRepository
            => _facilityRepository ??= new FacilityRepository(_context);

        public IEventRepository EventRepository
            => _eventRepository ??= new EventRepository(_context);

        public IFacilityPriceRepository FacilityPriceRepository
            => _facilityPriceRepository ??= new FacilityPriceRepository(_context);

        public IFacilityTimeSlotRepository FacilityTimeSlotRepository
           => _facilityTimeSlotRepository ??= new FacilityTimeSlotRepository(_context);
        public IEmailTemplateRepository EmailTemplateRepository
           => _emailTemplateRepository ??= new EmailTemplateRepository(_context);

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