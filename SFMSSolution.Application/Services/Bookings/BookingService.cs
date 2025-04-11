using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SFMSSolution.Application.DataTransferObjects.Booking;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Application.Services.EmailTemplates;
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
        private readonly UserManager<User> _userManager;
        private readonly IEmailTemplateService _emailTemplateService;

        public BookingService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<User> userManager,
            IEmailTemplateService emailTemplateService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _emailTemplateService = emailTemplateService;
        }

        public async Task<(IEnumerable<BookingDto> Bookings, int TotalCount)> GetAllBookingsAsync(string? name, int pageNumber, int pageSize)
        {
            var (bookings, totalCount) = await _unitOfWork.BookingRepository.GetAllBookingsWithDetailsAsync(name, pageNumber, pageSize);

            var bookingDtos = bookings.Select(b => new BookingDto
            {
                Id = b.Id,
                FacilityId = b.FacilityId,
                FacilityName = b.Facility?.Name ?? "",
                FacilityAddress = b.Facility?.Address ?? "",
                BookingDate = b.BookingDate,
                StartTime = b.StartTime ?? TimeSpan.Zero,
                EndTime = b.EndTime ?? TimeSpan.Zero,
                FinalPrice = b.FinalPrice,
                OwnerFullName = b.Facility?.Owner?.FullName ?? "",
                OwnerPhone = b.Facility?.Owner?.Phone ?? "",
                PaymentMethod = b.PaymentMethod,
                Note = b.Note
            });

            return (bookingDtos, totalCount);
        }

        public async Task<ApiResponse<BookingDto>> GetBookingDetailAsync(Guid bookingId)
        {
            var booking = await _unitOfWork.BookingRepository.GetBookingByIdWithDetailsAsync(bookingId);
            if (booking == null)
                return new ApiResponse<BookingDto>("Booking not found.");

            var owner = await _userManager.FindByIdAsync(booking.Facility?.OwnerId.ToString() ?? "");

            var result = new BookingDto
            {
                Id = booking.Id,
                FacilityId = booking.FacilityId,
                FacilityName = booking.Facility?.Name ?? "",
                FacilityAddress = booking.Facility?.Address ?? "",
                OwnerFullName = owner?.FullName ?? "",
                OwnerEmail = owner?.Email ?? "",
                OwnerPhone = owner?.Phone ?? "",

                BookingDate = booking.BookingDate,
                StartTime = booking.StartTime ?? TimeSpan.Zero,
                EndTime = booking.EndTime ?? TimeSpan.Zero,
                FinalPrice = booking.FinalPrice,

                CustomerName = booking.CustomerName,
                CustomerEmail = booking.CustomerEmail,
                CustomerPhone = booking.CustomerPhone,

                PaymentMethod = booking.PaymentMethod,
                Note = booking.Note,
                PayImageUrl = booking.ImageUrl,
                Status = booking.Status.ToString(),

                CreatedDate = booking.CreatedDate,
            };

            return new ApiResponse<BookingDto>(result);
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByUserAsync(Guid userId)
        {
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByUserAsync(userId);
            var result = new List<BookingDto>();

            foreach (var booking in bookings)
            {
                var facility = booking.Facility;
                var owner = await _userManager.FindByIdAsync(facility.OwnerId.ToString());

                var dto = new BookingDto
                {
                    Id = booking.Id,
                    FacilityId = facility.Id,
                    FacilityName = facility.Name,
                    FacilityAddress = facility.Address,

                    OwnerFullName = owner?.FullName ?? "",
                    OwnerEmail = owner?.Email ?? "",
                    OwnerPhone = owner?.Phone ?? "",

                    BookingDate = booking.BookingDate,
                    StartTime = booking.StartTime ?? booking.FacilityTimeSlot?.StartTime ?? TimeSpan.Zero,
                    EndTime = booking.EndTime ?? booking.FacilityTimeSlot?.EndTime ?? TimeSpan.Zero,
                    FinalPrice = booking.FinalPrice,

                    CustomerName = booking.CustomerName,
                    CustomerEmail = booking.CustomerEmail,
                    CustomerPhone = booking.CustomerPhone,

                    PaymentMethod = booking.PaymentMethod,
                    Note = booking.Note,
                    PayImageUrl = booking.ImageUrl,

                    Status = booking.Status.ToString(),

                    CreatedDate = booking.CreatedDate,
                };

                result.Add(dto);
            }

            return result;
        }



        //public async Task<ApiResponse<string>> CreateBookingAsync(BookingCreateRequestDto request, Guid userId)
        //{
        //    // Kiểm tra xem slot đã được đặt chưa
        //    var isBooked = await _unitOfWork.BookingRepository
        //        .IsTimeSlotBooked(request.FacilityTimeSlotId, request.BookingDate, request.StartTime, request.EndTime);

        //    if (isBooked)
        //        return new ApiResponse<string>("Time slot is already booked.");

        //    // Lấy thông tin sân
        //    var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(request.FacilityId);
        //    if (facility == null)
        //        return new ApiResponse<string>("Facility not found.");

        //    var owner = await _userManager.FindByIdAsync(facility.OwnerId.ToString());

        //    // Tạo mới booking
        //    var booking = new Booking
        //    {
        //        Id = Guid.NewGuid(),
        //        FacilityId = request.FacilityId,
        //        FacilityTimeSlotId = request.FacilityTimeSlotId,
        //        BookingDate = request.BookingDate.Date,
        //        StartTime = request.StartTime,
        //        EndTime = request.EndTime,
        //        FinalPrice = request.FinalPrice,
        //        CustomerName = request.CustomerName,
        //        CustomerPhone = request.CustomerPhone,
        //        CustomerEmail = request.CustomerEmail,
        //        PaymentMethod = request.PaymentMethod,
        //        Note = request.Note,
        //        ImageUrl = request.PayImageUrl,
        //        Status = BookingStatus.Pending,
        //        UserId = userId,
        //        CreatedDate = DateTime.UtcNow
        //    };

        //    await _unitOfWork.BookingRepository.AddAsync(booking);
        //    await _unitOfWork.CompleteAsync();

        //    // Tóm tắt kết quả
        //    var summary = $"Đặt sân thành công: {facility.Name}, ngày {booking.BookingDate:dd/MM/yyyy}, " +
        //                  $"giờ {booking.StartTime:hh\\:mm} - {booking.EndTime:hh\\:mm}. Chủ sân: {owner?.FullName}, SĐT: {owner?.PhoneNumber}.";

        //    // Gửi email cho chủ sân để xác nhận
        //    var placeholders = new Dictionary<string, string>
        //        {
        //            { "OwnerName", owner.FullName },
        //            { "CustomerName", request.CustomerName },
        //            { "FacilityName", facility.Name },
        //            { "BookingTime", $"{booking.BookingDate:dd/MM/yyyy} {booking.StartTime:hh\\:mm} - {booking.EndTime:hh\\:mm}" },
        //            { "Price", booking.FinalPrice.ToString("N0") + " VND" },
        //            { "ConfirmLink", $"https://yourdomain.com/booking/confirm/{booking.Id}" },
        //            { "RejectLink", $"https://yourdomain.com/booking/reject/{booking.Id}" }
        //        };

        //    var emailTo = owner.Email;
        //    Console.WriteLine($"Đang gửi email xác nhận booking đến chủ sân: {emailTo}");

        //    var emailSent = await _emailTemplateService.SendFromTemplateAsync(
        //        templateName: "BookingRequestOwner",
        //        toEmail: emailTo,
        //        placeholders: placeholders
        //    );

        //    Console.WriteLine($"Kết quả gửi email: {(emailSent.Success ? "Thành công" : "Thất bại")} - {emailSent.Message}");
        //    if (!emailSent.Success)
        //    {
        //        return new ApiResponse<string>(false, $"Đặt sân thành công nhưng gửi email cho chủ sân thất bại: {emailSent.Message}");
        //    }

        //    return new ApiResponse<string>(true, summary);
        //}

        public async Task<ApiResponse<string>> CreateBookingAsync(BookingCreateRequestDto request, Guid userId)
        {
            var isBooked = await _unitOfWork.BookingRepository
                .IsTimeSlotBooked(request.FacilityTimeSlotId, request.BookingDate, request.StartTime, request.EndTime);

            if (isBooked)
                return new ApiResponse<string>("Time slot is already booked.");

            var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(request.FacilityId);
            if (facility == null)
                return new ApiResponse<string>("Facility not found.");

            var owner = await _userManager.FindByIdAsync(facility.OwnerId.ToString());
            if (owner == null || string.IsNullOrEmpty(owner.Email))
                return new ApiResponse<string>("Chủ sân không có email để gửi xác nhận.");

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                FacilityId = request.FacilityId,
                FacilityTimeSlotId = request.FacilityTimeSlotId,
                BookingDate = request.BookingDate.Date,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                FinalPrice = request.FinalPrice,
                CustomerName = request.CustomerName,
                CustomerPhone = request.CustomerPhone,
                CustomerEmail = request.CustomerEmail,
                PaymentMethod = request.PaymentMethod,
                Note = request.Note,
                ImageUrl = request.PayImageUrl,
                Status = BookingStatus.Pending,
                UserId = userId,
                CreatedDate = DateTime.UtcNow
            };

            await _unitOfWork.BookingRepository.AddAsync(booking);
            await _unitOfWork.CompleteAsync();

            var placeholders = new Dictionary<string, string>
                {
                    { "OwnerName", owner.FullName },
                    { "CustomerName", request.CustomerName },
                    { "CustomerPhone", request.CustomerPhone },
                    { "CustomerEmail", request.CustomerEmail },
                    { "FacilityName", facility.Name },
                    { "FacilityAddress", facility.Address },
                    { "BookingDate", booking.BookingDate.ToString("dd/MM/yyyy") },
                    { "BookingTime", $"{booking.StartTime:hh\\:mm} - {booking.EndTime:hh\\:mm}" },
                    { "PaymentMethod", request.PaymentMethod == "cash" ? "Tiền mặt" : "VNPay" },
                    { "Note", string.IsNullOrWhiteSpace(request.Note) ? "Không có ghi chú" : request.Note },
                    { "Price", booking.FinalPrice.ToString("N0") + " VND" },
                    { "PayUrl", string.IsNullOrEmpty(booking.ImageUrl) ? "" : booking.ImageUrl },
                    { "ConfirmLink", $"https://localhost:4200/booking/confirm/{booking.Id}" },
                    { "RejectLink", $"https://localhost:4200/booking/reject/{booking.Id}" }
                };


            Console.WriteLine($"Đang gửi email xác nhận booking đến chủ sân: {owner.Email}");

            var emailSent = await _emailTemplateService.SendFromTemplateAsync(
                templateName: "BookingRequestOwner",
                toEmail: owner.Email,
                placeholders: placeholders
            );

            Console.WriteLine($"Kết quả gửi email: {(emailSent.Success ? "Thành công" : "Thất bại")} - {emailSent.Message}");
            if (!emailSent.Success)
            {
                return new ApiResponse<string>(false, $"Đặt sân thành công nhưng gửi email cho chủ sân thất bại: {emailSent.Message}");
            }

            var summary = $"Yêu cầu đặt sân đã được gửi: {facility.Name}, ngày {booking.BookingDate:dd/MM/yyyy}, " +
              $"giờ {booking.StartTime:hh\\:mm} - {booking.EndTime:hh\\:mm}. Đang chờ chủ sân xác nhận.";

            return new ApiResponse<string>(true, summary);
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

        //    public async Task<ApiResponse<FacilityBookingCalendarDto>> GetCalendarForCustomerAsync(Guid facilityId, Guid? userId)
        //    {
        //        var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(facilityId);
        //        if (facility == null)
        //            return new ApiResponse<FacilityBookingCalendarDto>("Facility not found.");

        //        var today = DateTime.Today;
        //        var futureDays = 14;
        //        var timeSlots = await _unitOfWork.FacilityTimeSlotRepository.GetByFacilityIdAsync(facilityId);
        //        var bookings = await _unitOfWork.BookingRepository
        //.GetBookingsByFacilityAsync(facilityId, today, today.AddDays(14));

        //        var calendarItems = new List<FacilityBookingSlotDto>();
        //        var slotDuration = TimeSpan.FromMinutes(90);

        //        for (int i = 0; i < futureDays; i++)
        //        {
        //            var date = today.AddDays(i);

        //            foreach (var slot in timeSlots)
        //            {
        //                if (slot.StartDate <= date && slot.EndDate >= date)
        //                {
        //                    var current = slot.StartTime;
        //                    while (current + slotDuration <= slot.EndTime)
        //                    {
        //                        var subStart = current;
        //                        var subEnd = current + slotDuration;

        //                        var booking = bookings.FirstOrDefault(b =>
        //                            b.FacilityTimeSlotId == slot.Id &&
        //                            b.BookingDate.Date == date &&
        //                            b.StartTime == subStart &&
        //                            b.EndTime == subEnd);

        //                        var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slot.Id);

        //                        // Logic phân biệt guest vs customer
        //                        var status = booking != null
        //                            ? (userId.HasValue && booking.UserId == userId.Value ? SlotStatus.Booked : SlotStatus.Full)
        //                            : slot.Status == SlotStatus.Closed ? SlotStatus.Closed : SlotStatus.Available;

        //                        calendarItems.Add(new FacilityBookingSlotDto
        //                        {
        //                            SlotId = slot.Id,
        //                            StartTime = subStart,
        //                            EndTime = subEnd,
        //                            StartDate = date,
        //                            EndDate = date,
        //                            Status = status,
        //                            Note = booking?.Note ?? string.Empty,
        //                            FinalPrice = price?.FinalPrice ?? 0,
        //                            UserId = booking?.UserId
        //                        });

        //                        current += slotDuration;
        //                    }
        //                }
        //            }
        //        }

        //        var result = new FacilityBookingCalendarDto
        //        {
        //            FacilityId = facility.Id,
        //            Name = facility.Name,
        //            Address = facility.Address,
        //            Description = facility.Description,
        //            ImageUrl = facility.ImageUrl,
        //            Calendar = calendarItems
        //        };

        //        return new ApiResponse<FacilityBookingCalendarDto>(result);
        //    }

        public async Task<ApiResponse<FacilityBookingCalendarDto>> GetCalendarForCustomerAsync(Guid facilityId, Guid? userId)
        {
            var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(facilityId);
            if (facility == null)
                return new ApiResponse<FacilityBookingCalendarDto>("Facility not found.");

            var today = DateTime.Today;
            var futureDays = 14;
            var timeSlots = await _unitOfWork.FacilityTimeSlotRepository.GetByFacilityIdAsync(facilityId);
            var bookings = await _unitOfWork.BookingRepository
                .GetBookingsByFacilityAsync(facilityId, today, today.AddDays(14));

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
                                b.BookingDate.Date == date &&
                                b.StartTime == subStart &&
                                b.EndTime == subEnd);

                            var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slot.Id);

                            SlotStatus status;
                            if (booking != null)
                            {
                                if (booking.Status == BookingStatus.Pending)
                                    status = SlotStatus.Pending;
                                else if (userId.HasValue && booking.UserId == userId.Value)
                                    status = SlotStatus.Booked;
                                else
                                    status = SlotStatus.Full;
                            }
                            else
                            {
                                status = slot.Status == SlotStatus.Closed ? SlotStatus.Closed : SlotStatus.Available;
                            }

                            calendarItems.Add(new FacilityBookingSlotDto
                            {
                                SlotId = slot.Id,
                                StartTime = subStart,
                                EndTime = subEnd,
                                StartDate = date,
                                EndDate = date,
                                Status = status,
                                Note = booking?.Note ?? string.Empty,
                                FinalPrice = price?.FinalPrice ?? 0,
                                UserId = booking?.UserId
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
                Description = facility.Description,
                ImageUrl = facility.ImageUrl,
                Calendar = calendarItems
            };

            return new ApiResponse<FacilityBookingCalendarDto>(result);
        }

        public async Task<ApiResponse<FacilityBookingCalendarDto>> GetCalendarForGuestAsync(Guid facilityId)
        {
            var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(facilityId);
            if (facility == null)
                return new ApiResponse<FacilityBookingCalendarDto>("Facility not found.");

            var today = DateTime.Today;
            var futureDays = 14;
            var slotDuration = TimeSpan.FromMinutes(90);

            var slots = await _unitOfWork.FacilityTimeSlotRepository.GetByFacilityIdAsync(facilityId);
            var bookings = await _unitOfWork.BookingRepository
    .GetBookingsByFacilityAsync(facilityId, today, today.AddDays(14));

            var calendarItems = new List<FacilityBookingSlotDto>();

            foreach (var day in Enumerable.Range(0, futureDays))
            {
                var date = today.AddDays(day);
                foreach (var slot in slots)
                {
                    if (slot.StartDate > date || slot.EndDate < date) continue;

                    for (var time = slot.StartTime; time + slotDuration <= slot.EndTime; time += slotDuration)
                    {
                        var booking = bookings.FirstOrDefault(b =>
                                 b.BookingDate.Date == date &&
                                 b.FacilityTimeSlotId == slot.Id &&
                                 b.StartTime == time &&
                                 b.EndTime == time + slotDuration);


                        var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slot.Id);

                        var status = booking != null
                                 ? SlotStatus.Full
                                 : slot.Status == SlotStatus.Closed ? SlotStatus.Closed : SlotStatus.Available;

                        calendarItems.Add(new FacilityBookingSlotDto
                        {
                            SlotId = slot.Id,
                            StartTime = time,
                            EndTime = time + slotDuration,
                            StartDate = date,
                            EndDate = date,
                            Status = status,
                            Note = booking?.Note ?? "",
                            FinalPrice = price?.FinalPrice ?? 0
                        });
                    }
                }
            }

            var result = new FacilityBookingCalendarDto
            {
                FacilityId = facility.Id,
                Name = facility.Name,
                Address = facility.Address,
                Description = facility.Description,
                ImageUrl = facility.ImageUrl,
                Calendar = calendarItems
            };

            return new ApiResponse<FacilityBookingCalendarDto>(result);
        }


        //    public async Task<ApiResponse<FacilityBookingCalendarDto>> GetFacilityCalendarAsync(Guid facilityId, Guid? userId = null)
        //    {
        //        var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(facilityId);
        //        if (facility == null)
        //            return new ApiResponse<FacilityBookingCalendarDto>("Facility not found.");

        //        var today = DateTime.Today;
        //        var futureDays = 14;

        //        var timeSlots = await _unitOfWork.FacilityTimeSlotRepository.GetByFacilityIdAsync(facilityId);
        //        var bookings = await _unitOfWork.BookingRepository
        //.GetBookingsByFacilityAsync(facilityId, today, today.AddDays(14));

        //        var calendarItems = new List<FacilityBookingSlotDto>();

        //        for (int i = 0; i < futureDays; i++)
        //        {
        //            var date = today.AddDays(i);

        //            foreach (var slot in timeSlots)
        //            {
        //                if (slot.StartDate <= date && slot.EndDate >= date)
        //                {
        //                    var current = slot.StartTime;
        //                    var slotDuration = TimeSpan.FromMinutes(90);

        //                    while (current + slotDuration <= slot.EndTime)
        //                    {
        //                        var subStart = current;
        //                        var subEnd = current + slotDuration;

        //                        var booking = bookings.FirstOrDefault(b =>
        //                                b.FacilityTimeSlotId == slot.Id &&
        //                                b.BookingDate.Date == date &&
        //                                b.StartTime == subStart &&
        //                                b.EndTime == subEnd);

        //                        var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slot.Id);

        //                        SlotStatus status;
        //                        if (booking != null)
        //                        {
        //                            if (userId.HasValue && booking.UserId == userId.Value)
        //                                status = SlotStatus.Booked;
        //                            else
        //                                status = SlotStatus.Full;
        //                        }
        //                        else
        //                        {
        //                            status = slot.Status == SlotStatus.Closed ? SlotStatus.Closed : SlotStatus.Available;
        //                        }

        //                        calendarItems.Add(new FacilityBookingSlotDto
        //                        {
        //                            SlotId = slot.Id,
        //                            StartTime = subStart,
        //                            EndTime = subEnd,
        //                            StartDate = date,
        //                            EndDate = date,
        //                            Status = status,
        //                            Note = booking?.Note ?? "",
        //                            FinalPrice = price?.FinalPrice ?? 0
        //                        });

        //                        current += slotDuration;
        //                    }
        //                }
        //            }
        //        }

        //        var result = new FacilityBookingCalendarDto
        //        {
        //            FacilityId = facility.Id,
        //            Name = facility.Name,
        //            Address = facility.Address,
        //            Description = facility.Description,
        //            ImageUrl = facility.ImageUrl,
        //            Calendar = calendarItems
        //        };

        //        return new ApiResponse<FacilityBookingCalendarDto>(result);
        //    }

        public async Task<ApiResponse<FacilityBookingCalendarDto>> GetFacilityCalendarAsync(Guid facilityId, Guid? userId = null)
        {
            var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(facilityId);
            if (facility == null)
                return new ApiResponse<FacilityBookingCalendarDto>("Facility not found.");

            var today = DateTime.Today;
            var futureDays = 14;

            var timeSlots = await _unitOfWork.FacilityTimeSlotRepository.GetByFacilityIdAsync(facilityId);
            var bookings = await _unitOfWork.BookingRepository
                .GetBookingsByFacilityAsync(facilityId, today, today.AddDays(14));

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
                                b.StartTime == subStart &&
                                b.EndTime == subEnd);

                            var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slot.Id);

                            SlotStatus status;
                            if (booking != null)
                            {
                                if (booking.Status == BookingStatus.Pending)
                                    status = SlotStatus.Pending;
                                else if (userId.HasValue && booking.UserId == userId.Value)
                                    status = SlotStatus.Booked;
                                else
                                    status = SlotStatus.Full;
                            }
                            else
                            {
                                status = slot.Status == SlotStatus.Closed ? SlotStatus.Closed : SlotStatus.Available;
                            }

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
                Description = facility.Description,
                ImageUrl = facility.ImageUrl,
                Calendar = calendarItems
            };

            return new ApiResponse<FacilityBookingCalendarDto>(result);
        }

        public async Task<ApiResponse<SlotDetailDto>> GetCalendarSlotDetailAsync(
        Guid slotId,
        DateTime date,
        TimeSpan startTime,
        TimeSpan endTime)
            {
                // Lấy slot có kèm Facility và User chủ sân
                var slot = await _unitOfWork.FacilityTimeSlotRepository.GetByIdWithFacilityAndOwnerAsync(slotId);
                if (slot == null)
                    return new ApiResponse<SlotDetailDto>("Slot not found.");

                // Kiểm tra thời gian nằm trong khung slot tổng
                if (startTime < slot.StartTime || endTime > slot.EndTime)
                    return new ApiResponse<SlotDetailDto>("Time range doesn't belong to this slot.");

                // Lấy thông tin booking nếu có cho slot con cụ thể
                var booking = await _unitOfWork.BookingRepository
                    .GetBookingBySlotAndDateAndTimeAsync(slotId, date, startTime, endTime);

                // Lấy giá
                var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slotId);

                // Lấy thông tin chủ sân
                var facility = slot.Facility;
                var owner = await _userManager.FindByIdAsync(facility.OwnerId.ToString());

                // Xác định trạng thái
                var status = booking != null ? SlotStatus.Booked : slot.Status;

                // Trả về chi tiết slot nhỏ
                var dto = new SlotDetailDto
                {
                    SlotId = slot.Id,
                    StartTime = startTime,
                    EndTime = endTime,
                    StartDate = date,
                    EndDate = date,
                    Status = status,
                    Note = booking?.Note ?? string.Empty,

                    BasePrice = price?.BasePrice ?? 0,
                    Coefficient = price?.Coefficient ?? 1,
                Deposit = (price?.FinalPrice ?? 0) * 0.3m,
                FinalPrice = price?.FinalPrice ?? 0,

                // Thông tin sân và chủ sân
                FacilityName = facility?.Name ?? string.Empty,
                FacilityAddress = facility?.Address ?? string.Empty,
                FacilityImageUrl = facility?.ImageUrl ?? string.Empty,
                OwnerFullName = owner?.FullName ?? string.Empty,
                OwnerUserName = owner?.UserName ?? string.Empty,
                OwnerPhone = owner?.Phone ?? string.Empty
            };

            return new ApiResponse<SlotDetailDto>(dto);
        }



        public async Task<ApiResponse<string>> UpdateCalendarSlotDetailAsync(UpdateSlotDetailRequestDto request)
        {
            // 1. Check FacilityTimeSlot tồn tại
            var slot = await _unitOfWork.FacilityTimeSlotRepository.GetByIdAsync(request.SlotId);
            if (slot == null)
                return new ApiResponse<string>("Slot not found.");

            // 2. Validate thời gian nằm trong slot tổng
            if (request.StartTime < slot.StartTime || request.EndTime > slot.EndTime)
                return new ApiResponse<string>("Time range is outside the defined slot.");

            // 3. Check nếu đã có khách đặt rồi thì không cho chỉnh sửa
            var existingBooking = await _unitOfWork.BookingRepository
                .GetBookingBySlotAndDateAndTimeAsync(request.SlotId, request.Date, request.StartTime, request.EndTime);

            if (existingBooking != null)
                return new ApiResponse<string>("This slot is already booked. You cannot modify it.");

            // 4. Tìm SlotDetail theo slot con
            var slotDetail = await _unitOfWork.SlotDetailRepository
                .GetBySlotAndTimeAsync(request.SlotId, request.Date, request.StartTime, request.EndTime);

            if (slotDetail != null)
            {
                bool changed = false;

                if (request.FinalPrice.HasValue && slotDetail.FinalPrice != request.FinalPrice.Value)
                {
                    slotDetail.FinalPrice = request.FinalPrice.Value;
                    changed = true;
                }

                if (!string.IsNullOrWhiteSpace(request.Note) && slotDetail.Note != request.Note)
                {
                    slotDetail.Note = request.Note;
                    changed = true;
                }

                if (request.Status.HasValue && slotDetail.Status != request.Status.Value)
                {
                    slotDetail.Status = request.Status.Value;
                    changed = true;
                }

                if (changed)
                {
                    slotDetail.UpdatedDate = DateTime.UtcNow;
                    await _unitOfWork.SlotDetailRepository.UpdateAsync(slotDetail);
                    await _unitOfWork.CompleteAsync();
                }
            }

            await _unitOfWork.CompleteAsync();
            return new ApiResponse<string>(true, "Slot detail updated successfully.");
        }

    }
}
