using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Booking.Request
{
    public class BookingByGuestRequestDto
    {
        public Guid FacilityTimeSlotId { get; set; } // slot tổng
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string PaymentMethod { get; set; } // e.g. "Cash", "VNPay"
        public string? Note { get; set; }
    }
}
