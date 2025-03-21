using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> GetGeneralReportAsync()
        {
            var bookings = await _unitOfWork.BookingRepository.GetAllAsync();
            //var revenue = bookings.Where(b => b.Status == Domain.Enums.BookingStatus.Completed).Sum(b => b.TotalPrice);

            var result = new
            {
                TotalBookings = bookings.Count(),
                CompletedBookings = bookings.Count(b => b.Status == Domain.Enums.BookingStatus.Completed),
                //Revenue = revenue,
                PendingBookings = bookings.Count(b => b.Status == Domain.Enums.BookingStatus.Pending)
            };

            return result;
        }

        public async Task<object> GetFacilityReportAsync(Guid facilityId)
        {
            var bookings = await _unitOfWork.BookingRepository.FindAsync(b => b.FacilityId == facilityId);
            //var revenue = bookings.Where(b => b.Status == Domain.Enums.BookingStatus.Completed).Sum(b => b.TotalPrice);

            var result = new
            {
                TotalBookings = bookings.Count(),
                CompletedBookings = bookings.Count(b => b.Status == Domain.Enums.BookingStatus.Completed),
                //Revenue = revenue
            };

            return result;
        }

        public async Task<List<object>> GetTopBookedFacilitiesAsync()
        {
            var bookings = await _unitOfWork.BookingRepository.GetAllAsync();
            var topFacilities = bookings
                .GroupBy(b => b.FacilityId)
                .Select(g => new
                {
                    FacilityId = g.Key,
                    BookingCount = g.Count()
                })
                .OrderByDescending(f => f.BookingCount)
                .Take(5)
                .ToList();

            return topFacilities.Cast<object>().ToList();
        }
    }
}
