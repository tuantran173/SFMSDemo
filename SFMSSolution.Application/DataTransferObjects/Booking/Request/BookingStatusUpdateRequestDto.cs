using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Booking.Request
{
    public class BookingStatusUpdateRequestDto
    {
        public Guid BookingId { get; set; }
        public string Status { get; set; } = string.Empty; // Confirmed / Cancelled
    }
}
