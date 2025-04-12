using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Booking
{
    public class BookingDto
    {
        public Guid Id { get; set; }

        // Thông tin sân
        public Guid FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAddress { get; set; }

        // Thời gian đặt
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // Giá cả
        public decimal FinalPrice { get; set; }

        // Chủ sân
        public string OwnerFullName { get; set; }
        public string OwnerPhone { get; set; }
        public string OwnerEmail { get; set; }

        // Customer
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }

        public string? Note { get; set; }
        public string? PriceImageUrl { get; set; }

        public string PayImageUrl { get; set; }

        public string? Status { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
