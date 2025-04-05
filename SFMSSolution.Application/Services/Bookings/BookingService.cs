using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Booking;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Domain.Enums;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using SFMSSolution.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<(IEnumerable<BookingDto> Bookings, int TotalCount)> GetAllBookingsAsync(string? name, int pageNumber, int pageSize)
        {
            var (bookings, totalCount) = await _unitOfWork.BookingRepository.GetAllBookingsWithDetailsAsync(name, pageNumber, pageSize);
            var bookingDtos = _mapper.Map<IEnumerable<BookingDto>>(bookings);
            return (bookingDtos, totalCount);
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByUserAsync(Guid userId)
        {
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByUserAsync(userId);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }



        public async Task<bool> CreateBookingAsync(BookingCreateRequestDto request)
        {
            var isBooked = await _unitOfWork.BookingRepository.IsTimeSlotBooked(request.FacilityTimeSlotId, request.BookingDate);
            if (isBooked)
                throw new Exception("Time slot is already booked.");

            var booking = _mapper.Map<Booking>(request);
            booking.Status = BookingStatus.Pending;

            await _unitOfWork.BookingRepository.AddAsync(booking);
            await _unitOfWork.CompleteAsync();

            // Gửi email
            //var user = await _unitOfWork.AdminRepository.GetByIdAsync(request.UserId);
            //var facility = await _unitOfWork.FacilityRepository.GetByIdAsync(request.FacilityId);
            //var timeSlot = await _unitOfWork.FacilityTimeSlotRepository.GetByIdAsync(request.FacilityTimeSlotId);

            //await _emailService.SendAsync(new EmailDto
            //{
            //    To = user.Email,
            //    Subject = "Xác nhận đặt sân",
            //    Body = $"Bạn đã đặt sân {facility.Name} vào lúc {timeSlot.StartTime} - {timeSlot.EndTime} ngày {request.BookingDate:dd/MM/yyyy}."
            //});
            return true;
        }

        public async Task<bool> UpdateBookingAsync(Guid id, BookingUpdateRequestDto request)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(id);
            if (booking == null)
                return false;

            booking.BookingDate = request.BookingDate;
            booking.Note = request.Note;
            booking.FacilityTimeSlotId = request.FacilityTimeSlotId;

            if (!string.IsNullOrWhiteSpace(request.Status) &&
                Enum.TryParse<BookingStatus>(request.Status, true, out var parsedStatus))
            {
                booking.Status = parsedStatus;
            }

            booking.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.BookingRepository.UpdateAsync(booking);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> DeleteBookingAsync(Guid id)
        {
            await _unitOfWork.BookingRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateBookingStatusAsync(Guid bookingId, BookingStatusUpdateRequestDto request)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId);
            if (booking == null) return false;

            if (!Enum.TryParse<BookingStatus>(request.Status, true, out var status))
                throw new Exception("Invalid status value.");

            booking.Status = status;
            booking.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.BookingRepository.UpdateAsync(booking);
            await _unitOfWork.CompleteAsync();

            // Gửi thông báo qua SignalR và Email ở bước dưới
            return true;
        }

        public async Task<IEnumerable<BookingDto>> GetBookingHistoryForUserAsync(Guid userId)
        {
            var now = DateTime.UtcNow;
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByUserAsync(userId);

            var history = bookings
                .Where(b => b.BookingDate.Date < now.Date ||
                       (b.BookingDate.Date == now.Date && b.FacilityTimeSlot.EndTime < now.TimeOfDay))
                .ToList();

            return _mapper.Map<IEnumerable<BookingDto>>(history);
        }

        //public async Task<ApiResponse<FacilityBookingCalendarDto>> GetFacilityCalendarAsync(Guid facilityId, Guid? userId = null)
        //{
        //    var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(facilityId);
        //    if (facility == null)
        //        return new ApiResponse<FacilityBookingCalendarDto>("Facility not found.");

        //    var today = DateTime.Today;
        //    var futureDays = 14;

        //    var timeSlots = await _unitOfWork.FacilityTimeSlotRepository.GetByFacilityIdAsync(facilityId);
        //    var bookings = await _unitOfWork.BookingRepository.GetBookingsByFacilityAsync(facilityId, DateTime.UtcNow);

        //    var calendarItems = new List<FacilityBookingSlotDto>();

        //    for (int i = 0; i < futureDays; i++)
        //    {
        //        var date = today.AddDays(i);

        //        foreach (var slot in timeSlots)
        //        {
        //            if (slot.StartDate <= date && slot.EndDate >= date)
        //            {
        //                var booking = bookings.FirstOrDefault(b =>
        //                    b.FacilityTimeSlotId == slot.Id &&
        //                    b.BookingDate.Date == date);

        //                var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slot.Id);

        //                SlotStatus status;
        //                if (booking != null)
        //                {
        //                    if (userId.HasValue && booking.UserId == userId.Value)
        //                        status = SlotStatus.Booked;
        //                    else
        //                        status = SlotStatus.Full;
        //                }
        //                else
        //                {
        //                    status = slot.Status == SlotStatus.Closed ? SlotStatus.Closed : SlotStatus.Available;
        //                }

        //                calendarItems.Add(new FacilityBookingSlotDto
        //                {
        //                    SlotId = slot.Id,
        //                    StartTime = slot.StartTime,
        //                    EndTime = slot.EndTime,
        //                    StartDate = date,
        //                    EndDate = date,
        //                    Status = status,
        //                    Note = booking?.Note ?? string.Empty,
        //                    FinalPrice = price?.FinalPrice ?? 0
        //                });
        //            }
        //        }
        //    }

        //    var result = new FacilityBookingCalendarDto
        //    {
        //        FacilityId = facility.Id,
        //        Name = facility.Name,
        //        Address = facility.Address,
        //        ImageUrl = facility.ImageUrl,
        //        Calendar = calendarItems
        //    };

        //    return new ApiResponse<FacilityBookingCalendarDto>(result);
        //}
        public async Task<ApiResponse<FacilityBookingCalendarDto>> GetCalendarForCustomerAsync(Guid facilityId, Guid? userId)
        {
            var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(facilityId);
            if (facility == null)
                return new ApiResponse<FacilityBookingCalendarDto>("Facility not found.");

            var today = DateTime.Today;
            var futureDays = 14;
            var timeSlots = await _unitOfWork.FacilityTimeSlotRepository.GetByFacilityIdAsync(facilityId);
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByFacilityAsync(facilityId, DateTime.UtcNow);

            var calendarItems = new List<FacilityBookingSlotDto>();
            var slotDuration = TimeSpan.FromMinutes(90);

            for (int i = 0; i < futureDays; i++)
            {
                var date = today.AddDays(i);

                foreach (var slot in timeSlots)
                {
                    if (slot.StartDate <= date && slot.EndDate >= date)
                    {
                        var current = slot.StartTime;
                        while (current + slotDuration <= slot.EndTime)
                        {
                            var subStart = current;
                            var subEnd = current + slotDuration;

                            var booking = bookings.FirstOrDefault(b =>
                                b.FacilityTimeSlotId == slot.Id &&
                                b.BookingDate.Date == date);

                            var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slot.Id);

                            // Logic phân biệt guest vs customer
                            var status = booking != null
                                ? (userId.HasValue && booking.UserId == userId.Value ? SlotStatus.Booked : SlotStatus.Full)
                                : slot.Status == SlotStatus.Closed ? SlotStatus.Closed : SlotStatus.Available;

                            calendarItems.Add(new FacilityBookingSlotDto
                            {
                                SlotId = slot.Id,
                                StartTime = subStart,
                                EndTime = subEnd,
                                StartDate = date,
                                EndDate = date,
                                Status = status,
                                Note = booking?.Note ?? string.Empty,
                                FinalPrice = price?.FinalPrice ?? 0
                            });

                            current += slotDuration;
                        }
                    }
                }
            }

            var result = new FacilityBookingCalendarDto
            {
                FacilityId = facility.Id,
                Name = facility.Name,
                Address = facility.Address,
                ImageUrl = facility.ImageUrl,
                Calendar = calendarItems
            };

            return new ApiResponse<FacilityBookingCalendarDto>(result);
        }


        public async Task<ApiResponse<List<FacilityBookingSlotDto>>> GetCalendarForGuestAsync(Guid facilityId)
        {
            var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(facilityId);
            if (facility == null)
                return new ApiResponse<List<FacilityBookingSlotDto>>("Facility not found.");

            var today = DateTime.Today;
            var futureDays = 14;
            var slotDuration = TimeSpan.FromMinutes(90);
            var slots = await _unitOfWork.FacilityTimeSlotRepository.GetByFacilityIdAsync(facilityId);
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByFacilityAsync(facilityId, today);

            var result = new List<FacilityBookingSlotDto>();

            foreach (var day in Enumerable.Range(0, futureDays))
            {
                var date = today.AddDays(day);
                foreach (var slot in slots)
                {
                    if (slot.StartDate > date || slot.EndDate < date) continue;

                    for (var time = slot.StartTime; time + slotDuration <= slot.EndTime; time += slotDuration)
                    {
                        var booking = bookings.FirstOrDefault(b => b.BookingDate.Date == date && b.FacilityTimeSlotId == slot.Id);
                        var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slot.Id);

                        var status = booking != null ? SlotStatus.Full : (slot.Status == SlotStatus.Closed ? SlotStatus.Closed : SlotStatus.Available);

                        result.Add(new FacilityBookingSlotDto
                        {
                            SlotId = slot.Id,
                            StartTime = time,
                            EndTime = time + slotDuration,
                            StartDate = date,
                            EndDate = date,
                            Status = status,
                            Note = booking?.Note ?? string.Empty,
                            FinalPrice = price?.FinalPrice ?? 0
                        });
                    }
                }
            }

            return new ApiResponse<List<FacilityBookingSlotDto>>(result);
        }

        public async Task<ApiResponse<FacilityBookingCalendarDto>> GetFacilityCalendarAsync(Guid facilityId, Guid? userId = null)
        {
            var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(facilityId);
            if (facility == null)
                return new ApiResponse<FacilityBookingCalendarDto>("Facility not found.");

            var today = DateTime.Today;
            var futureDays = 14;

            var timeSlots = await _unitOfWork.FacilityTimeSlotRepository.GetByFacilityIdAsync(facilityId);
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByFacilityAsync(facilityId, DateTime.UtcNow);

            var calendarItems = new List<FacilityBookingSlotDto>();

            for (int i = 0; i < futureDays; i++)
            {
                var date = today.AddDays(i);

                foreach (var slot in timeSlots)
                {
                    if (slot.StartDate <= date && slot.EndDate >= date)
                    {
                        var current = slot.StartTime;
                        var slotDuration = TimeSpan.FromMinutes(90);

                        while (current + slotDuration <= slot.EndTime)
                        {
                            var subStart = current;
                            var subEnd = current + slotDuration;

                            var booking = bookings.FirstOrDefault(b =>
                                b.FacilityTimeSlotId == slot.Id &&
                                b.BookingDate.Date == date &&
                                b.FacilityTimeSlot.StartTime == subStart &&
                                b.FacilityTimeSlot.EndTime == subEnd);

                            var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slot.Id);

                            var status = slot.Status;
                            if (booking != null) status = SlotStatus.Booked;

                            calendarItems.Add(new FacilityBookingSlotDto
                            {
                                SlotId = slot.Id,
                                StartTime = subStart,
                                EndTime = subEnd,
                                StartDate = date,
                                EndDate = date,
                                Status = status,
                                Note = booking?.Note ?? "",
                                FinalPrice = price?.FinalPrice ?? 0
                            });

                            current += slotDuration;
                        }
                    }
                }
            }

            var result = new FacilityBookingCalendarDto
            {
                FacilityId = facility.Id,
                Name = facility.Name,
                Address = facility.Address,
                ImageUrl = facility.ImageUrl,
                Calendar = calendarItems
            };

            return new ApiResponse<FacilityBookingCalendarDto>(result);
        }

        public async Task<ApiResponse<FacilityBookingSlotDto>> GetCalendarSlotDetailAsync(
            Guid slotId,
            DateTime date,
            TimeSpan startTime,
            TimeSpan endTime)
        {
            // Lấy khung slot tổng
            var slot = await _unitOfWork.FacilityTimeSlotRepository.GetByIdAsync(slotId);
            if (slot == null)
                return new ApiResponse<FacilityBookingSlotDto>("Slot not found.");

            // Kiểm tra thời gian nằm trong khung slot tổng
            if (startTime < slot.StartTime || endTime > slot.EndTime)
                return new ApiResponse<FacilityBookingSlotDto>("Time range doesn't belong to this slot.");

            // Lấy thông tin booking nếu có cho slot con cụ thể
            var booking = await _unitOfWork.BookingRepository
                .GetBookingBySlotAndDateAndTimeAsync(slotId, date, startTime, endTime);

            // Lấy giá
            var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slotId);

            // Xác định trạng thái
            var status = booking != null ? SlotStatus.Booked : slot.Status;

            // Trả về chi tiết slot nhỏ
            var dto = new FacilityBookingSlotDto
            {
                SlotId = slot.Id,
                StartTime = startTime,
                EndTime = endTime,
                StartDate = date,
                EndDate = date,
                Status = status,
                Note = booking?.Note ?? string.Empty,
                FinalPrice = price?.FinalPrice ?? 0
            };

            return new ApiResponse<FacilityBookingSlotDto>(dto);
        }



        public async Task<ApiResponse<string>> UpdateCalendarSlotDetailAsync(UpdateSlotDetailRequestDto request)
        {
            var slot = await _unitOfWork.FacilityTimeSlotRepository.GetByIdAsync(request.SlotId);
            if (slot == null)
                return new ApiResponse<string>("Slot not found.");

            // Check time range
            if (request.StartTime < slot.StartTime || request.EndTime > slot.EndTime)
                return new ApiResponse<string>("Time range is outside the defined slot.");

            var booking = await _unitOfWork.BookingRepository
                .GetBookingBySlotAndDateAndTimeAsync(request.SlotId, request.Date, request.StartTime, request.EndTime);

            // ✅ Update note if exists
            if (booking != null && !string.IsNullOrWhiteSpace(request.Note))
            {
                booking.Note = request.Note;
                booking.UpdatedDate = DateTime.UtcNow;
                await _unitOfWork.BookingRepository.UpdateAsync(booking);
            }

            // ✅ Update price
            if (request.FinalPrice.HasValue)
            {
                var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(request.SlotId);
                if (price != null)
                {
                    price.FinalPrice = request.FinalPrice.Value;
                    price.UpdatedDate = DateTime.UtcNow;
                    await _unitOfWork.FacilityPriceRepository.UpdateAsync(price);
                }
            }

            if (request.Status.HasValue)
            {
                slot.Status = request.Status.Value;
                slot.UpdatedDate = DateTime.UtcNow;
                await _unitOfWork.FacilityTimeSlotRepository.UpdateAsync(slot);
            }

            await _unitOfWork.CompleteAsync();
            return new ApiResponse<string>(true, "Slot detail updated successfully.");
        }


    }
}
