using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Application.DataTransferObjects.Booking;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;

namespace SFMSSolution.Application.Services.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookingDto> GetBookingAsync(Guid id)
        {
            var booking = await _unitOfWork.BookingRepository.GetBookingByIdWithDetailsAsync(id);
            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<(IEnumerable<BookingDto> Bookings, int TotalCount)> GetAllBookingsAsync(int pageNumber, int pageSize)
        {
            var (bookings, totalCount) = await _unitOfWork.BookingRepository.GetAllBookingsWithDetailsAsync(pageNumber, pageSize);
            var bookingDtos = _mapper.Map<IEnumerable<BookingDto>>(bookings);
            return (bookingDtos, totalCount);
        }

        public async Task<bool> CreateBookingAsync(BookingCreateRequestDto request)
        {
            var booking = _mapper.Map<Booking>(request);

            // Đặt trạng thái mặc định là Pending
            booking.Status = BookingStatus.Pending;

            // Thêm mới booking vào DB (không gọi SaveChangesAsync tại đây)
            await _unitOfWork.BookingRepository.AddAsync(booking);

            // Gọi SaveChangesAsync từ UnitOfWork
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> UpdateBookingAsync(Guid id, BookingUpdateRequestDto request)
        {
            var existingBooking = await _unitOfWork.BookingRepository.GetByIdAsync(id);
            if (existingBooking == null)
                return false;

            existingBooking.BookingDate = request.BookingDate;
            existingBooking.StartTime = request.StartTime;
            existingBooking.EndTime = request.EndTime;

            if (!string.IsNullOrEmpty(request.Status) &&
                Enum.TryParse<BookingStatus>(request.Status, true, out var status))
            {
                existingBooking.Status = status;
            }

            existingBooking.UpdatedDate = DateTime.UtcNow;

            // Cập nhật booking (không gọi SaveChangesAsync tại đây)
            await _unitOfWork.BookingRepository.UpdateAsync(existingBooking);

            // Gọi SaveChangesAsync từ UnitOfWork
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> DeleteBookingAsync(Guid id)
        {
            // Xóa booking (không gọi SaveChangesAsync tại đây)
            await _unitOfWork.BookingRepository.DeleteAsync(id);

            // Gọi SaveChangesAsync từ UnitOfWork
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
