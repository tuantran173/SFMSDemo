﻿using AutoMapper;
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

                    Note = booking.Note,
                    PayImageUrl = booking.ImageUrl,

                    Status = booking.Status.ToString(),

                    CreatedDate = booking.CreatedDate,
                };

                result.Add(dto);
            }

            return result;
        }

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
                { "Note", string.IsNullOrWhiteSpace(request.Note) ? "Không có ghi chú" : request.Note },
                { "Price", booking.FinalPrice.ToString("N0") + " VND" },
                { "PayUrl", string.IsNullOrEmpty(booking.ImageUrl) ? "" : booking.ImageUrl },
                { "ManageLink", $"http://localhost:4200/shedule-owner/book/{booking.Id}" } // ✅ link đến chi tiết lịch
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

        public async Task<ApiResponse<string>> CancelBookingBySlotAsync(CancelBookingRequestDto request, Guid userId)
        {
            // Tìm booking tương ứng với slot + thời gian
            var booking = await _unitOfWork.BookingRepository
                .GetBookingBySlotAndDateAndTimeAsync(request.SlotId, request.Date, request.StartTime, request.EndTime);

            if (booking == null)
                return new ApiResponse<string>("Booking not found for this slot.");

            if (booking.UserId != userId)
                return new ApiResponse<string>("You are not authorized to cancel this booking.");

            // Lấy SlotDetail
            var slotDetail = await _unitOfWork.SlotDetailRepository
                .GetBySlotAndTimeAsync(request.SlotId, request.Date, request.StartTime, request.EndTime);

            if (slotDetail == null || slotDetail.Status != SlotStatus.Pending)
                return new ApiResponse<string>("Slot is not in a cancelable state.");

            // Tiến hành huỷ
            booking.Status = BookingStatus.Canceled;
            booking.UpdatedDate = DateTime.UtcNow;

            slotDetail.Status = SlotStatus.Available;
            slotDetail.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.BookingRepository.UpdateAsync(booking);
            await _unitOfWork.SlotDetailRepository.UpdateAsync(slotDetail);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<string>(true, "Booking canceled successfully.");
        }



        public async Task<ApiResponse<string>> ConfirmBookingAsync(Guid bookingId)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
                return new ApiResponse<string>("Không tìm thấy đơn đặt sân.");

            if (booking.Status != BookingStatus.Pending)
                return new ApiResponse<string>("Chỉ có thể xác nhận đơn đang chờ xử lý.");

            booking.Status = BookingStatus.Completed;
            booking.UpdatedDate = DateTime.UtcNow;

            // ✅ Update SlotDetail nếu có
            var slotDetail = await _unitOfWork.SlotDetailRepository
                .GetBySlotAndTimeAsync(booking.FacilityTimeSlotId, booking.BookingDate, booking.StartTime.Value, booking.EndTime.Value);

            if (slotDetail != null)
            {
                slotDetail.Status = SlotStatus.Booked;
                slotDetail.UpdatedDate = DateTime.UtcNow;
            }

            await _unitOfWork.BookingRepository.UpdateAsync(booking);
            await _unitOfWork.CompleteAsync();

            // ✅ Gửi mail xác nhận lại cho KH
            var customerEmail = booking.CustomerEmail;
            if (!string.IsNullOrWhiteSpace(customerEmail))
            {
                var placeholders = new Dictionary<string, string>
        {
            { "CustomerName", booking.CustomerName },
            { "FacilityName", booking.Facility?.Name ?? "" },
            { "BookingDate", booking.BookingDate.ToString("dd/MM/yyyy") },
            { "BookingTime", $"{booking.StartTime:hh\\:mm} - {booking.EndTime:hh\\:mm}" },
            { "Price", booking.FinalPrice.ToString("N0") + " VND" }
        };

                await _emailTemplateService.SendFromTemplateAsync(
                    "BookingConfirmed",
                    customerEmail,
                    placeholders
                );
            }

            return new ApiResponse<string>(true, "Xác nhận đơn đặt sân thành công.");
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


        // Calendar
        public async Task<ApiResponse<FacilityBookingCalendarDto>> GetCalendarForCustomerAsync(Guid facilityId, Guid? userId)
        {
            var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(facilityId);
            if (facility == null)
                return new ApiResponse<FacilityBookingCalendarDto>("Facility not found.");

            var today = DateTime.Today;
            var futureDays = 14;
            var timeSlots = await _unitOfWork.FacilityTimeSlotRepository.GetByFacilityIdAsync(facilityId);
            var bookings = await _unitOfWork.BookingRepository
                .GetBookingsByFacilityAsync(facilityId, today, today.AddDays(futureDays));

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

                            var slotDetail = await _unitOfWork.SlotDetailRepository
                                .GetBySlotAndTimeAsync(slot.Id, date, subStart, subEnd);

                            var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slot.Id);

                            SlotStatus status;

                            if (slotDetail != null)
                            {
                                status = slotDetail.Status;
                            }
                            else if (booking != null)
                            {
                                if (booking.Status == BookingStatus.Pending)
                                {
                                    if (userId.HasValue && booking.UserId == userId.Value)
                                        status = SlotStatus.Pending;
                                    else
                                        status = SlotStatus.Full;
                                }
                                else if (userId.HasValue && booking.UserId == userId.Value)
                                {
                                    status = SlotStatus.Booked;
                                }
                                else
                                {
                                    status = SlotStatus.Full;
                                }
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
                                Note = slotDetail?.Note ?? booking?.Note ?? "",
                                FinalPrice = slotDetail?.FinalPrice ?? price?.FinalPrice ?? 0,
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
                        var subStart = time;
                        var subEnd = time + slotDuration;

                        var booking = bookings.FirstOrDefault(b =>
                            b.BookingDate.Date == date &&
                            b.FacilityTimeSlotId == slot.Id &&
                            b.StartTime == subStart &&
                            b.EndTime == subEnd);

                        var slotDetail = await _unitOfWork.SlotDetailRepository
                            .GetBySlotAndTimeAsync(slot.Id, date, subStart, subEnd);

                        var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slot.Id);

                        var status = booking != null
                            ? SlotStatus.Full
                            : slotDetail?.Status ?? (slot.Status == SlotStatus.Closed ? SlotStatus.Closed : SlotStatus.Available);

                        calendarItems.Add(new FacilityBookingSlotDto
                        {
                            SlotId = slot.Id,
                            StartTime = subStart,
                            EndTime = subEnd,
                            StartDate = date,
                            EndDate = date,
                            Status = status,
                            Note = slotDetail?.Note ?? booking?.Note ?? "",
                            FinalPrice = slotDetail?.FinalPrice ?? price?.FinalPrice ?? 0
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

        public async Task<ApiResponse<FacilityBookingCalendarDto>> GetFacilityCalendarAsync(Guid facilityId, Guid? userId = null)
        {
            var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(facilityId);
            if (facility == null)
                return new ApiResponse<FacilityBookingCalendarDto>("Facility not found.");

            var today = DateTime.Today;
            var futureDays = 14;

            var timeSlots = await _unitOfWork.FacilityTimeSlotRepository.GetByFacilityIdAsync(facilityId);
            var bookings = await _unitOfWork.BookingRepository
                .GetBookingsByFacilityAsync(facilityId, today, today.AddDays(futureDays));

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

                            var slotDetail = await _unitOfWork.SlotDetailRepository
                                .GetBySlotAndTimeAsync(slot.Id, date, subStart, subEnd);

                            var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slot.Id);

                            SlotStatus status;

                            // ✅ Ưu tiên SlotDetail.Status nếu có
                            if (slotDetail != null)
                            {
                                status = slotDetail.Status;
                            }
                            else if (booking != null)
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
                                Note = slotDetail?.Note ?? booking?.Note ?? "",
                                FinalPrice = slotDetail?.FinalPrice ?? price?.FinalPrice ?? 0,
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



        public async Task<ApiResponse<SlotDetailDto>> GetCalendarSlotDetailAsync(
    Guid slotId,
    DateTime date,
    TimeSpan startTime,
    TimeSpan endTime)
        {
            var slot = await _unitOfWork.FacilityTimeSlotRepository.GetByIdWithFacilityAndOwnerAsync(slotId);
            if (slot == null)
                return new ApiResponse<SlotDetailDto>("Slot not found.");

            if (startTime < slot.StartTime || endTime > slot.EndTime)
                return new ApiResponse<SlotDetailDto>("Time range doesn't belong to this slot.");

            // Lấy slot detail trước để ưu tiên trạng thái từ chủ sân
            var slotDetail = await _unitOfWork.SlotDetailRepository
                .GetBySlotAndTimeAsync(slot.Id, date, startTime, endTime);

            var booking = await _unitOfWork.BookingRepository
                .GetBookingBySlotAndDateAndTimeAsync(slotId, date, startTime, endTime);

            var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slotId);

            var facility = slot.Facility;
            var owner = await _userManager.FindByIdAsync(facility.OwnerId.ToString());

            // ✅ Ưu tiên SlotDetail.Status nếu có
            SlotStatus status;
            if (slotDetail != null)
            {
                status = slotDetail.Status;
            }
            else if (booking != null)
            {
                if (booking.Status == BookingStatus.Completed)
                    status = SlotStatus.Booked;
                else if (booking.Status == BookingStatus.Pending)
                    status = SlotStatus.Pending;
                else
                    status = slot.Status;
            }
            else
            {
                status = slot.Status;
            }

            var dto = new SlotDetailDto
            {
                SlotId = slot.Id,
                StartTime = startTime,
                EndTime = endTime,
                StartDate = date,
                EndDate = date,
                Status = status,
                Note = slotDetail?.Note ?? booking?.Note ?? string.Empty,

                BasePrice = price?.BasePrice ?? 0,
                Coefficient = price?.Coefficient ?? 1,
                Deposit = (price?.FinalPrice ?? 0) * 0.3m,
                FinalPrice = slotDetail?.FinalPrice ?? price?.FinalPrice ?? 0,
                PriceImageUrl = price?.ImageUrl ?? string.Empty,

                FacilityName = facility?.Name ?? string.Empty,
                FacilityAddress = facility?.Address ?? string.Empty,
                FacilityImageUrl = facility?.ImageUrl ?? string.Empty,
                OwnerFullName = owner?.FullName ?? string.Empty,
                OwnerUserName = owner?.UserName ?? string.Empty,
                OwnerPhone = owner?.Phone ?? string.Empty
            };

            return new ApiResponse<SlotDetailDto>(dto);
        }


        //public async Task<ApiResponse<string>> UpdateSlotStatusByOwnerAsync(Guid slotId, DateTime date, TimeSpan startTime, TimeSpan endTime, SlotStatus status)
        //{
        //    var slot = await _unitOfWork.FacilityTimeSlotRepository.GetByIdAsync(slotId);
        //    if (slot == null)
        //        return new ApiResponse<string>("Slot not found.");

        //    if (startTime < slot.StartTime || endTime > slot.EndTime)
        //        return new ApiResponse<string>("Time range is outside the defined slot.");

        //    var slotDetail = await _unitOfWork.SlotDetailRepository
        //        .GetBySlotAndTimeAsync(slotId, date, startTime, endTime);

        //    // ❌ Nếu slot đang ở trạng thái Booked thì không được update
        //    if (slotDetail != null && slotDetail.Status == SlotStatus.Booked)
        //        return new ApiResponse<string>("This slot is already booked and cannot be updated.");

        //    if (slotDetail == null)
        //    {
        //        slotDetail = new SlotDetail
        //        {
        //            Id = Guid.NewGuid(),
        //            SlotId = slotId,
        //            Date = date,
        //            StartTime = startTime,
        //            EndTime = endTime,
        //            Status = status,
        //            CreatedDate = DateTime.UtcNow
        //        };
        //        await _unitOfWork.SlotDetailRepository.AddAsync(slotDetail);
        //    }
        //    else
        //    {
        //        slotDetail.Status = status;
        //        slotDetail.UpdatedDate = DateTime.UtcNow;
        //    }

        //    await _unitOfWork.CompleteAsync();
        //    return new ApiResponse<string>(true, "Slot status updated successfully.");
        //}
        public async Task<ApiResponse<string>> UpdateSlotStatusByOwnerAsync(
    Guid slotId, DateTime date, TimeSpan startTime, TimeSpan endTime, SlotStatus status)
        {
            var slot = await _unitOfWork.FacilityTimeSlotRepository.GetByIdAsync(slotId);
            if (slot == null)
                return new ApiResponse<string>("Slot not found.");

            if (startTime < slot.StartTime || endTime > slot.EndTime)
                return new ApiResponse<string>("Time range is outside the defined slot.");

            var slotDetail = await _unitOfWork.SlotDetailRepository
                .GetBySlotAndTimeAsync(slotId, date, startTime, endTime);

            bool isChangedToBooked = false;

            if (slotDetail == null)
            {
                slotDetail = new SlotDetail
                {
                    Id = Guid.NewGuid(),
                    SlotId = slotId,
                    Date = date,
                    StartTime = startTime,
                    EndTime = endTime,
                    Status = status,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };
                await _unitOfWork.SlotDetailRepository.AddAsync(slotDetail);

                if (status == SlotStatus.Booked)
                    isChangedToBooked = true;
            }
            else
            {
                if (slotDetail.Status == SlotStatus.Pending && status == SlotStatus.Booked)
                    isChangedToBooked = true;

                slotDetail.Status = status;
                slotDetail.UpdatedDate = DateTime.UtcNow;
                await _unitOfWork.SlotDetailRepository.UpdateAsync(slotDetail);
            }

            // ✅ Nếu chuyển sang Booked thì xử lý booking + gửi mail
            if (isChangedToBooked)
            {
                var booking = await _unitOfWork.BookingRepository
                    .GetBookingBySlotAndDateAndTimeAsync(slotId, date, startTime, endTime);

                if (booking != null && booking.Status == BookingStatus.Pending)
                {
                    booking.Status = BookingStatus.Completed;
                    booking.UpdatedDate = DateTime.UtcNow;
                    await _unitOfWork.BookingRepository.UpdateAsync(booking);

                    var customer = await _userManager.FindByIdAsync(booking.UserId.ToString());
                    var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(slot.FacilityId);

                    if (customer != null && !string.IsNullOrEmpty(customer.Email))
                    {
                        var placeholders = new Dictionary<string, string>
                {
                    { "CustomerName", customer.FullName },
                    { "FacilityName", facility?.Name ?? "Sân thể thao" },
                    { "FacilityAddress", facility?.Address ?? "Địa chỉ chưa cập nhật" },
                    { "BookingDate", date.ToString("dd/MM/yyyy") },
                    { "BookingTime", $"{startTime:hh\\:mm} - {endTime:hh\\:mm}" },
                    { "Price", booking.FinalPrice.ToString("N0") + " VND" },
                    { "Note", string.IsNullOrWhiteSpace(booking.Note) ? "Không có ghi chú" : booking.Note },
                    { "PayUrl", string.IsNullOrEmpty(booking.ImageUrl) ? "" : booking.ImageUrl }
                };

                        Console.WriteLine($"Gửi email xác nhận đặt sân thành công đến: {customer.Email}");

                        var emailSent = await _emailTemplateService.SendFromTemplateAsync(
                            templateName: "BookingConfirmed",
                            toEmail: customer.Email,
                            placeholders: placeholders
                        );

                        Console.WriteLine($"Gửi mail xác nhận cho khách: {(emailSent.Success ? "✅ Thành công" : "❌ Thất bại")} - {emailSent.Message}");
                    }
                }
            }

            await _unitOfWork.CompleteAsync();
            return new ApiResponse<string>(true, "Slot status updated successfully.");
        }



    }
}
